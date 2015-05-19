using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Forms;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.Helper;
using KSPModAdmin.Core.Utils.Localization;
using KSPModAdmin.Core.Views;
using KSPModAdmin.Plugin.BackupTab.Controller;
using KSPModAdmin.Plugin.BackupTab.Model;
using KSPModAdmin.Plugin.BackupTab.Properties;

namespace KSPModAdmin.Plugin.BackupTab.Views
{
    using KSPModAdmin.Core.Utils;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class UcBackupView : ucBase
    {
        private bool updating = false;

        #region Properties

        /// <summary>
        /// The Model of the TreeViewAdv for the Backups.
        /// </summary>
        public BackupTreeModel Model
        {
            get { return tvBackups.Model as BackupTreeModel; }
            set { tvBackups.Model = value; }
        }

        /// <summary>
        /// The path to save the backups in.
        /// </summary>
        public string BackupPath
        {
            get { return tbBackupPath.Text; }
            set
            {
                if (tbBackupPath.Text != value)
                {
                    tbBackupPath.Text = value;
                    BackupPathChanged();
                }
            }
        }

        /// <summary>
        /// The selected backup of the TreeViewAdv.
        /// </summary>
        public BackupNode SelectedBackup
        {
            get { return tvBackups.SelectedNode != null ? tvBackups.SelectedNode.Tag as BackupNode : null; }
        }

        /// <summary>
        /// Flag to determine if the processing icon should be shown or not.
        /// </summary>
        public bool ShowProcessing
        {
            get { return tslProcessing.Visible; }
            set { tslProcessing.Visible = value; }
        }

        /// <summary>
        /// Toggles the on off state of the auto backup function.
        /// </summary>
        public bool AutoBackup 
        { 
            get { return tsbAutoBackup.Checked; }
            set { tsbAutoBackup.Checked = value; }
        }

        /// <summary>
        /// The interval to do a backup of the save folder (in minutes).
        /// </summary>
        [DefaultValue(60)]
        public int BackupInterval
        {
            get { return tbBackupIterval.IntValue; }
            set { tbBackupIterval.Text = value.ToString(); }
        }

        /// <summary>
        /// Maximum of auto backup files.
        /// If maximum is reached older auto backup will be replaced.
        /// </summary>
        [DefaultValue(5)]
        public int MaxBackupFiles
        {
            get { return tbMaxBackupFiles.IntValue; }
            set { tbMaxBackupFiles.Text = value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the flag to determine if we should make a backup on a launch of KSP.
        /// </summary>
        public bool BackupOnKSPLaunch
        {
            get { return tsbBackupOnKSPLaunch.Checked; }
            set { tsbBackupOnKSPLaunch.Checked = value; }
        }

        /// <summary>
        /// Gets or sets the flag to determine if we should make a backup on a launch of KSP Mod Admin.
        /// </summary>
        public bool BackupOnKSPMALaunch
        {
            get { return tsbBackupOnStartup.Checked; }
            set { tsbBackupOnStartup.Checked = value; }
        }

        private bool ShowSelectPathLabel
        {
            get { return pnlSelectBackupPath.Visible; } 
            set { pnlSelectBackupPath.Visible = value; }
        }

        private bool ShowOptions
        {
            get { return pnlOptions.Visible; }
            set
            {
                pnlOptions.Visible = value;
                UpdateEnabldeState();
            }
        }

        private bool HasValidBackupPath { get; set; }

        private List<ColumnData> Columns
        {
            get
            {
                List<ColumnData> columns = new List<ColumnData>()
                {
                    new ColumnData()
                    {
                        Name = "FileName",
                        Header = Localizer.GlobalInstance["UcBackupView_Item_00"], // "Filename",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 250,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Name",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3,
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = "Note",
                        Header = Localizer.GlobalInstance["UcBackupView_Item_01"], // "Note",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 350,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Note",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    }
                };

                return columns;
            }
        }

        #endregion

        /// <summary>
        /// Creates a new instance of the ucTranslationView class.
        /// </summary>
        public UcBackupView()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            UcBackupViewController.Initialize(this);
        }

        #region Event handling

        private void ucPluginView_Load(object sender, EventArgs e)
        {
            // do View related init here or in the UcBackupViewController.Initialize(...) methode.
            TreeViewAdvColumnHelper.ColumnsToTreeViewAdv(tvBackups, Columns);
            UcBackupViewController.LoadBackupSettings();
            UcBackupViewController.ScanBackupDirectory();
        }

        private void btnBackupPath_Click(object sender, EventArgs e)
        {
            UcBackupViewController.SelectNewBackupPath();
        }

        private void btnOpenBackupDir_Click(object sender, EventArgs e)
        {
            UcBackupViewController.OpenBackupPath();
        }

        private void btnRecoverBackup_Click(object sender, EventArgs e)
        {
            UcBackupViewController.RecoverSelectedBackup();
        }

        private void tsbNewBackup_Click(object sender, EventArgs e)
        {
            UcBackupViewController.NewBackup();
        }

        private void tsbBackupSaves_Click(object sender, EventArgs e)
        {
            UcBackupViewController.BackupSaves();
        }

        private void tsbRemoveBackup_Click(object sender, EventArgs e)
        {
            UcBackupViewController.RemoveSelectedBackup();
        }

        private void tsbRemoveAllBackups_Click(object sender, EventArgs e)
        {
            UcBackupViewController.RemoveAllBackups();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            UcBackupViewController.SaveBackupSettings();
            UcBackupViewController.LoadBackupSettings();
            UcBackupViewController.ScanBackupDirectory();
        }

        private void tsbEditBackupNote_Click(object sender, EventArgs e)
        {
            EditSelectedBackupNote();
        }

        private void tsbBackupOptions_CheckStateChanged(object sender, EventArgs e)
        {
            tsbBackupOptions.Image = tsbBackupOptions.CheckState == CheckState.Checked ? Resources.gear_new : Resources.gear;
            ShowOptions = tsbBackupOptions.CheckState == CheckState.Checked;
        }

        private void tvBackups_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedBackupNote();
        }

        private void tvBackups_SelectionChanged(object sender, EventArgs e)
        {
            UpdateEnabldeState();
        }

        private void AutoBackup_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCheckedState(sender, tsbAutoBackup, cbAutoBackup);

            tsbAutoBackup.Image = tsbAutoBackup.Checked ? Resources.data_gearwheel_new : Resources.data_gearwheel;
            UcBackupViewController.AutoBackupOnOff = cbAutoBackup.Checked;
        }

        private void BackupOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCheckedState(sender, tsbBackupOnStartup, cbBackupOnStartup);

            tsbBackupOnStartup.Image = cbBackupOnStartup.Checked ? Resources.KMA2_new_24x24 : Resources.KMA2_24;
        }

