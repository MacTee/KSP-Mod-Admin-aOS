using System;
using System.IO;
using System.Net;
using System.Web;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using HtmlAgilityPack;

namespace KSPModAdmin.Core.Utils
{
    public class CurseForgeHandler : ISiteHandler
    {
        private const string cName = "CurseForge";
        private const string cURL = "http://kerbal.curseforge.com/";
        private const string cURL2 = "http://www.kerbal.curseforge.com/";

        /// <summary>
        /// Gets the Name of the ISiteHandler.
        /// </summary>
        /// <returns>The Name of the ISiteHandler.</returns>
        public string Name { get { return cName; } }

        /// <summary>
        /// Checks if the passed URL is a CurseForge URL.
        /// </summary>
        /// <param name="url">The URL to check.</param>
        /// <returns>True if the passed URL is a valid CurseForge URL, otherwise false.</returns>
        public bool IsValidURL(string url)
        {
            return (!string.IsNullOrEmpty(url) && (url.ToLower().StartsWith(cURL) || url.ToLower().StartsWith(cURL2)));
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
            url = ReduceToPlainCurseForgeModURL(url);

            ModInfo modInfo = GetModInfo(url);
            if (modInfo == null)
                return null;

            if (!string.IsNullOrEmpty(modName))
                modInfo.Name = modName;

            ModNode newMod = null;
            if (DownloadMod(ref modInfo, downloadProgressHandler))
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
            ModInfo modInfo = new ModInfo();
            modInfo.SiteHandlerName = Name;
            modInfo.ModURL = url;
            if (ParseSite(url, ref modInfo))
                return modInfo;
            else
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
        /// <param name="downloadProgressHandler">Callback function for download progress.</param>
        /// <returns>True if the mod was downloaded.</returns>
        public bool DownloadMod(ref ModInfo modInfo, DownloadProgressChangedEventHandler downloadProgressHandler = null)
        {
            if (modInfo == null)
                return false;

            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = web.Load(modInfo.ModURL);
            htmlDoc.OptionFixNestedTags = true;

            //get filename from hover text
            HtmlNode fileNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='content']/section[2]/div[4]/div[2]/ul/li/div[2]/p/a/span");
            string filename = fileNode.Attributes["title"].Value;

            string downloadURL = GetDownloadURL(modInfo.ModURL);

            modInfo.LocalPath = Path.Combine(OptionsController.DownloadPath, filename);

            www.DownloadFile(downloadURL, modInfo.LocalPath, downloadProgressHandler);

            return File.Exists(modInfo.LocalPath);
        }

        private string ReduceToPlainCurseForgeModURL(string curseForgeURL)
        {
            if (curseForgeURL.EndsWith("/files/latest"))
                return curseForgeURL.Replace("/files/latest", string.Empty);
            if (curseForgeURL.EndsWith("/files"))
                return curseForgeURL.Replace("/files", string.Empty);
            if (curseForgeURL.EndsWith("/images"))
                return curseForgeURL.Replace("/images", string.Empty);

            return curseForgeURL;
        }

        private bool ParseSite(string url, ref ModInfo modInfo)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = web.Load(url);
            htmlDoc.OptionFixNestedTags = true;

