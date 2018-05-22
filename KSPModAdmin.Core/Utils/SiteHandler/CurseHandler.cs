using System.Net;
using HtmlAgilityPack;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Handles the GetModInfo and Mod download for mods on Cures.
    /// </summary>
    public class CurseHandler : ISiteHandler
    {
        private const string NAME = "Curse";
        private const string URL = "http://www.curseforge.com/kerbal/ksp-mods/";
        private const string URL1 = "https://www.curseforge.com/kerbal/ksp-mods/";

        private const string XPATHCURSEFORGEURL = "XPathCurseForgeUrl";

        private string XPathCurseForgeUrl
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHCURSEFORGEURL))
                    OptionsController.OtherAppOptions.Add(XPATHCURSEFORGEURL, "//*[@id='content']/section/div/aside/div[2]/div[3]/p/a");
                return OptionsController.OtherAppOptions[XPATHCURSEFORGEURL];
            }
        }


        /// <summary>
        /// Gets the Name of the ISiteHandler.
        /// </summary>
        /// <returns>The Name of the ISiteHandler.</returns>
        public string Name
        {
            get { return NAME; }
        }


        /// <summary>
        /// Checks if the passed URL is a Curse URL.
        /// </summary>
        /// <param name="url">The URL to check.</param>
        /// <returns>True if the passed URL is a valid CurseForge URL, otherwise false.</returns>
        public bool IsValidURL(string url)
        {
            return url.StartsWith(URL) || url.StartsWith(URL1);
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
        /// <param name="downloadProgressCallback">Callback function for download progress.</param>
        /// <returns>The root node of the added mod, or null.</returns>
        public ModNode HandleAdd(string url, string modName, bool install, DownloadProgressCallback downloadProgressCallback = null)
        {
            ISiteHandler curseForge = SiteHandlerManager.GetSiteHandlerByName("CurseForge");
            ModNode modNode = curseForge.HandleAdd(GetDownloadURL(url), modName, install, downloadProgressCallback);
            ////modNode.VersionControllerName = Name;

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
        /// <param name="downloadProgressCallback">Callback function for download progress.</param>
        /// <returns>True if the mod was downloaded.</returns>
        public bool DownloadMod(ref ModInfo modInfo, DownloadProgressCallback downloadProgressCallback = null)
        {
            ISiteHandler curseForge = SiteHandlerManager.GetSiteHandlerByName("CurseForge");
            return curseForge.DownloadMod(ref modInfo, downloadProgressCallback);
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
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            HtmlNode curseUrlNode = doc.DocumentNode.SelectSingleNode(XPathCurseForgeUrl);
            url = curseUrlNode.Attributes["href"].Value;
            url += url.EndsWith("/") ? "files/latest" : "/files/latest";
            return url;
        }
    }
}
