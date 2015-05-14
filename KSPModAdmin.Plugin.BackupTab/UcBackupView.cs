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

namespace KSPModAdmin.Plugin.BackupTab
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class UcBackupView : ucBase
    {
        #region Properties

        public BackupTreeModel Model
        {
            get { return tvBackups.Model as BackupTreeModel; }
            set { tvBackups.Model = value; }
        }

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

        public BackupNode SelectedBackup
        {
            get { return tvBackups.SelectedNode != null ? tvBackups.SelectedNode.Tag as BackupNode : null; }
        }

        private bool HasValidBackupPath { get; set; }

        private bool ShowSelectPathLabel
        {
            get { return pnlSelectBackupPath.Visible; } 
            set { pnlSelectBackupPath.Visible = value; }
        }

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

        private void btnRecoverBackup_Click(object sender, EventArgs e)
        {
            UcBackupViewController.RecoverSelectedBackup();
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
            btnOpenBackupDir.Enabled = HasValidBackupPath;
            tsbNewBackup.Enabled = HasValidBackupPath;
            tsbBackupSaves.Enabled = HasValidBackupPath;
            btnRecoverBackup.Enabled = (selBackup != null) && HasValidBackupPath;
            tsbRemoveBackup.Enabled = (selBackup != null) && HasValidBackupPath;
            tsbRemoveAllBackups.Enabled = HasValidBackupPath;
        }

    }


    public class BackupDataNode : Node
    {
        public string Name { get; private set; }

        public string Note { get; private set; }
    }
}
