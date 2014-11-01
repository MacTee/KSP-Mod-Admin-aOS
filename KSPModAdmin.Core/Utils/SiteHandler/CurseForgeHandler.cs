using System;
using System.IO;
using System.Net;
using System.Web;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;

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
            if (ParseSite(www.Load(url), ref modInfo))
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
            if (modInfo == null)
                return false;

            string downloadURL = GetDownloadURL(modInfo.ModURL);

            string siteContent = www.Load(GetFilesURL(modInfo.ModURL));
            string filename = GetFileName(siteContent);
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

        private bool ParseSite(string siteContent, ref ModInfo modInfo)
        {
            int i1 = modInfo.ModURL.LastIndexOf("/") + 1;
            if (i1 < 0) return false;
            int i2 = modInfo.ModURL.IndexOf("-", i1);
            if (i2 < 0) return false;
            int l = i2 - i1;
            if (l <= 0) return false;
            modInfo.ProductID = modInfo.ModURL.Substring(i1, l);

            string searchString = "<h1 class=\"project-title\">";
            i1 = siteContent.IndexOf(searchString) + searchString.Length;
            if (i1 < 0) return false;
            i2 = siteContent.IndexOf("</h1>", i1);
            if (i2 < 0) return false;
            modInfo.Name = GetName(siteContent.Substring(i1, i2 - i1));
            siteContent = siteContent.Substring(i2);

            // get creation date
            searchString = "<li>Created: <span><abbr class=\"tip standard-date standard-datetime\" title=\"";
            int index = siteContent.IndexOf(searchString);
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + searchString.Length);
            index = siteContent.IndexOf("\"");
            if (index < 0) return false;
            string creationDate = siteContent.Substring(0, index).Trim();
            modInfo.CreationDate = GetDateTime(creationDate).ToString();

            // get last released file date
            searchString = "<li>Last Released File: <span><abbr class=\"tip standard-date standard-datetime\" title=\"";
            index = siteContent.IndexOf(searchString);
            if (index >= 0)
            {
                siteContent = siteContent.Substring(index + searchString.Length);
                index = siteContent.IndexOf("\"");
                if (index >= 0)
                {
                    creationDate = siteContent.Substring(0, index).Trim();
                    if (GetDateTime(creationDate) > modInfo.CreationDateAsDateTime)
                        modInfo.CreationDate = GetDateTime(creationDate).ToString();
                }
            }

            // get rating count
            index = siteContent.IndexOf("<li>Total Downloads: <span>");
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + 27);
            index = siteContent.IndexOf("</span>");
            if (index < 0) return false;
            modInfo.Downloads = siteContent.Substring(0, index).Trim();

            // more infos could be parsed here (like: short description, Tab content (overview, installation, ...), comments, ...)
            return true;
        }

        private string GetName(string titleHTMLString)
        {
            int index = titleHTMLString.IndexOf("title=\"");
            titleHTMLString = titleHTMLString.Substring(index + 7);
            index = titleHTMLString.IndexOf("\">");
            string name = titleHTMLString.Substring(0, index);
            return name.Trim();
        }

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
