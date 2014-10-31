using System;

namespace KSPModAdmin.Core.Utils.Controls
{
    /// <summary>
    /// A file download event arg
    /// </summary>
    public class FileDownloadEventArgs :EventArgs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="downloadUri">the URI the file is downloading from</param>
        public FileDownloadEventArgs(Uri downloadUri)
        {
            this.DownloadUri = downloadUri;
        }

        /// <summary>
        /// Gets the URI the file is downloading from
        /// </summary>
        public Uri DownloadUri { get; private set; }
    }
}