        private void BackupOnKSPLaunch_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCheckedState(sender, tsbBackupOnKSPLaunch, cbBackupOnKSPLaunch);

            tsbBackupOnKSPLaunch.Image = cbBackupOnKSPLaunch.Checked ? Resources.kerbal_new_24x24 : Resources.kerbal_24x24;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!updating)
            {
                if (AutoBackup)
                {
                    // reset backup timer
                    UcBackupViewController.AutoBackupOnOff = false;
                    UcBackupViewController.AutoBackupOnOff = true;
                }
                UcBackupViewController.SaveBackupSettings();
            }
        }

        #endregion

        /// <summary>
        /// Add a ActionKey CallbackFunction binding to the backup TreeViewAdv.
        /// </summary>
        /// <param name="key">The action key that raises the callback.</param>
        /// <param name="callback">The callback function with the action that should be called.</param>
        /// <param name="modifierKeys">Required state of the modifier keys to get the callback function called.</param>
        /// <param name="once">Flag to determine if the callback function should only be called once.</param>
        public void AddActionKey(VirtualKey key, ActionKeyHandler callback, ModifierKey[] modifierKeys = null, bool once = false)
        {
            tvBackups.AddActionKey(key, callback, modifierKeys, once);
        }

        /// <summary>
        /// Forces the view to redraw.
        /// </summary>
        public override void InvalidateView()
        {
            Invalidate();
            Update();
            Refresh();
            UpdateEnabldeState();
        }

        /// <summary>
        /// Gets the Name for the parent TabPage.
        /// </summary>
        /// <returns>The Name for the parent TabPage.</returns>
        public override string GetTabCaption()
        {
            return Messages.MSG_BACKUPTAB_VIEW_TITLE;
        }

        /// <summary>
        /// Starts the update mode.
        /// Value changes to backup options wont invoke a save options.
        /// </summary>
        public void StartUpdate()
        {
            updating = true;
        }

        /// <summary>
        /// Ends the update mode.
        /// Value changes to backup options will invoke a save options again.
        /// </summary>
        public void EndUpdate()
        {
            updating = false;
        }

        /// <summary>
        /// Sets the enabled state of some view controls.
        /// </summary>
        public void SetEnabledOfAllControls(bool enable)
        {
            // Enable/Disable your View Controls here.
            // Normally when KSP MA calls this methode with enable = false, all controls should be disabled.
            this.Enabled = enable;
        }

        internal void LanguageChanged()
        {
            // translates the controls of the view.
            ControlTranslator.TranslateControls(Localizer.GlobalInstance, this as Control, OptionsController.SelectedLanguage);

            // translate columns of ModSelection TreeView
            foreach (NamedTreeColumn column in tvBackups.Columns)
            {
                var newColData = TreeViewAdvColumnHelper.GetColumn(Columns, column.Name);
                if (newColData != null)
                    column.Header = newColData.Header;
            }
        }

        private void BackupPathChanged()
        {
            HasValidBackupPath = false;
            try { HasValidBackupPath = !string.IsNullOrEmpty(BackupPath) && Directory.Exists(BackupPath); }
            catch { }

            UpdateEnabldeState();

            ShowSelectPathLabel = !HasValidBackupPath;

            if (!updating)
                UcBackupViewController.SaveBackupSettings();
        }

        private void UpdateEnabldeState()
        {
            var selBackup = SelectedBackup;
            btnOpenBackupDir.Enabled = HasValidBackupPath && !ShowOptions;
            tsbNewBackup.Enabled = HasValidBackupPath && !ShowOptions;
            tsbBackupSaves.Enabled = HasValidBackupPath && !ShowOptions;
            btnRecoverBackup.Enabled = (selBackup != null) && HasValidBackupPath && !ShowOptions;
            tsbRemoveBackup.Enabled = (selBackup != null) && HasValidBackupPath && !ShowOptions;
            tsbRemoveAllBackups.Enabled = HasValidBackupPath && !ShowOptions;
            tsbRefreshBackupview.Enabled = HasValidBackupPath && !ShowOptions;
            tsbEditBackupNote.Enabled = (selBackup != null) && HasValidBackupPath && !ShowOptions;
        }

        private void UpdateCheckedState(object sender, ToolStripButton tsb, CheckBox cb)
        {
            var value = (sender == tsb) ? tsb.Checked : cb.Checked;
            if (value != cb.Checked)
                cb.Checked = value;
            if (value != tsb.Checked)
                tsb.Checked = value;

            if (!updating)
                UcBackupViewController.SaveBackupSettings();
        }

        private void EditSelectedBackupNote()
        {
            var row = SelectedBackup;
            if (row == null)
                return;

            var dlg = new frmEditNote { BackupName = row.Name, BackupNote = row.Note };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                row.Name = dlg.BackupName;
                row.Note = dlg.BackupNote;
                UcBackupViewController.SaveBackupSettings();
            }
        }
    }
}
