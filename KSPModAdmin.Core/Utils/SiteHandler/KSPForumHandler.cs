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
using KSPModAdmin.Core.Views;

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
			var modInfo = new ModInfo
			{
				SiteHandlerName = Name,
				ModURL = url,
			};
			if (ParseSite(url, ref modInfo))
				return modInfo;
			return null;
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

		private static bool ParseSite(string url, ref ModInfo modInfo)
		{
			// changed to use the curse page as it provides the same info but also game version
			// there's no good way to get a mod version from curse. Could use file name? Is using update date (best method?)
			var htmlDoc = new HtmlWeb().Load(url);
			htmlDoc.OptionFixNestedTags = true;

			// To scrape the fields, now using HtmlAgilityPack and XPATH search strings.
			// Easy way to get XPATH search: use chrome, inspect element, highlight the needed data and right-click and copy XPATH

			// gets name, version, and ID
			HtmlNode nameNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='pagetitle']/h1/span/a");
			HtmlNode authorNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='posts']/li[1]/div[2]/div[1]/div[1]/div[1]/a");
			HtmlNode createNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='posts']/li[1]/div[1]/span[1]");
			HtmlNode updateNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='posts']/li[1]/div[2]/div[2]/div[2]/blockquote[1]");

			//*[@id='posts']/li[1]/div[2]/div[2]/div[1]/div


			if (nameNode == null)
				return false;

			modInfo.Name = nameNode.InnerHtml;
			modInfo.ProductID = new Regex(@".*\/(.*?)-.*").Replace(nameNode.Attributes["href"].Value, "$1");
			modInfo.GameVersion = new Regex(@"\[(.*)\].*").Replace(nameNode.InnerHtml, "$1");
			modInfo.Author = authorNode.InnerText.Trim();

			modInfo.CreationDateAsDateTime = GetDateTime(createNode.InnerText.Trim());
			modInfo.ChangeDateAsDateTime = GetDateTime(updateNode.InnerText.Trim());

			var links = new List<DownloadInfo>();
			foreach (var link in htmlDoc.DocumentNode.SelectNodes("//*[@id='posts']/li[1]/div[2]/div[2]/div/div/div/blockquote//a[@href]"))
			{
				if (Uri.IsWellFormedUriString(link.Attributes["href"].Value, UriKind.Absolute))
				{
					var hostUrl = link.Attributes["href"].Value;
					if (hostUrl != null)
					{
						var dInfo = www.GetDirectDownloadURLFromHostSite(hostUrl);

						if (dInfo.KnownHost)
						{
							if (link.InnerText != null)
								dInfo.Name = link.InnerText;

							links.Add(dInfo);
						}
					}
				}
				
			}

			var dlg = new frmSelectDownload {Links = links};
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				

				dlg.InvalidateView();
			}



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

		private static DateTime GetDateTime(string dateString)
		{
			dateString = HtmlEntity.DeEntitize(dateString);

			DateTime date;

			// Standard creation date and edit date longer than 2 days
			if (DateTime.TryParse(new Regex(@"(st|rd|th|nd|at)").Replace(dateString, ""), out date))
				return date;

			// Prepare an edited date for parsing
			dateString = dateString.Substring(dateString.IndexOf(';') + 1);
			// TODO if forums use localization these strings need localization
			if (dateString.Contains("Today"))
			{
				date = DateTime.Now;
				dateString = dateString.Replace("Today at", "").Trim();
				var time = Convert.ToDateTime(dateString);
				return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
			}

			if (dateString.Contains("Yesterday"))
			{
				date = DateTime.Now.AddDays(-1);
				dateString = dateString.Replace("Yesterday at", "").Trim();
				var time = Convert.ToDateTime(dateString);
				return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
			}

			// If all else fails just make the date today
			return DateTime.Now;
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
			var parts = new List<string> { new Uri(url).Scheme, new Uri(url).Authority };
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
