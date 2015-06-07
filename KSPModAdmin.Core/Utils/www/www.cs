using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using KSPModAdmin.Core.Utils.Logging;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Delegate of a callback function to propagate the download progress.
    /// </summary>
    /// <param name="bytesReceived">the bytes received so far.</param>
    /// <param name="fileSize">Full file size.</param>
    public delegate void DownloadProgressCallback(long bytesReceived, long fileSize);

    /// <summary>
    /// Wrapper class for www (internet) related logic.
    /// </summary>
    public class Www
    {
        ////public static Regex ArchiveRegEx = new Regex("http(s?):(.*)[.](zip|rar|7zip)(\")");
        ////public static Regex HTMLLinkRegEx = new Regex("<a href=\"http(s?):(.*?)\" target=\"(.*?)\">(.*?)</a>");


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
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException wEx)
            {
				if (PlatformHelper.GetPlatform() != Platform.Win && wEx.Status == WebExceptionStatus.SendFailure)
                {
                    string msg = string.Format("Error during www.Load({0}){1}Possible problem: Missing certificates.{1}Enter \"mozroots --sync --import\" in your terminal to add missing certificates.", url, Environment.NewLine);
                    try
                    {
                        MessageBox.Show(msg, Messages.MSG_TITLE_ERROR);
                    }
                    catch (Exception)
                    { }
                    Messenger.AddError(msg, wEx);
                }
                else
                    Messenger.AddError(string.Format("Error in www.Load({0})", url), wEx);
            }
            catch (Exception ex)
            {
                Log.AddErrorS(string.Format("Error in www.Load({0})", url), ex);
            }

            return string.Empty;
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

            using (HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd().Trim();
                }
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

            ////ASCIIEncoding encoding = new ASCIIEncoding();
            ////return encoding.GetBytes(postData.ToString().Substring(0, postData.Length - 1));
        }


        /// <summary>
        /// Downloads a file.
        /// </summary>
        /// <param name="downloadURL">Url to the file to download.</param>
        /// <param name="downloadPath">Path to save the file to.</param>
        public static void DownloadFile(string downloadURL, string downloadPath, DownloadProgressCallback downloadProgressCallback = null)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Credentials = CredentialCache.DefaultCredentials;
                webClient.DownloadProgressChanged += (sender, args) =>
                    {
                        if (downloadProgressCallback != null)
                            downloadProgressCallback(args.BytesReceived, args.TotalBytesToReceive);
                    };

                bool wait = true;
                webClient.DownloadFileCompleted += (sender, args) =>
                    {
                        wait = false;
                    };

                // User DownloadFileAsync cause DownloadFile doesn't fire the DownloadProgressChanged event.
                webClient.DownloadFileAsync(new Uri(downloadURL), downloadPath);
                while (wait)
                {
                    // Wait till download is finished.
                }
            }
        }

        /// <summary>
        /// Downloads a file.
        /// Filename will be taken from WebResponse.ResponseUri.AbsolutePath
        /// </summary>
        /// <param name="downloadURL">Url to the file to download.</param>
        /// <param name="downloadPath">Path to save the file to without filename!</param>
        /// <param name="downloadProgressCallback">Callback function which will receive download progress information.</param>
        /// <returns>The full path to the downloaded file (path and filename).</returns>
        public static string DownloadFile2(string downloadURL, string downloadPath, DownloadProgressCallback downloadProgressCallback = null)
        {
            string fullpath = string.Empty;
            using (WebClient wcDownload = new WebClient())
            {
                Stream strResponse = null;
                Stream strLocal = null;
                try
                {
                    // Create a request to the file we are downloading
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(downloadURL);
                    // Set default authentication for retrieving the file
                    webRequest.Credentials = CredentialCache.DefaultCredentials;
                    // Retrieve the response from the server
                    HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                    // Ask the server for the file size and store it
                    long fileSize = webResponse.ContentLength;
                    string filename = Path.GetFileName(webResponse.ResponseUri.AbsolutePath);
                    fullpath = Path.Combine(downloadPath, filename);
                    fullpath = GetNewFilenameWhenDestinationExists(fullpath);

                    // Open the URL for download 
                    strResponse = wcDownload.OpenRead(downloadURL);
                    // Create a new file stream where we will be saving the data (local drive)
                    strLocal = new FileStream(fullpath, FileMode.Create, FileAccess.Write, FileShare.None);

                    // It will store the current number of bytes we retrieved from the server
                    int bytesSize = 0;
                    // A buffer for storing and writing the data retrieved from the server
                    byte[] downBuffer = new byte[2048];

                    // Loop through the buffer until the buffer is empty
                    while ((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
                    {
                        // Write the data from the buffer to the local hard drive
                        strLocal.Write(downBuffer, 0, bytesSize);

                        // Invoke the method that propagates the progress.
                        if (downloadProgressCallback != null)
                            downloadProgressCallback(strLocal.Length, fileSize);
                    }
                }
                catch (Exception ex)
                {
                    Messenger.AddError(string.Format("Error while downloading \"{0}\"!", downloadURL), ex);
                    fullpath = string.Empty;
                }
                finally
                {
                    // When the above code has ended, close the streams
                    strResponse.Close();
                    strLocal.Close();
                }

                return fullpath;
            }
        }

        private static string GetNewFilenameWhenDestinationExists(string fullpath)
        {
            int count = 1;
            string newFullpath = fullpath;
            while (File.Exists(newFullpath))
            {
                string newFilename = string.Format("{0}_({1}){2}", Path.GetFileNameWithoutExtension(fullpath), count, Path.GetExtension(fullpath));
                newFullpath = Path.Combine(Path.GetDirectoryName(fullpath), newFilename);
                count++;
            }

            return newFullpath;
        }


        ////public static DownloadInfo GetDirectDownloadURLFromHostSite(string hostURL)
        ////{
        ////    DownloadInfo dInfo = new DownloadInfo();
        ////    if (MediaFire.IsValidURL(hostURL))
        ////    {
        ////        dInfo.DownloadURL = MediaFire.GetDownloadURL(hostURL);
        ////        dInfo.Filename = MediaFire.GetFileName(dInfo.DownloadURL);
        ////        dInfo.KnownHost = true;
        ////    }
        ////    else if (GitHub.IsValidURL(hostURL))
        ////    {
        ////        dInfo.DownloadURL = GitHub.GetDownloadURL(hostURL);
        ////        dInfo.Filename = GitHub.GetFileName(dInfo.DownloadURL);
        ////        dInfo.KnownHost = true;
        ////    }
        ////    else if (DropBox.IsValidURL(hostURL))
        ////    {
        ////        dInfo.DownloadURL = DropBox.GetDownloadURL(hostURL);
        ////        dInfo.Filename = DropBox.GetFileName(dInfo.DownloadURL);
        ////        dInfo.KnownHost = true;
        ////    }
        ////    //else if (Curse.IsValidURL(hostURL))
        ////    //{
        ////    //    string filename = string.Empty;
        ////    //    dInfo.DownloadURL = Curse.GetDownloadURL(hostURL, ref filename);
        ////    //    dInfo.Filename = filename;
        ////    //    dInfo.KnownHost = true;
        ////    //}
        ////    //else if (CurseForge.IsValidURL(hostURL))
        ////    //{
        ////    //    string filename = string.Empty;
        ////    //    dInfo.DownloadURL = CurseForge.GetDownloadURL(hostURL, ref filename);
        ////    //    dInfo.Filename = filename;
        ////    //    dInfo.KnownHost = true;
        ////    //}

        ////    return dInfo;
        ////}


        ////public static List<LinkInfo> GetHTMLLinks(string siteContent)
        ////{
        ////    if (string.IsNullOrEmpty(siteContent))
        ////        return new List<LinkInfo>();
        ////    else
        ////    {
        ////        List<LinkInfo> result = new List<LinkInfo>();
        ////        var links = (from object a in HTMLLinkRegEx.Matches(siteContent) select a.ToString()).ToList();
        ////        foreach (var link in links)
        ////        {
        ////            string url = link.Replace("<a href=\"", string.Empty);
        ////            int index = url.IndexOf(">") + 1;
        ////            string name = RemoveHTMLTags(url.Substring(index, url.IndexOf("</a>") - index).Trim());
        ////            url = url.Substring(0, url.IndexOf("\"")).Trim();
        ////            if (!name.Contains("<img"))
        ////                result.Add(new LinkInfo() { Name = name, URL = url });
        ////        }

        ////        return result;
        ////    }
        ////}

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

        ////public static List<string> GetArchiveDownloadLinks(string siteContent)
        ////{
        ////    if (string.IsNullOrEmpty(siteContent))
        ////        return new List<string>();
        ////    else
        ////        return (from object a in ArchiveRegEx.Matches(siteContent) select a.ToString()).ToList();
        ////}

        ////public static bool IsValidArchiveDownloadURL(string url)
        ////{
        ////    return ArchiveRegEx.IsMatch(url);
        ////}


        ////public static string GetFileName(string downloadURL)
        ////{
        ////    int index = downloadURL.LastIndexOf("/");
        ////    string filename = downloadURL.Substring(index + 1);
        ////    if (filename.Contains("?"))
        ////        filename = filename.Substring(0, filename.IndexOf("?"));

        ////    return filename;
        ////}
    }

    /// <summary>
    /// Class that holds download related informations.
    /// </summary>
    public class DownloadInfo
    {
        /// <summary>
        /// Name of the mod.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL to the download of the mod.
        /// </summary>
        public string DownloadURL { get; set; }

        /// <summary>
        /// The filename of the mod archive.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Flag to determine if the host of the DownloadUrl is known.
        /// </summary>
        public bool KnownHost { get; set; }

        /// <summary>
        /// Flag that indicates if the DownloadURL and Filename is not null and not empty.
        /// </summary>
        public bool IsValid { get { return !string.IsNullOrEmpty(DownloadURL) && !string.IsNullOrEmpty(Filename); } }


        /// <summary>
        /// Creates a string of the format "Name (DownloadURL)"
        /// </summary>
        /// <returns>The "Name (DownloadURL)" formated string.</returns>
        public override string ToString()
        {
            return Name + " (" + DownloadURL + ")";
        }
    }

    /// <summary>
    /// Class that holds link related informations.
    /// </summary>
    public class LinkInfo
    {
        /// <summary>
        /// Name if the link (the text of a link).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The URL the link points to.
        /// </summary>
        public string URL { get; set; }
    }
}
