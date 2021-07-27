﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.VisualBasic;
using FileSystem = Microsoft.VisualBasic.FileIO.FileSystem;

namespace Graphical_Backup_Program
{
    public partial class Form : System.Windows.Forms.Form
    {
        //https://stackoverflow.com/a/11882118
        private readonly string _projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        public Form()
        {
            InitializeComponent();
        }

        //TODO: ParseAllPaths()

        //When user wants to begin copying just the Common Paths, go through line by line and determine which ones are marked 'common'.
        private List<string> ParseCommonPaths()
        {
            string[] allPaths = pathsTextBox.Text.Split("\r\n");
            List<string> commonPaths = new();

            foreach (string path in allPaths)
            {
                if (Char.ToLower(path[0]) == 'c')
                {
                    string splitPath = path.Substring(2);
                    commonPaths.Add(splitPath);
                    if (splitPath[0] == ' ')
                        splitPath = splitPath.Substring(1);
                }
            }

            return commonPaths;
        }

        //Returns true if path is a file, false if not (folder). Might not be best method...
        private bool IsFile(string path)
        {
            return Path.HasExtension(path);
        }

        //Used for copying a single item to path1 and/or path2, and for putting some log output in the paths TextBox.
        //pathNum is either 1 or 2.
        private void CopyAndLog(string src, string dest, int pathNum)
        {
            if (IsFile(src))
            {
                pathsTextBox.Text += "Copying file " + src + " to path" + pathNum + "\r\n";
                string srcFileName = Path.GetFileName(src);
                string finalDest = Path.Combine(dest, srcFileName);
                File.Copy(src, finalDest);


                if (File.Exists(src))
                    pathsTextBox.Text += "Successfully copied file " + src + " to path" + pathNum + "\r\n\r\n";
                else
                    pathsTextBox.Text += "ERROR. File " + src + " was NOT successfully copied to path" + pathNum + "\r\n\r\n";
            }
            else
            {
                pathsTextBox.Text += "Copying folder " + src + " to path" + pathNum + "\r\n";
                //CopyDirectory(src, dest);

                //Get the name of the folder and copy stuff there. CopyDirectory() doesn't do that automatically for some reason... https://stackoverflow.com/a/5229311
                string dirName = new DirectoryInfo(src).Name;
                string fullPath = Path.Combine(dest, dirName);
                FileSystem.CopyDirectory(src, fullPath); //https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualbasic.fileio.filesystem.copydirectory?view=net-5.0

                if (Directory.Exists(src))
                    pathsTextBox.Text += "Successfully copied folder " + src + " to path" + pathNum + "\r\n\r\n";
                else
                    pathsTextBox.Text += "ERROR. Folder " + src + " was NOT successfully copied to path" + pathNum + "\r\n\r\n";

            }
        }

        private void AllPathsBtn_Click(object sender, EventArgs e)
        {
            //File.WriteAllText(_projectDirectory + "/paths.txt", pathsTextBox.Text);
            //TODO
        }

        private void CommonPathsBtn_Click(object sender, EventArgs e)
        {
            File.WriteAllText(_projectDirectory + "/paths.txt", pathsTextBox.Text);
            List<string> commonPaths = ParseCommonPaths();
            pathsTextBox.Text = "Backing up items...\r\n---------------------------------------------------------------\r\n";

            foreach (string path in commonPaths)
            {
                //Copy each item to path1 and/or path2, as long as the box is checked AND the TextBox isn't blank.
                if (path1CheckBox.Checked && path1TextBox.Text != String.Empty)
                    CopyAndLog(path, path1TextBox.Text, 1);

                if (path2CheckBox.Checked && path2TextBox.Text != String.Empty)
                    CopyAndLog(path, path2TextBox.Text, 2);
            }
            pathsTextBox.Text += "---------------------------------------------------------------";
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //If both of these are disabled, don't allow user to push buttons cuz that doesn't make any sense.
            if (path1CheckBox.Checked == false && path2CheckBox.Checked == false)
            {
                AllFilesBtn.Enabled = false;
                CommonFilesBtn.Enabled = false;
            }
            else
            {
                AllFilesBtn.Enabled = true;
                CommonFilesBtn.Enabled = true;
            }
        }

        public void PathTextBox_TextChanged(object sender, EventArgs e)
        {
            //If both of these are blank, don't allow user to push buttons cuz that doesn't make any sense.
            if (path1TextBox.Text == String.Empty && path2TextBox.Text == String.Empty)
            {
                AllFilesBtn.Enabled = false;
                CommonFilesBtn.Enabled = false;
            }
            else
            {
                AllFilesBtn.Enabled = true;
                CommonFilesBtn.Enabled = true;
            }
        }

        private void PathsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (pathsTextBox.Text == String.Empty)
            {
                AllFilesBtn.Enabled = false;
                CommonFilesBtn.Enabled = false;
            }
            else
            {
                AllFilesBtn.Enabled = true;
                CommonFilesBtn.Enabled = true;
            }
        }

        //Perform some necessary initialization stuff when program is ran.
        private void Form_Shown(object sender, EventArgs e)
        {
            pathsTextBox.Text = File.ReadAllText(_projectDirectory + "/paths.txt");

            if (File.Exists(_projectDirectory + "/App.config"))
            {
                path1CheckBox.Checked = Boolean.Parse(ConfigurationManager.AppSettings.Get("path1Checked") ?? throw new InvalidOperationException()); //https://stackoverflow.com/questions/446835/what-do-two-question-marks-together-mean-in-c
                path1TextBox.Text = ConfigurationManager.AppSettings.Get("path1Text");
                path2CheckBox.Checked = Boolean.Parse(ConfigurationManager.AppSettings.Get("path2Checked") ?? throw new InvalidOperationException());
                path2TextBox.Text = ConfigurationManager.AppSettings.Get("path2Text");

                autoClearRadio.Checked = Boolean.Parse(ConfigurationManager.AppSettings.Get("autoClearRadioChecked") ?? throw new InvalidOperationException());
                clearWithPromptRadio.Checked = Boolean.Parse(ConfigurationManager.AppSettings.Get("clearWithPromptRadioChecked") ?? throw new InvalidOperationException());
                dontClearRadio.Checked = Boolean.Parse(ConfigurationManager.AppSettings.Get("dontClearRadioChecked") ?? throw new InvalidOperationException());
            }

            if (pathsTextBox.Text == String.Empty || (path1TextBox.Text == String.Empty && path2TextBox.Text == String.Empty) || (path1CheckBox.Checked == false && path2CheckBox.Checked == false))
            {
                AllFilesBtn.Enabled = false;
                CommonFilesBtn.Enabled = false;
            }
            else
            {
                AllFilesBtn.Enabled = true;
                CommonFilesBtn.Enabled = true;
            }

        }

        //On exit, write stuff to config file for next time.
        private void Form_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            File.WriteAllText(_projectDirectory + "/paths.txt", pathsTextBox.Text);

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["path1Checked"].Value = path1CheckBox.Checked.ToString();
            config.AppSettings.Settings["path1Text"].Value = path1TextBox.Text;
            config.AppSettings.Settings["path2Checked"].Value = path2CheckBox.Checked.ToString();
            config.AppSettings.Settings["path2Text"].Value = path2TextBox.Text;

            config.AppSettings.Settings["autoClearRadioChecked"].Value = autoClearRadio.Checked.ToString();
            config.AppSettings.Settings["clearWithPromptRadioChecked"].Value = clearWithPromptRadio.Checked.ToString();
            config.AppSettings.Settings["dontClearRadioChecked"].Value = dontClearRadio.Checked.ToString();

            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
