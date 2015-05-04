using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Core.Utils;

namespace KSPModAdmin.Core.Views
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class frmSelectDownload : frmBase
    {
        /// <summary>
        /// List of links that could be selected.
        /// </summary>
        public List<DownloadInfo> Links
        {
            get
            {
                if (cbLinks.Items.Count > 0)
                    return cbLinks.Items.Cast<DownloadInfo>().ToList();
                else
                    return new List<DownloadInfo>();
            }
            set
            {
                cbLinks.Items.Clear();
                if (value != null && value.Count > 0)
                {
                    foreach (var e in value)
                        cbLinks.Items.Add(e);
                    cbLinks.SelectedItem = cbLinks.Items[0];
                    cbLinks.DropDownWidth = DropDownWidth(cbLinks);
                }
                else
                    cbLinks.SelectedItem = null;
            }
        }

        /// <summary>
        /// The selected link.
        /// </summary>
        public DownloadInfo SelectedLink
        {
            get
            {
                return (DownloadInfo)cbLinks.SelectedItem;
            }
            set
            {
                if (value != null)
                {
                    foreach (DownloadInfo info in cbLinks.Items)
                    {
                        if (info.DownloadURL == value.DownloadURL)
                            cbLinks.SelectedItem = info;
                    }
                }
                else
                {
                    cbLinks.SelectedItem = null;
                }
            }
        }

        /// <summary>
        /// Creates a new instance of the frmSelectDownload class.
        /// </summary>
        public frmSelectDownload()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new instance of the frmSelectDownload class.
        /// </summary>
        public frmSelectDownload(List<DownloadInfo> links)
        {
            InitializeComponent();

            Links = links;
        }

        /// <summary>
        /// Creates a new instance of the frmSelectDownload class.
        /// </summary>
        public frmSelectDownload(List<DownloadInfo> links, DownloadInfo selectedLink)
        {
            InitializeComponent();

            Links = links;
            SelectedLink = selectedLink;
        }


        private void frmSelectDownloadURL_Load(object sender, EventArgs e)
        {
            cbLinks.Select();
            cbLinks.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Gets the KSPDialogResult.
        /// </summary>
        /// <returns>The KSPDialogResult.</returns>
        public override KSPDialogResult GetKSPDialogResults()
        {
            return new KSPDialogResult(DialogResult, SelectedLink);
        }


        /// <summary>
        /// Calculates the needed width for the ComboBox DropDown to fit entry lengths.
        /// </summary>
        /// <param name="myCombo">The comboBox to calculate the new width for.</param>
        /// <returns>The new calculated ComboBox DropDown width.</returns>
        private int DropDownWidth(ComboBox myCombo)
        {
            int maxWidth = 0, temp = 0;
            foreach (var obj in myCombo.Items)
            {
                temp = TextRenderer.MeasureText(obj.ToString(), myCombo.Font).Width;
                if (temp > maxWidth)
                    maxWidth = temp;
            }

            return maxWidth;
        }
    }
}
