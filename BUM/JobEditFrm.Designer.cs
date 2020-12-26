
namespace BUM {
   partial class JobEditFrm {
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobEditFrm));
         this.JobIDTB = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.SourcesCLB = new System.Windows.Forms.CheckedListBox();
         this.label3 = new System.Windows.Forms.Label();
         this.DestFolderSearchBtn = new System.Windows.Forms.Button();
         this.DestFolderNameTB = new System.Windows.Forms.TextBox();
         this.label4 = new System.Windows.Forms.Label();
         this.JobOptsGB = new System.Windows.Forms.GroupBox();
         this.label2 = new System.Windows.Forms.Label();
         this.DeleteDetectCB = new System.Windows.Forms.ComboBox();
         this.MaintainHistCB = new System.Windows.Forms.CheckBox();
         this.CancelBtn = new System.Windows.Forms.Button();
         this.OKBtn = new System.Windows.Forms.Button();
         this.DestFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
         this.label5 = new System.Windows.Forms.Label();
         this.StopErrCntCB = new System.Windows.Forms.ComboBox();
         this.JobOptsGB.SuspendLayout();
         this.SuspendLayout();
         // 
         // JobIDTB
         // 
         this.JobIDTB.Location = new System.Drawing.Point(56, 12);
         this.JobIDTB.MaxLength = 10;
         this.JobIDTB.Name = "JobIDTB";
         this.JobIDTB.ReadOnly = true;
         this.JobIDTB.Size = new System.Drawing.Size(117, 22);
         this.JobIDTB.TabIndex = 10;
         this.JobIDTB.TabStop = false;
         this.JobIDTB.WordWrap = false;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(15, 15);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(24, 16);
         this.label1.TabIndex = 127;
         this.label1.Text = "ID:";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // SourcesCLB
         // 
         this.SourcesCLB.FormattingEnabled = true;
         this.SourcesCLB.Location = new System.Drawing.Point(18, 71);
         this.SourcesCLB.Name = "SourcesCLB";
         this.SourcesCLB.Size = new System.Drawing.Size(784, 140);
         this.SourcesCLB.TabIndex = 20;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(15, 52);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(291, 16);
         this.label3.TabIndex = 131;
         this.label3.Text = "Mark one or more sources to backup in this Job:";
         this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // DestFolderSearchBtn
         // 
         this.DestFolderSearchBtn.Image = ((System.Drawing.Image)(resources.GetObject("DestFolderSearchBtn.Image")));
         this.DestFolderSearchBtn.Location = new System.Drawing.Point(823, 245);
         this.DestFolderSearchBtn.Name = "DestFolderSearchBtn";
         this.DestFolderSearchBtn.Size = new System.Drawing.Size(34, 34);
         this.DestFolderSearchBtn.TabIndex = 32;
         this.DestFolderSearchBtn.UseVisualStyleBackColor = true;
         this.DestFolderSearchBtn.Click += new System.EventHandler(this.DestFolderSearchBtn_Click);
         // 
         // DestFolderNameTB
         // 
         this.DestFolderNameTB.Location = new System.Drawing.Point(18, 251);
         this.DestFolderNameTB.MaxLength = 250;
         this.DestFolderNameTB.Name = "DestFolderNameTB";
         this.DestFolderNameTB.ReadOnly = true;
         this.DestFolderNameTB.Size = new System.Drawing.Size(784, 22);
         this.DestFolderNameTB.TabIndex = 30;
         this.DestFolderNameTB.WordWrap = false;
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(15, 230);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(201, 16);
         this.label4.TabIndex = 132;
         this.label4.Text = "Backup Destination Folder Root:";
         this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // JobOptsGB
         // 
         this.JobOptsGB.Controls.Add(this.StopErrCntCB);
         this.JobOptsGB.Controls.Add(this.label5);
         this.JobOptsGB.Controls.Add(this.label2);
         this.JobOptsGB.Controls.Add(this.DeleteDetectCB);
         this.JobOptsGB.Controls.Add(this.MaintainHistCB);
         this.JobOptsGB.Location = new System.Drawing.Point(25, 297);
         this.JobOptsGB.Name = "JobOptsGB";
         this.JobOptsGB.Size = new System.Drawing.Size(777, 192);
         this.JobOptsGB.TabIndex = 135;
         this.JobOptsGB.TabStop = false;
         this.JobOptsGB.Text = "Job Options:";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(17, 73);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(162, 16);
         this.label2.TabIndex = 136;
         this.label2.Text = "Deleted Files Processing:";
         this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // DeleteDetectCB
         // 
         this.DeleteDetectCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.DeleteDetectCB.FormattingEnabled = true;
         this.DeleteDetectCB.Items.AddRange(new object[] {
            "Leave alone in destination folder ",
            "Delete from destination folder (two-way synch)",
            "Move to an archive folder for deletes (using date stamps for versions)"});
         this.DeleteDetectCB.Location = new System.Drawing.Point(248, 70);
         this.DeleteDetectCB.Name = "DeleteDetectCB";
         this.DeleteDetectCB.Size = new System.Drawing.Size(497, 24);
         this.DeleteDetectCB.TabIndex = 45;
         // 
         // MaintainHistCB
         // 
         this.MaintainHistCB.AutoSize = true;
         this.MaintainHistCB.Location = new System.Drawing.Point(20, 34);
         this.MaintainHistCB.Name = "MaintainHistCB";
         this.MaintainHistCB.Size = new System.Drawing.Size(359, 20);
         this.MaintainHistCB.TabIndex = 40;
         this.MaintainHistCB.Text = "Maintain a History Folder of Updated (overwritten) Files?";
         this.MaintainHistCB.UseVisualStyleBackColor = true;
         // 
         // CancelBtn
         // 
         this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.CancelBtn.Location = new System.Drawing.Point(541, 511);
         this.CancelBtn.Name = "CancelBtn";
         this.CancelBtn.Size = new System.Drawing.Size(93, 38);
         this.CancelBtn.TabIndex = 85;
         this.CancelBtn.Text = "&Cancel";
         this.CancelBtn.UseVisualStyleBackColor = true;
         // 
         // OKBtn
         // 
         this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.OKBtn.Location = new System.Drawing.Point(239, 511);
         this.OKBtn.Name = "OKBtn";
         this.OKBtn.Size = new System.Drawing.Size(93, 38);
         this.OKBtn.TabIndex = 80;
         this.OKBtn.Text = "&Save";
         this.OKBtn.UseVisualStyleBackColor = true;
         this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
         // 
         // DestFolderBrowser
         // 
         this.DestFolderBrowser.Description = "Backup Destination Folder";
         this.DestFolderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(17, 114);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(210, 16);
         this.label5.TabIndex = 136;
         this.label5.Text = "Stop backup if total errors exceed:";
         this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // StopErrCntCB
         // 
         this.StopErrCntCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.StopErrCntCB.FormattingEnabled = true;
         this.StopErrCntCB.Items.AddRange(new object[] {
            "Leave alone in destination folder ",
            "Delete from destination folder (two-way synch)",
            "Move to an archive folder for deletes (using date stamps for versions)"});
         this.StopErrCntCB.Location = new System.Drawing.Point(248, 111);
         this.StopErrCntCB.Name = "StopErrCntCB";
         this.StopErrCntCB.Size = new System.Drawing.Size(192, 24);
         this.StopErrCntCB.TabIndex = 50;
         // 
         // JobEditFrm
         // 
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
         this.CancelButton = this.CancelBtn;
         this.ClientSize = new System.Drawing.Size(884, 561);
         this.Controls.Add(this.CancelBtn);
         this.Controls.Add(this.OKBtn);
         this.Controls.Add(this.JobOptsGB);
         this.Controls.Add(this.DestFolderSearchBtn);
         this.Controls.Add(this.DestFolderNameTB);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.SourcesCLB);
         this.Controls.Add(this.JobIDTB);
         this.Controls.Add(this.label1);
         this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.MaximizeBox = false;
         this.Name = "JobEditFrm";
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Backup Job Definition";
         this.JobOptsGB.ResumeLayout(false);
         this.JobOptsGB.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox JobIDTB;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.CheckedListBox SourcesCLB;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Button DestFolderSearchBtn;
      private System.Windows.Forms.TextBox DestFolderNameTB;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.GroupBox JobOptsGB;
      private System.Windows.Forms.CheckBox MaintainHistCB;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.ComboBox DeleteDetectCB;
      private System.Windows.Forms.Button CancelBtn;
      private System.Windows.Forms.Button OKBtn;
      private System.Windows.Forms.FolderBrowserDialog DestFolderBrowser;
      private System.Windows.Forms.ComboBox StopErrCntCB;
      private System.Windows.Forms.Label label5;
   }
}