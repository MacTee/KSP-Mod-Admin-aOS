using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Core.Views;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Plugin.FlagsTab.Controller;

namespace KSPModAdmin.Plugin.FlagsTab.Views
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class ucFlagsView : ucBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the currently selected filter.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the flag to determine if the processing icon should be shown or not.
        /// </summary>
        public bool ShowProcessingIcon
        { 
            get { return tslProcessing.Visible; } 
            set { tslProcessing.Visible = value; } 
        }

        /// <summary>
        /// Gets a list of all filters.
        /// </summary>
        public IEnumerable<string> FlagFilter { get { return tsscbModFilter.Items.Cast<string>(); } }

        /// <summary>
        /// Gets the currently selected Flag.
        /// </summary>
        public ListView.SelectedListViewItemCollection SelectedFlags { get { return lvFlags.SelectedItems; } }

        #endregion

        #region Constructor

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

        #endregion

        #region Event handling

        private void ucPluginView_Load(object sender, EventArgs e)
        {
            // do View related init here or in the FlagsViewController.Initialize(...) methode.
            FlagsViewController.CreateKMA2Flag();

            if (lvFlags.Items.Count == 0)
                FlagsViewController.RefreshFlagTab();
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


        /// <summary>
        /// Clears all data of this view.
        /// </summary>
        public void ClearAll()
        {
            lvFlags.Items.Clear();
            lvFlags.Groups.Clear();

            ilFlags.Images.Clear();

            tsscbModFilter.Items.Clear();
        }

        /// <summary>
        /// Adds a filter to the list of possible filters.
        /// </summary>
        /// <param name="filter">The filter to add.</param>
        public void AddFilter(string filter)
        {
            tsscbModFilter.Items.Add(filter);
        }

        /// <summary>
        /// Creates a new ListViewItem for a flag.
        /// </summary>
        /// <param name="flagname">Name of the Flag.</param>
        /// <param name="group">The name of the group (for the ListViewItem).</param>
        /// <param name="image">The image of the flag.</param>
        /// <param name="filename">Full path of the flag file.</param>
        /// <returns>The new created ListViewItem.</returns>
        public ListViewItem CreateNewFlagItem(string flagname, string group, Image image, string filename)
        {
            InvokeIfRequired(() =>
            {
                ilFlags.Images.Add(image);
            });

            ListViewItem lvItem = new ListViewItem();
            lvItem.Text = flagname;
            lvItem.ImageIndex = ilFlags.Images.Count - 1;
            lvItem.Group = GetGroup(group);
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
            {
                InvokeIfRequired(() =>
                {
                    tsscbModFilter.Items.Add(groupName);
                });
            }

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

        /// <summary>
        /// Add a ActionKey CallbackFunction binding to the flag ListView.
        /// </summary>
        /// <param name="key">The action key that raises the callback.</param>
        /// <param name="callback">The callback function with the action that should be called.</param>
        /// <param name="modifierKeys">Required state of the modifier keys to get the callback function called.</param>
        /// <param name="once">Flag to determine if the callback function should only be called once.</param>
        public void AddActionKey(VirtualKey key, ActionKeyHandler callback, ModifierKey[] modifierKeys = null, bool once = false)
        {
            lvFlags.AddActionKey(key, callback, modifierKeys, once);
        }
    }
}
