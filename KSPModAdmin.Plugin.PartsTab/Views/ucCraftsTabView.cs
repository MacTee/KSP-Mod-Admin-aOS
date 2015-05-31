using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.Helper;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.NodeControls;
using KSPModAdmin.Core.Utils.Localization;
using KSPModAdmin.Core.Views;
using KSPModAdmin.Plugin.PartsAndCraftsTab.Controller;
using KSPModAdmin.Plugin.PartsAndCraftsTab.Model;
using KSPModAdmin.Plugin.PartsAndCraftsTab.Properties;

namespace KSPModAdmin.Plugin.PartsAndCraftsTab.Views
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class ucCraftsTabView : ucBase
    {
        #region Properties
        
        /// <summary>
        /// The Model of the TreeViewAdv for the Backups.
        /// </summary>
        public CraftsTreeModel Model
        {
            get { return tvCrafts.Model as CraftsTreeModel; }
            set { tvCrafts.Model = value; }
        }

        /// <summary>
        /// Gets the selected CraftNode.
        /// </summary>
        public CraftNode SelectedCraft
        {
            get { return tvCrafts.SelectedNode != null ? tvCrafts.SelectedNode.Tag as CraftNode : null; }
        }

        /// <summary>
        /// Gets or sets the selected building filter.
        /// </summary>
        public string SelectedBuildingFilter
        {
            get { return cbCraftsTabBuildingFilter.SelectedItem != null ? cbCraftsTabBuildingFilter.SelectedItem as string : PartsTabViewController.All; }
            set { cbCraftsTabBuildingFilter.SelectedItem = value; }
        }

        /// <summary>
        /// Gets or sets the flag to determine if the processing icon should be shown or not.
        /// </summary>
        public bool ShowProcessingIcon
        {
            get { return tslCraftsTabProcessing.Visible; }
            set
            {
                tslCraftsTabProcessing.Visible = value;
                SetEnabledOfAllControls(!value);
            }
        }

        /// <summary>
        /// Gets or sets the text of the craft count label (lblCraftsTabCount).
        /// </summary>
        public string CraftCountText
        {
            get { return lblCraftsTabCount.Text; }
            set { lblCraftsTabCount.Text = value; }
        }

        /// <summary>
        /// Gets the Column definition for the TreeViewAdv control.
        /// </summary>
        private List<ColumnData> Columns
        {
            get
            {
                List<ColumnData> columns = new List<ColumnData>()
                {
                    new ColumnData()
                    {
                        Name = "Name",
                        Header = Localizer.GlobalInstance["UcCraftsTabView_Item_00"], // "Craft/Part",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 200,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Name",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = "Type",
                        Header = Localizer.GlobalInstance["UcCraftsTabView_Item_01"], // "Type",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 180,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Type",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3,
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = "Folder",
                        Header = Localizer.GlobalInstance["UcCraftsTabView_Item_02"], // "Folder",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 90,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Folder",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = "Version",
                        Header = Localizer.GlobalInstance["UcCraftsTabView_Item_03"], // "Version",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 200,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Version",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = "Mods",
                        Header = Localizer.GlobalInstance["UcCraftsTabView_Item_04"], // "Mods",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 200,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Mods",
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
        public ucCraftsTabView()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            CraftsTabViewController.Initialize(this);
            SetEnabledOfAllControls(true);
        }

        #region Event handling

        private void ucPluginView_Load(object sender, EventArgs e)
        {
            // do View related init here or in the PluginViewController.Initialize(...) methode.
            TreeViewAdvColumnHelper.ColumnsToTreeViewAdv(tvCrafts, Columns);
            cbCraftsTabBuildingFilter.SelectedIndex = 0;
        }

        private void tsbCraftsTabRefresh_Click(object sender, EventArgs e)
        {
            CraftsTabViewController.RefreshCraftsTab();
        }

        private void tsbCraftsTabValidate_Click(object sender, EventArgs e)
        {
            CraftsTabViewController.ValidateCrafts();
        }

        private void tsbCraftsTabRename_Click(object sender, EventArgs e)
        {
            CraftsTabViewController.RenameSelectedCraft();
        }

        private void tsbCraftsTabSwap_Click(object sender, EventArgs e)
        {
            CraftsTabViewController.SwapBuildingOfSelectedCraft();
        }

        private void tsbCraftsTabRemove_Click(object sender, EventArgs e)
        {
            CraftsTabViewController.RemoveSelectedCraft();
        }

        private void cbCraftsTabBuildingFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            CraftsTabViewController.RefreshTreeView();
        }

        private void cmsCraftsTab_Opening(object sender, CancelEventArgs e)
        {
            var sel = SelectedCraft;
            tsmiCraftsTabRefresh.Enabled = true;
            tsmiCraftsTabRemoveCraft.Enabled = sel != null;
            tsmiCraftsTabRenameCraft.Enabled = sel != null;
            tsmiCraftsTabSwapBuildings.Enabled = sel != null;
            tsmiCraftsTabValidateCrafts.Enabled = true;
        }
        
        private void tvCrafts_DrawControl(object o, DrawEventArgs e)
        {
            CraftNode node = (CraftNode)e.Node.Tag;
            if (e.Text != node.Name)
                return;

            e.TextColor = Color.Black;

            if (node.IsInvalidOrHasInvalidChilds)
                e.TextColor = Color.FromArgb(255, 0, 0);
        }

        #endregion

        internal void LanguageChanged()
        {
            // translates the controls of the view.
            ControlTranslator.TranslateControls(Localizer.GlobalInstance, this as Control, OptionsController.SelectedLanguage);

            // translate columns of ModSelection TreeView
            foreach (NamedTreeColumn column in tvCrafts.Columns)
            {
                var newColData = TreeViewAdvColumnHelper.GetColumn(Columns, column.Name);
                if (newColData != null)
                    column.Header = newColData.Header;
            }
        }

        /// <summary>
        /// Add a ActionKey CallbackFunction binding to the backup TreeViewAdv.
        /// </summary>
        /// <param name="key">The action key that raises the callback.</param>
        /// <param name="callback">The callback function with the action that should be called.</param>
        /// <param name="modifierKeys">Required state of the modifier keys to get the callback function called.</param>
        /// <param name="once">Flag to determine if the callback function should only be called once.</param>
        public void AddActionKey(VirtualKey key, ActionKeyHandler callback, ModifierKey[] modifierKeys = null, bool once = false)
        {
            tvCrafts.AddActionKey(key, callback, modifierKeys, once);
        }

        /// <summary>
        /// Forces the view to redraw.
        /// </summary>
        public override void InvalidateView()
        {
            Invalidate();
            Update();
            Refresh();
        }

        /// <summary>
        /// Gets the Name for the parent TabPage.
        /// </summary>
        /// <returns>The Name for the parent TabPage.</returns>
        public override string GetTabCaption()
        {
            return Messages.MSG_CRAFTSTAB_VIEW_TITLE;
        }

        /// <summary>
        /// Sets the enabled state of some view controls.
        /// </summary>
        public void SetEnabledOfAllControls(bool enable)
        {
            // Enable/Disable your View Controls here.
            // Normally when KSP MA calls this methode with enable = false, all controls should be disabled.
            ////this.Enabled = enable;
            if (!enable)
            {
                tsbCraftsTabRefresh.Enabled = enable;
                tsbCraftsTabRemove.Enabled = enable;
                tsbCraftsTabRename.Enabled = enable;
                tsbCraftsTabSwap.Enabled = enable;
                tsbCraftsTabValidate.Enabled = enable;
                cbCraftsTabBuildingFilter.Enabled = enable;
                tvCrafts.Enabled = enable;
            }
            else
            {
                var sel = SelectedCraft;
                tsbCraftsTabRefresh.Enabled = true;
                tsbCraftsTabRemove.Enabled = sel != null;
                tsbCraftsTabRename.Enabled = sel != null;
                tsbCraftsTabSwap.Enabled = sel != null;
                tsbCraftsTabValidate.Enabled = true;
                cbCraftsTabBuildingFilter.Enabled = true;
                tvCrafts.Enabled = true;
            }
        }
    }
}
