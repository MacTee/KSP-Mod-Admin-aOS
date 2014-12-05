using System;
using System.Collections.Generic;
using KSPModAdmin.Core;
using KSPModAdmin.Translation.Plugin.Properties;

namespace KSPModAdmin.Translation.Plugin
{
    public class KSPMATranslationPlugin : IKSPMAPlugin
    {
        private TabView[] mMainTabViews = null;
        private TabView[] mOptionTabViews = new List<TabView>().ToArray();


        public string Name { get { return "Translation Plugin"; } }

        public string Description
        {
            get
            {
                return "This Plugin adds a tab to help translation of all KSP ModAdmin aOS controls and messages.";
            }
        }

        public TabView[] MainTabViews
        {
            get
            {
                if (mMainTabViews == null)
                    mMainTabViews = new[] { new TabView(new ucTranslationView(), Resources.text) };

                return mMainTabViews;
            }
        }

        public TabView[] OptionTabViews
        {
            get
            {
                return mOptionTabViews;
            }
        }
    }
}
