// --------------------------------
//  File Collection Utility - Creates a master archive of photos
//  Copyright(C) 2025  Anthony W. van Leeuwen
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
// ---------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using FileCollectionUtility.Properties;

namespace FileCollectionUtility
{
    /// <summary>
    /// FileCollectionUtilityForm() is the main form of the application designed to 
    /// collect digital photo files or other types of files into a single target directory,
    /// organized into seperate folders based on file creation date.
    /// </summary> 
    public partial class FileCollectionUtilityForm : Form
    {
        // SourceDirectory is the directory containing the source files to be processed
        private string SourceDirectory;

        // SourceDescription is a user defined description of the source directory
        private string SourceDescription;

        // TargetRootDirectory is the root directory where the renamed files will be deposited
        private string TargetRootDirectory;

        // ShortTargetRootDirectory is the shortened version of the TargetRootDirectory for display
        private string ShortTargetRootDirectory;

        // LogFileName is the full path and name of the log file
        private string LogFileName;

        // MaxTextBoxChars is the maximum number of characters to display in the textboxes
        // initially set to zero and defined during form load event
        private int MaxTextBoxChars = 0;

        // ToolTip tt is used to display tooltips for various controls
        ToolTip tt = new ToolTip();

        // Define HashSets for different picture file types
        private HashSet<string> PictureSet = new HashSet<string> { ".jpg", ".png", ".gif", ".bmp", ".tiff", ".svg", ".webp", ".heic", ".raw",
        ".psd", ".ai", ".eps", ".indd", ".pdf", ".ico", ".jfif", ".jp2", ".jpx", ".j2k", ".svgz", ".tif", ".nef" };

        // Define HashSet for different video file types
        private HashSet<string> VideoSet = new HashSet<string> { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv", ".webm", ".mpeg", ".mpg", ".3gp",
            ".m4v", ".vob", ".ogv", ".rm", ".rmvb", ".ts", ".divx", ".xvid" };

        //a Define a merged HashSet that would contain picture or video or both file types
        private HashSet<string> MergeSet = new HashSet<string> { };

        /// <summary>
        ///   The structure "statistics" is used to contain several variables needed for 
        ///   updating the contents of textboxes and the progressbar from the background worker thread.
        /// </summary>
        public struct Statistics
        {
            /// <summary>The number of files in the source directory</summary>
            public int NumberOfFiles;
            /// <summary>The number of files copied</summary>
            public int FilesCopied;
            /// <summary>The number of files renamed</summary>
            public int FilesRenamed;
            /// <summary>The number of files skipped</summary>
            public int FilesSkipped;
            /// <summary>The progressbar progress amount</summary>
            public int ProgressAmount;
        }

        /// <summary>
        /// My cancel token is used to interrupt and stop the processing of files
        /// by the background worker thread when set to true.
        /// </summary>
        public bool MyCancelToken;

        /// <summary>
        /// The busy signal indicating files are being processed.
        /// </summary>
        public bool Busy;

        /// <summary>
        ///   The Copystatus enum identifies the different status signals for files copied, 
        ///   renamed, or skipped.
        /// </summary>
        enum CopyStatus
        {
            Unknown,
            Copied,
            Renamed,
            Skipped
        }

        /// <summary>
        /// Constructor for FileCollectionUtilityForm()
        /// </summary>
        public FileCollectionUtilityForm()
        {
            InitializeComponent();

            // set labels in the status bar
            VersionLbl.Text = "File Collection Utility - Version: " + GetAppVersion();
            SoftwareLicenseLbl.Text = "Licensed under Apache 2.0";

            // redraw groupboxes for more visible borders
            SourceDirectoryGrpBx.Paint += new PaintEventHandler(groupBox1_Paint);
            TargetDirectoryGrpBx.Paint += new PaintEventHandler(groupBox1_Paint);
            StatisticsGrpBx.Paint += new PaintEventHandler(groupBox1_Paint);
            ProcessSourceDirectoryGrpBx.Paint += new PaintEventHandler(groupBox1_Paint);
            PhotoVideoGrpBx.Paint += new PaintEventHandler(groupBox1_Paint);
        }

