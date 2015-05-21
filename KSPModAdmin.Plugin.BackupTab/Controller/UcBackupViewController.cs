using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using FolderSelect;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Config;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Plugin.BackupTab.Model;
using KSPModAdmin.Plugin.BackupTab.Views;
using SharpCompress.Archive;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;

namespace KSPModAdmin.Plugin.BackupTab.Controller
{
    /// <summary>
    /// Controller class for the Translation view.
    /// </summary>
    public class UcBackupViewController
    {
        #region Members

        private const string BACKUP_CONFIG_FILENAME = "KSPMABackup.cfg";
        private const string ROOT = "BackupCFG";
        private const string NAME = "Name";
        private const string VALUE = "Value";
        private const string NOTE = "Note";
        private const string BACKUPPATH = "BackupPath";
        private const string AUTOBACKUP = "AutoBackup";
        private const string BACKUPINTERVAL = "BackupInterval";
        private const string MAXBACKUPFILES = "MaxBackupFiles";
        private const string BACKUPONKSPLAUNCH = "BackupOnKSPLaunch";
        private const string BACKUPONKSPMALAUNCH = "BackupOnKSPMALaunch";
        private const string BACKUPFILES = "BackupFiles";
        private const string BACKUPFILE = "BackupFile";

        private static BackupTreeModel model = new BackupTreeModel();
        private static Dictionary<string, string> backupNotes = new Dictionary<string, string>();
        private static UcBackupViewController instance = null;
        private static Timer autoBackupTimer = null;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton of this class.
        /// </summary>
        protected static UcBackupViewController Instance { get { return instance ?? (instance = new UcBackupViewController()); } }

        /// <summary>
        /// Gets the full path to the backup configuration file.
        /// </summary>
        public static string FullBackupConfigPath
        {
            get { return Path.Combine(KSPPathHelper.GetPath(KSPPaths.KSPRoot), BACKUP_CONFIG_FILENAME); }
        }

        /// <summary>
        /// Gets or sets the view of the controller.
        /// </summary>
        public static UcBackupView View { get; protected set; }

        /// <summary>
        /// The model of the backup TreeViewAdv.
        /// </summary>
        public static BackupTreeModel Model
        {
            get { return model as BackupTreeModel; }
            set { model = value; }
        }

        /// <summary>
        /// The backup path where the backup will be saved to.
        /// </summary>
        public static string BackupPath
        {
            get { return View.BackupPath; }
            set { View.BackupPath = value; }
        }

        /// <summary>
        /// The selected backup file.
        /// </summary>
        public static BackupNode SelectedBackup
        {
            get { return View.SelectedBackup; }
        }

        /// <summary>
        /// Toggles the on off state of the auto backup function.
        /// </summary>
        public static bool AutoBackupOnOff
        {
            get { return autoBackupTimer.Tag != null && (bool)autoBackupTimer.Tag; }
            set
            {
                autoBackupTimer.Tag = value;

                if (View.AutoBackup != value)
                    View.AutoBackup = value;

                if (View.AutoBackup)
                {
                    autoBackupTimer.Stop();
                    autoBackupTimer.Interval = (int)(BackupInterval * 60 * 1000); // minutes to milisecs.
                    autoBackupTimer.Start();
                }
                else
                {
                    autoBackupTimer.Stop();
                }
            }
        }

        /// <summary>
        /// The interval to do a backup of the save folder (in minutes).
        /// </summary>
        [DefaultValue(60)]
        public static int BackupInterval
        {
            get { return View.BackupInterval; }
            set
            {
                if (View.BackupInterval != value)
                    View.BackupInterval = value;
            }
        }

        /// <summary>
        /// Maximum of auto backup files.
        /// If maximum is reached older auto backup will be replaced.
        /// </summary>
        [DefaultValue(5)]
        public static int MaxBackupFiles
        {
            get { return View.MaxBackupFiles; }
            set
            {
                if (View.MaxBackupFiles != value)
                    View.MaxBackupFiles = value;
            }
        }

