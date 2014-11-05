using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using KSPModAdmin.Core.Config;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Localization;
using KSPModAdmin.Core.Utils.Logging;
using KSPModAdmin.Core.Views;

namespace KSPModAdmin.Core.Controller
{
    public class MainController : BaseController<MainController, frmMain>, IMessageReceiver
    {
        private const string KSPMA_LOG_FILENAME = "KSPMA.log";


        /// <summary>
        /// Gets or sets the selected KSP path.
        /// </summary>
        public static List<NoteNode> KnownKSPPaths
        {
            get { return (View == null) ? new List<NoteNode>() : View.KnownKSPPaths; } 
            set { if (View != null) View.KnownKSPPaths = value; }
        }

        /// <summary>
        /// Gets or sets the selected KSP path.
        /// </summary>
        public static string SelectedKSPPath
        {
            get { return (View == null) ? string.Empty : View.SelectedKSPPath; }
            set
            {
                if (View != null)
                {
                    View.SilentSetSelectedKSPPath(value);
                    OptionsController.SelectedKSPPath = value;
                }
            }
        }

        /// <summary>
        /// Flag to determine if the shut down process is running.
        /// </summary>
        public static bool IsShutDown { get; set; }


        /// <summary>
        /// Private constructor
        /// </summary>
        private MainController()
        {
            if (mInstance != null)
                throw new Exception(Messages.MSG_ONLY_ONE_INSTANCE_OF_CONTROLLER_ALLOWED);

            mInstance = this;
        }


        /// <summary>
        /// 
        /// </summary>
        public static void ShutDown()
        {
            IsShutDown = true;

            SaveAppConfig();
            SaveKSPConfig();

            // TODO
            //if (m_SearchDLG != null)
            //    m_SearchDLG.Close();

            Log.AddInfoS(string.Format("---> KSP MA v{0} closed <---{1}", VersionHelper.GetAssemblyVersion(true), Environment.NewLine));
        }

        #region IMessageReceiver implementation

        /// <summary>
        /// Adds a message to the info message box.
        /// </summary>
        /// <param name="msg">The message to add.</param>
        public void AddMessage(string msg)
        {
            View.InvokeIfRequired(() => View.tbMessages.AppendText(msg + Environment.NewLine));
        }

        /// <summary>
        /// Adds a message to the info message box.
        /// </summary>
        /// <param name="msg">The info message to add.</param>
        public void AddInfo(string msg)
        {
            View.InvokeIfRequired(() => View.tbMessages.AppendText(msg + Environment.NewLine));
            Log.AddInfoS(msg);
        }

        /// <summary>
        /// Adds a message to the info message box.
        /// </summary>
        /// <param name="msg">The debug message to add.</param>
        public void AddDebug(string msg)
        {
            View.InvokeIfRequired(() => View.tbMessages.AppendText(msg + Environment.NewLine));
            Log.AddDebugS(msg);
        }

        /// <summary>
        /// Adds a message to the info message box.
        /// </summary>
        /// <param name="msg">The warning message to add.</param>
        public void AddWarning(string msg)
        {
            View.InvokeIfRequired(() => View.tbMessages.AppendText(msg + Environment.NewLine));
            Log.AddWarningS(msg);
        }

        /// <summary>
        /// Adds a message to the info message box.
        /// </summary>
        /// <param name="msg">The error message to add.</param>
        /// <param name="ex">The exception to add to the error message.</param>
        public void AddError(string msg, Exception ex = null)
        {
            View.InvokeIfRequired(() => View.tbMessages.AppendText(msg + Environment.NewLine));
            Log.AddErrorS(msg, ex);
        }

        #endregion


        /// <summary>
        /// Sets the selected KSP path without raising event SelectedIndexChanged.
        /// </summary>
        /// <param name="kspPath">The new selected KSP path.</param>
        internal static void SilentSetSelectedKSPPath(string kspPath)
        {
            if (View != null)
                View.SilentSetSelectedKSPPath(kspPath);
        }


