using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Utils.Controls
{
    /// <summary>
    /// TextBox Control that allows only numeric input.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class TextBoxNumeric : TextBox
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Minus { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Plus { get; set; }

        public string DecimalSeparator { get; set; }

        public string ValidCharacters { get; set; }

        public double Minimum { get; set; }
        public double Maximum { get; set; }


        public TextBoxNumeric()
        {
            DecimalSeparator = ",";
            Minus = "-";
            Plus = "+";
            ValidCharacters = "-+.,1234567890";
        }


        /// <summary>
        /// Handles the KeyPress event.
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                if (!ValidCharacters.Contains(e.KeyChar.ToString()))
                    e.Handled = true;

                if (Plus == e.KeyChar.ToString() && (SelectionStart != 0 || Text.StartsWith(Plus) || Text.StartsWith(Minus)))
                    e.Handled = true;

                if (Minus == e.KeyChar.ToString() && (SelectionStart != 0 || Text.StartsWith(Plus) || Text.StartsWith(Minus)))
                    e.Handled = true;

                double value = 0;
                string newText = GetNewText(e.KeyChar);
                if (!string.IsNullOrEmpty(newText) && double.TryParse(newText, out value) &&
                    (value < Minimum || value > Maximum))
                    e.Handled = true;
            }

            // only allow one decimal separator
            if (e.KeyChar.ToString() == DecimalSeparator && Text.Contains(DecimalSeparator))
                e.Handled = true;
        }

        private string GetNewText(char p)
        {
            string newText = string.Empty;
            if (SelectionLength > 0)
                newText = Text.Remove(SelectionStart, SelectionLength);

            if (SelectionStart < newText.Length)
                return newText.Insert(SelectionStart, p.ToString());
            else
                return newText + p;
        }
    }
}
