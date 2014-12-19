using System;
using System.ComponentModel;
using System.Windows.Forms;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Logging;

namespace KSPModAdmin.Core.Views
{
    public partial class frmEditModInfo : frmBase
    {
        #region Properties

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ModNode ModZipRoot 
        { 
            set
            {
                if (value != null)
                {
                    ModName = value.Text;

                    SiteHandlerName = value.SiteHandlerName;
                    ModURL = value.ModURL;
                    AdditionalURL = value.AdditionalURL;

                    tbName.ReadOnly = value.IsInstalled;

                    ProductID = value.ProductID;
                    Version = value.Version;
	                KSPVersion = value.KSPVersion;
                    Author = value.Author;
                    DownloadDate = value.AddDate;
                    ChangeDate = value.ChangeDate;
                    CreationDate = value.CreationDate;
                    Rating = value.Rating;
                    Downloads = value.Downloads;
                    Note = value.Note;
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ModInfo ModInfo
        {
            get
            {
                ModInfo modInfo = new ModInfo();
                modInfo.Author = Author;
                modInfo.CreationDate = CreationDate;
                modInfo.DownloadDate = DownloadDate;
                modInfo.ChangeDate = ChangeDate;
                modInfo.Downloads = Downloads;
                modInfo.Name = ModName;
                modInfo.KSPVersion = KSPVersion;
                modInfo.ProductID = ProductID;
                modInfo.Rating = Rating;
                modInfo.SiteHandlerName = SiteHandlerName;
                modInfo.AdditionalURL = AdditionalURL;
                return modInfo;
            }
            set
            {
                if (value != null)
                {
                    if (!tbName.ReadOnly)
                        ModName = value.Name;

                    Author = value.Author;
                    Downloads = value.Downloads;
                    ProductID = value.ProductID;
                    Rating = value.Rating;
                    CreationDate = value.CreationDate;
                    ChangeDate = value.ChangeDate;
                    DownloadDate = value.DownloadDate;
                    SiteHandlerName = value.SiteHandlerName;
                    ModURL = value.ModURL;
                    AdditionalURL = value.AdditionalURL;
                    KSPVersion = value.KSPVersion;
	                Version = value.Version;
                }
                else
                {
                    ModName = string.Empty;
                    Author = string.Empty;
                    dtpCreation.Value = DateTime.Now.Date;
                    dtpDownload.Value = dtpCreation.Value;
                    dtpChange.Value = dtpCreation.Value;
                    Downloads = "0";
                    ProductID = string.Empty;
                    Rating = "0 (0)";
                    SiteHandlerName = Messages.NONE;
                    ModURL = string.Empty;
                    AdditionalURL = AdditionalURL;
                    KSPVersion = string.Empty;
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Author
        {
            get
            {
                return tbAuthor.Text;
            }
            set
            {
                tbAuthor.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Downloads
        {
            get
            {
                return tbDownloads.Text;
            }
            set
            {
                tbDownloads.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string ModName
        {
            get
            {
                return tbName.Text;
            }
            set
            {
                tbName.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Note
        {
            get
            {
                return tbNote.Text;
            }
            set
            {
                tbNote.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string ProductID
        {
            get
            {
                return tbProductID.Text;
            }
            set
            {
                tbProductID.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Rating
        {
            get
            {
                return tbRating.Text;
            }
            set
            {
                tbRating.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Version
        {
            get
            {
                return tbVersion.Text;
            }
            set
            {
                tbVersion.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string CreationDate
        {
            get
            {
                return dtpCreation.Value.ToString();
            }
            set
            {
                if (value != null)
                {
                    DateTime dtTemp = DateTime.Now.Date;
                     
                    if (DateTime.TryParse(value, out dtTemp))
                        dtpCreation.Value = dtTemp;
                    else
                        dtpCreation.Value = dtpDownload.Value == DateTime.MinValue ? DateTime.Now.Date : dtpDownload.Value;
                }
                else
                    dtpCreation.Value = dtpDownload.Value == DateTime.MinValue ? DateTime.Now.Date : dtpDownload.Value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string ChangeDate
        {
            get
            {
                return dtpChange.Value.ToString();
            }
            set
            {
                if (value != null)
                {
                    DateTime dtTemp = DateTime.Now.Date;
                    dtpChange.Value = (DateTime.TryParse(value, out dtTemp)) ? dtTemp : DateTime.Now.Date;
                }
                else
                    dtpChange.Value = DateTime.Now.Date;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string DownloadDate
        {
            get
            {
                return dtpDownload.Value.ToString();
            }
            set
            {
                if (value != null)
                {
                    DateTime dtTemp = DateTime.Now.Date;
                    dtpDownload.Value = (DateTime.TryParse(value, out dtTemp)) ? dtTemp : DateTime.Now.Date;
                }
                else
                    dtpDownload.Value = DateTime.Now.Date;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string SiteHandlerName
        {
            get { return cbVersionControl.Text; }
            set
            {
                bool found = false;
                if (!string.IsNullOrEmpty(value))
                {
                    foreach (var siteHandler in SiteHandlerManager.SiteHandlerArray)
                    {
                        if (value != siteHandler.Name)
                            continue;

                        cbVersionControl.SelectedItem = siteHandler.Name;
                        found = true;
                        break;
                    }
                }

                if (!found)
                    cbVersionControl.SelectedIndex = 0;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string ModURL
        {
            get
            {
                return tbVersionControlURL.Text;
            }
            set
            {
                tbVersionControlURL.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string AdditionalURL
        {
            get
            {
                return tbAdditionalURL.Text;
            }
            set
            {
                tbAdditionalURL.Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string KSPVersion
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        #endregion


        public frmEditModInfo()
        {
            InitializeComponent();

            cbVersionControl.Items.Clear();
            cbVersionControl.Items.Add(Messages.NONE);
            cbVersionControl.SelectedIndex = 0;
            foreach (var siteHandler in SiteHandlerManager.SiteHandlerArray)
                cbVersionControl.Items.Add(siteHandler.Name);
        }


        private void btnGotoSpaceport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ModURL))
                MessageBox.Show(this, Messages.MSG_ENTER_VALID_URL_FIRST);
            else
            {
                try
                {
                    ISiteHandler siteHandler = SiteHandlerManager.GetSiteHandlerByName(cbVersionControl.SelectedItem as string);
                    if (siteHandler == null)
                        return;

                    ModInfo newModInfo = null;
                    if (siteHandler.IsValidURL(ModURL))
                        newModInfo = siteHandler.GetModInfo(ModURL);

                    if (newModInfo != null)
                        ModInfo = newModInfo;
                }
                catch (Exception ex)
                {
                    string msg = string.Format(Messages.MSG_ERROR_DURING_MODINFO_UPDATE, ModURL, Environment.NewLine, ex.Message);
                    MessageBox.Show(this, msg, Messages.MSG_TITLE_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Log.AddErrorS(msg, ex);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // check for one valid url or users permission.
            ISiteHandler siteHandler = SiteHandlerManager.GetSiteHandlerByName(cbVersionControl.SelectedItem as string);
            if (siteHandler == null || siteHandler.IsValidURL(ModURL) || (MessageBox.Show(this, Messages.MSG_INVALID_URL_SAVE_ANYWAY, Messages.MSG_TITLE_ATTENTION, MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