        /// <summary>
        /// FileCollectionUtilityForm_Load() processes the form load event
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void FileCollectionUtilityForm_Load(object sender, EventArgs e)
        {
            // Create a user settings file if it does not exist
            if (Settings.Default.IsFirstRun)
            {
                // save all connection variables to the settings file
                Properties.Settings.Default.SourceDescription = string.Empty;
                Properties.Settings.Default.SourceDirectory = string.Empty;
                Properties.Settings.Default.TargetRootDirectory = string.Empty;
                Properties.Settings.Default.LogFileName = string.Empty;
                Properties.Settings.Default.IsFirstRun = false;
                Properties.Settings.Default.Save();
            }

            // Determine the maximum number of characters that can be displayed in the textboxes
            MaxTextBoxChars = GetMaxTextBoxLength(TargetRootDirectoryTextBox);

            // Get Source, Target Root, and Log File Directories from settings file
            CheckDirectorySettings();
        }

        #region Settings

        /// <summary>
        /// SaveSettings() saves the SourceDirectory, TargetRootDirectory,  
        /// LogFileName and SourceDescription in the settings file. 
        /// </summary>
        private void SaveSettings()
        {
            // need to check that these values are defined
            if (string.IsNullOrEmpty(SourceDirectory) || string.IsNullOrEmpty(TargetRootDirectory)
                || string.IsNullOrEmpty(LogFileName))
            {
                // if any of these are undefined - abort saving the settings
                string Message = "Directory or Log File Not Set - Settings Not Saved";
                MessageBox.Show(Message, "File or Directory Not Set", MessageBoxButtons.OK);
                return;
            }

            Properties.Settings.Default.SourceDescription = SourceDescription.ToString();
            Properties.Settings.Default.SourceDirectory = SourceDirectory.ToString();
            Properties.Settings.Default.TargetRootDirectory = TargetRootDirectory.ToString();
            Properties.Settings.Default.LogFileName = LogFileName.ToString();
            Properties.Settings.Default.Save();

            Properties.Settings.Default.Reload();
        }

        /// <summary>
        /// CheckDirectorySettings() retrieves the Target or Root directory and Log Filename from settings
        /// to verify that the settings are defined.  If not, a messagebox pops up telling the user that 
        /// the settings need to be defined. Otherwise, the settings are loaded by executing the 
        /// LoadDirectorySettings() method.
        /// </summary>
        private void CheckDirectorySettings()
        {
            // Retrieve configuration settings from the settings file
            string strTargetRootDirectory = Properties.Settings.Default.TargetRootDirectory.ToString();
            string strLogFileName = Properties.Settings.Default.LogFileName.ToString();

            // Check to make sure none of the directory values are empty.
            if (string.IsNullOrEmpty(strTargetRootDirectory) || string.IsNullOrEmpty(strLogFileName))
            {
                // Show message to user to enter directory names
                string Message = "Please set the Target Root and Log File Name.";
                MessageBox.Show(Message, "FileCollectionUtility: ", MessageBoxButtons.OK);
            }
            else
            {
                LoadDirectorySettings();
            }
        }

        /// <summary>
        /// LoadDirectorySettings() loads the directory settings previously defined. Note that
        /// the directory and file names are shortened for display in the textboxes if required.
        /// </summary>
        private void LoadDirectorySettings()
        {
            // Retrieve configuration settings from the settings file
            SourceDescription = Properties.Settings.Default.SourceDescription.ToString();
            SourceDirectory = Properties.Settings.Default.SourceDirectory.ToString();
            TargetRootDirectory = Properties.Settings.Default.TargetRootDirectory.ToString();
            LogFileName = Properties.Settings.Default.LogFileName.ToString();

            if (SourceDescription.Length > 0)
            {
                SourceDescriptionTextBox.Text = SourceDescription.ToString();
            }

            if (SourceDirectory.Length > 0)
            {
                SourceDirectoryTextBox.Text = ShortenFilePath(SourceDirectory, MaxTextBoxChars);
            }

            if (TargetRootDirectory.Length > 0)
            {
                ShortTargetRootDirectory = ShortenFilePath(TargetRootDirectory, MaxTextBoxChars);
                TargetRootDirectoryTextBox.Text = ShortTargetRootDirectory.ToString();
            }

            if (LogFileName.Length > 0)
            {
                LogFileNameTextBox.Text = ShortenFilePath(LogFileName, MaxTextBoxChars);
            }
        }

        #endregion Settings

        #region Target Root Directory and Log File

        /// <summary>
        /// SelectTargetRootDirectoryBtn_click() processes the SelectTargetRootDirectory button click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SelectTargetRootDirectoryBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.MyComputer,

