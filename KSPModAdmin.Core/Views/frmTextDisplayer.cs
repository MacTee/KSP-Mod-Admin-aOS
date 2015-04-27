using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Views
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class frmTextDisplayer : Form
    {
        /// <summary>
        /// Creates a new instance of the frmTextDisplayer class.
        /// </summary>
        public frmTextDisplayer()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
