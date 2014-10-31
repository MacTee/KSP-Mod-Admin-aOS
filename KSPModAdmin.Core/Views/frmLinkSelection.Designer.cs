namespace KSPModAdmin.Core.Views
{
    partial class frmLinkSelection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLinkSelection));
            this.lblLinkSelectionDescription = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.webBrowserEx1 = new KSPModAdmin.Core.Utils.Controls.WebBrowserEx();
            this.SuspendLayout();
            // 
            // lblLinkSelectionDescription
            // 
            this.lblLinkSelectionDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLinkSelectionDescription.Location = new System.Drawing.Point(12, 7);
            this.lblLinkSelectionDescription.Name = "lblLinkSelectionDescription";
            this.lblLinkSelectionDescription.Size = new System.Drawing.Size(600, 28);
            this.lblLinkSelectionDescription.TabIndex = 1;
            this.lblLinkSelectionDescription.Text = "Please browse to the download link and click it.";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(618, 11);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(154, 17);
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Visible = false;
            // 
            // webBrowserEx1
            // 
            this.webBrowserEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserEx1.Location = new System.Drawing.Point(2, 38);
            this.webBrowserEx1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserEx1.Name = "webBrowserEx1";
            this.webBrowserEx1.ScriptErrorsSuppressed = true;
            this.webBrowserEx1.Size = new System.Drawing.Size(781, 522);
            this.webBrowserEx1.TabIndex = 3;
            this.webBrowserEx1.FileDownloading += new System.EventHandler<KSPModAdmin.Core.Utils.Controls.FileDownloadEventArgs>(this.webBrowserEx1_FileDownloading);
            this.webBrowserEx1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowserEx1_Navigated);
            this.webBrowserEx1.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowserEx1_Navigating);
            // 
            // frmLinkSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.webBrowserEx1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblLinkSelectionDescription);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLinkSelection";
            this.ShowIcon = false;
            this.Text = "Link selection";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLinkSelectionDescription;
        private System.Windows.Forms.ProgressBar progressBar1;
        private Core.Utils.Controls.WebBrowserEx webBrowserEx1;
    }
}