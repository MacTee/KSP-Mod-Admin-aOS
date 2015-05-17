using System.Diagnostics.CodeAnalysis;
using KSPModAdmin.Core.Views;

namespace KSPModAdmin.Plugin.BackupTab.Views
{
    /// <summary>
    /// This form let the user edit the note of a backup.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class frmEditNote : frmBase
    {
        /// <summary>
        /// Creates a instance off the class frmEditNote.
        /// This form let the user edit the note of a backup.
        /// </summary>
        public frmEditNote()
        {
            InitializeComponent();
            tbNote.Focus();
        }

        /// <summary>
        /// Gets or sets the note of the backup.
        /// </summary>
        public string BackupNote { get { return tbNote.Text; } set { tbNote.Text = value; } }

        /// <summary>
        /// Gets or sets the name of the backup.
        /// </summary>
        public string BackupName { get { return tbName.Text; } set { tbName.Text = value; } }
    }
}
