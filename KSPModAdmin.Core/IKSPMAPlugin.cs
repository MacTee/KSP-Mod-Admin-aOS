using System;
using System.Drawing;
using KSPModAdmin.Core.Views;

namespace KSPModAdmin.Core
{
    using KSPModAdmin.Core.Utils;

    /// <summary>
    /// Interface for a KSP Mod Admin plugin.
    /// </summary>
    public interface IKSPMAPlugin
    {
        /// <summary>
        /// Name of the plugin.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Short description of the plugin.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Array of TabViews which should be added to the Main TabControl.
        /// </summary>
        TabView[] MainTabViews { get; }

        /// <summary>
        /// Array of TabViews which should be added to the Options TabControl.
        /// </summary>
        TabView[] OptionTabViews { get; }

        /////// <summary>
        /////// Array of new SiteHandler classes to import.
        /////// </summary>
        ////ISiteHandler[] SiteHandler { get; }
    }

    /// <summary>
    /// Class that holds the view for a Tab of the frmMain of a KSPMA plugin.
    /// </summary>
    public class TabView
    {
        /// <summary>
        /// A Guid as unique identifier for this view.
        /// </summary>
        public Guid UniqueIdentifier { get; private set; }

        /// <summary>
        /// The ucBase derived UserControl to add to the TabPage.
        /// </summary>
        public ucBase TabUserControl { get; private set; }

        /// <summary>
        /// The Icon of the TabPage.
        /// </summary>
        public Image TabIcon { get; private set; }

        /// <summary>
        /// Creates a new instance of the TabView class.
        /// </summary>
        /// <param name="uniqueIdentifier">A new create guid to identifier this view.</param>
        /// <param name="tabUserControl">The UserControl that should be added to the TabPage</param>
        /// <param name="tabIcon">The icon that should be used on the TabHeader.</param>
        public TabView(Guid uniqueIdentifier, ucBase tabUserControl, Image tabIcon = null)
        {
            UniqueIdentifier = uniqueIdentifier;
            TabUserControl = tabUserControl;
            TabIcon = tabIcon;
        }
    }
}
