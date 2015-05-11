using System;
using System.Diagnostics.CodeAnalysis;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Plugin.BackupTab
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class Messages
    {
        public static string MSG_BACKUPTAB_VIEW_TITLE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUPTAB_VIEW_TITLE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUPTAB_VIEW_TITLE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUPTAB_VIEW_TITLE = "Backup";


        // Add more messages here and use them in your code to support localization.
        // When you add new messages here you have to also add new line to all language files (here: KSPMA.TemplatePlugin.eng.lang and KSPMA.TemplatePlugin.fake.lang)
        // See the MSG_PLUGIN_VIEW_TITLE for a sample.
    }
}