namespace KSPModAdmin.Core.Utils
{
    public abstract class GitHub
    {
        public static bool IsValidURL(string url)
        {
            return (url.StartsWith("https://github.com") || url.StartsWith("http://github.com"));
        }

        public static string GetDownloadURL(string url)
        {
            string siteContent = www.Load(url);
            int index1 = siteContent.IndexOf("<ul class=\"release-downloads\">");
            if (index1 < 0)
                return string.Empty;
            index1 = siteContent.IndexOf("href=\"", index1) + 6;
            siteContent = siteContent.Substring(index1);
            index1 = siteContent.IndexOf("\"");
            if (index1 < 0)
                return string.Empty;
            string downloadURL = "https://github.com" + siteContent.Substring(0, index1);

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