        /// <summary>
        /// This method gets called when your Controller should be initialized.
        /// Perform additional initialization of your UserControl here.
        /// </summary>
        protected override void Initialize()
        {
            SetupLogFile();
            Messenger.AddListener(this);

            EventDistributor.KSPRootChanging += KSPRootChanging;
            EventDistributor.KSPRootChanged += KSPRootChanged;

            LoadConfigs();

            OptionsController.AvailableLanguages = Localizer.GlobalInstance.AvailableLanguages;
            OptionsController.SelectedLanguage = Localizer.GlobalInstance.CurrentLanguage;

            LoadSiteHandler();

            LoadPlugins();

            if (!KSPPathHelper.IsKSPInstallFolder(OptionsController.SelectedKSPPath))
            {
                frmWelcome dlg = new frmWelcome();
                if (dlg.ShowDialog(View) != DialogResult.OK)
                {
                    View.Close();
                    return;
                }

                OptionsController.AddKSPPath(dlg.KSPPath);
                OptionsController.SelectedKSPPath = dlg.KSPPath;
            }

            // auto mod update check.
            OptionsController.Check4ModUpdates(true);
        }

        /// <summary>
        /// This method gets called when a critical asynchrony task will be started.
        /// Disable all controls of your View here to avoid multiple critical KSP MA changes.
        /// </summary>
        protected override void AsyncroneTaskStarted(object sender)
        {
            View.cbKSPPath.Enabled = false;
        }

        /// <summary>
        /// This method gets called when a critical asynchrony task is complete.
        /// Enable the controls of your View again here.
        /// </summary>
        protected override void AsyncroneTaskDone(object sender)
        {
            View.cbKSPPath.Enabled = true;
        }

        /// <summary>
        /// This method gets called when the language of KSP MA was changed.
        /// Perform extra translation work for your View here.
        /// </summary>
        protected override void LanguageHasChanged(object sender)
        {
            foreach (TabView tabView in mAddedTabViews.Values)
            {
                //tabView.TabUserControl.
            }
        }

        /// <summary>
        /// Event handler for the KSPPathChanging event from OptionsController.
        /// </summary>
        /// <param name="oldKSPPath">The last selected KSP path.</param>
        /// <param name="newKSPPath">The new selected ksp path.</param>
        private static void KSPRootChanging(string oldKSPPath, string newKSPPath)
        {
            if (!string.IsNullOrEmpty(oldKSPPath))
                SaveKSPConfig();
        }

        /// <summary>
        /// Event handler for the KSPPathChanged event from OptionsController.
        /// </summary>
        /// <param name="kspPath">the new selected ksp path.</param>
        private static void KSPRootChanged(string kspPath)
        {
            if (!string.IsNullOrEmpty(kspPath))
                LoadKSPConfig();

            SilentSetSelectedKSPPath(kspPath);
        }

        /// <summary>
        /// Deletes the old content of the log file when file size is above 1mb and creates a new log file.
        /// </summary>
        private static void SetupLogFile()
        {
            //#if DEBUG
            Log.GlobalInstance.LogMode = LogMode.All;
            //#else
            //            Log.GlobalInstance.LogMode = LogMode.WarningsAndErrors;
            //#endif
            try
            {
                string logPath = Path.Combine(Application.StartupPath, KSPMA_LOG_FILENAME);
                if (File.Exists(logPath))
                {
                    FileInfo fInfo = new FileInfo(logPath);
                    if (fInfo.Length > 2097152) // > 2mb
                        File.Delete(logPath);
                }

                Log.GlobalInstance.FullPath = logPath;

                Log.AddInfoS(string.Format("---> KSP MA v{0} started <---", VersionHelper.GetAssemblyVersion(true)));
            }
            catch (Exception)
            {
                MessageBox.Show(Messages.MSG_CANT_CREATE_KSPMA_LOG);
                Log.GlobalInstance.LogMode = LogMode.None;
            }
        }

