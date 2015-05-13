using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using FolderSelect;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;

namespace KSPModAdmin.Plugin.BackupTab
{
    /// <summary>
    /// Controller class for the Translation view.
    /// </summary>
    public class UcBackupViewController
    {

        // - Disable 64-Bit vor windows.
        // - Set default button on remove mods to No ...
        // - LINUX - Steam installs KSP to a hidden folder...
        //   Workaround: Create a link to the install directory and use that instead



        private const string BACKUP_CONFIG_FILENAME = "KSPMABackup.cfg";

        private static BackupTreeModel model = new BackupTreeModel();
        private static Dictionary<string, string> backupNotes = new Dictionary<string, string>(); 

        /// <summary>
        /// Gets or sets the view of the controller.
        /// </summary>
        public static UcBackupView View { get; protected set; }


        public static BackupTreeModel Model
        {
            get { return model as BackupTreeModel; }
            set { model = value; }
        }

        public static string BackupPath
        {
            get { return View.BackupPath; }
            set { View.BackupPath = value; }
        }

        public static BackupDataNode SelectedBackup
        {
            get { return View.SelectedBackup; }
        }

        internal static void Initialize(UcBackupView view)
        {
            View = view;

            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;
            EventDistributor.LanguageChanged += LanguageChanged;
            EventDistributor.KSPRootChanged += KSPRootChanged;

            // Add your stuff to initialize here.
            View.Model = model;
        }

        #region EventDistributor callback functions.

        /// <summary>
        /// Callback function for the AsyncTaskStarted event.
        /// Should disable all controls of the BaseView.
        /// </summary>
        protected static void AsyncTaskStarted(object sender)
        {
            View.SetEnabledOfAllControls(true);
        }

        /// <summary>
        /// Callback function for the AsyncTaskDone event.
        /// Should enable all controls of the BaseView.
        /// </summary>
        protected static void AsyncTaskDone(object sender)
        {
            View.SetEnabledOfAllControls(false);
        }

        /// <summary>
        /// Callback function for the LanguageChanged event.
        /// This is the place where you can translate non accessible controls.
        /// </summary>
        private static void LanguageChanged(object sender)
        {
            View.LanguageChanged();
        }

        /// <summary>
        /// Callback function for the KSPRootChanged event.
        /// This is the place to handle a change of the selected KSP installation path..
        /// </summary>
        private static void KSPRootChanged(string kspPath)
        {

        }

        #endregion

        public static void SelectNewBackupPath()
        {
            var dlg = new FolderSelectDialog();
            dlg.Title = "Please select a new Backup path.";
            dlg.InitialDirectory = "";

            if (dlg.ShowDialog(View.ParentForm.Handle))
                View.BackupPath = dlg.FileName;

            // TODO: Save new backuppath.
        }

        public static void OpenBackupPath()
        {
            if (!string.IsNullOrEmpty(View.BackupPath))
                OptionsController.OpenFolder(View.BackupPath);
        }

        public static void NewBackup()
        {

        }

        public static void BackupSaves()
        {

        }

        public static void RemoveBackup()
        {

        }

        public static void RemoveAllBackups()
        {

        }

        public static void RecoverBackup()
        {

        }

        private const string ROOT = "BackupCFG";
        private const string PATH = "Path";
        private const string NAME = "Name";
        private const string NOTE = "Note";
        private const string BACKUPPATH = "BackupPath";
        private const string BACKUPFILES = "BackupFiles";
        private const string BACKUPFILE = "BackupFile";

