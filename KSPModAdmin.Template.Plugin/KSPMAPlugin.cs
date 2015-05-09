using System;
using System.Collections.Generic;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Template.Plugin.Properties;

namespace KSPModAdmin.Template.Plugin
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
        public string Name { get { return "Generic Plugin"; } }

        /// <summary>
        /// Description of the plugin.
        /// </summary>
        public string Description
        {
            get
            {
                return "This Plugin adds a tab to [insert purpose here].";
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
                    mMainTabViews = new[] { new TabView(new ucPluginView(), Resources.text) };

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
