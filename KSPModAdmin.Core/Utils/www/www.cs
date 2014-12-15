using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using KSPModAdmin.Core.Utils.SiteHandler;

namespace KSPModAdmin.Core.Utils
{
    public class www
    {
        public static Regex ArchiveRegEx = new Regex("http(s?):(.*)[.](zip|rar|7zip)(\")");
        public static Regex HTMLLinkRegEx = new Regex("<a href=\"http(s?):(.*?)\" target=\"(.*?)\">(.*?)</a>");


        /// <summary>
        /// Loads the content of the site from the passed URL.
        /// </summary>
        /// <param name="url">The URL to get the content from.</param>
        /// <returns>The content of the site from the passed URL as a string.</returns>
        public static string Load(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Loads the content of the site from the passed URL.
        /// </summary>
        /// <param name="url">The URL to get the content from.</param>
        /// <param name="postParameter">List of post parameter for the site.</param>
        /// <returns>The content of the site from the passed URL as a string.</returns>
        public static string Load(string url, Dictionary<string, string> postParameter)
        {
            string data = CreatePostParameter(postParameter);

            WebRequest httpWReq = WebRequest.Create(url);
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;

            using (StreamWriter w = new StreamWriter(httpWReq.GetRequestStream()))
            {
                w.Write(data);
            }

            string result = null;
            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd().Trim();
            }

            return result;
        }

        /// <summary>
        /// Creates a byte array from the passed parameters.
        /// </summary>
        /// <param name="parameter">The parameters to create the byte array from.</param>
        /// <returns>A byte array from the passed parameters.</returns>
        public static string CreatePostParameter(Dictionary<string, string> parameter)
        {
            StringBuilder postData = new StringBuilder();
            foreach (var entry in parameter)
            {
                if (string.IsNullOrEmpty(entry.Value))
                    postData.Append(string.Format("{0}", entry.Key));
                else
                    postData.Append(string.Format("{0}={1}", entry.Key, entry.Value));

                postData.Append("&");
            }

            string result = postData.ToString();
            return result.Substring(0, result.Length - 1);

            //ASCIIEncoding encoding = new ASCIIEncoding();
            //return encoding.GetBytes(postData.ToString().Substring(0, postData.Length - 1));
        }


        /// <summary>
        /// Downloads a file.
        /// </summary>
        /// <param name="downloadURL">Url to the file to download.</param>
        /// <param name="downloadPath">Path to save the file to.</param>
        /// <param name="downloadProgressHandler"></param>
        public static void DownloadFile(string downloadURL, string downloadPath, DownloadProgressChangedEventHandler downloadProgressHandler = null)
        {
            WebClient webClient = new WebClient();
            webClient.Credentials = CredentialCache.DefaultCredentials;
            if (downloadProgressHandler != null)
                webClient.DownloadProgressChanged += downloadProgressHandler;

            webClient.DownloadFile(new Uri(downloadURL), downloadPath);
        }


        public static DownloadInfo GetDirectDownloadURLFromHostSite(string hostURL)
        {
            DownloadInfo dInfo = new DownloadInfo();
            if (MediaFire.IsValidURL(hostURL))
            {
                dInfo.DownloadURL = MediaFire.GetDownloadURL(hostURL);
                dInfo.Filename = MediaFire.GetFileName(dInfo.DownloadURL);
                dInfo.KnownHost = true;
            }
            else if (GitHub.IsValidURL(hostURL))
            {
                dInfo.DownloadURL = GitHub.GetDownloadURL(hostURL);
                dInfo.Filename = GitHub.GetFileName(dInfo.DownloadURL);
                dInfo.KnownHost = true;
            }
            else if (DropBox.IsValidURL(hostURL))
            {
                dInfo.DownloadURL = DropBox.GetDownloadURL(hostURL);
                dInfo.Filename = DropBox.GetFileName(dInfo.DownloadURL);
                dInfo.KnownHost = true;
            }
			else if (new KerbalStuffHandler().IsValidURL(hostURL))
			{
				dInfo.DownloadURL = new KerbalStuffHandler().
			}
            //else if (Curse.IsValidURL(hostURL))
            //{
            //    string filename = string.Empty;
            //    dInfo.DownloadURL = Curse.GetDownloadURL(hostURL, ref filename);
            //    dInfo.Filename = filename;
            //    dInfo.KnownHost = true;
            //}
            //else if (CurseForge.IsValidURL(hostURL))
            //{
            //    string filename = string.Empty;
            //    dInfo.DownloadURL = CurseForge.GetDownloadURL(hostURL, ref filename);
            //    dInfo.Filename = filename;
            //    dInfo.KnownHost = true;
            //}

            return dInfo;
        }

		// TODO Not used?
        public static List<LinkInfo> GetHTMLLinks(string siteContent)
        {
            if (string.IsNullOrEmpty(siteContent))
                return new List<LinkInfo>();
            else
            {
                List<LinkInfo> result = new List<LinkInfo>();
                var links = (from object a in HTMLLinkRegEx.Matches(siteContent) select a.ToString()).ToList();
                foreach (var link in links)
                {
                    string url = link.Replace("<a href=\"", string.Empty);
                    int index = url.IndexOf(">") + 1;
                    string name = RemoveHTMLTags(url.Substring(index, url.IndexOf("</a>") - index).Trim());
                    url = url.Substring(0, url.IndexOf("\"")).Trim();
                    if (!name.Contains("<img"))
                        result.Add(new LinkInfo() { Name = name, URL = url });
                }

                return result;
            }
        }

		// TODO Only used in above function
        private static string RemoveHTMLTags(string str)
        {
            bool open = false;
            StringBuilder sb = new StringBuilder(str.Length);
            foreach (char c in str)
            {
                bool closed = false;
                if (c == '<')
                    open = true;
                else if (c == '>')
                {
                    open = false;
                    closed = true;
                }

                if (!open && !closed)
                    sb.Append(c);
            }

            return sb.ToString();
        }

		// TODO Not used?
        public static List<string> GetArchiveDownloadLinks(string siteContent)
        {
            if (string.IsNullOrEmpty(siteContent))
                return new List<string>();
            else
                return (from object a in ArchiveRegEx.Matches(siteContent) select a.ToString()).ToList();
        }

		// TODO Not used?
        public static bool IsValidArchiveDownloadURL(string url)
        {
            return ArchiveRegEx.IsMatch(url);
        }

		// TODO Not used?
        public static string GetFileName(string downloadURL)
        {
            int index = downloadURL.LastIndexOf("/");
            string filename = downloadURL.Substring(index + 1);
            if (filename.Contains("?"))
                filename = filename.Substring(0, filename.IndexOf("?"));

            return filename;
        }
    }

    public class DownloadInfo
    {
        public string Name { get; set; }
        public string DownloadURL { get; set; }
        public string Filename { get; set; }
        public bool KnownHost { get; set; }
        public bool IsValid { get { return !string.IsNullOrEmpty(DownloadURL) && !string.IsNullOrEmpty(Filename); } }


        public override string ToString()
        {
            return Name + " (" + DownloadURL + ")";
        }
    }

    public class LinkInfo
    {
        public string Name { get; set; }
        public string URL { get; set; }
    }
}
