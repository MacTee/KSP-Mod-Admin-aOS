using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Helper class to load Internet site or download a file.
    /// </summary>
    public class www
    {
        /// <summary>
        /// Loads the content of the site from the passed URL.
        /// </summary>
        /// <param name="url">The URL to get the content from.</param>
        /// <returns>The content of the site from the passed URL as a string.</returns>
        public static string Load(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            return reader.ReadToEnd();
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
            webClient.DownloadFile(new Uri(downloadURL), downloadPath);
            if (downloadProgressHandler != null)
                webClient.DownloadProgressChanged += downloadProgressHandler;
        }
    }
}
