namespace LoadCalc
{
    partial class UcMain
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainPanel = new System.Windows.Forms.Panel();
            this.M1Q = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.V2 = new System.Windows.Forms.Label();
            this.BuildDate = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.UploadButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.MainPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.panel2);
            this.MainPanel.Controls.Add(this.BuildDate);
            this.MainPanel.Controls.Add(this.V2);
            this.MainPanel.Controls.Add(this.groupBox1);
            this.MainPanel.Controls.Add(this.UploadButton);
            this.MainPanel.Controls.Add(this.SaveButton);
            this.MainPanel.Controls.Add(this.M1Q);
            this.MainPanel.Location = new System.Drawing.Point(3, 3);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(478, 557);
            this.MainPanel.TabIndex = 0;
            // 
            // M1Q
            // 
            this.M1Q.Location = new System.Drawing.Point(14, 30);
            this.M1Q.Name = "M1Q";
            this.M1Q.Size = new System.Drawing.Size(82, 25);
            this.M1Q.TabIndex = 0;
            this.M1Q.Text = "Move to 1Q";
            this.M1Q.UseVisualStyleBackColor = true;
            this.M1Q.Click += new System.EventHandler(this.M1Q_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "SILLS",
            "HEADERS",
            "FLOOR SUPPORTS"});
            this.checkedListBox1.Location = new System.Drawing.Point(6, 19);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(129, 49);
            this.checkedListBox1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkedListBox1);
            this.groupBox1.Location = new System.Drawing.Point(169, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(146, 74);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Upload";
            // 
            // V2
            // 
            this.V2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.V2.AutoSize = true;
            this.V2.Location = new System.Drawing.Point(396, 6);
            this.V2.Name = "V2";
            this.V2.Size = new System.Drawing.Size(35, 13);
            this.V2.TabIndex = 12;
            this.V2.Text = "V: 3.0";
            // 
            // BuildDate
            // 
            this.BuildDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BuildDate.AutoSize = true;
            this.BuildDate.Location = new System.Drawing.Point(396, 25);
            this.BuildDate.Name = "BuildDate";
            this.BuildDate.Size = new System.Drawing.Size(59, 13);
            this.BuildDate.TabIndex = 13;
            this.BuildDate.Text = "Build Date:";
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(12, 90);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 14;
            // 
            // UploadButton
            // 
            this.UploadButton.Image = global::LoadCalc.Properties.Resources.Upload;
            this.UploadButton.Location = new System.Drawing.Point(321, 25);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(45, 45);
            this.UploadButton.TabIndex = 2;
            this.UploadButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Image = global::LoadCalc.Properties.Resources.Save_black_512;
            this.SaveButton.Location = new System.Drawing.Point(118, 25);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(45, 45);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // ucMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainPanel);
            this.Name = "ucMain";
            this.Size = new System.Drawing.Size(484, 563);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        internal System.Windows.Forms.Button M1Q;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button UploadButton;
        internal System.Windows.Forms.Label BuildDate;
        internal System.Windows.Forms.Label V2;
        internal System.Windows.Forms.Panel panel2;
    }
}
