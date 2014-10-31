using System;
using System.Windows.Forms;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Localization;
using KSPModAdmin.Core.Views;

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

            // Try load languages.
            bool langLoadFailed = false;
            try
            {
                Localizer.GlobalInstance.DefaultLanguage = "eng";
                langLoadFailed = !Localizer.GlobalInstance.LoadLanguages(KSPPathHelper.GetPath(KSPPaths.LanguageFolder), true);
            }
            catch
            {
                langLoadFailed = true;
            }

            if (langLoadFailed)
            {
                MessageBox.Show("Can not load languages!" + Environment.NewLine + "Fall back to defalut language: English", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Localizer.GlobalInstance.Clear();
            }

//Localizer.GlobalInstance.CurrentLanguage = "fake";
//var a = new frmUpdateDLG();
//a.ShowDialog();

            // Run KSP MA
            frmMain main = new frmMain();
            if (!MainController.IsShutDown)
                Application.Run(main);
        }
    }
}
