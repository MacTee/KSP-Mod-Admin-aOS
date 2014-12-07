using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;

namespace KSPModAdmin.Core.Utils.SiteHandler
{
	public class GitHubHandler : ISiteHandler
	{
		private const string cName = "GitHub";
		private const string Host = "github.com";

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
			return (!string.IsNullOrEmpty(url) && Host.Equals(new Uri(url).Authority));
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
				ModURL = url,
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

            string downloadUrl = GetDownloadUrl(modInfo.ModURL);
			modInfo.LocalPath = Path.Combine(OptionsController.DownloadPath, GetDownloadName(downloadUrl));
            www.DownloadFile(downloadUrl, modInfo.LocalPath, downloadProgressHandler);

            return File.Exists(modInfo.LocalPath);
        }

		public void ParseSite(ref ModInfo modInfo)
		{
			var htmlDoc = new HtmlWeb().Load(modInfo.ModURL);
			htmlDoc.OptionFixNestedTags = true;

			// To scrape the fields, now using HtmlAgilityPack and XPATH search strings.
			// Easy way to get XPATH search: use chrome, inspect element, highlight the needed data and right-click and copy XPATH
			HtmlNode versionNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='js-repo-pjax-container']/div[2]/div[1]/div[1]/ul/li[1]/a/span[2]");
			HtmlNode updateNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='js-repo-pjax-container']/div[2]/div[1]/div[2]/div[1]/p/time");
			HtmlNode downloadNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='js-repo-pjax-container']/div[2]/div[1]/div[2]/ul/li[1]/a");

			if (versionNode == null) return;

			modInfo.Version = versionNode.InnerText;
			modInfo.ChangeDateAsDateTime = GetDateTime(updateNode.Attributes["datetime"].Value);
		}

        private static DateTime GetDateTime(string dateString)
        {
	        var date = dateString.Split('T')[0].ToString();
			var dtfi = new DateTimeFormatInfo {ShortDatePattern = "yyyy-MM-dd", DateSeparator = "-"};
	        return Convert.ToDateTime(date, dtfi);
        }

        private static string GetDownloadUrl(string modUrl)
        {
	        string url = modUrl;
			if (!modUrl.Contains("releases"))
	        {
				var parts = GetUrlParts(modUrl);
				url = parts[0] + "://" + parts[1] + "/" + parts[2] + "/" + parts[3] + "/releases";
	        }

			return GitHub.GetDownloadURL(url);
        }

		private static string GetDownloadName(string url)
		{
			return new Uri(url).Segments.Last();
		}

		/// <summary>
		/// Splits a url into it's segment parts
		/// </summary>
		/// <param name="url">A url to split</param>
		/// <exception cref="ArgumentException"></exception>
		/// <returns>An array of the url segments</returns>
		private static List<string> GetUrlParts(string url)
		{
			// Split the url into parts
			var parts = new List<string> {new Uri(url).Scheme, new Uri(url).Authority};
			parts.AddRange(new Uri(url).Segments);

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
    }
}
