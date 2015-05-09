using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using Newtonsoft.Json.Linq;

namespace KSPModAdmin.Core.Utils.SiteHandler
{
    /// <summary>
    /// Handles the GetModInfo and Mod download for mods on KerbalStuff.
    /// </summary>
    public class KerbalStuffHandler : ISiteHandler
    {
        private const string NAME = "KerbalStuff";
        private const string URL = "http://kerbalstuff.com/";
        private const string URL2 = "https://kerbalstuff.com/";
        private const string URL3 = "http://beta.kerbalstuff.com/"; // some mods still have this as the link
        private const string MODINFO_URL = "https://kerbalstuff.com/api/mod/";


        /// <summary>
        /// Gets the Name of the ISiteHandler.
        /// </summary>
        /// <returns>The Name of the ISiteHandler.</returns>
        public string Name { get { return NAME; } }


        /// <summary>
        /// Checks if the passed URL is a KerbalStuff URL.
        /// </summary>
        /// <param name="url">The URL to check.</param>
        /// <returns>True if the passed URL is a valid KerbalStuff URL, otherwise false.</returns>
        public bool IsValidURL(string url)
        {
            // Should we perhaps put valid hostnames into an array? Would keep the next line growing out of control 
            return (!string.IsNullOrEmpty(url) && (url.ToLower().StartsWith(URL) || url.ToLower().StartsWith(URL2) || url.ToLower().StartsWith(URL3)));
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
            string modInfoUrl = MODINFO_URL + (url.Split(new string[] { "/" }, StringSplitOptions.None).ToList())[4];

            if (string.IsNullOrEmpty(modInfoUrl))
                return null;

            string content = Www.Load(modInfoUrl);
            if (string.IsNullOrEmpty(content))
                return null;

            JObject jObject = JObject.Parse(content);

            var modInfo = new ModInfo
            {
                SiteHandlerName = Name,
                ModURL = url,
                ProductID = GetString(jObject["id"]),
                Name = GetString(jObject["name"]),
                Downloads = GetString(jObject["downloads"]),
                Author = GetString(jObject["author"]),
                Version = GetVersion(jObject["versions"] as JToken),
                KSPVersion = GetKSPVersion(jObject["versions"] as JToken)
            };
            ////modInfo.CreationDate = kerbalMod.Versions.Last().Date; // TODO when KS API supports dates from versions

            return modInfo;
        }

        private static string GetString(JToken jToken)
        {
            if (jToken == null)
                return string.Empty;

            return (string)jToken;
        }

        private static string GetVersion(JToken jToken)
        {
            if (jToken == null)
                return string.Empty;

            string version = string.Empty;
            foreach (var child in jToken.Children<JObject>())
            {
                if (child["friendly_version"] != null)
                {
                    version = child["friendly_version"].ToString();
                    break;
                }
                
                // just inspect the first child.
                break;
            }

            return version;
        }

        private static string GetKSPVersion(JToken jToken)
        {
            if (jToken == null)
                return string.Empty;

            string version = string.Empty;
            foreach (var child in jToken.Children<JObject>())
            {
                if (child["ksp_version"] != null)
                {
                    version = child["ksp_version"].ToString();
                    break;
                }
                
                // just inspect the first child.
                break;
            }

            return version;
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
            if (modInfo.Version == null)
            {
                return true;
            }
            else if (!modInfo.Version.Equals(newModInfo.Version))
            {
                return true;
            }

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
            if (modInfo == null)
                return false;

            string downloadUrl = GetDownloadURL(modInfo);

            ////string siteContent = www.Load(GetFilesURL(modInfo.ModURL));
            ////string filename = GetFileName(siteContent);
            modInfo.LocalPath = Path.Combine(OptionsController.DownloadPath, GetDownloadName(modInfo));

            Www.DownloadFile(downloadUrl, modInfo.LocalPath, downloadProgressHandler);

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
            var dateParts = dateString.Split(new string[] { "-" }, StringSplitOptions.None);
            return new DateTime(Convert.ToInt32(dateParts[0]), Convert.ToInt32(dateParts[2]), Convert.ToInt32(dateParts[1]));

            ////int index = dateString.IndexOf(",") + 2;
            ////dateString = dateString.Substring(index);
            ////index = dateString.IndexOf(" ") + 1;
            ////index = dateString.IndexOf(" ", index) + 1;
            ////index = dateString.IndexOf(" ", index) + 1;
            ////index = dateString.IndexOf(" ", index);
            ////string date = dateString.Substring(0, index);
            ////index = dateString.IndexOf("(");
            ////string tempTimeZone = dateString.Substring(index).Substring(0);
            ////index = tempTimeZone.IndexOf("-");
            ////if (index < 0)
            ////    index = tempTimeZone.IndexOf("+") + 1;
            ////else
            ////    index += 1;
            ////string timeZone = tempTimeZone.Substring(0, index);
            ////tempTimeZone = tempTimeZone.Substring(index);
            ////index = tempTimeZone.IndexOf(":");
            ////if (index < 2)
            ////    timeZone += "0" + tempTimeZone;
            ////else
            ////    timeZone += tempTimeZone;

            ////TimeZoneInfo curseZone = null;
            ////foreach (TimeZoneInfo zone in TimeZoneInfo.GetSystemTimeZones())
            ////{
            ////    if (!zone.DisplayName.StartsWith(timeZone))
            ////        continue;

            ////    curseZone = zone;
            ////    break;
            ////}

            ////DateTime myDate = DateTime.MinValue;
            ////if (DateTime.TryParse(date, out myDate) && curseZone != null)
            ////    return TimeZoneInfo.ConvertTime(myDate, curseZone, TimeZoneInfo.Local);
            ////else
            ////    return DateTime.MinValue;
        }

        private string GetDownloadURL(ModInfo modInfo)
        {
            return modInfo.ModURL.EndsWith("/") ? modInfo.ModURL + "download/" + modInfo.Version : modInfo.ModURL + "/download/" + modInfo.Version;
        }

        private string GetDownloadName(ModInfo modInfo)
        {
            return (modInfo.Name.Replace(' ', '_') + '-' + modInfo.Version + ".zip");
        }
    }
}
