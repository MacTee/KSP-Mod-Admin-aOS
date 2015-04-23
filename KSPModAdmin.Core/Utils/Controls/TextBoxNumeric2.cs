using System.Globalization;
using System.Media;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Utils.Controls
{
    /// <summary>
    /// TextBox Control that allows only numeric input.
    /// </summary>
    public class TextBoxNumeric2 : TextBox
    {
        private bool allowSpace = false;
        private bool allowDecimalSeparator = false;
        private bool allowGroupSeparator = false;
        private bool allowNegativeSign = false;

        /// <summary>
        /// Gets the Text of the TextBox as int.
        /// </summary>
        public int IntValue
        {
            get
            {
                return int.Parse(this.Text);
            }
        }

        /// <summary>
        /// Gets the Text of the TextBox as decimal.
        /// </summary>
        public decimal DecimalValue
        {
            get
            {
                return decimal.Parse(this.Text);
            }
        }

        /// <summary>
        /// Flag to determine if white spaces are allowed.
        /// </summary>
        public bool AllowSpace
        {
            set
            {
                this.allowSpace = value;
            }

            get
            {
                return this.allowSpace;
            }
        }

        /// <summary>
        /// Flag to determine if decimal separators are allowed.
        /// </summary>
        public bool AllowDecimalSeparator
        {
            set
            {
                this.allowDecimalSeparator = value;
            }

            get
            {
                return this.allowDecimalSeparator;
            }
        }

        /// <summary>
        /// Flag to determine if group separators are allowed.
        /// </summary>
        public bool AllowGroupSeparator
        {
            set
            {
                this.allowGroupSeparator = value;
            }

            get
            {
                return this.allowGroupSeparator;
            }
        }

        /// <summary>
        /// Flag to determine if the negative sign are allowed.
        /// </summary>
        public bool AllowNegativeSign
        {
            set
            {
                this.allowNegativeSign = value;
            }

            get
            {
                return this.allowNegativeSign;
            }
        }

        /// <summary>
        /// Restricts the entry of characters to digits (including hex), the negative sign,
        /// the decimal point, and editing keystrokes (backspace).
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            if (char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (this.allowDecimalSeparator && keyInput.Equals(decimalSeparator))
            {
                // Decimal separator may be OK
            }
            else if (this.allowGroupSeparator && keyInput.Equals(groupSeparator))
            {
                // Group separator may bes OK
            }
            else if (this.allowNegativeSign && keyInput.Equals(negativeSign))
            {
                // Negative sign may be OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else if (this.allowSpace && e.KeyChar == ' ')
            {
                // Space key may be OK
            }
            // else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
            // {
            //  // Let the edit control handle control and alt key combinations
            // }
            else
            {
                // Consume this invalid key and beep
                e.Handled = true;
                // SystemSounds.Asterisk.Play();
                SystemSounds.Beep.Play();
                // SystemSounds.Exclamation.Play();
                // SystemSounds.Hand.Play();
                // SystemSounds.Question.Play();
            }
        }
    }
}