        /// <summary>
        /// Loads the BackupNotes from the Backup.cfg file.
        /// </summary>
        /// <returns>A dictionary with the format Filename, Note.</returns>
        private static Dictionary<string, string> LoadBackupNotes()
        {
            var result = new Dictionary<string, string>();

            var fullpath = Path.Combine(BackupPath, BACKUP_CONFIG_FILENAME);
            if (!File.Exists(fullpath))
            {
                Messenger.AddInfo(string.Format("Backup config file not found. \"{0}\"", fullpath));
                return result;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(fullpath);

            BackupNode root = new BackupNode() { Key = ROOT };
            XmlNodeList nodeList = doc.GetElementsByTagName(BACKUPPATH);
            if (nodeList.Count >= 1 && nodeList[0].Attributes != null)
            {
                foreach (XmlAttribute att in nodeList[0].Attributes)
                {
                    if (att.Name == NAME)
                        BackupPath = att.Value;
                }
            }

            XmlNodeList mods = doc.GetElementsByTagName(Constants.MOD);
            foreach (XmlNode mod in mods)
            {
                string name = string.Empty;
                string note = string.Empty;
                foreach (XmlAttribute att in mod.Attributes)
                {
                    if (att.Name == NAME)
                        name = att.Value;
                    if (att.Name == NOTE)
                        note = att.Value;
                }

                if (string.IsNullOrEmpty(name))
                    continue;

                result.Add(name, note);
            }

            return result;
        }

        /// <summary>
        /// Reads the backup directory and fills the backup TreeView.
        /// </summary>
        private static void ScanBackupDirectory()
        {
            model.Nodes.Clear();

            try
            {
                if (Directory.Exists(BackupPath))
                {
                    foreach (string file in Directory.EnumerateFiles(BackupPath, "*" + Constants.EXT_ZIP))
                    {
                        string dispTxt = GetBackupDisplayName(file);
                        string note = GetNote(dispTxt);
                        model.Nodes.Add(new BackupNode(file, dispTxt, note));
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(BackupPath))
                        Messenger.AddInfo(string.Format(Messages.MSG_BACKUP_FOLDER_NOT_FOUND_0, BackupPath));
                }
            }
            catch (Exception ex)
            {
                Messenger.AddError(string.Format(Messages.MSG_BACKUP_LOAD_ERROR_0, ex.Message), ex);
            }
        }

        /// <summary>
        /// Creates the display name of a backup file.
        /// </summary>
        /// <param name="fullpath">Full path of the backup file.</param>
        /// <returns>The display name of a backup file.</returns>
        private static string GetBackupDisplayName(string fullpath)
        {
            string filename = Path.GetFileName(fullpath);
            string dispTxt = filename;
            if (filename != null && filename.StartsWith("AutoBackup_"))
            {
                FileInfo fi = GetFileInfoFromFilename(filename);
                dispTxt = fi.DateTime.ToString("MMM dd. yyyy HH:mm") + " - ";
                dispTxt += filename.Substring(0, filename.IndexOf("_"));
                string temp = filename.Substring(filename.IndexOf("_") + 1);
                dispTxt += " " + temp.Substring(0, temp.IndexOf("_"));
            }
            else if (filename != null && filename.Contains("_"))
            {
                string[] temp = filename.Split('_');
                if (temp.Length >= 3 && temp[1].Length == 8 && temp[2].Length >= 4)
                {
                    int y = 0;
                    int m = 0;
                    int d = 0;
                    int h = 0;
                    int min = 0;

                    if (int.TryParse(temp[1].Substring(0, 4), out y) && int.TryParse(temp[1].Substring(4, 2), out m) &&
                        int.TryParse(temp[1].Substring(6, 2), out d) && int.TryParse(temp[2].Substring(0, 2), out h) &&
                        int.TryParse(temp[2].Substring(2, 2), out min))
                    {
                        DateTime time = new DateTime(y, m, d, h, min, 00);
                        dispTxt = time.ToString("MMM dd. yyyy HH:mm") + " - " + temp[0];
                    }
                }
            }
            return dispTxt;
        }

        /// <summary>
        /// Creates a FileInfo from a file.
        /// </summary>
        /// <param name="file">Full path to the file to create the FileInfo of.</param>
        /// <returns>The FileInfo of the passed file.</returns>
        private static FileInfo GetFileInfoFromFilename(string file)
        {
            FileInfo fi = null;
            try
            {
                string filename = Path.GetFileNameWithoutExtension(file);

                int index = filename.IndexOf("_");
                string temp = filename.Substring(index + 1);

                index = temp.IndexOf("_");
                string num = temp.Substring(0, index);
                int number = int.Parse(num);
                temp = temp.Substring(index + 1);

                index = temp.IndexOf("_");
                string date = temp.Substring(0, index);
                string time = temp.Substring(index + 1);
                int year = int.Parse(date.Substring(0, 4));
                int month = int.Parse(date.Substring(4, 2));
                int day = int.Parse(date.Substring(6, 2));
                int hour = int.Parse(time.Substring(0, 2));
                int min = int.Parse(time.Substring(2, 2));

                DateTime dateTime = new DateTime(year, month, day, hour, min, 0);

                fi = new FileInfo(file, dateTime, number);
            }
            catch (Exception ex)
            {
                string NoWaringPLS = ex.Message;
            }

            return fi;
        }

        /// <summary>
        /// Returns the note to the corresponding display text.
        /// </summary>
        /// <param name="fileDisplayTxt">The display text of the backup file.</param>
        /// <returns>The note to the corresponding display text.</returns>
        private static string GetNote(string fileDisplayTxt)
        {
            return backupNotes.ContainsKey(fileDisplayTxt) ? backupNotes[fileDisplayTxt] : string.Empty;
        }

        class FileInfo
        {
            private string mFullPath = string.Empty;
            private DateTime mDateTime = DateTime.MinValue;
            private int mNumber = -1;

            public string FullPath
            {
                get
                {
                    return mFullPath;
                }
                set
                {
                    mFullPath = value;
                }
            }

            public DateTime DateTime
            {
                get
                {
                    return mDateTime;
                }
                set
                {
                    mDateTime = value;
                }
            }

            public int Number
            {
                get
                {
                    return mNumber;
                }
                set
                {
                    mNumber = value;
                }
            }

            public FileInfo(string fullPath, DateTime dateTime, int number)
            {
                FullPath = fullPath;
                DateTime = dateTime;
                Number = number;
            }
        }
    }
}
