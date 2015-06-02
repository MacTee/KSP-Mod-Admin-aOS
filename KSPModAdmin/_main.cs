using System;
using System.Windows.Forms;
using KSPModAdmin.Core.Controller;

namespace KSPModAdmin
{
    /// <summary>
    /// Entry point
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainController.ShowMainForm();
        }
    }
}
