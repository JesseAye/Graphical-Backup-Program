﻿
namespace Graphical_Backup_Program
{
    partial class Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.AllFilesBtn = new System.Windows.Forms.Button();
            this.CommonFilesBtn = new System.Windows.Forms.Button();
            this.TextBoxLabel = new System.Windows.Forms.Label();
            this.pathsTextBox = new System.Windows.Forms.TextBox();
            this.whereToBackupBox = new System.Windows.Forms.GroupBox();
            this.path2TextBox = new System.Windows.Forms.TextBox();
            this.path2CheckBox = new System.Windows.Forms.CheckBox();
            this.path1TextBox = new System.Windows.Forms.TextBox();
            this.path1CheckBox = new System.Windows.Forms.CheckBox();
            this.clearingFoldersGroupBox = new System.Windows.Forms.GroupBox();
            this.dontClearRadio = new System.Windows.Forms.RadioButton();
            this.clearWithPromptRadio = new System.Windows.Forms.RadioButton();
            this.autoClearRadio = new System.Windows.Forms.RadioButton();
            this.backupMode = new System.Windows.Forms.GroupBox();
            this.modeBox = new System.Windows.Forms.GroupBox();
            this.fileExlorerBtn = new System.Windows.Forms.RadioButton();
            this.backupModeBtn = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.createTimestampFolderBtn = new System.Windows.Forms.RadioButton();
            this.dontCreateFolderBtn = new System.Windows.Forms.RadioButton();
            this.whereToBackupBox.SuspendLayout();
            this.clearingFoldersGroupBox.SuspendLayout();
            this.backupMode.SuspendLayout();
            this.modeBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AllFilesBtn
            // 
            this.AllFilesBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.AllFilesBtn.Location = new System.Drawing.Point(8, 337);
            this.AllFilesBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AllFilesBtn.Name = "AllFilesBtn";
            this.AllFilesBtn.Size = new System.Drawing.Size(197, 53);
            this.AllFilesBtn.TabIndex = 0;
            this.AllFilesBtn.Text = "Backup All Paths";
            this.AllFilesBtn.UseVisualStyleBackColor = true;
            this.AllFilesBtn.Click += new System.EventHandler(this.AllPathsBtn_Click);
            // 
            // CommonFilesBtn
            // 
            this.CommonFilesBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CommonFilesBtn.Location = new System.Drawing.Point(208, 337);
            this.CommonFilesBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CommonFilesBtn.Name = "CommonFilesBtn";
            this.CommonFilesBtn.Size = new System.Drawing.Size(197, 53);
            this.CommonFilesBtn.TabIndex = 1;
            this.CommonFilesBtn.Text = "Backup Just Common Paths";
            this.CommonFilesBtn.UseVisualStyleBackColor = true;
            this.CommonFilesBtn.Click += new System.EventHandler(this.CommonPathsBtn_Click);
            // 
            // TextBoxLabel
            // 
            this.TextBoxLabel.AutoSize = true;
            this.TextBoxLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextBoxLabel.Location = new System.Drawing.Point(8, 4);
            this.TextBoxLabel.Name = "TextBoxLabel";
            this.TextBoxLabel.Size = new System.Drawing.Size(535, 20);
            this.TextBoxLabel.TabIndex = 2;
            this.TextBoxLabel.Text = "Paths to Backup. C for Common, U for Uncommon, other characters are ignored.";
            // 
            // pathsTextBox
            // 
            this.pathsTextBox.AcceptsReturn = true;
            this.pathsTextBox.Location = new System.Drawing.Point(8, 28);
            this.pathsTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pathsTextBox.Multiline = true;
            this.pathsTextBox.Name = "pathsTextBox";
            this.pathsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.pathsTextBox.Size = new System.Drawing.Size(615, 301);
            this.pathsTextBox.TabIndex = 3;
            this.pathsTextBox.WordWrap = false;
            this.pathsTextBox.TextChanged += new System.EventHandler(this.PathsTextBox_TextChanged);
            // 
            // whereToBackupBox
            // 
            this.whereToBackupBox.Controls.Add(this.path2TextBox);
            this.whereToBackupBox.Controls.Add(this.path2CheckBox);
            this.whereToBackupBox.Controls.Add(this.path1TextBox);
            this.whereToBackupBox.Controls.Add(this.path1CheckBox);
            this.whereToBackupBox.Location = new System.Drawing.Point(8, 421);
            this.whereToBackupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.whereToBackupBox.Name = "whereToBackupBox";
            this.whereToBackupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.whereToBackupBox.Size = new System.Drawing.Size(394, 103);
            this.whereToBackupBox.TabIndex = 5;
            this.whereToBackupBox.TabStop = false;
            this.whereToBackupBox.Text = "Where to Backup";
            // 
            // path2TextBox
            // 
            this.path2TextBox.Location = new System.Drawing.Point(104, 65);
            this.path2TextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.path2TextBox.Name = "path2TextBox";
            this.path2TextBox.Size = new System.Drawing.Size(277, 27);
            this.path2TextBox.TabIndex = 3;
            this.path2TextBox.TextChanged += new System.EventHandler(this.PathTextBox_TextChanged);
            // 
            // path2CheckBox
            // 
            this.path2CheckBox.AutoSize = true;
            this.path2CheckBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.path2CheckBox.Location = new System.Drawing.Point(3, 65);
            this.path2CheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.path2CheckBox.Name = "path2CheckBox";
            this.path2CheckBox.Size = new System.Drawing.Size(102, 24);
            this.path2CheckBox.TabIndex = 2;
            this.path2CheckBox.Text = "Use Path 2:";
            this.path2CheckBox.UseVisualStyleBackColor = true;
            this.path2CheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // path1TextBox
            // 
            this.path1TextBox.Location = new System.Drawing.Point(104, 24);
            this.path1TextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.path1TextBox.Name = "path1TextBox";
            this.path1TextBox.Size = new System.Drawing.Size(277, 27);
            this.path1TextBox.TabIndex = 1;
            this.path1TextBox.TextChanged += new System.EventHandler(this.PathTextBox_TextChanged);
            // 
            // path1CheckBox
            // 
            this.path1CheckBox.AutoSize = true;
            this.path1CheckBox.Checked = true;
            this.path1CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.path1CheckBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.path1CheckBox.Location = new System.Drawing.Point(3, 27);
            this.path1CheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.path1CheckBox.Name = "path1CheckBox";
            this.path1CheckBox.Size = new System.Drawing.Size(102, 24);
            this.path1CheckBox.TabIndex = 0;
            this.path1CheckBox.Text = "Use Path 1:";
            this.path1CheckBox.UseVisualStyleBackColor = true;
            this.path1CheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // clearingFoldersGroupBox
            // 
            this.clearingFoldersGroupBox.Controls.Add(this.dontClearRadio);
            this.clearingFoldersGroupBox.Controls.Add(this.clearWithPromptRadio);
            this.clearingFoldersGroupBox.Controls.Add(this.autoClearRadio);
            this.clearingFoldersGroupBox.Location = new System.Drawing.Point(8, 532);
            this.clearingFoldersGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clearingFoldersGroupBox.Name = "clearingFoldersGroupBox";
            this.clearingFoldersGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clearingFoldersGroupBox.Size = new System.Drawing.Size(395, 123);
            this.clearingFoldersGroupBox.TabIndex = 0;
            this.clearingFoldersGroupBox.TabStop = false;
            this.clearingFoldersGroupBox.Text = "Clearing Backup Folders";
            // 
            // dontClearRadio
            // 
            this.dontClearRadio.AutoSize = true;
            this.dontClearRadio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dontClearRadio.Location = new System.Drawing.Point(6, 95);
            this.dontClearRadio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dontClearRadio.Name = "dontClearRadio";
            this.dontClearRadio.Size = new System.Drawing.Size(156, 24);
            this.dontClearRadio.TabIndex = 3;
            this.dontClearRadio.Text = "Don\'t Clear Folders";
            this.dontClearRadio.UseVisualStyleBackColor = true;
            // 
            // clearWithPromptRadio
            // 
            this.clearWithPromptRadio.AutoSize = true;
            this.clearWithPromptRadio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.clearWithPromptRadio.Location = new System.Drawing.Point(6, 62);
            this.clearWithPromptRadio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clearWithPromptRadio.Name = "clearWithPromptRadio";
            this.clearWithPromptRadio.Size = new System.Drawing.Size(201, 24);
            this.clearWithPromptRadio.TabIndex = 2;
            this.clearWithPromptRadio.Text = "Clear Folders with Prompt";
            this.clearWithPromptRadio.UseVisualStyleBackColor = true;
            // 
            // autoClearRadio
            // 
            this.autoClearRadio.AutoSize = true;
            this.autoClearRadio.Checked = true;
            this.autoClearRadio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.autoClearRadio.Location = new System.Drawing.Point(6, 28);
            this.autoClearRadio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.autoClearRadio.Name = "autoClearRadio";
            this.autoClearRadio.Size = new System.Drawing.Size(299, 24);
            this.autoClearRadio.TabIndex = 0;
            this.autoClearRadio.TabStop = true;
            this.autoClearRadio.Text = "Clear Folders Automatically (No Prompt)";
            this.autoClearRadio.UseVisualStyleBackColor = true;
            // 
            // backupMode
            // 
            this.backupMode.Controls.Add(this.dontCreateFolderBtn);
            this.backupMode.Controls.Add(this.createTimestampFolderBtn);
            this.backupMode.Location = new System.Drawing.Point(418, 532);
            this.backupMode.Name = "backupMode";
            this.backupMode.Size = new System.Drawing.Size(205, 125);
            this.backupMode.TabIndex = 7;
            this.backupMode.TabStop = false;
            this.backupMode.Text = "Backup Mode";
            // 
            // modeBox
            // 
            this.modeBox.Controls.Add(this.fileExlorerBtn);
            this.modeBox.Controls.Add(this.backupModeBtn);
            this.modeBox.Location = new System.Drawing.Point(418, 421);
            this.modeBox.Name = "modeBox";
            this.modeBox.Size = new System.Drawing.Size(192, 89);
            this.modeBox.TabIndex = 6;
            this.modeBox.TabStop = false;
            this.modeBox.Text = "GBP Mode";
            // 
            // fileExlorerBtn
            // 
            this.fileExlorerBtn.AutoSize = true;
            this.fileExlorerBtn.Location = new System.Drawing.Point(6, 54);
            this.fileExlorerBtn.Name = "fileExlorerBtn";
            this.fileExlorerBtn.Size = new System.Drawing.Size(168, 24);
            this.fileExlorerBtn.TabIndex = 1;
            this.fileExlorerBtn.Text = "Open in File Explorer";
            this.fileExlorerBtn.UseVisualStyleBackColor = true;
            // 
            // backupModeBtn
            // 
            this.backupModeBtn.AutoSize = true;
            this.backupModeBtn.Checked = true;
            this.backupModeBtn.Location = new System.Drawing.Point(6, 24);
            this.backupModeBtn.Name = "backupModeBtn";
            this.backupModeBtn.Size = new System.Drawing.Size(185, 24);
            this.backupModeBtn.TabIndex = 0;
            this.backupModeBtn.TabStop = true;
            this.backupModeBtn.Text = "Normal Mode (Backup)";
            this.backupModeBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 397);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Options";
            // 
            // createTimestampFolderBtn
            // 
            this.createTimestampFolderBtn.AutoSize = true;
            this.createTimestampFolderBtn.Checked = true;
            this.createTimestampFolderBtn.Location = new System.Drawing.Point(6, 26);
            this.createTimestampFolderBtn.Name = "createTimestampFolderBtn";
            this.createTimestampFolderBtn.Size = new System.Drawing.Size(197, 24);
            this.createTimestampFolderBtn.TabIndex = 2;
            this.createTimestampFolderBtn.TabStop = true;
            this.createTimestampFolderBtn.Text = "Create Timestamp Folder";
            this.createTimestampFolderBtn.UseVisualStyleBackColor = true;
            // 
            // dontCreateFolderBtn
            // 
            this.dontCreateFolderBtn.AutoSize = true;
            this.dontCreateFolderBtn.Location = new System.Drawing.Point(6, 56);
            this.dontCreateFolderBtn.Name = "dontCreateFolderBtn";
            this.dontCreateFolderBtn.Size = new System.Drawing.Size(159, 24);
            this.dontCreateFolderBtn.TabIndex = 2;
            this.dontCreateFolderBtn.Text = "Don\'t Create Folder";
            this.dontCreateFolderBtn.UseVisualStyleBackColor = true;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 680);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backupMode);
            this.Controls.Add(this.modeBox);
            this.Controls.Add(this.pathsTextBox);
            this.Controls.Add(this.whereToBackupBox);
            this.Controls.Add(this.clearingFoldersGroupBox);
            this.Controls.Add(this.TextBoxLabel);
            this.Controls.Add(this.CommonFilesBtn);
            this.Controls.Add(this.AllFilesBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form";
            this.Text = "Graphical Backup Program";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_FormClosed);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.whereToBackupBox.ResumeLayout(false);
            this.whereToBackupBox.PerformLayout();
            this.clearingFoldersGroupBox.ResumeLayout(false);
            this.clearingFoldersGroupBox.PerformLayout();
            this.backupMode.ResumeLayout(false);
            this.backupMode.PerformLayout();
            this.modeBox.ResumeLayout(false);
            this.modeBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AllFilesBtn;
        private System.Windows.Forms.Button CommonFilesBtn;
        private System.Windows.Forms.Label TextBoxLabel;
        private System.Windows.Forms.TextBox pathsTextBox;
        private System.Windows.Forms.GroupBox whereToBackupBox;
        private System.Windows.Forms.TextBox path2TextBox;
        private System.Windows.Forms.CheckBox path2CheckBox;
        private System.Windows.Forms.TextBox path1TextBox;
        private System.Windows.Forms.CheckBox path1CheckBox;
        private System.Windows.Forms.GroupBox clearingFoldersGroupBox;
        private System.Windows.Forms.RadioButton dontClearRadio;
        private System.Windows.Forms.RadioButton clearWithPromptRadio;
        private System.Windows.Forms.RadioButton autoClearRadio;
        private System.Windows.Forms.GroupBox modeBox;
        private System.Windows.Forms.RadioButton fileExlorerBtn;
        private System.Windows.Forms.RadioButton backupModeBtn;
        private System.Windows.Forms.GroupBox backupMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton dontCreateFolderBtn;
        private System.Windows.Forms.RadioButton createTimestampFolderBtn;
    }
}

