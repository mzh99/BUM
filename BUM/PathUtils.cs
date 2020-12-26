using System;
using System.IO;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace BUM {

   public static class PathUtils {

      // https://docs.microsoft.com/en-us/windows/win32/fileio/naming-a-file?redirectedfrom=MSDN#maximum-path-length-limitation
      // https://docs.microsoft.com/en-us/windows/win32/fileio/maximum-file-path-limitation

      // to detect support for long file names and paths,
      // The registry key Computer\HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem\LongPathsEnabled (Type: REG_DWORD) must exist and be set to 1.

      public static readonly int MaxFileNameLength = 260;
      public static readonly int PathMaxLength = 248;

      public static readonly bool SupportsLongFilenames = SystemSupportsLongFilenames();
      public static readonly Encoding UTFWithNoBom = new UTF8Encoding(false, true);

      public static bool SystemSupportsLongFilenames() {
         int val = 0;   // default to no support
         try {
            val = (int) Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem\", "LongPathsEnabled", 0);
         }
         // eat known exceptions in this case and return no support
         catch (SecurityException) { }
         catch (IOException) { }
         catch (ArgumentException) { }
         return (val == 1);
      }

      /// <summary>Return a path name where the path of srcPath is appended to destPath</summary>
      /// <param name="srcPath">Can be a folder or file path. If a root only, destPath will be returned.</param>
      /// <param name="destPath">The destination path (root or folder only) to append to. If path passed doesn't end with a slash, one will be appended.</param>
      /// <returns>The new calculated path</returns>
      public static string GraftPath(string srcPath, string destPath) {
         if (string.IsNullOrEmpty(destPath))
            throw new ArgumentException("destination path cannot be null or empty");
         if (srcPath == null)
            throw new ArgumentException("source path cannot be null");
         // get the beginning if it's rooted; if not, we'll append as-is.
         // Note: GetPathRoot() with empty string raises exception. we are checking that first
         string prefix = (srcPath == string.Empty) ? string.Empty : Path.GetPathRoot(srcPath);
         string src = (string.IsNullOrEmpty(prefix)) ? srcPath : srcPath.Substring(prefix.Length);
         // make sure dest ends with a slash
         if (destPath[destPath.Length - 1] != Path.DirectorySeparatorChar)
            destPath += Path.DirectorySeparatorChar;
         return Path.Combine(destPath, src);
      }

      /// <summary>Determines the name of a versioned file for the History folder.</summary>
      /// <param name="sourceFileName">the full file name with mandatory path</param>
      /// <param name="histFolderName">History folder name segment</param>
      /// <returns>full path name of versioned file</returns>
      /// <remarks>
      ///   Versioned file methodology:
      ///   Attempt to reconstruct a path with format: \Sourcepath\filename.ext to: \BumRoot\History\SourcepathHierarchy\filename_JobStamp.ext
      ///   If reconstructed path is too long:
      ///      - Create a version: BumRoot\History\Hash_JobTimeStamp_filename where hash is a Pearson64 hash of the original source folder
      /// </remarks>
      public static string GetVersionedHistFileName(string sourceFileName, string histFolderName, string destPathRoot, string stamp, string sep) {
         if (string.IsNullOrEmpty(sourceFileName))
            throw new ArgumentException("Source file cannot be null or empty", nameof(sourceFileName));
         if (string.IsNullOrEmpty(destPathRoot))
            throw new ArgumentException("Destination path cannot be null or empty", nameof(destPathRoot));
         string origPath = Path.GetDirectoryName(sourceFileName);
         string verName = GetVersionedBaseName(sourceFileName, stamp, sep);
         string reconName = Path.Combine(origPath, verName);
         string destPath = Path.Combine(destPathRoot, histFolderName);
         string newName = GraftPath(reconName, destPath);
         if (newName.Length < MaxFileNameLength)
            return newName;
         // if new path name for versioned file is too long, version it at the root using a hashed name; note: UTF-8 avoids endian issues
         return HashedVersionName(sourceFileName, destPath, stamp, sep);
      }

      /// <summary>Constructs the name of a versioned file for the Trash folder</summary>
      /// <param name="delFileName">full path and file name being deleted</param>
      /// <param name="delFolderName">short name of Trash folder name segment</param>
      /// <param name="destPathRoot">root folder for backups</param>
      /// <param name="stamp">stamp for datetime versioning</param>
      /// <param name="sep">separator string, typically a dash or underscore</param>
      /// <returns>the new full name including version</returns>
      /// <remarks>
      ///   This can only be called with delFileName as a child of destPathRoot. Otherwise, an exception is thrown. This is by design and can catch program logic errors/bugs.
      ///
      ///   Versioning for deleted files:
      ///      Reconstruct path: \BumRoot\Path\filename.ext into: \BumRoot\BumTrash\Path\filename_stamp.ext
      ///      If reconstructed path is too long:
      ///         Create a version: BumRoot\BumTrash\Hash_JobTimeStamp_filename where hash is a Pearson64 hash of the original deleted file folder
      /// </remarks>
       public static string GetVersionedTrashFileName(string delFileName, string delFolderName, string destPathRoot, string stamp, string sep) {
         if (string.IsNullOrEmpty(delFileName))
            throw new ArgumentException("File to delete cannot be null or empty", nameof(delFileName));
         if (string.IsNullOrEmpty(destPathRoot))
            throw new ArgumentException("Destination path cannot be null or empty", nameof(destPathRoot));
         if (delFileName.StartsWith(destPathRoot) == false)
            throw new ArgumentException($"Delete file name must be a child of destPathRoot: {destPathRoot}", nameof(delFileName));
         bool hasTrailingSlash = destPathRoot[destPathRoot.Length - 1] == Path.DirectorySeparatorChar;
         // since deleted files are already rooted at our backup location, we don't want to double (duplicate) the path so we are removing the repeated path portion
         string destPath = Path.Combine(destPathRoot, delFolderName);
         int addlStrip = hasTrailingSlash ? 0 : 1;
         string origMinusRepeatPath = delFileName.Substring(destPathRoot.Length + addlStrip);
         string unVersionedName = Path.Combine(destPath, origMinusRepeatPath);
         // we have the correct path and name now, but it's not versioned, so break apart and version it
         string verName = GetVersionedBaseName(unVersionedName, stamp, sep);
         string newName = Path.Combine(Path.GetDirectoryName(unVersionedName), verName);
         if (newName.Length < MaxFileNameLength)
            return newName;
         // if new path name for versioned file is too long, version it at the root using a hashed name; note: UTF-8 avoids endian issues
         return HashedVersionName(delFileName, destPath, stamp, sep);
      }

      /// <summary>reconstruct path\filename.ext to: path\filename_JobStamp.ext where underscore represents the separator, sep</summary>
      /// <param name="sourceFileName">the full source file path and name</param>
      /// <param name="stamp">stamp for datetime versioning</param>
      /// <param name="sep">separator string, typically a dash or underscore</param>
      /// <returns></returns>
      public static string GetVersionedBaseName(string sourceFileName, string stamp, string sep) {
         if (string.IsNullOrEmpty(sourceFileName))
            throw new ArgumentException("Source file cannot be null or empty", nameof(sourceFileName));
         string baseName = Path.GetFileName(sourceFileName);
         return Path.GetFileNameWithoutExtension(baseName) + sep + stamp + Path.GetExtension(baseName);
      }

      /// <summary>Return a hashed versioned name for a source file</summary>
      /// <param name="sourceFileName">the full source file path and name</param>
      /// <param name="destPath">the destination path</param>
      /// <param name="stamp">stamp for datetime versioning</param>
      /// <param name="sep">separator string, typically a dash or underscore</param>
      /// <returns>hashed version name</returns>
      /// <remarks>
      ///   Hash is based on the source folder name only. Combined with stamp and file name, it should be unique.
      ///   The returned name is formatted like: \destpath\hash_filename_stamp.ext where hash is a hex string (unpadded)
      /// </remarks>
      public static string HashedVersionName(string sourceFileName, string destPath, string stamp, string sep) {
         if (string.IsNullOrEmpty(sourceFileName))
            throw new ArgumentException("Source file cannot be null or empty", nameof(sourceFileName));
         string origPath = Path.GetDirectoryName(sourceFileName);
         string baseName = Path.GetFileName(sourceFileName);
         string hash = PearsonHash64.HashKeyToHexUnpadded(UTFWithNoBom.GetBytes(origPath));
         return Path.Combine(destPath, GetVersionedBaseName(hash + sep + baseName, stamp, sep));
      }

   }

}
