using System;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;

namespace KSPModAdmin.Core.Utils.SiteHandler
{
    /// <summary>
    /// Handles the GetModInfo and Mod download for mods on CuresForge.
    /// </summary>
    public class CurseForgeHandler : ISiteHandler
    {
        #region Constants

        private const string NAME = "CurseForge";
        private const string HOST = "kerbal.curseforge.com";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Name of the ISiteHandler.
        /// </summary>
        /// <returns>The Name of the ISiteHandler.</returns>
        public string Name { get { return NAME; } }

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
                var cursePage = CurseForgeParser.GetCurseUrl(doc);

                // changed to use the curse page as it provides the same info but also game version
                // there's no good way to get a mod version from curse. Could use file name? Is using update date (best method?)
                doc = web.Load(cursePage);
                doc.OptionFixNestedTags = true;

                modInfo.Name = CurseForgeParser.GetModName(doc);
                modInfo.ProductID = CurseForgeParser.GetModId(doc);
                modInfo.CreationDateAsDateTime = CurseForgeParser.GetModCreationDate(doc);
                modInfo.ChangeDateAsDateTime = CurseForgeParser.GetChangeDate(doc);
                modInfo.Downloads = CurseForgeParser.GetModDownloadCount(doc);
                modInfo.Author = CurseForgeParser.GetModAuthor(doc);
                modInfo.KSPVersion = CurseForgeParser.GetModGameVersion(doc);

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

    public class CurseForgeParser
    {
        // Curse stores the date as both text and as Epoch. Go for the most precise value (Epoch).
        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        #region XPath

        // To scrape the fields, now using HtmlAgilityPack and XPATH search strings.
        // Easy way to get XPATH search: use chrome, inspect element, highlight the needed data and right-click and copy XPATH

        private static readonly string XPATHCURSEURL = "XPathCurseUrl";
        private static readonly string XPATHCURSEFORGEURL = "XPathCurseForgeUrl";
        private static readonly string XPATHMODNAME = "XPathModName";
        private static readonly string XPATHMODID = "XPathModId";
        private static readonly string XPATHMODCREATEDATE = "XPathModCreateDate";
        private static readonly string XPATHMODUPDATEDATE = "XPathModUpdateDate";
        private static readonly string XPATHMODDOWNLOADCOUNT = "XPathModDownloadCount";
        private static readonly string XPATHMODAUTHOR = "XPathModAuthor";
        private static readonly string XPATHGAMEVERSION = "XPathGameVersion";

