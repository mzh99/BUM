namespace BUM {
   partial class RuleEditFrm {
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
         this.RuleTB = new System.Windows.Forms.TextBox();
         this.CancelBtn = new System.Windows.Forms.Button();
         this.OKBtn = new System.Windows.Forms.Button();
         this.CompileBtn = new System.Windows.Forms.Button();
         this.DiagTB = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.DescTB = new System.Windows.Forms.TextBox();
         this.Strip1 = new System.Windows.Forms.StatusStrip();
         this.StripLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
         this.label2 = new System.Windows.Forms.Label();
         this.SampleRulesCB = new System.Windows.Forms.ComboBox();
         this.AddRuleBtn = new System.Windows.Forms.Button();
         this.ApplyToCB = new System.Windows.Forms.ComboBox();
         this.label3 = new System.Windows.Forms.Label();
         this.Strip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // RuleTB
         // 
         this.RuleTB.AcceptsReturn = true;
         this.RuleTB.Location = new System.Drawing.Point(32, 143);
         this.RuleTB.Multiline = true;
         this.RuleTB.Name = "RuleTB";
         this.RuleTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.RuleTB.Size = new System.Drawing.Size(714, 336);
         this.RuleTB.TabIndex = 30;
         this.RuleTB.WordWrap = false;
         this.RuleTB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RuleTB_MouseClick);
         this.RuleTB.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RuleTB_KeyUp);
         // 
         // CancelBtn
         // 
         this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.CancelBtn.Location = new System.Drawing.Point(511, 495);
         this.CancelBtn.Name = "CancelBtn";
         this.CancelBtn.Size = new System.Drawing.Size(178, 38);
         this.CancelBtn.TabIndex = 50;
         this.CancelBtn.Text = "&Cancel";
         this.CancelBtn.UseVisualStyleBackColor = true;
         // 
         // OKBtn
         // 
         this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.OKBtn.Location = new System.Drawing.Point(288, 495);
         this.OKBtn.Name = "OKBtn";
         this.OKBtn.Size = new System.Drawing.Size(178, 38);
         this.OKBtn.TabIndex = 45;
         this.OKBtn.Text = "&Save Rule";
         this.OKBtn.UseVisualStyleBackColor = true;
         this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
         // 
         // CompileBtn
         // 
         this.CompileBtn.Location = new System.Drawing.Point(65, 495);
         this.CompileBtn.Name = "CompileBtn";
         this.CompileBtn.Size = new System.Drawing.Size(178, 38);
         this.CompileBtn.TabIndex = 40;
         this.CompileBtn.Text = "Compile Test";
         this.CompileBtn.UseVisualStyleBackColor = true;
         this.CompileBtn.Click += new System.EventHandler(this.CompileBtn_Click);
         // 
         // DiagTB
         // 
         this.DiagTB.Location = new System.Drawing.Point(32, 552);
         this.DiagTB.Multiline = true;
         this.DiagTB.Name = "DiagTB";
         this.DiagTB.ReadOnly = true;
         this.DiagTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.DiagTB.Size = new System.Drawing.Size(714, 97);
         this.DiagTB.TabIndex = 80;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(29, 23);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(79, 16);
         this.label1.TabIndex = 125;
         this.label1.Text = "Description:";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // DescTB
         // 
         this.DescTB.Location = new System.Drawing.Point(148, 20);
         this.DescTB.MaxLength = 80;
         this.DescTB.Name = "DescTB";
         this.DescTB.Size = new System.Drawing.Size(598, 22);
         this.DescTB.TabIndex = 10;
         this.DescTB.WordWrap = false;
         // 
         // Strip1
         // 
         this.Strip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripLabel1});
         this.Strip1.Location = new System.Drawing.Point(0, 689);
         this.Strip1.Name = "Strip1";
         this.Strip1.Size = new System.Drawing.Size(784, 22);
         this.Strip1.SizingGrip = false;
         this.Strip1.TabIndex = 126;
         // 
         // StripLabel1
         // 
         this.StripLabel1.Name = "StripLabel1";
         this.StripLabel1.Size = new System.Drawing.Size(0, 17);
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(29, 101);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(96, 16);
         this.label2.TabIndex = 127;
         this.label2.Text = "Sample Rules:";
         this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // SampleRulesCB
         // 
         this.SampleRulesCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.SampleRulesCB.Location = new System.Drawing.Point(148, 98);
         this.SampleRulesCB.Name = "SampleRulesCB";
         this.SampleRulesCB.Size = new System.Drawing.Size(468, 24);
         this.SampleRulesCB.TabIndex = 20;
         // 
         // AddRuleBtn
         // 
         this.AddRuleBtn.Location = new System.Drawing.Point(639, 94);
         this.AddRuleBtn.Name = "AddRuleBtn";
         this.AddRuleBtn.Size = new System.Drawing.Size(107, 31);
         this.AddRuleBtn.TabIndex = 22;
         this.AddRuleBtn.Text = "Add";
         this.AddRuleBtn.UseVisualStyleBackColor = true;
         this.AddRuleBtn.Click += new System.EventHandler(this.AddRuleBtn_Click);
         // 
         // ApplyToCB
         // 
         this.ApplyToCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.ApplyToCB.Location = new System.Drawing.Point(148, 59);
         this.ApplyToCB.Name = "ApplyToCB";
         this.ApplyToCB.Size = new System.Drawing.Size(329, 24);
         this.ApplyToCB.TabIndex = 15;
         this.ApplyToCB.SelectedIndexChanged += new System.EventHandler(this.ApplyToCB_SelectedIndexChanged);
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(29, 62);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(66, 16);
         this.label3.TabIndex = 130;
         this.label3.Text = "Apply To:";
         this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // RuleEditFrm
         // 
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
         this.CancelButton = this.CancelBtn;
         this.ClientSize = new System.Drawing.Size(784, 711);
         this.Controls.Add(this.ApplyToCB);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.AddRuleBtn);
         this.Controls.Add(this.SampleRulesCB);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.Strip1);
         this.Controls.Add(this.DescTB);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.DiagTB);
         this.Controls.Add(this.CompileBtn);
         this.Controls.Add(this.CancelBtn);
         this.Controls.Add(this.OKBtn);
         this.Controls.Add(this.RuleTB);
         this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.KeyPreview = true;
         this.Margin = new System.Windows.Forms.Padding(4);
         this.MaximizeBox = false;
         this.Name = "RuleEditFrm";
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Add Exclusion Rule";
         this.Strip1.ResumeLayout(false);
         this.Strip1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox RuleTB;
      private System.Windows.Forms.Button CancelBtn;
      private System.Windows.Forms.Button OKBtn;
      private System.Windows.Forms.Button CompileBtn;
      private System.Windows.Forms.TextBox DiagTB;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox DescTB;
      private System.Windows.Forms.StatusStrip Strip1;
      private System.Windows.Forms.ToolStripStatusLabel StripLabel1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.ComboBox SampleRulesCB;
      private System.Windows.Forms.Button AddRuleBtn;
      private System.Windows.Forms.ComboBox ApplyToCB;
      private System.Windows.Forms.Label label3;
   }
}