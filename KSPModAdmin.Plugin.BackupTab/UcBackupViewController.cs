using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using FolderSelect;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Config;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;
using SharpCompress.Archive;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;

namespace KSPModAdmin.Plugin.BackupTab
{
    /// <summary>
    /// Controller class for the Translation view.
    /// </summary>
    public class UcBackupViewController
    {
        private const string BACKUP_CONFIG_FILENAME = "KSPMABackup.cfg";

        private static BackupTreeModel model = new BackupTreeModel();
        private static Dictionary<string, string> backupNotes = new Dictionary<string, string>();
        private static UcBackupViewController mInstance = null;

        /// <summary>
        /// Gets or sets the view of the controller.
        /// </summary>
        public static UcBackupView View { get; protected set; }


        /// <summary>
        /// Gets the singleton of this class.
        /// </summary>
        protected static UcBackupViewController Instance { get { return mInstance ?? (mInstance = new UcBackupViewController()); } }

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
            if (!ValidBackupDirectory(BackupPath))
                return;

            FolderSelectDialog dlg = new FolderSelectDialog();
            dlg.Title = "Source folder selection";
            dlg.InitialDirectory = KSPPathHelper.GetPath(KSPPaths.KSPRoot);
            if (dlg.ShowDialog(View.ParentForm.Handle))
                BackupDirectoryAsync(dlg.FileName);
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
        private const string NAME = "Name";
        private const string NOTE = "Note";
        private const string BACKUPPATH = "BackupPath";
        private const string BACKUPFILES = "BackupFiles";
        private const string BACKUPFILE = "BackupFile";

