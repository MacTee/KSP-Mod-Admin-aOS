using System;
using System.Windows.Forms;
using KSPModAdmin.Core.Controller;

namespace KSPModAdmin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainController.ShowMainForm();
        }
    }
}
