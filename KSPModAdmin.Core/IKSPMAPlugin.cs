using System.Collections.Specialized;
using System.Drawing;
using KSPModAdmin.Core.Views;

namespace KSPModAdmin.Core
{
    public interface IKSPMAPlugin
    {
        TabView[] GetMainTabViews();

        TabView[] GetOptionTabViews();
    }

    public class TabView
    {
        public string TabName { get { return TabUserControl.GetTabName(); } }

        public ucBase TabUserControl { get; private set; }

        public Image TabIcon { get; private set; }


        public TabView(ucBase tabUserControl, Image tabIcon = null)
        {
            TabUserControl = tabUserControl;
            TabIcon = tabIcon;
        }
    }
}
