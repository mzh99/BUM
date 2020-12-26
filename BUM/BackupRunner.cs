using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OCSS.Util.DirSearch;

namespace BUM {

   public class BackupRunner {

      private static readonly int DebugEntriesBeforeWriting = 100_000;  // should be in the neighborhood of debugSB.capacity / 250
      public static readonly string HistoryFolderName = "BumHistory";
      public static readonly string TrashFolderName = "BumTrash";
      public static readonly string StatsFilePrefix = "BumStats";
      public static readonly string ErrorFilePrefix = "BumErrors";
      public static readonly string DebugFilePrefix = "BumDebug";
      public static readonly string LogFileExt = ".txt";
      public static readonly string FileNameSegmentSep = "_";

      public bool CancelFlag { get; set; }
      public int JobID { get; private set; }
      public BackupSettings Settings { get; private set; }
      public BackupStats Stats { get; private set; }
      public string RunError { get; private set; }
      public bool RanSuccessfully { get; private set; }

      public delegate void FileHit(FileInfo fileInfo, ref bool cancelFlag);
      public delegate void FolderHit(DirectoryInfo folderInfo, ref bool cancelFlag);
      public event FileHit OnFileHit;
      public event FolderHit OnFolderHit;

      private readonly Dictionary<string, SourceFolderExclude> knownFolderExcludeDict = new Dictionary<string, SourceFolderExclude>();
      private readonly Dictionary<string, CompiledExclusionRule> globalExcludeDict = new Dictionary<string, CompiledExclusionRule>();
      // processed files reverse-lookup; used for delete detection
      private readonly Dictionary<string, string> processedFileDict = new Dictionary<string, string>();

      private BackupJob job = null;
      private int debugEntryCnt;
      private int currentSrcNdx;
      private List<SourceDef> sourcesToBackup;
      private DirSearch dirSearch;
      private readonly List<string> prefixesToSkipForDeleteDetection = new List<string>();
      private readonly List<string> foldersToSkipForDeleteDetection = new List<string>();

      private readonly List<string> errList = new List<string>();
      private readonly StringBuilder debugSB = new StringBuilder(1024 * 1024 * 25);    // rough estimate of 25MB buffer

      public BackupRunner(BackupSettings settings, int jobID) {
         if (settings == null)
            throw new ArgumentNullException("Settings must be specified.");
         Settings = settings;
         JobID = jobID;
         CancelFlag = false;
         RunError = string.Empty;
         RanSuccessfully = true;
      }

