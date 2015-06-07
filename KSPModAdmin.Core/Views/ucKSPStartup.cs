using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Core.Utils;

namespace KSPModAdmin.Core.Views
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class ucKSPStartup : UserControl
    {
        private bool settingsFileFound = false;

        #region Constants

        private const string PARAM_POPUPWINDOW = "-popupwindow";
        private const string PARAM_FORCE_OPENGL = "-force-opengl";
        private const string PARAM_0_EQUALS_1 = "{0} = {1}";
        private const string PARAM_0_X_1 = "{0}x{1}";
        private const string PARAM_SETTINGS_CFG = "settings.cfg";
        private const string EQUALS = "=";
        private const string TRUE = "true";

        #endregion

        #region Properties

        /// <summary>
        /// Flag to determine if KPS should be started with a BorderLess windows.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool BorderlessWin
        {
            get { return cbBorderlessWin.Checked; }
            set { cbBorderlessWin.Checked = value; }
        }

        /// <summary>
        /// Gets or sets the list of available screen resolutions.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public string[] ScreenResolutions
        {
            get { return cbResolutions.Items.Cast<string>().ToArray(); }
            set
            {
                cbResolutions.Items.Clear();
                if (value != null)
                {
                    foreach (string str in value)
                        cbResolutions.Items.Add(str);
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected screen resolution.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public string ScreenResolution
        {
            get
            {
                if (cbResolutions.SelectedItem == null && cbResolutions.Items.Count > 0)
                    cbResolutions.SelectedItem = cbResolutions.Items[0];

                return (string)cbResolutions.SelectedItem;
            }
            set { cbResolutions.SelectedItem = value; }
        }

        /// <summary>
        /// Gets or sets the checked state of the full screen flag.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool Fullscreen { get { return rbFullscreen.Checked; } set { rbFullscreen.Checked = value; rbWindowed.Checked = !value; } }

        /// <summary>
        /// Gets or sets the checked state of the Use64Bit flag.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool Use64Bit { get { return cbUse64Bit.Checked; } set { cbUse64Bit.Checked = value; } }

        /// <summary>
        /// Gets or sets the checked state of the ForceOpenGL flag.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool ForceOpenGL { get { return cbForceOpenGL.Checked; } set { cbForceOpenGL.Checked = value; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the ucKSPStartup class.
        /// </summary>
        public ucKSPStartup()
        {
            InitializeComponent();
        }

        #endregion

        private void ucKSPStartup_Load(object sender, EventArgs e)
        {
            ScreenResolutions = ScreenHelper.GetScreenResolutions();

            EventDistributor.KSPRootChanged += KSPRootChanged;
            EventDistributor.AsyncTaskStarted += TaskStarted;
            EventDistributor.AsyncTaskDone += TaskDone;

            bool validPath = File.Exists(KSPPathHelper.GetPath(KSPPaths.KSPExe));
            if (validPath) 
                ReadKSPSettings();
            SetEnableStates(validPath);
        }

        private void btnLunchKSP_Click(object sender, EventArgs e)
        {
            string fullpath = KSPPathHelper.GetPath(KSPPaths.KSPExe);
            string fullpath64 = KSPPathHelper.GetPath(KSPPaths.KSPX64Exe);
            try
            {
                if (File.Exists(fullpath64) && cbUse64Bit.Checked)
                    fullpath = fullpath64;

                if (File.Exists(fullpath))
                {
                    WriteKSPSettings();

                    EventDistributor.InvokeStartingKSP(this);

                    Messenger.AddInfo(Messages.MSG_STARTING_KSP);
                    System.Diagnostics.Process kspexe = new System.Diagnostics.Process();
#if __MonoCS__
                    kspexe.StartInfo.UseShellExecute = false;
                    kspexe.StartInfo.EnvironmentVariables.Add("LC_ALL", "C");
#endif
                    kspexe.StartInfo.FileName = fullpath;
                    kspexe.StartInfo.WorkingDirectory = Path.GetDirectoryName(fullpath);
                    if (rbWindowed.Checked && cbBorderlessWin.Checked)
                        kspexe.StartInfo.Arguments = PARAM_POPUPWINDOW;
                    if (cbForceOpenGL.Checked)
                        kspexe.StartInfo.Arguments += " " + PARAM_FORCE_OPENGL;

                    kspexe.Start();
                }
                else
                    Messenger.AddError(Messages.MSG_CANT_FIND_KSP_EXE);
            }
            catch (Exception ex)
            {
                Messenger.AddError(Messages.MSG_KSP_LAUNCH_FAILED, ex);
            }
        }

        private void rbFullscreen_CheckedChanged(object sender, EventArgs e)
        {
            cbBorderlessWin.Enabled = !rbFullscreen.Checked;

            if (rbFullscreen.Checked)
                WriteKSPSettings();
        }

        private void rbWindowed_CheckedChanged(object sender, EventArgs e)
        {
            cbBorderlessWin.Enabled = rbWindowed.Checked;

            if (rbFullscreen.Checked)
                WriteKSPSettings();
        }

        private void cbResolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            WriteKSPSettings();
        }

        private void KSPRootChanged(string kspPath)
        {
            SetEnableStates(File.Exists(KSPPathHelper.GetPath(KSPPaths.KSPExe)));
            ReadKSPSettings();
        }

        private void TaskStarted(object sender)
        {
            SetEnableStates(false);
        }

        private void TaskDone(object sender)
        {
            SetEnableStates(File.Exists(KSPPathHelper.GetPath(KSPPaths.KSPExe)));
        }


        private void WriteKSPSettings()
        {
            string settingsPath = Path.Combine(KSPPathHelper.GetPath(KSPPaths.KSPRoot), PARAM_SETTINGS_CFG);
            if (File.Exists(settingsPath))
            {
                if (ScreenResolution == null)
                    return;

                int index1 = -1;
                int index2 = -1;
                string temp = string.Empty;
                string allText = File.ReadAllText(settingsPath);
                string[] size = ScreenResolution.Split("x");
                if (size.Length == 2)
                {
                    index1 = allText.IndexOf(Constants.SCREEN_WIDTH);
                    index2 = allText.IndexOf(Environment.NewLine, index1);
                    temp = allText.Substring(index1, index2 - index1);
                    allText = allText.Replace(temp, string.Format(PARAM_0_EQUALS_1, Constants.SCREEN_WIDTH, size[0]));

                    index1 = allText.IndexOf(Constants.SCREEN_HEIGHT);
                    index2 = allText.IndexOf(Environment.NewLine, index1);
                    temp = allText.Substring(index1, index2 - index1);
                    allText = allText.Replace(temp, string.Format(PARAM_0_EQUALS_1, Constants.SCREEN_HEIGHT, size[1]));
                }

                index1 = allText.IndexOf(Constants.FULLSCREEN);
                index2 = allText.IndexOf(Environment.NewLine, index1);
                temp = allText.Substring(index1, index2 - index1);
                allText = allText.Replace(temp, string.Format(PARAM_0_EQUALS_1, Constants.FULLSCREEN, rbFullscreen.Checked));
                File.WriteAllText(settingsPath, allText);
                Messenger.AddInfo(Messages.MSG_UPDATE_KSP_SETTINGS);

                settingsFileFound = true;
            }
            else
            {
                Messenger.AddInfo(Messages.MSG_CANT_FIND_KSP_SETTINGS);
                settingsFileFound = false;
            }
        }

        private void ReadKSPSettings()
        {
            string path = Path.Combine(KSPPathHelper.GetPath(KSPPaths.KSPRoot), PARAM_SETTINGS_CFG);
            if (File.Exists(path))
            {
                string fileContent = File.ReadAllText(path);
                int index1 = fileContent.IndexOf(Constants.SCREEN_WIDTH) + Constants.SCREEN_WIDTH.Length;
                int index2 = fileContent.IndexOf(Environment.NewLine, index1);
                string width = fileContent.Substring(index1, index2 - index1).Replace(EQUALS, string.Empty).Trim();
                index1 = fileContent.IndexOf(Constants.SCREEN_HEIGHT) + Constants.SCREEN_HEIGHT.Length;
                index2 = fileContent.IndexOf(Environment.NewLine, index1);
                string height = fileContent.Substring(index1, index2 - index1).Replace(EQUALS, string.Empty).Trim();
                ScreenResolution = string.Format(PARAM_0_X_1, width, height);
                index1 = fileContent.IndexOf(Constants.FULLSCREEN) + Constants.FULLSCREEN.Length;
                index2 = fileContent.IndexOf(Environment.NewLine, index1);
                Fullscreen = fileContent.Substring(index1, index2 - index1).Replace(EQUALS, string.Empty).Trim().ToLower() == TRUE;

                settingsFileFound = true;
            }
            else
            {
                Messenger.AddInfo(Messages.MSG_CANT_FIND_KSP_SETTINGS);
                settingsFileFound = false;
            }
        }

        private void SetEnableStates(bool enable)
        {
            tableLayoutPanel2.Enabled = enable && settingsFileFound;
            btnLaunchKSP.Enabled = enable;
            rbFullscreen.Enabled = enable;
            rbWindowed.Enabled = enable;
            cbBorderlessWin.Enabled = (enable && rbWindowed.Checked);
            cbResolutions.Enabled = (enable && cbResolutions.Items != null && cbResolutions.Items.Count > 0);
            cbForceOpenGL.Enabled = (enable && PlatformHelper.GetPlatform() == Platform.Win);
            cbForceOpenGL.Visible = PlatformHelper.GetPlatform() == Platform.Win;
            cbUse64Bit.Enabled = (enable && (PlatformHelper.GetPlatform() == Platform.OsX || PlatformHelper.GetPlatform() == Platform.Linux));
            cbUse64Bit.Visible = (PlatformHelper.GetPlatform() == Platform.OsX || PlatformHelper.GetPlatform() == Platform.Linux);
        }
    }
}
