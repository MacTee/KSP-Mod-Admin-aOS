using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Core.Views
{
    /// <summary>
    /// Base Form class for all KSP Mod Admin forms.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class frmBase : Form, IView
    {
        /// <summary>
        /// Constructor for VS Designer only!
        /// </summary>
        public frmBase()
        {
            InitializeComponent();

#if DEBUG
            this.MouseDoubleClick += HandleMouseDoubleClick;
#endif
        }

#if DEBUG

        private void HandleMouseDoubleClick(object sender, MouseEventArgs e)
        {
            string xml = ControlTranslator.CreateTranslateSettingsFileOfControls(this);
            string filepath = Path.Combine(Application.StartupPath, string.Format("{0}.eng.lang", Name));

            var assembly = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                            from type in asm.GetTypes()
                            where type.IsClass && type.Name == this.GetType().Name
                            select asm).FirstOrDefault();

            if (assembly != null)
            {
                Type objectType = (from type in assembly.GetTypes()
                                    where type.IsClass && type.Name == "Messages"
                                   select type).FirstOrDefault();

                if (objectType != null)
                {
                    object obj = Activator.CreateInstance(objectType);

                    var p = obj.GetType().GetProperties();

                    FieldInfo[] fields = obj.GetType().GetFields(
                         BindingFlags.NonPublic | BindingFlags.Static);

                    xml = xml.Replace("</Language>", string.Empty);

                    foreach (FieldInfo fInfo in fields)
                    {
                        string name = fInfo.Name;
                        if (fInfo.FieldType != typeof(string)) 
                            continue;
                        
                        string value = fInfo.GetValue(obj).ToString();
                        string node = string.Format("<String Name=\"{0}\" Value=\"{1}\"/>", name.Replace("DEFAULT_", string.Empty), ControlTranslator.GetXmlEscapedString(value));
                        xml += node + Environment.NewLine;
                    }

                    xml += "</Language>";
                }
            }
            File.WriteAllText(filepath, xml);
            MessageBox.Show(string.Format("Control translation file saved to \"{0}\"", filepath));
        }

#endif

        /// <summary>
        /// Invokes the passed function if required.
        /// </summary>
        /// <param name="action">Function that should be invoked if required.</param>
        public void InvokeIfRequired(MethodInvoker action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        /// <summary>
        /// Forces the view to redraw.
        /// </summary>
        public void InvalidateView() { }

        /// <summary>
        /// Shows the Form.
        /// </summary>
        /// <returns>The results of the dialog.</returns>
        public new DialogResult ShowDialog()
        {
            ControlTranslator.TranslateControls(Localizer.GlobalInstance, this);
            return base.ShowDialog();
        }

        /// <summary>
        /// Shows the Form.
        /// </summary>
        /// <returns>The results of the dialog.</returns>
        public new DialogResult ShowDialog(IWin32Window p)
        {
            ControlTranslator.TranslateControls(Localizer.GlobalInstance, this);
            return base.ShowDialog(p);
        }

        /// <summary>
        /// Shows the Form.
        /// </summary>
        public new void Show()
        {
            ControlTranslator.TranslateControls(Localizer.GlobalInstance, this);
            base.Show();
        }

        /// <summary>
        /// Shows the Form.
        /// </summary>
        public new void Show(IWin32Window p)
        {
            ControlTranslator.TranslateControls(Localizer.GlobalInstance, this);
            base.Show(p);
        }


        /// <summary>
        /// Gets the KSPDialogResult.
        /// </summary>
        /// <returns>The KSPDialogResult.</returns>
        public virtual KSPDialogResult GetKSPDialogResults()
        {
            return new KSPDialogResult(DialogResult);
        }
    }


    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class KSPDialogResult
    {
        public DialogResult DialogResult { get; private set; }

        public object AdditionalResult { get; private set; }

        public Exception Exception { get; private set; }


        public KSPDialogResult(DialogResult dialogResult, object additionalResult = null, Exception ex = null)
        {
            DialogResult = dialogResult;
            AdditionalResult = additionalResult;
            Exception = ex;
        }
    }
}