      private bool Validate(bool simMode, bool debugMode) {
         RunError = string.Empty;
         Stats = new BackupStats { JobID = this.JobID, RunInSimMode = simMode, RunInDebugMode = debugMode };
         job = Settings.Jobs.FirstOrDefault<BackupJob>(j => j.ID == JobID);
         if (job == null) {
            var eligibleList = string.Join(", ", Settings.Jobs.Select(j => j.ID.ToString()));
            RunError = $"Job {JobID} is an invalid Backup Job ID. Try one of: {eligibleList}";
            return false;
         }
         // destination folder has JobID appended
         string destOutPath = Path.Combine(job.Destination, JobID.ToString());
         // append trailing slash for small performance improvement over time so it's not constantly added for each filename grafted later
         if (destOutPath[destOutPath.Length - 1] != Path.DirectorySeparatorChar)
            destOutPath += Path.DirectorySeparatorChar;
         // set the destination root, then the dependent log and stats file names
         Stats.DestPathRoot = destOutPath;
         Stats.ErrLogFileName = GetDestRootFileNameLog(ErrorFilePrefix);
         Stats.StatsLogFileName = GetDestRootFileNameLog(StatsFilePrefix);
         Stats.DebugFileName = GetDestRootFileNameLog(DebugFilePrefix);

         // setup folder skips for delete detection. The paths must have a trailing slash so matching works correctly
         foldersToSkipForDeleteDetection.Clear();
         foldersToSkipForDeleteDetection.Add(Path.Combine(Stats.DestPathRoot, HistoryFolderName) + Path.DirectorySeparatorChar);
         foldersToSkipForDeleteDetection.Add(Path.Combine(Stats.DestPathRoot, TrashFolderName) + Path.DirectorySeparatorChar);

         // build a list of known Bum files to skip (based on prefix convention) for delete detection
         prefixesToSkipForDeleteDetection.Clear();
         prefixesToSkipForDeleteDetection.Add(Stats.DestPathRoot + ErrorFilePrefix + FileNameSegmentSep);
         prefixesToSkipForDeleteDetection.Add(Stats.DestPathRoot + StatsFilePrefix + FileNameSegmentSep);
         prefixesToSkipForDeleteDetection.Add(Stats.DestPathRoot + DebugFilePrefix + FileNameSegmentSep);
         // skip any Settings_* backup files
         prefixesToSkipForDeleteDetection.Add(Stats.DestPathRoot + BackupSettings.SettingsFilenameBase + FileNameSegmentSep);

         // make sure all folder structures exist including special folders for BUM
         FileUtils.CreateDestFolderStructure(Stats.DestPathRoot);
         if (Stats.RunInDebugMode)
            AddDebugEntryToLog($"Creating root output folder (if not present): {Stats.DestPathRoot}");
         foreach (var folderName in foldersToSkipForDeleteDetection) {
            FileUtils.CreateDestFolderStructure(folderName);
            if (Stats.RunInDebugMode)
               AddDebugEntryToLog($"Creating special output folder (if not present): {folderName}");
         }
         sourcesToBackup = new List<SourceDef>();
         foreach (var id in job.SourceIDs) {
            SourceDef srcDef = Settings.SourceDefs.FirstOrDefault(s => s.ID == id);
            if (srcDef == null) {
               RunError = $"Job {JobID} references an invalid source ID {id} that is not found in the current settings";
               return false;
            }
            sourcesToBackup.Add(srcDef);
            // create dictionary lookup of "SouceID-Foldername" for each known exclusion folder
            foreach (var folder in srcDef.KnownFolderExcludes) {
               knownFolderExcludeDict.Add(MakeFolderExcludeDictKey(srcDef.ID, folder.Loc), folder);
            }
            for (int ndx = 0; ndx < srcDef.GlobalExcludeRules.Count; ndx++) {
               // global rule lookup id will use SourceID-RuleListIndex
               CompiledExclusionRule excRule = new CompiledExclusionRule(srcDef.GlobalExcludeRules[ndx]);
               if (excRule.HasErrors) {
                  RunError = $"Job {JobID} and Backup Source ID {srcDef.ID} have compile errors in rule {ndx + 1}";
                  return false;
               }
               globalExcludeDict.Add(MakeCompiledCodeDictKey(srcDef.ID, ndx), excRule);
            }
         }

         return true;
      }

      /// <summary>Run a backup</summary>
      /// <param name="simMode">flag for simulation mode</param>
      /// <param name="debugMode">flag for debug mode</param>
      /// <returns>an empty string for success. Otherwise, an error message for a fatal error encountered prior to the run.</returns>
      /// <remarks>
      ///   Note: All I/O errors and anticipated exceptions encountered during the backup are captured in the error log.
      /// </remarks>
      public void Run(bool simMode, bool debugMode) {
         RanSuccessfully = false;
         SetupPrep();
         if (Validate(simMode, debugMode) == false) {
            // a serious validation error occurred prior to job; we cannot continue nor can we log anything safely
            return;
         }
         dirSearch = new DirSearch();
         AddSearchDelegates();
         for (currentSrcNdx = 0; currentSrcNdx < sourcesToBackup.Count; currentSrcNdx++) {
            // process each backup source
            dirSearch.SearchMask = DirSearch.SearchMaskAllFilesAndFolders;
            dirSearch.BaseDir = sourcesToBackup[currentSrcNdx].StartingFolder;
            dirSearch.SearchType = AttrSearchType.IgnoreAttributeMatch;
            dirSearch.FileAttribs = DirSearch.AllAttributes;   // not really needed currently as we are set to IgnoreAttributeMatch
            dirSearch.ProcessSubs = sourcesToBackup[currentSrcNdx].TraverseSubs;
            dirSearch.Execute();
            if (CancelFlag)
               break;
         }
         RemoveSearchDelegates();
         if (CancelFlag == false && TrackDeletes()) {
            CheckDeletes();
         }
         Stats.DtTmEnded = DateTime.Now;
         WriteLogs();
         RanSuccessfully = true;
      }

