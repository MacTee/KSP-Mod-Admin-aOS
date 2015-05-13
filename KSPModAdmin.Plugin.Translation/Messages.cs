using System;
using System.Diagnostics.CodeAnalysis;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Plugin.Translation
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class Messages
    {
        public static string MSG_TRANSLATION_VIEW_TITLE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_TRANSLATION_VIEW_TITLE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_TRANSLATION_VIEW_TITLE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_TRANSLATION_VIEW_TITLE = "Translation";
        
        public static string MSG_ERROR_0_DURING_LOADING_LANGUAGES
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_0_DURING_LOADING_LANGUAGES"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_0_DURING_LOADING_LANGUAGES).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_0_DURING_LOADING_LANGUAGES = "Error \"{0}\" during loading available languages!";

        public static string MSG_ERROR_0_DURING_LOADING_LANGUAGE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_0_DURING_LOADING_LANGUAGE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_0_DURING_LOADING_LANGUAGE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_0_DURING_LOADING_LANGUAGE = "Error \"{0}\" during loading of language file!";
    }
}