            // To scrape the fields, now using HtmlAgilityPack and XPATH search strings.
            // Easy way to get XPATH search: use chrome, inspect element, highlight the needed data and right-click and copy XPATH
            HtmlNode nameNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='project-details-container']/div[@class='project-user']/h1[@class='project-title']/a/span");
            HtmlNode idNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='sidebar-actions']/ul/li[@class='view-on-curse']/a");
            HtmlNode createNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='content']/section[2]/div[1]/div[2]/ul/li[1]/div[2]/abbr");
            HtmlNode updateNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='content']/section[2]/div[1]/div[2]/ul/li[2]/div[2]/abbr");
            HtmlNode downloadNode = htmlDoc.DocumentNode.SelectSingleNode("//ul[@class='cf-details project-details']/li/div[starts-with(., 'Total Downloads')]/following::div[1]");
            HtmlNode authorNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='content']/section[2]/div[3]/div[2]/ul/li/div[2]/p/a/span");

            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); //Curse stores the date as both text and as Epoch. Go for the most precise value (Epoch).
            
            if (nameNode !=null)
            {
                modInfo.Name = nameNode.InnerHtml;
                modInfo.ProductID = idNode.Attributes["href"].Value.Substring(idNode.Attributes["href"].Value.LastIndexOf("/") + 1);
                modInfo.CreationDateAsDateTime = epoch.AddSeconds(Convert.ToDouble(createNode.Attributes["data-epoch"].Value));
                modInfo.ChangeDateAsDateTime = epoch.AddSeconds(Convert.ToDouble(updateNode.Attributes["data-epoch"].Value));
                modInfo.Downloads = downloadNode.InnerHtml;
                modInfo.Author = authorNode.InnerHtml;
                return true;
            }
            
            // more infos could be parsed here (like: short description, Tab content (overview, installation, ...), comments, ...)
            return false;
        }

        //private string GetName(string titleHTMLString) // not in use?
        //{
        //    int index = titleHTMLString.IndexOf("title=\"");
        //    titleHTMLString = titleHTMLString.Substring(index + 7);
        //    index = titleHTMLString.IndexOf("\">");
        //    string name = titleHTMLString.Substring(0, index);
        //    return name.Trim();
        //}

        private DateTime GetDateTime(string curseForgeDateString)
        {
            int index = curseForgeDateString.IndexOf(",") + 2;
            curseForgeDateString = curseForgeDateString.Substring(index);
            index = curseForgeDateString.IndexOf(" ") + 1;
            index = curseForgeDateString.IndexOf(" ", index) + 1;
            index = curseForgeDateString.IndexOf(" ", index) + 1;
            index = curseForgeDateString.IndexOf(" ", index);
            string date = curseForgeDateString.Substring(0, index);
            index = curseForgeDateString.IndexOf("(");
            string tempTimeZone = curseForgeDateString.Substring(index).Substring(0);
            index = tempTimeZone.IndexOf("-");
            if (index < 0)
                index = tempTimeZone.IndexOf("+") + 1;
            else
                index += 1;
            string timeZone = tempTimeZone.Substring(0, index);
            tempTimeZone = tempTimeZone.Substring(index);
            index = tempTimeZone.IndexOf(":");
            if (index < 2)
                timeZone += "0" + tempTimeZone;
            else
                timeZone += tempTimeZone;

            TimeZoneInfo curseZone = null;
            foreach (TimeZoneInfo zone in TimeZoneInfo.GetSystemTimeZones())
            {
                if (!zone.DisplayName.StartsWith(timeZone))
                    continue;

                curseZone = zone;
                break;
            }

            DateTime myDate = DateTime.MinValue;
            if (DateTime.TryParse(date, out myDate) && curseZone != null)
                return TimeZoneInfo.ConvertTime(myDate, curseZone, TimeZoneInfo.Local);
            else
                return DateTime.MinValue;
        }

        private string GetFileName(string siteContent)
        {
            siteContent = siteContent.Replace(Environment.NewLine, string.Empty);

            string searchString = "<a class=\"overflow-tip\" href=\"";
            int i1 = siteContent.IndexOf(searchString) + searchString.Length;
            if (i1 <= searchString.Length) return string.Empty;
            int i2 = siteContent.IndexOf("</a>", i1);
            if (i2 < 0) return string.Empty;
            siteContent = siteContent.Substring(i1, i2 - i1);
            int index = siteContent.IndexOf("\">") + 2;
            string fileName = siteContent.Substring(index);
            if (fileName.Contains("<span title=\""))
            {
                i1 = fileName.IndexOf("<span title=\"") + 13;
                if (i1 < 0) return string.Empty;
                i2 = fileName.IndexOf("\"", i1);
                if (i2 < 0) return string.Empty;
                fileName = fileName.Substring(i1, i2 - i1);
            }
            return HttpUtility.HtmlDecode(fileName.Trim());
        }

        private string GetDownloadURL(string curseForgeURL)
        {
            if (curseForgeURL.EndsWith("/"))
                return curseForgeURL + "files/latest";
            else
                return curseForgeURL + "/files/latest";
        }

        private string GetFilesURL(string curseForgeURL)
        {
            if (curseForgeURL.EndsWith("/"))
                return curseForgeURL + "files";
            else
                return curseForgeURL + "/files";
        }
    }
}
