namespace BUM {
   partial class SourceEditFrm {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this.components = new System.ComponentModel.Container();
         System.Windows.Forms.ColumnHeader DescCH;
         System.Windows.Forms.ColumnHeader LenCH;
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourceEditFrm));
         this.label1 = new System.Windows.Forms.Label();
         this.StartFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
         this.StartFolderName_TB = new System.Windows.Forms.TextBox();
         this.FolderSearchBtn = new System.Windows.Forms.Button();
         this.OKBtn = new System.Windows.Forms.Button();
         this.CancelBtn = new System.Windows.Forms.Button();
         this.SubfoldersCB = new System.Windows.Forms.CheckBox();
         this.GlobalRulesTB = new System.Windows.Forms.TextBox();
         this.GlobalRulesLV = new System.Windows.Forms.ListView();
         this.AppliesCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.GlobalRulesCM = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.GlobRuleAddMI = new System.Windows.Forms.ToolStripMenuItem();
         this.GlobRuleEditMI = new System.Windows.Forms.ToolStripMenuItem();
         this.GlobRuleDelMI = new System.Windows.Forms.ToolStripMenuItem();
         this.GlobRuleAddBtn = new System.Windows.Forms.Button();
         this.GlobRuleRemoveBtn = new System.Windows.Forms.Button();
         this.label2 = new System.Windows.Forms.Label();
         this.GlobRuleEditBtn = new System.Windows.Forms.Button();
         this.label3 = new System.Windows.Forms.Label();
         this.KnownEditBtn = new System.Windows.Forms.Button();
         this.KnownDelBtn = new System.Windows.Forms.Button();
         this.KnownAddBtn = new System.Windows.Forms.Button();
         this.KnownFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
         this.KnownFoldersCLB = new System.Windows.Forms.CheckedListBox();
         this.KnownFoldersCM = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.KnownToggleMI = new System.Windows.Forms.ToolStripMenuItem();
         this.KnownAddMI = new System.Windows.Forms.ToolStripMenuItem();
         this.KnownEditMI = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.KnownDelMI = new System.Windows.Forms.ToolStripMenuItem();
         this.label4 = new System.Windows.Forms.Label();
         this.IDTB = new System.Windows.Forms.TextBox();
         DescCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         LenCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.GlobalRulesCM.SuspendLayout();
         this.KnownFoldersCM.SuspendLayout();
         this.SuspendLayout();
         // 
         // DescCH
         // 
         DescCH.Text = "Description";
         DescCH.Width = 450;
         // 
         // LenCH
         // 
         LenCH.Text = "Length";
         LenCH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         LenCH.Width = 75;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(12, 44);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(98, 16);
         this.label1.TabIndex = 0;
         this.label1.Text = "Starting Folder:";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // StartFolderBrowser
         // 
         this.StartFolderBrowser.Description = "Starting Folder";
         this.StartFolderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
         this.StartFolderBrowser.ShowNewFolderButton = false;
         // 
         // StartFolderName_TB
         // 
         this.StartFolderName_TB.Location = new System.Drawing.Point(126, 41);
         this.StartFolderName_TB.MaxLength = 250;
         this.StartFolderName_TB.Name = "StartFolderName_TB";
         this.StartFolderName_TB.ReadOnly = true;
         this.StartFolderName_TB.Size = new System.Drawing.Size(703, 22);
         this.StartFolderName_TB.TabIndex = 5;
         this.StartFolderName_TB.TabStop = false;
         this.StartFolderName_TB.WordWrap = false;
         // 
         // FolderSearchBtn
         // 
         this.FolderSearchBtn.Image = ((System.Drawing.Image)(resources.GetObject("FolderSearchBtn.Image")));
         this.FolderSearchBtn.Location = new System.Drawing.Point(847, 36);
         this.FolderSearchBtn.Name = "FolderSearchBtn";
         this.FolderSearchBtn.Size = new System.Drawing.Size(34, 34);
         this.FolderSearchBtn.TabIndex = 8;
         this.FolderSearchBtn.UseVisualStyleBackColor = true;
         this.FolderSearchBtn.Click += new System.EventHandler(this.FolderSearchBtn_Click);
         // 
         // OKBtn
         // 
         this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.OKBtn.Location = new System.Drawing.Point(241, 700);
         this.OKBtn.Name = "OKBtn";
         this.OKBtn.Size = new System.Drawing.Size(93, 38);
         this.OKBtn.TabIndex = 90;
         this.OKBtn.Text = "&Save";
         this.OKBtn.UseVisualStyleBackColor = true;
         this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
         // 
         // CancelBtn
         // 
         this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.CancelBtn.Location = new System.Drawing.Point(543, 700);
         this.CancelBtn.Name = "CancelBtn";
         this.CancelBtn.Size = new System.Drawing.Size(93, 38);
         this.CancelBtn.TabIndex = 95;
         this.CancelBtn.Text = "&Cancel";
         this.CancelBtn.UseVisualStyleBackColor = true;
         // 
         // SubfoldersCB
         // 
         this.SubfoldersCB.AutoSize = true;
         this.SubfoldersCB.Checked = true;
         this.SubfoldersCB.CheckState = System.Windows.Forms.CheckState.Checked;
         this.SubfoldersCB.Location = new System.Drawing.Point(126, 69);
         this.SubfoldersCB.Name = "SubfoldersCB";
         this.SubfoldersCB.Size = new System.Drawing.Size(233, 20);
         this.SubfoldersCB.TabIndex = 10;
         this.SubfoldersCB.Text = "Traverse Subfolders (child folders)";
         this.SubfoldersCB.UseVisualStyleBackColor = true;
         // 
         // GlobalRulesTB
         // 
         this.GlobalRulesTB.AcceptsReturn = true;
         this.GlobalRulesTB.Location = new System.Drawing.Point(18, 373);
         this.GlobalRulesTB.Multiline = true;
         this.GlobalRulesTB.Name = "GlobalRulesTB";
         this.GlobalRulesTB.ReadOnly = true;
         this.GlobalRulesTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.GlobalRulesTB.Size = new System.Drawing.Size(750, 97);
         this.GlobalRulesTB.TabIndex = 50;
         this.GlobalRulesTB.TabStop = false;
         // 
         // GlobalRulesLV
         // 
         this.GlobalRulesLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            DescCH,
            this.AppliesCH,
            LenCH});
         this.GlobalRulesLV.ContextMenuStrip = this.GlobalRulesCM;
         this.GlobalRulesLV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.GlobalRulesLV.FullRowSelect = true;
         this.GlobalRulesLV.GridLines = true;
         this.GlobalRulesLV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
         this.GlobalRulesLV.HideSelection = false;
         this.GlobalRulesLV.LabelEdit = true;
         this.GlobalRulesLV.Location = new System.Drawing.Point(18, 121);
         this.GlobalRulesLV.MultiSelect = false;
         this.GlobalRulesLV.Name = "GlobalRulesLV";
         this.GlobalRulesLV.ShowGroups = false;
         this.GlobalRulesLV.Size = new System.Drawing.Size(750, 246);
         this.GlobalRulesLV.TabIndex = 20;
         this.GlobalRulesLV.UseCompatibleStateImageBehavior = false;
         this.GlobalRulesLV.View = System.Windows.Forms.View.Details;
         this.GlobalRulesLV.SelectedIndexChanged += new System.EventHandler(this.GlobalRulesLV_SelectedIndexChanged);
         this.GlobalRulesLV.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GlobalRulesLV_MouseDoubleClick);
         // 
         // AppliesCH
         // 
         this.AppliesCH.Text = "Apply To";
         this.AppliesCH.Width = 150;
         // 
         // GlobalRulesCM
         // 
         this.GlobalRulesCM.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.GlobalRulesCM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GlobRuleAddMI,
            this.GlobRuleEditMI,
            this.GlobRuleDelMI});
         this.GlobalRulesCM.Name = "GlobalRulesCM";
         this.GlobalRulesCM.Size = new System.Drawing.Size(124, 70);
         this.GlobalRulesCM.Opened += new System.EventHandler(this.GlobalRulesCM_Opened);
         // 
         // GlobRuleAddMI
         // 
         this.GlobRuleAddMI.Name = "GlobRuleAddMI";
         this.GlobRuleAddMI.Size = new System.Drawing.Size(123, 22);
         this.GlobRuleAddMI.Text = "Add...";
         this.GlobRuleAddMI.Click += new System.EventHandler(this.GlobRuleAddMI_Click);
         // 
         // GlobRuleEditMI
         // 
         this.GlobRuleEditMI.Name = "GlobRuleEditMI";
         this.GlobRuleEditMI.Size = new System.Drawing.Size(123, 22);
         this.GlobRuleEditMI.Text = "Edit...";
         this.GlobRuleEditMI.Click += new System.EventHandler(this.GlobRuleEditMI_Click);
         // 
         // GlobRuleDelMI
         // 
         this.GlobRuleDelMI.Name = "GlobRuleDelMI";
         this.GlobRuleDelMI.Size = new System.Drawing.Size(123, 22);
         this.GlobRuleDelMI.Text = "Remove";
         this.GlobRuleDelMI.Click += new System.EventHandler(this.GlobRuleDelMI_Click);
         // 
         // GlobRuleAddBtn
         // 
         this.GlobRuleAddBtn.Location = new System.Drawing.Point(788, 166);
         this.GlobRuleAddBtn.Name = "GlobRuleAddBtn";
         this.GlobRuleAddBtn.Size = new System.Drawing.Size(93, 32);
         this.GlobRuleAddBtn.TabIndex = 30;
         this.GlobRuleAddBtn.Text = "Add...";
         this.GlobRuleAddBtn.UseVisualStyleBackColor = true;
         this.GlobRuleAddBtn.Click += new System.EventHandler(this.GlobRuleAddBtn_Click);
         // 
         // GlobRuleRemoveBtn
         // 
         this.GlobRuleRemoveBtn.Location = new System.Drawing.Point(788, 262);
         this.GlobRuleRemoveBtn.Name = "GlobRuleRemoveBtn";
         this.GlobRuleRemoveBtn.Size = new System.Drawing.Size(93, 32);
         this.GlobRuleRemoveBtn.TabIndex = 40;
         this.GlobRuleRemoveBtn.Text = "Remove";
         this.GlobRuleRemoveBtn.UseVisualStyleBackColor = true;
         this.GlobRuleRemoveBtn.Click += new System.EventHandler(this.GlobRuleRemoveBtn_Click);
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(15, 100);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(367, 16);
         this.label2.TabIndex = 126;
         this.label2.Text = "Dynamic Exclusion Rules (evaluated at runtime via scripting):";
         this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // GlobRuleEditBtn
         // 
         this.GlobRuleEditBtn.Location = new System.Drawing.Point(788, 213);
         this.GlobRuleEditBtn.Name = "GlobRuleEditBtn";
         this.GlobRuleEditBtn.Size = new System.Drawing.Size(93, 32);
         this.GlobRuleEditBtn.TabIndex = 35;
         this.GlobRuleEditBtn.Text = "Edit...";
         this.GlobRuleEditBtn.UseVisualStyleBackColor = true;
         this.GlobRuleEditBtn.Click += new System.EventHandler(this.GlobRuleEditBtn_Click);
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(15, 486);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(504, 16);
         this.label3.TabIndex = 128;
         this.label3.Text = "Fixed/Known Folder Exclusions: (mark the checkbox to exclude all its subfolders t" +
    "oo)";
         this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // KnownEditBtn
         // 
         this.KnownEditBtn.Location = new System.Drawing.Point(788, 578);
         this.KnownEditBtn.Name = "KnownEditBtn";
         this.KnownEditBtn.Size = new System.Drawing.Size(93, 32);
         this.KnownEditBtn.TabIndex = 70;
         this.KnownEditBtn.Text = "Edit...";
         this.KnownEditBtn.UseVisualStyleBackColor = true;
         this.KnownEditBtn.Click += new System.EventHandler(this.KnownEditBtn_Click);
         // 
         // KnownDelBtn
         // 
         this.KnownDelBtn.Location = new System.Drawing.Point(788, 627);
         this.KnownDelBtn.Name = "KnownDelBtn";
         this.KnownDelBtn.Size = new System.Drawing.Size(93, 32);
         this.KnownDelBtn.TabIndex = 75;
         this.KnownDelBtn.Text = "Remove";
         this.KnownDelBtn.UseVisualStyleBackColor = true;
         this.KnownDelBtn.Click += new System.EventHandler(this.KnownDelBtn_Click);
         // 
         // KnownAddBtn
         // 
         this.KnownAddBtn.Location = new System.Drawing.Point(788, 531);
         this.KnownAddBtn.Name = "KnownAddBtn";
         this.KnownAddBtn.Size = new System.Drawing.Size(93, 32);
         this.KnownAddBtn.TabIndex = 65;
         this.KnownAddBtn.Text = "Add...";
         this.KnownAddBtn.UseVisualStyleBackColor = true;
         this.KnownAddBtn.Click += new System.EventHandler(this.KnownAddBtn_Click);
         // 
         // KnownFolderBrowser
         // 
         this.KnownFolderBrowser.Description = "Subfolder to Exclude";
         this.KnownFolderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
         this.KnownFolderBrowser.ShowNewFolderButton = false;
         // 
         // KnownFoldersCLB
         // 
         this.KnownFoldersCLB.ContextMenuStrip = this.KnownFoldersCM;
         this.KnownFoldersCLB.Location = new System.Drawing.Point(18, 506);
         this.KnownFoldersCLB.Name = "KnownFoldersCLB";
         this.KnownFoldersCLB.Size = new System.Drawing.Size(750, 174);
         this.KnownFoldersCLB.TabIndex = 60;
         this.KnownFoldersCLB.ThreeDCheckBoxes = true;
         // 
         // KnownFoldersCM
         // 
         this.KnownFoldersCM.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.KnownFoldersCM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KnownToggleMI,
            this.KnownAddMI,
            this.KnownEditMI,
            this.toolStripSeparator1,
            this.KnownDelMI});
         this.KnownFoldersCM.Name = "KnownFoldersCM";
         this.KnownFoldersCM.Size = new System.Drawing.Size(170, 98);
         this.KnownFoldersCM.Opened += new System.EventHandler(this.KnownFoldersCM_Opened);
         // 
         // KnownToggleMI
         // 
         this.KnownToggleMI.Name = "KnownToggleMI";
         this.KnownToggleMI.Size = new System.Drawing.Size(169, 22);
         this.KnownToggleMI.Text = "Toggle Checked";
         this.KnownToggleMI.Click += new System.EventHandler(this.KnownToggleMI_Click);
         // 
         // KnownAddMI
         // 
         this.KnownAddMI.Name = "KnownAddMI";
         this.KnownAddMI.Size = new System.Drawing.Size(169, 22);
         this.KnownAddMI.Text = "Add...";
         this.KnownAddMI.Click += new System.EventHandler(this.KnownAddMI_Click);
         // 
         // KnownEditMI
         // 
         this.KnownEditMI.Name = "KnownEditMI";
         this.KnownEditMI.Size = new System.Drawing.Size(169, 22);
         this.KnownEditMI.Text = "Edit...";
         this.KnownEditMI.Click += new System.EventHandler(this.KnownEditMI_Click);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
         // 
         // KnownDelMI
         // 
         this.KnownDelMI.Name = "KnownDelMI";
         this.KnownDelMI.Size = new System.Drawing.Size(169, 22);
         this.KnownDelMI.Text = "Remove";
         this.KnownDelMI.Click += new System.EventHandler(this.KnownDelMI_Click);
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(15, 12);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(24, 16);
         this.label4.TabIndex = 129;
         this.label4.Text = "ID:";
         this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // IDTB
         // 
         this.IDTB.Location = new System.Drawing.Point(126, 9);
         this.IDTB.MaxLength = 250;
         this.IDTB.Name = "IDTB";
         this.IDTB.ReadOnly = true;
         this.IDTB.Size = new System.Drawing.Size(106, 22);
         this.IDTB.TabIndex = 130;
         this.IDTB.TabStop = false;
         this.IDTB.WordWrap = false;
         // 
         // SourceEditFrm
         // 
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
         this.CancelButton = this.CancelBtn;
         this.ClientSize = new System.Drawing.Size(934, 761);
         this.Controls.Add(this.IDTB);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.KnownFoldersCLB);
         this.Controls.Add(this.KnownEditBtn);
         this.Controls.Add(this.KnownDelBtn);
         this.Controls.Add(this.KnownAddBtn);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.GlobRuleEditBtn);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.GlobRuleRemoveBtn);
         this.Controls.Add(this.GlobRuleAddBtn);
         this.Controls.Add(this.GlobalRulesLV);
         this.Controls.Add(this.GlobalRulesTB);
         this.Controls.Add(this.SubfoldersCB);
         this.Controls.Add(this.CancelBtn);
         this.Controls.Add(this.OKBtn);
         this.Controls.Add(this.FolderSearchBtn);
         this.Controls.Add(this.StartFolderName_TB);
         this.Controls.Add(this.label1);
         this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Margin = new System.Windows.Forms.Padding(4);
         this.Name = "SourceEditFrm";
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.GlobalRulesCM.ResumeLayout(false);
         this.KnownFoldersCM.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.FolderBrowserDialog StartFolderBrowser;
      private System.Windows.Forms.TextBox StartFolderName_TB;
      private System.Windows.Forms.Button FolderSearchBtn;
      private System.Windows.Forms.Button OKBtn;
      private System.Windows.Forms.Button CancelBtn;
      private System.Windows.Forms.CheckBox SubfoldersCB;
      private System.Windows.Forms.TextBox GlobalRulesTB;
      private System.Windows.Forms.ListView GlobalRulesLV;
      private System.Windows.Forms.Button GlobRuleAddBtn;
      private System.Windows.Forms.Button GlobRuleRemoveBtn;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Button GlobRuleEditBtn;
      private System.Windows.Forms.ContextMenuStrip GlobalRulesCM;
      private System.Windows.Forms.ToolStripMenuItem GlobRuleAddMI;
      private System.Windows.Forms.ToolStripMenuItem GlobRuleEditMI;
      private System.Windows.Forms.ToolStripMenuItem GlobRuleDelMI;
      private System.Windows.Forms.ColumnHeader AppliesCH;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Button KnownEditBtn;
      private System.Windows.Forms.Button KnownDelBtn;
      private System.Windows.Forms.Button KnownAddBtn;
      private System.Windows.Forms.FolderBrowserDialog KnownFolderBrowser;
      private System.Windows.Forms.CheckedListBox KnownFoldersCLB;
      private System.Windows.Forms.ContextMenuStrip KnownFoldersCM;
      private System.Windows.Forms.ToolStripMenuItem KnownToggleMI;
      private System.Windows.Forms.ToolStripMenuItem KnownAddMI;
      private System.Windows.Forms.ToolStripMenuItem KnownEditMI;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripMenuItem KnownDelMI;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.TextBox IDTB;
   }
}