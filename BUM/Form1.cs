using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BUM {
   public partial class Main_Frm: Form {

      // backup processing errors reserved for: 20 to 40
      public static readonly int AppError_JobFailedInternalValidation = 20;

      private readonly BackupSettings settings;
      private BackupRunner runner = null;

      public Main_Frm() {
         InitializeComponent();
         settings = BackupSettings.GetSettings();
         PopulateSourceDefs();
         PopulateJobs();
      }

      private async void Main_Frm_Load(object sender, EventArgs e) {
         // Command-line parsing was already done in static Main()
         // Perform program-specific validations of command-line parms. If any errors, set error code and close form to close program
         // If valid, launch job, and close when finished
         if (Program.cmdLineHelper.RunViaCommandLine) {
            var retVals = Program.cmdLineHelper.ReturnPackage;
            // check if JobID is a valid Job within our settings
            int jobNdx = settings.Jobs.FindIndex(j => j.ID == retVals.JobID);
            if (jobNdx == -1) {
               Program.cmdLineHelper.CmdErrorCode = CmdlineHelper.AppError_CmdLineInvalidJobID;
               Close();
               return;
            }
            // Note: RunJob uses the index into the Job array, not the JobID itself
            await RunJob(jobNdx, retVals.RunInSimMode, retVals.DebugMode, retVals.ExportConfig, retVals.ShowLogFile, retVals.ShowErrFile, retVals.ShowDebugFile);
            Close();
         }
      }

      #region Source Definition triggers from UI

      private void SrcAddBtn_Click(object sender, EventArgs e) {
         AddSrc();
      }

      private void SrcEditBtn_Click(object sender, EventArgs e) {
         EditSrc();
      }

      private void SrcDelBtn_Click(object sender, EventArgs e) {
         DelSrc();
      }

      // from Context Menu
      private void SrcAddMI_Click(object sender, EventArgs e) {
         AddSrc();
      }

      private void SrcEditMI_Click(object sender, EventArgs e) {
         EditSrc();
      }

      private void SrcDelMI_Click(object sender, EventArgs e) {
         DelSrc();
      }

      private void SrcCopyMI_Click(object sender, EventArgs e) {
         CopySrc();
      }

      private void SrcCopyBtn_Click(object sender, EventArgs e) {
         CopySrc();
      }

      // double click default action = edit
      private void SourceDefsLV_MouseDoubleClick(object sender, MouseEventArgs e) {
         EditSrc();
      }

      private void SourceDefsLV_SelectedIndexChanged(object sender, EventArgs e) {
         bool canEnable = OneSourceIsSelected();
         SrcEditBtn.Enabled = canEnable;
         SrcCopyBtn.Enabled = canEnable;
         SrcDelBtn.Enabled = canEnable;
      }

      #endregion

      private void SrcDefCM_Opened(object sender, EventArgs e) {
         bool itemSelected = OneSourceIsSelected();
         SrcEditMI.Enabled = itemSelected;
         SrcDelMI.Enabled = itemSelected;
         SrcCopyMI.Enabled = itemSelected;
      }

      private void CopySrc() {
         if (OneSourceIsSelected()) {
            int ndx = SourceDefsLV.SelectedIndices[0];
            SourceDef def = settings.SourceDefs[ndx].DeepCopyUsingJson();
            // duplicate entire record minus the ID, which must be unique
            int nextID = GenNextSrcID();
            def.ID = nextID;
            settings.SourceDefs.Add(def);
            settings.HighestSourceID = nextID;
            SaveSettings();
            PopulateSourceDefs();
         }
      }

      private void DelSrc() {
         if (OneSourceIsSelected()) {
            int ndx = SourceDefsLV.SelectedIndices[0];
            string[] jobs = JobsReferencingSourceID(settings.SourceDefs[ndx].ID).Select(j => j.ToString()).ToArray();
            if (jobs.Length > 0) {
               var strList = String.Join(", ", jobs);
               ShowErrMsg($"Cannot delete source definition as it is referenced in backup job(s): {strList}. These backup jobs must be removed first.");
               return;
            }
            if (IsConfirmed($"Are you sure you would like to delete the backup source entry that uses starting folder: {settings.SourceDefs[ndx].StartingFolder}?")) {
               settings.SourceDefs.RemoveAt(ndx);
               SaveSettings();
               PopulateSourceDefs();
            }
         }
      }

      private IEnumerable<int> JobsReferencingSourceID(int id) {
         for (int ndx = 0; ndx < settings.Jobs.Count; ndx++) {
            if (settings.Jobs[ndx].SourceIDs.Any(s => s == id)) {
               yield return settings.Jobs[ndx].ID;
            }
         }
      }

      private bool OneSourceIsSelected() {
         return (SourceDefsLV.SelectedIndices.Count == 1);
      }

      private void SaveSettings() {
         BackupSettings.SaveSettings(settings);
         UpdateStatusBarText($"Configuration saved to: {BackupSettings.GetUserSaveName()} at {DateTime.Now}");
      }

      private void EditSrc() {
         if (OneSourceIsSelected()) {
            int ndx = SourceDefsLV.SelectedIndices[0];
            var frm = new SourceEditFrm(settings.SourceDefs[ndx], false);
            var res = frm.ShowDialog();
            if (res == DialogResult.OK) {
               settings.SourceDefs[ndx] = frm.Result;
               SaveSettings();
               PopulateSourceDefs();
            }
         }
      }

      private void AddSrc() {
         int nextID = GenNextSrcID();
         var def = new SourceDef(nextID, string.Empty, true, new List<ExcludeRule>{ CommonRules.DefaultAttrRuleForFiles }, null);
         var frm = new SourceEditFrm(def, true);
         if (frm.ShowDialog() == DialogResult.OK) {
            // Important: Update highest Source ID in settings for future adds
            settings.HighestSourceID = nextID;
            settings.SourceDefs.Add(frm.Result);
            SaveSettings();
            PopulateSourceDefs();
         }
      }

      private int GenNextSrcID() {
         int nextID = settings.HighestSourceID + 1;
         while (settings.SourceDefs.Any(s => s.ID == nextID)) {
            nextID++;
         }
         return nextID;
      }

      private void PopulateSourceDefs() {
         SourceDefsLV.BeginUpdate();
         SourceDefsLV.Items.Clear();
         foreach (var def in settings.SourceDefs) {
            ListViewItem lvi = new ListViewItem(def.ID.ToString());
            lvi.SubItems.Add(def.StartingFolder);
            lvi.SubItems.Add(def.TraverseSubs ? "Y" : "N");
            lvi.SubItems.Add(def.GlobalExcludeRules.Count.ToString());
            SourceDefsLV.Items.Add(lvi);
         }
         SourceDefsLV.EndUpdate();
      }

      private void PopulateJobs() {
         JobsLV.BeginUpdate();
         JobsLV.Items.Clear();
         foreach (var jb in settings.Jobs) {
            ListViewItem lvi = new ListViewItem(jb.ID.ToString());
            string srcDefList = string.Join(", ", jb.SourceIDs.Select(s => $"#{s}").ToArray());
            lvi.SubItems.Add(srcDefList);
            // lvi.SubItems.Add(jb.SourceIDs.Count.ToString());
            lvi.SubItems.Add(jb.Destination);
            JobsLV.Items.Add(lvi);
         }
         JobsLV.EndUpdate();
      }

      private bool OneJobIsSelected() {
         return (JobsLV.SelectedIndices.Count == 1);
      }

      #region Job triggers from UI

      private void JobAddBtn_Click(object sender, EventArgs e) {
         AddJob();
      }

      private void JobEditBtn_Click(object sender, EventArgs e) {
         EditJob();
      }

      private void JobDelBtn_Click(object sender, EventArgs e) {
         DelJob();
      }

      private async void JobRunBtn_Click(object sender, EventArgs e) {
         await RunJob();
      }

      private void JobAddMI_Click(object sender, EventArgs e) {
         AddJob();
      }

      private void JobEditMI_Click(object sender, EventArgs e) {
         EditJob();
      }

      private void JobDelMI_Click(object sender, EventArgs e) {
         DelJob();
      }

      private async void JobRunMI_Click(object sender, EventArgs e) {
         await RunJob();
      }

      private void JobCM_Opened(object sender, EventArgs e) {
         bool canRunFlag = (OneJobIsSelected());
         JobEditMI.Enabled = canRunFlag;
         JobDelMI.Enabled = canRunFlag;
         JobRunMI.Enabled = canRunFlag;
         JobCmdlineMI.Enabled = canRunFlag;
      }

      private void JobCmdlineMI_Click(object sender, EventArgs e) {
         ShowJobCmd();
      }

      private void JobCmdlineBtn_Click(object sender, EventArgs e) {
         ShowJobCmd();
      }

      private void ShowJobCmd() {
         if (OneJobIsSelected()) {
            int ndx = JobsLV.SelectedIndices[0];
            int jobID = settings.Jobs[ndx].ID;
            var parms = new CmdLineParmRet {
               JobID = jobID,
               DebugMode = DebugModeCB.Checked,
               ExportConfig = BackupSettingsCB.Checked,
               RunInSimMode = SimModeCB.Checked,
               ShowDebugFile = AutoShowDebugCB.Checked,
               ShowErrFile = AutoShowErrsCB.Checked,
               ShowLogFile = AutoShowLogCB.Checked
            };
            string cmd = "\"" + Application.ExecutablePath + "\" " + CmdlineHelper.ConstructCommandLineString(parms, @"/");
            Clipboard.SetText(cmd, TextDataFormat.Text);
            ShowInfoMsg("The following command-line has been copied to the system clipboard:\r\n" + cmd);
         }
      }

      private void JobsLV_SelectedIndexChanged(object sender, EventArgs e) {
         bool canEnable = OneJobIsSelected();
         JobEditBtn.Enabled = canEnable;
         JobDelBtn.Enabled = canEnable;
         JobCmdlineBtn.Enabled = canEnable;
         JobRunBtn.Enabled = canEnable;
      }

      #endregion

      private void AutoShowDebugCB_CheckedChanged(object sender, EventArgs e) {
         // if auto-show debug is checked but debug mode is unchecked, check debug
         if (AutoShowDebugCB.Checked && DebugModeCB.Checked == false)
            DebugModeCB.Checked = true;
      }

      private void DebugModeCB_CheckedChanged(object sender, EventArgs e) {
         // if debug mode is unchecked, also turn off auto-show debug log
         if (DebugModeCB.Checked == false && AutoShowDebugCB.Checked)
            AutoShowDebugCB.Checked = false;
      }

      private int GenNextJobID() {
         int nextID = settings.HighestJobID + 1;
         while (settings.Jobs.Any(j => j.ID == nextID)) {
            nextID++;
         }
         return nextID;
      }

      private void AddJob() {
         if (settings.SourceDefs.Count == 0) {
            ShowErrMsg("You must define one or more backup sources before configuring a backup job.");
            return;
         }
         int nextID = GenNextJobID();
         var frm = new JobEditFrm(null, true, nextID, settings.SourceDefs.AsReadOnly());
         if (frm.ShowDialog() == DialogResult.OK) {
            // Important: Update highest Job ID in settings for future adds
            settings.HighestJobID = nextID;  // update ID tracker
            settings.Jobs.Add(frm.Result);
            SaveSettings();
            PopulateJobs();
         }
      }

      private void EditJob() {
         if (OneJobIsSelected()) {
            int ndx = JobsLV.SelectedIndices[0];
            var frm = new JobEditFrm(settings.Jobs[ndx], false, 0, settings.SourceDefs.AsReadOnly());
            var res = frm.ShowDialog();
            if (res == DialogResult.OK) {
               settings.Jobs[ndx] = frm.Result;
               SaveSettings();
               PopulateJobs();
            }
         }
      }

      private void DelJob() {
         if (OneJobIsSelected()) {
            int ndx = JobsLV.SelectedIndices[0];
            if (IsConfirmed($"Are you sure you would like to delete the Backup Job entry with ID: {settings.Jobs[ndx].ID}?")) {
               settings.Jobs.RemoveAt(ndx);
               SaveSettings();
               PopulateJobs();
            }
         }
      }

      private async Task RunJob(int jobNdx, bool simMode, bool debugMode, bool exportConfig, bool showLogFile, bool showErrFile, bool showDebugFile) {
         SetUIJobRunning();
         runner = new BackupRunner(settings, settings.Jobs[jobNdx].ID);
         if (Program.cmdLineHelper.RunViaCommandLine == false)   // no DirSearch delegate hooks if running via command-line
            runner.OnFolderHit += Runner_OnFolderHit;
         // run the slow part async to allow UI to be responsive
         await Task.Run(() => {
            runner.Run(simMode, debugMode);
         });
         if (Program.cmdLineHelper.RunViaCommandLine == false)
            runner.OnFolderHit -= Runner_OnFolderHit;
         // this is an exception where we are showing an error dialog even if it might be run via command-line
         // this is because any errors indicate a fatal pre-run validation error
         if (runner.RanSuccessfully == false) {
            ShowErrMsg("Fatal error during job run. Error: " + runner.RunError);
            // processing errors start at -10 to -20
            Program.cmdLineHelper.CmdErrorCode = AppError_JobFailedInternalValidation;
            SetUIJobReady();
            return;
         }
         // export config only if instructed and simulation mode is off
         if (exportConfig && simMode == false) {
            string exportName = Path.Combine(runner.Stats.DestPathRoot, BackupSettings.GetBaseNameVersioned(runner.Stats.JobStamp));
            BackupSettings.SaveSettings(settings, exportName);
         }
         string endHow = runner.CancelFlag ? "Cancelled by User" : "Completed";
         UpdateStatusBarText($"{endHow} - Errors: {runner.Stats.NumErrors} Log File: {runner.Stats.StatsLogFileName}");
         if (showLogFile) {
            Process.Start(runner.Stats.StatsLogFileName);
         }
         if (runner.Stats.NumErrors > 0) {
            ShowErrMsg($"Errors encountered: {runner.Stats.NumErrors}. See error log: {runner.Stats.ErrLogFileName}");
            if (showErrFile) {
               Process.Start(runner.Stats.ErrLogFileName);
            }
         }
         if (showDebugFile) {
            Process.Start(runner.Stats.DebugFileName);
         }
         runner = null;
         SetUIJobReady();
      }

      // Run method for UI invocation. Use other run for command-line and programmatic control
      private async Task RunJob() {
         if (OneJobIsSelected()) {
            await RunJob(JobsLV.SelectedIndices[0], SimModeCB.Checked, DebugModeCB.Checked, BackupSettingsCB.Checked, AutoShowLogCB.Checked, AutoShowErrsCB.Checked, AutoShowDebugCB.Checked);
         }
         else {
            ShowErrMsg("Highlight a single job before using the Run command.");
         }
      }

      private void SetUIJobReady() {
         CancelRunBtn.Enabled = false;
         JobRunBtn.Enabled = true;
      }

      private void SetUIJobRunning() {
         JobRunBtn.Enabled = false;
         CancelRunBtn.Enabled = true;
      }

      private void Runner_OnFolderHit(DirectoryInfo folderInfo, ref bool cancelFlag) {
         UpdateStatusBarText($"Elapsed: {runner.Stats.JobSpanElapsedAsStr} Scanning: {folderInfo.FullName}");
      }

      private void ShowErrMsg(string msg) {
         if (Program.cmdLineHelper.RunViaCommandLine == false) {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.DialogResult = DialogResult.None;
         }
      }

      private void ShowInfoMsg(string msg) {
         MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
         this.DialogResult = DialogResult.None;
      }

      private bool IsConfirmed(string msg) {
         return (MessageBox.Show(msg, "Confirmation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
      }

      private void UpdateStatusBarText(string msg) {
         BeginInvoke(new Action(() => { SbarLbl.Text = msg; StatusBar.Update(); }));
      }

      private void CancelRunBtn_Click(object sender, EventArgs e) {
         if (runner != null) {
            runner.CancelFlag = true;
         }
      }

   }

}
