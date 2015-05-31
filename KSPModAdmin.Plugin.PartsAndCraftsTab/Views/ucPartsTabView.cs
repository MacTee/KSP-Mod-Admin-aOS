using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Forms;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.Helper;
using KSPModAdmin.Core.Utils.Localization;
using KSPModAdmin.Core.Views;
using KSPModAdmin.Plugin.PartsAndCraftsTab.Controller;
using KSPModAdmin.Plugin.PartsAndCraftsTab.Model;
using Messages = KSPModAdmin.Plugin.PartsAndCraftsTab.Messages;

namespace KSPModAdmin.Plugin.PartsAndCraftsTab.Views
{
    using System.Linq;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class ucPartsTabView : ucBase
    {
        #region Properties

        /// <summary>
        /// The Model of the TreeViewAdv for the Backups.
        /// </summary>
        public PartsTreeModel Model
        {
            get { return tvParts.Model as PartsTreeModel; }
            set { tvParts.Model = value; }
        }

        /// <summary>
        /// Gets the selected PartNode.
        /// </summary>
        public PartNode SelectedPart
        {
            get { return tvParts.SelectedNode != null ? tvParts.SelectedNode.Tag as PartNode : null; }
        }

        /// <summary>
        /// Gets or sets the flag to determine if the processing icon should be shown or not.
        /// </summary>
        public bool ShowProcessingIcon
        {
            get { return tslPartsProcessing.Visible; }
            set
            {
                tslPartsProcessing.Visible = value;
                SetEnabledOfAllControls(!value);
            }
        }

        /// <summary>
        /// Gets or sets the text of the part count label (lblPartsCount).
        /// </summary>
        public string PartCountText
        { 
            get { return lblPartsCount.Text; }
            set { lblPartsCount.Text = value; }
        }

        /// <summary>
        /// Gets or sets the category filter.
        /// </summary>
        public string[] CategoryFilter
        {
            get
            {
                return cbCategoryFilter.Items.Cast<string>().ToArray();
            }
            set
            {
                cbCategoryFilter.Items.Clear();
                foreach (var entry in value)
                    cbCategoryFilter.Items.Add(entry);
            }
        }

        /// <summary>
        /// Gets or sets the selected category filter.
        /// </summary>
        public string SelectedCategoryFilter
        {
            get { return cbCategoryFilter.SelectedItem != null ? cbCategoryFilter.SelectedItem as string : PartsTabViewController.All; }
            set { cbCategoryFilter.SelectedItem = value; } 
        }

        /// <summary>
        /// Gets or sets the mod filter.
        /// </summary>
        public string[] ModFilter
        {
            get
            {
                return cbModFilter.Items.Cast<string>().ToArray();
            }
            set
            {
                cbModFilter.Items.Clear();
                foreach (string entry in value)
                    cbModFilter.Items.Add(entry);
            }
        }

        /// <summary>
        /// Gets or sets the selected mod filter.
        /// </summary>
        public string SelectedModFilter
        {
            get { return cbModFilter.SelectedItem != null ? cbModFilter.SelectedItem as string : PartsTabViewController.All; }
            set { cbModFilter.SelectedItem = value; } 
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
                        Name = "Title",
                        Header = Localizer.GlobalInstance["UcPartsTabView_Item_00"], // "Title/Craft",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 200,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Title",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = "PartCraft",
                        Header = Localizer.GlobalInstance["UcPartsTabView_Item_01"], // "Part",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 180,
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
                        Name = "Category",
                        Header = Localizer.GlobalInstance["UcPartsTabView_Item_02"], // "Category",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 90,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Category",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = "Mod",
                        Header = Localizer.GlobalInstance["UcPartsTabView_Item_03"], // "Mod",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 200,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Mod",
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
        public ucPartsTabView()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            SelectedCategoryFilter = PartsTabViewController.All;
            SelectedModFilter = PartsTabViewController.All;
            PartsTabViewController.Initialize(this);
        }

        #region Event handling

        private void ucPluginView_Load(object sender, EventArgs e)
        {
            // do View related init here or in the PluginViewController.Initialize(...) methode.
            TreeViewAdvColumnHelper.ColumnsToTreeViewAdv(tvParts, Columns);
        }

        private void tsbPartsRefresh_Click(object sender, EventArgs e)
        {
            PartsTabViewController.RefreshPartsTab();
        }

        private void tsbPartsRemove_Click(object sender, EventArgs e)
        {
            PartsTabViewController.RemoveSelectedPart();
        }

        private void tsbPartsEdit_Click(object sender, EventArgs e)
        {
            PartsTabViewController.EditSelectedPart();
        }

        private void tvParts_SelectionChanged(object sender, EventArgs e)
        {
            UpdateEnabldeState();
        }

        private void Filter_DropDown(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb == null)
                return;

            int maxWidth = 0;
            int temp = 0;
            Label label1 = new Label();

            foreach (var obj in cb.Items)
            {
                label1.Text = obj.ToString();
                temp = label1.PreferredWidth;
                if (temp > maxWidth)
                    maxWidth = temp;
            }
            label1.Dispose();

            if (maxWidth > cb.Width)
                cb.DropDownWidth = maxWidth;
        }

        private void Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            PartsTabViewController.RefreshTreeView();
        }

        private void cmsParts_Opening(object sender, CancelEventArgs e)
        {
            var selPart = SelectedPart;
            tsmiPartsRemovePart.Enabled = (selPart != null);
            tsmiPartsEditPart.Enabled = (selPart != null);
        }

        #endregion

        internal void LanguageChanged()
        {
            // translates the controls of the view.
            ControlTranslator.TranslateControls(Localizer.GlobalInstance, this as Control, OptionsController.SelectedLanguage);

            // translate columns of ModSelection TreeView
            foreach (NamedTreeColumn column in tvParts.Columns)
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
            tvParts.AddActionKey(key, callback, modifierKeys, once);
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
            return Messages.MSG_PARTSTAB_VIEW_TITLE;
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
                tsbPartsRefresh.Enabled = enable;
                tsbPartsRemove.Enabled = enable;
                tsbPartsEdit.Enabled = enable;
                cbCategoryFilter.Enabled = enable;
                cbModFilter.Enabled = enable;
                tvParts.Enabled = enable;
            }
            else
                UpdateEnabldeState();
        }

        /// <summary>
        /// Updates the enabled state for each control on this view.
        /// </summary>
        private void UpdateEnabldeState()
        {
            var selPart = SelectedPart;

            tsbPartsRefresh.Enabled = true;
            tsbPartsRemove.Enabled = (selPart != null);
            tsbPartsEdit.Enabled = (selPart != null);
            cbCategoryFilter.Enabled = true;
            cbModFilter.Enabled = true;
            tvParts.Enabled = true;
        }
    }
}
