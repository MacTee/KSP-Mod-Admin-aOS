using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Utils.Controls
{
    public class WebBrowserEx : WebBrowser
    {
        /// <summary>
        /// Event called when the browser is about to download a file
        /// </summary>
        public event EventHandler<FileDownloadEventArgs> FileDownloading;

        /// <summary>
        /// The manager of action keys that handels our key strokes.
        /// </summary>
        private ActionKeyManager m_ActionKeyManager = new ActionKeyManager();

        #region Overrides

        /// <summary>
        /// Returns a reference to the unmanaged WebBrowser ActiveX control site,
        /// which you can extend to customize the managed <see cref="T:System.Windows.Forms.WebBrowser"/> control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.WebBrowser.WebBrowserSite"/> that represents the WebBrowser ActiveX control site.
        /// </returns>
        protected override WebBrowserSiteBase CreateWebBrowserSiteBase()
        {
            DownloadWebBrowserSite downloadWebBrowserSite = new DownloadWebBrowserSite(this);
            downloadWebBrowserSite.FileDownloading += new EventHandler<FileDownloadEventArgs>(downloadManager_FileDownloading);
            return downloadWebBrowserSite;
        }

        #endregion

        /// <summary>
        /// Callback of IDownloadManager, is called when browser tries to download a file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void downloadManager_FileDownloading(object sender, FileDownloadEventArgs e)
        {
            if (FileDownloading != null)
                FileDownloading(sender, e);
        }

        /// <summary>
        /// COnstants
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// Ok
            /// </summary>
            public const int S_OK = 0;
            /// <summary>
            /// No interface
            /// </summary>
            public const int E_NOINTERFACE = unchecked((int)0x80004002);
            /// <summary>
            /// Default action
            /// </summary>
            public const int INET_E_DEFAULT_ACTION = unchecked((int)0x800C0011);

            /// <summary>
            /// Unspecified failure
            /// </summary>
            public const int E_FAIL = unchecked((int)0x80004005);

            /// <summary>
            /// Guid of the download manager
            /// </summary>
            public static readonly Guid IID_IDownloadManager = new Guid("{988934A4-064B-11D3-BB80-00104B35E7F9}");

            /// <summary>
            /// Guid of the ITargetFrame
            /// </summary>
            public static readonly Guid IID_ITargetFrame2 = new Guid("{86d52e11-94a8-11d0-82af-00c04fd5ae38}");

            /// <summary>
            /// GUID of http response
            /// </summary>
            public static readonly Guid IID_IHttpNegotiate = new Guid("{79EAC9D2-BAF9-11CE-8C82-00AA004BA90B}");
        }

        /// <summary>
        /// Represents the host window of a System.Windows.Forms.WebBrowser control.
        /// Exposes a download manager
        /// </summary>
        protected class DownloadWebBrowserSite : WebBrowserSite, IServiceProvider
        {
            /// <summary>
            /// Event called when the browser is about to download a file
            /// </summary>
            public event EventHandler<FileDownloadEventArgs> FileDownloading;

            private readonly DownloadManager m_DownloadManager;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="host">the hose</param>
            public DownloadWebBrowserSite(WebBrowser host)
                : base(host)
            {
                m_DownloadManager = new DownloadManager();
                m_DownloadManager.FileDownloading += new EventHandler<FileDownloadEventArgs>(downloadManager_FileDownloading);
            }

            #region Implementation of IServiceProvider

            /// <summary>
            /// Queries for a service
            /// </summary>
            /// <param name="guidService">the service GUID</param>
            /// <param name="riid"></param>
            /// <param name="ppvObject"></param>
            /// <returns></returns>
            public int QueryService(ref Guid guidService, ref Guid riid, out IntPtr ppvObject)
            {
                Debug.WriteLine("MyBrowser: " + guidService.ToString());
                if ((guidService == Constants.IID_IDownloadManager && riid == Constants.IID_IDownloadManager))
                {
                    ppvObject = Marshal.GetComInterfaceForObject(m_DownloadManager, typeof(IDownloadManager));
                    return Constants.S_OK;
                }
                ppvObject = IntPtr.Zero;
                return Constants.E_NOINTERFACE;
            }

            #endregion

            public void downloadManager_FileDownloading(object sender, FileDownloadEventArgs e)
            {
                if (FileDownloading != null)
                    FileDownloading(sender, e);
            }
        }
    }
}
