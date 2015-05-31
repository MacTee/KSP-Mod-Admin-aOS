using System;
using System.Collections.Generic;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Plugin.PartsAndCraftsTab.Views;
using KSPModAdmin.Plugin.PartsAndCraftsTab.Properties;

namespace KSPModAdmin.Plugin.PartsAndCraftsTab
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
        public string Name { get { return "PartsAndCraftsTab"; } }

        /// <summary>
        /// Description of the plugin.
        /// </summary>
        public string Description
        {
            get
            {
                return "This Plugin adds two tabs. These tabs let you manage the parts and crafts of the chosen KSP install path.";
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
                    mMainTabViews = new[]
                    {
                        new TabView(new Guid("{7AE29F16-C3B9-4B63-BFE5-C85D48BC4757}"), new ucPartsTabView(), Resources.bricks),
                        new TabView(new Guid("{EE4DA64E-A343-48BD-8689-39E04F8FBDD5}"), new ucCraftsTabView(), Resources.airplane)
                    };

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
