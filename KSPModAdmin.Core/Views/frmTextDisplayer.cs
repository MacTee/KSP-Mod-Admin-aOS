using System;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Views
{
    public partial class frmTextDisplayer : Form
    {
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
