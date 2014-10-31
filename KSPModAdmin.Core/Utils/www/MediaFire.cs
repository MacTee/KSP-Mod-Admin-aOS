namespace KSPModAdmin.Core.Utils
{
    public abstract class MediaFire
    {
        public static bool IsValidURL(string url)
        {
            return (url.StartsWith("http://www.mediafire.com/") || url.StartsWith("http://mediafire.com/"));
        }

        public static string GetDownloadURL(string mediafireURL)
        {
            if (string.IsNullOrEmpty(mediafireURL))
                return string.Empty;

            string siteContent = www.Load(mediafireURL);
            int index = siteContent.IndexOf("kNO = \"");
            if (index < 0)
                return string.Empty;
            siteContent = siteContent.Substring(index);
            index = siteContent.IndexOf("\"") + 1;
            if (index < 0)
                return string.Empty;
            int index1 = siteContent.IndexOf("\"", index);
            if (index1 <= index)
                return string.Empty;
            string url = siteContent.Substring(index, index1 - index);

            return (url.StartsWith("http:/")) ? url : string.Empty;
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
