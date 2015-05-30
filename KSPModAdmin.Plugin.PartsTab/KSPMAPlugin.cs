using System;
using System.Collections.Generic;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Plugin.PartsTab.Properties;
using KSPModAdmin.Plugin.PartsTab.Views;

namespace KSPModAdmin.Plugin.PartsTab
{
    /// <summary>
    /// KSP MA plugin container class.
    /// </summary>
    public class KSPMAPlugin : IKSPMAPlugin
    {
        private TabView[] mMainTabViews = null;
        private TabView[] mOptionTabViews = new List<TabView>().ToArray(); // Initialize with an empty array if not needed.


        /// <summary>
        /// Name of the plugin.
        /// </summary>
        public string Name { get { return "PartsTab"; } }

        /// <summary>
        /// Description of the plugin.
        /// </summary>
        public string Description
        {
            get
            {
                return "This Plugin adds a tab to manage the parts of the chosen KSP install path.";
            }
        }

        /// <summary>
        /// Array of TabViews that the mod imports.
        /// </summary>
        public TabView[] MainTabViews
        {
            get
            {
                if (mMainTabViews == null)
                    mMainTabViews = new[] { new TabView(new Guid("{7AE29F16-C3B9-4B63-BFE5-C85D48BC4757}"), new ucPartsTabView(), Resources.bricks) };

                return mMainTabViews;
            }
        }

        /// <summary>
        /// Array of OptionTabViews that the mod imports.
        /// </summary>
        public TabView[] OptionTabViews
        {
            get
            {
                return mOptionTabViews;
            }
        }
    }
}