        /// <summary>
        /// Gets or sets the flag to determine if we should make a backup on a launch of KSP.
        /// </summary>
        public static bool BackupOnKSPLaunch
        {
            get { return View.BackupOnKSPLaunch; }
            set
            {
                if (View.BackupOnKSPLaunch != value)
                    View.BackupOnKSPLaunch = value;
            }
        }

        /// <summary>
        /// Gets or sets the flag to determine if we should make a backup on a launch of KSP Mod Admin.
        /// </summary>
        public static bool BackupOnKSPMALaunch
        {
            get { return View.BackupOnKSPMALaunch; }
            set
            {
                if (View.BackupOnKSPMALaunch != value)
                    View.BackupOnKSPMALaunch = value;
            }
        }

        #endregion

        /// <summary>
        /// Initializes the controller.
        /// </summary>
        /// <param name="view">^The view of the controller.</param>
        internal static void Initialize(UcBackupView view)
        {
            View = view;

            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;
            EventDistributor.LanguageChanged += LanguageChanged;
            EventDistributor.KSPRootChanged += KSPRootChanged;
            EventDistributor.StartingKSP += StartingKSP;
            EventDistributor.KSPMAStarted += KSPMAStarted;

            // Add your stuff to initialize here.
            View.Model = model;
            View.AddActionKey(VirtualKey.VK_DELETE, RemoveBackup);
            View.AddActionKey(VirtualKey.VK_BACK, RemoveBackup);

            autoBackupTimer = new Timer();
            autoBackupTimer.Tick += new EventHandler(AutoBackupTimerTick);
            autoBackupTimer.Tag = false;
        }

        #region EventDistributor callback functions.

        /// <summary>
        /// Callback function for the AsyncTaskStarted event.
        /// Should disable all controls of the BaseView.
        /// </summary>
        protected static void AsyncTaskStarted(object sender)
        {
            View.SetEnabledOfAllControls(false);
        }

        /// <summary>
        /// Callback function for the AsyncTaskDone event.
        /// Should enable all controls of the BaseView.
        /// </summary>
        protected static void AsyncTaskDone(object sender)
        {
            View.SetEnabledOfAllControls(true);
        }

        /// <summary>
        /// Callback function for the LanguageChanged event.
        /// This is the place where you can translate non accessible controls.
        /// </summary>
        protected static void LanguageChanged(object sender)
        {
            View.LanguageChanged();
        }

        /// <summary>
        /// Callback function for the KSPRootChanged event.
        /// This is the place to handle a change of the selected KSP installation path..
        /// </summary>
        protected static void KSPRootChanged(string kspPath)
        {
            View.StartUpdate();
            View.BackupPath = string.Empty;
            LoadBackupSettings();
            ScanBackupDirectory();
        }

        /// <summary>
        /// Callback function for the StartingKSP event.
        /// This is the place to handle anything when KSP is launching.
        /// </summary>
        protected static void StartingKSP(object sender)
        {
            LoadBackupSettings();
            ScanBackupDirectory();
            if (BackupOnKSPLaunch)
                KSPLaunchBackup();
        }

        /// <summary>
        /// Callback function for the KSPMAStarted event.
        /// This is the place to handle anything like additional initializing if init is depended on fully loaded ModSelection.
        /// </summary>
        protected static void KSPMAStarted(object sender)
        {
            LoadBackupSettings();
            ScanBackupDirectory();
            if (BackupOnKSPMALaunch)
                StartupBackup();
        }

        /// <summary>
        /// Handles the Tick event of the mAutoBackupTimer timer.
        /// </summary>
        protected static void AutoBackupTimerTick(object sender, EventArgs e)
        {
            DoAutoBackup();
        }

