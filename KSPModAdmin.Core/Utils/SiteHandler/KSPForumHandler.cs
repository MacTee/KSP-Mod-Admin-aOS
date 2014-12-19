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
	public class KspForumHandler : ISiteHandler
	{
		private const string cName = "KSP Forum";
		private const string Host = "forum.kerbalspaceprogram.com";

		/// <summary>
		/// Gets the Name of the ISiteHandler.
		/// </summary>
		/// <returns>The Name of the ISiteHandler.</returns>
		public string Name { get { return cName; } }

		/// <summary>
		/// Checks if the passed URL is a KSP Forum URL.
		/// </summary>
		/// <param name="url">The URL to check.</param>
		/// <returns>True if the passed URL is a valid KSP Forum URL, otherwise false.</returns>
		public bool IsValidURL(string url)
		{
			return (!string.IsNullOrEmpty(url) && Host.Equals(new Uri(url).Authority));
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
        /// Gets the content of the site of the passed URL and parses it for ModInfos.
        /// </summary>
        /// <param name="url">The URL of the site to parse the ModInfos from.</param>
        /// <returns>The ModInfos parsed from the site of the passed URL.</returns>
        public ModInfo GetModInfo(string url)
		{
			var modInfo = KSPForum.GetModInfo(url);

			//var modInfo = new ModInfo
			//{
			//	SiteHandlerName = Name,
			//	ModURL = url,
			//	Name = parts[3],
			//	Author = parts[2]
			//};
			//modInfo.CreationDate = kerbalMod.Versions.Last().Date;	// TODO when Adding github tags parser

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
	        return !modInfo.Version.Equals(newModInfo.Version);
        }

		private bool ParseSite(string url, ref ModInfo modInfo)
		{
			// changed to use the curse page as it provides the same info but also game version
			// there's no good way to get a mod version from curse. Could use file name? Is using update date (best method?)
			HtmlWeb web = new HtmlWeb();
			HtmlDocument htmlDoc = web.Load(url);
			htmlDoc.OptionFixNestedTags = true;

			//// To scrape the fields, now using HtmlAgilityPack and XPATH search strings.
			//// Easy way to get XPATH search: use chrome, inspect element, highlight the needed data and right-click and copy XPATH
			//HtmlNode nameNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='pagetitle']/h1/span/a");
			////HtmlNode idNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='project-overview']/div/div[2]/div/div/div[1]/div[2]/ul[2]/li[8]/a");
			//HtmlNode createNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='post_1464892']/div[1]/span[1]/span");

			//HtmlNode updateNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='project-overview']/div/div[2]/div/div/div[1]/div[2]/ul[2]/li[5]/abbr");

			////HtmlNode authorNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id="yui-gen26"]/strong");

			//HtmlNode gameVersionNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='project-overview']/div/div[2]/div/div/div[1]/div[2]/ul[2]/li[3]");

			//var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); //Curse stores the date as both text and as Epoch. Go for the most precise value (Epoch).

			//if (nameNode == null)
			//	return false;

			//modInfo.Name = nameNode.InnerHtml;
			//modInfo.ProductID = idNode.Attributes["href"].Value.Substring(idNode.Attributes["href"].Value.LastIndexOf("/") + 1);
			//modInfo.CreationDateAsDateTime = epoch.AddSeconds(Convert.ToDouble(createNode.Attributes["data-epoch"].Value));
			//modInfo.ChangeDateAsDateTime = epoch.AddSeconds(Convert.ToDouble(updateNode.Attributes["data-epoch"].Value));
			//modInfo.Downloads = downloadNode.InnerHtml.Split(" ")[0];
			//modInfo.Author = authorNode.InnerHtml;
			//modInfo.KSPVersion = gameVersionNode.InnerHtml.Split(" ")[1];
			return true;

			// more infos could be parsed here (like: short description, Tab content (overview, installation, ...), comments, ...)
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

            string downloadUrl = GetDownloadUrl(modInfo);
			modInfo.LocalPath = Path.Combine(OptionsController.DownloadPath, GetDownloadName(downloadUrl));
            www.DownloadFile(downloadUrl, modInfo.LocalPath, downloadProgressHandler);

            return File.Exists(modInfo.LocalPath);
        }

	    /// <summary>
	    /// Returns the plain url to the mod, where the ModInfos would be get from.
	    /// </summary>
	    /// <param name="url">The url to reduce.</param>
	    /// <returns>The plain url to the mod, where the ModInfos would be get from.</returns>
	    public string ReduceToPlainUrl(string url)
	    {
	        return url;
	    }

	    private DateTime GetDateTime(string dateString)
        {
	        var dateParts = dateString.Split(new string[] {"-"}, StringSplitOptions.None);
			return new DateTime(Convert.ToInt32(dateParts[0]), Convert.ToInt32(dateParts[2]), Convert.ToInt32(dateParts[1]));

			//int index = dateString.IndexOf(",") + 2;
			//dateString = dateString.Substring(index);
			//index = dateString.IndexOf(" ") + 1;
			//index = dateString.IndexOf(" ", index) + 1;
			//index = dateString.IndexOf(" ", index) + 1;
			//index = dateString.IndexOf(" ", index);
			//string date = dateString.Substring(0, index);
			//index = dateString.IndexOf("(");
			//string tempTimeZone = dateString.Substring(index).Substring(0);
			//index = tempTimeZone.IndexOf("-");
			//if (index < 0)
			//	index = tempTimeZone.IndexOf("+") + 1;
			//else
			//	index += 1;
			//string timeZone = tempTimeZone.Substring(0, index);
			//tempTimeZone = tempTimeZone.Substring(index);
			//index = tempTimeZone.IndexOf(":");
			//if (index < 2)
			//	timeZone += "0" + tempTimeZone;
			//else
			//	timeZone += tempTimeZone;

			//TimeZoneInfo curseZone = null;
			//foreach (TimeZoneInfo zone in TimeZoneInfo.GetSystemTimeZones())
			//{
			//	if (!zone.DisplayName.StartsWith(timeZone))
			//		continue;

			//	curseZone = zone;
			//	break;
			//}

			//DateTime myDate = DateTime.MinValue;
			//if (DateTime.TryParse(date, out myDate) && curseZone != null)
			//	return TimeZoneInfo.ConvertTime(myDate, curseZone, TimeZoneInfo.Local);
			//else
			//	return DateTime.MinValue;
        }

        private string GetDownloadUrl(ModInfo modInfo)
        {
	        string url;
	        if (!modInfo.ModURL.Contains("releases"))
	        {
				var parts = GetUrlParts(modInfo.ModURL);
				url = parts[0] + "://" + parts[1] + "/" + parts[2] + "/" + parts[3] + "/releases";
	        }
	        else
	        {
		        url = modInfo.ModURL;
	        }

			return url;
        }

		private string GetDownloadName(string url)
		{
			return new Uri(url).Segments.Last();
		}

		/// <summary>
		/// Splits a url into it's segment parts
		/// </summary>
		/// <param name="url">A url to split</param>
		/// <exception cref="ArgumentException"></exception>
		/// <returns>An array of the url segments</returns>
		private List<string> GetUrlParts(string url)
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