        /// <summary>
        /// Loads the AppConfig & KSPConfig.
        /// </summary>
        private static void LoadConfigs()
        {
            Instance.AddInfo(Messages.MSG_LOADING_KSPMA_SETTINGS);
            string path = KSPPathHelper.GetPath(KSPPaths.AppConfig);
            if (File.Exists(path))
            {
                if (AdminConfig.Load(path))
                {
                    Instance.AddInfo(Messages.MSG_DONE);

                    // LoadKSPConfig will be started by KSPPathChange event.
                    //if (KSPPathHelper.IsKSPInstallFolder(OptionsController.SelectedKSPPath))
                    //    LoadKSPConfig();
                }
                else
                {
                    Instance.AddInfo(Messages.MSG_LOADING_KSPMA_SETTINGS_FAILED);
                }
            }
            else
            {
                Instance.AddInfo(Messages.MSG_KSPMA_SETTINGS_NOT_FOUND);
            }

            DeleteOldAppConfigs();
        }

        /// <summary>
        /// Loads the KSPConfig from the selected KSP folder.
        /// </summary>
        private static void LoadKSPConfig()
        {
            ModSelectionController.ClearMods();

            string configPath = KSPPathHelper.GetPath(KSPPaths.KSPConfig);
            if (File.Exists(configPath))
            {
                Instance.AddInfo(Messages.MSG_LOADING_KSP_MOD_CONFIGURATION);
                List<ModNode> mods = new List<ModNode>();
                KSPConfig.Load(configPath, ref mods);
                ModSelectionController.AddMods(mods.ToArray());
                ModSelectionController.SortModSelection();
            }
            else
            {
                Instance.AddInfo(Messages.MSG_KSP_MOD_CONFIGURATION_NOT_FOUND);
            }

            ModSelectionController.RefreshCheckedStateAllMods();
            Instance.AddInfo(Messages.MSG_DONE);
        }

        /// <summary>
        /// Deletes older config paths and files.
        /// </summary>
        private static void DeleteOldAppConfigs()
        {
            string path = KSPPathHelper.GetPath(KSPPaths.AppConfig);
            string[] dirs = Directory.GetDirectories(Path.GetDirectoryName(path));
            foreach (string dir in dirs)
            {
                try
                {
                    if (!Directory.Exists(dir))
                        continue;

                    Directory.Delete(dir, true);
                }
                catch (Exception)
                { }
            }
        }

        /// <summary>
        /// Saves the AppConfig to "c:\ProgramData\..."
        /// </summary>
        private static void SaveAppConfig()
        {
            try
            {
                Instance.AddInfo(Messages.MSG_SAVING_KSPMA_SETTINGS);
                string path = KSPPathHelper.GetPath(KSPPaths.AppConfig);
                if (path != string.Empty && Directory.Exists(Path.GetDirectoryName(path)))
                    AdminConfig.Save(path);
                else
                    Instance.AddError(Messages.MSG_KSPMA_SETTINGS_PATH_INVALID);
            }
            catch (Exception ex)
            {
                Instance.AddError(Messages.MSG_ERROR_DURING_SAVING_KSPMA_SETTINGS, ex);
                ShowAdminRightsDlg(ex);
            }
        }

        /// <summary>
        /// Saves the KSPConfig to the selected KSP folder.
        /// </summary>
        public static void SaveKSPConfig()
        {
            try
            {
                string path = KSPPathHelper.GetPath(KSPPaths.KSPConfig);
                if (path != string.Empty && Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Instance.AddInfo(Messages.MSG_SAVING_KSP_MOD_SETTINGS);
                    KSPConfig.Save(path, ModSelectionController.Mods);
                }
                else
                    Instance.AddError(Messages.MSG_KSP_MOD_SETTINGS_PATH_INVALID);
            }
            catch (Exception ex)
            {
                Instance.AddError(Messages.MSG_ERROR_DURING_SAVING_KSP_MOD_SETTINGS, ex);
                ShowAdminRightsDlg(ex);
            }
        }