        /// <summary>
        /// Loads the BackupNotes from the Backup.cfg file.
        /// </summary>
        /// <returns>A dictionary with the format Filename, Note.</returns>
        public static Dictionary<string, string> LoadBackupSettings()
        {
            var result = new Dictionary<string, string>();

            var fullpath = BACKUP_CONFIG_FILENAME;
            if (!File.Exists(fullpath))
            {
                Messenger.AddInfo(string.Format("Backup config file not found. \"{0}\"", fullpath));
                return result;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(fullpath);

            BackupNode root = new BackupNode(ROOT, ROOT, "");
            XmlNodeList nodeList = doc.GetElementsByTagName(BACKUPPATH);
            if (nodeList.Count >= 1 && nodeList[0].Attributes != null)
            {
                foreach (XmlAttribute att in nodeList[0].Attributes)
                {
                    if (att.Name == NAME)
                        BackupPath = att.Value;
                }
            }

            XmlNodeList mods = doc.GetElementsByTagName(BACKUPFILE);
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
        /// Saves the backup settings to a xml file.
        /// </summary>
        /// <param name="backupFileNodes">The backupfiles from the backup folder (to save the note).</param>
        public static void SaveBackupSettings()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode root = doc.CreateElement(Constants.ROOT);
            doc.AppendChild(root);

            XmlNode node = ConfigHelper.CreateConfigNode(doc, BACKUPPATH, NAME, BackupPath);
            root.AppendChild(node);

            node = doc.CreateElement(BACKUPFILES);
            root.AppendChild(node);

            foreach (BackupDataNode backupFile in model.Nodes)
            {
                XmlNode columnNode = ConfigHelper.CreateConfigNode(doc, BACKUPFILE, new string[,]
                {
                    { NAME, backupFile.Name },
                    { NOTE, backupFile.Note }
                });
                node.AppendChild(columnNode);
            }

            doc.Save(BACKUP_CONFIG_FILENAME);
        }

        /// <summary>
        /// Reads the backup directory and fills the backup TreeView.
        /// </summary>
        public static void ScanBackupDirectory()
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


        /// <summary>
        /// Validates the backup directory.
        /// </summary>
        /// <param name="dir">The directory to check for existance</param>
        /// <returns>True if hte passed path exists.</returns>
        private static bool ValidBackupDirectory(string dir)
        {
            bool result = false;
            try
            {
                if (dir != string.Empty && Directory.Exists(dir))
                    result = true;
            }
            catch (Exception ex)
            {
                string NoWarningPLS = ex.Message;
            }

            if (!result)
                MessageBox.Show(View.ParentForm, Messages.MSG_BACKUP_SELECT_FOLDER);

            return true;
        }


        /// <summary>
        /// Starts the backup of a directory.
        /// </summary>
        /// <param name="dir">The directory to backup.</param>
        /// <param name="name"></param>
        /// <param name="backupPath"></param>
        /// <param name="zipPath"></param>
        private static void BackupDirectoryAsync(string dir, string name = "", string backupPath = "", string zipPath = "")
        {
            try
            {
                string nameAuto;
                string backupPathAuto;
                string zipPathAuto;
                nameAuto = CreateBackupFilename(dir, out backupPathAuto, out zipPathAuto);

                if (string.IsNullOrEmpty(name))
                    name = nameAuto;

                if (string.IsNullOrEmpty(backupPath))
                    backupPath = backupPathAuto;

                if (string.IsNullOrEmpty(zipPath))
                    zipPath = zipPathAuto;

                if (!Directory.Exists(BackupPath))
                    Directory.CreateDirectory(BackupPath);

                if (Directory.Exists(dir))
                {
                    EventDistributor.InvokeAsyncTaskStarted(Instance);
                    Messenger.AddInfo("Backup started.");
                    AsyncTask<string>.DoWork(() => BackupDir(dir, name, backupPath), BackupDirFinished);
                }
                else
                    Messenger.AddInfo(string.Format(Messages.MSG_BACKUP_SRC_FOLDER_NOT_FOUND, dir));
            }
            catch (Exception ex)
            {
                Messenger.AddError(string.Format(Messages.MSG_BACKUP_ERROR, ex.Message), ex);
            }
        }

        /// <summary>
        /// Creates the name for a backup file.
        /// </summary>
        /// <param name="dir">The path to backup to.</param>
        /// <param name="fullpath">Full path of the created backup file.</param>
        /// <param name="zipPath"></param>
        /// <returns>The created name of the Backupfile.</returns>
        private static string CreateBackupFilename(string dir, out string fullpath, out string zipPath)
        {
            // create the name of the backupfile from the backup directory
            string name = dir.Substring(dir.LastIndexOf(Constants.PATHSEPERATOR) + 1, dir.Length - (dir.LastIndexOf(Constants.PATHSEPERATOR) + 1));
            string filename = String.Format("{0}_{1}{2}", name, DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
            fullpath = Path.Combine(BackupPath, filename);

            // get zip intern dir
            zipPath = dir.Replace(KSPPathHelper.GetPath(KSPPaths.KSPRoot) + "\\", "");

            // add _(x) to the filename if the file already exists.
            int i = 1;
            while (File.Exists(fullpath))
                fullpath = Path.GetDirectoryName(fullpath) + Constants.PATHSEPERATOR + Path.GetFileNameWithoutExtension(fullpath) + "_" + i++.ToString("00") + Path.GetExtension(fullpath);

            return name;
        }

        private static string BackupDir(string dir, string name, string backupPath)
        {
            int i = 0;
            using (var archive = ZipArchive.Create())
            {
                if (name != string.Empty && backupPath != string.Empty)
                {
                    // TODO: Add file/dir wise, not whole dir at once ...
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        string directoryName = Path.GetDirectoryName(file);
                        if (directoryName != null)
                        {
                            string temp =
                                Path.Combine(directoryName.Replace(KSPPathHelper.GetPath(KSPPaths.KSPRoot) + "\\", ""),
                                    Path.GetFileName(file));
                            archive.AddEntry(temp, file);
                        }
                    }

                    AddSubDirs(dir, archive);

                    archive.SaveTo(backupPath, CompressionType.None);
                }
                else
                    Messenger.AddInfo(Messages.MSG_BACKUP_CREATION_ERROR);
            }

            return name;
        }

        private static void BackupDirFinished(string name, Exception ex)
        {
            EventDistributor.InvokeAsyncTaskDone(Instance);

            if (ex != null)
            {
                Messenger.AddError(Messages.MSG_BACKUP_CREATION_ERROR, ex);
            }
            else
            {
                ScanBackupDirectory();

                Messenger.AddInfo(string.Format(Messages.MSG_BACKUP_COMPLETE, name));
            }
        }

        /// <summary>
        /// Adds the sub directory to the Archive.
        /// </summary>
        /// <param name="parentDir">Parent directory to scenn for sub dirs & files.</param>
        /// <param name="archive">The Archive to add the dirs & files to.</param>
        private static void AddSubDirs(string parentDir, ZipArchive archive)
        {
            foreach (string subDir in Directory.GetDirectories(parentDir))
            {
                foreach (string file in Directory.GetFiles(subDir))
                {
                    string directoryName = Path.GetDirectoryName(file);
                    if (directoryName != null)
                    {
                        string temp = Path.Combine(directoryName.Replace(KSPPathHelper.GetPath(KSPPaths.KSPRoot) + "\\", ""), Path.GetFileName(file));
                        archive.AddEntry(temp, file);
                    }
                }

                AddSubDirs(subDir, archive);
            }
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
