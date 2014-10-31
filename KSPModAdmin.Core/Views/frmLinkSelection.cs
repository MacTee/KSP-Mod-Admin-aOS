using System;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Views
{
    public partial class frmLinkSelection : frmBase
    {
        public string Title
        {
            get { return Text; }
            set { Text = value; }
        }

        public string Descrition
        {
            get { return lblLinkSelectionDescription.Text; }
            set { lblLinkSelectionDescription.Text = value; }
        }

        public string URL
        {
            get { return webBrowserEx1.Url.ToString(); }
            set { webBrowserEx1.Navigate(new Uri(value)); }
        }

        public string SelectedLink { get; set; }


        public frmLinkSelection()
        {
            InitializeComponent();

            DialogResult = DialogResult.Cancel;

            webBrowserEx1.ProgressChanged += (sender, e) =>
                                             {
                                                 if ((int)e.CurrentProgress > 0)
                                                 {
                                                     progressBar1.Maximum = (int)e.MaximumProgress;
                                                     if (progressBar1.Maximum >= e.CurrentProgress)
                                                        progressBar1.Value = (int)e.CurrentProgress;
                                                 }
                                             };

            // hook to NewWindow event to prevent pop ups.
            SHDocVw.WebBrowser_V1 Web_V1; //Interface to expose ActiveX methods
            Web_V1 = (SHDocVw.WebBrowser_V1)webBrowserEx1.ActiveXInstance;
            Web_V1.NewWindow += new SHDocVw.DWebBrowserEvents_NewWindowEventHandler(webBrowserEx1_NewWindow);
        }


        private void webBrowserEx1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            progressBar1.Visible = true;
        }

        private void webBrowserEx1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            progressBar1.Visible = false;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
        }

        private void webBrowserEx1_FileDownloading(object sender, Utils.Controls.FileDownloadEventArgs e)
        {
            SelectedLink = e.DownloadUri.ToString();

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Callback if a NewPopup Window will be opened.
        /// Avoids New Windows and navigates to the popup url.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="flags"></param>
        /// <param name="targetFrameName"></param>
        /// <param name="postData"></param>
        /// <param name="headers"></param>
        /// <param name="processed"></param>
        private void webBrowserEx1_NewWindow(string url, int flags, string targetFrameName, ref object postData, string headers, ref bool processed)
        {
            // Stop event from being processed
            processed = true;

            webBrowserEx1.Navigate(url);
        }
    }
}