      private string GetDestRootFileNameLog(string prefix) {
         if (string.IsNullOrEmpty(prefix))
            throw new ArgumentException("prefix must not be empty or null");
         return Stats.DestPathRoot + prefix + FileNameSegmentSep + Stats.JobStamp + LogFileExt;
      }

      private void WriteLogs() {
         // flush debug log first as it can error and write en entry to the error log which should be saved after
         if (Stats.RunInDebugMode)
            AppendDebugLog(); // write any cached leftover debug entries
         ExportErrors();
         ExportStats();
      }

      private void SetupPrep() {
         processedFileDict.Clear();
         globalExcludeDict.Clear();
         knownFolderExcludeDict.Clear();
         ClearDebugBuffer();
         errList.Clear();
         CancelFlag = false;
      }

      private void RemoveSearchDelegates() {
         dirSearch.OnFileMatch -= OnFileInitialMatch;
         dirSearch.OnFolderMatch -= OnFolderInitialMatch;
         dirSearch.OnFileExcept -= OnFileException;
         dirSearch.OnFolderExcept -= OnFolderException;
         dirSearch.OnFolderFilter -= OnFolderFilter;
         dirSearch.OnFileFilter -= OnFileFilter;
      }

      private void AddSearchDelegates() {
         dirSearch.OnFileMatch += OnFileInitialMatch;
         dirSearch.OnFolderMatch += OnFolderInitialMatch;
         dirSearch.OnFileExcept += OnFileException;
         dirSearch.OnFolderExcept += OnFolderException;
         dirSearch.OnFolderFilter += OnFolderFilter;
         dirSearch.OnFileFilter += OnFileFilter;
      }

      private bool TrackDeletes() {
         // Only run delete-detection in non simulation mode and with specific DeleteDetectHandling modes
         return !(Stats.RunInSimMode || job.Options.DeleteHandling == DeleteDetectHandling.LeaveAloneOnDest);
      }

      private string MakeFolderExcludeDictKey(int srcID, string folder) {
         string key = srcID.ToString() + "-" + folder;
         // folders processed from MHDirSearch have a separator character appended, so make sure ours matches
         if (key[key.Length - 1] != Path.DirectorySeparatorChar)
            key += Path.DirectorySeparatorChar;
         return key;
      }

      private string MakeCompiledCodeDictKey(int srcID, int ruleNdx) {
         return srcID.ToString() + "-" + ruleNdx.ToString();
      }

      /// <summary>Delete handling for destination folder where file is no longer on source</summary>
      /// <remarks>
      ///   Reverse lookup for delete-detection using list of scanned files and destination folder
      ///   - only called when settings for job require delete-detection
      ///   - foldersToSkipForDeleteDetection contains the list of trash and History folders to be skipped.
      ///   - a file filter is not used to skip BUM error and stat logs as it's done directly in OnDelFileInitialMatch() using prefixesToSkipForDeleteDetection
      /// </remarks>
      private void CheckDeletes() {
         DirSearch delSearch = new DirSearch("*.*", Stats.DestPathRoot, AttrSearchType.AnyMatch, DirSearch.AllAttributesMinusSysAndHidden, true);
         delSearch.OnFileMatch += OnDelFileInitialMatch;
         delSearch.OnFileExcept += OnDelFileException;
         delSearch.OnFolderExcept += OnDelFolderException;
         // Folder filtering is used to skip History and Trash folders
         delSearch.OnFolderFilter += OnDelFolderFilter;
         delSearch.Execute();
         delSearch.OnFileMatch -= OnDelFileInitialMatch;
         delSearch.OnFileExcept -= OnDelFileException;
         delSearch.OnFolderExcept -= OnDelFolderException;
         delSearch.OnFolderFilter -= OnDelFolderFilter;
         processedFileDict.Clear();    // free memory as it can be substantial
      }

