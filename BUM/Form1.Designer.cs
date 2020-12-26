namespace BUM {
   partial class Main_Frm {
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
         System.Windows.Forms.Label label1;
         System.Windows.Forms.ColumnHeader IDCH;
         System.Windows.Forms.ColumnHeader StartFolderCH;
         System.Windows.Forms.ColumnHeader DoSubsCH;
         System.Windows.Forms.ColumnHeader NumRulesCH;
         System.Windows.Forms.ColumnHeader SrcDefListCH;
         System.Windows.Forms.ColumnHeader DestCH;
         System.Windows.Forms.Label label2;
         System.Windows.Forms.ColumnHeader SrcIDCH;
         this.StatusBar = new System.Windows.Forms.StatusStrip();
         this.SbarLbl = new System.Windows.Forms.ToolStripStatusLabel();
         this.JobsLV = new System.Windows.Forms.ListView();
         this.JobCM = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.JobAddMI = new System.Windows.Forms.ToolStripMenuItem();
         this.JobEditMI = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.JobDelMI = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
         this.JobRunMI = new System.Windows.Forms.ToolStripMenuItem();
         this.SourceDefsLV = new System.Windows.Forms.ListView();
         this.SrcDefCM = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.SrcAddMI = new System.Windows.Forms.ToolStripMenuItem();
         this.SrcEditMI = new System.Windows.Forms.ToolStripMenuItem();
         this.SrcCopyMI = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
         this.SrcDelMI = new System.Windows.Forms.ToolStripMenuItem();
         this.SrcEditBtn = new System.Windows.Forms.Button();
         this.SrcDelBtn = new System.Windows.Forms.Button();
         this.SrcAddBtn = new System.Windows.Forms.Button();
         this.JobEditBtn = new System.Windows.Forms.Button();
         this.JobDelBtn = new System.Windows.Forms.Button();
         this.JobAddBtn = new System.Windows.Forms.Button();
         this.JobRunBtn = new System.Windows.Forms.Button();
         this.AutoShowLogCB = new System.Windows.Forms.CheckBox();
         this.AutoShowErrsCB = new System.Windows.Forms.CheckBox();
         this.DebugModeCB = new System.Windows.Forms.CheckBox();
         this.SimModeCB = new System.Windows.Forms.CheckBox();
         this.AutoShowDebugCB = new System.Windows.Forms.CheckBox();
         this.CancelRunBtn = new System.Windows.Forms.Button();
         this.SrcCopyBtn = new System.Windows.Forms.Button();
         this.RunOptsGB = new System.Windows.Forms.GroupBox();
         this.BackupSettingsCB = new System.Windows.Forms.CheckBox();
         this.JobCmdlineBtn = new System.Windows.Forms.Button();
         this.JobCmdlineMI = new System.Windows.Forms.ToolStripMenuItem();
         label1 = new System.Windows.Forms.Label();
         IDCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         StartFolderCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         DoSubsCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         NumRulesCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         SrcDefListCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         DestCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         label2 = new System.Windows.Forms.Label();
         SrcIDCH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.StatusBar.SuspendLayout();
         this.JobCM.SuspendLayout();
         this.SrcDefCM.SuspendLayout();
         this.RunOptsGB.SuspendLayout();
         this.SuspendLayout();
         // 
         // label1
         // 
         label1.AutoSize = true;
         label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         label1.Location = new System.Drawing.Point(15, 9);
         label1.Name = "label1";
         label1.Size = new System.Drawing.Size(168, 16);
         label1.TabIndex = 5;
         label1.Text = "Backup Source Definitions:";
         // 
         // IDCH
         // 
         IDCH.Text = "ID";
         IDCH.Width = 75;
         // 
         // StartFolderCH
         // 
         StartFolderCH.Text = "Starting Folder";
         StartFolderCH.Width = 600;
         // 
         // DoSubsCH
         // 
         DoSubsCH.Text = "Subfolders?";
         DoSubsCH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         DoSubsCH.Width = 100;
         // 
         // NumRulesCH
         // 
         NumRulesCH.Text = "Rules";
         NumRulesCH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         NumRulesCH.Width = 75;
         // 
         // SrcDefListCH
         // 
         SrcDefListCH.Text = "Source Defs";
         SrcDefListCH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         SrcDefListCH.Width = 125;
         // 
         // DestCH
         // 
         DestCH.Text = "Backup Destination";
         DestCH.Width = 650;
         // 
         // label2
         // 
         label2.AutoSize = true;
         label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
         label2.Location = new System.Drawing.Point(15, 308);
         label2.Name = "label2";
         label2.Size = new System.Drawing.Size(90, 16);
         label2.TabIndex = 131;
         label2.Text = "Backup Jobs:";
         // 
         // SrcIDCH
         // 
         SrcIDCH.Text = "ID";
         // 
         // StatusBar
         // 
         this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SbarLbl});
         this.StatusBar.Location = new System.Drawing.Point(0, 739);
         this.StatusBar.Name = "StatusBar";
         this.StatusBar.Size = new System.Drawing.Size(1084, 22);
         this.StatusBar.SizingGrip = false;
         this.StatusBar.TabIndex = 1;
         // 
         // SbarLbl
         // 
         this.SbarLbl.AutoSize = false;
         this.SbarLbl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.SbarLbl.Name = "SbarLbl";
         this.SbarLbl.Size = new System.Drawing.Size(1069, 17);
         this.SbarLbl.Spring = true;
         this.SbarLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // JobsLV
         // 
         this.JobsLV.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.JobsLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            IDCH,
            SrcDefListCH,
            DestCH});
         this.JobsLV.ContextMenuStrip = this.JobCM;
         this.JobsLV.FullRowSelect = true;
         this.JobsLV.GridLines = true;
         this.JobsLV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
         this.JobsLV.HideSelection = false;
         this.JobsLV.Location = new System.Drawing.Point(18, 331);
         this.JobsLV.MultiSelect = false;
         this.JobsLV.Name = "JobsLV";
         this.JobsLV.ShowGroups = false;
         this.JobsLV.Size = new System.Drawing.Size(908, 217);
         this.JobsLV.TabIndex = 50;
         this.JobsLV.UseCompatibleStateImageBehavior = false;
         this.JobsLV.View = System.Windows.Forms.View.Details;
         this.JobsLV.SelectedIndexChanged += new System.EventHandler(this.JobsLV_SelectedIndexChanged);
         // 
         // JobCM
         // 
         this.JobCM.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.JobCM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.JobAddMI,
            this.JobEditMI,
            this.toolStripSeparator1,
            this.JobDelMI,
            this.toolStripSeparator2,
            this.JobRunMI,
            this.JobCmdlineMI});
         this.JobCM.Name = "JobCM";
         this.JobCM.Size = new System.Drawing.Size(148, 126);
         this.JobCM.Opened += new System.EventHandler(this.JobCM_Opened);
         // 
         // JobAddMI
         // 
         this.JobAddMI.Name = "JobAddMI";
         this.JobAddMI.Size = new System.Drawing.Size(147, 22);
         this.JobAddMI.Text = "Add...";
         this.JobAddMI.Click += new System.EventHandler(this.JobAddMI_Click);
         // 
         // JobEditMI
         // 
         this.JobEditMI.Name = "JobEditMI";
         this.JobEditMI.Size = new System.Drawing.Size(147, 22);
         this.JobEditMI.Text = "Edit...";
         this.JobEditMI.Click += new System.EventHandler(this.JobEditMI_Click);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(144, 6);
         // 
         // JobDelMI
         // 
         this.JobDelMI.Name = "JobDelMI";
         this.JobDelMI.Size = new System.Drawing.Size(147, 22);
         this.JobDelMI.Text = "Remove";
         this.JobDelMI.Click += new System.EventHandler(this.JobDelMI_Click);
         // 
         // toolStripSeparator2
         // 
         this.toolStripSeparator2.Name = "toolStripSeparator2";
         this.toolStripSeparator2.Size = new System.Drawing.Size(144, 6);
         // 
         // JobRunMI
         // 
         this.JobRunMI.Name = "JobRunMI";
         this.JobRunMI.Size = new System.Drawing.Size(147, 22);
         this.JobRunMI.Text = "Run";
         this.JobRunMI.Click += new System.EventHandler(this.JobRunMI_Click);
         // 
         // SourceDefsLV
         // 
         this.SourceDefsLV.Alignment = System.Windows.Forms.ListViewAlignment.Default;
         this.SourceDefsLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            SrcIDCH,
            StartFolderCH,
            DoSubsCH,
            NumRulesCH});
         this.SourceDefsLV.ContextMenuStrip = this.SrcDefCM;
         this.SourceDefsLV.FullRowSelect = true;
         this.SourceDefsLV.GridLines = true;
         this.SourceDefsLV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
         this.SourceDefsLV.HideSelection = false;
         this.SourceDefsLV.Location = new System.Drawing.Point(18, 30);
         this.SourceDefsLV.MultiSelect = false;
         this.SourceDefsLV.Name = "SourceDefsLV";
         this.SourceDefsLV.ShowGroups = false;
         this.SourceDefsLV.Size = new System.Drawing.Size(908, 262);
         this.SourceDefsLV.TabIndex = 51;
         this.SourceDefsLV.UseCompatibleStateImageBehavior = false;
         this.SourceDefsLV.View = System.Windows.Forms.View.Details;
         this.SourceDefsLV.SelectedIndexChanged += new System.EventHandler(this.SourceDefsLV_SelectedIndexChanged);
         this.SourceDefsLV.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SourceDefsLV_MouseDoubleClick);
         // 
         // SrcDefCM
         // 
         this.SrcDefCM.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.SrcDefCM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SrcAddMI,
            this.SrcEditMI,
            this.SrcCopyMI,
            this.toolStripSeparator3,
            this.SrcDelMI});
         this.SrcDefCM.Name = "SrcDefCM";
         this.SrcDefCM.Size = new System.Drawing.Size(124, 98);
         this.SrcDefCM.Opened += new System.EventHandler(this.SrcDefCM_Opened);
         // 
         // SrcAddMI
         // 
         this.SrcAddMI.Name = "SrcAddMI";
         this.SrcAddMI.Size = new System.Drawing.Size(123, 22);
         this.SrcAddMI.Text = "Add...";
         this.SrcAddMI.Click += new System.EventHandler(this.SrcAddMI_Click);
         // 
         // SrcEditMI
         // 
         this.SrcEditMI.Name = "SrcEditMI";
         this.SrcEditMI.Size = new System.Drawing.Size(123, 22);
         this.SrcEditMI.Text = "Edit...";
         this.SrcEditMI.Click += new System.EventHandler(this.SrcEditMI_Click);
         // 
         // SrcCopyMI
         // 
         this.SrcCopyMI.Name = "SrcCopyMI";
         this.SrcCopyMI.Size = new System.Drawing.Size(123, 22);
         this.SrcCopyMI.Text = "Copy";
         this.SrcCopyMI.Click += new System.EventHandler(this.SrcCopyMI_Click);
         // 
         // toolStripSeparator3
         // 
         this.toolStripSeparator3.Name = "toolStripSeparator3";
         this.toolStripSeparator3.Size = new System.Drawing.Size(120, 6);
         // 
         // SrcDelMI
         // 
         this.SrcDelMI.Name = "SrcDelMI";
         this.SrcDelMI.Size = new System.Drawing.Size(123, 22);
         this.SrcDelMI.Text = "Remove";
         this.SrcDelMI.Click += new System.EventHandler(this.SrcDelMI_Click);
         // 
         // SrcEditBtn
         // 
         this.SrcEditBtn.Enabled = false;
         this.SrcEditBtn.Location = new System.Drawing.Point(946, 83);
         this.SrcEditBtn.Name = "SrcEditBtn";
         this.SrcEditBtn.Size = new System.Drawing.Size(115, 36);
         this.SrcEditBtn.TabIndex = 130;
         this.SrcEditBtn.Text = "Edit...";
         this.SrcEditBtn.UseVisualStyleBackColor = true;
         this.SrcEditBtn.Click += new System.EventHandler(this.SrcEditBtn_Click);
         // 
         // SrcDelBtn
         // 
         this.SrcDelBtn.Enabled = false;
         this.SrcDelBtn.Location = new System.Drawing.Point(946, 222);
         this.SrcDelBtn.Name = "SrcDelBtn";
         this.SrcDelBtn.Size = new System.Drawing.Size(115, 36);
         this.SrcDelBtn.TabIndex = 129;
         this.SrcDelBtn.Text = "Remove";
         this.SrcDelBtn.UseVisualStyleBackColor = true;
         this.SrcDelBtn.Click += new System.EventHandler(this.SrcDelBtn_Click);
         // 
         // SrcAddBtn
         // 
         this.SrcAddBtn.Location = new System.Drawing.Point(946, 30);
         this.SrcAddBtn.Name = "SrcAddBtn";
         this.SrcAddBtn.Size = new System.Drawing.Size(115, 36);
         this.SrcAddBtn.TabIndex = 128;
         this.SrcAddBtn.Text = "Add...";
         this.SrcAddBtn.UseVisualStyleBackColor = true;
         this.SrcAddBtn.Click += new System.EventHandler(this.SrcAddBtn_Click);
         // 
         // JobEditBtn
         // 
         this.JobEditBtn.Enabled = false;
         this.JobEditBtn.Location = new System.Drawing.Point(946, 373);
         this.JobEditBtn.Name = "JobEditBtn";
         this.JobEditBtn.Size = new System.Drawing.Size(115, 36);
         this.JobEditBtn.TabIndex = 70;
         this.JobEditBtn.Text = "Edit...";
         this.JobEditBtn.UseVisualStyleBackColor = true;
         this.JobEditBtn.Click += new System.EventHandler(this.JobEditBtn_Click);
         // 
         // JobDelBtn
         // 
         this.JobDelBtn.Enabled = false;
         this.JobDelBtn.Location = new System.Drawing.Point(946, 457);
         this.JobDelBtn.Name = "JobDelBtn";
         this.JobDelBtn.Size = new System.Drawing.Size(115, 36);
         this.JobDelBtn.TabIndex = 80;
         this.JobDelBtn.Text = "Remove";
         this.JobDelBtn.UseVisualStyleBackColor = true;
         this.JobDelBtn.Click += new System.EventHandler(this.JobDelBtn_Click);
         // 
         // JobAddBtn
         // 
         this.JobAddBtn.Location = new System.Drawing.Point(946, 331);
         this.JobAddBtn.Name = "JobAddBtn";
         this.JobAddBtn.Size = new System.Drawing.Size(115, 36);
         this.JobAddBtn.TabIndex = 60;
         this.JobAddBtn.Text = "Add...";
         this.JobAddBtn.UseVisualStyleBackColor = true;
         this.JobAddBtn.Click += new System.EventHandler(this.JobAddBtn_Click);
         // 
         // JobRunBtn
         // 
         this.JobRunBtn.Enabled = false;
         this.JobRunBtn.Location = new System.Drawing.Point(946, 628);
         this.JobRunBtn.Name = "JobRunBtn";
         this.JobRunBtn.Size = new System.Drawing.Size(115, 36);
         this.JobRunBtn.TabIndex = 110;
         this.JobRunBtn.Text = "Run";
         this.JobRunBtn.UseVisualStyleBackColor = true;
         this.JobRunBtn.Click += new System.EventHandler(this.JobRunBtn_Click);
         // 
         // AutoShowLogCB
         // 
         this.AutoShowLogCB.AutoSize = true;
         this.AutoShowLogCB.Checked = true;
         this.AutoShowLogCB.CheckState = System.Windows.Forms.CheckState.Checked;
         this.AutoShowLogCB.Location = new System.Drawing.Point(21, 84);
         this.AutoShowLogCB.Name = "AutoShowLogCB";
         this.AutoShowLogCB.Size = new System.Drawing.Size(181, 20);
         this.AutoShowLogCB.TabIndex = 94;
         this.AutoShowLogCB.Text = "Auto-show log file after run";
         this.AutoShowLogCB.UseVisualStyleBackColor = true;
         // 
         // AutoShowErrsCB
         // 
         this.AutoShowErrsCB.AutoSize = true;
         this.AutoShowErrsCB.Checked = true;
         this.AutoShowErrsCB.CheckState = System.Windows.Forms.CheckState.Checked;
         this.AutoShowErrsCB.Location = new System.Drawing.Point(21, 110);
         this.AutoShowErrsCB.Name = "AutoShowErrsCB";
         this.AutoShowErrsCB.Size = new System.Drawing.Size(273, 20);
         this.AutoShowErrsCB.TabIndex = 96;
         this.AutoShowErrsCB.Text = "Auto-show error file after run (only if errors)";
         this.AutoShowErrsCB.UseVisualStyleBackColor = true;
         // 
         // DebugModeCB
         // 
         this.DebugModeCB.AutoSize = true;
         this.DebugModeCB.Location = new System.Drawing.Point(21, 58);
         this.DebugModeCB.Name = "DebugModeCB";
         this.DebugModeCB.Size = new System.Drawing.Size(497, 20);
         this.DebugModeCB.TabIndex = 92;
         this.DebugModeCB.Text = "Run in debug/detail mode (slower - a log of every file scanned, copied, deleted)";
         this.DebugModeCB.UseVisualStyleBackColor = true;
         this.DebugModeCB.CheckedChanged += new System.EventHandler(this.DebugModeCB_CheckedChanged);
         // 
         // SimModeCB
         // 
         this.SimModeCB.AutoSize = true;
         this.SimModeCB.Location = new System.Drawing.Point(21, 32);
         this.SimModeCB.Name = "SimModeCB";
         this.SimModeCB.Size = new System.Drawing.Size(426, 20);
         this.SimModeCB.TabIndex = 90;
         this.SimModeCB.Text = "Run in simulation mode ( no file copies, history archiving, or deletes)";
         this.SimModeCB.UseVisualStyleBackColor = true;
         // 
         // AutoShowDebugCB
         // 
         this.AutoShowDebugCB.AutoSize = true;
         this.AutoShowDebugCB.Location = new System.Drawing.Point(21, 136);
         this.AutoShowDebugCB.Name = "AutoShowDebugCB";
         this.AutoShowDebugCB.Size = new System.Drawing.Size(360, 20);
         this.AutoShowDebugCB.TabIndex = 98;
         this.AutoShowDebugCB.Text = "Auto-show debug file after run (only if run in debug mode)";
         this.AutoShowDebugCB.UseVisualStyleBackColor = true;
         this.AutoShowDebugCB.CheckedChanged += new System.EventHandler(this.AutoShowDebugCB_CheckedChanged);
         // 
         // CancelRunBtn
         // 
         this.CancelRunBtn.Enabled = false;
         this.CancelRunBtn.Location = new System.Drawing.Point(946, 680);
         this.CancelRunBtn.Name = "CancelRunBtn";
         this.CancelRunBtn.Size = new System.Drawing.Size(115, 36);
         this.CancelRunBtn.TabIndex = 115;
         this.CancelRunBtn.Text = "Cancel";
         this.CancelRunBtn.UseVisualStyleBackColor = true;
         this.CancelRunBtn.Click += new System.EventHandler(this.CancelRunBtn_Click);
         // 
         // SrcCopyBtn
         // 
         this.SrcCopyBtn.Enabled = false;
         this.SrcCopyBtn.Location = new System.Drawing.Point(946, 136);
         this.SrcCopyBtn.Name = "SrcCopyBtn";
         this.SrcCopyBtn.Size = new System.Drawing.Size(115, 36);
         this.SrcCopyBtn.TabIndex = 132;
         this.SrcCopyBtn.Text = "Copy";
         this.SrcCopyBtn.UseVisualStyleBackColor = true;
         this.SrcCopyBtn.Click += new System.EventHandler(this.SrcCopyBtn_Click);
         // 
         // RunOptsGB
         // 
         this.RunOptsGB.Controls.Add(this.BackupSettingsCB);
         this.RunOptsGB.Controls.Add(this.AutoShowDebugCB);
         this.RunOptsGB.Controls.Add(this.SimModeCB);
         this.RunOptsGB.Controls.Add(this.DebugModeCB);
         this.RunOptsGB.Controls.Add(this.AutoShowErrsCB);
         this.RunOptsGB.Controls.Add(this.AutoShowLogCB);
         this.RunOptsGB.Location = new System.Drawing.Point(18, 560);
         this.RunOptsGB.Name = "RunOptsGB";
         this.RunOptsGB.Size = new System.Drawing.Size(908, 176);
         this.RunOptsGB.TabIndex = 200;
         this.RunOptsGB.TabStop = false;
         this.RunOptsGB.Text = "Run Options";
         // 
         // BackupSettingsCB
         // 
         this.BackupSettingsCB.AutoSize = true;
         this.BackupSettingsCB.Checked = true;
         this.BackupSettingsCB.CheckState = System.Windows.Forms.CheckState.Checked;
         this.BackupSettingsCB.Location = new System.Drawing.Point(562, 32);
         this.BackupSettingsCB.Name = "BackupSettingsCB";
         this.BackupSettingsCB.Size = new System.Drawing.Size(264, 20);
         this.BackupSettingsCB.TabIndex = 100;
         this.BackupSettingsCB.Text = "Backup a copy of Configuration/Settings";
         this.BackupSettingsCB.UseVisualStyleBackColor = true;
         // 
         // JobCmdlineBtn
         // 
         this.JobCmdlineBtn.Enabled = false;
         this.JobCmdlineBtn.Location = new System.Drawing.Point(946, 576);
         this.JobCmdlineBtn.Name = "JobCmdlineBtn";
         this.JobCmdlineBtn.Size = new System.Drawing.Size(115, 36);
         this.JobCmdlineBtn.TabIndex = 90;
         this.JobCmdlineBtn.Text = "Show Cmd...";
         this.JobCmdlineBtn.UseVisualStyleBackColor = true;
         this.JobCmdlineBtn.Click += new System.EventHandler(this.JobCmdlineBtn_Click);
         // 
         // JobCmdlineMI
         // 
         this.JobCmdlineMI.Name = "JobCmdlineMI";
         this.JobCmdlineMI.Size = new System.Drawing.Size(147, 22);
         this.JobCmdlineMI.Text = "Show Cmd...";
         this.JobCmdlineMI.Click += new System.EventHandler(this.JobCmdlineMI_Click);
         // 
         // Main_Frm
         // 
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
         this.ClientSize = new System.Drawing.Size(1084, 761);
         this.Controls.Add(this.JobCmdlineBtn);
         this.Controls.Add(this.RunOptsGB);
         this.Controls.Add(this.SrcCopyBtn);
         this.Controls.Add(this.CancelRunBtn);
         this.Controls.Add(this.JobRunBtn);
         this.Controls.Add(this.JobEditBtn);
         this.Controls.Add(this.JobDelBtn);
         this.Controls.Add(this.JobAddBtn);
         this.Controls.Add(label2);
         this.Controls.Add(this.SrcEditBtn);
         this.Controls.Add(this.SrcDelBtn);
         this.Controls.Add(this.SrcAddBtn);
         this.Controls.Add(this.SourceDefsLV);
         this.Controls.Add(label1);
         this.Controls.Add(this.JobsLV);
         this.Controls.Add(this.StatusBar);
         this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.Margin = new System.Windows.Forms.Padding(4);
         this.Name = "Main_Frm";
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Backup Manager";
         this.Load += new System.EventHandler(this.Main_Frm_Load);
         this.StatusBar.ResumeLayout(false);
         this.StatusBar.PerformLayout();
         this.JobCM.ResumeLayout(false);
         this.SrcDefCM.ResumeLayout(false);
         this.RunOptsGB.ResumeLayout(false);
         this.RunOptsGB.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion
      private System.Windows.Forms.StatusStrip StatusBar;
      private System.Windows.Forms.ToolStripStatusLabel SbarLbl;
      private System.Windows.Forms.ListView JobsLV;
      private System.Windows.Forms.ListView SourceDefsLV;
      private System.Windows.Forms.Button SrcEditBtn;
      private System.Windows.Forms.Button SrcDelBtn;
      private System.Windows.Forms.Button SrcAddBtn;
      private System.Windows.Forms.ContextMenuStrip SrcDefCM;
      private System.Windows.Forms.ToolStripMenuItem SrcAddMI;
      private System.Windows.Forms.ToolStripMenuItem SrcEditMI;
      private System.Windows.Forms.ToolStripMenuItem SrcDelMI;
      private System.Windows.Forms.Button JobEditBtn;
      private System.Windows.Forms.Button JobDelBtn;
      private System.Windows.Forms.Button JobAddBtn;
      private System.Windows.Forms.ContextMenuStrip JobCM;
      private System.Windows.Forms.ToolStripMenuItem JobAddMI;
      private System.Windows.Forms.ToolStripMenuItem JobEditMI;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripMenuItem JobDelMI;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private System.Windows.Forms.ToolStripMenuItem JobRunMI;
      private System.Windows.Forms.Button JobRunBtn;
      private System.Windows.Forms.CheckBox AutoShowLogCB;
      private System.Windows.Forms.CheckBox AutoShowErrsCB;
      private System.Windows.Forms.CheckBox DebugModeCB;
      private System.Windows.Forms.CheckBox SimModeCB;
      private System.Windows.Forms.CheckBox AutoShowDebugCB;
      private System.Windows.Forms.Button CancelRunBtn;
      private System.Windows.Forms.ToolStripMenuItem SrcCopyMI;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
      private System.Windows.Forms.Button SrcCopyBtn;
      private System.Windows.Forms.GroupBox RunOptsGB;
      private System.Windows.Forms.CheckBox BackupSettingsCB;
      private System.Windows.Forms.ToolStripMenuItem JobCmdlineMI;
      private System.Windows.Forms.Button JobCmdlineBtn;
   }
}

