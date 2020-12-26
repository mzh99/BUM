using System;
using System.Linq;
using System.Windows.Forms;

namespace BUM {

   public partial class RuleEditFrm: Form {

      public ExcludeRule Result { get; private set; }

      private int caretLine = 0;
      private int caretCol = 0;

      private readonly string[] descForFiles = CommonRules.RulesFiles.Select(r => r.Desc).ToArray();
      private readonly string[] descForFoldersAndSubs = CommonRules.RulesFoldersAndSubs.Select(r => r.Desc).ToArray();
      private readonly string[] descForParentFolders = CommonRules.RulesParentFolder.Select(r => r.Desc).ToArray();

      public RuleEditFrm(ExcludeRule init, bool addMode) {
         InitializeComponent();
         this.Text = addMode ? "Add Exclusion Rule" : "Edit Exclusion Rule";
         Result = init ?? new ExcludeRule();
         RuleTB.Text = Result.RuleCode;
         DescTB.Text = Result.Desc;
         DiagTB.Text = string.Empty;
         //SampleRulesCB.DataSource = CommonRules.AllRules.Select(r => r.Desc).ToArray();
         ApplyToCB.DataSource = Enum.GetNames(typeof(RuleAppliesTo));
         ApplyToCB.SelectedIndex = (int) Result.AppliesTo;
         SampleRulesCB.SelectedIndex = -1;
      }

      private void CompileBtn_Click(object sender, EventArgs e) {
         TryCompile(RuleTB.Text.Trim());
      }

      private bool TryCompile(string script) {
         string err;
         if (script == string.Empty) {
            DiagTB.Text = "No script code to compile";
            return false;
         }
         ScriptRunner.CompileScript(script, out err);
         if (err == string.Empty) {
            DiagTB.Text = $"Successful compile at: {DateTime.Now}";
            return true;
         }
         DiagTB.Text = "Compile Errors:\r\n" + err;
         return false;
      }

      private void OKBtn_Click(object sender, EventArgs e) {
         string desc = DescTB.Text.Trim();
         if (desc == string.Empty) {
            ShowErrMsg("Please enter a description.");
            return;
         }
         if (ApplyToCB.SelectedIndex == -1) {
            ShowErrMsg("Please select a rule applies to mode.");
            ApplyToCB.Focus();
            return;
         }
         string ruleCode = RuleTB.Text.Trim();
         if (ruleCode == string.Empty) {
            ShowErrMsg("Please enter some script code that evaluates to a bool.");
            return;
         }
         if (TryCompile(ruleCode) == false) {
            ShowErrMsg("Compile errors found. See specific diagnostics on form.");
            return;
         }
         Result.AppliesTo = (RuleAppliesTo) ApplyToCB.SelectedIndex;
         Result.Desc = desc;
         Result.RuleCode = ruleCode;
      }

      private void ShowErrMsg(string msg) {
         MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
         this.DialogResult = DialogResult.None;
      }

      private void ShowCaret() {
         GetCaretLineCol();
         Strip1.Items[0].Text = $"Cursor at Line: {caretLine}, Column: {caretCol}";
      }

      private void GetCaretLineCol() {
         var index = RuleTB.SelectionStart;     // returns caret pos even if no selection
         caretLine = RuleTB.GetLineFromCharIndex(index);
         caretCol = index - RuleTB.GetFirstCharIndexFromLine(caretLine);
         //// change zero indexed to 1 indexed
         caretLine++;
         caretCol++;
      }

      private void RuleTB_KeyUp(object sender, KeyEventArgs e) {
         ShowCaret();
      }

      private void RuleTB_MouseClick(object sender, MouseEventArgs e) {
         ShowCaret();
      }

      private void AddRuleBtn_Click(object sender, EventArgs e) {
         ExcludeRule rule = null;
         if (SampleRulesCB.SelectedIndex >= 0) {
            switch ((RuleAppliesTo) ApplyToCB.SelectedIndex) {
               case RuleAppliesTo.Files:
                  rule = CommonRules.RulesFiles[SampleRulesCB.SelectedIndex];
                  break;
               case RuleAppliesTo.FolderAndSubfolders:
                  rule = CommonRules.RulesFoldersAndSubs[SampleRulesCB.SelectedIndex];
                  break;
               case RuleAppliesTo.ParentFolderOnly:
                  rule = CommonRules.RulesParentFolder[SampleRulesCB.SelectedIndex];
                  break;
            }
            if (rule != null) {
               RuleTB.Text += Environment.NewLine + @"// " + rule.Desc + Environment.NewLine + rule.RuleCode;
               if (DescTB.Text == string.Empty)
                  DescTB.Text = rule.Desc;
            }
         }
      }

      private void ApplyToCB_SelectedIndexChanged(object sender, EventArgs e) {
         if (ApplyToCB.SelectedIndex == -1) {
            SampleRulesCB.DataSource = null;
         }
         else {
            switch ((RuleAppliesTo) ApplyToCB.SelectedIndex) {
               case RuleAppliesTo.Files:
                  SampleRulesCB.DataSource = descForFiles;
                  break;
               case RuleAppliesTo.FolderAndSubfolders:
                  SampleRulesCB.DataSource = descForFoldersAndSubs;
                  break;
               case RuleAppliesTo.ParentFolderOnly:
                  SampleRulesCB.DataSource = descForParentFolders;
                  break;
            }
         }
      }

   }

}
