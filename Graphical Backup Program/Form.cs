using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using FileSystem = Microsoft.VisualBasic.FileIO.FileSystem;

namespace Graphical_Backup_Program
{
	public partial class Form : System.Windows.Forms.Form
	{
		#region Private Fields

		private string timestamp;

		/// <summary>
		/// Provides reference/context to which directory this program is running from. If debugger is attached, set directory to base project directory. Else, set it to regular filepath of exe<para/>
		/// <see href="https://stackoverflow.com/questions/1343053/detecting-if-a-program-was-run-by-visual-studio-as-opposed-to-run-from-windows">Check if debugger is attached</see><para/>
		/// <see href="https://stackoverflow.com/a/11882118">Setting the directory depending on context (debugging or standalone exe)</see>
		/// </summary>
		private static string ProjectDirectory { get { return Debugger.IsAttached ? Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName : Environment.CurrentDirectory; } }

		/// <summary>
		/// Reference to the config file location
		/// </summary>
		private static string ConfigFilePath { get { return ProjectDirectory + "/config.txt"; } }

		/// <summary>
		/// Reference to the paths file location
		/// </summary>
		private static string PathsFilePath { get { return ProjectDirectory + "/paths.txt"; } }

		/// <summary>
		/// If at least one of the groupCheckBoxes are checked
		/// </summary>
		private bool OneGroupIsChecked = false;

		#endregion

		/// <summary>
		/// Program.cs enters here. Initialize components.
		/// </summary>
		public Form()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Verifies which path to back up to, and that the path string is not empty
		/// </summary>
		/// <param name="trimmedPath">The path without the group number at the beginning</param>
		private void CopyBackupPath(string trimmedPath)
		{
			if (path1Btn.Checked && path1TextBox.Text != String.Empty)
				CopyAndLog(trimmedPath, path1TextBox.Text, 1);
			else if (path2Btn.Checked && path2TextBox.Text != String.Empty)
				CopyAndLog(trimmedPath, path2TextBox.Text, 2);
		}

		/// <summary>
		/// Used for copying a single item to path1 or path2, and for putting some log output in the paths TextBox. pathNum is either 1 or 2.
		/// </summary>
		/// <param name="src"></param>
		/// <param name="dest"></param>
		/// <param name="pathNum"></param>
		private void CopyAndLog(string src, string dest, int pathNum)
		{
			//TODO: Unable to protect this program from IOException when performing File.Copy() or FileSystem.CopyDirectory(), seems to happen on files with system Attribute.
			//A major problem with .NET in regards to this project is the lack of ability to perform File/Directory copy and filter out system objects that will present access issues
			//Recursive folder creation and File.Copy() is an answer, but we can run into StackOverflow depending on depth of folders:
			//Ancestry issue & StackOverflow exception: https://www.codeproject.com/Tips/512208/Folder-Directory-Deep-Copy-including-sub-directori
			dest = Path.Combine(dest, "GBP Backup " + timestamp);

			if (Path.HasExtension(src)) //if a file
			{
				string srcFileName = Path.GetFileName(src);
				string finalDest = Path.Combine(dest, srcFileName);

				if (!Directory.Exists(dest))
					Directory.CreateDirectory(dest);

				try
				{
					//DirectoryInfo directoryInfo = new DirectoryInfo(src);
					//var test = directoryInfo.GetNonSystemFileInfosRecursive("*");
					File.Copy(src, finalDest);
				}

				catch (DirectoryNotFoundException e)
				{
					GBP_Log.Append("ERROR when trying to copy file " + src + "\r\nCould not find path. Did you enter the path correctly?\r\n" + e.Message + Environment.NewLine);
					return;
				}

				catch (IOException e)
				{
					if (e.Message.Contains("being used by another process."))
					{
						//TODO: Figure out how to handle files/folders being used by another process - This may be resolved by excluding system files/folders from being copied
					}

					else
					{
						GBP_Log.Append("ERROR when trying to copy file " + src + "\r\nMost likely the path already exists\r\n" + e.Message + Environment.NewLine);
					}
					return;
				}

				catch (Exception e)
				{
					GBP_Log.Append("ERROR: " + e.Message);
				}

				if (File.Exists(finalDest))
					GBP_Log.Append("\r\nSuccessfully copied file " + src + " to path" + pathNum + "\r\n");
				else
					GBP_Log.Append("\r\nERROR. File " + src + " was NOT successfully copied to path" + pathNum + "\r\n");
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
					//DirectoryInfo directoryInfo = new DirectoryInfo(src);
					//var test = directoryInfo.GetNonSystemFileInfosRecursive("*");
					FileSystem.CopyDirectory(src, fullPath); //https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualbasic.fileio.filesystem.copydirectory?view=net-5.0
				}

				catch (DirectoryNotFoundException e)
				{
					GBP_Log.Append("ERROR when trying to copy folder " + src + "\r\nCould not find path. Did you enter the path correctly?\r\n" + e.Message + Environment.NewLine);
					return;
				}

				catch (IOException e)
				{
					GBP_Log.Append("ERROR when trying to copy folder " + src + "\r\nMost likely the path already exists\r\n\r\n" + e.Message + Environment.NewLine);
					return;
				}

				catch (Exception e)
				{
					GBP_Log.Append("ERROR: " + e.Message);
				}

				if (Directory.Exists(fullPath))
					GBP_Log.Append("\r\nSuccessfully copied folder " + src + " to path" + pathNum + "\r\n");
				else
					GBP_Log.Append("\r\nERROR. Folder " + src + " was NOT successfully copied to path" + pathNum + "\r\n");
			}
		}

