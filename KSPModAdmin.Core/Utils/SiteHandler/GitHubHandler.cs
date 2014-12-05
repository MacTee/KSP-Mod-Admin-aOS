using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Threading.Tasks;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils;

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
		/// Checks if the passed URL is a KerbalStuff URL.
		/// </summary>
		/// <param name="url">The URL to check.</param>
		/// <returns>True if the passed URL is a valid KerbalStuff URL, otherwise false.</returns>
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
			var parts = GetUrlParts(url);

			var modInfo = new ModInfo
			{
				SiteHandlerName = Name,
				ModURL = url,
				Name = parts[2],
				Author = parts[1]
			};
			//modInfo.CreationDate = kerbalMod.Versions.Last().Date;	// TODO when KS API supports dates from versions

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

            string downloadUrl = GetDownloadURL(modInfo);

			//string siteContent = www.Load(GetFilesURL(modInfo.ModURL));
			//string filename = GetFileName(siteContent);
			modInfo.LocalPath = Path.Combine(OptionsController.DownloadPath, GetDownloadName(downloadUrl));

            www.DownloadFile(downloadUrl, modInfo.LocalPath, downloadProgressHandler);

            return File.Exists(modInfo.LocalPath);
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

        private string GetDownloadURL(ModInfo modInfo)
        {
			return GitHub.GetDownloadURL(modInfo.ModURL);
        }

		private string GetDownloadName(string url)
		{
			return new Uri(url).Segments.Last();
		}

		/// <summary>
		/// Splits a url into it's segment parts
		/// </summary>
		/// <param name="url">A url to split</param>
		/// <returns>An array of the url segments</returns>
		private string[] GetUrlParts(string url)
		{
			var parts = new Uri(url).Segments;

			for (int index = 0; index < parts.Length; index++)
			{
				parts[index] = parts[index].Trim(new char[] { '/' });
			}

			return parts;
		}
    }
}
