using System;
using System.IO;
using System.Linq;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using Newtonsoft.Json.Linq;

namespace KSPModAdmin.Core.Utils.SiteHandler
{
    /// <summary>
    /// Handles the GetModInfo and Mod download for mods on Spacedock.
    /// </summary>
    public class SpacedockHandler : ISiteHandler
    {
        private const string NAME = "Spacedock";
        private const string MODINFO_URL = "https://spacedock.info/api/mod/";
        private static string[] VALIDURLS = new string[]
        {
            "http://www.spacedock.info/",
            "http://spacedock.info/",
            "https://www.spacedock.info/",
            "https://spacedock.info/",
        };


        /// <summary>
        /// Gets the Name of the ISiteHandler.
        /// </summary>
        /// <returns>The Name of the ISiteHandler.</returns>
        public string Name { get { return NAME; } }


        /// <summary>
        /// Checks if the passed URL is a Spacedock URL.
        /// </summary>
        /// <param name="url">The URL to check.</param>
        /// <returns>True if the passed URL is a valid Spacedock URL, otherwise false.</returns>
        public bool IsValidURL(string url)
        {
            return (!string.IsNullOrEmpty(url) && VALIDURLS.Any(x => url.StartsWith(x, StringComparison.CurrentCultureIgnoreCase)));
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

            var modInfo = new ModInfo
            {
                SiteHandlerName = Name,
                ModURL = url,
                AdditionalURL = SpacedockParser.GetAdditionalURL(content),
                ProductID = SpacedockParser.GetProductID(content),
                Name = SpacedockParser.GetModName(content),
                Downloads = SpacedockParser.GetDownloadCount(content),
                Author = SpacedockParser.GetAuthor(content),
                Version = SpacedockParser.GetVersion(content),
                KSPVersion = SpacedockParser.GetKSPVersion(content),
                Note = SpacedockParser.GetNote(content)
            };
            ////modInfo.CreationDate = kerbalMod.Versions.Last().Date; // TODO when KS API supports dates from versions

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
            return (VersionComparer.CompareVersions(modInfo.Version, newModInfo.Version) == VersionComparer.Result.AisSmallerB);
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

            modInfo.LocalPath = Path.Combine(OptionsController.DownloadPath, GetDownloadName(modInfo));

            string downloadUrl = GetDownloadURL(modInfo);
            Www.DownloadFile(downloadUrl, modInfo.LocalPath, downloadProgressCallback);

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

        private string GetDownloadURL(ModInfo modInfo)
        {
            return modInfo.ModURL.EndsWith("/") ? modInfo.ModURL + "download/" + modInfo.Version : modInfo.ModURL + "/download/" + modInfo.Version;
        }

        private string GetDownloadName(ModInfo modInfo)
        {
            return (modInfo.Name.Replace(' ', '_') + '-' + modInfo.Version + ".zip");
        }
    }

    public class SpacedockParser
    {
        public static string GetAdditionalURL(string content)
        {
            JObject jObject = JObject.Parse(content);
            return GetString(jObject["website"]);
        }
        public static string GetProductID(string content)
        {
            JObject jObject = JObject.Parse(content);
            return GetString(jObject["id"]);
        }
        public static string GetModName(string content)
        {
            JObject jObject = JObject.Parse(content);
            return GetString(jObject["name"]);
        }
        public static string GetDownloadCount(string content)
        {
            JObject jObject = JObject.Parse(content);
            return GetString(jObject["downloads"]);
        }
        public static string GetAuthor(string content)
        {
            JObject jObject = JObject.Parse(content);
            return GetString(jObject["author"]);
        }
        public static string GetVersion(string content)
        {
            JObject jObject = JObject.Parse(content);
            return GetVersion(jObject["versions"]);
        }
        public static string GetKSPVersion(string content)
        {
            JObject jObject = JObject.Parse(content);
            return GetKSPVersion(jObject["versions"]);
        }
        public static string GetNote(string content)
        {
            JObject jObject = JObject.Parse(content);
            return GetString(jObject["description"]);
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
                if (child["game_version"] != null)
                {
                    version = child["game_version"].ToString();
                    break;
                }

                // just inspect the first child.
                break;
            }

            return version;
        }
    }
}
