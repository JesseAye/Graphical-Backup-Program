﻿using System;
using System.IO;
using FileSystem = Microsoft.VisualBasic.FileIO.FileSystem;

namespace Graphical_Backup_Program
{
    public partial class Form : System.Windows.Forms.Form
    {
        //https://stackoverflow.com/a/11882118
        private readonly string _projectDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;

        public Form()
        {
            InitializeComponent();
        }

        //Used for copying a single item to path1 and/or path2, and for putting some log output in the paths TextBox. pathNum is either 1 or 2.
        private void CopyAndLog(string src, string dest, int pathNum)
        {
            src = src.Trim(); //Remove pesky whitespace from start and end of path.

            //If user wants stuff to be copied to a special timestamp folder, this is where it's done at.
            if (createTimestampFolderBtn.Checked)
            {
                string timestamp = DateTime.Now.ToString("M-d-yyyy hh;mm tt"); //'/' and ':' won't work in paths because Windows.
                dest = Path.Combine(dest, "GBP backup " + timestamp);
            }

            if (Path.HasExtension(src)) //if a file
            {
                string srcFileName = Path.GetFileName(src);
                string finalDest = Path.Combine(dest, srcFileName);

                if (!Directory.Exists(dest))
                    Directory.CreateDirectory(dest);

                try
                {
                    File.Copy(src, finalDest);
                }
                catch (DirectoryNotFoundException)
                {
                    pathsTextBox.Text += "ERROR when trying to copy file " + src + "\r\nCould not find path. Did you enter the path correctly?\r\n\r\n";
                    return;
                }
                catch (IOException)
                {
                    pathsTextBox.Text += "ERROR when trying to copy file " + src + "\r\nMost likely the path already exists\r\n\r\n";
                    return;
                }

                if (File.Exists(finalDest))
                    pathsTextBox.Text += "Successfully copied file " + src + " to path" + pathNum + "\r\n";
                else
                    pathsTextBox.Text += "ERROR. File " + src + " was NOT successfully copied to path" + pathNum + "\r\n";
            }
            else //if a folder
            {
                //Get the name of the folder and copy stuff there. CopyDirectory() doesn't do that automatically for some reason... https://stackoverflow.com/a/5229311
                string dirName = new DirectoryInfo(src).Name;
                string fullPath = Path.Combine(dest, dirName);

                if (!Directory.Exists(dest))
                    Directory.CreateDirectory(dest);

                try
                {
                    FileSystem.CopyDirectory(src, fullPath); //https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualbasic.fileio.filesystem.copydirectory?view=net-5.0
                }
                catch (DirectoryNotFoundException)
                {
                    pathsTextBox.Text += "ERROR when trying to copy folder " + src + "\r\nCould not find path. Did you enter the path correctly?\r\n\r\n";
                    return;
                }
                catch (IOException)
                {
                    pathsTextBox.Text += "ERROR when trying to copy folder " + src + "\r\nMost likely the path already exists\r\n\r\n";
                    return;
                }

                if (Directory.Exists(fullPath))
                    pathsTextBox.Text += "Successfully copied folder " + src + " to path" + pathNum + "\r\n\r\n";
                else
                    pathsTextBox.Text += "ERROR. Folder " + src + " was NOT successfully copied to path" + pathNum + "\r\n\r\n";
            }
        }

        //When backup completes, open path1 and/or path2 in File Explorer if user checks their box.
        private void ShowPath1And2()
        {
            if (openPath1Box.Checked)
                OpenInExplorer(path1TextBox.Text);

            if (openPath2Box.Checked)
                OpenInExplorer(path2TextBox.Text);
        }

        //Open an item in Explorer. https://stackoverflow.com/a/13680458
        private void OpenInExplorer(string path)
        {
            if (path != String.Empty)
            {
                path = System.IO.Path.GetFullPath(path);
                System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{path}\"");
            }
        }

        private void AllPathsBtn_Click(object sender, EventArgs e)
        {
            TextBoxLabel.Text = "Log";
            File.WriteAllText(_projectDirectory + "/paths.txt", pathsTextBox.Text);

            //When user wants to begin copying all C and U paths, go through line by line and determine which ones are marked C or U.
            string[] allPaths = pathsTextBox.Text.Split("\r\n");
            pathsTextBox.Text = "Backing up all C and U items...\r\n----------------------------------------------------------------------------------------------------------------------------\r\n";

            foreach (string path in allPaths)
            {
                string trimmedPath = path.Substring(2); //path without char and ' '.

                if (Char.ToLower(path[0]) is 'c' or 'u')
                {
                    if (backupModeBtn.Checked)
                    {
                        //Copy each item to path1 and/or path2, as long as the box is checked AND the TextBox isn't blank.
                        if (path1CheckBox.Checked && path1TextBox.Text != String.Empty)
                            CopyAndLog(trimmedPath, path1TextBox.Text, 1);

                        if (path2CheckBox.Checked && path2TextBox.Text != String.Empty)
                            CopyAndLog(trimmedPath, path2TextBox.Text, 2);
                    }
                    else if (fileExplorerBtn.Checked)
                        OpenInExplorer(trimmedPath);
                }
                else
                    pathsTextBox.Text += "\r\nSkipping path " + trimmedPath + "\r\nGBP cannot understand this line\r\n";
            }

            pathsTextBox.Text += "----------------------------------------------------------------------------------------------------------------------------\r\nGBP has finished the backup.";
            allPathsBtn.Enabled = false;
            commonPathsBtn.Enabled = false;
            resetBtn.Enabled = true;
            ShowPath1And2();
        }

