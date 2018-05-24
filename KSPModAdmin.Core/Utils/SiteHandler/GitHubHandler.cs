using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HtmlAgilityPack;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Views;

namespace KSPModAdmin.Core.Utils.SiteHandler
{
    /// <summary>
    /// Handles the GetModInfo and Mod download for mods on GitHub.
    /// </summary>
    public class GitHubHandler : ISiteHandler
    {
        #region Constants

        private const string NAME = "GitHub";
        private const string HOST = "github.com";
        private const string URL_0_1 = "https://github.com/{0}/{1}";
        private const string HOST2 = "raw.githubusercontent.com";

        #endregion

        /// <summary>
        /// Builds the url from the passed user and project name.
        /// </summary>
        /// <param name="userName">Name of the user from the GitHub repository.</param>
        /// <param name="projectName">Name of the project from the GitHub repository.</param>
        /// <returns>The build GitHub project URL or empty string.</returns>
        public static string GetProjectUrl(string userName, string projectName)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(projectName))
                return string.Format(URL_0_1, userName, projectName);

            return string.Empty;
        }

        /// <summary>
        /// Gets the Name of the ISiteHandler.
        /// </summary>
        /// <returns>The Name of the ISiteHandler.</returns>
        public string Name { get { return NAME; } }


        /// <summary>
        /// Checks if the passed URL is a valid URL for GitHub.
        /// </summary>
        /// <param name="url">The URL to check.</param>
        /// <returns>True if the passed URL is a valid URL, otherwise false.</returns>
        public bool IsValidURL(string url)
        {
            return !string.IsNullOrEmpty(url) && (HOST.Equals(new Uri(url).Authority) || HOST2.Equals(new Uri(url).Authority));
        }

        /// <summary>
        /// Gets the content of the site of the passed URL and parses it for ModInfos.
        /// </summary>
        /// <param name="url">The URL of the site to parse the ModInfos from.</param>
        /// <returns>The ModInfos parsed from the site of the passed URL.</returns>
        public ModInfo GetModInfo(string url)
        {
            var parts = GetUrlParts(url);
            var modInfo = new ModInfo
            {
                SiteHandlerName = Name,
                ModURL = ReduceToPlainUrl(url),
                Name = parts[3],
                Author = parts[2]
            };
            ParseSite(ref modInfo);
            return modInfo;
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
        /// Checks if updates are available for the passed mod.
        /// </summary>
        /// <param name="modInfo">The ModInfos of the mod to check for updates.</param>
        /// <param name="newModInfo">A reference to an empty ModInfo to write the updated ModInfos to.</param>
        /// <returns>True if there is an update, otherwise false.</returns>
        public bool CheckForUpdates(ModInfo modInfo, ref ModInfo newModInfo)
        {
            newModInfo = GetModInfo(modInfo.ModURL);
            if (string.IsNullOrEmpty(modInfo.Version) && !string.IsNullOrEmpty(newModInfo.Version))
                return true;
            else if (!string.IsNullOrEmpty(modInfo.Version) && !string.IsNullOrEmpty(newModInfo.Version))
                return (VersionComparer.CompareVersions(modInfo.Version, newModInfo.Version) == VersionComparer.Result.AisSmallerB);
            else if (string.IsNullOrEmpty(modInfo.CreationDate) && !string.IsNullOrEmpty(newModInfo.CreationDate))
                return true;
            else if (!string.IsNullOrEmpty(modInfo.CreationDate) && !string.IsNullOrEmpty(newModInfo.CreationDate))
                return modInfo.CreationDateAsDateTime < newModInfo.CreationDateAsDateTime;

            return false;
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

            var downloadInfos = GetDownloadInfo(modInfo);
            DownloadInfo selected = null;

            if (downloadInfos.Count > 1)
            {
                // create new selection form if more than one download option found
                var dlg = new frmSelectDownload(downloadInfos);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    selected = dlg.SelectedLink;
                }
            }
            else if (downloadInfos.Count == 1)
            {
                selected = downloadInfos.First();
            }
            else
            {
                string msg = string.Format(Messages.MSG_NO_BINARY_DOWNLOAD_FOUND_AT_0, modInfo.SiteHandlerName);
                MessageBox.Show(msg, Messages.MSG_TITLE_ERROR);
                Messenger.AddDebug(msg);
                return false;
            }

            if (selected != null)
            {
                string downloadUrl = selected.DownloadURL;
                modInfo.LocalPath = Path.Combine(OptionsController.DownloadPath, selected.Filename);
                Www.DownloadFile(downloadUrl, modInfo.LocalPath, downloadProgressCallback);
            }

            return File.Exists(modInfo.LocalPath);
        }

        /// <summary>
        /// Takes a GitHub url and sets it to the shortest path to the project
        /// </summary>
        /// <param name="url">GitHub project url</param>
        /// <returns>Shortest GitHub project url</returns>
        public string ReduceToPlainUrl(string url)
        {
            var parts = GetUrlParts(url);
            if (parts[1].Equals(HOST2))
            {
                return parts[0] + "://www.github.com/" + parts[2] + "/" + parts[3];
            }
            return parts[0] + "://" + parts[1] + "/" + parts[2] + "/" + parts[3];
        }

        /// <summary>
        /// Splits a url into it's segment parts
        /// </summary>
        /// <param name="modUrl">A url to split</param>
        /// <exception cref="ArgumentException">ArgumentException("GitHub URL must point to a repository.")</exception>
        /// <returns>An array of the url segments
        /// 1: Scheme
        /// 2: Authority
        /// 4+: Additional path</returns>
        public static List<string> GetUrlParts(string modUrl)
        {
            // Split the url into parts
            var parts = new List<string>
            {
                new Uri(modUrl).Scheme, 
                new Uri(modUrl).Authority
            };

            parts.AddRange(new Uri(modUrl).Segments);

            for (int index = 0; index < parts.Count; index++)
            {
                parts[index] = parts[index].Trim(new char[] { '/' });
            }

            // Remove empty parts from the list
            parts = parts.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

            // TODO Error message should go wherever strings are going.
            if (parts.Count < 4)
                throw new System.ArgumentException("GitHub URL must point to a repository.");

            return parts;
        }

        /// <summary>
        /// Takes a site url and parses the site for mod info
        /// </summary>
        /// <param name="modInfo">The modInfo to add data to</param>
        private void ParseSite(ref ModInfo modInfo)
        {
            try
            {
                var htmlDoc = new HtmlWeb().Load(GetPathToReleases(modInfo.ModURL));
                htmlDoc.OptionFixNestedTags = true;

                var version = GitHubParser.GetVersion(htmlDoc);
                var changeDate = GitHubParser.GetChangeDate(htmlDoc);

                if (string.IsNullOrEmpty(version) || changeDate == DateTime.MinValue)
                {
                    Messenger.AddError("Error! Can't parse Version or ChangeDate from gitHub repository!");
                    return;
                }

                modInfo.Version = version;
                modInfo.ChangeDateAsDateTime = changeDate;
            }
            catch (Exception ex)
            {
                Messenger.AddError("Error! Can't parse GitHub repository content!", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Gets a URL to a repository's releases
        /// </summary>
        /// <param name="modUrl">The URL to resolve</param>
        /// <returns>The resolved URL</returns>
        private string GetPathToReleases(string modUrl)
        {
            var url = modUrl;
            if (modUrl.Contains("releases")) return url;

            var parts = GetUrlParts(modUrl);
            url = parts[0] + "://" + parts[1] + "/" + parts[2] + "/" + parts[3] + "/releases";

            return url;
        }

        /// <summary>
        /// Creates a list of DownloadInfos from a GitHub release
        /// </summary>
        /// <param name="modInfo">The mod to generate the list from</param>
        /// <returns>A list of one or more DownloadInfos for the most recent release of the selected repository</returns>
        private List<DownloadInfo> GetDownloadInfo(ModInfo modInfo)
        {
            try
            {
                var htmlDoc = new HtmlWeb().Load(GetPathToReleases(modInfo.ModURL));
                htmlDoc.OptionFixNestedTags = true;
                return GitHubParser.GetDownloadInfos(htmlDoc);
            }
            catch (Exception ex)
            {
                Messenger.AddError("Error! Can't parse GitHib for binaries!", ex);
                throw ex;
            }
        }
    }

    public class GitHubParser
    {
        #region XPath

        private static readonly string XPATHGITHUBTIMELINE = "xPathGitHubTimeLine";
        private static readonly string XPATHGITHUBLABEL = "xPathGitHubLabel";
        private static readonly string XPATHGITHUBTAGS = "xPathGitHubTags";
        private static readonly string XPATHGITHUBTAGS2 = "xPathGitHubTags2";
        private static readonly string XPATHGITHUBLABELVERSION = "xPathGitHubLabelVersion";
        private static readonly string XPATHGITHUBLABELDATE = "xPathGitHubLabelDate";
        private static readonly string XPATHGITHUBTAGSVERSION = "xPathGitHubTagsVersion";
        private static readonly string XPATHGITHUBTAGSDATE = "xPathGitHubTagsDate";
        private static readonly string XPATHGITHUBTAGS2VERSION = "xPathGitHubTags2Version";
        private static readonly string XPATHGITHUBTAGS2DATE = "xPathGitHubTags2Date";
        private static readonly string XPATHGITHUBLABELRELEASESLATEST = "xPathGithubLabelReleasesLatest";
        private static readonly string XPATHGITHUBLABELRELEASES = "xPathGithubLabelReleases";
        private static readonly string XPATHGITHUBTAGSRELEASES = "xPathGithubTagsReleases";
        private static readonly string XPATHGITHUBTAGS2RELEASES = "xPathGithubTags2Releases";

        private static string xPathGitHubTimeLine
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGITHUBTIMELINE))
                    OptionsController.OtherAppOptions.Add(XPATHGITHUBTIMELINE, "//*[@id='js-repo-pjax-container']/div[2]/div[1]/div[2]");
                return OptionsController.OtherAppOptions[XPATHGITHUBTIMELINE];
            }
        }
        private static string xPathGitHubLabel
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGITHUBLABEL))
                    OptionsController.OtherAppOptions.Add(XPATHGITHUBLABEL, "//*[@class='release clearfix label-latest']");
                return OptionsController.OtherAppOptions[XPATHGITHUBLABEL];
            }
        }
        private static string xPathGitHubTags
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGITHUBTAGS))
                    OptionsController.OtherAppOptions.Add(XPATHGITHUBTAGS, "//*[@class='releases-tag-list']");
                return OptionsController.OtherAppOptions[XPATHGITHUBTAGS];
            }
        }
        private static string xPathGitHubTags2
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGITHUBTAGS2))
                    OptionsController.OtherAppOptions.Add(XPATHGITHUBTAGS2, "//*[@class='release-timeline-tags list-style-none']");
                return OptionsController.OtherAppOptions[XPATHGITHUBTAGS2];
            }
        }
        private static string xPathGitHubLabelVersion
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGITHUBLABELVERSION))
                    OptionsController.OtherAppOptions.Add(XPATHGITHUBLABELVERSION, "//*[@class='release label-latest']/div/ul/li/a/span");
                return OptionsController.OtherAppOptions[XPATHGITHUBLABELVERSION];
            }
        }
        private static string xPathGitHubLabelDate
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGITHUBLABELDATE))
                    OptionsController.OtherAppOptions.Add(XPATHGITHUBLABELDATE, "//*[@class='release label-latest']/div[2]/div/p/relative-time");
                return OptionsController.OtherAppOptions[XPATHGITHUBLABELDATE];
            }
        }
        private static string xPathGitHubTagsVersion
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGITHUBTAGSVERSION))
                    OptionsController.OtherAppOptions.Add(XPATHGITHUBTAGSVERSION, "//*/tr[2]/td[2]/div/h3/a/span");
                return OptionsController.OtherAppOptions[XPATHGITHUBTAGSVERSION];
            }
        }
        private static string xPathGitHubTagsDate
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGITHUBTAGSDATE))
                    OptionsController.OtherAppOptions.Add(XPATHGITHUBTAGSDATE, "//*/tr[2]/td[1]/a/relative-time");
                return OptionsController.OtherAppOptions[XPATHGITHUBTAGSDATE];
            }
        }
        private static string xPathGitHubTags2Version
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGITHUBTAGS2VERSION))
                    OptionsController.OtherAppOptions.Add(XPATHGITHUBTAGS2VERSION, "//*/li[1]/div/div/h3/a/span");
                return OptionsController.OtherAppOptions[XPATHGITHUBTAGS2VERSION];
            }
        }
        private static string xPathGitHubTags2Date
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGITHUBTAGS2DATE))
                    OptionsController.OtherAppOptions.Add(XPATHGITHUBTAGS2DATE, "//*/li/span/relative-time");
                return OptionsController.OtherAppOptions[XPATHGITHUBTAGS2DATE];
            }
        }
        private static string xPathGithubLabelReleasesLatest
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGITHUBLABELRELEASESLATEST))
                    OptionsController.OtherAppOptions.Add(XPATHGITHUBLABELRELEASESLATEST, "//*[@class='release clearfix label-latest']/div[2]/div[2]/ul/li[1]/a");
                return OptionsController.OtherAppOptions[XPATHGITHUBLABELRELEASESLATEST];
            }
        }
        private static string xPathGithubLabelReleases
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGITHUBLABELRELEASES))
                    OptionsController.OtherAppOptions.Add(XPATHGITHUBLABELRELEASES, "//*[@class='release clearfix label-']/div[2]/div[2]/ul/li[1]/a");
                return OptionsController.OtherAppOptions[XPATHGITHUBLABELRELEASES];
            }
        }
        private static string xPathGithubTagsReleases
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGITHUBTAGSRELEASES))
                    OptionsController.OtherAppOptions.Add(XPATHGITHUBTAGSRELEASES, "//*/tr/td[2]/div/ul/li[2]/a");
                return OptionsController.OtherAppOptions[XPATHGITHUBTAGSRELEASES];
            }
        }
        private static string xPathGithubTags2Releases
        {
            get
            {
                if (!OptionsController.OtherAppOptions.ContainsKey(XPATHGITHUBTAGS2RELEASES))
                    OptionsController.OtherAppOptions.Add(XPATHGITHUBTAGS2RELEASES, "//*[@class='d-block clearfix']/div/div/ul/li[2]/a");
                return OptionsController.OtherAppOptions[XPATHGITHUBTAGS2RELEASES];
            }
        }

        #endregion

        public static List<DownloadInfo> GetDownloadInfos(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            var releases = new List<DownloadInfo>();

            // get the node that contains all releases (indepandend of label or tags view)
            // Easy way to get XPATH search: use chrome, inspect element, highlight the needed data, right-click and copy XPATH
            var timeLineNode = htmlDoc.DocumentNode.SelectSingleNode(xPathGitHubTimeLine);
            var lableNode = timeLineNode.SelectSingleNode(xPathGitHubLabel);
            var tagsNode = timeLineNode.SelectSingleNode(xPathGitHubTags);
            var tagsNode2 = timeLineNode.SelectSingleNode(xPathGitHubTags2);

            // get data from label or tag view.
            HtmlNodeCollection releaseNodes = null;
            if (lableNode != null)
            {
                // try find last release (select all link nodes of the download section within the class 'release label-latest')
                releaseNodes = lableNode.SelectNodes(xPathGithubLabelReleasesLatest);

                // try find other releases (select all link nodes of the download section within the classes 'release label-')
                if (releaseNodes == null)
                    releaseNodes = lableNode.SelectNodes(xPathGithubLabelReleases);
            }
            else if (tagsNode != null)
            {
                // try find releases (select all link nodes of the download section within the class 'releases-tag-list')
                releaseNodes = tagsNode.SelectNodes(xPathGithubTagsReleases);
            }
            else //if (tagsNode2 != null)
            {
                // try find releases (select all link nodes of the download section within the class 'release-timeline-tags list-style-none')
                releaseNodes = tagsNode2.SelectNodes(xPathGithubTags2Releases);
            }

            if (releaseNodes != null)
            {
                // iterate over all link nodes and get only urls with 'releases' in it.
                foreach (var s in releaseNodes)
                {
                    var url = "https://github.com" + s.Attributes["href"].Value;

                    if (!url.Contains("releases") && !url.Contains("archive")) continue;

                    var dInfo = new DownloadInfo
                    {
                        DownloadURL = url,
                        Filename = GitHubHandler.GetUrlParts(url).Last(),
                        Name = Path.GetFileNameWithoutExtension(GitHubHandler.GetUrlParts(url).Last())
                    };

                    releases.Add(dInfo);
                }
            }

            return releases;
        }

        public static string GetVersion(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            // get the node that contains all releases (indepandend of label or tags view)
            // Easy way to get XPATH search: use chrome, inspect element, highlight the needed data, right-click and copy XPATH
            var timeLineNode = htmlDoc.DocumentNode.SelectSingleNode(xPathGitHubTimeLine);
            var lableNode = timeLineNode.SelectSingleNode(xPathGitHubLabel);
            var tagsNode = timeLineNode.SelectSingleNode(xPathGitHubTags);
            var tagsNode2 = timeLineNode.SelectSingleNode(xPathGitHubTags2);

            if (lableNode == null && tagsNode == null && tagsNode2 == null)
            {
                return null;
            }

            // get data from label or tag view.
            HtmlNode versionNode = null;
            if (lableNode != null)
            {
                versionNode = lableNode.SelectSingleNode(xPathGitHubLabelVersion);
            }
            else if (tagsNode != null)
            {
                versionNode = tagsNode.SelectSingleNode(xPathGitHubTagsVersion);
            }
            else //if (tagsNode2 != null)
            {
                versionNode = tagsNode2.SelectSingleNode(xPathGitHubTags2Version);
            }

            if (versionNode != null)
                return Regex.Replace(versionNode.InnerText, @"[A-z]", string.Empty);

            return null;
        }

        public static DateTime GetChangeDate(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            var changeDate = DateTime.MinValue;

            // get the node that contains all releases (indepandend of label or tags view)
            // Easy way to get XPATH search: use chrome, inspect element, highlight the needed data, right-click and copy XPATH
            var timeLineNode = htmlDoc.DocumentNode.SelectSingleNode(xPathGitHubTimeLine);
            var lableNode = timeLineNode.SelectSingleNode(xPathGitHubLabel);
            var tagsNode = timeLineNode.SelectSingleNode(xPathGitHubTags);
            var tagsNode2 = timeLineNode.SelectSingleNode(xPathGitHubTags2);

            if (lableNode == null && tagsNode == null && tagsNode2 == null)
            {
                return changeDate;
            }

            // get data from label or tag view.
            HtmlNode updateNode = null;
            if (lableNode != null)
            {
                updateNode = lableNode.SelectSingleNode(xPathGitHubLabelDate);
            }
            else if (tagsNode != null)
            {
                updateNode = tagsNode.SelectSingleNode(xPathGitHubTagsDate);
            }
            else //if (tagsNode2 != null)
            {
                updateNode = tagsNode2.SelectSingleNode(xPathGitHubTags2Date);
            }

            if (updateNode != null)
                changeDate = DateTime.Parse(updateNode.Attributes["datetime"].Value);

            return changeDate;
        }
    }
}
