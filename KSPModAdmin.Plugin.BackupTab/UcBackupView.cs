using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Forms;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.Helper;
using KSPModAdmin.Core.Utils.Localization;
using KSPModAdmin.Core.Views;
using KSPModAdmin.Plugin.BackupTab.Properties;

namespace KSPModAdmin.Plugin.BackupTab
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class UcBackupView : ucBase
    {
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
            UcBackupViewController.LoadBackupSettings();
            UcBackupViewController.ScanBackupDirectory();
        }

        private void tsbBackupOptions_CheckStateChanged(object sender, EventArgs e)
        {
            tsbBackupOptions.Image = tsbBackupOptions.CheckState == CheckState.Checked ? Resources.gear_new : Resources.gear;
            ShowOptions = tsbBackupOptions.CheckState == CheckState.Checked;
        }

        private void tvBackups_SelectionChanged(object sender, EventArgs e)
        {
            UpdateEnabldeState();
        }

        #endregion

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
        /// Sets the enabled state of some view controls.
        /// </summary>
        public void SetEnabledOfAllControls(bool enable)
        {
            // Enable/Disable your View Controls here.
            // Normally when KSP MA calls this methode with enable = false, all controls should be disabled.
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
        }

        private void cbAutoBackup_CheckedChanged(object sender, EventArgs e)
        {
            if (tsbAutoBackup.Checked != cbAutoBackup.Checked)
                tsbAutoBackup.Checked = cbAutoBackup.Checked;
        }

        private void cbBackupOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (tsbBackupOnStartup.Checked != cbBackupOnStartup.Checked)
                tsbBackupOnStartup.Checked = cbBackupOnStartup.Checked;
        }

        private void cbBackupOnKSPLaunch_CheckedChanged(object sender, EventArgs e)
        {
            if (tsbBackupOnKSPLaunch.Checked != cbBackupOnKSPLaunch.Checked)
                tsbBackupOnKSPLaunch.Checked = cbBackupOnKSPLaunch.Checked;
        }

        private void tsbAutoBackup_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoBackup.Checked != tsbAutoBackup.Checked)
                cbAutoBackup.Checked = tsbAutoBackup.Checked;
        }

        private void tsbBackupOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBackupOnStartup.Checked != tsbBackupOnStartup.Checked)
                cbBackupOnStartup.Checked = tsbBackupOnStartup.Checked;
        }

        private void tsbBackupOnKSPLaunch_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBackupOnKSPLaunch.Checked != tsbBackupOnKSPLaunch.Checked)
                cbBackupOnKSPLaunch.Checked = tsbBackupOnKSPLaunch.Checked;
        }
    }
}