        private void CommonPathsBtn_Click(object sender, EventArgs e)
        {
            TextBoxLabel.Text = "Log";
            File.WriteAllText(_projectDirectory + "/paths.txt", pathsTextBox.Text);

            //When user wants to begin copying just the Common Paths, go through line by line and determine which ones are marked 'common' (c).
            string[] allPaths = pathsTextBox.Text.Split("\r\n");
            pathsTextBox.Text = "Backing up just common items...\r\n-----------------------------------------------------------------\r\n";

            foreach (string path in allPaths)
            {
                string trimmedPath = path.Substring(2); //path without char and ' '.

                if (Char.ToLower(path[0]) == 'c')
                {
                    if (backupModeBtn.Checked)
                    {
                        //Copy each item to path1 and/or path2, as long as the box is checked AND the TextBox isn't blank.
                        if (path1CheckBox.Checked && path1TextBox.Text != String.Empty)
                            CopyAndLog(trimmedPath, path1TextBox.Text, 1);

                        if (path2CheckBox.Checked && path2TextBox.Text != String.Empty)
                            CopyAndLog(trimmedPath, path2TextBox.Text, 2);
                    }
                    else if (fileExplorerBtn.Checked)
                        OpenInExplorer(trimmedPath);
                }
                else if (Char.ToLower(path[0]) == 'u')
                    pathsTextBox.Text += "Skipping path " + trimmedPath + "\r\nbecause it is marked 'U'\r\n";
                else
                    pathsTextBox.Text += "\r\nSkipping path " + trimmedPath + "\r\nGBP cannot understand this line\r\n";
            }

            pathsTextBox.Text += "----------------------------------------------------------------------------------------------------------------------------\r\nGBP has finished the backup.";
            allPathsBtn.Enabled = false;
            commonPathsBtn.Enabled = false;
            resetBtn.Enabled = true;
            ShowPath1And2();
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //If both of these are disabled, don't allow user to push buttons cuz that doesn't make any sense.
            if (path1CheckBox.Checked == false && path2CheckBox.Checked == false)
            {
                allPathsBtn.Enabled = false;
                commonPathsBtn.Enabled = false;
            }
            else
            {
                allPathsBtn.Enabled = true;
                commonPathsBtn.Enabled = true;
            }
        }

        private void PathTextBox_TextChanged(object sender, EventArgs e)
        {
            //If both of these are blank, don't allow user to push buttons cuz that doesn't make any sense.
            if (path1TextBox.Text == String.Empty && path2TextBox.Text == String.Empty)
            {
                allPathsBtn.Enabled = false;
                commonPathsBtn.Enabled = false;
            }
            else
            {
                allPathsBtn.Enabled = true;
                commonPathsBtn.Enabled = true;
            }
        }

        private void PathsTextBox_TextChanged(object sender, EventArgs e)
        {
            //Don't allow buttons to be pressed if the paths TextBox is empty.
            if (pathsTextBox.Text == String.Empty)
            {
                allPathsBtn.Enabled = false;
                commonPathsBtn.Enabled = false;
            }
            else
            {
                allPathsBtn.Enabled = true;
                commonPathsBtn.Enabled = true;
            }
        }

