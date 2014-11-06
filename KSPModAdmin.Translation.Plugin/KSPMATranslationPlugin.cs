using System.Collections.Generic;
using KSPModAdmin.Core;
using KSPModAdmin.Translation.Plugin.Properties;

namespace KSPModAdmin.Translation.Plugin
{
    public class KSPMATranslationPlugin : IKSPMAPlugin
    {
        private TabView mTabView = null;


        public TabView[] GetMainTabViews()
        {
            if (mTabView == null)
                mTabView = new TabView(new ucTranslationView(), Resources.text);

            return new[] { mTabView };
        }

        public TabView[] GetOptionTabViews()
        {
            return new List<TabView>().ToArray();
        }
    }
}
