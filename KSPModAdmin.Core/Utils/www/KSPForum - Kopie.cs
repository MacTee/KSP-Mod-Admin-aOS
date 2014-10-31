using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Views;

namespace KSPModAdmin.Core.Utils
{
    public static class KSPForum
    {
        /// <summary>
        /// Checks if the passed url is a KSP Forum link (url).
        /// </summary>
        /// <param name="url">The url to check.</param>
        /// <returns>True if the passed url is a valid KSP Forum link, otherwise false.</returns>
        public static bool IsValidURL(string url)
        {
            return (!string.IsNullOrEmpty(url) && (url.ToLower().StartsWith("http://forum.kerbalspaceprogram.com/") || url.ToLower().StartsWith("http://www.forum.kerbalspaceprogram.com/")));
        }

        /// <summary>
        /// Gets the content of the site of the passed URL and parses it for ModInfos.
        /// </summary>
        /// <param name="url">The URL of the site to parse the ModInfos from.</param>
        /// <returns>The ModInfos parsed from the site of the passed URL.</returns>
        public static ModInfo GetModInfo(string forumURL)
        {
            ModInfo modInfo = new ModInfo();
            modInfo.ForumURL = forumURL;
            modInfo.VersionControl = VersionControl.KSPForum;
            if (ParseSite(www.Load(forumURL), ref modInfo))
                return modInfo;
            else
                return null;
        }

        /// <summary>
        /// Parses the sites content for urls that ends with .zip, .rar or .7zip.
        /// </summary>
        /// <param name="siteContent">The sites content to parse for the urls.</param>
        /// <returns>List of download urls found in the site.</returns>
        public static List<string> GetDownloadURLs(string forumURL)
        {
            string siteContent = www.Load(forumURL);

            List<string> result = new List<string>();

            // just search the first post for zip, rar, 7zip
            int index = siteContent.IndexOf("<div class=\"postbody\">");
            if (index < 0) return result;
            siteContent = siteContent.Substring(index);
            index = siteContent.IndexOf("<div class=\"after_content\">");
            if (index < 0) return result;
            siteContent = siteContent.Substring(0, index);

            Regex regex = new Regex("http(s?):(.*)[.](zip|rar|7zip)(\")");
            var matches = regex.Matches(siteContent);
            // http://taniwha.org/~bill/ModularFuelTanks_v4.3.zip
            // http://taniwha.org/~bill/modularfueltanks_v4.3.zip
            // http://taniwha.org/~bill/ModularFuelTanks_v4.3.zip
            foreach (var entry in matches)
            {
                string url = entry.ToString().Replace("\"", "").ToLower();
                if (url.StartsWith("http://www.mediafire.com") || url.StartsWith("www.mediafire.com"))
                {
                    siteContent = www.Load(url);
                    index = siteContent.IndexOf("http://download");
                    if (index < 0)
                        continue;
                    siteContent = siteContent.Substring(index);
                    index = siteContent.IndexOf("\"");
                    url = siteContent.Substring(0, index);
                    if(regex.IsMatch(siteContent))
                        result.Add(url);
                }
                else if (url.StartsWith("https://skydrive.live.com") || url.StartsWith("http://skydrive.live.com"))
                {
                    // TODO: skydrive parsing.
                }
                else
                    result.Add(url);
            }

            return result;
        }

        /// <summary>
        /// Returns the download URL of the mod archive.
        /// Asks the User if necessary.
        /// </summary>
        /// <param name="forumURL">The KSP Forum thread URL of the mod.</param>
        /// <returns>The download URL of the mod archive.</returns>
        public static string GetDownloadURL(string forumURL)
        {
            string downloadURL = string.Empty;
            List<string> downloadURLs = GetDownloadURLs(forumURL); 
            if (downloadURLs.Count != 1)
            {
                if (downloadURLs.Count > 1 || DialogResult.Yes ==
                    MessageBox.Show(string.Format("No download link to a zip, rar or 7zip found.{0}Do you want to select the download link manually?",
                                    Environment.NewLine), "Error", MessageBoxButtons.YesNo))
                {
                    frmLinkSelection dlg = new frmLinkSelection { URL = forumURL };
                    if (dlg.ShowDialog() == DialogResult.OK)
                        downloadURL = dlg.SelectedLink;
                }
            }
            else if (downloadURLs.Count == 1)
            {
                downloadURL = downloadURLs[0];
            }
            //else if (downloadURLs.Count > 1)
            //{
            //    frmSelectDownloadURL frm = new frmSelectDownloadURL { Links = downloadURLs };
            //    if (frm.ShowDialog() == DialogResult.OK)
            //        downloadURL = frm.SelectedLink;
            //}

            return downloadURL;
        }