		/// <summary>
		/// When backup completes, open path1 and/or path2 in File Explorer if user checks the box to copy stuff there and to open it in File Explorer.
		/// </summary>
		private void ShowBackupPath()
		{
			if (openOnComplete.Checked)
			{
				if (path1Btn.Checked)
					OpenInExplorer(zipCheckBox.Checked ? Path.Combine(path1TextBox.Text, "GBP Backup " + timestamp + ".zip") : Path.Combine(path1TextBox.Text, "GBP Backup " + timestamp));

				else if (path2Btn.Checked)
					OpenInExplorer(zipCheckBox.Checked ? Path.Combine(path2TextBox.Text, "GBP Backup " + timestamp + ".zip") : Path.Combine(path2TextBox.Text, "GBP Backup " + timestamp));
			}
		}
		
		/// <summary>
		/// Prompts the user with Yes/No/Cancel MessageBox if they would like to delete the contents of the folder defined by whichever path TextBox is checked<para/>
		/// This is asked before backup is performed
		/// </summary>
		/// <returns>
		/// True if the user's choice did not cause an issue<para/>
		/// False if there was an issue processing the user's choice
		/// </returns>
		private static bool ClearFolder(string dir)
		{
			switch (MessageBox.Show($"Would you like to delete all of the contents of {dir} before backing up?", "Delete backup folder before?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
			{
				case DialogResult.Yes:
					return DeleteDirectory(dir);

				case DialogResult.No:
					return true;

				case DialogResult.Cancel:
					return false;

				default:
					throw new Exception("ClearFolder() DialogResult is not handled. Somehow it didn't return Yes/No/Cancel.");
			}
		}

		/// <summary>
		/// Checks if a group is checkmarked/enabled
		/// </summary>
		/// <param name="group">The group to check</param>
		/// <returns>True if checked, false if not</returns>
		private bool GroupChecked(char group)
		{
			return group == '0' && checkBox0.Checked ||
					group == '1' && checkBox1.Checked ||
					group == '2' && checkBox2.Checked ||
					group == '3' && checkBox3.Checked ||
					group == '4' && checkBox4.Checked ||
					group == '5' && checkBox5.Checked ||
					group == '6' && checkBox6.Checked ||
					group == '7' && checkBox7.Checked ||
					group == '8' && checkBox8.Checked ||
					group == '9' && checkBox9.Checked;
		}

		/// <summary>
		/// Updates progress bar based on size of backup folder against the size of the folder to be backed up
		/// </summary>
		/// <param name="backupPath">The destination path of the backup</param>
		private void RunProgressBar(string backupPath)
		{
			double currentSize = 0;
			double finalSize = UpdateBackupSize();

			//TODO: There are instances where currentSize never reaches finalSize, causing a perpetually true while loop
			while (currentSize < finalSize)
			{
				currentSize = GetFolderSize(Path.Combine(backupPath, "GBP Backup " + timestamp));
				int progress = Convert.ToInt32((currentSize / finalSize) * 100);
				progressBar.Value = progress;
				TaskbarProgress.SetValue(Handle, progress, 100);
			}
		}

		/// <summary>
		/// Creates a compressed backup after all files/folders are copied to the backup folder
		/// </summary>
		/// <param name="textBoxText">Path of the Backup folder</param>
		/// <param name="pathNum">Only used in the log file to explain which pathTextBox was selected when backup was created</param>
		private void CompressBackup(string textBoxText, int pathNum)
		{
			string backupPath = Path.Combine(textBoxText, "GBP Backup " + timestamp);
			string zipPath = Path.Combine(textBoxText, "GBP Backup " + timestamp + ".zip");
			ZipFile.CreateFromDirectory(backupPath, zipPath);
			if (File.Exists(zipPath))
				GBP_Log.Append("\nSuccessfully compressed backup of path" + pathNum + '\n');
		}

		/// <summary>
		/// Gets the size of the backup as the user currently has it configured. Updates status strip with backup size, including setting the unit of measurement based on size.
		/// </summary>
		/// <returns>Backup size in bytes</returns>
		private double UpdateBackupSize()
		{
			double backupSize = 0;
			foreach (string line in pathsTextBox.Text.Split("\r\n"))
			{
				if (line == String.Empty || !GroupChecked(line[0])) continue;
				string path = line[2..];

				if (Path.HasExtension(path))
					backupSize += GetFileSize(path);
				else
					backupSize += GetFolderSize(path);
			}

			string unit;
			double convertedBackupSize;

			if (backupSize >= 1100000000000)
			{
				unit = "TB";
				convertedBackupSize = backupSize / 1100000000000;
			}
			else if (backupSize >= 1074000000)
			{
				unit = "GB";
				convertedBackupSize = backupSize / 1074000000;
			}
			else if (backupSize >= 1049000)
			{
				unit = "MB";
				convertedBackupSize = backupSize / 1049000;
			}
			else if (backupSize >= 1024)
			{
				unit = "KB";
				convertedBackupSize = backupSize / 1024;
			}
			else
			{
				unit = "bytes";
				convertedBackupSize = backupSize;
			}

			numberLabel.Text = Math.Round(convertedBackupSize, 3).ToString();
			unitLabel.Text = unit;

			return backupSize;
		}

		/// <summary>
		/// Save to the paths file, and save to the config file
		/// </summary>
		private void SaveToFiles()
		{
			File.WriteAllText(PathsFilePath, pathsTextBox.Text);
			string configFileText = checkBox0.Checked + "\r\n" + checkBox1.Checked + "\r\n" + checkBox2.Checked + "\r\n" + checkBox3.Checked + "\r\n" + checkBox4.Checked + "\r\n" + checkBox5.Checked + "\r\n" + checkBox6.Checked + "\r\n" + checkBox7.Checked + "\r\n" + checkBox8.Checked + "\r\n" + checkBox9.Checked + "\r\n" + textBox0.Text + "\r\n" + textBox1.Text + "\r\n" + textBox2.Text + "\r\n" + textBox3.Text + "\r\n" + textBox4.Text + "\r\n" + textBox5.Text + "\r\n" + textBox6.Text + "\r\n" + textBox7.Text + "\r\n" + textBox8.Text + "\r\n" + textBox9.Text + "\r\n" + path1Btn.Checked + "\r\n" + path1TextBox.Text + "\r\n" + path2Btn.Checked + "\r\n" + path2TextBox.Text + "\r\n" + openOnComplete.Checked + "\r\n" + urlCheckBox.Checked + "\r\n" + urlTextBox.Text + "\r\n" + zipCheckBox.Checked;
			File.WriteAllText(ConfigFilePath, configFileText);
		}

		/// <summary>
		/// Sets all checkBoxes to true or false
		/// </summary>
		/// <param name="toggled">What to set the checkBoxes to</param>
		private void ToggleAllChecks(bool toggled)
		{
			checkBox0.Checked = toggled;
			checkBox1.Checked = toggled;
			checkBox2.Checked = toggled;
			checkBox3.Checked = toggled;
			checkBox4.Checked = toggled;
			checkBox5.Checked = toggled;
			checkBox6.Checked = toggled;
			checkBox7.Checked = toggled;
			checkBox8.Checked = toggled;
			checkBox9.Checked = toggled;
		}

		/// <summary>
		/// Determines what text to put in the StatusStrip label and if the Backup Button needs to be disabled
		/// </summary>
		private void UpdateControls()
		{
			//TODO: Have a check in place to make sure pathsTextBox contains valid paths

			if (pathsTextBox.Text == String.Empty)
			{
				stripLabel.Text = "Enter at least 1 path in the big TextBox that you want backed up.";
				backupBtn.Enabled = false;
				return;
			}

			if (!OneGroupIsChecked)
			{
				stripLabel.Text = "At least one group needs to be checked to begin backup";
				backupBtn.Enabled = false;
				return;
			}

			if (path1Btn.Checked)
			{
				if (path1TextBox.Text == "")
				{
					stripLabel.Text = "Enter a backup path for path1";
					backupBtn.Enabled = false;
					return;
				}

				else if (Path.HasExtension(path1TextBox.Text))
				{
					stripLabel.Text = "path1 cannot be a file!";
					backupBtn.Enabled = false;
					return;
				}
			}

			else if (path2Btn.Checked && path2TextBox.Text == "")
			{
				if (path2TextBox.Text == "")
				{
					stripLabel.Text = "Enter a backup path for path2";
					backupBtn.Enabled = false;
					return;
				}

				else if (Path.HasExtension(path2TextBox.Text))
				{
					stripLabel.Text = "path2 cannot be a file!";
					backupBtn.Enabled = false;
					return;
				}
			}

			stripLabel.Text = "Ready to begin backup";
			backupBtn.Enabled = true;
		}

		#region Private Static Methods

		/// <summary>
		/// Gets the file size in bytes<para/>
		/// <see href="https://stackoverflow.com/questions/1380839/how-do-you-get-the-file-size-in-c/20863065">Get file size</see>
		/// </summary>
		/// <param name="path">The path of the file</param>
		/// <returns>Size in bytes</returns>
		private static long GetFileSize(string path)
		{
			if (File.Exists(path))
			{
				return new FileInfo(path).Length;
			}

			else
			{
				MessageBox.Show("Could not find file " + path, "Cannot Find File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return 0;
			}
		}

		/// <summary>
		/// Gets the file sizes of all files in a folder<para/>
		/// <see href="https://stackoverflow.com/questions/12166404/how-do-i-get-folder-size-in-c">Get size of files in folder</see>
		/// </summary>
		/// <param name="path">The folder with the files to get the sizes</param>
		/// <returns>Size in bytes</returns>
		private static long GetFolderSize(string path)
		{
			DirectoryInfo di = new(path);

			if (di.Exists)
			{
				return di.SafeGetFileInfosRecursive("*").Sum(x => x.Length);
			}

			else
			{
				MessageBox.Show("Could not find folder " + path, "Cannot Find Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return 0;
		}

		/// <summary>
		/// Quick check to make sure the group number is valid
		/// </summary>
		/// <param name="group">The group to check</param>
		/// <returns>True if valid, false if not</returns>
		private static bool ValidGroupChar(char group)
		{
			return (Char.GetNumericValue(group) >= 0 && Char.GetNumericValue(group) <= 9 && Char.IsNumber(group));
		}

		/// <summary>
		/// Opens a link in the default browser. Also checks for invalid ampersand char in URL, and attempts to correct it. <see href="https://stackoverflow.com/a/43232486">Reference for opening url in browser</see>
		/// </summary>
		/// <param name="url">The URL to attempt at opening</param>
		private static void OpenUrl(string url)
		{
			url = url.Replace("&", "^&"); //This is needed because command prompt needs to know that an ampersand in the url var is not to be parsed as a second command; it is part of the url

			if (url.StartsWith("http://"))
			{
				MessageBox.Show("The URL is not going to a secure version of the website, please double check the website loads as HTTPS", "SSL Site Not Requested", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			else if (!url.StartsWith("https://"))
			{
				url = $"https://{url}";
			}

			//I'm no injection/exploitation expert, but I've tried the following lines in the urlTextBox:
			//google.com^&cd C:\^&tree
			//^&cd C:\^&tree
			//^&cd ..^&tree
			//I can't get the process to follow through with the cd or the tree commands. Maybe it's safe?
			Process.Start(new ProcessStartInfo($"{url}") { CreateNoWindow = true, UseShellExecute = true });
		}

		/// <summary>
		/// Open (and highlight) an item in Explorer.<para/>
		/// <see href="https://stackoverflow.com/a/13680458">Link to source.</see>
		/// </summary>
		/// <param name="path">The file path that needs to be opened</param>
		private static void OpenInExplorer(string path)
		{
			if (path != String.Empty)
			{
				path = Path.GetFullPath(path);
				Process.Start("explorer.exe", $"/select,\"{path}\"");
			}
		}

		/// <summary>
		/// Delete a single directory, ignoring exception about it not existing/found.
		/// </summary>
		/// <param name="dir">The directory to be deleted</param>
		/// <exception cref="UnauthorizedAccessException"/>
		/// <exception cref="DirectoryNotFoundException"/>
		private static bool DeleteDirectory(string dir)
		{
			if (Directory.Exists(dir))
			{
				try
				{
					Directory.Delete(dir, true);
					return true;
				}

				catch (UnauthorizedAccessException e)
				{
					MessageBox.Show("An error occurred: " + e.Message);
					return false;
				}
			}

			else
			{
				MessageBox.Show($"{dir} was not found! Unable to delete the backup destination folder! Stopping backup.", "Couldn't Delete Backup Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		#endregion

		#region Form Events

		/// <summary>
		/// When any of the 10 group CheckBoxes are (un)checked. Calls UpdateControls() and UpdateBackupSize()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GroupCheckBoxes_CheckedChanged(object sender, EventArgs e)
		{
			OneGroupIsChecked = checkBox0.Checked
					   || checkBox1.Checked
					   || checkBox2.Checked
					   || checkBox3.Checked
					   || checkBox4.Checked
					   || checkBox5.Checked
					   || checkBox6.Checked
					   || checkBox7.Checked
					   || checkBox8.Checked
					   || checkBox9.Checked;

			UpdateControls();
			UpdateBackupSize();
		}

		/// <summary>
		/// Calls UpdateControls() when the checked PathsRadioBtn changes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PathRadioBtn_CheckedChanged(object sender, EventArgs e)
		{
			if (path1Btn.Checked)
			{

			}

			else if (path2Btn.Checked)
			{

			}

			UpdateControls();
		}

		/// <summary>
		/// Sets all checkBoxes to true
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectAllBtn_Click(object sender, EventArgs e)
		{
			ToggleAllChecks(true);
			UpdateControls();
			UpdateBackupSize();
		}

		/// <summary>
		/// Sets all checkBoxes to false
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeselectAllBtn_Click(object sender, EventArgs e)
		{
			ToggleAllChecks(false);
			UpdateControls();
			UpdateBackupSize();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// JesseAye - I haven't looked through this method yet, so I don't feel comfortable creating documentation on it at this time.
		private void SortBtn_Click(object sender, EventArgs e)
		{
			List<GroupPath> groupPaths = new();
			List<GroupPath> otherGroupPaths = new(); //Stores GroupPaths that aren't legal, like "# I am illegal"
			string[] lines = pathsTextBox.Text.Split("\r\n");

			foreach (string line in lines)
			{
				if (line == String.Empty || line.Length < 3) continue;
				string trimmed = line.Trim();
				char group = trimmed[0];
				string path = trimmed[2..];

				if (Char.GetNumericValue(group) >= 0 && Char.GetNumericValue(group) <= 9 && Char.IsNumber(trimmed[0]))
					groupPaths.Add(new(group, path));
				else
					otherGroupPaths.Add(new(group, path));
			}

			//I love how simple this is. Found on StackOverflow somewhere.
			groupPaths = groupPaths.OrderBy(g => g.group).ThenBy(g => g.path).ToList();
			otherGroupPaths = otherGroupPaths.OrderBy(g => g.group).ThenBy(g => g.path).ToList();
			pathsTextBox.Text = String.Empty;

			foreach (GroupPath groupPath in groupPaths)
				pathsTextBox.Text += groupPath.group + " " + groupPath.path + Environment.NewLine;

			foreach (GroupPath groupPath in otherGroupPaths)
				pathsTextBox.Text += groupPath.group + " " + groupPath.path + Environment.NewLine;
		}

		/// <summary>
		/// Event for when the user deselects the Paths To Backup TextBox, will call UpdateBackupSize()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PathsTextBox_Leave(object sender, EventArgs e)
		{
			UpdateBackupSize();
		}

		/// <summary>
		/// On startup, assign GUI controls values from files, and disable any controls, if necessary.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form_Shown(object sender, EventArgs e)
		{
			if (!File.Exists(PathsFilePath))
				File.Create(PathsFilePath);
			else
				pathsTextBox.Text = File.ReadAllText(PathsFilePath);

			if (!File.Exists(ConfigFilePath))
			{
				FileStream file = File.Create(ConfigFilePath);
				file.Close();
			}

			//TODO: Potentially convert the config file to XML for better usability/readability
			//Read in config stuff. Is this extremely stupid and sub-optimal? Yes. Does it work? Also yes.
			string configFileTxt = File.ReadAllText(ConfigFilePath);

			//If config file has no text, write default values to file.
			if (configFileTxt == String.Empty)
			{
				const string defaultConfigValues = "false\r\nfalse\r\nfalse\r\nfalse\r\nfalse\r\nfalse\r\nfalse\r\nfalse\r\nfalse\r\nfalse\r\nUse these for...\r\n...labeling groups\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\ntrue\r\n\r\nfalse\r\n\r\ntrue\r\nfalse\r\n\r\nfalse";
				File.WriteAllText(ConfigFilePath, defaultConfigValues);
				configFileTxt = defaultConfigValues; //Process like normal, even though technically it didn't read anything in from the file.
			}

			string[] config = configFileTxt.Split("\r\n");
			checkBox0.Checked = Boolean.Parse(config[0]);
			checkBox1.Checked = Boolean.Parse(config[1]);
			checkBox2.Checked = Boolean.Parse(config[2]);
			checkBox3.Checked = Boolean.Parse(config[3]);
			checkBox4.Checked = Boolean.Parse(config[4]);
			checkBox5.Checked = Boolean.Parse(config[5]);
			checkBox6.Checked = Boolean.Parse(config[6]);
			checkBox7.Checked = Boolean.Parse(config[7]);
			checkBox8.Checked = Boolean.Parse(config[8]);
			checkBox9.Checked = Boolean.Parse(config[9]);
			textBox0.Text = config[10];
			textBox1.Text = config[11];
			textBox2.Text = config[12];
			textBox3.Text = config[13];
			textBox4.Text = config[14];
			textBox5.Text = config[15];
			textBox6.Text = config[16];
			textBox7.Text = config[17];
			textBox8.Text = config[18];
			textBox9.Text = config[19];
			path1Btn.Checked = Boolean.Parse(config[20]);
			path1TextBox.Text = config[21];
			path2Btn.Checked = Boolean.Parse(config[22]);
			path2TextBox.Text = config[23];
			openOnComplete.Checked = Boolean.Parse(config[24]);
			urlCheckBox.Checked = Boolean.Parse(config[25]);
			urlTextBox.Text = config[26];
			zipCheckBox.Checked = Boolean.Parse(config[27]);

			UpdateBackupSize();
		}

		/// <summary>
		/// Calls SaveToFiles() before quitting application
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveToFiles();
		}

		/// <summary>
		/// Clear the path1TextBox text, and UpdateControls()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClearPath1_Click(object sender, EventArgs e)
		{
			path1TextBox.Text = String.Empty;
			UpdateControls();
		}

		/// <summary>
		/// Clear the path2TextBox text, and UpdateControls()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClearPath2_Click(object sender, EventArgs e)
		{
			path2TextBox.Text = String.Empty;
			UpdateControls();
		}

		/// <summary>
		/// Calls UpdateControls() when the user changes something in the big PathsTextBox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PathsTextBox_TextChanged(object sender, EventArgs e)
		{
			UpdateControls();
		}

		/// <summary>
		/// Ask the user if they want to delete the contents of the backup folder, then proceeds to create the backup if ClearFolder() returns true
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BackupBtn_Click(object sender, EventArgs e)
		{
			File.WriteAllText(PathsFilePath, pathsTextBox.Text);
			string BackupDest;

			if (path1Btn.Checked && path1TextBox.Text != String.Empty)
			{
				BackupDest = path1TextBox.Text;
			}

			else if (path2Btn.Checked && path2TextBox.Text != String.Empty)
			{
				BackupDest = path2TextBox.Text;
			}

			else throw new Exception("Unable to get backup destination path");

			//TODO: Make sure the destination path textbox that is selected is a valid location before enabling the Backup button
			if (!ClearFolder(BackupDest)) //If user selected cancel, or there was an issue processing the user's choice, cancel the backup operation
				return;

			backupBtn.Enabled = false;
			progressBar.Value = 0;
			timestamp = DateTime.Now.ToString("M-d-yyyy hh;mm;ss tt"); //'/' and ':' won't work in paths because Windows.
			GBP_Log.Append("GBP Backup " + timestamp + "\n");
			GBP_Log.AppendDivider();
			stripLabel.Text = "Backing up...";

			List<Thread> threads = new();

			//for each line in the big Paths textbox, get rid of the prepending group number on the line, and call CopyBackupPath
			foreach (string srcPath in pathsTextBox.Text.Split("\r\n").ToList())
			{
				if (srcPath == String.Empty) continue;

				string trimmedSourcePath = srcPath.Trim();
				char group = trimmedSourcePath[0];
				trimmedSourcePath = trimmedSourcePath[2..];

				if (ValidGroupChar(group))
				{
					if (GroupChecked(group))
					{
						Thread t = new(() => CopyBackupPath(trimmedSourcePath));
						t.Start();
						threads.Add(t);
					}
				}

				else if (Char.ToLower(srcPath[0]) != '#') //# are used for comments
					GBP_Log.Append($"\r\nGBP cannot understand this line: \"{srcPath}\"\r\n");
			}

			//Create destination folder if it doesn't exist
			if (!Directory.Exists(Path.Combine(BackupDest, "GBP Backup " + timestamp)))
				Directory.CreateDirectory(Path.Combine(BackupDest, "GBP Backup " + timestamp));

			Thread pThread = new(() => { progressBar.BeginInvoke(new Action(() => { RunProgressBar(BackupDest); })); });
			pThread.Start();

			//This kind stranger's answer from long ago is how I finally got this stupid damn progress bar working after so, so very many hours... https://stackoverflow.com/a/1239662
			while (pThread.IsAlive)
				Application.DoEvents();

			pThread.Join();

			foreach (Thread thread in threads) //Wait for all threads to finish.
				thread.Join();

			if (urlCheckBox.Checked && urlTextBox.Text != "")
				OpenUrl(urlTextBox.Text);

			if (zipCheckBox.Checked)
			{
				if (path1Btn.Checked && path1TextBox.Text != "")
					CompressBackup(path1TextBox.Text, 1);

				else if (path2Btn.Checked && path2TextBox.Text != "")
					CompressBackup(path2TextBox.Text, 2);
			}

			ShowBackupPath();
			backupBtn.Enabled = true;
			stripLabel.Text = "Backup completed. Ready to exit or begin next backup.";
			GBP_Log.AppendDivider();

			Process.Start("notepad.exe", GBP_Log.FilePath); //Open log in Notepad.
		}

		/// <summary>
		/// Calls UpdateControls() when one of the two PathTextBoxes are changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PathTextBox_TextChanged(object sender, EventArgs e)
		{
			UpdateControls();
		}

		#endregion

		#region Private Structs

		/// <summary>
		/// Represents a single path in the paths TextBox plus its group number.
		/// </summary>
		private readonly struct GroupPath
		{
			public readonly char group;
			public readonly string path;

			public GroupPath(char g, string p)
			{
				group = g;
				path = p;
			}
		}

		#endregion
	}

	public static class MyExtensions
	{
		/// <summary>
		/// Extension to get FileInfo of all files/subfolders SAFELY since <![CDATA[IEnumerable<FileInfo>]]>.EnumerateFiles() completely collapses on first exception thrown with no way to handle the individual exception.<para/>
		/// <see href="https://stackoverflow.com/a/15179942">Error on getting DirectoryInfo with GetFileSystemInfos()</see>
		/// </summary>
		/// <param name="di">Makes this method an extension of <![CDATA[IEnumerable<FileInfo>]]> so we can call it just like a built-in method</param>
		/// <param name="searchPattern">The search pattern for which files need to be enumerated</param>
		/// <returns>Enumerable collection of FileInfo based on searchPattern, starting in the directory of this object</returns>
		public static IEnumerable<FileInfo> SafeGetFileInfosRecursive(this DirectoryInfo di, string searchPattern)
		{
			try
			{
				return di
					.EnumerateFiles(searchPattern)
					.Concat(
						di
							.EnumerateDirectories()
							.SelectMany(x => x.SafeGetFileInfosRecursive(searchPattern))
					);
			}

			catch (UnauthorizedAccessException)
			{
				return Enumerable.Empty<FileInfo>();
			}
		}

		/// <summary>
		/// This is a hot mess, but I'm currently working on how to filter out files/folders with the System attribute
		/// </summary>
		/// <param name="di">Makes this method an extension of <![CDATA[IEnumerable<FileInfo>]]> so we can call it just like a built-in method</param>
		/// <param name="searchPattern">The search pattern for which files need to be enumerated</param>
		/// <returns></returns>
		public static List<FileInfo> GetNonSystemFileInfosRecursive(this DirectoryInfo di, string searchPattern)
		{
			IEnumerable<FileInfo> di_enumerated_files = di.EnumerateFiles(searchPattern)
															.Where(file => !file.Attributes.HasFlag(FileAttributes.System));

			IEnumerable<DirectoryInfo> di_enum_dirs_where = di.EnumerateDirectories()
																.Where(dir => !dir.Attributes.HasFlag(FileAttributes.System));

			IEnumerable<FileInfo> di_enum_dirs_where_select = di_enum_dirs_where
																.SelectMany(recurse => recurse.GetNonSystemFileInfosRecursive(searchPattern));

			IEnumerable<FileInfo> combined = di_enumerated_files.Concat(di_enum_dirs_where_select);

			return combined.ToList();
		}
	}
}