        //On startup, assign GUI controls values from files, and disable any controls, if necessary.
        private void Form_Shown(object sender, EventArgs e)
        {
            pathsTextBox.Text = File.ReadAllText(_projectDirectory + "/paths.txt");

            //Read in config stuff. Is this stupid? Yes. Does it work? Also yes.
            string configFileTxt = File.ReadAllText(_projectDirectory + "/Config.txt");
            string[] config = configFileTxt.Split("\r\n");
            path1CheckBox.Checked = Boolean.Parse(config[0]);
            path1TextBox.Text = config[1];
            path2CheckBox.Checked = Boolean.Parse(config[2]);
            path2TextBox.Text = config[3];
            autoClearRadio.Checked = Boolean.Parse(config[4]);
            clearWithPromptRadio.Checked = Boolean.Parse(config[5]);
            dontClearRadio.Checked = Boolean.Parse(config[6]);
            backupModeBtn.Checked = Boolean.Parse(config[7]);
            fileExplorerBtn.Checked = Boolean.Parse(config[8]);
            createTimestampFolderBtn.Checked = Boolean.Parse(config[9]);
            dontCreateFolderBtn.Checked = Boolean.Parse(config[10]);

            if (pathsTextBox.Text == String.Empty || (path1TextBox.Text == String.Empty && path2TextBox.Text == String.Empty) || (path1CheckBox.Checked == false && path2CheckBox.Checked == false))
            {
                allPathsBtn.Enabled = false;
                commonPathsBtn.Enabled = false;
            }
            else
            {
                allPathsBtn.Enabled = true;
                commonPathsBtn.Enabled = true;
            }

            if (fileExplorerBtn.Checked)
            {
                allPathsBtn.Enabled = true;
                commonPathsBtn.Enabled = true;
            }
        }

        //On exit, save config stuff for next time.
        private void Form_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            string fileText = path1CheckBox.Checked + "\r\n" + path1TextBox.Text + "\r\n" + path2CheckBox.Checked + "\r\n" + path2TextBox.Text + "\r\n" + autoClearRadio.Checked + "\r\n" + clearWithPromptRadio.Checked + "\r\n" + dontClearRadio.Checked + "\r\n" + backupModeBtn.Checked + "\r\n" + fileExplorerBtn.Checked + "\r\n" + createTimestampFolderBtn.Checked + "\r\n" + dontCreateFolderBtn.Checked;
            File.WriteAllText(_projectDirectory + "/Config.txt", fileText);
        }

        //Press this to run another backup without having to restart the whole program.
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            pathsTextBox.Text = File.ReadAllText(_projectDirectory + "/paths.txt");
            TextBoxLabel.Text = "Paths to Backup. C for Common, U for Uncommon, other characters are ignored.";
            allPathsBtn.Enabled = true;
            commonPathsBtn.Enabled = true;
            resetBtn.Enabled = false;
        }

        private void clearPath1_Click(object sender, EventArgs e)
        {
            path1TextBox.Text = "";
        }

        private void clearPath2_Click(object sender, EventArgs e)
        {
            path2TextBox.Text = "";
        }

        private void backupModeBtn_CheckedChanged(object sender, EventArgs e)
        {
            allPathsBtn.Text = "Backup All Paths";
            commonPathsBtn.Text = "Backup Just Common Paths";

            if (pathsTextBox.Text == String.Empty || (path1TextBox.Text == String.Empty && path2TextBox.Text == String.Empty) || (path1CheckBox.Checked == false && path2CheckBox.Checked == false))
            {
                allPathsBtn.Enabled = false;
                commonPathsBtn.Enabled = false;
            }
            else
            {
                allPathsBtn.Enabled = true;
                commonPathsBtn.Enabled = true;
            }

            path1CheckBox.Enabled = true;
            path1TextBox.Enabled = true;
            path2CheckBox.Enabled = true;
            path2TextBox.Enabled = true;
            clearPath1.Enabled = true;
            clearPath2.Enabled = true;
            openPath1Box.Enabled = true;
            openPath2Box.Enabled = true;
            whereToBackupBox.Enabled = true;
            clearingFoldersGroupBox.Enabled = true;
            autoClearRadio.Enabled = true;
            clearWithPromptRadio.Enabled = true;
            dontClearRadio.Enabled = true;
            backupMode.Enabled = true;
            createTimestampFolderBtn.Enabled = true;
            dontCreateFolderBtn.Enabled = true;
        }

        private void fileExplorerBtn_CheckedChanged(object sender, EventArgs e)
        {
            allPathsBtn.Text = "Open All Paths in File Explorer";
            commonPathsBtn.Text = "Open Just Common Paths in File Explorer";

            allPathsBtn.Enabled = true;
            commonPathsBtn.Enabled = true;

            //Disable controls that don't make sense with this enabled.
            path1CheckBox.Enabled = false;
            path1TextBox.Enabled = false;
            path2CheckBox.Enabled = false;
            path2TextBox.Enabled = false;
            clearPath1.Enabled = false;
            clearPath2.Enabled = false;
            openPath1Box.Enabled = false;
            openPath2Box.Enabled = false;
            whereToBackupBox.Enabled = false;
            clearingFoldersGroupBox.Enabled = false;
            autoClearRadio.Enabled = false;
            clearWithPromptRadio.Enabled = false;
            dontClearRadio.Enabled = false;
            backupMode.Enabled = false;
            createTimestampFolderBtn.Enabled = false;
            dontCreateFolderBtn.Enabled = false;
        }
    }
}
