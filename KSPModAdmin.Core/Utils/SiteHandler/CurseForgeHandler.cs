using System;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using HtmlAgilityPack;
using KSPModAdmin.Core.Utils.Logging;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Handles the GetModInfo and Mod download for mods on CuresForge.
    /// </summary>
    public class CurseForgeHandler : ISiteHandler
    {
        #region Constants

        private const string NAME = "CurseForge";
        private const string HOST = "kerbal.curseforge.com";
        private const string XPATHCURSEURL = "XPathCurseUrl";
        private const string XPATHMODNAME = "XPathModName";
        private const string XPATHMODID = "XPathModId";
        private const string XPATHMODCREATEDATE = "XPathModCreateDate";
        private const string XPATHMODUPDATEDATE = "XPathModUpdateDate";
        private const string XPATHMODDOWNLOADCOUNT = "XPathModDownloadCount";
        private const string XPATHMODAUTHOR = "XPathModAuthor";
        private const string XPATHGAMEVERSION = "XPathGameVersion";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Name of the ISiteHandler.
        /// </summary>
        /// <returns>The Name of the ISiteHandler.</returns>
        public string Name { get { return NAME; } }

        private string xPathCurseUrl
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHCURSEURL))
                    OptionsController.OtherAppOptions.Add(XPATHCURSEURL, "//*[@class='view-on-curse']/a");
                return OptionsController.OtherAppOptions[XPATHCURSEURL];
            }
        }

        private string xPathModName
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHMODNAME))
                    OptionsController.OtherAppOptions.Add(XPATHMODNAME, "//*[@id='project-overview']/header/h2");
                return OptionsController.OtherAppOptions[XPATHMODNAME];
            }
        }

        private string xPathModId
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHMODID))
                    OptionsController.OtherAppOptions.Add(XPATHMODID, "//*[@id='project-overview']/div[1]/div[2]/div/ul/li[2]/span");
                return OptionsController.OtherAppOptions[XPATHMODID];
            }
        }

        private string xPathModCreateDate
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHMODCREATEDATE))
                    OptionsController.OtherAppOptions.Add(XPATHMODCREATEDATE, "//*[@id='project-overview']/div[1]/div[2]/ul[2]/li[6]/abbr");
                return OptionsController.OtherAppOptions[XPATHMODCREATEDATE];
            }
        }

        private string xPathModUpdateDate
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHMODUPDATEDATE))
                    OptionsController.OtherAppOptions.Add(XPATHMODUPDATEDATE, "//*[@id='project-overview']/div[1]/div[2]/ul[2]/li[5]/abbr");
                return OptionsController.OtherAppOptions[XPATHMODUPDATEDATE];
            }
        }

        private string xPathModDownloadCount
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHMODDOWNLOADCOUNT))
                    OptionsController.OtherAppOptions.Add(XPATHMODDOWNLOADCOUNT, "//*[@id='project-overview']/div[1]/div[2]/ul[2]/li[4]");
                return OptionsController.OtherAppOptions[XPATHMODDOWNLOADCOUNT];
            }
        }

        private string xPathModAuthor
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHMODAUTHOR))
                    OptionsController.OtherAppOptions.Add(XPATHMODAUTHOR, "//*[@id='project-overview']/div[1]/div[2]/ul[1]/li[1]/a");
                return OptionsController.OtherAppOptions[XPATHMODAUTHOR]; 
            }
        }

        private string xPathGameVersion
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGAMEVERSION))
                    OptionsController.OtherAppOptions.Add(XPATHGAMEVERSION, "//*[@id='project-overview']/div[1]/div[2]/ul[2]/li[3]");
                return OptionsController.OtherAppOptions[XPATHGAMEVERSION]; 
            }
        }

        #endregion

        /// <summary>
        /// Checks if the passed URL is a CurseForge URL.
        /// </summary>
        /// <param name="url">The URL to check.</param>
        /// <returns>True if the passed URL is a valid CurseForge URL, otherwise false.</returns>
        public bool IsValidURL(string url)
        {
            return !string.IsNullOrEmpty(url) && HOST.Equals(new Uri(url).Authority);
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
            url = ReduceToPlainUrl(url);

            ModInfo modInfo = GetModInfo(url);
            if (modInfo == null)
                return null;

            if (!string.IsNullOrEmpty(modName))
                modInfo.Name = modName;

            ModNode newMod = null;
            if (DownloadMod(ref modInfo, downloadProgressCallback))
                newMod = ModSelectionController.HandleModAddViaModInfo(modInfo, install);

            return newMod;
        }

        /// <summary>
        /// Gets the content of the site of the passed URL and parses it for ModInfos.
        /// </summary>
        /// <param name="url">The URL of the site to parse the ModInfos from.</param>
        /// <returns>The ModInfos parsed from the site of the passed URL.</returns>
        public ModInfo GetModInfo(string url)
        {
            ModInfo modInfo = new ModInfo
            {
                SiteHandlerName = Name, 
                ModURL = ReduceToPlainUrl(url)
            };

            if (ParseSite(url, ref modInfo))
                return modInfo;

            return null;
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
            return modInfo.ChangeDateAsDateTime < newModInfo.ChangeDateAsDateTime;
        }

        /// <summary>
        /// Downloads the mod.
        /// </summary>
        /// <param name="modInfo">The infos of the mod. Must have at least ModURL and LocalPath</param>
        /// <param name="downloadProgressCallback">Callback function for download progress.</param>
        /// <returns>True if the mod was downloaded.</returns>
        public bool DownloadMod(ref ModInfo modInfo, DownloadProgressCallback downloadProgressCallback = null)
        {
            if (modInfo == null)
                return false;

            string downloadURL = GetDownloadURL(modInfo.ModURL);
            modInfo.LocalPath = Www.DownloadFile2(downloadURL, OptionsController.DownloadPath, downloadProgressCallback);

            return !string.IsNullOrEmpty(modInfo.LocalPath) && File.Exists(modInfo.LocalPath);
        }

        /// <summary>
        /// Returns the plain url to the mod, where the ModInfos would be get from.
        /// </summary>
        /// <param name="url">The url to reduce.</param>
        /// <returns>The plain url to the mod, where the ModInfos would be get from.</returns>
        public string ReduceToPlainUrl(string url)
        {
            Uri uri = new Uri(url);
            if (uri.AbsoluteUri.EndsWith("/files/latest"))
                return uri.AbsoluteUri.Replace("/files/latest", string.Empty);
            if (uri.AbsoluteUri.EndsWith("/files"))
                return url.Replace("/files", string.Empty);
            if (uri.AbsoluteUri.EndsWith("/images"))
                return url.Replace("/images", string.Empty);
            
            return uri.AbsoluteUri;
        }

        /// <summary>
        /// Takes a curse project site, fetches the page, and extracts mod info from the page
        /// </summary>
        /// <param name="url">URL to a kerbal.curseforge.com project</param>
        /// <param name="modInfo">Stores the extracted mod data</param>
        /// <returns>Returns true if successfully extracts data</returns>
        private bool ParseSite(string url, ref ModInfo modInfo)
        {
            try
            {
                // get curse.com link for this mod.
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                HtmlNode curseUrlNode = doc.DocumentNode.SelectSingleNode(xPathCurseUrl);
                var cursePage = curseUrlNode.Attributes["href"].Value;

                // changed to use the curse page as it provides the same info but also game version
                // there's no good way to get a mod version from curse. Could use file name? Is using update date (best method?)
                HtmlDocument htmlDoc = web.Load(cursePage);
                htmlDoc.OptionFixNestedTags = true;

                // To scrape the fields, now using HtmlAgilityPack and XPATH search strings.
                // Easy way to get XPATH search: use chrome, inspect element, highlight the needed data and right-click and copy XPATH
                HtmlNode nameNode = htmlDoc.DocumentNode.SelectSingleNode(xPathModName);
                HtmlNode idNode = htmlDoc.DocumentNode.SelectSingleNode(xPathModId);
                HtmlNode createNode = htmlDoc.DocumentNode.SelectSingleNode(xPathModCreateDate);
                HtmlNode updateNode = htmlDoc.DocumentNode.SelectSingleNode(xPathModUpdateDate);
                HtmlNode downloadNode = htmlDoc.DocumentNode.SelectSingleNode(xPathModDownloadCount);
                HtmlNode authorNode = htmlDoc.DocumentNode.SelectSingleNode(xPathModAuthor);
                HtmlNode gameVersionNode = htmlDoc.DocumentNode.SelectSingleNode(xPathGameVersion);

                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); // Curse stores the date as both text and as Epoch. Go for the most precise value (Epoch).

                modInfo.Name = nameNode.InnerHtml;
                modInfo.ProductID = idNode.Attributes["data-id"].Value.Trim('/');
                modInfo.CreationDateAsDateTime = epoch.AddSeconds(Convert.ToDouble(createNode.Attributes["data-epoch"].Value));
                modInfo.ChangeDateAsDateTime = epoch.AddSeconds(Convert.ToDouble(updateNode.Attributes["data-epoch"].Value));
                if (downloadNode != null)
                    modInfo.Downloads = downloadNode.InnerHtml.Split(" ")[0];
                if (authorNode != null)
                    modInfo.Author = authorNode.InnerHtml;
                if (gameVersionNode != null)
                    modInfo.KSPVersion = gameVersionNode.InnerHtml.Split(" ").Last().Trim();

                return true;
            }
            catch (Exception ex)
            {
                Messenger.AddError("Parsing of mod page failed!", ex);
            }

            return false;
        }

        /// <summary>
        /// Gets the latest download URL of a curse project
        /// </summary>
        /// <param name="url">URL of a curse project</param>
        /// <returns>URL pointing to the latest file download</returns>
        private string GetDownloadURL(string url)
        {
            url = ReduceToPlainUrl(url);
            if (url.EndsWith("/"))
                return url + "files/latest";
            else
                return url + "/files/latest";
        }
    }
}