        /// <summary>
        /// ActionKey callback function to handle Delete and Back key.
        /// Deletes the selected backup.
        /// </summary>
        /// <returns>True, cause we have handled the key.</returns>
        protected static bool RemoveBackup(ActionKeyInfo keyState)
        {
            RemoveSelectedBackup();

            return true;
        }

        #endregion

        #region Public

        /// <summary>
        /// Loads the BackupNotes from the Backup.cfg file.
        /// </summary>
        /// <returns>A dictionary with the format Filename, Note.</returns>
        public static Dictionary<string, string> LoadBackupSettings()
        {
            backupNotes = new Dictionary<string, string>();
            View.StartUpdate();

            var fullpath = FullBackupConfigPath;
            if (!File.Exists(fullpath))
            {
                Messenger.AddInfo(string.Format(Messages.MSG_CREATE_NEW_BACKUP_CFG, fullpath));
                SaveBackupSettings();
            }

            Messenger.AddInfo(string.Format(Messages.MSG_BACKUP_LOAD_CFG, fullpath));

            XmlDocument doc = new XmlDocument();
            doc.Load(fullpath);

            XmlNodeList nodeList = doc.GetElementsByTagName(BACKUPPATH);
            if (nodeList.Count >= 1 && nodeList[0].Attributes != null)
            {
                foreach (XmlAttribute att in nodeList[0].Attributes)
                {
                    if (att.Name == NAME)
                        BackupPath = att.Value;
                }
            }

            nodeList = doc.GetElementsByTagName(AUTOBACKUP);
            if (nodeList.Count >= 1 && nodeList[0].Attributes != null)
            {
                foreach (XmlAttribute att in nodeList[0].Attributes)
                {
                    if (att.Name == VALUE)
                        AutoBackupOnOff = att.Value.Equals("true", StringComparison.CurrentCultureIgnoreCase);
                }
            }

            nodeList = doc.GetElementsByTagName(BACKUPINTERVAL);
            if (nodeList.Count >= 1 && nodeList[0].Attributes != null)
            {
                foreach (XmlAttribute att in nodeList[0].Attributes)
                {
                    int value = 60;
                    if (att.Name == VALUE && int.TryParse(att.Value, out value))
                        BackupInterval = value;
                }
            }

            nodeList = doc.GetElementsByTagName(MAXBACKUPFILES);
            if (nodeList.Count >= 1 && nodeList[0].Attributes != null)
            {
                foreach (XmlAttribute att in nodeList[0].Attributes)
                {
                    int value = 5;
                    if (att.Name == VALUE && int.TryParse(att.Value, out value))
                        MaxBackupFiles = value;
                }
            }

            nodeList = doc.GetElementsByTagName(BACKUPONKSPLAUNCH);
            if (nodeList.Count >= 1 && nodeList[0].Attributes != null)
            {
                foreach (XmlAttribute att in nodeList[0].Attributes)
                {
                    if (att.Name == VALUE)
                        BackupOnKSPLaunch = att.Value.Equals("true", StringComparison.CurrentCultureIgnoreCase);
                }
            }

            nodeList = doc.GetElementsByTagName(BACKUPONKSPMALAUNCH);
            if (nodeList.Count >= 1 && nodeList[0].Attributes != null)
            {
                foreach (XmlAttribute att in nodeList[0].Attributes)
                {
                    if (att.Name == VALUE)
                        BackupOnKSPMALaunch = att.Value.Equals("true", StringComparison.CurrentCultureIgnoreCase);
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

                backupNotes.Add(name, note);
            }

            View.EndUpdate();

            return backupNotes;
        }

        /// <summary>
        /// Saves the backup settings to a xml file.
        /// </summary>
        public static void SaveBackupSettings()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode root = doc.CreateElement(ROOT);
            doc.AppendChild(root);

            XmlNode node = ConfigHelper.CreateConfigNode(doc, BACKUPPATH, NAME, BackupPath);
            root.AppendChild(node);

            node = ConfigHelper.CreateConfigNode(doc, AUTOBACKUP, VALUE, AutoBackupOnOff.ToString());
            root.AppendChild(node);

            node = ConfigHelper.CreateConfigNode(doc, BACKUPINTERVAL, VALUE, BackupInterval.ToString());
            root.AppendChild(node);

            node = ConfigHelper.CreateConfigNode(doc, MAXBACKUPFILES, VALUE, MaxBackupFiles.ToString());
            root.AppendChild(node);

            node = ConfigHelper.CreateConfigNode(doc, BACKUPONKSPLAUNCH, VALUE, BackupOnKSPLaunch.ToString());
            root.AppendChild(node);

            node = ConfigHelper.CreateConfigNode(doc, BACKUPONKSPMALAUNCH, VALUE, BackupOnKSPMALaunch.ToString());
            root.AppendChild(node); 

            node = doc.CreateElement(BACKUPFILES);
            root.AppendChild(node);

            foreach (BackupNode backupFile in model.Nodes)
            {
                XmlNode columnNode = ConfigHelper.CreateConfigNode(doc, BACKUPFILE, new string[,]
                {
                    { NAME, backupFile.Name },
                    { NOTE, backupFile.Note }
                });
                node.AppendChild(columnNode);
            }

            doc.Save(FullBackupConfigPath);
        }

