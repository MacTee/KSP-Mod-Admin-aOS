using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Core.Views
{
    public partial class ucOptions : ucBase
    {
        #region Constants

        const string DEST_DETECTED = "DestinationDetected";
        const string DEST_MISSING = "DestinationMissing";
        const string DEST_CONFLICT = "DestinationConflict";
        const string MOD_INSTALLED = "ModInstalled";
        const string MOD_ARCHIVE_MISSING = "ModArchiveMissing";
        const string MOD_OUTDATED = "ModOutdated";
        const string ZERO = "0";

        #endregion

        #region Properties

        #region Update Tab

        [DefaultValue(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public string KSPMAVersion
        {
            get { return lblKSPModAdminVersion.Text.Split(':')[1].Trim(); }
            set { lblKSPModAdminVersion.Text = string.Format(Messages.MSG_CURRENT_VERSION_0, value); }
        }

        /// <summary>
        /// Gets or sets the cbVersionCheck CheckBox.
        /// Determines whether we should check for updates at start up.
        /// </summary>
        [DefaultValue(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool VersionCheck { get { return cbVersionCheck.Checked; } set { cbVersionCheck.Checked = value; } }

        /// <summary>
        /// The action the should be performed after an update download.
        /// </summary>
        [DefaultValue(PostDownloadAction.Ask), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public PostDownloadAction PostDownloadAction
        {
            get
            {
                return (PostDownloadAction)cbPostDownloadAction.SelectedIndex;
            }
            set
            {
                if (value >= PostDownloadAction.Ignore && value <= PostDownloadAction.AutoUpdate)
                    cbPostDownloadAction.SelectedIndex = (int)value;
                else
                    cbPostDownloadAction.SelectedIndex = (int)PostDownloadAction.Ask;
            }
        }

        /// <summary>
        /// The interval of mod updating.
        /// </summary>
        [DefaultValue(ModUpdateInterval.Manualy), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public ModUpdateInterval ModUpdateInterval
        {
            get
            {
                return (ModUpdateInterval)cbModUpdateInterval.SelectedIndex;
            }
            set
            {
                if (value >= ModUpdateInterval.Manualy && value <= ModUpdateInterval.OnceAWeek)
                    cbModUpdateInterval.SelectedIndex = (int)value;
                else
                    cbModUpdateInterval.SelectedIndex = (int)ModUpdateInterval.Manualy;
            }
        }

        /// <summary>
        /// Date of last mod update check
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public DateTime LastModUpdateTry { get; set; }

        /// <summary>
        /// Gets or sets the action that should be performed when the mod will be auto updated.
        /// </summary>
        [DefaultValue(ModUpdateBehavior.RemoveAndAdd), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public ModUpdateBehavior ModUpdateBehavior
        {
            get
            {
                int index = 0;
                InvokeIfRequired(() => index = cbModUpdateBehavior.SelectedIndex);
                return (ModUpdateBehavior)index;
            }
            set
            {
                InvokeIfRequired(() =>
                {
                    if (value >= ModUpdateBehavior.RemoveAndAdd && value <= ModUpdateBehavior.Manualy)
                        cbModUpdateBehavior.SelectedIndex = (int)value;
                    else
                        cbModUpdateBehavior.SelectedIndex = (int)ModUpdateBehavior.RemoveAndAdd;
                });
            }
        }

        /// <summary>
        /// Gets or sets the a flag that determines if a mod archive should be deleted after an update.
        /// </summary>
        [DefaultValue(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool DeleteOldArchivesAfterUpdate { get { return cbDeleteOldArchive.Checked; } set { cbDeleteOldArchive.Checked = value; } }

        /// <summary>
        /// Get or sets the up to date image visibility.
        /// </summary>
        [DefaultValue(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool Up2Date { get { return pbUp2Date.Visible; } set { pbUp2Date.Visible = value; } }

        #endregion

        #region Path Tab

        /// <summary>
        /// Gets or sets the download/mods path.
        /// </summary>
        [DefaultValue(""), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public string DownloadPath { get { return tbDownloadPath.Text; } set { tbDownloadPath.Text = value; } }

        /// <summary>
        /// Gets or sets the selected KSP path.
        /// </summary>
        [DefaultValue(""), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public string SelectedKSPPath
        {
            get
            {
                return mSelectedKSPPath;
            }
            set
            {
                if (cbKSPPath.Items.Count <= 0) 
                    return;

                foreach (string path in cbKSPPath.Items)
                {
                    if (value != path)
                        continue;

                    cbKSPPath.SelectedItem = path;
                    mSelectedKSPPath = value;
                    break;
                }

                foreach (NoteNode path in tvKnownPaths.Nodes)
                {
                    if (path.Text == value)
                    {
                        tvKnownPaths.SelectedNode = path;
                        tbNote.Text = path.Note;
                    }
                }

                btnDownloadPath.Enabled = KSPPathHelper.IsKSPInstallFolder(value);
            }
        }
        private string mSelectedKSPPath = string.Empty;

        /// <summary>
        /// Gets or sets the selected KSP path.
        /// </summary>
        [DefaultValue(null), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public NoteNode SelectedKnownKSPPath
        {
            get
            {
                return (tvKnownPaths.Nodes.Count > 0) ? (NoteNode)tvKnownPaths.SelectedNode : null;
            }
            set
            {
                if (tvKnownPaths.Nodes.Count <= 0)
                    return;

                foreach (NoteNode node in tvKnownPaths.Nodes)
                {
                    if (value != node)
                        continue;

                    tvKnownPaths.SelectedNode = node;
                    tbNote.Text = node.Note;
                    break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the known KSP install paths.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<NoteNode> KnownKSPPaths
        {
            get { return tvKnownPaths.Nodes.Cast<NoteNode>().ToList(); }
            set
            {
                tvKnownPaths.Nodes.Clear();
                cbKSPPath.Items.Clear();
                if (value != null)
                {
                    cbKSPPath.SelectedIndexChanged -= cbKSPPath_SelectedIndexChanged;
                    foreach (NoteNode path in value)
                    {
                        tvKnownPaths.Nodes.Add(path);
                        cbKSPPath.Items.Add(path.Name);
                    }
                    cbKSPPath.SelectedIndexChanged += cbKSPPath_SelectedIndexChanged;
                }
            }
        }

        /// <summary>
        /// Gets or sets the search depth.
        /// </summary>
        [DefaultValue(3), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public int SearchDepth { get { return int.Parse(tbDepth.Text); } set { tbDepth.Text = value.ToString(); } }

        #endregion

        #region Misc
        
        #region Destination detection

        /// <summary>
        /// Gets or sets the destination detection type.
        /// </summary>
        [DefaultValue(DestinationDetectionType.SmartDetection), Browsable(false),
         EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public DestinationDetectionType DestinationDetectionType
        {
            get
            {
                if (rbDDSmartDestDetection.Checked)
                    return DestinationDetectionType.SmartDetection;
                else if (rbDDJustDump.Checked)
                    return DestinationDetectionType.SimpleDump;

                return DestinationDetectionType.SmartDetection;

            }
            set
            {
                switch (value)
                {
                    case DestinationDetectionType.SmartDetection:
                        rbDDSmartDestDetection.Checked = true;
                        break;
                    case DestinationDetectionType.SimpleDump:
                        rbDDJustDump.Checked = true;
                        break;
                    default:
                        rbDDSmartDestDetection.Checked = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the flag to determine of mod archives should be copied to GameData folder, if no destination was detected..
        /// </summary>
        [DefaultValue(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool CopyToGameData { get { return cbDDCopyToGameData.Checked; } set { cbDDCopyToGameData.Checked = value; } }

        #endregion

        #region ToolTip

        /// <summary>
        /// Gets or sets the flag to determine if the ToolTip should be activated or not.
        /// </summary>
        [DefaultValue(true), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool ToolTipOnOff { get { return cbToolTipOnOff.Checked; } set { cbToolTipOnOff.Checked = value; } }

        /// <summary>
        /// Gets or sets the delay time after a ToolTip should be displayed.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public decimal ToolTipDelay { get { return tbToolTipDelay.DecimalValue; } set { tbToolTipDelay.Text = value.ToString("0.00"); } }

        /// <summary>
        /// Gets or sets the display time of the ToolTips.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public decimal ToolTipDisplayTime { get { return tbToolTipDisplayTime.DecimalValue; } set { tbToolTipDisplayTime.Text = value.ToString("0.00"); } }

        #endregion

        /// <summary>
        /// Gets or sets the flag to determine if the Conflict detection should be turned on or off.
        /// </summary>
        [DefaultValue(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool ConflictDetectionOnOff { get { return cbConflictDetectionOnOff.Checked; } set { cbConflictDetectionOnOff.Checked = value; } }

        /// <summary>
        /// Gets or sets the flag to determine if the ConflictSolver dialog should be shown or not.
        /// </summary>
        [DefaultValue(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool ShowConflictSolver { get { return cbShowConflictSolver.Checked; } set { cbShowConflictSolver.Checked = value; } }

        #region Colors

        /// <summary>
        /// Gets or sets the color for TreeNodes where a destination was found.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public Color ColorDestinationDetected
        {
            get
            {
                return pDestinationDetected.BackColor;
            }
            set
            {
                pDestinationDetected.BackColor = value;
                tbDestinationDetectedRed.Text = value.R.ToString();
                tbDestinationDetectedGreen.Text = value.G.ToString();
                tbDestinationDetectedBlue.Text = value.B.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the color for TreeNodes where a destination is missing.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public Color ColorDestinationMissing
        {
            get
            {
                return pDestinationMissing.BackColor;
            }
            set
            {
                pDestinationMissing.BackColor = value;
                tbDestinationMissingRed.Text = value.R.ToString();
                tbDestinationMissingGreen.Text = value.G.ToString();
                tbDestinationMissingBlue.Text = value.B.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the color for TreeNodes where the mod has conflicts with other mods.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public Color ColorDestinationConflict
        {
            get
            {
                return pDestinationConflict.BackColor;
            }
            set
            {
                pDestinationConflict.BackColor = value;
                tbDestinationConflictRed.Text = value.R.ToString();
                tbDestinationConflictGreen.Text = value.G.ToString();
                tbDestinationConflictBlue.Text = value.B.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the color for TreeNodes where is installed.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public Color ColorModInstalled
        {
            get
            {
                return pModInstalled.BackColor;
            }
            set
            {
                pModInstalled.BackColor = value;
                tbModInstalledRed.Text = value.R.ToString();
                tbModInstalledGreen.Text = value.G.ToString();
                tbModInstalledBlue.Text = value.B.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the color for TreeNodes where mod archive missing.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public Color ColorModArchiveMissing
        {
            get
            {
                return pModArchiveMissing.BackColor;
            }
            set
            {
                pModArchiveMissing.BackColor = value;
                tbModArchiveMissingRed.Text = value.R.ToString();
                tbModArchiveMissingGreen.Text = value.G.ToString();
                tbModArchiveMissingBlue.Text = value.B.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the color for TreeNodes with outdated mods.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public Color ColorModOutdated
        {
            get
            {
                return pModOutdated.BackColor;
            }
            set
            {
                pModOutdated.BackColor = value;
                tbModOutdatedRed.Text = value.R.ToString();
                tbModOutdatedGreen.Text = value.G.ToString();
                tbModOutdatedBlue.Text = value.B.ToString();
            }
        }

        #endregion

        #endregion

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public List<Language> AvailableLanguages
        {
            get { return cbLanguages.Items.Cast<Language>().ToList(); }
            set
            {
                cbLanguages.Items.Clear(); 
                if (value != null) 
                    cbLanguages.Items.AddRange(value.ToArray());
            }
        }

        /// <summary>
        /// Gets or sets the selected language.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public string SelectedLanguage
        {
            get
            {
                return (cbLanguages.SelectedItem == null) ? string.Empty : ((Language)cbLanguages.SelectedItem).Name;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    Localizer.GlobalInstance.CurrentLanguage = value;

                foreach (var item in cbLanguages.Items)
                {
                    if (((Language)item).Name == Localizer.GlobalInstance.CurrentLanguage)
                    {
                        cbLanguages.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the OptionsTab control.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public TabControl TabControl { get { return tabControl2; } }

        #endregion


        /// <summary>
        /// Creates a new instance of the ucOptions class.
        /// </summary>
        public ucOptions()
        {
            InitializeComponent();

            cbPostDownloadAction.SelectedIndex = 1;
            cbModUpdateInterval.SelectedIndex = 0;
            cbModUpdateBehavior.SelectedIndex = 2;

            ToolTipDelay = 0.5m;
            ToolTipDisplayTime = 10m;

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            OptionsController.Initialize(this);
        }


        /// <summary>
        /// Sets the selected KSP path without raising event SelectedIndexChanged.
        /// </summary>
        /// <param name="kspPath">The new selected KSP path.</param>
        internal void SilentSetSelectedKSPPath(string kspPath)
        {
            cbKSPPath.SelectedIndexChanged -= cbKSPPath_SelectedIndexChanged;
            SelectedKSPPath = null;
            SelectedKSPPath = kspPath;
            mSelectedKSPPath = kspPath;
            cbKSPPath.SelectedIndexChanged += cbKSPPath_SelectedIndexChanged;
        }


        #region Update Tab events

        /// <summary>
        /// Handles the Click event of the btnOpenDownloads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenDownloads_Click(object sender, EventArgs e)
        {
            OptionsController.OpenDownloadFolder();
        }

        /// <summary>
        /// Handles the Click event of the btnUpdate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            OptionsController.Check4AppUpdates();
        }

        /// <summary>
        /// Handles the Click event of the btnCheckModUpdates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckModUpdates_Click(object sender, EventArgs e)
        {
            OptionsController.Check4ModUpdates(ModSelectionController.Mods.ToArray());
        }

        #endregion

        #region Path Tab events

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbKSPPath.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbKSPPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            OptionsController.SelectedKSPPath = cbKSPPath.SelectedItem.ToString();
        }

        /// <summary>
        /// Handles the Click event of the btnOpenKSPRoot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenKSPRoot_Click(object sender, EventArgs e)
        {
            OptionsController.OpenKSPRoot();
        }

        /// <summary>
        /// Handles the Click event of the btnOpenDownloadFolder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenDownloadFolder_Click(object sender, EventArgs e)
        {
            OptionsController.OpenDownloadFolder();
        }

        /// <summary>
        /// Handles the Click event of the btnDownloadPath.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownloadPath_Click(object sender, EventArgs e)
        {
            OptionsController.SelectNewDownloadPath();
        }

        /// <summary>
        /// Handles the Click event of the btnAddPath.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPath_Click(object sender, EventArgs e)
        {
            OptionsController.AddKSPPath();
        }

        /// <summary>
        /// Handles the Click event of the btnRemove.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            OptionsController.RemoveKSPPath();
        }

        /// <summary>
        /// Handles the Click event of the btnSteamSearch.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSteamSearch_Click(object sender, EventArgs e)
        {
            OptionsController.SteamSearch4KSPPath();
        }

        /// <summary>
        /// Handles the Click event of the btnFolderSearch.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFolderSearch_Click(object sender, EventArgs e)
        {
            OptionsController.FolderSearch4KSPPath();
        }

        /// <summary>
        /// Handles the AfterSelect event of the tvKnownPaths.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvKnownPaths_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvKnownPaths.SelectedNode != null)
            {
                NoteNode node = (NoteNode)tvKnownPaths.SelectedNode;
                tbNote.Text = node.Note;
                tbNote.Enabled = true;
            }
            else
            {
                tbNote.Text = string.Empty;
                tbNote.Enabled = false;
            }
        }
        
        /// <summary>
        /// Handles the TextChanged event of the tbNote.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbNote_TextChanged(object sender, EventArgs e)
        {
            if (tvKnownPaths.SelectedNode != null)
            {
                NoteNode node = (NoteNode)tvKnownPaths.SelectedNode;
                node.Note = tbNote.Text;
            }
        }

        #endregion

        #region Misc Tab events

        private void rbDDSmartDestDetection_CheckedChanged(object sender, EventArgs e)
        {
            cbDDCopyToGameData.Enabled = rbDDSmartDestDetection.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the cbToolTipOnOff.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbToolTipOnOff_CheckedChanged(object sender, EventArgs e)
        {
            OptionsController.ToolTipOnOff = cbToolTipOnOff.Checked;
        }

        /// <summary>
        /// Handles the TextChanged event of the tbToolTipDelay.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbToolTipDelay_TextChanged(object sender, EventArgs e)
        {
            OptionsController.ToolTipDelay = tbToolTipDelay.DecimalValue;
        }

        /// <summary>
        /// Handles the TextChanged event of the tbToolTipDisplayTime.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbToolTipDisplayTime_TextChanged(object sender, EventArgs e)
        {
            OptionsController.ToolTipDisplayTime = tbToolTipDisplayTime.DecimalValue;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the cbConflictDetectionOnOff.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbConflictDetectionOnOff_CheckedChanged(object sender, EventArgs e)
        {
            OptionsController.ConflictDetectionOnOff = cbConflictDetectionOnOff.Checked;
        }

        /// <summary>
        /// Handles the Click event of the color change buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorChangeButton_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (((Button)sender).Name.Contains(DEST_DETECTED))
            {
                dlg.Color = pDestinationDetected.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    pDestinationDetected.BackColor = dlg.Color;
            }
            else if (((Button)sender).Name.Contains(DEST_MISSING))
            {
                dlg.Color = pDestinationMissing.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    pDestinationMissing.BackColor = dlg.Color;
            }
            else if (((Button)sender).Name.Contains(DEST_CONFLICT))
            {
                dlg.Color = pDestinationConflict.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    pDestinationConflict.BackColor = dlg.Color;
            }
            else if (((Button)sender).Name.Contains(MOD_INSTALLED))
            {
                dlg.Color = pModInstalled.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    pModInstalled.BackColor = dlg.Color;
            }
            else if (((Button)sender).Name.Contains(MOD_ARCHIVE_MISSING))
            {
                dlg.Color = pModArchiveMissing.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    pModArchiveMissing.BackColor = dlg.Color;
            }
            else if (((Button)sender).Name.Contains(MOD_OUTDATED))
            {
                dlg.Color = pModOutdated.BackColor;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    pModOutdated.BackColor = dlg.Color;
            }

            ModSelectionController.InvalidateView();
        }

        private void ColorTextBoxes_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
                tb.Text = ZERO;

            if (tb.Name.Contains(DEST_DETECTED))
            {
                pDestinationDetected.BackColor = Color.FromArgb(255, int.Parse(tbDestinationDetectedRed.Text),
                    int.Parse(tbDestinationDetectedGreen.Text), int.Parse(tbDestinationDetectedBlue.Text));
            }
            else if (tb.Name.Contains(DEST_MISSING))
            {
                pDestinationMissing.BackColor = Color.FromArgb(255, int.Parse(tbDestinationMissingRed.Text),
                    int.Parse(tbDestinationMissingGreen.Text), int.Parse(tbDestinationMissingBlue.Text));
            }
            else if (tb.Name.Contains(DEST_CONFLICT))
            {
                pDestinationConflict.BackColor = Color.FromArgb(255, int.Parse(tbDestinationConflictRed.Text),
                    int.Parse(tbDestinationConflictGreen.Text), int.Parse(tbDestinationConflictBlue.Text));
            }
            else if (tb.Name.Contains(MOD_INSTALLED))
            {
                pModInstalled.BackColor = Color.FromArgb(255, int.Parse(tbModInstalledRed.Text),
                    int.Parse(tbModInstalledGreen.Text), int.Parse(tbModInstalledBlue.Text));
            }
            else if (tb.Name.Contains(MOD_ARCHIVE_MISSING))
            {
                pModArchiveMissing.BackColor = Color.FromArgb(255, int.Parse(tbModArchiveMissingRed.Text),
                    int.Parse(tbModArchiveMissingGreen.Text), int.Parse(tbModArchiveMissingBlue.Text));
            }
            else if (tb.Name.Contains(MOD_OUTDATED))
            {
                pModOutdated.BackColor = Color.FromArgb(255, int.Parse(tbModOutdatedRed.Text),
                    int.Parse(tbModOutdatedGreen.Text), int.Parse(tbModOutdatedBlue.Text));
            }

            ModSelectionController.InvalidateView();
        }

        #endregion

        private void cbLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lang = (cbLanguages.SelectedItem != null && !string.IsNullOrEmpty(cbLanguages.SelectedItem.ToString())) ? cbLanguages.SelectedItem.ToString() : string.Empty;
            Localizer.GlobalInstance.CurrentLanguage = Localizer.GlobalInstance.GetLanguageNameByLongName(lang);
            EventDistributor.InvokeLanguageChanged(this);
        }
    }
}
