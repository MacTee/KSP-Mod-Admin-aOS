using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;

namespace KSPModAdmin.Translation.Plugin
{
    public class TranslationController : BaseController<TranslationController, ucTranslationView>
    {
        #region Overrides

        protected override void Initialize()
        {

        }

        protected override void AsyncroneTaskDone(object sender)
        {
            View.SetEnabledOfAllControls(true);
        }

        protected override void AsyncroneTaskStarted(object sender)
        {
            View.SetEnabledOfAllControls(false);
        }

        protected override void LanguageHasChanged(object sender)
        {
            // do nothing;
        }

        #endregion


        public static LanguageSelectInfo[] GetAvailableLanguages(string filePath = null)
        {
            List<LanguageSelectInfo> result = new List<LanguageSelectInfo>();
            if (string.IsNullOrEmpty(filePath))
                filePath = Path.Combine(Constants.LANGUAGE_FOLDER, string.Empty);

            try
            {
                if (!Directory.Exists(filePath))
                    return result.ToArray();

                foreach (var file in Directory.GetFiles(filePath))
                    result.Add(new LanguageSelectInfo() { Name = Path.GetFileName(file), Path = file});
            }
            catch (Exception ex)
            {
                string msg = string.Format(Messages.MSG_ERROR_0_DURING_LOADING_LANGUAGES, ex.Message);
                Messenger.AddError(msg, ex);
                MessageBox.Show(View.ParentForm, msg, Core.Messages.MSG_TITLE_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result.ToArray();
        }

        public static LanguageFileContent LoadSelectedLanguage(string filename)
        {
            LanguageFileContent result = null;

            if (string.IsNullOrEmpty(filename) || !File.Exists(filename))
                return result;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);

                XmlNodeList nodes = doc.GetElementsByTagName(Constants.LANGUAGE);
                if (nodes.Count > 0)
                {
                    result = new LanguageFileContent(filename, nodes[0]);
                    CreateChildEntries(nodes[0], ref result);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format(Messages.MSG_ERROR_0_DURING_LOADING_LANGUAGES, ex.Message);
                Messenger.AddError(msg, ex);
                MessageBox.Show(View.ParentForm, msg, Core.Messages.MSG_TITLE_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }

        private static void CreateChildEntries(XmlNode node, ref LanguageFileContent content)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                content.Entries.Add(new LanguageEntry(childNode));
                CreateChildEntries(childNode, ref content);
            }
        }
    }
}