      private void ExportStats() {
         const int colLen = 25;
         StringBuilder sb = new StringBuilder();
         sb.AppendLine("Job ID:".PadRight(colLen) + Stats.JobID.ToString());
         sb.AppendLine("Debug Mode:".PadRight(colLen) + Stats.RunInDebugMode.ToString());
         sb.AppendLine("Cancelled by User:".PadRight(colLen) + CancelFlag.ToString());
         sb.AppendLine("Destination Root:".PadRight(colLen) + Stats.DestPathRoot);
         sb.AppendLine("Started:".PadRight(colLen) + Stats.DtTmStarted.ToString());
         sb.AppendLine("Ended:".PadRight(colLen) +  Stats.DtTmEnded.ToString());
         sb.AppendLine("Duration:".PadRight(colLen) + Stats.JobSpanAsStr);
         sb.AppendLine("Errors:".PadRight(colLen) + Stats.NumErrors.ToString());
         if (Stats.NumErrors > 0)
            sb.AppendLine("".PadRight(colLen) + "(see associated error log for details)");
         sb.AppendLine("Warnings:".PadRight(colLen) + Stats.NumWarnings.ToString());
         sb.AppendLine("Excluded Folders:".PadRight(colLen) + Stats.FoldersExcluded.ToString());
         sb.AppendLine("Excluded Files:".PadRight(colLen) + Stats.FilesExcluded.ToString());
         sb.AppendLine("Scanned Folders:".PadRight(colLen) + Stats.FoldersScanned.ToString());
         sb.AppendLine("Scanned Files:".PadRight(colLen) + Stats.FilesScanned.ToString());
         sb.AppendLine("Size of Files Scanned:".PadRight(colLen) + $"{Stats.VolumeScanned} ({FileSizeHelper.LongAsSizeStrBestApprox(Stats.VolumeScanned)})");
         if (Stats.RunInSimMode) {
            sb.AppendLine("Simulation Mode:".PadRight(colLen) + Stats.RunInSimMode.ToString());
            sb.AppendLine("Number of Sim Adds:".PadRight(colLen) + Stats.SimNumAdds.ToString());
            sb.AppendLine("Number of Sim Updates:".PadRight(colLen) + Stats.NumUpdates.ToString());
            sb.AppendLine("Size of Sim Adds:".PadRight(colLen) + $"{Stats.SimVolumeAdds} ({FileSizeHelper.LongAsSizeStrBestApprox(Stats.SimVolumeAdds)})");
            sb.AppendLine("Size of Sim Updates:".PadRight(colLen) + $"{Stats.SimVolumeUpdates} ({FileSizeHelper.LongAsSizeStrBestApprox(Stats.SimVolumeUpdates)})");
         }
         else {
            sb.AppendLine("Number of File Adds:".PadRight(colLen) + Stats.NumAdds.ToString());
            sb.AppendLine("Number of File Updates:".PadRight(colLen) + Stats.NumUpdates.ToString());
            sb.AppendLine("Number of File Deletes:".PadRight(colLen) + Stats.NumDeletes.ToString());
            sb.AppendLine("Size of File Adds:".PadRight(colLen) + $"{Stats.VolumeAdds} ({FileSizeHelper.LongAsSizeStrBestApprox(Stats.VolumeAdds)})");
            sb.AppendLine("Size of File Updates:".PadRight(colLen) + $"{Stats.VolumeUpdates} ({FileSizeHelper.LongAsSizeStrBestApprox(Stats.VolumeUpdates)})");
            sb.AppendLine("Size of File Deletes:".PadRight(colLen) + $"{Stats.VolumeDeletes} ({FileSizeHelper.LongAsSizeStrBestApprox(Stats.VolumeDeletes)})");
         }
         File.WriteAllText(Stats.StatsLogFileName, sb.ToString(), PathUtils.UTFWithNoBom);
      }

      private void ExportErrors() {
         if (errList.Count > 0) {
            File.WriteAllLines(Stats.ErrLogFileName, errList, PathUtils.UTFWithNoBom);
         }
      }

      private void AddError(string errMsg) {
         errList.Add(errMsg);
         Stats.NumErrors++;
         if (Stats.NumErrors >= job.MaxErrors) {
            CancelFlag = true;
         }
      }

      private void AddDebugEntryToLog(string msg) {
         if (Stats.RunInDebugMode) {
            debugSB.AppendLine(msg);
            if (++debugEntryCnt >= DebugEntriesBeforeWriting) {
               AppendDebugLog();
            }
         }
      }

      private void AppendDebugLog() {
         if (Stats.RunInDebugMode && debugEntryCnt > 0) {
            string errMsg = FileUtils.AppendTextToFile(debugSB.ToString(), Stats.DebugFileName);
            if (errMsg != string.Empty)
               AddError($"Error writing debug log segment. Msg: {errMsg}");
            ClearDebugBuffer();
         }
      }

