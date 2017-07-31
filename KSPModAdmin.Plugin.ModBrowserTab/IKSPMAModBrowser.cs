using System.Windows.Forms;

namespace KSPModAdmin.Plugin.ModBrowserTab
{
    /// <summary>
    /// Interface class for pluggable ModBrowsers.
    /// </summary>
    public interface IKSPMAModBrowser
    {
        /// <summary>
        /// Name of the plugin.
        /// </summary>
        string ModBrowserName { get; }

        /// <summary>
        /// Short description of the plugin.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// View which should be added to the ModBrowser.
        /// </summary>
        UserControl ModBrowserView { get; }
    }
}