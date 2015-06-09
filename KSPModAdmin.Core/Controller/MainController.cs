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
    /// <summary>
    /// Controller for the frmMain.
    /// </summary>
    public class MainController : IMessageReceiver
    {
        private const string KSPMA_LOG_FILENAME = "KSPMA.log";

        #region Properties

        /// <summary>
        /// Gets the singleton of this class.
        /// </summary>
        protected static MainController Instance { get { return mInstance ?? (mInstance = new MainController()); } }
        private static MainController mInstance = null;

        /// <summary>
        /// Flag to determine if the shut down process is running.
        /// </summary>
        public static bool IsShutDown { get; set; }

        /// <summary>
        /// Gets or sets the view of the controller.
        /// </summary>
        public static frmMain View { get; protected set; }

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
        /// Gets or sets the selected KSP path silently (without throwing events).
        /// </summary>
        internal static string _SelectedKSPPath
        {
            get { return SelectedKSPPath; }
            set
            {
                if (View != null)
                    View.SilentSetSelectedKSPPath(value);
            }
        }

        /// <summary>
        /// Gets the KSP LaunchPanel (ucKSPStartup).
        /// </summary>
        public static ucKSPStartup LaunchPanel { get { return View.ucKSPStartup1; } }

        /// <summary>
        /// Gets or sets the TabOrder of the main TabControl by UniqueIdentifier.
        /// </summary>
        public static IEnumerable<string> TapOrder
        {
            get { return View.TapOrder; }
            set { View.TapOrder = value; }
        }

        /// <summary>
        /// Gets or sets the last TabOrder of the main TabControl by UniqueIdentifier.
        /// </summary>
        public static List<string> LastTabOrder { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor (use static function only).
        /// </summary>
        private MainController()
        {
        }

        /// <summary>
        /// Static constructor. Creates a singleton of this class.
        /// </summary>
        static MainController()
        {
            if (mInstance == null)
                mInstance = new MainController();

            LastTabOrder = new List<string>();
        }

        #endregion

        /// <summary>
        /// Shows the KSP MA frmMain.
        /// </summary>
        public static void ShowMainForm()
        {
            try
            {
                SetupLogFile();

                LoadLanguages();

                View = new frmMain();

                Initialize();

                if (!IsShutDown)
                    Application.Run(View);
            }
            catch (Exception ex)
            {
                string msg = string.Format("Unexpected runtime error: \"{0}\"", ex.Message);
                string displayMsg = string.Format("{0}{1}{1}If you want to help please send the {2} from the KSP Mod Admin intall dir to{1}mackerbal@mactee.de{1}or use the issue tracker{1}https://github.com/MacTee/KSP-Mod-Admin-aOS/issues", msg, Environment.NewLine, KSPMA_LOG_FILENAME);
                MessageBox.Show(displayMsg, Messages.MSG_TITLE_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.AddErrorS(msg, ex);
            }

            Log.AddInfoS(string.Format("---> KSP MA v{0} closed <---{1}", VersionHelper.GetAssemblyVersion(true), Environment.NewLine));
        }

        /// <summary>
        /// Shuts KSP MA down.
        /// </summary>
        public static void ShutDown(bool closeMainView = true)
        {
            IsShutDown = true;

            SaveAppConfig();
            SaveKSPConfig();

            if (closeMainView)
                View.Close();
        }

        /// <summary>
        /// Invokes the ShowsDialog of a frmBase derived Form.
        /// This way you can show a dialog from a worker thread.
        /// </summary>
        /// <typeparam name="T_FormType">The form that should be shown.</typeparam>
        /// <param name="parameters">Optional array of From constructor parameters.</param>
        /// <returns>The KSPDialogResults of the dialog.</returns>
        public static KSPDialogResult ShowForm<T_FormType>(object[] parameters = null) where T_FormType : frmBase
        {
            KSPDialogResult result = null;
            try
            {
                View.Invoke(new Action(() =>
                {
                    T_FormType frm = null;
                    if (parameters != null)
                        frm = Activator.CreateInstance(typeof(T_FormType), parameters) as T_FormType;
                    else
                        frm = Activator.CreateInstance<T_FormType>();

                    if (frm != null)
                    {
                        frm.ShowDialog(View);
                        result = frm.GetKSPDialogResults();
                    }
                    else
                    {
                        result = new KSPDialogResult(DialogResult.Cancel, null, new Exception(string.Format("Can not create a Form of type {0}!", typeof(T_FormType))));
                    }
                }));
            }
            catch (Exception ex)
            {
                result = new KSPDialogResult(DialogResult.Cancel, null, ex);
            }

            return result;
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
        /// Sets the ToolTip values to all controls of the view.
        /// </summary>
        internal static void SetToolTipValues(bool ttOnOff, decimal ttDelay, decimal ttDisplayTime)
        {
            SetToolTipValues(ttOnOff, ttDelay, ttDisplayTime, View);
        }

        /// <summary>
        /// Sets the ToolTip values to all controls of the view.
        /// </summary>
        protected static void SetToolTipValues(bool ttOnOff, decimal ttDelay, decimal ttDisplayTime, Control control)
        {
            if (control == null)
                return;

            ToolTip tt = ControlTranslator.GetToolTipControl(control);
            if (tt != null)
            {
                tt.Active = ttOnOff;
                tt.InitialDelay = (int)(ttDelay * 1000);
                tt.AutoPopDelay = (int)(ttDisplayTime * 1000);
            }

            if (control.GetType() == typeof(ToolStrip))
                ((ToolStrip)control).ShowItemToolTips = ttOnOff;

            foreach (Control ctrl in control.Controls)
                SetToolTipValues(ttOnOff, ttDelay, ttDisplayTime, ctrl);
        }

        /// <summary>
        /// Loads all available languages.
        /// </summary>
        protected static void LoadLanguages()
        {
            Log.AddDebugS("Loading languages ...");

            // Try load languages.
            bool langLoadFailed = false;
            try
            {
                Localizer.GlobalInstance.DefaultLanguage = "eng";
                langLoadFailed = !Localizer.GlobalInstance.LoadLanguages(new[] 
                    {
                        KSPPathHelper.GetPath(KSPPaths.LanguageFolder), 
                        Path.Combine(KSPPathHelper.GetPath(KSPPaths.KSPMA_Plugins), Constants.LANGUAGE_FOLDER) 
                    }, true);
            }
            catch (Exception ex)
            {
                Log.AddErrorS("Error in MainController.LoadLanguages()", ex);
                langLoadFailed = true;
            }

            if (langLoadFailed)
            {
                MessageBox.Show("Can not load languages!" + Environment.NewLine + "Fall back to defalut language: English", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Localizer.GlobalInstance.Clear();
            }

            Log.AddDebugS(string.Format("Loading languages done. ({0})", string.Join(", ", Localizer.GlobalInstance.AvailableLanguages)));
        }

        /// <summary>
        /// This method gets called when your Controller should be initialized.
        /// Perform additional initialization of your UserControl here.
        /// </summary>
        protected static void Initialize()
        {
            Messenger.AddListener(Instance);

            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;
            EventDistributor.LanguageChanged += LanguageChanged;
            EventDistributor.KSPRootChanging += KSPRootChanging;
            EventDistributor.KSPRootChanged += KSPRootChanged;
            EventDistributor.KSPMAStarted += KSPMAStarted;

            CreateConfigDir();
            LoadConfigs();

            LoadPlugins();

            View.TapOrder = LastTabOrder;

            OptionsController.AvailableLanguages = Localizer.GlobalInstance.AvailableLanguages;
            OptionsController.SelectedLanguage = Localizer.GlobalInstance.CurrentLanguage;

            LoadSiteHandler();

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

            // Initializing is done.
            EventDistributor.InvokeKSPMAStarted(Instance);
        }

        /// <summary>
        /// Deletes the old content of the log file when file size is above 1mb and creates a new log file.
        /// </summary>
        protected static void SetupLogFile()
        {
            ////#if DEBUG
            Log.GlobalInstance.LogMode = LogMode.All;
            ////#else
            ////            Log.GlobalInstance.LogMode = LogMode.WarningsAndErrors;
            ////#endif
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
        /// First run initialization of config directory.
        /// </summary>    
        protected static void CreateConfigDir()
        {
            string path = KSPPathHelper.GetPath(KSPPaths.AppConfig);
            path = Directory.GetParent(path).ToString();
            if (!Directory.Exists(path))
            {
                Messenger.AddDebug("Creating config directory: " + path);
                Directory.CreateDirectory(path);
            }
        }
        
        /// <summary>
        /// Loads the AppConfig and KSPConfig.
        /// </summary>
        protected static void LoadConfigs()
        {
            Messenger.AddInfo(Messages.MSG_LOADING_KSPMA_SETTINGS);
            string path = KSPPathHelper.GetPath(KSPPaths.AppConfig);
            if (File.Exists(path))
            {
                if (AdminConfig.Load(path))
                {
                    Messenger.AddInfo(Messages.MSG_DONE);

                    // LoadKSPConfig will be started by KSPPathChange event.
                    ////if (KSPPathHelper.IsKSPInstallFolder(OptionsController.SelectedKSPPath))
                    ////    LoadKSPConfig();
                }
                else
                {
                    Messenger.AddInfo(Messages.MSG_LOADING_KSPMA_SETTINGS_FAILED);
                }
            }
            else
            {
                Messenger.AddInfo(Messages.MSG_KSPMA_SETTINGS_NOT_FOUND);
            }

            DeleteOldAppConfigs();
        }

        /// <summary>
        /// Loads the KSPConfig from the selected KSP folder.
        /// </summary>
        protected static void LoadKSPConfig()
        {
            ModSelectionController.ClearMods();

            string configPath = KSPPathHelper.GetPath(KSPPaths.KSPConfig);
            if (File.Exists(configPath))
            {
                Messenger.AddInfo(Messages.MSG_LOADING_KSP_MOD_CONFIGURATION);
                List<ModNode> mods = new List<ModNode>();
                KSPConfig.Load(configPath, ref mods);
                ModSelectionController.AddMods(mods.ToArray());
                ModSelectionController.SortModSelection();
            }
            else
            {
                Messenger.AddInfo(Messages.MSG_KSP_MOD_CONFIGURATION_NOT_FOUND);
            }

            ModSelectionController.RefreshCheckedStateAllMods();
            Messenger.AddInfo(Messages.MSG_DONE);
        }

        /// <summary>
        /// Deletes older config paths and files.
        /// </summary>
        protected static void DeleteOldAppConfigs()
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
        protected static void SaveAppConfig()
        {
            try
            {
                Messenger.AddInfo(Messages.MSG_SAVING_KSPMA_SETTINGS);
                string path = KSPPathHelper.GetPath(KSPPaths.AppConfig);
                if (path != string.Empty && Directory.Exists(Path.GetDirectoryName(path)))
                    AdminConfig.Save(path);
                else
                    Messenger.AddError(Messages.MSG_KSPMA_SETTINGS_PATH_INVALID);
            }
            catch (Exception ex)
            {
                Messenger.AddError(Messages.MSG_ERROR_DURING_SAVING_KSPMA_SETTINGS, ex);
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
                    Messenger.AddInfo(Messages.MSG_SAVING_KSP_MOD_SETTINGS);
                    KSPConfig.Save(path, ModSelectionController.Mods);
                }
                else
                    Messenger.AddError(Messages.MSG_KSP_MOD_SETTINGS_PATH_INVALID);
            }
            catch (Exception ex)
            {
                Messenger.AddError(Messages.MSG_ERROR_DURING_SAVING_KSP_MOD_SETTINGS, ex);
                ShowAdminRightsDlg(ex);
            }
        }

        /// <summary>
        /// Shows a MessageBox with the info, that KSP MA needs admin rights if KSP is installed to c:\{ProgramFiles}\...
        /// </summary>
        /// <param name="ex">The message of the Exception will be displayed too.</param>
        protected static void ShowAdminRightsDlg(Exception ex)
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
        protected static void LoadSiteHandler()
        {
            // Add default SiteHandler
            var siteHandlers = PluginLoader.GetPlugins<ISiteHandler>(new[] { Assembly.GetExecutingAssembly() });
            foreach (ISiteHandler handler in siteHandlers)
                SiteHandlerManager.RegisterSiteHandler(handler);

            // Add additional SiteHandlers
            siteHandlers = PluginLoader.LoadPlugins<ISiteHandler>(KSPPathHelper.GetPath(KSPPaths.KSPMA_Plugins));
            foreach (ISiteHandler handler in siteHandlers)
                SiteHandlerManager.RegisterSiteHandler(handler);
        }

        /// <summary>
        /// Loads all Plugins for KSP Mod Admin.
        /// </summary>
        protected static void LoadPlugins()
        {
            Messenger.AddDebug("Loading plugins ...");
            List<IKSPMAPlugin> plugins = null;
            try
            {
                plugins = PluginLoader.LoadPlugins<IKSPMAPlugin>(KSPPathHelper.GetPath(KSPPaths.KSPMA_Plugins));
            }
            catch (Exception ex)
            {
                Messenger.AddError("Error during plugin loading! Plugin loading aborded!", ex);
            }

            if (plugins == null)
                return;

            foreach (IKSPMAPlugin plugin in plugins)
            {
                try
                {
                    Messenger.AddDebug(string.Format("Try add plugin \"{0}\" ...", plugin.Name));

                    TabView[] tabViews = plugin.MainTabViews;
                    foreach (TabView tabView in tabViews)
                    {
                        if (!mAddedTabViews.ContainsKey(tabView.TabUserControl.GetTabCaption()))
                        {
                            Log.AddDebugS(string.Format("Try add TabPage \"{0}\" ...", tabView.TabUserControl.GetTabCaption()));

                            TabPage tabPage = new TabPage();
                            tabPage.Tag = tabView.UniqueIdentifier.ToString();
                            tabPage.Text = tabView.TabUserControl.GetTabCaption();
                            tabPage.Controls.Add(tabView.TabUserControl);
                            tabView.TabUserControl.Dock = DockStyle.Fill;
                            if (tabView.TabIcon != null)
                            {
                                View.TabControl.ImageList.Images.Add(tabView.TabIcon);
                                tabPage.ImageIndex = View.TabControl.ImageList.Images.Count - 1;
                            }
                            View.TabControl.TabPages.Add(tabPage);

                            mAddedTabViews.Add(tabView.TabUserControl.GetTabCaption(), tabView);
                        }
                        else
                        {
                            Messenger.AddError(string.Format(Messages.MSG_ERROR_PLUGIN_LOADING_TABVIEWS_0,
                                tabView.TabUserControl.GetTabCaption()));
                        }
                    }

                    tabViews = plugin.OptionTabViews;
                    if (tabViews == null)
                        continue;

                    foreach (TabView tabView in tabViews)
                    {
                        if (!mAddedTabViews.ContainsKey(tabView.TabUserControl.GetTabCaption()))
                        {
                            Log.AddDebugS(string.Format("Try add Options TabPage \"{0}\" ...", tabView.TabUserControl.GetTabCaption()));

                            TabPage tabPage = new TabPage();
                            tabPage.Text = tabView.TabUserControl.GetTabCaption();
                            tabPage.Controls.Add(tabView.TabUserControl);
                            tabView.TabUserControl.Dock = DockStyle.Fill;
                            OptionsController.View.TabControl.TabPages.Add(tabPage);

                            mAddedTabViews.Add(tabView.TabUserControl.GetTabCaption(), tabView);
                        }
                        else
                        {
                            Messenger.AddError(string.Format(Messages.MSG_ERROR_PLUGIN_LOADING_OPTIONVIEWS_0,
                                tabView.TabUserControl.GetTabCaption()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Messenger.AddError(string.Format("Error during loading of plugin \"{0}\"", plugin.Name), ex);
                }
            }
        }
        private static Dictionary<string, TabView> mAddedTabViews = new Dictionary<string, TabView>();

        #region EventDistributor callback functions.

        /// <summary>
        /// Callback function for the AsyncTaskStarted event.
        /// Should disable all controls of the BaseView.
        /// </summary>
        protected static void AsyncTaskDone(object sender)
        {
            View.cbKSPPath.Enabled = true;
        }

        /// <summary>
        /// Callback function for the AsyncTaskDone event.
        /// Should enable all controls of the BaseView.
        /// </summary>
        protected static void AsyncTaskStarted(object sender)
        {
            View.cbKSPPath.Enabled = false;
        }

        /// <summary>
        /// Callback function for the LanguageChanged event.
        /// Translates all controls of the BaseView.
        /// </summary>
        protected static void LanguageChanged(object sender)
        {
            // translates the controls of the view.
            ControlTranslator.TranslateControls(Localizer.GlobalInstance, View as Control, OptionsController.SelectedLanguage);

            foreach (TabView addedTabView in mAddedTabViews.Values)
            {
                TabPage tabPage = addedTabView.TabUserControl.Parent as TabPage;
                if (tabPage != null)
                    tabPage.Text = addedTabView.TabUserControl.GetTabCaption();
            }
        }

        /// <summary>
        /// Event handler for the KSPPathChanging event from OptionsController.
        /// </summary>
        /// <param name="oldKSPPath">The last selected KSP path.</param>
        /// <param name="newKSPPath">The new selected ksp path.</param>
        protected static void KSPRootChanging(string oldKSPPath, string newKSPPath)
        {
            if (!string.IsNullOrEmpty(oldKSPPath))
                SaveKSPConfig();
        }

        /// <summary>
        /// Event handler for the KSPPathChanged event from OptionsController.
        /// Loads the new KSPConfig from the new ksp path.
        /// </summary>
        /// <param name="kspPath">the new selected ksp path.</param>
        protected static void KSPRootChanged(string kspPath)
        {
            if (!string.IsNullOrEmpty(kspPath))
                LoadKSPConfig();

            _SelectedKSPPath = kspPath;
        }

        /// <summary>
        /// Event handler for the KSPMAStarted event.
        /// Starts the app and mod update checks.
        /// </summary>
        protected static void KSPMAStarted(object sender)
        {
            EventDistributor.InvokeAsyncTaskStarted(Instance);
            ModSelectionController.View.ShowBusy = true;

            AsyncTask<bool>.DoWork(() =>
                {
                    // Auto KSP MA update check.
                    OptionsController.Check4AppUpdates();

                    // Auto mod update check.
                    OptionsController.Check4ModUpdates(true);

                    return true;
                },
                (result, ex) =>
                {
                    EventDistributor.InvokeAsyncTaskDone(Instance);
                    ModSelectionController.View.ShowBusy = false;
                    if (ex != null)
                        Messenger.AddError(string.Format("Error during startup update checks! {0}", ex.Message), ex);
                }
            );
        }

        #endregion
    }
}