      private void ClearDebugBuffer() {
         debugSB.Clear();
         debugEntryCnt = 0;
      }

      #region File Delete Detection Delegate/Event functions

      private void OnDelFolderFilter(DirectoryInfo dirInfo, ref bool skip, ref bool skipChildFolders) {
         if (foldersToSkipForDeleteDetection.Any(f => dirInfo.FullName == f)) {
            skip = true;
            skipChildFolders = true;
            if (Stats.RunInDebugMode)
               AddDebugEntryToLog($"Skipping special BUM folder during delete-detection: {dirInfo.FullName}");
         }
      }

      private void OnDelFolderException(string errorMsg) {
         AddError($"Folder error during delete detection. Msg: {errorMsg}");
      }

      private void OnDelFileException(string errorMsg) {
         AddError($"File error during delete detection. Msg: {errorMsg}");
      }

      private void OnDelFileInitialMatch(FileInfo fileInfo, ref bool cancelFlag) {
         cancelFlag = CancelFlag;
         if (cancelFlag)
            return;
         if (prefixesToSkipForDeleteDetection.Any(s => fileInfo.FullName.StartsWith(s))) {
            if (Stats.RunInDebugMode)
               AddDebugEntryToLog($"Skipping special BUM file during delete-detection: {fileInfo.FullName}");
         }
         else {
            // check if file is not in prior scanned list
            if (processedFileDict.ContainsKey(fileInfo.FullName) == false) {
               // this is a file eligible to delete or archive; we're skipping the other options as they were filtered earlier in code
               Stats.NumDeletes++;
               Stats.VolumeDeletes += fileInfo.Length;
               switch (job.Options.DeleteHandling) {
                  case DeleteDetectHandling.DeleteFromDest:
                     if (Stats.RunInDebugMode)
                        AddDebugEntryToLog($"Deleted file {fileInfo.FullName} based on delete-detection handling setting");
                     string delErrMsg = FileUtils.DeleteFile(fileInfo.FullName);
                     if (delErrMsg != string.Empty) {
                        AddError(delErrMsg);
                        return;
                     }
                     break;
                  case DeleteDetectHandling.MoveToDeleteArchive:
                     // Perform a copy, then delete for more of an atomic operation as an error could delete the source file using File.Move()
                     string arcFile = PathUtils.GetVersionedTrashFileName(fileInfo.FullName, TrashFolderName, Stats.DestPathRoot, Stats.JobStamp, FileNameSegmentSep);
                     if (Stats.RunInDebugMode)
                        AddDebugEntryToLog($"Delete-detection found file {fileInfo.FullName} and copied to Trash folder as: {arcFile}");
                     string copyErrMsg = FileUtils.CopyFile(fileInfo.FullName, arcFile, true, true, true);
                     if (copyErrMsg != string.Empty) {
                        AddError($"File error copying file into trashcan. Msg: {copyErrMsg}");
                        return;
                     }
                     delErrMsg = FileUtils.DeleteFile(fileInfo.FullName);
                     if (delErrMsg != string.Empty) {
                        AddError(delErrMsg);
                        return;
                     }
                     if (Stats.RunInDebugMode)
                        AddDebugEntryToLog($"Deleted {fileInfo.FullName} after successfully copying to Trash folder");
                     break;
               }
            }
         }
      }

      #endregion

      #region File Search Delegate/Event functions

      private void OnFileFilter(FileInfo fileInfo, ref bool skip) {
         string err;
         GlobalScriptVars glob = new GlobalScriptVars(fileInfo, Stats.DtTmStarted);
         // Check if any of the global exclude rules tell us to skip processing on file
         // If any of the rules indicate skip, set flag and bail out
         for (int ndx = 0; ndx < sourcesToBackup[currentSrcNdx].GlobalExcludeRules.Count; ndx++) {
            string key = MakeCompiledCodeDictKey(sourcesToBackup[currentSrcNdx].ID, ndx);
            CompiledExclusionRule rule = globalExcludeDict[key];
            if (rule.AppliesTo == RuleAppliesTo.Files) {
               bool doSkip = ScriptRunner.ExecuteCode(rule.CompiledCode, glob, out err);
               if (err == string.Empty) {
                  if (doSkip) {
                     Stats.FilesExcluded++;
                     skip = doSkip;
                     if (Stats.RunInDebugMode)
                        AddDebugEntryToLog($"Scripting rule {ndx} triggered skip of file: {fileInfo.FullName}");
                     return;
                  }
               }
               else {
                  AddError($"File: {fileInfo.FullName} failed to process compiled script exclusion rule {ndx} on backup source {currentSrcNdx}");
                  skip = true;
                  if (Stats.RunInDebugMode)
                     AddDebugEntryToLog($"Auto-skipped file {fileInfo.FullName} due to scripting compile/execution error on rule {ndx}");
                  return;
               }
            }
         }
      }

