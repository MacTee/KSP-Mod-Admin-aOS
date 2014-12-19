using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;

namespace KSPModAdmin.Core.Utils.SiteHandler
{
    public class BitbucketHandler : ISiteHandler
    {
		private const string cName = "Bitbucket";
		private const string Host = "bitbucket.org";


        /// <summary>
        /// Gets the Name of the ISiteHandler.
        /// </summary>
        /// <returns>The Name of the ISiteHandler.</returns>
        public string Name { get { return cName; } }


        /// <summary>
        /// Checks if the passed URL is a valid URL for Bitbucket.
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
			var modInfo = new ModInfo
			{
				SiteHandlerName = Name,
				ModURL = ReduceToPlainUrl(url)
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
        /// <param name="newModInfo">A reference to a empty ModInfo to write the updated ModInfos to.</param>
        /// <returns>True if there is an update, otherwise false.</returns>
        public bool CheckForUpdates(ModInfo modInfo, ref ModInfo newModInfo)
        {
            newModInfo = GetModInfo(modInfo.ModURL);
	        return modInfo.ChangeDateAsDateTime == newModInfo.ChangeDateAsDateTime;
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

			string downloadUrl = GetDownloadPath(GetPathToDownloads(modInfo.ModURL));
			modInfo.LocalPath = Path.Combine(OptionsController.DownloadPath, downloadUrl.Split("/").Last());
			www.DownloadFile(downloadUrl, modInfo.LocalPath, downloadProgressHandler);

			return File.Exists(modInfo.LocalPath);
        }

        /// <summary>
        /// Takes a Bitbucket url and sets it to the shortest path to the project
        /// </summary>
        /// <param name="modUrl">Bitbucket project url</param>
        /// <returns>Shortest Bitbucket project url</returns>
        public string ReduceToPlainUrl(string modUrl)
        {
            var parts = GetUrlParts(modUrl);
            return parts[0] + "://" + parts[1] + "/" + parts[2] + "/" + parts[3];
        }


		/// <summary>
		/// Loads a mod's source page and extracts mod info from it
		/// </summary>
		/// <param name="modInfo">A mod to add info to</param>
	    private static void ParseSite(ref ModInfo modInfo)
	    {
			var htmlDoc = new HtmlWeb().Load(GetPathToDownloads(modInfo.ModURL));
			htmlDoc.OptionFixNestedTags = true;

			// To scrape the fields, now using HtmlAgilityPack and XPATH search strings.
			// Easy way to get XPATH search: use chrome, inspect element, highlight the needed data and right-click and copy XPATH
			HtmlNode authorNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='repo-owner-link']");
			HtmlNode nameNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='content']/div[1]/div[1]/div[1]/header/div/div[2]/h1/a");
			HtmlNode updateNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='uploaded-files']/tbody/tr[2]/td[5]/div/time");
			HtmlNode downloadNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='uploaded-files']/tbody/tr[2]/td[4]");

		    modInfo.Author = authorNode.InnerHtml;
		    modInfo.Name = nameNode.InnerHtml;
			modInfo.ChangeDateAsDateTime = DateTime.Parse(updateNode.Attributes["datetime"].Value);
		    modInfo.Downloads = downloadNode.InnerHtml;
	    }

		/// <summary>
		/// Splits a url into it's segment parts
		/// </summary>
		/// <param name="modUrl">A url to split</param>
		/// <exception cref="ArgumentException"></exception>
		/// <returns>An array of the url segments</returns>
		private static List<string> GetUrlParts(string modUrl)
		{
			// Split the url into parts
			var parts = new List<string> { new Uri(modUrl).Scheme, new Uri(modUrl).Authority };
			parts.AddRange(new Uri(modUrl).Segments);

			for (int index = 0; index < parts.Count; index++)
			{
				parts[index] = parts[index].Trim(new char[] { '/' });
			}

			// Remove empty parts from the list
			parts = parts.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

			// TODO Error message should go wherever strings are going.
			if (parts.Count < 4)
				throw new System.ArgumentException("Bitbucket URL must point to a repository.");

			return parts;
		}

		/// <summary>
		/// Takes a Bitbucket url and determines if it points to the download path
		/// </summary>
		/// <param name="modUrl">URL to a Bitbucket repository</param>
		/// <returns>A url that points to the downloads section of a Bitbucket repository</returns>
	    private static string GetPathToDownloads(string modUrl)
	    {
			var url = modUrl;
			if (modUrl.Contains("downloads")) return url;

			var parts = GetUrlParts(modUrl);
			url = parts[0] + "://" + parts[1] + "/" + parts[2] + "/" + parts[3] + "/downloads";

			return url;
	    }

		/// <summary>
		/// Gets the file name of the latest download from a Bitbucket repository
		/// </summary>
		/// <param name="modUrl">URL to a Bitbucket repository</param>
		/// <returns>The name of the latest file in the repository</returns>
		private static string GetDownloadPath(string modUrl)
		{
			var htmlDoc = new HtmlWeb().Load(GetPathToDownloads(modUrl));
			htmlDoc.OptionFixNestedTags = true;
			var partial = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='uploaded-files']/tbody/tr[2]/td[1]").Attributes["href"].Value;
			return GetUrlParts(modUrl)[0] + "://" + GetUrlParts(modUrl)[1] + partial;
		}
    }
}
