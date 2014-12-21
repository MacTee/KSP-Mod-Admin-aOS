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
        public static AVCInfo ReadFromWeb(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            string content = www.Load(url);
            if (string.IsNullOrEmpty(content))
                return null;

            return ReadFromString(content);
        }

        public static AVCInfo ReadFromFile(string path)
        {
            if (!File.Exists(path))
                return null;
            
            return ReadFromString(File.ReadAllText(path));
        }

        public static AVCInfo ReadFromString(string jsonString)
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
                avcInfo.GitHubAllowPreRelease = GetString(jGitHub["ALLOW_PRE_RELEASE"]).Equals(Constants.TRUE, StringComparison.CurrentCultureIgnoreCase);
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

        public bool IsEmpty
        {
            get
            {
                return (Name == string.Empty &&
                        Url == string.Empty &&
                        Download == string.Empty &&
                        ChangeLog == string.Empty &&
                        ChangeLogUrl == string.Empty &&
                        GitHubUsername == string.Empty &&
                        GitHubRepository == string.Empty &&
                        GitHubAllowPreRelease == false &&
                        Version == string.Empty &&
                        KspVersion == string.Empty &&
                        KspVersionMin == string.Empty &&
                        KspVersionMax == string.Empty);
            }
        }


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


        public override string ToString()
        {
            if (IsEmpty)
                return "AVCInfo(empty)";

            StringBuilder sb = new StringBuilder();
            sb.Append("AVCInfo(");
            if (!string.IsNullOrEmpty(Name))
                sb.Append("Name = {0}, ");
            if (!string.IsNullOrEmpty(Url))
                sb.Append("URL = {1}, ");
            if (!string.IsNullOrEmpty(Download))
                sb.Append("Download = {2}, ");
            if (!string.IsNullOrEmpty(ChangeLog))
                sb.Append("ChangeLog = {3}, ");
            if (!string.IsNullOrEmpty(ChangeLogUrl))
                sb.Append("ChangeLogUrl = {4}, ");
            if (!string.IsNullOrEmpty(GitHubUsername))
                sb.Append("GitHubUsername = {5}, ");
            if (!string.IsNullOrEmpty(GitHubRepository))
                sb.Append("GitHubRepository = {6}, ");
            if (!string.IsNullOrEmpty(GitHubUsername))
                sb.Append("GitHubAllowPreRelease = {7}, ");
            if (!string.IsNullOrEmpty(Version))
                sb.Append("Version = {8}, ");
            if (!string.IsNullOrEmpty(KspVersion))
                sb.Append("KspVersion = {9}, ");
            if (!string.IsNullOrEmpty(KspVersionMin))
                sb.Append("KspVersionMin = {10}, ");
            if (!string.IsNullOrEmpty(KspVersionMin))
                sb.Append("KspVersionMin = {11}, ");

            string temp = sb.ToString();
            temp = temp.Substring(0, temp.Length-2) + ")";
            return string.Format(temp, Name, Url, Download, ChangeLog, ChangeLogUrl, GitHubUsername, GitHubRepository, GitHubAllowPreRelease, 
                Version, KspVersion, KspVersionMin, KspVersionMax);
        }

        public string ToJson()
        {
            if (IsEmpty)
                return "{}";

            dynamic jObject = new JObject();
            jObject.NAME = Name;
            jObject.URL = Url;
            if (!string.IsNullOrEmpty(Download))
                jObject.DOWNLOAD = Download;
            if (!string.IsNullOrEmpty(ChangeLog))
                jObject.CHANGE_LOG = ChangeLog;
            if (!string.IsNullOrEmpty(ChangeLogUrl))
                jObject.CHANGE_LOG_URL = ChangeLogUrl;
            if (!string.IsNullOrEmpty(GitHubUsername))
            {
                jObject.GITHUB = new JObject();
                jObject.GITHUB.USERNAME = GitHubUsername;
                jObject.GITHUB.REPOSITORY = GitHubRepository;
                jObject.GITHUB.ALLOW_PRE_RELEASE = GitHubAllowPreRelease;
            }
            if (!string.IsNullOrEmpty(Version))
            {
                string[] temp = Version.Split('.');

                jObject.VERSION = new JObject();
                if (temp.Length >= 1)
                    jObject.VERSION.MAJOR = temp[0];
                if (temp.Length >= 2)
                    jObject.VERSION.MINOR = temp[1];
                if (temp.Length >= 3)
                    jObject.VERSION.PATCH = temp[2];
                if (temp.Length >= 4)
                    jObject.VERSION.BUILD = temp[3];
            }
            if (!string.IsNullOrEmpty(KspVersion))
            {
                string[] temp = KspVersion.Split('.');

                jObject.KSP_VERSION = new JObject();
                if (temp.Length >= 1)
                    jObject.KSP_VERSION.MAJOR = temp[0];
                if (temp.Length >= 2)
                    jObject.KSP_VERSION.MINOR = temp[1];
                if (temp.Length >= 3)
                    jObject.KSP_VERSION.PATCH = temp[2];
            }
                
            if (!string.IsNullOrEmpty(KspVersionMin))
            {
                string[] temp = KspVersionMin.Split('.');

                jObject.KSP_VERSION_MIN = new JObject();
                if (temp.Length >= 1)
                    jObject.KSP_VERSION_MIN.MAJOR = temp[0];
                if (temp.Length >= 2)
                    jObject.KSP_VERSION_MIN.MINOR = temp[1];
                if (temp.Length >= 3)
                    jObject.KSP_VERSION_MIN.PATCH = temp[2];
            }
            if (!string.IsNullOrEmpty(KspVersionMax))
            {
                string[] temp = KspVersionMax.Split('.');

                jObject.KSP_VERSION_MAX = new JObject();
                if (temp.Length >= 1)
                    jObject.KSP_VERSION_MAX.MAJOR = temp[0];
                if (temp.Length >= 2)
                    jObject.KSP_VERSION_MAX.MINOR = temp[1];
                if (temp.Length >= 3)
                    jObject.KSP_VERSION_MAX.PATCH = temp[2];
            }
            
            return jObject.ToString();
        }
    }
}
