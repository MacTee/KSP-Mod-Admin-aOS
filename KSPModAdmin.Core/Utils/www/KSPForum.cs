using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using KSPModAdmin.Core.Model;

namespace KSPModAdmin.Core.Utils
{
    public static class KSPForum
    {
        /// <summary>
        /// Name of the VersionController
        /// </summary>
        public static string Name { get { return "KSPForum"; } }


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
        /// <param name="url">The URL to the KSP forum site to parse the ModInfos from.</param>
        /// <returns>The ModInfos parsed from the site of the passed URL.</returns>
        public static ModInfo GetModInfo(string url)
        {
            ModInfo modInfo = new ModInfo();
            modInfo.SiteHandlerName = Name;
            modInfo.ModURL = url;
            if (ParseSite(www.Load(url), ref modInfo))
                return modInfo;
            else
                return null;
        }

//        /// <summary>
//        /// Parses the sites content for URLs that ends with .zip, .rar or .7zip.
//        /// </summary>
//        /// <param name="forumURL">The URL to the KSP forum site to parse the links from.</param>
//        /// <returns>List of download URLs found in the site.</returns>
//        public static List<DownloadInfo> GetDownloadURLs(string forumURL)
//        {
//            List<DownloadInfo> result = new List<DownloadInfo>();

//            string firstPost = GetFirstPost(www.Load(forumURL));
//            foreach (LinkInfo entry in www.GetHTMLLinks(firstPost))
//            {
//                string url = entry.URL.ToLower();
//                if (url.Contains("forum.kerbal")) // sort forum link out.
//                    continue;

//                DownloadInfo dInfo = www.GetDirectDownloadURLFromHostSite(url);

//                if (dInfo.IsValid)
//                {
//                    dInfo.Name = entry.Name;
//                    result.Add(dInfo);
//                }
//                else if (!dInfo.KnownHost)
//                {
//                    if (www.IsValidArchiveDownloadURL(url))
//                        result.Add(new DownloadInfo() { Name = entry.Name, DownloadURL = url, Filename = www.GetFileName(url) });
//                    //else
//                    //    result.Add(new DownloadInfo() { Name = entry.Name, DownloadURL = url });
//                }
//            }

//            return result;
//        }

//        public static DownloadInfo GetDownloadURL(string forumURL)
//        {
//            DownloadInfo dInfo = null;
//            List<DownloadInfo> dInfos = GetDownloadURLs(forumURL);

//            if (dInfos.Count > 0)
//            {
//                frmSelectDownloadURL frm = new frmSelectDownloadURL { Links = dInfos };
//                DialogResult result = frm.ShowDialog();
//                if (result == DialogResult.OK)
//                    dInfo = frm.SelectedLink;
//            }
//            else
//            {
//#if !MONOBUILD
//                MessageBox.Show("No direct download link detected, please use the ModBrowser to download the mod.");
//#else
//                MessageBox.Show("No direct download link detected, please use your favorite browser to download the mod and add it manually.");
//#endif
//            }
//            //if (dInfos.Count != 1)
//            //{
//                //if (dInfos.Count > 1 || DialogResult.Yes ==
//                //    MessageBox.Show(string.Format("No download link to a zip, rar or 7zip found.{0}Do you want to select the download link manually?",
//                //                    Environment.NewLine), "Error", MessageBoxButtons.YesNo))
//                //{
//                    //frmLinkSelection dlg = new frmLinkSelection { URL = forumURL };
//                    //if (dlg.ShowDialog() == DialogResult.OK)
//                    //    downloadURL = dlg.SelectedLink;
//                //}
//            //}
//            //else if (dInfos.Count == 1)
//            //{
//            //    downloadURL = dInfos[0].DownloadURL;
//            //}
//            //else if (downloadURLs.Count > 1)
//            //{
//            //    frmSelectDownloadURL frm = new frmSelectDownloadURL { Links = downloadURLs };
//            //    if (frm.ShowDialog() == DialogResult.OK)
//            //        downloadURL = frm.SelectedLink;
//            //}

//            return dInfo;
//        }

//        public static ModInfo DownloadMod(string downloadURL, string kspForumURL, string filename, DownloadProgressChangedEventHandler downloadProgressHandler = null)
//        {
//            ModInfo modInfo = GetModInfo(kspForumURL);
//            modInfo.LocalPath = Path.Combine(MainForm.Instance.Options.DownloadPath, filename);

//            www.DownloadFile(downloadURL, modInfo.LocalPath, downloadProgressHandler);

//            return (File.Exists(modInfo.LocalPath)) ? modInfo : null;
//        }

//        /// <summary>
//        /// Downloads a mod from KSP Spaceport.
//        /// </summary>
//        /// <param name="modInfo"></param>
//        /// <returns></returns>
//        public static bool DownloadMod(string downloadURL, ref ModInfo modInfo, DownloadProgressChangedEventHandler downloadProgressHandler = null)
//        {
//            if (modInfo == null || string.IsNullOrEmpty(downloadURL))
//                return false;

//            // get save path 
//            string filename = downloadURL;
//            int start = filename.LastIndexOf("?");
//            if (start > -1)
//            {
//                string filenameA = filename.Substring(0, start);
//                string filenameB = filename.Substring(start + 1);
//                Regex regEx = new Regex("[.](zip)|(rar)|(7zip)");
//                if (regEx.IsMatch(filenameA))
//                    filename = filenameA;
//                else if (regEx.IsMatch(filenameB))
//                    filename = filenameB;
//                else
//                    return false;
//            }
//            start = filename.LastIndexOf("/") + 1;
//            filename = filename.Substring(start, filename.Length - start);
//            if (string.IsNullOrEmpty(filename))
//                return false;

//            modInfo.LocalPath = Path.Combine(MainForm.Instance.Options.DownloadPath, filename);
//            www.DownloadFile(downloadURL, modInfo.LocalPath, downloadProgressHandler);

//            return true;
//        }

//        /// <summary>
//        /// Starts a async download of a mod from KSP Spaceport.
//        /// </summary>
//        /// <param name="modInfo"></param>
//        /// <param name="finished"></param>
//        /// <param name="progressChanged"></param>
//        public static void DownloadModAsync(string downloadURL, ref ModInfo modInfo, AsyncResultHandler<bool> finished = null, AsyncProgressChangedHandler progressChanged = null)
//        {
//            // get save path 
//            int start = downloadURL.LastIndexOf("/") + 1;
//            string filename = downloadURL.Substring(start, downloadURL.Length - start);
//            modInfo.LocalPath = Path.Combine(downloadURL, filename);

//            AsyncTask<bool> asyncJob = new AsyncTask<bool>();
//            asyncJob.SetDownloadCallbackFunctions(modInfo.SpaceportURL, modInfo.LocalPath, finished, progressChanged);
//            asyncJob.RunDownload(); ;
//        }

