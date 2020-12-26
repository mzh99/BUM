using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace BUM {

   public partial class SourceEditFrm: Form {

      public SourceDef Result { get; private set; }

      private readonly bool addMode;

      public SourceEditFrm(SourceDef def, bool addMode) {
         InitializeComponent();
         this.addMode = addMode;
         this.Text = addMode ? "Add Source Backup Definition" : "Edit Source Backup Definition";
         Result = (def == null) ? new SourceDef() : def.DeepCopyUsingJson();
         StartFolderName_TB.Text = Result.StartingFolder;
         SubfoldersCB.Checked = Result.TraverseSubs;
         IDTB.Text = Result.ID.ToString();
         RefreshAllGlobRules();
         PopulateFixedFolders();
      }

      #region Folder search UI trigger and methods
      private void FolderSearchBtn_Click(object sender, EventArgs e) {
         if (StartFolderName_TB.Text != string.Empty)
            StartFolderBrowser.SelectedPath = StartFolderName_TB.Text;
         if (StartFolderBrowser.ShowDialog() == DialogResult.OK) {
            StartFolderName_TB.Text = StartFolderBrowser.SelectedPath;
            CheckBrokenKnownSubs();
         }
      }

      private void CheckBrokenKnownSubs() {
         string startFolder = StartFolderName_TB.Text;
         int removeCnt = 0;
         // Important: work backward to remove items from ListView
         for (int z = KnownFoldersCLB.Items.Count - 1; z >= 0; z--) {
            string subfolder = (string) KnownFoldersCLB.Items[z];
            if (subfolder.Contains(startFolder) == false) {
               KnownFoldersCLB.Items.RemoveAt(z);
               removeCnt++;
            }
         }
         if (removeCnt > 0) {
            ShowWarningMsg($"Removed {removeCnt} subfolder exclusion(s) due to change in starting folder");
         }
      }
      #endregion

      #region Dynamic Script Rules UI triggers and methods

      private void GlobRuleAddBtn_Click(object sender, EventArgs e) {
         GlobRuleAdd();
      }

      private void GlobRuleEditBtn_Click(object sender, EventArgs e) {
         GlobRuleEdit();
      }

      private void GlobRuleRemoveBtn_Click(object sender, EventArgs e) {
         GlobRuleDel();
      }

      private void GlobalRulesLV_MouseDoubleClick(object sender, MouseEventArgs e) {
         GlobRuleEdit();
      }

      private void GlobRuleAddMI_Click(object sender, EventArgs e) {
         GlobRuleAdd();
      }

      private void GlobRuleEditMI_Click(object sender, EventArgs e) {
         GlobRuleEdit();
      }

      private void GlobRuleDelMI_Click(object sender, EventArgs e) {
         GlobRuleDel();
      }

      private void GlobalRulesCM_Opened(object sender, EventArgs e) {
         bool itemSelected = (GlobalRulesLV.SelectedIndices.Count == 1);
         GlobRuleEditMI.Enabled = itemSelected;
         GlobRuleDelMI.Enabled = itemSelected;
      }

      private void GlobalRulesLV_SelectedIndexChanged(object sender, EventArgs e) {
         if (GlobalRulesLV.SelectedIndices.Count >= 1) {
            int ndx = GlobalRulesLV.SelectedIndices[0];
            GlobalRulesTB.Text = Result.GlobalExcludeRules[ndx].RuleCode;
         }
         else {
            GlobalRulesTB.Text = string.Empty;
         }
      }

      private void GlobRuleAdd() {
         var frm = new RuleEditFrm(null, true);
         var res = frm.ShowDialog();
         if (res == DialogResult.OK) {
            Result.GlobalExcludeRules.Add(frm.Result);
            RefreshAllGlobRules();
         }
      }

      private void GlobRuleEdit() {
         if (GlobalRulesLV.SelectedIndices.Count == 1) {
            int ndx = GlobalRulesLV.SelectedIndices[0];
            var frm = new RuleEditFrm(Result.GlobalExcludeRules[ndx], false);
            var res = frm.ShowDialog();
            if (res == DialogResult.OK) {
               Result.GlobalExcludeRules[ndx] = frm.Result;
               GlobalRulesTB.Text = Result.GlobalExcludeRules[ndx].RuleCode;
               RefreshAllGlobRules();
            }
         }
      }

      private void GlobRuleDel() {
         if (GlobalRulesLV.SelectedIndices.Count == 1) {
            int ndx = GlobalRulesLV.SelectedIndices[0];
            Result.GlobalExcludeRules.RemoveAt(ndx);
            GlobalRulesTB.Text = string.Empty;
            RefreshAllGlobRules();
         }
      }

      private void RefreshAllGlobRules() {
         GlobalRulesLV.BeginUpdate();
         GlobalRulesLV.Items.Clear();
         foreach (var rule in Result.GlobalExcludeRules) {
            ListViewItem lvi = new ListViewItem(rule.Desc);
            lvi.SubItems.Add(rule.AppliesTo.ToString());
            lvi.SubItems.Add(rule.RuleCode.Length.ToString());
            GlobalRulesLV.Items.Add(lvi);
         }
         GlobalRulesLV.EndUpdate();
      }

      #endregion

      #region Fixed/Known folder UI triggers and methods

      private void KnownAddBtn_Click(object sender, EventArgs e) {
         KnownAdd();
      }

      private void KnownEditBtn_Click(object sender, EventArgs e) {
         KnownEdit();
      }

      private void KnownDelBtn_Click(object sender, EventArgs e) {
         KnownDel();
      }

      private void KnownToggleMI_Click(object sender, EventArgs e) {
         int ndx = KnownFoldersCLB.SelectedIndex;
         if (ndx >= 0) {
            bool newState = !KnownFoldersCLB.GetItemChecked(ndx);    // toggle checked
            KnownFoldersCLB.SetItemChecked(KnownFoldersCLB.SelectedIndex, newState);
         }
      }

      private void KnownAddMI_Click(object sender, EventArgs e) {
         KnownAdd();
      }

      private void KnownEditMI_Click(object sender, EventArgs e) {
         KnownEdit();
      }

      private void KnownDelMI_Click(object sender, EventArgs e) {
         KnownDel();
      }

      private void KnownAdd() {
         string startFolder = StartFolderName_TB.Text;
         if (startFolder == string.Empty) {
            ShowErrMsg("Please enter a starting folder before adding subfolders");
            return;
         }
         KnownFolderBrowser.SelectedPath = startFolder;
         if (KnownFolderBrowser.ShowDialog() == DialogResult.OK) {
            var folder = KnownFolderBrowser.SelectedPath;
            if (folder == startFolder) {
               ShowErrMsg($"Folder must be a child folder of {startFolder}");
               return;
            }
            if (folder.Contains(startFolder) == false) {
               ShowErrMsg($"Folder must be a child folder of {startFolder}");
               return;
            }
            int repeatNdx = GetKnownRuleRepeated(folder);
            if (repeatNdx >= 0) {
               ShowErrMsg($"New folder is a repeat or redundant based on exclusion folder entry #{repeatNdx + 1}.");
               return;
            }
            KnownFoldersCLB.Items.Add(folder);
         }
      }

      private void KnownEdit() {
         int ndx = KnownFoldersCLB.SelectedIndex;
         if (ndx >= 0) {
            string startFolder = StartFolderName_TB.Text;
            if (startFolder == string.Empty) {
               ShowErrMsg("Please enter a starting folder before editing subfolders");
               return;
            }
            KnownFolderBrowser.SelectedPath = (string) KnownFoldersCLB.Items[ndx];
            if (KnownFolderBrowser.ShowDialog() == DialogResult.OK) {
               var folder = KnownFolderBrowser.SelectedPath;
               if (folder == startFolder) {
                  ShowErrMsg($"Folder must be a child folder of {startFolder}");
                  return;
               }
               if (folder.Contains(startFolder) == false) {
                  ShowErrMsg($"Folder must be a child folder of {startFolder}");
                  return;
               }
               int repeatNdx = GetKnownRuleExceptRepeated(ndx, folder);
               if (repeatNdx >= 0) {
                  ShowErrMsg($"New folder is a repeat or redundant based on exclusion folder entry #{repeatNdx + 1}.");
                  return;
               }
               KnownFoldersCLB.Items[ndx] = folder;
            }
         }
      }

      private void KnownDel() {
         if (KnownFoldersCLB.SelectedIndex >= 0) {
            KnownFoldersCLB.Items.RemoveAt(KnownFoldersCLB.SelectedIndex);
         }
      }

      private void KnownFoldersCM_Opened(object sender, EventArgs e) {
         bool itemSelected = (KnownFoldersCLB.SelectedIndex >= 0);
         KnownEditMI.Enabled = itemSelected;
         KnownDelMI.Enabled = itemSelected;
      }

      private int GetIndexOfKnownName(string folder) {
         for (int z = 0; z < KnownFoldersCLB.Items.Count; z++) {
            if ((string) KnownFoldersCLB.Items[z] == folder)
               return z;
         }
         return -1;
      }

      private int GetKnownRuleRepeated(string newfolder) {
         var repeatRule = GetFolderExclusions().FirstOrDefault(f => (f.AllSubs == true && newfolder.Contains(f.Loc + Path.DirectorySeparatorChar)) || f.Loc == newfolder);
         return repeatRule == null ? -1 : GetIndexOfKnownName(repeatRule.Loc);
      }

      private int GetKnownRuleExceptRepeated(int ndx, string newfolder) {
         var repeatRule = GetFolderExclusionsExcept(ndx).FirstOrDefault(f => (f.AllSubs == true && newfolder.Contains(f.Loc + Path.DirectorySeparatorChar)) || f.Loc == newfolder);
         return repeatRule == null ? -1 : GetIndexOfKnownName(repeatRule.Loc);
      }

      private void PopulateFixedFolders() {
         KnownFoldersCLB.BeginUpdate();
         KnownFoldersCLB.Items.Clear();
         foreach (var exc in Result.KnownFolderExcludes) {
            KnownFoldersCLB.Items.Add(exc.Loc, exc.AllSubs);
         }
         KnownFoldersCLB.EndUpdate();
      }

      #endregion

      private void ShowErrMsg(string msg) {
         MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
         this.DialogResult = DialogResult.None;
      }

      private void ShowWarningMsg(string msg) {
         MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
         this.DialogResult = DialogResult.None;
      }

      private void OKBtn_Click(object sender, EventArgs e) {
         if (StartFolderName_TB.Text == string.Empty) {
            ShowErrMsg("Please enter a starting folder or browse to valid folder.");
            StartFolderName_TB.Focus();
            return;
         }
         if (Directory.Exists(StartFolderName_TB.Text) == false) {
            ShowErrMsg("Starting folder is not valid.");
            StartFolderName_TB.Focus();
            return;
         }
         // Check that rules are not repeats; this can happen when editing checkboxes after an entry is added.
         // Alternately, you could hook into the ListBox item checked event
         for (int z = 0; z < KnownFoldersCLB.Items.Count; z++) {
            string folder = (string) KnownFoldersCLB.Items[z];
            int repeatNdx = GetKnownRuleExceptRepeated(z, folder);
            if (repeatNdx >= 0) {
               ShowErrMsg($"Entry {z + 1} is a repeat or redundant based on exclusion folder entry #{repeatNdx + 1}.");
               return;
            }
         }
         Result.StartingFolder = StartFolderName_TB.Text;
         Result.TraverseSubs = SubfoldersCB.Checked;
         Result.KnownFolderExcludes = GetFolderExclusions().ToList();
         // dynamic script rules have already been updated as we've gone along
      }



      /// <summary>Builds a list of source folder exclusions from the UI list</summary>
      /// <returns>IEnumerable of type SourceFolderExclude</returns>
      private IEnumerable<SourceFolderExclude> GetFolderExclusions() {
         return GetFolderExclusionsExcept(-1);
      }

      /// <summary>Builds a list of source folder exclusions from the UI list</summary>
      /// <param name="ndx">the index of the UI item to exclude from the check. If -1, no items are excluded. This is handy when editing an item and checking for redundant exclusions.</param>
      /// <returns>IEnumerable of type SourceFolderExclude</returns>
      private IEnumerable<SourceFolderExclude> GetFolderExclusionsExcept(int ndx) {
         for (int z = 0; z < KnownFoldersCLB.Items.Count; z++) {
            if (z != ndx) {
               var folder = (string) KnownFoldersCLB.Items[z];
               bool isChecked = KnownFoldersCLB.GetItemChecked(z);
               yield return new SourceFolderExclude(folder, isChecked);
            }
         }
      }
   }

}
