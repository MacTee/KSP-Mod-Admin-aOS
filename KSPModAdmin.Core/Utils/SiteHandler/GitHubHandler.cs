using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using HtmlAgilityPack;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Views;

namespace KSPModAdmin.Core.Utils.SiteHandler
{
	public class GitHubHandler : ISiteHandler
	{
		private const string cName = "GitHub";
		private const string Host = "github.com";
		private const string Host2 = "raw.githubusercontent.com";

		/// <summary>
		/// Gets the Name of the ISiteHandler.
		/// </summary>
		/// <returns>The Name of the ISiteHandler.</returns>
		public string Name { get { return cName; } }

		/// <summary>
		/// Checks if the passed URL is a valid URL for GitHub.
		/// </summary>
		/// <param name="url">The URL to check.</param>
		/// <returns>True if the passed URL is a valid URL, otherwise false.</returns>
		public bool IsValidURL(string url)
		{
			return (!string.IsNullOrEmpty(url) && Host.Equals(new Uri(url).Authority) || Host2.Equals(new Uri(url).Authority));
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
		/// <param name="downloadProgressHandler">Callback function for download progress.</param>
		/// <return>The root node of the added mod, or null.</return>
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
        /// Checks if updates are available for the passed mod.
        /// </summary>
        /// <param name="modInfo">The ModInfos of the mod to check for updates.</param>
        /// <param name="newModInfo">A reference to an empty ModInfo to write the updated ModInfos to.</param>
        /// <returns>True if there is an update, otherwise false.</returns>
        public bool CheckForUpdates(ModInfo modInfo, ref ModInfo newModInfo)
        {
            newModInfo = GetModInfo(modInfo.ModURL);
	        return !modInfo.Version.Equals(newModInfo.Version);
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

	        var downloadInfos = GetDownloadInfo(modInfo);
	        DownloadInfo selected = null;

			if (downloadInfos.Count > 1)
			{
				// create new selection form if more than one download option found
				var dlg = new frmSelectDownload(downloadInfos);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					selected = dlg.SelectedLink;
					dlg.InvalidateView();
				}
			}
			else
			{
				selected = downloadInfos.First();
			}

	        if (selected != null)
	        {
		        string downloadUrl = selected.DownloadURL;
		        modInfo.LocalPath = Path.Combine(OptionsController.DownloadPath, selected.Filename);
		        www.DownloadFile(downloadUrl, modInfo.LocalPath, downloadProgressHandler);
	        }

	        return File.Exists(modInfo.LocalPath);
        }

		/// <summary>
		/// Takes a site url and parses the site for mod info
		/// </summary>
		/// <param name="modInfo">The modInfo to add data to</param>
		public void ParseSite(ref ModInfo modInfo)
		{
			var htmlDoc = new HtmlWeb().Load(GetPathToReleases(modInfo.ModURL));
			htmlDoc.OptionFixNestedTags = true;

			// To scrape the fields, now using HtmlAgilityPack and XPATH search strings.
			// Easy way to get XPATH search: use chrome, inspect element, highlight the needed data and right-click and copy XPATH
			HtmlNode versionNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='js-repo-pjax-container']/div[2]/div[1]/div[1]/ul/li[1]/a/span[2]");
			HtmlNode updateNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='js-repo-pjax-container']/div[2]/div[1]/div[2]/div[1]/p/time");

			if (versionNode == null) return;

			modInfo.Version = versionNode.InnerText;
			modInfo.ChangeDateAsDateTime = DateTime.Parse(updateNode.Attributes["datetime"].Value);
		}

		/// <summary>
		/// Takes a GitHub url and sets it to the shortest path to the project
		/// </summary>
		/// <param name="url">GitHub project url</param>
		/// <returns>Shortest GitHub project url</returns>
		private static string ReduceToPlainUrl(string url)
		{
			var parts = GetUrlParts(url);
			if (parts[1].Equals(Host2))
			{
				return parts[0] + "://www.github.com/" + parts[2] + "/" + parts[3];
			}
			return parts[0] + "://" + parts[1] + "/" + parts[2] + "/" + parts[3];
		}

		/// <summary>
		/// Splits a url into it's segment parts
		/// </summary>
		/// <param name="modUrl">A url to split</param>
		/// <exception cref="ArgumentException"></exception>
		/// <returns>An array of the url segments
		/// 1: Scheme
		/// 2: Authority
		/// 4+: Additional path</returns>
		private static List<string> GetUrlParts(string modUrl)
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
		/// Gets a URL to a repository's releases
		/// </summary>
		/// <param name="modUrl">The URL to resolve</param>
		/// <returns>The resolved URL</returns>
        private static string GetPathToReleases(string modUrl)
        {
	        var url = modUrl;
			if (modUrl.Contains("releases")) return url;

			var parts = GetUrlParts(modUrl);
			url = parts[0] + "://" + parts[1] + "/" + parts[2] + "/" + parts[3] + "/releases";

			return url;
        }
		
		/// <summary>
		/// Gets the download path to the latest release in a repository
		/// </summary>
		/// <param name="modUrl">URL to the repository's releases page</param>
		/// <returns>Direct download path of latest release</returns>
		private static string GetDownloadPath(string modUrl)
		{
			var htmlDoc = new HtmlWeb().Load(modUrl);
			htmlDoc.OptionFixNestedTags = true;
			var partial = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='js-repo-pjax-container']/div[2]/div[1]/div[2]/ul/li[1]/a").Attributes["href"].Value;
			return GetUrlParts(modUrl)[0] + "://" + GetUrlParts(modUrl)[1] + partial;
		}

		/// <summary>
		/// Creates a list of DownloadInfos from a GitHub release
		/// </summary>
		/// <param name="modInfo">The mod to genereate the list from</param>
		/// <returns>A list of one or more DownloadInfos for the most recent release of the selected repository</returns>
		private static List<DownloadInfo> GetDownloadInfo(ModInfo modInfo)
		{
			var htmlDoc = new HtmlWeb().Load(GetPathToReleases(modInfo.ModURL));
			htmlDoc.OptionFixNestedTags = true;

			var releases = new List<DownloadInfo>();

			foreach (var s in htmlDoc.DocumentNode.SelectNodes("//*[@id='js-repo-pjax-container']/div[2]/div[1]/div[2]/ul/li/a[@class='button primary']"))
			{
				var url = "https://github.com" + s.Attributes["href"].Value;
				var dInfo = new DownloadInfo
				{
					DownloadURL = url,
					Filename = GetUrlParts(url).Last(),
					Name = Path.GetFileNameWithoutExtension(GetUrlParts(url).Last())
				};

				releases.Add(dInfo);
			}

			return releases;
		}
    }
}
