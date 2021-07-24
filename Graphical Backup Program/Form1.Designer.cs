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
            this.label1 = new System.Windows.Forms.Label();
            this.whereToBackupBox = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.path1CheckBox = new System.Windows.Forms.CheckBox();
            this.clearingFoldersGroupBox = new System.Windows.Forms.GroupBox();
            this.dontClearRadio = new System.Windows.Forms.RadioButton();
            this.clearWithPromptRadio = new System.Windows.Forms.RadioButton();
            this.autoClearRadio = new System.Windows.Forms.RadioButton();
            this.whereToBackupBox.SuspendLayout();
            this.clearingFoldersGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AllFilesBtn
            // 
            this.AllFilesBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.AllFilesBtn.Location = new System.Drawing.Point(7, 254);
            this.AllFilesBtn.Name = "AllFilesBtn";
            this.AllFilesBtn.Size = new System.Drawing.Size(172, 40);
            this.AllFilesBtn.TabIndex = 0;
            this.AllFilesBtn.Text = "Backup All Files";
            this.AllFilesBtn.UseVisualStyleBackColor = true;
            // 
            // CommonFilesBtn
            // 
            this.CommonFilesBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CommonFilesBtn.Location = new System.Drawing.Point(180, 254);
            this.CommonFilesBtn.Name = "CommonFilesBtn";
            this.CommonFilesBtn.Size = new System.Drawing.Size(172, 40);
            this.CommonFilesBtn.TabIndex = 1;
            this.CommonFilesBtn.Text = "Backup Only Common Files";
            this.CommonFilesBtn.UseVisualStyleBackColor = true;
            // 
            // TextBoxLabel
            // 
            this.TextBoxLabel.AutoSize = true;
            this.TextBoxLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextBoxLabel.Location = new System.Drawing.Point(7, 3);
            this.TextBoxLabel.Name = "TextBoxLabel";
            this.TextBoxLabel.Size = new System.Drawing.Size(280, 15);
            this.TextBoxLabel.TabIndex = 2;
            this.TextBoxLabel.Text = "Paths to Backup. C for Common; U for Uncommon.";
            // 
            // pathsTextBox
            // 
            this.pathsTextBox.AcceptsReturn = true;
            this.pathsTextBox.Location = new System.Drawing.Point(7, 21);
            this.pathsTextBox.Multiline = true;
            this.pathsTextBox.Name = "pathsTextBox";
            this.pathsTextBox.Size = new System.Drawing.Size(345, 227);
            this.pathsTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(7, 297);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Options";
            // 
            // whereToBackupBox
            // 
            this.whereToBackupBox.Controls.Add(this.textBox2);
            this.whereToBackupBox.Controls.Add(this.checkBox1);
            this.whereToBackupBox.Controls.Add(this.textBox1);
            this.whereToBackupBox.Controls.Add(this.path1CheckBox);
            this.whereToBackupBox.Location = new System.Drawing.Point(7, 315);
            this.whereToBackupBox.Name = "whereToBackupBox";
            this.whereToBackupBox.Size = new System.Drawing.Size(345, 77);
            this.whereToBackupBox.TabIndex = 5;
            this.whereToBackupBox.TabStop = false;
            this.whereToBackupBox.Text = "Where to Backup";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(83, 47);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(243, 23);
            this.textBox2.TabIndex = 3;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox1.Location = new System.Drawing.Point(3, 49);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 19);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Use Path 2:";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(83, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(243, 23);
            this.textBox1.TabIndex = 1;
            // 
            // path1CheckBox
            // 
            this.path1CheckBox.AutoSize = true;
            this.path1CheckBox.Checked = true;
            this.path1CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.path1CheckBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.path1CheckBox.Location = new System.Drawing.Point(3, 20);
            this.path1CheckBox.Name = "path1CheckBox";
            this.path1CheckBox.Size = new System.Drawing.Size(84, 19);
            this.path1CheckBox.TabIndex = 0;
            this.path1CheckBox.Text = "Use Path 1:";
            this.path1CheckBox.UseVisualStyleBackColor = true;
            // 
            // clearingFoldersGroupBox
            // 
            this.clearingFoldersGroupBox.Controls.Add(this.dontClearRadio);
            this.clearingFoldersGroupBox.Controls.Add(this.clearWithPromptRadio);
            this.clearingFoldersGroupBox.Controls.Add(this.autoClearRadio);
            this.clearingFoldersGroupBox.Location = new System.Drawing.Point(7, 396);
            this.clearingFoldersGroupBox.Name = "clearingFoldersGroupBox";
            this.clearingFoldersGroupBox.Size = new System.Drawing.Size(345, 100);
            this.clearingFoldersGroupBox.TabIndex = 0;
            this.clearingFoldersGroupBox.TabStop = false;
            this.clearingFoldersGroupBox.Text = "Clearing Backup Folders";
            // 
            // dontClearRadio
            // 
            this.dontClearRadio.AutoSize = true;
            this.dontClearRadio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dontClearRadio.Location = new System.Drawing.Point(3, 72);
            this.dontClearRadio.Name = "dontClearRadio";
            this.dontClearRadio.Size = new System.Drawing.Size(125, 19);
            this.dontClearRadio.TabIndex = 3;
            this.dontClearRadio.TabStop = true;
            this.dontClearRadio.Text = "Don\'t Clear Folders";
            this.dontClearRadio.UseVisualStyleBackColor = true;
            // 
            // clearWithPromptRadio
            // 
            this.clearWithPromptRadio.AutoSize = true;
            this.clearWithPromptRadio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.clearWithPromptRadio.Location = new System.Drawing.Point(3, 47);
            this.clearWithPromptRadio.Name = "clearWithPromptRadio";
            this.clearWithPromptRadio.Size = new System.Drawing.Size(162, 19);
            this.clearWithPromptRadio.TabIndex = 2;
            this.clearWithPromptRadio.TabStop = true;
            this.clearWithPromptRadio.Text = "Clear Folders with Prompt";
            this.clearWithPromptRadio.UseVisualStyleBackColor = true;
            // 
            // autoClearRadio
            // 
            this.autoClearRadio.AutoSize = true;
            this.autoClearRadio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.autoClearRadio.Location = new System.Drawing.Point(3, 22);
            this.autoClearRadio.Name = "autoClearRadio";
            this.autoClearRadio.Size = new System.Drawing.Size(240, 19);
            this.autoClearRadio.TabIndex = 0;
            this.autoClearRadio.TabStop = true;
            this.autoClearRadio.Text = "Clear Folders Automatically (No Prompt)";
            this.autoClearRadio.UseVisualStyleBackColor = true;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 501);
            this.Controls.Add(this.clearingFoldersGroupBox);
            this.Controls.Add(this.whereToBackupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pathsTextBox);
            this.Controls.Add(this.TextBoxLabel);
            this.Controls.Add(this.CommonFilesBtn);
            this.Controls.Add(this.AllFilesBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.Text = "Graphical Backup Program (GBP)";
            this.whereToBackupBox.ResumeLayout(false);
            this.whereToBackupBox.PerformLayout();
            this.clearingFoldersGroupBox.ResumeLayout(false);
            this.clearingFoldersGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AllFilesBtn;
        private System.Windows.Forms.Button CommonFilesBtn;
        private System.Windows.Forms.Label TextBoxLabel;
        private System.Windows.Forms.TextBox pathsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox whereToBackupBox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox path1CheckBox;
        private System.Windows.Forms.GroupBox clearingFoldersGroupBox;
        private System.Windows.Forms.RadioButton dontClearRadio;
        private System.Windows.Forms.RadioButton clearWithPromptRadio;
        private System.Windows.Forms.RadioButton autoClearRadio;
    }
}