        /// <summary>
        /// Parses the siteContent for ModInfo data.
        /// </summary>
        /// <param name="siteContent">The KSP Forum sites content for a mod.</param>
        /// <param name="modInfo">The ModInfo to fill with the data of the site.</param>
        private static bool ParseSite(string siteContent, ref ModInfo modInfo)
        {
            #region constants

            const string RELPATH = "var RELPATH = \"threads/";
            const string MINUS = "-";
            const string TITLETAG = "<title>";
            const string TITLEENDTAG = "</title>";
            const string RATING = "<li id=\"threadrating_current\" title=\"Thread Rating: ";
            const string VOTES = " votes,";
            const string AVERAGE = " average.";
            const string LASTEDIT = "Last edited by ";
            const string SEMICOLON = ";";
            const string LINKENDTAG = "</a>";
            const string SPANTAG = "<span class=\"time\">";
            const string SPANENDTAG = "</span>";
            const string AT = " at";
            const string YESTERDAY = "Yesterday";
            const string TODAY = "Today";
            const string DATEFORMAT = "dd.MM.yyyy";
            const string WHITESPACE = " ";
            const string FIRST = "st ";
            const string SECOND = "nd ";
            const string THIRD = "rd ";
            const string TH = "th ";

            #endregion

            // get threadID
            int index = siteContent.IndexOf(RELPATH);
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + RELPATH.Length);
            index = siteContent.IndexOf(MINUS);
            if (index < 0) return false;
            modInfo.ProductID = siteContent.Substring(0, index).Trim();

            //get mod title
            index = siteContent.IndexOf(TITLETAG);
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + TITLETAG.Length);
            index = siteContent.IndexOf(TITLEENDTAG);
            if (index < 0) return false;
            modInfo.Name = siteContent.Substring(0, index).Trim();

            // get rating
            index = siteContent.IndexOf(RATING);
            if (index >= 0)
            {
                siteContent = siteContent.Substring(index + RATING.Length);
                index = siteContent.IndexOf(VOTES);
                string votes = siteContent.Substring(0, index).Trim();
                siteContent = siteContent.Substring(index + VOTES.Length);
                index = siteContent.IndexOf(AVERAGE);
                string rating = siteContent.Substring(0, index).Trim();
                modInfo.Rating = string.Format("{0}({1})", rating, votes);
            }

            // get author
            index = siteContent.IndexOf(LASTEDIT);
            if (index < 0) return false;
            siteContent = siteContent.Substring(index + LASTEDIT.Length);
            index = siteContent.IndexOf(SEMICOLON);
            if (index < 0) return false;
            modInfo.Author = siteContent.Substring(0, index).Trim();
            modInfo.Author = modInfo.Author.Replace(LINKENDTAG, string.Empty).Trim();
            siteContent = siteContent.Substring(index);

            // get edit date
            index = siteContent.IndexOf(SPANENDTAG) - 1;
            if (index < 0) return false;
            string temp = siteContent.Substring(1, index);
            temp = temp.Replace(AT, string.Empty);
            temp = temp.Replace(SPANTAG, string.Empty);
            if (temp.Contains(YESTERDAY))
                temp = temp.Replace(YESTERDAY, DateTime.Now.AddDays(-1).ToString(DATEFORMAT));
            else if (temp.Contains(TODAY))
                temp = temp.Replace(TODAY, DateTime.Now.ToString(DATEFORMAT));
            else
            {
                temp = temp.Replace(FIRST, WHITESPACE);
                temp = temp.Replace(SECOND, WHITESPACE);
                temp = temp.Replace(THIRD, WHITESPACE);
                temp = temp.Replace(TH, WHITESPACE);
            }

            DateTime dtCreationDate = DateTime.Now;
            modInfo.CreationDate = (DateTime.TryParse(temp, out dtCreationDate)) ? dtCreationDate.ToString() : DateTime.Now.ToString();

            // more infos could be parsed here (like: short description, Tab content (overview, installation, ...), comments, ...)
            return true;
        }

        //private static string GetFirstPost(string siteContent)
        //{
        //    const string STARTTAG = "<div class=\"postbody\">";
        //    const string ENDTAG = "<div class=\"after_content\">";

        //    int index = siteContent.IndexOf(STARTTAG);
        //    if (index < 0)
        //        return string.Empty;

        //    siteContent = siteContent.Substring(index);
        //    index = siteContent.IndexOf(ENDTAG);
        //    if (index < 0)
        //        return string.Empty;

        //    return siteContent.Substring(0, index);
        //}


    }
}