                Description = "Select Folder To Deposit Renamed Files:",
                ShowNewFolderButton = true
            };

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                TargetRootDirectory = fbd.SelectedPath.ToString();
            }

            // Shorten TargetRootDirectory for display in textbox
            ShortTargetRootDirectory = ShortenFilePath(TargetRootDirectory, MaxTextBoxChars);

            this.TargetRootDirectoryTextBox.Text = ShortTargetRootDirectory.ToString();
        }


        /// <summary>
        /// GetLogFileNameBtn_Click() processes the GetLogFileName button click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GetLogFileNameBtn_Click(object sender, EventArgs e)
        {
            // Define the Open File Dialog filter for file extensions
            string myFilter = "xml files (*.xml)|*.xml|txt files (*.txt)|*.txt|All Files (*.*)|*.*";

            // Create an OpenFileDialog
            OpenFileDialog ofd = new OpenFileDialog
            {
                FilterIndex = 1,
                Filter = myFilter,
                CheckFileExists = false
            };

            // Open the file dialog and handle the Open and Cancel button events
            DialogResult result = ofd.ShowDialog();

            if (result == DialogResult.OK)
            {
                // get the full file rootpath names
                LogFileName = ofd.FileName.ToString();
            }
            else if (result == DialogResult.Cancel)
            {
                LogFileName = null;
                LogFileNameTextBox.Text = null;
                return;
            }

            // if file LogFileName does not exist then create it
            if (!File.Exists(LogFileName))
            {
                CreateLogFile(LogFileName);
            }

            //LogFileNameTextBox.Text = LogFileName.ToString();
            LogFileNameTextBox.Text = ShortenFilePath(LogFileName, MaxTextBoxChars);
        }


        #endregion Target Root Directory and Log File

        #region Source Directory and Description 

        /// <summary>
        /// SelectSourceDirectoryBtn_Click() processes the SelectSource Directory button click event
        /// to select the Directory containing the source files or photos.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SelectSourceDirectoryBtn_Click(object sender, EventArgs e)
        {
            // Clear form text boxes each time a new source directory is chosen
            ClearFormTextboxes();

            // Set the SourceDirectory variable to empty
            SourceDirectory = string.Empty;

            // Open the FolderBrowserDialog to select the source directory
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                // Set the root folder to view all disk devices attached to computer
                RootFolder = Environment.SpecialFolder.MyComputer,
                Description = "Select Folder with Source Files:",

                // Prevent user from creating a new empty folder
                ShowNewFolderButton = false
            };

            // Show the folder browser dialog and process result
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK)
            {
                SourceDirectory = fbd.SelectedPath.ToString();
            }
            else if (result == DialogResult.Cancel)
            {
                // handle cancel action
                SourceDescription = null;
                SourceDescriptionTextBox.Text = string.Empty;
                SourceDirectory = null;
                SourceDirectoryTextBox.Text = string.Empty;
                return;
            }

            // copy Source Directory to appropriate textbox
            SourceDirectoryTextBox.Text = ShortenFilePath(SourceDirectory, MaxTextBoxChars);

            // Check if source directory is empty
            if (IsDirectoryEmpty(SourceDirectory))
            {
                string Message = "The selected source directory is empty.  Please select a different source directory.";
                MessageBox.Show(Message, "FileCollectionUtility: Empty Directory Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ClearFormTextboxes() clears all the textboxes on the form that indicate the number of 
        /// files processed and the progressbar.
        /// </summary>
        private void ClearFormTextboxes()
        {
            FileCountTextBox.Clear();
            FilesCopiedTextBox.Clear();
            FilesRenamedTextBox.Clear();
            FilesSkippedTextBox.Clear();

            BackGroundWorkerProgress.Value = 0;
        }

        /// <summary>
        /// ProcessSourceDirectoryBtn_Click event starts processing of the files in the source directory
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void ProcessSourceDirectoryBtn_Click(object sender, EventArgs e)
        {
            // set cancel token to false to prevent the background worker from stopping until
            // the user clicks on the Cancel Process button - set busy to true
            MyCancelToken = false;
            Busy = true;

            // Check that all directories and log files are defined
            if (string.IsNullOrEmpty(SourceDirectory) || string.IsNullOrEmpty(TargetRootDirectory)
                || string.IsNullOrEmpty(LogFileName))
            {
                // if any of these are undefined - abort processing the source directory
                string Message = "Directory or Log File Not Set";
                MessageBox.Show(Message, "File or Directory Not Set", MessageBoxButtons.OK);
                return;
            }

            // SourceDescription must not be null, set a default value 
            SourceDescription = SourceDescriptionTextBox.Text.ToString();
            if (string.IsNullOrEmpty(SourceDescription))
            {
                string dirname = SourceDirectory.Split(Path.DirectorySeparatorChar).Last();
                if (dirname != null)
                {
                    SourceDescription = dirname;
                }
                else
                {
                    SourceDescription = "Undefined";
                }
            }

            // Check freespace on drive where the target root directory is located
            var drive = new DriveInfo(Path.GetPathRoot(TargetRootDirectory));
            long driveFreeSpace = drive.AvailableFreeSpace;

            // Create a statistics object to hold the progress information
            Statistics statistics = new Statistics();

            // Create a progress object to report progress to the UI thread
            IProgress<Statistics> progress = new Progress<Statistics>(Statistics =>
            {
                BackGroundWorkerProgress.Value = statistics.ProgressAmount;
                FileCountTextBox.Text = statistics.NumberOfFiles.ToString();
                FilesCopiedTextBox.Text = statistics.FilesCopied.ToString();
                FilesSkippedTextBox.Text = statistics.FilesSkipped.ToString();
                FilesRenamedTextBox.Text = statistics.FilesRenamed.ToString();
            });

            // Check if Pictures or Video checkboxes are checked and set MergeSet accordingly
            MergeSet.Clear();
            if (PicturesChkBx.Checked)
            {
                MergeSet.UnionWith(PictureSet);
            }
            if (VideoChkBx.Checked)
            {
                MergeSet.UnionWith(VideoSet);
            }

            // Start processing the source directory asynchronously
            int result = await Task.Run(() =>
            {
                // define variables
                int FilesCopied = 0;
                int FilesRenamed = 0;
                int FilesSkipped = 0;

                // get the file count in the source directory based on the selected file types
                int fileCnt = GetFileCountInSourceDirectory(SourceDirectory, MergeSet);

                // Get DirectoryInfo object for the SourceDirectory Path
                DirectoryInfo dir = new DirectoryInfo(SourceDirectory);

                // Get the FileInfo object for the files in the source directory and all subdirectories
                FileInfo[] fileInfo = dir.GetFiles("*", SearchOption.AllDirectories);

                // Calculate the total size in bytes for the source files and add 30 Mb
                long totalByteSize = fileInfo.Sum(f => f.Length) + 30000000;

                // Set the MyCancelToken to true if there is not enough free drive space left for the directory where
                // the target root folder is located
                if (totalByteSize > driveFreeSpace)
                {
                    // Not enough free space on target drive
                    string Message = "Insufficient Free Drive Space For Target Root Folder!";
                    string Caption = "File Collection Utility Message";
                    DialogResult dlgresult = MessageBox.Show(Message, Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    if (dlgresult == DialogResult.OK)
                    {
                        // setting the cancel token to true will abort the process if insufficient drive
                        // space remains
                        MyCancelToken = true;
                    }
                }

                statistics.NumberOfFiles = fileCnt;
                int completed = 0;

                // Process each file in the Source Directory
                foreach (FileInfo fi in fileInfo)
                {
                    // If MyCancelToken is true, break to stop further processing.
                    if (MyCancelToken)
                    {
                        break;
                    }

                    // Get the info from the FileInfo item
                    string sfilename = fi.Name;
                    string sfullfilename = fi.FullName;
                    long sfilesize = fi.Length;
                    DateTime sCreateTime = fi.CreationTime;
                    DateTime sLastWriteTime = fi.LastWriteTime;

                    // *** get file extension ***
                    string sfilextension = fi.Extension.ToLower();
                    if (MergeSet.Count > 1 && !MergeSet.Contains(sfilextension))
                    {
                        // skip this file and continue to next file
                        continue;
                    }

                    string targetFileFullName;
                    int FileStatus;

                    // increment file count for use in progress bar
                    completed++;

                    // Get true file creation time
                    DateTime realCreateTime = GetSmallestDateTime(sCreateTime, sLastWriteTime);

                    // Get Target subdirectory folder name using the realCreateTime
                    string targetSubdirectoryName = GetTargetFolderName(TargetRootDirectory, realCreateTime);

                    // Check if sfilename already exists in the targetSubdirectoryName folder
                    if (File.Exists(Path.Combine(targetSubdirectoryName, sfilename)))
                    {
                        // Get fileinfo for existing file located in the target subdirectory of the same name
                        FileInfo fi2 = GetFileInfoForTargetFile(targetSubdirectoryName, sfilename);

                        targetFileFullName = fi2.FullName;

                        // File Exists so check to see if they are really the same
                        if (AreFilesTheSame(fi, fi2))
                        {
                            // files the same and do nothing
                            FilesSkipped++;
                            FileStatus = (int)CopyStatus.Skipped;
                        }
                        else
                        {
                            // files are different so
                            // rename file and append '(n)' and copy to destination folder
                            targetFileFullName = GetNextFileName(targetFileFullName);

                            File.Copy(sfullfilename, targetFileFullName);

                            FilesRenamed++;
                            FileStatus = (int)CopyStatus.Renamed;
                        }
                    }
                    else
                    {
                        // file does not exist in the destination directory and 
                        // copy file to destination directory
                        targetFileFullName = Path.Combine(targetSubdirectoryName, sfilename);

                        File.Copy(sfullfilename, targetFileFullName);

                        FileStatus = (int)CopyStatus.Copied;
                        FilesCopied++;
                    }

                    // Write entry for file processed to the log file
                    WriteLogEntry(SourceDescription, sfullfilename, sfilesize, realCreateTime,
                         targetFileFullName, GetCopyStatus((CopyStatus)FileStatus), LogFileName);

                    // calculate progress value to report to the progressbar
                    double progressPercent = (double)completed / fileCnt * 100;
                    int progressValue = Convert.ToInt32((double)progressPercent);

                    // gather statistics and report progress to capture of last file
                    statistics.FilesCopied = FilesCopied;
                    statistics.FilesRenamed = FilesRenamed;
                    statistics.FilesSkipped = FilesSkipped;
                    statistics.ProgressAmount = progressValue;
                    progress.Report(statistics);

                } // end of foreach
                return 1;
            });
            Busy = false;
        }

        #endregion Source Directory and Description

        #region Form Controls

        /// <summary>
        /// btnCloseCmd_Click() processes the Close button click event
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void CloseCmdBtn_Click(object sender, EventArgs e)
        {
            // Check to see if background worker is busy
            if (Busy)
            {
                string Message = "File processing is still in progress. Please cancel processing before closing the application.";
                MessageBox.Show(Message, "File Collection Utility: Processing In Progress", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Save Settings before closing form
            SaveSettings();

            // Close form
            this.Close();
        }


        /// <summary>
        /// Handles the Click event of the CancelProcessingBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CancelProcessingBtn_Click(object sender, EventArgs e)
        {
            MyCancelToken = true;
        }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        /// <returns>returns the versionInfo</returns>
        private string GetAppVersion()
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
            return versionInfo.ProductVersion;
        }

        #endregion

        #region File Name Routines

        /// <summary>
        /// GetTargetSubdirectoryName() gets the target subdirectory name from rootpath and datetime
        /// </summary>
        /// <param name="rootpath">Path to the root folder</param>
        /// <param name="dateTime">Datetime to determine subfolder name</param>
        /// <returns>TargetSubdirectoryName</returns>
        private string GetTargetSubdirectoryName(string rootpath, DateTime dateTime)
        {
            // Convert dateTime to a string to create target directory name
            // argument rootpath is the rootpath
            string SubdirectoryName = dateTime.ToString("yyyy-MMM-dd");
            string TargetSubdirectoryName = Path.Combine(rootpath, SubdirectoryName);
            return TargetSubdirectoryName;
        }

        private string GetTargetFolderName(string rootpath, DateTime dateTime)
        {
            // Convert datetime to a folder in the form of yyyy/yyyy-MMM-dd
            // check to see if Year subfolder exists
            string RootYearFolderName = dateTime.ToString("yyyy");
            string TargetRootYearFolderName = Path.Combine(rootpath, RootYearFolderName);

            // check to see if TargetRootYearFolderName exists
            if (!Directory.Exists(TargetRootYearFolderName))
            {
                Directory.CreateDirectory(TargetRootYearFolderName);
            }

            // check to see if the subfolder exists
            string SubfolderName = dateTime.ToString("yyy-MMM-dd");
            string TargetSubfolderName = Path.Combine(TargetRootYearFolderName, SubfolderName);
            if (!Directory.Exists(TargetSubfolderName))
            {
                Directory.CreateDirectory(TargetSubfolderName);
            }
            return TargetSubfolderName;
        }

        /// <summary>
        /// GetSmallestDateTime() gets the smallest of the createDate or lastModified date
        /// </summary>
        /// <param name="createDate">Create Date of the file</param>
        /// <param name="lastModified">Last Written to date of the file</param>
        /// <returns>Smallest DateTime</returns>
        private DateTime GetSmallestDateTime(DateTime createDate, DateTime lastModified)
        {
            if (lastModified <= createDate)
            {
                return lastModified;
            }
            else
            {
                return createDate;
            }
        }

        /// <summary>
        /// CreateTargetSubdirectoryIfNotFound() creates the target subdirectory if it does not exist.
        /// </summary>
        /// <param name="targetSubdirectoryName"></param>
        /// <returns>Returns true if the subdirectory is created</returns>
        public bool CreateTargetSubdirectoryIfNotFound(string targetSubdirectoryName)
        {
            if (!Directory.Exists(targetSubdirectoryName))
            {
                Directory.CreateDirectory(targetSubdirectoryName);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// CheckIfFileExistsInTargetSubdirectory() checks if file exists in Target Subdirectory
        /// </summary>
        /// <param name="targetSubdirectoryName">Name of target subdirectory</param>
        /// <param name="fileName">Name of file including extension</param>
        /// <returns>Returns true if file exists in target directory</returns>
        public bool CheckIfFileExistsInTargetSubdirectory(string targetSubdirectoryName, string fileName)
        {
            string TargetFileFullPath = Path.Combine(targetSubdirectoryName, fileName);
            if (File.Exists(TargetFileFullPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// GetFileInfoForTargetFile gets the FileInfo object for the target file
        /// </summary>
        /// <param name="targetSubdirectoryName">Name of target subdirectory</param>
        /// <param name="fileName">Name of the file including extension</param>
        /// <returns>Returns the FileInfo for the specified file</returns>
        public FileInfo GetFileInfoForTargetFile(string targetSubdirectoryName, string fileName)
        {
            FileInfo fi = new FileInfo(Path.Combine(targetSubdirectoryName, fileName));
            return fi;
        }

        /// <summary>
        /// AreFilesTheSame returns true if the fileinfo.Name, fileinfo.LastWriteTime, and fileinfo.length 
		/// and if the MD5 hash of each file is identical.
        /// Then the files are the same.  Returns false otherwise.
        /// </summary>
        /// <param name="fileInfo1">first file</param>
        /// <param name="fileInfo2">second file</param>
        /// <returns>returns true if files are equal</returns>
        public bool AreFilesTheSame(FileInfo fileInfo1, FileInfo fileInfo2)
        {
            // check if filenames are the same
            if (fileInfo1.Name == fileInfo2.Name)
            {
                // check if lastWriteTime is the same
                if (fileInfo1.LastWriteTime == fileInfo2.LastWriteTime)
                {
                    // check if files are the same length
                    if (fileInfo1.Length == fileInfo2.Length)
                    {
                        // Add code for md5 hash return true if equal
                        if (FilesAreEqual_Hash(fileInfo1, fileInfo2))
                        { return true; }
                        else
                        { return false; }
                    }
                    else { return false; }
                }
                else { return false; }
            }
            else { return false; }
        }

        /// <summary>
        /// getNextFile appends a numeric value in parenthesis to a file name if it already exists
        /// </summary>
        /// <param name="fileName">Name of file to which (n) is appended</param>
        /// <returns>
        /// Returns the modified filename:  path.filename(1).extension
        /// </returns>
        /// <remarks>
        /// Code was copied from StackOverflow article entitled "Is there an easy way to add (#) to
        /// a filename when you find a duplicate filename".  The solution was contributed by maxwellb
        /// who provided his solution on July 23, 2009.
        /// The code will work when the file does not have an extension or has the format of
        /// test.txt.txt.
        /// </remarks>
        private static string GetNextFileName(string fileName)
        {
            int i = 0;

            FileInfo fi = new FileInfo(fileName);

            string basename = fi.FullName.Substring(0, fi.FullName.Length - fi.Extension.Length);
            string extension = fi.Extension;

            while (fi.Exists)
            {
                fi = new FileInfo(string.Format("{0}({1}){2}", basename, ++i, extension));
            }

            return fi.FullName;
        }

        /// <summary>
        /// IsDirectoryEmpty returns true if the directory is empty
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        ///   <c>true</c> if [is directory empty] [the specified path]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        /// <summary>
        /// Shortens the file path string.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns>Shortened filepath string</returns>
        /// Source - https://stackoverflow.com/a/19546070
        /// Posted by Chris on October 23, 2013
        /// Retrieved 2025-12-06, License - CC BY-SA 3.0 
        /// Adapted here for use in FileCollectionUtility
        public static string ShortenFilePath(string filePath, int maxLength)
        {
            char delimiter = '\\';

            // if the filePath length is less than or equal to maxLength, return the original filePath
            if (filePath.Length <= maxLength)
            {
                return filePath;
            }

            // adjust maxLength to account for the ellipsis and spacing
            maxLength -= 3;

            string final = filePath;
            List<string> parts;

            int loops = 0;
            while (loops++ < 100)
            {
                parts = filePath.Split(delimiter).ToList();
                parts.RemoveRange(parts.Count - 1 - loops, loops);
                if (parts.Count == 1)
                {
                    return parts.Last();
                }

                parts.Insert(parts.Count - 1, "...");
                final = string.Join(delimiter.ToString(), parts);
                if (final.Length < maxLength)
                {
                    return final;
                }
            }

            return filePath.Split(delimiter).ToList().Last();
        }

        /// <summary>
        /// Gets the file count in source directory.
        /// </summary>
        /// <param name="sourceDirectory">The source directory.</param>
        /// <param name="extensions">The extensions.</param>
        /// <returns>returns the count of files found with matching file extensions</returns>
        private int GetFileCountInSourceDirectory(string sourceDirectory, HashSet<string> extensions)
        {
            int count = 0;
            DirectoryInfo dir = new DirectoryInfo(sourceDirectory);

            // if no extensions specified return total file count
            if (extensions.Count == 0)
            {
                count = dir.GetFiles("*", SearchOption.AllDirectories).Length;
                return count;
            }
            else
            {
                FileInfo[] fileInfo = dir.GetFiles("*", SearchOption.AllDirectories);

                // count files with specified extensions
                foreach (FileInfo fi in fileInfo)
                {
                    if (extensions.Contains(fi.Extension.ToLower()))
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        #endregion File Name Routines

        #region MD5 Routines

        /// <summary>
        /// FilesAreEqual_Hash() compares the MD5 hash or signature of both files referenced by the associated
        /// FileInfo objects.
        /// </summary>
        /// <param name="first">FileInfo object for first file</param>
        /// <param name="second">FileInfo object for second file</param>
        /// <returns>returns true if files have the same MD5 hash</returns>
        private static bool FilesAreEqual_Hash(FileInfo first, FileInfo second)
        {
            byte[] firstHash = MD5.Create().ComputeHash(first.OpenRead());
            byte[] secondHash = MD5.Create().ComputeHash(second.OpenRead());
            for (int i = 0; i < firstHash.Length; i++)
            {
                // compare the Hash character by character
                if (firstHash[i] != secondHash[i])
                    return false;
            }
            return true;
        }

        #endregion MD5 Routines

        #region XML Routines        
        /// <summary>
        /// Creates the log file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private void CreateLogFile(string fileName)
        {
            XmlTextWriter writer = new XmlTextWriter(fileName, null);
            //writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("files");
            writer.Close();
        }

        /// <summary>
        /// Writes the log entry.
        /// </summary>
        /// <param name="SourceDescription">The source description.</param>
        /// <param name="FullFileName">Full name of the file.</param>
        /// <param name="FileSize">Size of the file.</param>
        /// <param name="Created">The created.</param>
        /// <param name="TargetFileFullName">Full name of the target file.</param>
        /// <param name="Status">The status.</param>
        /// <param name="LogFileName">Name of the log file.</param>
        private void WriteLogEntry(string SourceDescription, string FullFileName, long FileSize, DateTime Created,
            string TargetFileFullName, string Status, string LogFileName)
        {
            // Check if LogFileName exists
            if (!File.Exists(LogFileName))
            {
                CreateLogFile(LogFileName);
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(LogFileName);
            //XmlNode root = xmlDoc;

            // Create the File element
            XmlElement MyFile = xmlDoc.CreateElement("file");

            // Create Source Description element
            XmlElement Source = xmlDoc.CreateElement("source");
            XmlText xmlSource = xmlDoc.CreateTextNode(SourceDescription);
            Source.AppendChild(xmlSource);
            MyFile.AppendChild(Source);

            // Create Full File Name element
            XmlElement FileName = xmlDoc.CreateElement("sourcefilename");
            XmlText xmlFileName = xmlDoc.CreateTextNode(FullFileName);
            FileName.AppendChild(xmlFileName);
            MyFile.AppendChild(FileName);

            // Create Filesize element
            XmlElement Filesize = xmlDoc.CreateElement("filesize");
            XmlText xmlFilesize = xmlDoc.CreateTextNode(FileSize.ToString());
            Filesize.AppendChild(xmlFilesize);
            MyFile.AppendChild(Filesize);

            // Create Create time element
            XmlElement CreateTime = xmlDoc.CreateElement("created");
            XmlText xmlCreateTime = xmlDoc.CreateTextNode(Created.ToString("yyyy-MMM-dd"));
            CreateTime.AppendChild(xmlCreateTime);
            MyFile.AppendChild(CreateTime);

            // Create Status element
            XmlElement fileStatus = xmlDoc.CreateElement(@"status");
            XmlText xmlFileStatus = xmlDoc.CreateTextNode(Status.ToString());
            fileStatus.AppendChild(xmlFileStatus);
            MyFile.AppendChild(fileStatus);

            // Create Target File Full Name element
            XmlElement TargetFileName = xmlDoc.CreateElement("targetfilename");
            XmlText xmlTargetFileName = xmlDoc.CreateTextNode(TargetFileFullName);
            TargetFileName.AppendChild(xmlTargetFileName);
            MyFile.AppendChild(TargetFileName);

            // Append subroot element MyFile to root and save
            xmlDoc.DocumentElement.AppendChild(MyFile);
            xmlDoc.Save(LogFileName);
        }

        #endregion Log File Code

        /// <summary>
        /// Gets the copy status.
        /// </summary>
        /// <param name="copyStatus">The copy status.</param>
        /// <returns>Returns constant value of enum type CopyStatus</returns>
        private string GetCopyStatus(CopyStatus copyStatus)
        {
            switch (copyStatus)
            {
                case CopyStatus.Unknown:
                    return "unknown";
                case CopyStatus.Copied:
                    return "copied";
                case CopyStatus.Renamed:
                    return "renamed";
                case CopyStatus.Skipped:
                    return "skipped";
                default:
                    return "unknown";
            }
        }

        private void TargetRootDirectoryTextBox_MouseHover(object sender, EventArgs e)
        {
            tt.SetToolTip(TargetRootDirectoryTextBox, TargetRootDirectory);
        }

        private void TargetRootDirectoryTextBox_MouseLeave(object sender, EventArgs e)
        {
            tt.RemoveAll();
        }

        private void LogFileNameTextBox_MouseHover(object sender, EventArgs e)
        {
            tt.SetToolTip(LogFileNameTextBox, LogFileName);
        }

        private void LogFileNameTextBox_MouseLeave(object sender, EventArgs e)
        {
            tt.RemoveAll();
        }

        private void SourceDirectoryTextBox_MouseHover(object sender, EventArgs e)
        {
            tt.SetToolTip(SourceDirectoryTextBox, SourceDirectory);
        }

        private void SourceDirectoryTextBox_MouseLeave(object sender, EventArgs e)
        {
            tt.RemoveAll();
        }

        /// <summary>
        /// Gets the maximum length of the text box.
        /// </summary>
        /// <param name="tb">The textbox</param>
        /// <returns>An integer defining the maximum characters in textbox</returns>
        private int GetMaxTextBoxLength(TextBox tb)
        {
            using (Graphics g = tb.CreateGraphics())
            {
                string testString = "C:\\Users\\Anthony\\source\\repos\\TestWindowsSetupProject\\Release\\TestWindowsSetupProject.msi";
                System.Drawing.SizeF totalSize = g.MeasureString(testString, tb.Font);
                float avgCharWidth = totalSize.Width / testString.Length;
                int charsFit = (int)(tb.Width / avgCharWidth);
                return charsFit + 5;
            }
        }

        // Source - https://stackoverflow.com/a/20042058
        // Posted by user1944617
        // Retrieved 2025-12-20, License - CC BY-SA 3.0

        internal void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            GroupBox box = sender as GroupBox;
            DrawGroupBox(box, e.Graphics, Color.Black, Color.OrangeRed);
        }


        private void DrawGroupBox(GroupBox box, Graphics g, Color textColor, Color borderColor)
        {
            if (box != null)
            {
                Brush textBrush = new SolidBrush(textColor);
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush);
                SizeF strSize = g.MeasureString(box.Text, box.Font);
                Rectangle rect = new Rectangle(box.ClientRectangle.X,
                                               box.ClientRectangle.Y + (int)(strSize.Height / 2),
                                               box.ClientRectangle.Width - 1,
                                               box.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);

                // Clear text and border
                g.Clear(this.BackColor);

                // Draw text
                g.DrawString(box.Text, box.Font, textBrush, box.Padding.Left, 0);

                // Drawing Border
                //Left
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                //Right
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Bottom
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Top1
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
                //Top2
                g.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }



    }
}