        private static string xPathCurseUrl
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHCURSEURL))
                    OptionsController.OtherAppOptions.Add(XPATHCURSEURL, "//*[@class='view-on-curse']/a");
                return OptionsController.OtherAppOptions[XPATHCURSEURL];
            }
        }

        private static string XPathCurseForgeUrl
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHCURSEFORGEURL))
                    OptionsController.OtherAppOptions.Add(XPATHCURSEFORGEURL, "//*[@id='content']/section/div/aside/div[2]/div[3]/p/a");
                return OptionsController.OtherAppOptions[XPATHCURSEFORGEURL];
            }
        }

        private static string xPathModName
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHMODNAME))
                    OptionsController.OtherAppOptions.Add(XPATHMODNAME, "//*[@id='content']/section/div/main/header/div[2]/h2");
                return OptionsController.OtherAppOptions[XPATHMODNAME];
            }
        }

        private static string xPathModId
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHMODID))
                    OptionsController.OtherAppOptions.Add(XPATHMODID, "//*[@id='content']/section/div/aside/div[2]/div[2]/a[3]");
                return OptionsController.OtherAppOptions[XPATHMODID];
            }
        }

        private static string xPathModCreateDate
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHMODCREATEDATE))
                    OptionsController.OtherAppOptions.Add(XPATHMODCREATEDATE, "//*[@id='content']/section/div/main/section[1]/div/p[2]/span/abbr");
                return OptionsController.OtherAppOptions[XPATHMODCREATEDATE];
            }
        }

        private static string xPathModUpdateDate
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHMODUPDATEDATE))
                    OptionsController.OtherAppOptions.Add(XPATHMODUPDATEDATE, "//*[@id='content']/section/div/main/header/div[2]/p/span[1]/abbr");
                return OptionsController.OtherAppOptions[XPATHMODUPDATEDATE];
            }
        }

        private static string xPathModDownloadCount
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHMODDOWNLOADCOUNT))
                    OptionsController.OtherAppOptions.Add(XPATHMODDOWNLOADCOUNT, "//*[@id='content']/section/div/main/section[1]/div/p[1]/span");
                return OptionsController.OtherAppOptions[XPATHMODDOWNLOADCOUNT];
            }
        }

        private static string xPathModAuthor
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHMODAUTHOR))
                    OptionsController.OtherAppOptions.Add(XPATHMODAUTHOR, "//*[@id='content']/section/div/main/section[1]/div/p[3]/span[2]/a");
                return OptionsController.OtherAppOptions[XPATHMODAUTHOR];
            }
        }

        private static string xPathGameVersion
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGAMEVERSION))
                    OptionsController.OtherAppOptions.Add(XPATHGAMEVERSION, "//*[@id='content']/section/div/main/header/div[2]/p/span[2]");
                return OptionsController.OtherAppOptions[XPATHGAMEVERSION];
            }
        }

        #endregion


        public static string GetCurseUrl(HtmlDocument doc)
        {
            HtmlNode curseUrlNode = doc.DocumentNode.SelectSingleNode(xPathCurseUrl);
            var cursePageUrl = curseUrlNode.Attributes["href"].Value;
            return cursePageUrl;
        }

        public static string GetCurseForgeUrl(HtmlDocument doc)
        {
            HtmlNode curseUrlNode = doc.DocumentNode.SelectSingleNode(XPathCurseForgeUrl);
            return curseUrlNode.Attributes["href"].Value;
        }

        public static string GetModName(HtmlDocument doc)
        {
            HtmlNode nameNode = doc.DocumentNode.SelectSingleNode(xPathModName);
            var modName = nameNode.InnerHtml; ;
            return modName;
        }

        public static string GetModId(HtmlDocument doc)
        {
            HtmlNode idNode = doc.DocumentNode.SelectSingleNode(xPathModId);
            var productID = idNode.Attributes["data-id"].Value.Trim('/');
            return productID;
        }

        public static DateTime GetModCreationDate(HtmlDocument doc)
        {
            HtmlNode createNode = doc.DocumentNode.SelectSingleNode(xPathModCreateDate);
            var creationDate = new DateTime(Epoch.Ticks).AddSeconds(Convert.ToDouble(createNode.Attributes["data-epoch"].Value));
            return creationDate;
        }

        public static DateTime GetChangeDate(HtmlDocument doc)
        {
            HtmlNode updateNode = doc.DocumentNode.SelectSingleNode(xPathModUpdateDate);
            var udpateDate = new DateTime(Epoch.Ticks).AddSeconds(Convert.ToDouble(updateNode.Attributes["data-epoch"].Value));
            return udpateDate;
        }

        public static string GetModDownloadCount(HtmlDocument doc)
        {
            HtmlNode downloadNode = doc.DocumentNode.SelectSingleNode(xPathModDownloadCount);
            return (downloadNode != null) ? downloadNode.InnerHtml.Split(" ")[0] : "0";
        }

        public static string GetModAuthor(HtmlDocument doc)
        {
            HtmlNode authorNode = doc.DocumentNode.SelectSingleNode(xPathModAuthor);
            return (authorNode != null) ? authorNode.InnerHtml : string.Empty;
        }

        public static string GetModGameVersion(HtmlDocument doc)
        {
            HtmlNode gameVersionNode = doc.DocumentNode.SelectSingleNode(xPathGameVersion);
            return (gameVersionNode != null) ? gameVersionNode.InnerHtml.Split(" ").Last().Trim() : string.Empty;
        }
    }
}