        /// <summary>
        /// Shows a MessageBox with the info, that KSP MA needs admin rights if KSP is installed to c:\Programme
        /// </summary>
        /// <param name="ex">The message of the Exception will be displayed too.</param>
        private static void ShowAdminRightsDlg(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format(Messages.MSG_ERROR_MESSAGE_0, ex.Message));
            sb.AppendLine();
            sb.AppendLine(Messages.MSG_ACCESS_DENIED_DIALOG_MESSAGE);
            MessageBox.Show(View, sb.ToString(), Messages.MSG_TITLE_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Loads and registers all SiteHandler.
        /// </summary>
        private void LoadSiteHandler()
        {
            //Add default SiteHandler
            var siteHandlers = PluginLoader.GetPlugins<ISiteHandler>(new[] {Assembly.GetExecutingAssembly()});
            foreach (ISiteHandler handler in siteHandlers)
                SiteHandlerManager.RegisterSiteHandler(handler);

            //Add additional SiteHandlers
            siteHandlers = PluginLoader.LoadPlugins<ISiteHandler>(KSPPathHelper.GetPath(KSPPaths.KSPMA_Plugins));
            foreach (ISiteHandler handler in siteHandlers)
                SiteHandlerManager.RegisterSiteHandler(handler);
        }

        /// <summary>
        /// Loads all Plugins for KSP Mod Admin.
        /// </summary>
        private void LoadPlugins()
        {
            try
            {
                var plugins = PluginLoader.LoadPlugins<IKSPMAPlugin>(KSPPathHelper.GetPath(KSPPaths.KSPMA_Plugins));
                foreach (IKSPMAPlugin plugin in plugins)
                {
                    TabView[] tabViews = plugin.GetMainTabViews();
                    foreach (TabView tabView in tabViews)
                    {
                        if (!mAddedTabViews.ContainsKey(tabView.TabName))
                        {
                            TabPage tabPage = new TabPage();
                            tabPage.Text = tabView.TabName;
                            tabPage.Controls.Add(tabView.TabUserControl);
                            tabView.TabUserControl.Dock = DockStyle.Fill;
                            if (tabView.TabIcon != null)
                            {
                                View.TabControl.ImageList.Images.Add(tabView.TabIcon);
                                tabPage.ImageIndex = View.TabControl.ImageList.Images.Count - 1;
                            }
                            View.TabControl.TabPages.Add(tabPage);

                            mAddedTabViews.Add(tabView.TabName, tabView);
                        }
                        else
                        {
                            Messenger.AddError(string.Format("Plugin loading error: TabView \"{0}\" already exists!", tabView.TabName));
                        }
                    }

                    tabViews = plugin.GetOptionTabViews();
                    foreach (TabView tabView in tabViews)
                    {
                        if (!mAddedTabViews.ContainsKey(tabView.TabName))
                        {
                            TabPage tabPage = new TabPage();
                            tabPage.Text = tabView.TabName;
                            tabPage.Controls.Add(tabView.TabUserControl);
                            tabView.TabUserControl.Dock = DockStyle.Fill; ;
                            OptionsController.View.TabControl.TabPages.Add(tabPage);

                            mAddedTabViews.Add(tabView.TabName, tabView);
                        }
                        else
                        {
                            Messenger.AddError(string.Format("Plugin loading error: Option TabView \"{0}\" already exists!", tabView.TabName));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Messenger.AddError(string.Format("Plugin loading error: \"{0}\"", ex.Message), ex);
            }
        }
        private Dictionary<string, TabView> mAddedTabViews = new Dictionary<string, TabView>();
    }
}
