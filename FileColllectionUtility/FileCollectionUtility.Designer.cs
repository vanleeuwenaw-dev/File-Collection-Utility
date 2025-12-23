namespace FileCollectionUtility
{
    partial class FileCollectionUtilityForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LogFileLbl = new System.Windows.Forms.Label();
            this.GetLogFileNameBtn = new System.Windows.Forms.Button();
            this.LogFileNameTextBox = new System.Windows.Forms.TextBox();
            this.TargetDirectoryLbl = new System.Windows.Forms.Label();
            this.TargetRootDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.SelectTargetRootDirectoryBtn = new System.Windows.Forms.Button();
            this.CancelProcessingBtn = new System.Windows.Forms.Button();
            this.FilesSkippedLbl = new System.Windows.Forms.Label();
            this.FilesSkippedTextBox = new System.Windows.Forms.TextBox();
            this.FilesRenamedLabel = new System.Windows.Forms.Label();
            this.FilesRenamedTextBox = new System.Windows.Forms.TextBox();
            this.FilesCopiedLbl = new System.Windows.Forms.Label();
            this.FilesCopiedTextBox = new System.Windows.Forms.TextBox();
            this.ProcessSourceDirectoryBtn = new System.Windows.Forms.Button();
            this.NumberFilesLbl = new System.Windows.Forms.Label();
            this.FileCountTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SourceDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.SourceDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.SelectSourceDirectoryBtn = new System.Windows.Forms.Button();
            this.CloseCmdBtn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.VersionLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status2Lbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.versionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.BackGroundWorkerProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.Status3Lbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.SoftwareLicenseLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.TargetDirectoryGrpBx = new System.Windows.Forms.GroupBox();
            this.SourceDirectoryGrpBx = new System.Windows.Forms.GroupBox();
            this.PhotoVideoGrpBx = new System.Windows.Forms.GroupBox();
            this.VideoChkBx = new System.Windows.Forms.CheckBox();
            this.PicturesChkBx = new System.Windows.Forms.CheckBox();
            this.StatisticsGrpBx = new System.Windows.Forms.GroupBox();
            this.ProcessSourceDirectoryGrpBx = new System.Windows.Forms.GroupBox();
            this.statusStrip1.SuspendLayout();
            this.TargetDirectoryGrpBx.SuspendLayout();
            this.SourceDirectoryGrpBx.SuspendLayout();
            this.PhotoVideoGrpBx.SuspendLayout();
            this.StatisticsGrpBx.SuspendLayout();
            this.ProcessSourceDirectoryGrpBx.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogFileLbl
            // 
            this.LogFileLbl.AutoSize = true;
            this.LogFileLbl.Location = new System.Drawing.Point(135, 122);
            this.LogFileLbl.Name = "LogFileLbl";
            this.LogFileLbl.Size = new System.Drawing.Size(99, 16);
            this.LogFileLbl.TabIndex = 6;
            this.LogFileLbl.Text = "Log File Name:";
            // 
            // GetLogFileNameBtn
            // 
            this.GetLogFileNameBtn.BackColor = System.Drawing.Color.Gainsboro;
            this.GetLogFileNameBtn.Location = new System.Drawing.Point(16, 108);
            this.GetLogFileNameBtn.Name = "GetLogFileNameBtn";
            this.GetLogFileNameBtn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.GetLogFileNameBtn.Size = new System.Drawing.Size(81, 52);
            this.GetLogFileNameBtn.TabIndex = 4;
            this.GetLogFileNameBtn.Text = "Select Log File Name";
            this.GetLogFileNameBtn.UseVisualStyleBackColor = false;
            this.GetLogFileNameBtn.Click += new System.EventHandler(this.GetLogFileNameBtn_Click);
            // 
            // LogFileNameTextBox
            // 
            this.LogFileNameTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LogFileNameTextBox.Location = new System.Drawing.Point(135, 138);
            this.LogFileNameTextBox.Name = "LogFileNameTextBox";
            this.LogFileNameTextBox.Size = new System.Drawing.Size(354, 22);
            this.LogFileNameTextBox.TabIndex = 3;
            this.LogFileNameTextBox.MouseLeave += new System.EventHandler(this.LogFileNameTextBox_MouseLeave);
            this.LogFileNameTextBox.MouseHover += new System.EventHandler(this.LogFileNameTextBox_MouseHover);
            // 
            // TargetDirectoryLbl
            // 
            this.TargetDirectoryLbl.AutoSize = true;
            this.TargetDirectoryLbl.Location = new System.Drawing.Point(135, 47);
            this.TargetDirectoryLbl.Name = "TargetDirectoryLbl";
            this.TargetDirectoryLbl.Size = new System.Drawing.Size(97, 16);
            this.TargetDirectoryLbl.TabIndex = 2;
            this.TargetDirectoryLbl.Text = "Root Directory:";
            // 
            // TargetRootDirectoryTextBox
            // 
            this.TargetRootDirectoryTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TargetRootDirectoryTextBox.Location = new System.Drawing.Point(135, 63);
            this.TargetRootDirectoryTextBox.Name = "TargetRootDirectoryTextBox";
            this.TargetRootDirectoryTextBox.Size = new System.Drawing.Size(354, 22);
            this.TargetRootDirectoryTextBox.TabIndex = 1;
            this.TargetRootDirectoryTextBox.MouseLeave += new System.EventHandler(this.TargetRootDirectoryTextBox_MouseLeave);
            this.TargetRootDirectoryTextBox.MouseHover += new System.EventHandler(this.TargetRootDirectoryTextBox_MouseHover);
            // 
            // SelectTargetRootDirectoryBtn
            // 
            this.SelectTargetRootDirectoryBtn.BackColor = System.Drawing.Color.Gainsboro;
            this.SelectTargetRootDirectoryBtn.Location = new System.Drawing.Point(16, 33);
            this.SelectTargetRootDirectoryBtn.Name = "SelectTargetRootDirectoryBtn";
            this.SelectTargetRootDirectoryBtn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.SelectTargetRootDirectoryBtn.Size = new System.Drawing.Size(81, 52);
            this.SelectTargetRootDirectoryBtn.TabIndex = 0;
            this.SelectTargetRootDirectoryBtn.Text = "Select Root Directory";
            this.SelectTargetRootDirectoryBtn.UseVisualStyleBackColor = false;
            this.SelectTargetRootDirectoryBtn.Click += new System.EventHandler(this.SelectTargetRootDirectoryBtn_Click);
            // 
            // CancelProcessingBtn
            // 
            this.CancelProcessingBtn.BackColor = System.Drawing.Color.Gainsboro;
            this.CancelProcessingBtn.Location = new System.Drawing.Point(249, 58);
            this.CancelProcessingBtn.Name = "CancelProcessingBtn";
            this.CancelProcessingBtn.Size = new System.Drawing.Size(74, 48);
            this.CancelProcessingBtn.TabIndex = 15;
            this.CancelProcessingBtn.Text = "Cancel Process";
            this.CancelProcessingBtn.UseVisualStyleBackColor = false;
            this.CancelProcessingBtn.Click += new System.EventHandler(this.CancelProcessingBtn_Click);
            // 
            // FilesSkippedLbl
            // 
            this.FilesSkippedLbl.AutoSize = true;
            this.FilesSkippedLbl.Location = new System.Drawing.Point(199, 95);
            this.FilesSkippedLbl.Name = "FilesSkippedLbl";
            this.FilesSkippedLbl.Size = new System.Drawing.Size(94, 16);
            this.FilesSkippedLbl.TabIndex = 13;
            this.FilesSkippedLbl.Text = "Files Skipped:";
            // 
            // FilesSkippedTextBox
            // 
            this.FilesSkippedTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.FilesSkippedTextBox.Location = new System.Drawing.Point(312, 92);
            this.FilesSkippedTextBox.Name = "FilesSkippedTextBox";
            this.FilesSkippedTextBox.Size = new System.Drawing.Size(75, 22);
            this.FilesSkippedTextBox.TabIndex = 12;
            // 
            // FilesRenamedLabel
            // 
            this.FilesRenamedLabel.AutoSize = true;
            this.FilesRenamedLabel.Location = new System.Drawing.Point(199, 54);
            this.FilesRenamedLabel.Name = "FilesRenamedLabel";
            this.FilesRenamedLabel.Size = new System.Drawing.Size(103, 16);
            this.FilesRenamedLabel.TabIndex = 11;
            this.FilesRenamedLabel.Text = "Files Renamed:";
            // 
            // FilesRenamedTextBox
            // 
            this.FilesRenamedTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.FilesRenamedTextBox.Location = new System.Drawing.Point(312, 51);
            this.FilesRenamedTextBox.Name = "FilesRenamedTextBox";
            this.FilesRenamedTextBox.Size = new System.Drawing.Size(75, 22);
            this.FilesRenamedTextBox.TabIndex = 10;
            // 
            // FilesCopiedLbl
            // 
            this.FilesCopiedLbl.AutoSize = true;
            this.FilesCopiedLbl.Location = new System.Drawing.Point(16, 95);
            this.FilesCopiedLbl.Name = "FilesCopiedLbl";
            this.FilesCopiedLbl.Size = new System.Drawing.Size(87, 16);
            this.FilesCopiedLbl.TabIndex = 9;
            this.FilesCopiedLbl.Text = "Files Copied:";
            // 
            // FilesCopiedTextBox
            // 
            this.FilesCopiedTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.FilesCopiedTextBox.Location = new System.Drawing.Point(118, 92);
            this.FilesCopiedTextBox.Name = "FilesCopiedTextBox";
            this.FilesCopiedTextBox.Size = new System.Drawing.Size(75, 22);
            this.FilesCopiedTextBox.TabIndex = 8;
            // 
            // ProcessSourceDirectoryBtn
            // 
            this.ProcessSourceDirectoryBtn.BackColor = System.Drawing.Color.Gainsboro;
            this.ProcessSourceDirectoryBtn.Location = new System.Drawing.Point(68, 57);
            this.ProcessSourceDirectoryBtn.Name = "ProcessSourceDirectoryBtn";
            this.ProcessSourceDirectoryBtn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.ProcessSourceDirectoryBtn.Size = new System.Drawing.Size(75, 49);
            this.ProcessSourceDirectoryBtn.TabIndex = 7;
            this.ProcessSourceDirectoryBtn.Text = "Process Source Files";
            this.ProcessSourceDirectoryBtn.UseVisualStyleBackColor = false;
            this.ProcessSourceDirectoryBtn.Click += new System.EventHandler(this.ProcessSourceDirectoryBtn_Click);
            // 
            // NumberFilesLbl
            // 
            this.NumberFilesLbl.AutoSize = true;
            this.NumberFilesLbl.Location = new System.Drawing.Point(16, 54);
            this.NumberFilesLbl.Name = "NumberFilesLbl";
            this.NumberFilesLbl.Size = new System.Drawing.Size(105, 16);
            this.NumberFilesLbl.TabIndex = 5;
            this.NumberFilesLbl.Text = "Number of Files:";
            // 
            // FileCountTextBox
            // 
            this.FileCountTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.FileCountTextBox.Location = new System.Drawing.Point(118, 51);
            this.FileCountTextBox.Name = "FileCountTextBox";
            this.FileCountTextBox.Size = new System.Drawing.Size(75, 22);
            this.FileCountTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Source Directory:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(132, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "Source Description:";
            // 
            // SourceDescriptionTextBox
            // 
            this.SourceDescriptionTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SourceDescriptionTextBox.Location = new System.Drawing.Point(132, 47);
            this.SourceDescriptionTextBox.Name = "SourceDescriptionTextBox";
            this.SourceDescriptionTextBox.Size = new System.Drawing.Size(354, 22);
            this.SourceDescriptionTextBox.TabIndex = 2;
            // 
            // SourceDirectoryTextBox
            // 
            this.SourceDirectoryTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SourceDirectoryTextBox.Location = new System.Drawing.Point(132, 109);
            this.SourceDirectoryTextBox.Name = "SourceDirectoryTextBox";
            this.SourceDirectoryTextBox.Size = new System.Drawing.Size(354, 22);
            this.SourceDirectoryTextBox.TabIndex = 1;
            this.SourceDirectoryTextBox.MouseLeave += new System.EventHandler(this.SourceDirectoryTextBox_MouseLeave);
            this.SourceDirectoryTextBox.MouseHover += new System.EventHandler(this.SourceDirectoryTextBox_MouseHover);
            // 
            // SelectSourceDirectoryBtn
            // 
            this.SelectSourceDirectoryBtn.BackColor = System.Drawing.Color.Gainsboro;
            this.SelectSourceDirectoryBtn.Location = new System.Drawing.Point(16, 30);
            this.SelectSourceDirectoryBtn.Name = "SelectSourceDirectoryBtn";
            this.SelectSourceDirectoryBtn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.SelectSourceDirectoryBtn.Size = new System.Drawing.Size(81, 52);
            this.SelectSourceDirectoryBtn.TabIndex = 0;
            this.SelectSourceDirectoryBtn.Text = "Select Source Directory";
            this.SelectSourceDirectoryBtn.UseVisualStyleBackColor = false;
            this.SelectSourceDirectoryBtn.Click += new System.EventHandler(this.SelectSourceDirectoryBtn_Click);
            // 
            // CloseCmdBtn
            // 
            this.CloseCmdBtn.BackColor = System.Drawing.Color.Gainsboro;
            this.CloseCmdBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseCmdBtn.Location = new System.Drawing.Point(953, 427);
            this.CloseCmdBtn.Name = "CloseCmdBtn";
            this.CloseCmdBtn.Size = new System.Drawing.Size(75, 41);
            this.CloseCmdBtn.TabIndex = 2;
            this.CloseCmdBtn.Text = "Close";
            this.CloseCmdBtn.UseVisualStyleBackColor = false;
            this.CloseCmdBtn.Click += new System.EventHandler(this.CloseCmdBtn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VersionLbl,
            this.Status2Lbl,
            this.versionLabel,
            this.BackGroundWorkerProgress,
            this.Status3Lbl,
            this.SoftwareLicenseLbl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 483);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1085, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // VersionLbl
            // 
            this.VersionLbl.Name = "VersionLbl";
            this.VersionLbl.Size = new System.Drawing.Size(165, 17);
            this.VersionLbl.Text = "File Collection Utility -Version:";
            // 
            // Status2Lbl
            // 
            this.Status2Lbl.Name = "Status2Lbl";
            this.Status2Lbl.Size = new System.Drawing.Size(70, 17);
            this.Status2Lbl.Text = "                     ";
            // 
            // versionLabel
            // 
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // BackGroundWorkerProgress
            // 
            this.BackGroundWorkerProgress.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackGroundWorkerProgress.Name = "BackGroundWorkerProgress";
            this.BackGroundWorkerProgress.Size = new System.Drawing.Size(500, 16);
            // 
            // Status3Lbl
            // 
            this.Status3Lbl.Name = "Status3Lbl";
            this.Status3Lbl.Size = new System.Drawing.Size(70, 17);
            this.Status3Lbl.Text = "                     ";
            // 
            // SoftwareLicenseLbl
            // 
            this.SoftwareLicenseLbl.Name = "SoftwareLicenseLbl";
            this.SoftwareLicenseLbl.Size = new System.Drawing.Size(95, 17);
            this.SoftwareLicenseLbl.Text = "Software License";
            // 
            // TargetDirectoryGrpBx
            // 
            this.TargetDirectoryGrpBx.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TargetDirectoryGrpBx.Controls.Add(this.SelectTargetRootDirectoryBtn);
            this.TargetDirectoryGrpBx.Controls.Add(this.TargetRootDirectoryTextBox);
            this.TargetDirectoryGrpBx.Controls.Add(this.TargetDirectoryLbl);
            this.TargetDirectoryGrpBx.Controls.Add(this.GetLogFileNameBtn);
            this.TargetDirectoryGrpBx.Controls.Add(this.LogFileLbl);
            this.TargetDirectoryGrpBx.Controls.Add(this.LogFileNameTextBox);
            this.TargetDirectoryGrpBx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TargetDirectoryGrpBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TargetDirectoryGrpBx.Location = new System.Drawing.Point(34, 24);
            this.TargetDirectoryGrpBx.Name = "TargetDirectoryGrpBx";
            this.TargetDirectoryGrpBx.Size = new System.Drawing.Size(533, 179);
            this.TargetDirectoryGrpBx.TabIndex = 19;
            this.TargetDirectoryGrpBx.TabStop = false;
            this.TargetDirectoryGrpBx.Text = "Root Directory and Log File";
            // 
            // SourceDirectoryGrpBx
            // 
            this.SourceDirectoryGrpBx.BackColor = System.Drawing.SystemColors.ControlLight;
            this.SourceDirectoryGrpBx.Controls.Add(this.PhotoVideoGrpBx);
            this.SourceDirectoryGrpBx.Controls.Add(this.SelectSourceDirectoryBtn);
            this.SourceDirectoryGrpBx.Controls.Add(this.SourceDescriptionTextBox);
            this.SourceDirectoryGrpBx.Controls.Add(this.label2);
            this.SourceDirectoryGrpBx.Controls.Add(this.label6);
            this.SourceDirectoryGrpBx.Controls.Add(this.SourceDirectoryTextBox);
            this.SourceDirectoryGrpBx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SourceDirectoryGrpBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SourceDirectoryGrpBx.Location = new System.Drawing.Point(34, 231);
            this.SourceDirectoryGrpBx.Name = "SourceDirectoryGrpBx";
            this.SourceDirectoryGrpBx.Size = new System.Drawing.Size(533, 237);
            this.SourceDirectoryGrpBx.TabIndex = 20;
            this.SourceDirectoryGrpBx.TabStop = false;
            this.SourceDirectoryGrpBx.Text = "Source Directory and Description";
            // 
            // PhotoVideoGrpBx
            // 
            this.PhotoVideoGrpBx.Controls.Add(this.VideoChkBx);
            this.PhotoVideoGrpBx.Controls.Add(this.PicturesChkBx);
            this.PhotoVideoGrpBx.Location = new System.Drawing.Point(132, 149);
            this.PhotoVideoGrpBx.Name = "PhotoVideoGrpBx";
            this.PhotoVideoGrpBx.Size = new System.Drawing.Size(353, 71);
            this.PhotoVideoGrpBx.TabIndex = 5;
            this.PhotoVideoGrpBx.TabStop = false;
            this.PhotoVideoGrpBx.Text = "Restrict Files To:";
            // 
            // VideoChkBx
            // 
            this.VideoChkBx.AutoSize = true;
            this.VideoChkBx.Location = new System.Drawing.Point(221, 30);
            this.VideoChkBx.Name = "VideoChkBx";
            this.VideoChkBx.Size = new System.Drawing.Size(95, 20);
            this.VideoChkBx.TabIndex = 1;
            this.VideoChkBx.Text = "Video Files";
            this.VideoChkBx.UseVisualStyleBackColor = true;
            // 
            // PicturesChkBx
            // 
            this.PicturesChkBx.AutoSize = true;
            this.PicturesChkBx.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PicturesChkBx.Location = new System.Drawing.Point(47, 30);
            this.PicturesChkBx.Name = "PicturesChkBx";
            this.PicturesChkBx.Size = new System.Drawing.Size(168, 20);
            this.PicturesChkBx.TabIndex = 0;
            this.PicturesChkBx.Text = "Photos and Image Files";
            this.PicturesChkBx.UseVisualStyleBackColor = false;
            // 
            // StatisticsGrpBx
            // 
            this.StatisticsGrpBx.BackColor = System.Drawing.SystemColors.ControlLight;
            this.StatisticsGrpBx.Controls.Add(this.NumberFilesLbl);
            this.StatisticsGrpBx.Controls.Add(this.FileCountTextBox);
            this.StatisticsGrpBx.Controls.Add(this.FilesCopiedTextBox);
            this.StatisticsGrpBx.Controls.Add(this.FilesCopiedLbl);
            this.StatisticsGrpBx.Controls.Add(this.FilesRenamedLabel);
            this.StatisticsGrpBx.Controls.Add(this.FilesSkippedLbl);
            this.StatisticsGrpBx.Controls.Add(this.FilesRenamedTextBox);
            this.StatisticsGrpBx.Controls.Add(this.FilesSkippedTextBox);
            this.StatisticsGrpBx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StatisticsGrpBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatisticsGrpBx.Location = new System.Drawing.Point(618, 24);
            this.StatisticsGrpBx.Name = "StatisticsGrpBx";
            this.StatisticsGrpBx.Size = new System.Drawing.Size(410, 179);
            this.StatisticsGrpBx.TabIndex = 21;
            this.StatisticsGrpBx.TabStop = false;
            this.StatisticsGrpBx.Text = "Statistics";
            // 
            // ProcessSourceDirectoryGrpBx
            // 
            this.ProcessSourceDirectoryGrpBx.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ProcessSourceDirectoryGrpBx.Controls.Add(this.CancelProcessingBtn);
            this.ProcessSourceDirectoryGrpBx.Controls.Add(this.ProcessSourceDirectoryBtn);
            this.ProcessSourceDirectoryGrpBx.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ProcessSourceDirectoryGrpBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessSourceDirectoryGrpBx.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ProcessSourceDirectoryGrpBx.Location = new System.Drawing.Point(619, 231);
            this.ProcessSourceDirectoryGrpBx.Name = "ProcessSourceDirectoryGrpBx";
            this.ProcessSourceDirectoryGrpBx.Size = new System.Drawing.Size(409, 179);
            this.ProcessSourceDirectoryGrpBx.TabIndex = 22;
            this.ProcessSourceDirectoryGrpBx.TabStop = false;
            this.ProcessSourceDirectoryGrpBx.Text = "Process Source Directory";
            // 
            // FileCollectionUtilityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1085, 505);
            this.Controls.Add(this.ProcessSourceDirectoryGrpBx);
            this.Controls.Add(this.StatisticsGrpBx);
            this.Controls.Add(this.SourceDirectoryGrpBx);
            this.Controls.Add(this.TargetDirectoryGrpBx);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.CloseCmdBtn);
            this.Name = "FileCollectionUtilityForm";
            this.Text = "File Collection Utility";
            this.Load += new System.EventHandler(this.FileCollectionUtilityForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.TargetDirectoryGrpBx.ResumeLayout(false);
            this.TargetDirectoryGrpBx.PerformLayout();
            this.SourceDirectoryGrpBx.ResumeLayout(false);
            this.SourceDirectoryGrpBx.PerformLayout();
            this.PhotoVideoGrpBx.ResumeLayout(false);
            this.PhotoVideoGrpBx.PerformLayout();
            this.StatisticsGrpBx.ResumeLayout(false);
            this.StatisticsGrpBx.PerformLayout();
            this.ProcessSourceDirectoryGrpBx.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TargetRootDirectoryTextBox;
        private System.Windows.Forms.Button SelectTargetRootDirectoryBtn;
        private System.Windows.Forms.TextBox SourceDirectoryTextBox;
        private System.Windows.Forms.Button SelectSourceDirectoryBtn;
        private System.Windows.Forms.Button GetLogFileNameBtn;
        private System.Windows.Forms.TextBox LogFileNameTextBox;
        private System.Windows.Forms.Button CloseCmdBtn;
        private System.Windows.Forms.Button ProcessSourceDirectoryBtn;
        private System.Windows.Forms.Label NumberFilesLbl;
        private System.Windows.Forms.TextBox FileCountTextBox;
        private System.Windows.Forms.Label FilesSkippedLbl;
        private System.Windows.Forms.TextBox FilesSkippedTextBox;
        private System.Windows.Forms.Label FilesRenamedLabel;
        private System.Windows.Forms.TextBox FilesRenamedTextBox;
        private System.Windows.Forms.Label FilesCopiedLbl;
        private System.Windows.Forms.TextBox FilesCopiedTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox SourceDescriptionTextBox;
        private System.Windows.Forms.Button CancelProcessingBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LogFileLbl;
        private System.Windows.Forms.Label TargetDirectoryLbl;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel VersionLbl;
        private System.Windows.Forms.ToolStripStatusLabel versionLabel;
        private System.Windows.Forms.GroupBox TargetDirectoryGrpBx;
        private System.Windows.Forms.ToolStripProgressBar BackGroundWorkerProgress;
        private System.Windows.Forms.GroupBox SourceDirectoryGrpBx;
        private System.Windows.Forms.GroupBox StatisticsGrpBx;
        private System.Windows.Forms.GroupBox ProcessSourceDirectoryGrpBx;
        private System.Windows.Forms.ToolStripStatusLabel Status3Lbl;
        private System.Windows.Forms.ToolStripStatusLabel SoftwareLicenseLbl;
        private System.Windows.Forms.ToolStripStatusLabel Status2Lbl;
        private System.Windows.Forms.GroupBox PhotoVideoGrpBx;
        private System.Windows.Forms.CheckBox PicturesChkBx;
        private System.Windows.Forms.CheckBox VideoChkBx;
    }
}