        /// <summary>
        /// Opens the FolderSelectDialog to select a new backup path.
        /// </summary>
        public static void SelectNewBackupPath()
        {
            var dlg = new FolderSelectDialog();
            dlg.Title = "Please select a new Backup path.";
            dlg.InitialDirectory = string.Empty;

            if (dlg.ShowDialog(View.ParentForm.Handle))
                View.BackupPath = dlg.FileName;
        }

        /// <summary>
        /// Opens the backup path in a explorer window.
        /// </summary>
        public static void OpenBackupPath()
        {
            if (!string.IsNullOrEmpty(View.BackupPath))
                OptionsController.OpenFolder(View.BackupPath);
        }

        /// <summary>
        /// Opens the SelectFolderDialog to select a folder to backup
        /// and Starts the backup process async.
        /// </summary>
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

        /// <summary>
        /// Creates a backup of the saves folder of the current selected KSP install.
        /// </summary>
        public static void BackupSaves()
        {
            if (ValidBackupDirectory(BackupPath))
                BackupDirectoryAsync(KSPPathHelper.GetPath(KSPPaths.Saves));
        }

        /// <summary>
        /// Starts the backup of a directory.
        /// </summary>
        /// <param name="dir">The directory to backup.</param>
        /// <param name="name">The name of the backup file.</param>
        /// <param name="backupPath">The path to write the backup to.</param>
        /// <returns>The new name of the backup file.</returns>
        public static string BackupDir(string dir, string name, string backupPath)
        {
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
                                Path.Combine(directoryName.Replace(KSPPathHelper.GetPath(KSPPaths.KSPRoot) + Path.DirectorySeparatorChar, string.Empty),
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

        /// <summary>
        /// Removes all backups in the backup path.
        /// </summary>
        public static void RemoveAllBackups()
        {
            if (ValidBackupDirectory(BackupPath))
            {
                if (DialogResult.Yes != MessageBox.Show(View.ParentForm, Messages.MSG_BACKUP_DELETE_ALL_QUESTION, string.Empty, MessageBoxButtons.YesNo))
                    return;

                if (Directory.Exists(BackupPath))
                {
                    foreach (string file in Directory.EnumerateFiles(BackupPath, "*" + Constants.EXT_ZIP))
                        RemoveBackup(file);

                    Model.Nodes.Clear();
                }
            }
        }

        /// <summary>
        /// Deletes and removes the selected backup.
        /// </summary>
        public static void RemoveSelectedBackup()
        {
            if (ValidBackupDirectory(BackupPath))
            {
                if (DialogResult.Yes != MessageBox.Show(View.ParentForm, string.Format(Messages.MSG_BACKUP_DELETE_QUESTION, SelectedBackup.Name), string.Empty, MessageBoxButtons.YesNo))
                    return;

                if (Directory.Exists(BackupPath))
                {
                    RemoveBackup(SelectedBackup.Key);
                    Model.Nodes.Remove(SelectedBackup);
                }
            }
        }

        /// <summary>
        /// Deletes the backup file.
        /// </summary>
        /// <param name="file">The file to delete.</param>
        public static void RemoveBackup(string file)
        {
            if (File.Exists(file))
            {
                try
                {
                    File.Delete(file);
                    Messenger.AddInfo(string.Format(Messages.MSG_BACKUP_DELETED, Path.GetFileName(file)));
                }
                catch (Exception ex)
                {
                    Messenger.AddError(string.Format(Messages.MSG_BACKUP_DELETED_ERROR, Path.GetFileName(file)), ex);
                }
            }
            else
                Messenger.AddInfo(string.Format(Messages.MSG_BACKUP_NOT_FOUND, Path.GetFileName(file)));
        }

        /// <summary>
        /// Recovers the selected backup.
        /// </summary>
        public static void RecoverSelectedBackup()
        {
            if (SelectedBackup != null && ValidBackupDirectory(BackupPath))
            {
                string file = SelectedBackup.Key;
                if (File.Exists(file))
                {
                    string savesPath = KSPPathHelper.GetPath(KSPPaths.Saves);
                    if (Directory.Exists(savesPath))
                    {
                        string kspPath = KSPPathHelper.GetPath(KSPPaths.KSPRoot);
                        using (IArchive archive = ArchiveFactory.Open(file))
                        {
                            foreach (IArchiveEntry entry in archive.Entries)
                            {
                                string dir = Path.GetDirectoryName(entry.FilePath);
                                CreateNeededDir(dir);
                                entry.WriteToDirectory(Path.Combine(kspPath, dir));
                            }
                        }
                        Messenger.AddInfo(string.Format(Messages.MSG_BACKUP_REINSTALLED, SelectedBackup.Name));
                    }
                    else
                        Messenger.AddInfo(string.Format(Messages.MSG_FOLDER_NOT_FOUND, savesPath));
                }
            }
            else
                MessageBox.Show(View.ParentForm, Messages.MSG_BACKUP_SRC_MISSING);
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

                    SaveBackupSettings();
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

            View.InvalidateView();
        }

        /// <summary>
        /// Creates a Backup of the saves folder with the name "StartupBackup_{yyyyMMdd_HHmm}.zip".
        /// </summary>
        public static void StartupBackup()
        {
            Messenger.AddInfo("Backup on startup started.");
            string dir = KSPPathHelper.GetPath(KSPPaths.Saves);
            string zipPath = dir.Replace(KSPPathHelper.GetPath(KSPPaths.KSPRoot) + Path.DirectorySeparatorChar, string.Empty);
            string name = String.Format("StartupBackup_{0}{1}", DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
            BackupDirectoryAsync(dir, name, Path.Combine(BackupPath, name), zipPath);
        }

        /// <summary>
        /// Creates a Backup of the saves folder with the name "KSPLaunchBackup_{yyyyMMdd_HHmm}.zip".
        /// </summary>
        public static void KSPLaunchBackup()
        {
            Messenger.AddInfo("Backup on KSP launch started.");
            string dir = KSPPathHelper.GetPath(KSPPaths.Saves);
            string zipPath = dir.Replace(KSPPathHelper.GetPath(KSPPaths.KSPRoot) + Path.DirectorySeparatorChar, string.Empty);
            string name = String.Format("KSPLaunchBackup_{0}{1}", DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
            BackupDirectoryAsync(dir, name, Path.Combine(BackupPath, name), zipPath);
        }

        #endregion

        #region Private

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
                string noWaringPLS = ex.Message;
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
        /// <param name="dir">The directory to check for existence</param>
        /// <returns>True if the passed path exists.</returns>
        private static bool ValidBackupDirectory(string dir)
        {
            bool result = false;
            try
            {
                if (dir != string.Empty && Directory.Exists(dir))
                    result = true;
            }
            catch (Exception) { }

            if (!result)
                MessageBox.Show(View.ParentForm, Messages.MSG_BACKUP_SELECT_FOLDER);

            return true;
        }

        /// <summary>
        /// Starts the backup of a directory.
        /// </summary>
        /// <param name="dir">The directory to backup.</param>
        /// <param name="name">Name of the backup file.</param>
        /// <param name="backupPath">The path to write the backup file to.</param>
        /// <param name="zipPath">Base path within the zip archive.</param>
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
                    View.ShowProcessing = true;
                    Messenger.AddInfo(Messages.MSG_BACKUP_STARTED);
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
        /// <param name="zipPath">Path within the zip archive.</param>
        /// <returns>The created name of the backup file.</returns>
        private static string CreateBackupFilename(string dir, out string fullpath, out string zipPath)
        {
            // create the name of the backupfile from the backup directory
            string name = dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1, dir.Length - (dir.LastIndexOf(Path.DirectorySeparatorChar) + 1));
            string filename = String.Format("{0}_{1}{2}", name, DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
            fullpath = Path.Combine(BackupPath, filename);

            // get zip intern dir
            zipPath = dir.Replace(KSPPathHelper.GetPath(KSPPaths.KSPRoot) + Path.DirectorySeparatorChar, string.Empty);

            // add _(x) to the filename if the file already exists.
            int i = 1;
            while (File.Exists(fullpath))
                fullpath = Path.GetDirectoryName(fullpath) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(fullpath) + "_" + (i++).ToString("00") + Path.GetExtension(fullpath);

            return name;
        }

        /// <summary>
        /// Callback function after a backup.
        /// </summary>
        private static void BackupDirFinished(string name, Exception ex)
        {
            EventDistributor.InvokeAsyncTaskDone(Instance);
            View.ShowProcessing = false;

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
        /// <param name="parentDir">Parent directory to scan for sub directories and files.</param>
        /// <param name="archive">The Archive to add the directories and files to.</param>
        private static void AddSubDirs(string parentDir, ZipArchive archive)
        {
            foreach (string subDir in Directory.GetDirectories(parentDir))
            {
                foreach (string file in Directory.GetFiles(subDir))
                {
                    string directoryName = Path.GetDirectoryName(file);
                    if (directoryName != null)
                    {
                        string temp = Path.Combine(directoryName.Replace(KSPPathHelper.GetPath(KSPPaths.KSPRoot) + Path.DirectorySeparatorChar, string.Empty), Path.GetFileName(file));
                        archive.AddEntry(temp, file);
                    }
                }

                AddSubDirs(subDir, archive);
            }
        }

        /// <summary>
        /// Creates the directory if it not exists.
        /// </summary>
        /// <param name="directory">The directory to create.</param>
        private static void CreateNeededDir(string directory)
        {
            try
            {
                string path = KSPPathHelper.GetPath(KSPPaths.KSPRoot);
                if (!Directory.Exists(Path.Combine(path, directory)))
                {
                    string[] dirs = directory.Split('\\');
                    foreach (string dir in dirs)
                    {
                        path = Path.Combine(path, dir);
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                    }
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Creates a auto backup of the saves folder.
        /// </summary>
        private static void DoAutoBackup()
        {
            int count = 1;
            string name = string.Format("AutoBackup_{0}_{1}{2}", count, DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
            string dir = KSPPathHelper.GetPath(KSPPaths.Saves);
            string zipPath = dir.Replace(KSPPathHelper.GetPath(KSPPaths.KSPRoot) + Path.DirectorySeparatorChar, string.Empty);

            if (Directory.Exists(BackupPath))
            {
                FileInfo fileInfo = null;
                string[] autoBackups = Directory.EnumerateFiles(BackupPath, "AutoBackup_*" + Constants.EXT_ZIP).ToArray();
                List<FileInfo> list = new List<FileInfo>();
                foreach (string file in autoBackups)
                {
                    FileInfo fi = GetFileInfoFromFilename(file);
                    if (fi == null) continue;

                    list.Add(fi);
                    list.Sort(delegate(FileInfo a, FileInfo b) { return a.Number.CompareTo(b.Number); });
                }

                while (list.Count > MaxBackupFiles)
                    list.RemoveAt(list.Count - 1);

                if (list.Count == MaxBackupFiles)
                {
                    list.Sort(delegate(FileInfo a, FileInfo b)
                    {
                        if (b.DateTime.CompareTo(a.DateTime) == 0)
                            return b.Number.CompareTo(a.Number);

                        return b.DateTime.CompareTo(a.DateTime);
                    });

                    fileInfo = list.Last();
                    string fname = string.Format("AutoBackup_{0}_{1}{2}", fileInfo.Number, DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
                    if (File.Exists(fileInfo.FullPath))
                    {
                        File.Delete(fileInfo.FullPath);
                        Messenger.AddInfo(string.Format(Messages.MSG_BACKUP_DELETED, Path.GetFileName(fileInfo.FullPath)));
                    }
                    fileInfo = new FileInfo(Path.Combine(BackupPath, fname), DateTime.Now, fileInfo.Number);
                }
                else if (list.Count > 0)
                {
                    int number = 1;
                    string fname = string.Empty;
                    for (; number <= MaxBackupFiles; ++number)
                    {
                        bool found = false;
                        string key = string.Format("AutoBackup_{0}", number);
                        foreach (FileInfo fi in list)
                        {
                            var fileName = Path.GetFileName(fi.FullPath);
                            if (fileName == null || !fileName.StartsWith(key))
                                continue;

                            found = true;
                            break;
                        }

                        if (found)
                            continue;

                        fname = string.Format("AutoBackup_{0}_{1}{2}", number, DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
                        break;
                    }

                    if (fname == string.Empty)
                    {
                        fileInfo = list.Last();
                        number = fileInfo.Number;
                        fname = string.Format("AutoBackup_{0}_{1}{2}", number, DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
                        if (File.Exists(fileInfo.FullPath))
                        {
                            File.Delete(fileInfo.FullPath);
                            Messenger.AddInfo(string.Format(Messages.MSG_BACKUP_DELETED, Path.GetFileName(fileInfo.FullPath)));
                        }
                    }

                    fileInfo = new FileInfo(Path.Combine(BackupPath, fname), DateTime.Now, number);
                }

                if (fileInfo == null)
                {
                    string fname = string.Format("AutoBackup_{0}_{1}{2}", 1, DateTime.Now.ToString("yyyyMMdd_HHmm"), Constants.EXT_ZIP);
                    fileInfo = new FileInfo(Path.Combine(BackupPath, fname), DateTime.Now, 1);
                }

                if (File.Exists(fileInfo.FullPath))
                {
                    File.Delete(fileInfo.FullPath);
                    Messenger.AddInfo(string.Format(Messages.MSG_BACKUP_DELETED, Path.GetFileName(fileInfo.FullPath)));
                }

                name = Path.GetFileName(fileInfo.FullPath);

                if (name != null)
                {
                    Messenger.AddInfo("Autobackup started.");
                    BackupDirectoryAsync(dir, name, Path.Combine(BackupPath, name), zipPath);
                }
            }
            else
            {
                Messenger.AddInfo(string.Format(Messages.MSG_FOLDER_NOT_FOUND, BackupPath));
            }
        }

        #endregion

        #region Inner classes

        private class FileInfo
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

        #endregion
    }
}
