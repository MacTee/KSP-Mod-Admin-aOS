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
        public TabView(ucBase tabUserControl, Image tabIcon = null)
        {
            TabUserControl = tabUserControl;
            TabIcon = tabIcon;
        }
    }
}