        /// <summary>
        /// Downloads a mod from KSP Spaceport.
        /// </summary>
        /// <param name="modInfo"></param>
        /// <returns></returns>
        public static bool DownloadMod(string downloadURL, ref ModInfo modInfo, DownloadProgressChangedEventHandler downloadProgressHandler = null)
        {
            if (modInfo == null || string.IsNullOrEmpty(downloadURL))
                return false;

            // get save path 
            string filename = downloadURL;
            int start = filename.LastIndexOf("?");
            if (start > -1)
            {
                string filenameA = filename.Substring(0, start);
                string filenameB = filename.Substring(start + 1);
                Regex regEx = new Regex("[.](zip)|(rar)|(7zip)");
                if (regEx.IsMatch(filenameA))
                    filename = filenameA;
                else if (regEx.IsMatch(filenameB))
                    filename = filenameB;
                else
                    return false;
            }
            start = filename.LastIndexOf("/") + 1;
            filename = filename.Substring(start, filename.Length - start);
            modInfo.LocalPath = Path.Combine(OptionsController.DownloadPath, filename);
            www.DownloadFile(downloadURL, modInfo.LocalPath, downloadProgressHandler);

            return true;
        }

        /// <summary>
        /// Starts a async download of a mod from KSP Spaceport.
        /// </summary>
        /// <param name="modInfo"></param>
        /// <param name="finished"></param>
        /// <param name="progressChanged"></param>
        public static void DownloadModAsync(string downloadURL, ref ModInfo modInfo, AsyncResultHandler<bool> finished = null, AsyncProgressChangedHandler progressChanged = null)
        {
            // get save path 
            int start = downloadURL.LastIndexOf("/") + 1;
            string filename = downloadURL.Substring(start, downloadURL.Length - start);
            modInfo.LocalPath = Path.Combine(downloadURL, filename);

            AsyncTask<bool> asyncJob = new AsyncTask<bool>();
            asyncJob.SetDownloadCallbackFunctions(modInfo.SpaceportURL, modInfo.LocalPath, finished, progressChanged);
            asyncJob.RunDownload(); ;
        }

        /// <summary>
        /// Parses the siteContent for ModInfo data.
        /// </summary>
        /// <param name="siteContent">The KSP Forum sites content for a mod.</param>
        /// <param name="modInfo">The ModInfo to fill with the data of the site.</param>
        private static bool ParseSite(string siteContent, ref ModInfo modInfo)
        {
            // get threadID
            int index = siteContent.IndexOf("var RELPATH = \"threads/");
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + 23);
            index = siteContent.IndexOf("-");
            if (index < 0) return false;
            modInfo.ProductID = siteContent.Substring(0, index).Trim();

            //get mod title
            index = siteContent.IndexOf("<title>");
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + 7);
            index = siteContent.IndexOf("</title>");
            if (index < 0) return false;
            modInfo.Name = siteContent.Substring(0, index).Trim();

            // get rating
            index = siteContent.IndexOf("<li id=\"threadrating_current\" title=\"Thread Rating: ");
            if (index >= 0)
            {
                siteContent = siteContent.Substring(index + 52);
                index = siteContent.IndexOf(" votes,");
                string votes = siteContent.Substring(0, index).Trim();
                siteContent = siteContent.Substring(index + 7);
                index = siteContent.IndexOf(" average.");
                string rating = siteContent.Substring(0, index).Trim();
                modInfo.Rating = string.Format("{0}({1})", rating, votes);
            }

            // get author
            index = siteContent.IndexOf("Last edited by ");
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + 15);
            index = siteContent.IndexOf(";");
            if (index < 0) return false;
            modInfo.Author = siteContent.Substring(0, index).Trim();
            modInfo.Author = modInfo.Author.Replace("</a>", "").Trim();
            siteContent = siteContent.Substring(index);

            // get edit date
            index = siteContent.IndexOf("</span>") - 1;
            if (index < 0) return false;
            string temp = siteContent.Substring(1, index);
            temp = temp.Replace(" at", "");
            temp = temp.Replace("<span class=\"time\">", "");
            if (temp.Contains("Yesterday"))
                temp = temp.Replace("Yesterday", DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"));
            else if (temp.Contains("Today"))
                temp = temp.Replace("Today", DateTime.Now.ToString("dd.MM.yyyy"));
            else
            {
                temp = temp.Replace("st ", " ");
                temp = temp.Replace("nd ", " ");
                temp = temp.Replace("rd ", " ");
                temp = temp.Replace("th ", " ");
            }

            DateTime dtCreationDate = DateTime.Now;
            modInfo.CreationDate = (DateTime.TryParse(temp, out dtCreationDate)) ? dtCreationDate.ToString() : DateTime.Now.ToString();

            // more infos could be parsed here (like: short description, Tab content (overview, installation, ...), comments, ...)
            return true;
        }
    }
}
