using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace KSPModAdmin.Plugin.Translation.Executable
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class frmMain : Form
    {
        /// <summary>
        /// Creates a new instance of the frmMain class of the translation plugin.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }
    }
}
