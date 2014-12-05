using System.Drawing;
using KSPModAdmin.Core.Views;

namespace KSPModAdmin.Core
{
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
    }

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


        public TabView(ucBase tabUserControl, Image tabIcon = null)
        {
            TabUserControl = tabUserControl;
            TabIcon = tabIcon;
        }
    }
}
