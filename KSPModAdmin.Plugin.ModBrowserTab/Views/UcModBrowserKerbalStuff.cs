using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.Helper;
using KSPModAdmin.Core.Utils.Localization;
using KSPModAdmin.Core.Views;
using KSPModAdmin.Plugin.ModBrowserTab.Controller;
using KSPModAdmin.Plugin.ModBrowserTab.Model;

namespace KSPModAdmin.Plugin.ModBrowserTab.Views
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class UcModBrowserKerbalStuff : ucBase, IKSPMAModBrowser
    {
        #region Member

        private bool firstStart = true;

        #endregion

        #region Properties

        /// <summary>
        /// Name of the ModBrowser.
        /// </summary>
        public string ModBrowserName { get { return "KerbalStuff"; } }

        /// <summary>
        /// Description of the ModBrowser.
        /// </summary>
        public string Description { get { return "Mod Browser for KerbalStuff"; } }

        /// <summary>
        /// The View of the ModBrowser.
        /// </summary>
        public UserControl ModBrowserView { get { return this; } }

        /// <summary>
        /// The Model of the ModBrowser.
        /// </summary>
        public KerbalStuffTreeModel Model
        {
            get { return tvKsRepositories.Model as KerbalStuffTreeModel; }
            set { tvKsRepositories.Model = value; }
        }

        public int Page 
        {
            get
            {
                int page = 1;
                if (int.TryParse(toolStripTextBox1.Text, out page)) 
                    return page;

                return 1;
            }
            protected set
            {
                var temp = value.ToString();
                if (temp != toolStripTextBox1.Text)
                {
                    toolStripTextBox1.Text = temp;
                    ModBrowserKerbalStuffController.Refresh();
                }
            }
        }

        public int MaxPages
        {
            get
            {
                int page = 1;
                if (int.TryParse(toolStripTextBox2.Text, out page))
                    return page;

                return 1;
            }
            set
            {
                var temp = "1";
                if (value >= 1)
                    temp = value.ToString();

                if (temp != toolStripTextBox2.Text)
                {
                    toolStripTextBox2.Text = temp;

                    if (Page > MaxPages && Page != 1)
                        Page = 1;
                    else
                        ModBrowserKerbalStuffController.Refresh();
                }
            }
        }

        public bool ShowProcessing
        {
            get
            {
                return tslProcessing.Visible;
            }
            set
            {
                tslProcessing.Visible = value;
            }
        }

        /// <summary>
        /// Gets or sets the text of the label for the Mods count.
        /// </summary>
        public string CountLabelText
        {
            get { return lblModBrowserKsCount.Text; }
            set { lblModBrowserKsCount.Text = value; }
        }

        private List<ColumnData> Columns
        {
            get
            {
                List<ColumnData> columns = new List<ColumnData>()
                {
                    new ColumnData()
                    {
                        Name = "Name",
                        Header = Localizer.GlobalInstance["UcModBrowserCKANView_Item_00"], // "Name",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 250,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeCheckBox,
                                DataPropertyName = "Checked",
                                EditEnabled = true,
                                LeftMargin = 0
                            },
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeIcon,
                                DataPropertyName = "Icon",
                                LeftMargin = 3,
                                ImageScaleMode = ImageScaleMode.Clip
                            }, 
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Name",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 0,
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = "Version",
                        Header = Localizer.GlobalInstance["UcModBrowserCKANView_Item_01"], // "Version",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 50,
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
                        Name = "Author",
                        Header = Localizer.GlobalInstance["UcModBrowserCKANView_Item_02"], // "Author",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 90,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Author",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = "Description",
                        Header = Localizer.GlobalInstance["UcModBrowserCKANView_Item_03"], // "Description",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 350,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Description",
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
        public UcModBrowserKerbalStuff()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            ModBrowserKerbalStuffController.Initialize(this);
        }

        #region Event handling

        private void ucModBrowserKerbalStuff_Load(object sender, EventArgs e)
        {
            // do View related init here or in the PluginViewController.Initialize(...) methode.
            TreeViewAdvColumnHelper.ColumnsToTreeViewAdv(tvKsRepositories, Columns);
            ModBrowserKerbalStuffController.Refresh();
        }

        private void tsbModBrowserKsRefresh_Click(object sender, EventArgs e)
        {
            ModBrowserKerbalStuffController.Refresh();
        }

        private void tsbModBrowserKsBrowseNew_Click(object sender, EventArgs e)
        {
            ModBrowserKerbalStuffController.Refresh(RefreshType.New);
        }

        private void tsbModBrowserKsBrowseFeatured_Click(object sender, EventArgs e)
        {
            ModBrowserKerbalStuffController.Refresh(RefreshType.Featured);
        }

        private void tsbModBrowserKsBrowseTop_Click(object sender, EventArgs e)
        {
            ModBrowserKerbalStuffController.Refresh(RefreshType.Top);
        }

        private void tsbModBrowserKsFirstPage_Click(object sender, EventArgs e)
        {
            Page = 1;
        }

        private void tsbModBrowserKsPreviousPage_Click(object sender, EventArgs e)
        {
            if (Page + 1 > MaxPages)
                return;

            Page += 1;
        }

        private void tsbModBrowserKsNextPage_Click(object sender, EventArgs e)
        {
            if (Page - 1 < 1)
                return;

            Page -= 1;
        }

        private void tsbModBrowserKsLastPage_Click(object sender, EventArgs e)
        {
            Page = MaxPages;
        }

        private void tvKsRepositories_DrawControl(object sender, Core.Utils.Controls.Aga.Controls.Tree.NodeControls.DrawEventArgs e)
        {
            var node = e.Node.Tag as KerbalStuffNode;
            if (node == null)
                return;

            if (node.Added || node.ChildAdded)
                e.TextColor = OptionsController.ColorModInstalled;
            ////if (node.IsOutdated)
            ////    e.TextColor = OptionsController.ColorModOutdated;
        }

        internal void LanguageChanged()
        {
            // translates the controls of the view.
            ControlTranslator.TranslateControls(Localizer.GlobalInstance, this as Control, OptionsController.SelectedLanguage);

            // translate columns of ModSelection TreeView
            foreach (NamedTreeColumn column in tvKsRepositories.Columns)
            {
                var newColData = TreeViewAdvColumnHelper.GetColumn(Columns, column.Name);
                if (newColData != null)
                    column.Header = newColData.Header;
            }
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
        }

        /// <summary>
        /// Gets the Name for the parent TabPage.
        /// </summary>
        /// <returns>The Name for the parent TabPage.</returns>
        public override string GetTabCaption()
        {
            return Messages.MSG_MODBROWSER_KERBALSTUFF_VIEW_TITLE;
        }

        /// <summary>
        /// Sets the enabled state of some view controls.
        /// </summary>
        public void SetEnabledOfAllControls(bool enable)
        {
            // Enable/Disable your View Controls here.
            // Normally when KSP MA calls this methode with enable = false, all controls should be disabled.
            ////this.Enabled = enable;
            tvKsRepositories.Enabled = enable;
            tsbModBrowserKsRefresh.Enabled = enable;
            tsbModBrowserKsBrowseNew.Enabled = enable;
            tsbModBrowserKsBrowseFeatured.Enabled = enable;
            tsbModBrowserKsBrowseTop.Enabled = enable;
            tsbModBrowserKsFirstPage.Enabled = enable;
            tsbModBrowserKsLastPage.Enabled = enable;
            tsbModBrowserKsNextPage.Enabled = enable;
            tsbModBrowserKsPreviousPage.Enabled = enable;
        }
    }
}
