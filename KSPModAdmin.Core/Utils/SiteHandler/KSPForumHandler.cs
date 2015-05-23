using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HtmlAgilityPack;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils.Logging;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace KSPModAdmin.Core.Utils.SiteHandler
{
    /// <summary>
    /// Handles the GetModInfo and Mod download for mods on KSP Forum.
    /// </summary>
    public class KspForumHandler : ISiteHandler
    {
        private const string NAME = "KSPForum"; // don't change this! Needed for enum!
        private const string HOST = "forum.kerbalspaceprogram.com";
        private const string THREADS = "forum.kerbalspaceprogram.com/threads";

        /// <summary>
        /// Gets the Name of the ISiteHandler.
        /// </summary>
        /// <returns>The Name of the ISiteHandler.</returns>
        public string Name { get { return NAME; } }

        /// <summary>
        /// Checks if the passed URL is a KSP Forum URL.
        /// </summary>
        /// <param name="url">The URL to check.</param>
        /// <returns>True if the passed URL is a valid KSP Forum URL, otherwise false.</returns>
        public bool IsValidURL(string url)
        {
            return (!string.IsNullOrEmpty(url) && HOST.Equals(new Uri(url).Authority) && url.Contains(THREADS));
        }

        /// <summary>
        /// Handles a mod add via URL.
        /// Validates the URL, gets ModInfos, downloads mod archive, adds it to the ModSelection and installs the mod if selected.
        /// </summary>
        /// <param name="url">The URL to the mod.</param>
        /// <param name="modName">The name for the mod.</param>
        /// <param name="install">Flag to determine if the mod should be installed after adding.</param>
        /// <param name="downloadProgressHandler">Callback function for download progress.</param>
        /// <returns>The root node of the added mod, or null.</returns>
        public ModNode HandleAdd(string url, string modName, bool install, DownloadProgressChangedEventHandler downloadProgressHandler = null)
        {
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
            var modInfo = new ModInfo
            {
                SiteHandlerName = Name,
                ModURL = ReduceToPlainUrl(url)
            };
            ParseSite(ref modInfo);
            return modInfo;
        }

        /// <summary>
        /// Checks if updates are available for the passed mod.
        /// </summary>
        /// <param name="modInfo">The ModInfos of the mod to check for updates.</param>
        /// <param name="newModInfo">A reference to an empty ModInfo to write the updated ModInfos to.</param>
        /// <returns>True if there is an update, otherwise false.</returns>
        public bool CheckForUpdates(ModInfo modInfo, ref ModInfo newModInfo)
        {
            newModInfo = GetModInfo(modInfo.ModURL);

            if (string.IsNullOrEmpty(modInfo.ChangeDate) && !string.IsNullOrEmpty(newModInfo.ChangeDate))
                return true;
            else if (!string.IsNullOrEmpty(modInfo.ChangeDate) && !string.IsNullOrEmpty(newModInfo.ChangeDate))
                return modInfo.ChangeDateAsDateTime < newModInfo.ChangeDateAsDateTime;

            return false;
        }

        /// <summary>
        /// Downloads the mod.
        /// </summary>
        /// <param name="modInfo">The infos of the mod. Must have at least ModURL and LocalPath</param>
        /// <param name="downloadProgressHandler">Callback function for download progress.</param>
        /// <returns>True if the mod was downloaded.</returns>
        public bool DownloadMod(ref ModInfo modInfo, DownloadProgressChangedEventHandler downloadProgressHandler = null)
        {
            Messenger.AddError("No download support for KSP Forum mods, update check only!");
            MessageBox.Show("No download support for KSP Forum mods, update check only!", Messages.MSG_TITLE_ATTENTION);
            return false;
            //if (modInfo == null)
            //    return false;

            //string downloadUrl = GetDownloadUrl(modInfo);
            //modInfo.LocalPath = Path.Combine(OptionsController.DownloadPath, GetDownloadName(downloadUrl));
            //Www.DownloadFile(downloadUrl, modInfo.LocalPath, downloadProgressHandler);

            //return File.Exists(modInfo.LocalPath);
        }

        /// <summary>
        /// Returns the plain url to the mod, where the ModInfos would be get from.
        /// </summary>
        /// <param name="url">The url to reduce.</param>
        /// <returns>The plain url to the mod, where the ModInfos would be get from.</returns>
        public string ReduceToPlainUrl(string url)
        {
            if (!IsValidURL(url))
                return url;

            int index = url.IndexOf("-");
            if (index > 0)
                url = url.Substring(0, index);

            return url;
        }

        private void ParseSite(ref ModInfo modInfo)
        {
            var htmlDoc = new HtmlWeb().Load(modInfo.ModURL);
            htmlDoc.OptionFixNestedTags = true;

            // To scrape the fields, now using HtmlAgilityPack and XPATH search strings.
            // Easy way to get XPATH search: use chrome, inspect element, highlight the needed data and right-click and copy XPATH

            modInfo.ProductID = GetProductID(modInfo.ModURL);
            modInfo.CreationDateAsDateTime = GetCreationDate(htmlDoc);
            modInfo.ChangeDateAsDateTime = GetChangeDate(htmlDoc);
            if (modInfo.ChangeDateAsDateTime == DateTime.MinValue)
                modInfo.ChangeDateAsDateTime = modInfo.CreationDateAsDateTime;
            modInfo.Author = GetAuthor(htmlDoc);
            modInfo.Name = GetModName(htmlDoc);
        }

        private string GetModName(HtmlDocument htmlDoc)
        {
            var result = string.Empty;
            HtmlNode title = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='pagetitle']/h1/span/a");
            if (title != null)
                result = title.InnerText;
            return result;
        }

        private string GetAuthor(HtmlDocument htmlDoc)
        {
            var result = string.Empty;
            try
            {
                string content = htmlDoc.DocumentNode.InnerHtml;

                // find first post
                content = content.Substring(content.IndexOf("<div class=\"popupmenu memberaction\">"));
                string strong = "<strong>";
                content = content.Substring(content.IndexOf(strong) + strong.Length);
                result = content.Substring(0, content.IndexOf("</strong>"));
            }
            catch (Exception ex)
            {
                Log.AddErrorS("Error in KspForumHandler.GetAuthor!", ex);
            }
            return result;
        }

        private DateTime GetChangeDate(HtmlDocument htmlDoc)
        {
            // var latestRelease = htmlDoc.DocumentNode.SelectNodes("/html[1]/body[1]/div[2]/div[6]/ol[1]/li[1]/div[2]/div[2]/div[2]"); // don't works =/

            DateTime dt = DateTime.MinValue;
            try
            {
                string content = htmlDoc.DocumentNode.InnerHtml;

                // find first post
                content = content.Substring(content.IndexOf("<div class=\"userinfo\">"));
                content = content.Substring(0, content.IndexOf("<div class=\"postfoot\">"));

                // read last edited date
                int index = content.IndexOf("<blockquote class=\"postcontent lastedited\">");
                if (index > 0)
                {
                    string blockquote = "<blockquote class=\"postcontent lastedited\">";
                    content = content.Substring(content.IndexOf(blockquote));
                    string lastEdited = "Last edited by ";
                    content = content.Substring(content.IndexOf(lastEdited) + lastEdited.Length);
                    content = content.Substring(content.IndexOf(";") + 1);
                    content = content.Substring(0, content.IndexOf("<"));
                    content = content.Replace("at", "").Replace("th", "").Replace("st", "").Replace("nd", "").Replace("rd", "").Trim();
                    DateTime.TryParse(content, out dt);
                }
            }
            catch (Exception ex)
            {
                Log.AddErrorS("Error in KspForumHandler.GetChangeDate!", ex);
            }
            return dt;
        }

        private DateTime GetCreationDate(HtmlDocument htmlDoc)
        {
            HtmlNode creationDate = htmlDoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[2]/div[6]/ol[1]/li/div[1]/span[1]/span/text()");
            DateTime dt = DateTime.MinValue;
            if (creationDate != null)
                DateTime.TryParse(creationDate.OuterHtml.Replace(",&nbsp;", "").Replace("th", "").Replace("st", "").Replace("nd", "").Replace("rd", ""), out dt);
            return dt;
        }

        private static string GetProductID(string modUrl)
        {
            var result = string.Empty;
            try
            {
                int index = modUrl.IndexOf("-");
                if (index > 0)
                    result = modUrl.Substring(1, index).Replace(THREADS, "");
                else
                {
                    index = modUrl.IndexOf(THREADS) + THREADS.Length + 1;
                    if (index > 0)
                        result = modUrl.Substring(index);
                }
            }
            catch (Exception ex)
            {
                Log.AddErrorS("Error in KSPForumHandler.GetProductID()", ex);
            }

            return result;
        }
    }
}
