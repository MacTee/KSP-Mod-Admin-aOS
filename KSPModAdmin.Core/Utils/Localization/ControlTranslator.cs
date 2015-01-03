using System;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace KSPModAdmin.Core.Utils.Localization
{
    /// <summary>
    /// Class to create translation files for a the controls of a Form/UserControl and 
    /// to translate these controls.
    /// </summary>
    public static class ControlTranslator
    {
        #region Constants

        // All used string in this class.
        private const char INDENT = '\t';
        private const string NEWLINE_REPLACE_CHAR = "^";
        private const string LANGUAGE = "Language";
        private const string LONGNAME = "LongName";
        private const string NAME = "Name";
        private const string TEXT = "Text";
        private const string TOOLTIP = "_ToolTip";
        private const string CB_ITEM = "_cbItem_";
        private const string ITEM = "_Item_";
        private const string COLUMN = "_Column_";
        private const string GROUP = "_Group_";
        private const string SIMPLE_SETTINGS_ENTRY = "{2}{0} = \"{1}\"";
        private const string CHILD_CONTROL_XML_NODE = "{2}<ChildControl Name=\"{0}\" Value=\"{1}\"/>";
        private const string OPEN_ENDING_CONTROL_XML_NODE = "{2}<Control Name=\"{0}\" Value=\"{1}\"";
        private const string OPEN_CONTROL_XML_NODE = OPEN_ENDING_CONTROL_XML_NODE + ">";
        private const string CLOSE_CONTROL_XML_NODE = "</Control>";
        private const string CONTROL_XML_NODE = OPEN_ENDING_CONTROL_XML_NODE + "/>";
        private const string OPEN_LANGUAGE_XML_NODE_0_1 = "<Language Name=\"{0}\" LongName=\"{1}\">";
        private const string CLOSED_LANGUAGE_XML_NODE = "</Language>";
        private const string MSG_LANGUAGE_0_NOT_FOUND = "Translation failed! Language \"{0}\" not found! Fall back to default language \"English\"";

        #endregion

        #region Enumerations

        /// <summary>
        /// Possible XML Node types.
        /// </summary>
        enum NodeType
        {
            OpenControl,
            ClosedControl,
            ChildControl,
            SimpleSettings
        }

        #endregion


        /// <summary>
        /// Loads the passed language file and translates the control and all its childes to the passed language.
        /// The displayed Text and the ToolTip of the controls will be translated, if
        /// the value is found.
        /// </summary>
        /// <param name="filename">Path and file name to the language file to load.</param>
        /// <param name="control">The Control to translate (Forms or UserControls)</param>
        /// <param name="language">The language to translate the controls to.</param>
        public static void TranslateControls(string filename, Control control, string language)
        {
            Localizer localizer = new Localizer(filename);
            localizer.LoadLanguageFromXml(filename);
            TranslateControls(localizer, control, language);
        }

        /// <summary>
        /// Loads the passed language file and translates the control and all its childes to the passed language.
        /// The displayed Text and the ToolTip of the controls will be translated, if
        /// the value is found.
        /// </summary>
        /// <param name="localizer">A LanguageDictionaryManager with the wanted language to translate.</param>
        /// <param name="control">The Control to translate (Forms or UserControls)</param>
        /// <param name="language">The language to translate the controls to.</param>
        public static void TranslateControls(Localizer localizer, Control control, string language = null)
        {
            TranslateControls(localizer, control, language, null);
        }


        /// <summary>
        /// Loads the passed language file and translates the control and all its childes to the passed language.
        /// The displayed Text and the ToolTip of the controls will be translated, if
        /// the value is found.
        /// </summary>
        /// <param name="localizer">A LanguageDictionaryManager with the wanted language to translate.</param>
        /// <param name="control">The Control to translate (Forms or UserControls)</param>
        /// <param name="language">The language to translate the controls to.</param>
        /// <param name="tt">For internal use! The ToolTip Control of the control to translate. If null the function tries to get is automatically.</param>
        private static void TranslateControls(Localizer localizer, Control control, string language, ToolTip tt)
        {
            if (string.IsNullOrEmpty(language))
                language = localizer.CurrentLanguage;

            if (string.IsNullOrEmpty(language) || !localizer.ContainsLaguage(language))
            {
                Messenger.AddError(string.Format(MSG_LANGUAGE_0_NOT_FOUND, language));
                return;
            }

            string name = control.Name;

            // Get ToolTip Control from Forms or UserControls.
            Form form = control as Form;
            if (form != null)
                tt = GetToolTipControl(form);
            else
            {
                UserControl userControl = control as UserControl;
                if (userControl != null)
                    tt = GetToolTipControl(userControl);
            }


            if (!string.IsNullOrEmpty(name) && localizer.ContainsKey(language, name))
            {
                string value = localizer[language, name];

                if (!string.IsNullOrEmpty(value))
                    control.Text = value;
            }
            
            MenuStrip ms = control as MenuStrip;
            ToolStrip ts = control as ToolStrip;
            if (ms != null)
            {
                foreach (ToolStripItem item in ms.Items)
                    TryReadToolStripItems(localizer, language, item);
            }

            else if (ts != null)
            {
                foreach (ToolStripItem item in ts.Items)
                    TryReadToolStripItems(localizer, language, item);
            }

            else if (TryReadFromComboBox(localizer, language, control))
            { /* do nothing */ }

            else if (TryReadFromDataGridView(localizer, language, control))
            { /* do nothing */ }

            else if (TryReadFromListView(localizer, language, control))
            { /* do nothing */ }

            else
            { /* do nothing */ }

            // Try read ToolTip
            if (tt != null)
            {
                string key = control.Name + TOOLTIP;
                if (localizer.ContainsKey(language, key))
                    tt.SetToolTip(control, localizer[language, key]);
            }

            if (control.ContextMenuStrip != null)
                TranslateControls(localizer, control.ContextMenuStrip, language, tt);

            foreach (Control childControl in control.Controls)
                TranslateControls(localizer, childControl, language, tt);
        }

        #region Try read ToolStrip

        private static void TryReadToolStripItems(Localizer localizer, string language, ToolStripItem item)
        {
            if (TryReadToolStripComboBox(localizer, language, item))
            { }

            else if (TryReadToolStripDropDownButton(localizer, language, item))
            { }

            else if (TryReadToolStripSplitButton(localizer, language, item))
            { }

            else if (TryReadToolStripMenuItem(localizer, language, item))
            { }

            else if (TryReadToolStripLabel(localizer, language, item))
            { }

            else if (TryReadToolStripTextBox(localizer, language, item))
            { }

            else if (TryReadToolStripItem(localizer, language, item as object))
            { }

            string key = item.Name + TOOLTIP;
            if (localizer.ContainsKey(language, key))
                item.ToolTipText = localizer[language, key];
        }
        private static bool TryReadToolStripComboBox(Localizer localizer, string language, object item)
        {
            ToolStripComboBox tscb = item as ToolStripComboBox;
            if (tscb != null)
            {
                for (int i = 0; i < tscb.Items.Count; ++i)
                {
                    string key = tscb.Name + CB_ITEM + (i + 1);
                    if (localizer.ContainsKey(language, key))
                        tscb.Items[i] = localizer[language, key];
                }

                return true;
            }
            return false;
        }
        private static bool TryReadToolStripDropDownButton(Localizer localizer, string language, object item)
        {
            ToolStripDropDownButton tsddb = item as ToolStripDropDownButton;
            if (tsddb != null)
            {
                // TODO: 
                string key = tsddb.Name;
                if (localizer.ContainsKey(language, key))
                    tsddb.Text = localizer[language, key];

                if (tsddb.HasDropDownItems)
                {
                    foreach (ToolStripItem child in tsddb.DropDownItems)
                        TryReadToolStripItems(localizer, language, child);
                }
                //else if (tsddb.HasDropDown)
                //{
                //    foreach (ToolStripItem child in tsddb.DropDown.Items)
                //        TryReadToolStripItems(localizer, language, child);
                //}

                return true;
            }
            return false;
        }
        private static bool TryReadToolStripSplitButton(Localizer localizer, string language, object item)
        {
            ToolStripSplitButton tssb = item as ToolStripSplitButton;
            if (tssb != null)
            {
                string key = tssb.Name;
                if (localizer.ContainsKey(language, key))
                    tssb.Text = localizer[language, key];

                if (tssb.HasDropDownItems)
                {
                    foreach (ToolStripItem child in tssb.DropDownItems)
                        TryReadToolStripItems(localizer, language, child);
                }
                //else if (tssb.HasDropDown)
                //{
                //    foreach (ToolStripItem child in tssb.DropDown.Items)
                //        TryReadToolStripItems(localizer, language, child);
                //}

                return true;
            }
            return false;
        }
        private static bool TryReadToolStripLabel(Localizer localizer, string language, object item)
        {
            return TryReadToolStripItem(localizer, language, item);
        }
        private static bool TryReadToolStripTextBox(Localizer localizer, string language, object item)
        {
            return TryReadToolStripItem(localizer, language, item);
        }
        private static bool TryReadToolStripItem(Localizer localizer, string language, object item)
        {
            ToolStripItem tsi = item as ToolStripItem;
            if (tsi != null)
            {
                string key = tsi.Name;
                if (localizer.ContainsKey(language, key))
                    tsi.Text = localizer[language, key];

                return true;
            }
            return false;
        }
        private static bool TryReadToolStripMenuItem(Localizer localizer, string language, object item)
        {
            ToolStripMenuItem tsmi = item as ToolStripMenuItem;
            if (tsmi != null)
            {
                string key = tsmi.Name;
                if (localizer.ContainsKey(language, key))
                    tsmi.Text = localizer[language, key];

                if (tsmi.HasDropDownItems)
                {
                    foreach (ToolStripItem child in tsmi.DropDownItems)
                        TryReadToolStripItems(localizer, language, child);
                }
                //else if (tsmi.HasDropDown)
                //{
                //    foreach (ToolStripItem child in tsmi.DropDown.Items)
                //        TryReadToolStripItems(localizer, language, child);
                //}

                return true;
            }
            return false;
        }

        #endregion

        #region Try read from certain control types

        private static bool TryReadFromListView(Localizer localizer, string language, Control control)
        {
            ListView lv = control as ListView;
            if (lv != null)
            {
                for (int i = 0; i < lv.Columns.Count; ++i)
                {
                    string key = lv.Name + COLUMN + (i + 1);
                    if (localizer.ContainsKey(language, key))
                        lv.Columns[i].Text = localizer[language, key];
                }

                for (int i = 0; i < lv.Groups.Count; ++i)
                {
                    string key = lv.Name + GROUP + (i + 1);
                    if (localizer.ContainsKey(language, key))
                        lv.Groups[i].Header = localizer[language, key];
                }

                for (int i = 0; i < lv.Items.Count; ++i)
                {
                    string key = lv.Name + ITEM + (i + 1);
                    if (localizer.ContainsKey(language, key))
                        lv.Items[i].Text = localizer[language, key];
                }

                return true;
            }

            return false;
        }
        private static bool TryReadFromDataGridView(Localizer localizer, string language, Control control)
        {
            DataGridView dgv = control as DataGridView;
            if (dgv != null)
            {
                for (int i = 0; i < dgv.Columns.Count; ++i)
                {
                    var column = dgv.Columns[i];
                    string key = column.Name;
                    if (localizer.ContainsKey(language, key))
                        column.HeaderText = localizer[language, key];
                }

                return true;
            }

            return false;
        }
        private static bool TryReadFromComboBox(Localizer localizer, string language, Control control)
        {
            ComboBox cb = control as ComboBox;
            if (cb != null)
            {
                for (int i = 0; i < cb.Items.Count; ++i)
                {
                    string key = cb.Name + CB_ITEM + (i + 1);
                    if (localizer.ContainsKey(language, key))
                        cb.Items[i] = localizer[language, key];
                }

                return true;
            }

            return false;
        }

        #endregion 


        /// <summary>
        /// Creates a language file for the passed Control and its child controls.
        /// </summary>
        /// <param name="control">The control to create the language file for.</param>
        /// <param name="language">The language of the translation file.</param>
        /// <param name="longName">The long name of the language.</param>
        /// <param name="withIndent">Flag to determine if a indent should be used during creation or not.</param>
        /// <param name="asXml">Flag to determine if the output file should have xml format or not.</param>
        /// <returns></returns>
        public static string CreateTranslateSettingsFileOfControls(Control control, string language = "eng", string longName = "English (default)", bool withIndent = true, bool asXml = true)
        {
            StringBuilder sb = new StringBuilder();
            if (asXml)
                sb.AppendLine(string.Format(OPEN_LANGUAGE_XML_NODE_0_1, language, longName));
            else
            {
                sb.AppendLine(string.Format(SIMPLE_SETTINGS_ENTRY, LANGUAGE, language, string.Empty));
                sb.AppendLine(string.Format(SIMPLE_SETTINGS_ENTRY, LONGNAME, longName, string.Empty));
            }

            WriteTranslationEntrys(ref sb, control, null, 1, withIndent, asXml);
            
            if (asXml)
                sb.AppendLine(string.Format(CLOSED_LANGUAGE_XML_NODE));
            
            return sb.ToString();
        }

        #region Write control translation entrys

        private static void WriteTranslationEntrys(ref StringBuilder stringBuilder, object control, ToolTip tt = null, int depth = 0, bool withIndent = true, bool asXml = false)
        {
            if (control == null)
                return;

            // Get ToolTip Control from Forms or UserControls.
            Form form = control as Form;
            if (form != null)
                tt = GetToolTipControl(form);
            else
            {
                UserControl userControl = control as UserControl;
                if (userControl != null)
                    tt = GetToolTipControl(userControl);
            }

            // If not a Control -> try Reflection.
            Control tmpControl = control as Control;
            if (tmpControl == null)
            {
                string name = string.Empty;
                string myValue = string.Empty;
                PropertyInfo prop = control.GetType().GetProperty(NAME, BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanRead)
                {
                    name = prop.GetValue(control, null).ToString();

                    ToolStripItem tsi = control as ToolStripItem;
                    if (tsi != null)
                        WriteToolStripItemEntrys(ref stringBuilder, tsi, name, depth, withIndent, asXml);
                    else
                    {
                        prop = control.GetType().GetProperty(TEXT, BindingFlags.Public | BindingFlags.Instance);
                        if (null != prop && prop.CanRead)
                        {
                            myValue = prop.GetValue(control, null).ToString();

                            if (!string.IsNullOrEmpty(name))
                                WriteEntry(ref stringBuilder, name, myValue, depth, withIndent, (!asXml) ? NodeType.SimpleSettings : NodeType.ClosedControl);
                        }
                    }
                }
            }

                // Is Control!
            else
            {
                string name = tmpControl.Name;
                bool written = false;

                ToolStrip ts = control as ToolStrip;
                if (ts != null)
                {
                    written = true;

                    bool hasChilds = (ts.Items.Count > 0);
                    WriteControlEntry(ref stringBuilder, tmpControl, tt, depth, withIndent, asXml, hasChilds);

                    foreach (var item in ts.Items)
                        WriteTranslationEntrys(ref stringBuilder, item, tt, depth, withIndent, asXml);

                    if (asXml && hasChilds)
                        stringBuilder.AppendLine(new string(INDENT, depth) + CLOSE_CONTROL_XML_NODE);
                }

                //ContextMenuStrip cms = control as ContextMenuStrip;
                //if (cms != null && !string.IsNullOrEmpty(name))
                //{
                //    written = true;

                //    bool hasChilds = (cms.Items.Count > 0);
                //    WriteControlEntry(ref stringBuilder, tmpControl, tt, depth, withIndent, asXml, hasChilds);

                //    foreach (var item in cms.Items)
                //        WriteTranslationEntrys(ref stringBuilder, item, tt, depth, withIndent, asXml);

                //    if (asXml && hasChilds)
                //        stringBuilder.AppendLine(new string(INDENT, depth) + CLOSE_CONTROL_XML_NODE);
                //}

                ComboBox cb = control as ComboBox;
                if (cb != null && !string.IsNullOrEmpty(name))
                {
                    written = true;

                    bool hasChilds = (cb.Items.Count > 0);
                    WriteControlEntry(ref stringBuilder, tmpControl, tt, depth, withIndent, asXml, hasChilds);

                    int i = 0;
                    foreach (var item in cb.Items)
                        WriteEntry(ref stringBuilder, name + CB_ITEM + ++i, item.ToString(), depth + 1, withIndent, (!asXml) ? NodeType.SimpleSettings : NodeType.ChildControl);

                    if (asXml && hasChilds)
                        stringBuilder.AppendLine(new string(INDENT, depth) + CLOSE_CONTROL_XML_NODE);
                }

                DataGridView dgv = control as DataGridView;
                if (dgv != null)
                {
                    written = true;

                    bool hasChilds = (dgv.Columns.Count > 0);
                    WriteControlEntry(ref stringBuilder, tmpControl, tt, depth, withIndent, asXml, hasChilds);

                    foreach (DataGridViewColumn col in dgv.Columns)
                        WriteEntry(ref stringBuilder, col.Name, col.HeaderText, depth + 1, withIndent, (!asXml) ? NodeType.SimpleSettings : NodeType.ChildControl);

                    if (asXml && hasChilds)
                        stringBuilder.AppendLine(new string(INDENT, depth) + CLOSE_CONTROL_XML_NODE);
                }

                ListView lv = control as ListView;
                if (lv != null)
                {
                    written = true;

                    bool hasChilds = (lv.Columns.Count > 0 || lv.Groups.Count > 0 || lv.Items.Count > 0);
                    WriteControlEntry(ref stringBuilder, tmpControl, tt, depth, withIndent, asXml, hasChilds);

                    NodeType nt = (!asXml) ? NodeType.SimpleSettings : NodeType.ChildControl;
                    int i = 0;
                    foreach (ColumnHeader col in lv.Columns)
                        WriteEntry(ref stringBuilder, name + COLUMN + ++i, col.Text, depth + 1, withIndent, nt);

                    i = 0;
                    foreach (ListViewGroup group in lv.Groups)
                        WriteEntry(ref stringBuilder, name + GROUP + ++i, group.Header, depth + 1, withIndent, nt);

                    i = 0;
                    foreach (ListViewItem item in lv.Items)
                        WriteEntry(ref stringBuilder, name + ITEM + ++i, item.Text, depth + 1, withIndent, nt);

                    if (asXml && hasChilds)
                        stringBuilder.AppendLine(new string(INDENT, depth) + CLOSE_CONTROL_XML_NODE);
                }

                if (tmpControl.ContextMenuStrip != null)
                    WriteTranslationEntrys(ref stringBuilder, tmpControl.ContextMenuStrip, tt, depth + 1, withIndent, asXml);

                if (!written && !string.IsNullOrEmpty(tmpControl.Name))
                    WriteControlEntry(ref stringBuilder, tmpControl, tt, depth, withIndent, asXml, false);

                foreach (var childControl in tmpControl.Controls)
                    WriteTranslationEntrys(ref stringBuilder, childControl, tt, depth + 1, withIndent, asXml);
            }
        }

        private static void WriteToolStripItemEntrys(ref StringBuilder stringBuilder, ToolStripItem control, string name, int depth, bool withIndent, bool asXml)
        {
            ToolStripComboBox tscb = control as ToolStripComboBox;
            if (tscb != null && !string.IsNullOrEmpty(name))
            {
                bool hasChilds = (tscb.Items.Count > 0);
                WriteToolStripItemEntry(ref stringBuilder, tscb, depth + 1, withIndent, asXml, hasChilds);

                int i = 0;
                foreach (var item in tscb.Items)
                    WriteEntry(ref stringBuilder, name + CB_ITEM + ++i, item.ToString(), depth + 2, withIndent, (!asXml) ? NodeType.SimpleSettings : NodeType.ChildControl);

                if (asXml && hasChilds)
                    stringBuilder.AppendLine(new string(INDENT, depth + 1) + CLOSE_CONTROL_XML_NODE);

                return;
            }

            ToolStripDropDownButton tsddb = control as ToolStripDropDownButton;
            if (tsddb != null)
            {
                bool hasChilds = tsddb.HasDropDownItems;
                WriteToolStripItemEntry(ref stringBuilder, tsddb, depth + 1, withIndent, asXml, hasChilds);

                if (hasChilds)
                    foreach (ToolStripItem item in tsddb.DropDownItems)
                        WriteToolStripItemEntrys(ref stringBuilder, item, item.Name, depth + 2, withIndent, asXml);

                if (asXml && hasChilds)
                    stringBuilder.AppendLine(new string(INDENT, depth + 1) + CLOSE_CONTROL_XML_NODE);

                return;
            }

            ToolStripSplitButton tssb = control as ToolStripSplitButton;
            if (tssb != null)
            {
                bool hasChilds = tssb.HasDropDownItems;
                WriteToolStripItemEntry(ref stringBuilder, tssb, depth + 1, withIndent, asXml, hasChilds);

                if (hasChilds)
                    foreach (ToolStripItem item in tssb.DropDownItems)
                        WriteToolStripItemEntrys(ref stringBuilder, item, item.Name, depth + 2, withIndent, asXml);

                if (asXml && hasChilds)
                    stringBuilder.AppendLine(new string(INDENT, depth + 1) + CLOSE_CONTROL_XML_NODE);

                return;
            }

            ToolStripLabel tsl = control as ToolStripLabel;
            if (tsl != null)
            {
                WriteToolStripItemEntry(ref stringBuilder, tsl, depth + 1, withIndent, asXml, false);
                return;
            }

            ToolStripTextBox tstb = control as ToolStripTextBox;
            if (tstb != null)
            {
                WriteToolStripItemEntry(ref stringBuilder, tstb, depth + 1, withIndent, asXml, false);
                return;
            }

            ToolStripMenuItem tsmi = control as ToolStripMenuItem;
            if (tsmi != null)
            {
                bool hasChilds = (tsmi.HasDropDownItems); // || tsmi.HasDropDown);
                WriteToolStripItemEntry(ref stringBuilder, tsmi, depth + 1, withIndent, asXml, hasChilds);

                if (tsmi.HasDropDownItems)
                    foreach (ToolStripItem item in tsmi.DropDownItems)
                        WriteToolStripItemEntrys(ref stringBuilder, item, item.Name, depth + 2, withIndent, asXml);

                //else if (tsmi.HasDropDown)
                //    foreach (ToolStripItem item in tsmi.DropDown.Items)
                //        WriteToolStripItemEntrys(ref stringBuilder, item, item.Name, depth + 2, withIndent, asXml);

                if (asXml && hasChilds)
                    stringBuilder.AppendLine(new string(INDENT, depth + 1) + CLOSE_CONTROL_XML_NODE);

                return;
            }

            ToolStripButton tsb = control as ToolStripButton;
            if (tsb != null)
            {
                WriteToolStripItemEntry(ref stringBuilder, tsb, depth + 1, withIndent, asXml, false);
                return;
            }
        }

        private static void WriteToolStripItemEntry(ref StringBuilder stringBuilder, ToolStripItem tsi, int depth, bool withIndent, bool asXml, bool hasChilds)
        {
            bool hasToolTip = !string.IsNullOrEmpty(tsi.ToolTipText);
            NodeType nt = (!asXml) ? NodeType.SimpleSettings : (hasToolTip || hasChilds) ? NodeType.OpenControl : NodeType.ClosedControl;

            WriteEntry(ref stringBuilder, tsi.Name, tsi.Text, depth, withIndent, nt);
            if (hasToolTip)
            {
                WriteEntry(ref stringBuilder, tsi.Name + TOOLTIP, tsi.ToolTipText, depth + 1, withIndent, (!asXml) ? NodeType.SimpleSettings : NodeType.ChildControl);

                if (asXml && !hasChilds)
                    stringBuilder.AppendLine(new string(INDENT, depth) + CLOSE_CONTROL_XML_NODE);
            }
        }

        private static void WriteControlEntry(ref StringBuilder stringBuilder, Control tmpControl, ToolTip tt, int depth, bool withIndent, bool asXml, bool hasChilds)
        {
            string ttText = string.Empty;
            if (tt != null)
                ttText = tt.GetToolTip(tmpControl);
            bool hasToolTip = !string.IsNullOrEmpty(ttText);

            NodeType nt = (!asXml) ? NodeType.SimpleSettings : (hasToolTip || hasChilds) ? NodeType.OpenControl : NodeType.ClosedControl;
            WriteEntry(ref stringBuilder, tmpControl.Name, tmpControl.Text, depth, withIndent, nt);

            if (hasToolTip)
            {
                WriteEntry(ref stringBuilder, tmpControl.Name + TOOLTIP, ttText, depth + 1, withIndent, (!asXml) ? NodeType.SimpleSettings : NodeType.ChildControl);

                if (asXml && !hasChilds)
                    stringBuilder.AppendLine(new string(INDENT, depth) + CLOSE_CONTROL_XML_NODE);
            }
        }

        private static void WriteEntry(ref StringBuilder stringBuilder, string name, string value, int depth, bool withIndent, NodeType nodeType)
        {
            string indent = string.Empty;
            if (withIndent)
                indent = new string(INDENT, depth);

            string formatString = SIMPLE_SETTINGS_ENTRY;
            switch (nodeType)
            {
                case NodeType.ClosedControl:
                    formatString = CONTROL_XML_NODE;
                    break;
                case NodeType.OpenControl:
                    formatString = OPEN_CONTROL_XML_NODE;
                    break;
                case NodeType.ChildControl:
                    formatString = CHILD_CONTROL_XML_NODE;
                    break;
                default:
                case NodeType.SimpleSettings:
                    formatString = SIMPLE_SETTINGS_ENTRY;
                    break;
            }

            stringBuilder.AppendLine(string.Format(formatString, name, GetXmlEscapedString(value), indent));
        }

        public static string GetXmlEscapedString(string value)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode node = doc.CreateElement("root");
            value = value.Replace(Environment.NewLine, NEWLINE_REPLACE_CHAR);
            node.InnerText = value.Replace("\"", "&quot;");
            return node.InnerXml;
        }

        public static string GetXmlUnescapedString(string xmlEscapedString)
        {
            //XmlDocument doc = new XmlDocument();
            //XmlNode node = doc.CreateElement("root");
            //node.InnerXml = xmlEscapedString;
            return xmlEscapedString.Replace("&quot;", "\"").Replace(NEWLINE_REPLACE_CHAR, Environment.NewLine);
        }

        #endregion

        /// <summary>
        /// Gets the ToolTip Control from the passed control (via reflection).
        /// </summary>
        /// <param name="control">The control to search the ToolTip Control in.</param>
        /// <returns>The ToolTip Control from the passed control (via reflection).</returns>
        public static ToolTip GetToolTipControl(Control control)
        {
            ToolTip tt = null;
            FieldInfo[] fields = control.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                var member = field.GetValue(control);
                if (member != null && member.GetType() == typeof(ToolTip))
                    tt = member as ToolTip;
            }

            return tt;
        }
    }
}