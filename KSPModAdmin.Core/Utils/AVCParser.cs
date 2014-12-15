using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Class to parse a AVC version file.
    /// </summary>
    public class AVCParser
    {
        public static AVCInfo ReadVersionFile(string path)
        {
            if (!File.Exists(path))
                return null;
            
            return ReadFromString(File.ReadAllText(path));
        }

        private static AVCInfo ReadFromString(string jsonString)
        {
            AVCInfo avcInfo = new AVCInfo();
            JObject jObject = JObject.Parse(jsonString);
            avcInfo.Name = GetString(jObject["NAME"]);
            avcInfo.Url = GetString(jObject["URL"]);
            avcInfo.Download = GetString(jObject["DOWNLOAD"]);
            avcInfo.ChangeLog = GetString(jObject["CHANGE_LOG"]);
            avcInfo.ChangeLogUrl = GetString(jObject["CHANGE_LOG_URL"]);
            JToken jGitHub = jObject["GITHUB"];
            if (jGitHub != null)
            {
                avcInfo.GitHubUsername = GetString(jGitHub["USERNAME"]);
                avcInfo.GitHubRepository = GetString(jGitHub["REPOSITORY"]);
                avcInfo.GitHubAllowPreRelease = GetString(jGitHub["ALLOW_PRE_RELEASE"]).Equals("true", StringComparison.CurrentCultureIgnoreCase);
            }
            avcInfo.Version = GetVersion(jObject["VERSION"] as JToken);
            avcInfo.KspVersion = GetVersion(jObject["KSP_VERSION"] as JToken, 3);
            avcInfo.KspVersionMin = GetVersion(jObject["KSP_VERSION_MIN"] as JToken, 3);
            avcInfo.KspVersionMax = GetVersion(jObject["KSP_VERSION_MAX"] as JToken, 3);

            return avcInfo;
        }

        private static string GetString(JToken jToken)
        {
            if (jToken == null)
                return string.Empty;

            return (string)jToken;
        }

        private static string GetVersion(JToken jToken, int depth = 4)
        {
            if (jToken == null)
                return string.Empty;

            if (depth < 1)
                depth = 1;

            StringBuilder sb = new StringBuilder();
            if (jToken["MAJOR"] != null && depth >= 1)
                sb.Append(jToken["MAJOR"]);
            else if (depth >= 1)
                sb.Append("0");
            if (jToken["MINOR"] != null && depth >= 1)
                sb.Append("." + jToken["MINOR"]);
            else if (depth >= 2)
                sb.Append(".0");
            if (jToken["PATCH"] != null && depth >= 3)
                sb.Append("." + jToken["PATCH"]);
            else if (depth >= 3)
                sb.Append(".0");
            if (jToken["BUILD"] != null && depth >= 4)
                sb.Append("." + jToken["BUILD"]);
            else if (depth >= 4)
                sb.Append(".0");
            return sb.ToString();
        }
    }

    public class AVCInfo
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Download { get; set; }
        public string ChangeLog { get; set; }
        public string ChangeLogUrl { get; set; }
        public string GitHubUsername { get; set; }
        public string GitHubRepository { get; set; }
        public bool GitHubAllowPreRelease { get; set; }
        public string Version { get; set; }
        public string KspVersion { get; set; }
        public string KspVersionMin { get; set; }
        public string KspVersionMax { get; set; }


        public AVCInfo()
        {
            Name = string.Empty;
            Url = string.Empty;
            Download = string.Empty;
            ChangeLog = string.Empty;
            ChangeLogUrl = string.Empty;
            GitHubUsername = string.Empty;
            GitHubRepository = string.Empty;
            GitHubAllowPreRelease = false;
            Version = string.Empty;
            KspVersion = string.Empty;
            KspVersionMin = string.Empty;
            KspVersionMax = string.Empty;
        }
    }
}
