using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Utils.Controls;
using KSPModAdmin.Core.Views;
using KSPModAdmin.Plugin.FlagsTab.Controller;
using KSPModAdmin.Plugin.FlagsTab.Properties;

namespace KSPModAdmin.Plugin.FlagsTab.Views
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class ucFlagsView : ucBase
    {
        public string SelectedFilter
        {
            get { return tsscbModFilter.SelectedItem != null ? tsscbModFilter.SelectedItem.ToString() : FlagsViewController.FILTER_ALL; }
            set
            {
                if (tsscbModFilter.Items.Count == 0)
                    return;

                if (string.IsNullOrEmpty(value))
                    tsscbModFilter.SelectedIndex = 0;
                else
                    tsscbModFilter.SelectedItem = value;
            }
        }

        public bool ShowProcessingIcon
        { 
            get { return tslProcessing.Visible; } 
            set { tslProcessing.Visible = value; } 
        }

        public IEnumerable<string> FlagFilter { get { return tsscbModFilter.Items.Cast<string>(); } }

        public ListView.SelectedListViewItemCollection SelectedFlags { get { return lvFlags.SelectedItems; } }


        /// <summary>
        /// Creates a new instance of the ucTranslationView class.
        /// </summary>
        public ucFlagsView()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            FlagsViewController.Initialize(this);
        }

        #region Event handling

        private void ucPluginView_Load(object sender, EventArgs e)
        {
            // do View related init here or in the FlagsViewController.Initialize(...) methode.
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
            return Messages.MSG_FLAGS_VIEW_TITLE;
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

        private void tsbFlagsRefresh_Click(object sender, EventArgs e)
        {
            FlagsViewController.RefreshFlagTab();
        }

        private void tsbAddFlag_Click(object sender, EventArgs e)
        {
            FlagsViewController.ImportFlag();
        }

        private void tsbRemoveFlag_Click(object sender, EventArgs e)
        {
            FlagsViewController.DeleteSelectedFlag();
        }

        private void tsscbModFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListView(FlagsViewController.Flags);
        }


        public void ClearAll()
        {
            lvFlags.Items.Clear();
            lvFlags.Groups.Clear();

            ilFlags.Images.Clear();

            tsscbModFilter.Items.Clear();
        }

        public void AddFilter(string filter)
        {
            tsscbModFilter.Items.Add(filter);
        }

        public int AddImageListImage(Image image)
        {
            ilFlags.Images.Add(image);
            return ilFlags.Images.Count - 1;
        }


        /// <summary>
        /// Creates a new ListViewItem for a flag.
        /// </summary>
        /// <param name="flagname">Name of the Flag.</param>
        /// <param name="image">The image of the flag.</param>
        /// <param name="filename">Full path of the flag file.</param>
        /// <returns>The new created ListViewItem.</returns>
        public ListViewItem CreateNewFlagItem(string flagname, Image image, string filename)
        {
            ilFlags.Images.Add(image);

            ListViewItem lvItem = new ListViewItem();
            lvItem.Text = flagname;
            lvItem.ImageIndex = ilFlags.Images.Count - 1;
            lvItem.Group = GetGroup(FlagsViewController.FILTER_MYFLAG);
            lvItem.Tag = filename;

            return lvItem;
        }

        /// <summary>
        /// Gets a existing ListViewGroup or creates a new one.
        /// </summary>
        /// <param name="groupName">The name of the group.</param>
        /// <returns>A existing ListViewGroup or new created one.</returns>
        public ListViewGroup GetGroup(string groupName)
        {
            foreach (ListViewGroup group in lvFlags.Groups)
                if (group.Name.ToLower() == groupName.ToLower())
                    return group;

            ListViewGroup newGroup = new ListViewGroup();
            newGroup.Header = groupName;
            newGroup.Name = groupName;

            bool found = false;
            foreach (string group in tsscbModFilter.Items)
            {
                if (group.ToLower() == groupName.ToLower())
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                tsscbModFilter.Items.Add(groupName);

            return newGroup;
        }

        /// <summary>
        /// Fills the ListView depended on filter settings.
        /// </summary>
        public void FillListView(List<KeyValuePair<string, ListViewItem>> flags)
        {
            lvFlags.Items.Clear();
            foreach (KeyValuePair<string, ListViewItem> pair in flags)
                if (tsscbModFilter.SelectedItem != null &&
                    (((string)tsscbModFilter.SelectedItem) == FlagsViewController.FILTER_ALL ||
                     ((string)tsscbModFilter.SelectedItem).ToLower() == pair.Key.ToLower()))
                {
                    ListViewGroup group = GetGroup(pair.Key);
                    lvFlags.Groups.Add(group);
                    group.Items.Add(pair.Value);
                    lvFlags.Items.Add(pair.Value);
                }
        }
    }
}