      /// <summary>Custom folder filtering event. We use it for fast dictionary lookup of excluded names for each Source Backup ID</summary>
      /// <param name="folderInfo">DirectoryInfo representing the folder being processed</param>
      /// <param name="skip">set skip flag to inform DirSearch component to skip the current folder</param>
      /// <param name="skipChildFolders">set skipChildFolders flag to inform DirSearch component to skip the current folder's children</param>
      private void OnFolderFilter(DirectoryInfo folderInfo, ref bool skip, ref bool skipChildFolders) {
         string err;
         // check known folder excludes first
         string key = MakeFolderExcludeDictKey(sourcesToBackup[currentSrcNdx].ID, folderInfo.FullName);
         if (knownFolderExcludeDict.ContainsKey(key)) {
            skip = true;
            skipChildFolders = knownFolderExcludeDict[key].AllSubs;
            Stats.FoldersExcluded++;
            if (Stats.RunInDebugMode) {
               if (skipChildFolders) {
                  AddDebugEntryToLog($"Fixed folder rule triggered skip of folder (and subfolders): {folderInfo.FullName}");
               }
               else {
                  AddDebugEntryToLog($"Fixed folder rule triggered skip of folder: {folderInfo.FullName}");
               }
            }
            return;
         }
         // check folder-based rules
         GlobalScriptVars glob = new GlobalScriptVars(folderInfo, Stats.DtTmStarted);
         // Check if any of the global exclude rules tell us to skip processing on folder
         // If any of the rules indicate skip, set flag and bail out of folder and subfolders
         for (int ndx = 0; ndx < sourcesToBackup[currentSrcNdx].GlobalExcludeRules.Count; ndx++) {
            key = MakeCompiledCodeDictKey(sourcesToBackup[currentSrcNdx].ID, ndx);
            CompiledExclusionRule rule = globalExcludeDict[key];
            if (rule.AppliesTo == RuleAppliesTo.FolderAndSubfolders || rule.AppliesTo == RuleAppliesTo.ParentFolderOnly) {
               bool doSkip = ScriptRunner.ExecuteCode(rule.CompiledCode, glob, out err);
               if (err == string.Empty) {
                  if (doSkip) {
                     skip = doSkip;
                     skipChildFolders = (rule.AppliesTo == RuleAppliesTo.FolderAndSubfolders);
                     Stats.FoldersExcluded++;
                     if (Stats.RunInDebugMode) {
                        if (skipChildFolders) {
                           AddDebugEntryToLog($"Scripting rule {ndx} triggered skip of folder (and subfolders): {folderInfo.FullName}");
                        }
                        else {
                           AddDebugEntryToLog($"Scripting rule {ndx} triggered skip of folder: {folderInfo.FullName}");
                        }
                     }
                     return;
                  }
               }
               else {
                  AddError($"Folder: {folderInfo.FullName} failed to process compiled script exclusion rule {ndx} on backup source {currentSrcNdx}");
                  skip = true;
                  if (Stats.RunInDebugMode)
                     AddDebugEntryToLog($"Auto-skipped folder {folderInfo.FullName} due to scripting compile/execution error on rule {ndx}");
                  return;
               }
            }
         }
      }

      private void OnFolderException(string errorMsg) {
         AddError($"Folder error during scanning. Msg: {errorMsg}");
      }

      private void OnFileException(string errorMsg) {
         AddError($"File error during scanning. Msg: {errorMsg}");
      }

