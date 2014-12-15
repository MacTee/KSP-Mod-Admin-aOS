using System;
using HtmlAgilityPack;

namespace KSPModAdmin.Core.Utils
{
    public abstract class DropBox
    {
        public static bool IsValidURL(string url)
        {
	        var host = new Uri(url).Authority;
			return (host.Contains("dropbox.com"));
        }

        public static string GetDownloadURL(string url)
        {
			var htmlDoc = new HtmlWeb().Load(url);
			htmlDoc.OptionFixNestedTags = true;
			string downloadURL = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='default_content_download_button']").Attributes["href"].Value;

            return (downloadURL.StartsWith("http:/") || downloadURL.StartsWith("https:/")) ? downloadURL : string.Empty;
        }

        public static string GetFileName(string downloadURL)
        {
            int index = downloadURL.LastIndexOf("/");
            string filename = downloadURL.Substring(index + 1);
            if (filename.Contains("?"))
                filename = filename.Substring(0, filename.IndexOf("?"));

            return filename;
        }
    }
}
