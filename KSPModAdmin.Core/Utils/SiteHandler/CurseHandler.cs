using System.Net;
using KSPModAdmin.Core.Model;

namespace KSPModAdmin.Core.Utils
{
    public class CurseHandler : ISiteHandler
    {
        private const string cName = "Curse";
        private const string cURL = "http://www.curse.com/ksp-mods/kerbal/";
        private const string cURL2 = "http://curse.com/ksp-mods/kerbal/";


        /// <summary>
        /// Gets the Name of the ISiteHandler.
        /// </summary>
        /// <returns>The Name of the ISiteHandler.</returns>
        public string Name
        {
            get { return cName; }
        }


        /// <summary>
        /// Checks if the passed URL is a Curse URL.
        /// </summary>
        /// <param name="url">The URL to check.</param>
        /// <returns>True if the passed URL is a valid CurseForge URL, otherwise false.</returns>
        public bool IsValidURL(string url)
        {
            return (url.StartsWith(cURL) || url.StartsWith(cURL2));
        }

        /// <summary>
        /// Gets the content of the site of the passed URL and parses it for ModInfos.
        /// </summary>
        /// <param name="url">The URL of the site to parse the ModInfos from.</param>
        /// <returns>The ModInfos parsed from the site of the passed URL.</returns>
        public ModInfo GetModInfo(string url)
        {
            ISiteHandler curseForge = SiteHandlerManager.GetSiteHandlerByName("CurseForge");
            return curseForge.GetModInfo(GetDownloadURL(url));
        }

        /// <summary>
        /// Handles a mod add via URL.
        /// Validates the URL, gets ModInfos, downloads mod archive, adds it to the ModSelection and installs the mod if selected.
        /// </summary>
        /// <param name="url">The URL to the mod.</param>
        /// <param name="modName">The name for the mod.</param>
        /// <param name="install">Flag to determine if the mod should be installed after adding.</param>
        /// <param name="downloadProgressHandler">Callback function for download progress.</param>
        /// <return>The root node of the added mod, or null.</return>
        public ModNode HandleAdd(string url, string modName, bool install, DownloadProgressChangedEventHandler downloadProgressHandler = null)
        {
            ISiteHandler curseForge = SiteHandlerManager.GetSiteHandlerByName("CurseForge");
            ModNode modNode = curseForge.HandleAdd(GetDownloadURL(url), modName, install, downloadProgressHandler);
            //modNode.VersionControllerName = Name;

            return modNode;
        }

        /// <summary>
        /// Checks if updates are available for the passed mod.
        /// </summary>
        /// <param name="modInfo">The ModInfos of the mod to check for updates.</param>
        /// <param name="newModInfo">A reference to a empty ModInfo to write the updated ModInfos to.</param>
        /// <returns>True if there is an update, otherwise false.</returns>
        public bool CheckForUpdates(ModInfo modInfo, ref ModInfo newModInfo)
        {
            newModInfo = GetModInfo(modInfo.ModURL);
            return modInfo.CreationDateAsDateTime < newModInfo.CreationDateAsDateTime;
        }

        /// <summary>
        /// Downloads the mod.
        /// </summary>
        /// <param name="modInfo">The infos of the mod. Must have at least ModURL and LocalPath</param>
        /// <param name="downloadProgressHandler">Callback function for download progress.</param>
        /// <returns>True if the mod was downloaded.</returns>
        public bool DownloadMod(ref ModInfo modInfo, DownloadProgressChangedEventHandler downloadProgressHandler = null)
        {
            ISiteHandler curseForge = SiteHandlerManager.GetSiteHandlerByName("CurseForge");
            return curseForge.DownloadMod(ref modInfo, downloadProgressHandler);
        }

        /// <summary>
        /// Returns the plain url to the mod, where the ModInfos would be get from.
        /// </summary>
        /// <param name="url">The url to reduce.</param>
        /// <returns>The plain url to the mod, where the ModInfos would be get from.</returns>
        public string ReduceToPlainUrl(string url)
        {
            ISiteHandler curseForge = SiteHandlerManager.GetSiteHandlerByName("CurseForge");
            return curseForge.ReduceToPlainUrl(GetDownloadURL(url));
        }


        /// <summary>
        /// Parse the download URL from site.
        /// </summary>
        /// <param name="url">The URL to parse the download URL from.</param>
        /// <returns>The parsed download URL from site.</returns>
        private string GetDownloadURL(string url)
        {
            string filename = string.Empty;
            return GetDownloadURL(url, ref filename);
        }

        /// <summary>
        /// Parse the download URL from site.
        /// </summary>
        /// <param name="url">The URL to parse the download URL from.</param>
        /// <param name="filename">The Filename of the mod archive.</param>
        /// <returns>The parsed download URL from site.</returns>
        private string GetDownloadURL(string url, ref string filename)
        {
            string siteContent = www.Load(url);

            int index1 = siteContent.IndexOf("<li class=\"newest-file\">Newest File: ") + 37;
            if (index1 < 0)
                return string.Empty;
            siteContent = siteContent.Substring(index1);
            index1 = siteContent.IndexOf("</li>");
            if (index1 < 0)
                return string.Empty;
            filename = siteContent.Substring(0, index1);

            index1 = siteContent.IndexOf("<em class=\"cta-button download-large\">");
            if (index1 < 0)
                return string.Empty;
            index1 = siteContent.IndexOf("href=\"", index1) + 6;
            siteContent = siteContent.Substring(index1);
            index1 = siteContent.IndexOf("\"");
            if (index1 < 0)
                return string.Empty;

            // /ksp-mods/220221-mechjeb/download
            // http://kerbal.curseforge.com/ksp-mods/220221-mechjeb/files/latest
            string downloadURL = "http://kerbal.curseforge.com" + siteContent.Substring(0, index1).Replace("/kerbal/", "/").Replace("/download", "/") + "files/latest";

            return (downloadURL.StartsWith("http:/") || downloadURL.StartsWith("https:/")) ? downloadURL : string.Empty;
        }
    }
}