      private void OnFolderInitialMatch(DirectoryInfo folderInfo, ref bool cancelFlag) {
         Stats.FoldersScanned++;
         cancelFlag = CancelFlag;
         if (cancelFlag)
            return;
         OnFolderHit?.Invoke(folderInfo, ref cancelFlag);
         if (Stats.RunInDebugMode)
            AddDebugEntryToLog($"Scanned folder: {folderInfo.FullName}");
      }

      private void OnFileInitialMatch(FileInfo fileInfo, ref bool cancelFlag) {
         Stats.FilesScanned++;
         Stats.VolumeScanned += fileInfo.Length;
         cancelFlag = CancelFlag;
         if (cancelFlag)
            return;
         string destFileName = PathUtils.GraftPath(fileInfo.FullName, Stats.DestPathRoot);
         if (Stats.RunInDebugMode)
            AddDebugEntryToLog($"Scanned file: {fileInfo.FullName} has destination path: {destFileName}");
         if (destFileName.Length >= PathUtils.MaxFileNameLength) {
            AddError($"Resulting file name: {destFileName} would exceed file system limitation. File skipped.");
         }
         else {
            if (TrackDeletes()) {
               processedFileDict.Add(destFileName, fileInfo.FullName);  // add a processed file record
            }
            if (File.Exists(destFileName)) {
               // Potentially, files could have matching file creation, last-update times, and sizes but the contents could be different.
               // We are not checking the contents in this program if those things all match.
               // Alternately, we could check contents if the archive bit is set on source file, but that bit can be set incorrectly by other programs.
               if (FileUtils.FileInfoDiffers(fileInfo, destFileName)) {
                  if (Stats.RunInDebugMode)
                     AddDebugEntryToLog($"Change detected on file: {fileInfo.FullName}");
                  if (job.Options.MaintainHistoryFolder && Stats.RunInSimMode == false) {
                     // file is different and maintain history flag is on so we'll version and archive a copy before we overwrite
                     string versionedFile = PathUtils.GetVersionedHistFileName(fileInfo.FullName, HistoryFolderName, Stats.DestPathRoot, Stats.JobStamp, FileNameSegmentSep);
                     if (Stats.RunInDebugMode)
                        AddDebugEntryToLog($"Copying file {fileInfo.FullName} to versioned history named: {versionedFile}");
                     string errMsg = FileUtils.CopyFile(destFileName, versionedFile, true, true, true);
                     if (errMsg != string.Empty) {
                        AddError($"File error copying versioned file into history. Msg: {errMsg}");
                        return;
                     }
                  }
                  if (Stats.RunInSimMode) {
                     Stats.SimNumUpdates++;
                     Stats.SimVolumeUpdates += fileInfo.Length;
                  }
                  else {
                     // overwrite existing file with newest version (doesn't apply to simulation mode)
                     if (Stats.RunInDebugMode)
                        AddDebugEntryToLog($"Copying file {fileInfo.FullName} in overwrite mode to: {destFileName}");
                     string errMsg = FileUtils.CopyFile(fileInfo.FullName, destFileName, true, true, false);
                     if (errMsg != string.Empty) {
                        AddError($"File error copying file with overwrite. Msg: {errMsg}");
                        return;
                     }
                     Stats.NumUpdates++;
                     Stats.VolumeUpdates += fileInfo.Length;
                  }
               }
               else {
                  if (Stats.RunInDebugMode)
                     AddDebugEntryToLog($"No changes detected on file: {fileInfo.FullName}");
               }
            }
            else {
               if (Stats.RunInDebugMode)
                  AddDebugEntryToLog($"File {fileInfo.FullName} is a new file");
               if (Stats.RunInSimMode) {
                  Stats.SimNumAdds++;
                  Stats.SimVolumeAdds += fileInfo.Length;
               }
               else {
                  if (Stats.RunInDebugMode)
                     AddDebugEntryToLog($"Copying new file {fileInfo.FullName} to {destFileName}");
                  string errMsg = FileUtils.CopyFile(fileInfo.FullName, destFileName, true, true, true);
                  if (errMsg != string.Empty) {
                     AddError($"File error copying new file. Msg: {errMsg}");
                     return;
                  }
                  Stats.NumAdds++;
                  Stats.VolumeAdds += fileInfo.Length;
               }
            }
            OnFileHit?.Invoke(fileInfo, ref cancelFlag);
         }
      }

      #endregion

   }

}
