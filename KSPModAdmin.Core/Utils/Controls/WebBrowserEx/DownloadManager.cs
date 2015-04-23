using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace KSPModAdmin.Core.Utils.Controls
{
    /// <summary>
    /// Intercepts downloads of files, to add as PDFs or supplements
    /// </summary>
    [ComVisible(true)]
    [Guid("bdb9c34c-d0ca-448e-b497-8de62e709744")]
    public class DownloadManager : IDownloadManager
    {
        /// <summary>
        /// Event called when the browser is about to download a file
        /// </summary>
        public event EventHandler<FileDownloadEventArgs> FileDownloading;

        /// <summary>
        /// Return S_OK (0) so that IE will stop to download the file itself. 
        /// Else the default download user interface is used.
        /// </summary>
        /// <returns>Return S_OK (0) or 1.</returns>
        public int Download(IMoniker pmk, IBindCtx pbc, uint dwBindVerb, int grfBINDF, IntPtr pBindInfo,
                            string pszHeaders, string pszRedir, uint uiCP)
        {
            string name;
            pmk.GetDisplayName(pbc, null, out name);
            if (!string.IsNullOrEmpty(name))
            {
                Uri url;
                if (Uri.TryCreate(name, UriKind.Absolute, out url))
                {
                    Debug.WriteLine("DownloadManager: initial URL is: " + url);
                    if (FileDownloading != null)
                    {
                        FileDownloading(this, new FileDownloadEventArgs(url));
                        ////DownloadMod(url.ToString());
                    }

                    return WebBrowserEx.Constants.S_OK;
                }
            }
            return 1;
        }
    }
}
