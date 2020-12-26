using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace BUM {
   public partial class JobEditFrm: Form {

      public BackupJob Result { get; private set; }

      private readonly ReadOnlyCollection<SourceDef> sourceDefs;
      private readonly Dictionary<int, int> sourceIDToIndexDict = new Dictionary<int, int>();
      private readonly Dictionary<int, int> indexTosourceIDDict = new Dictionary<int, int>();

      public JobEditFrm(BackupJob job, bool addMode, int jobID, ReadOnlyCollection<SourceDef> sourceDefs) {
         InitializeComponent();
         this.sourceDefs = sourceDefs;
         this.Text = addMode ? "Add Backup Job" : "Edit Backup Job";
         // if an existing job is passed in, copy the data to a new instance
         Result = (job == null) ? new BackupJob() : job.DeepCopyUsingJson();
         if (addMode)
            Result.ID = jobID;
         // only load as a backup in case combobox doesn't have text descriptions
         if (DeleteDetectCB.Items.Count == 0)
            DeleteDetectCB.DataSource = Enum.GetNames(typeof(DeleteDetectHandling));
         DeleteDetectCB.SelectedIndex = (int) Result.Options.DeleteHandling;
         PopulateSources();
         StopErrCntCB.DataSource = BackupSettings.MaxErrOptList;
         // try to find the index of the max errors setting
         var errNdx = BackupSettings.MaxErrOptList.ToList().FindIndex(n => n == Result.MaxErrors);
         // if not found, use default. Otherwise, use array index of the matched entry
         StopErrCntCB.SelectedIndex = (errNdx == -1) ? BackupSettings.MaxErrOptDefaultIndex : errNdx;
         JobIDTB.Text = Result.ID.ToString();
         DestFolderNameTB.Text = Result.Destination;
         MaintainHistCB.Checked = Result.Options.MaintainHistoryFolder;
      }

      private void PopulateSources() {
         SourcesCLB.BeginUpdate();
         SourcesCLB.Items.Clear();
         sourceIDToIndexDict.Clear();
         indexTosourceIDDict.Clear();
         for (int z = 0; z < sourceDefs.Count; z++) {
            SourcesCLB.Items.Add(sourceDefs[z].StartingFolder);
            // create a source ID to listbox index cross-reference dictionary lookup
            sourceIDToIndexDict.Add(sourceDefs[z].ID, z);
            // create a listbox index to source ID cross-reference dictionary lookup
            indexTosourceIDDict.Add(z, sourceDefs[z].ID);
         }
         // now check any sources that apply to this backup job based on internal source IDs
         foreach (int id in Result.SourceIDs) {
            int ndx = sourceIDToIndexDict.ContainsKey(id) ? sourceIDToIndexDict[id] : -1;
            if (id == -1) {
               ShowErrMsg("Backup job references a source ID that has been deleted.");
            }
            else {
               SourcesCLB.SetItemChecked(ndx, true);  // mark it checked
            }
         }
         SourcesCLB.EndUpdate();
      }

      private void DestFolderSearchBtn_Click(object sender, EventArgs e) {
         if (DestFolderNameTB.Text != string.Empty)
            DestFolderBrowser.SelectedPath = DestFolderNameTB.Text;
         if (DestFolderBrowser.ShowDialog() == DialogResult.OK) {
            DestFolderNameTB.Text = DestFolderBrowser.SelectedPath;
         }
      }

      private void OKBtn_Click(object sender, EventArgs e) {
         if (SourcesCLB.CheckedItems.Count == 0) {
            ShowErrMsg("You must select one or more source definitions in this backup job.");
            return;
         }
         if (DestFolderNameTB.Text == string.Empty) {
            ShowErrMsg("You must enter a destination folder to use for this backup job.");
            return;
         }
         if (DeleteDetectCB.SelectedIndex == -1) {
            ShowErrMsg("You must choose a delete detection method.");
            return;
         }
         if (StopErrCntCB.SelectedIndex == -1) {
            ShowErrMsg("You must choose a max error threshold.");
            return;
         }
         Result.Destination = DestFolderNameTB.Text;
         // update source IDs from reverse lookup using listbox index to get source id
         Result.SourceIDs.Clear();
         foreach (int ndx in SourcesCLB.CheckedIndices) {
            Result.SourceIDs.Add(indexTosourceIDDict[ndx]);
         }
         Result.Options.DeleteHandling = (DeleteDetectHandling) DeleteDetectCB.SelectedIndex;
         Result.Options.MaintainHistoryFolder = MaintainHistCB.Checked;
         Result.MaxErrors = BackupSettings.MaxErrOptList[StopErrCntCB.SelectedIndex];
      }

      private void ShowErrMsg(string msg) {
         MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
         this.DialogResult = DialogResult.None;
      }

   }

}
