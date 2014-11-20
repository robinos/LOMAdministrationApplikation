using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

/*
 * Code based on JRINC currencyBox from Code Project though it does not behave
 * the same way.
 * http://www.codeproject.com/Articles/248989/A-Currency-Masked-TextBox-from-TextBox-Class
 * 
 * Added changes to OnKeyPress to allow separators.
 * Added changes to formatText for altered behaviour - numbers are now
 * displayed to the user as typed with separators.  Instead of formatting
 * 10000 to 100,00 it now formats it to 10 000,00 - so it autocompletes for
 * the user instead of assuming format.
 * Added an alternate form so that numbers with decimal amounts but without a
 * decimal separator can be used to enforce a minimum size, autocompleting
 * instead in the style of (5 decimals) 345 -> 00345.
 * 
 * Robin Osborne 17/10
 */
namespace JRINCCustomControls
{
	/// <summary>
	/// The Currency Text Box autocompletes currency ammounts with custom decimal
	/// separator, thousands separator, number of decimal places and prefix.
	/// </summary>
    public partial class currencyTextBox : TextBox
    {
        /// <summary>
        /// CurrencyTextBox
        /// </summary>
        public currencyTextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// currencyTextBox
        /// </summary>
        /// <param name="container"></param>
        public currencyTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private string _workingText, _preFix;
        private char _thousandsSeparator = ' ', _decimalsSeparator = ',';
        private int _decimalPlaces = 2;

        /// <summary>
        /// Contains the entered text without prefix.
        /// </summary>
        public string WorkingText
        {
            get { return _workingText; }
            private set { _workingText = value; }
        }

        /// <summary>
        /// Contains the prefix that preceed the inputted text.
        /// </summary>
        public string PreFix
        {
            get { return _preFix; }
            set { _preFix = value; }
        }

        /// <summary>
        /// Contains the separator symbol for thousands.
        /// </summary>
        public char ThousandsSeparator
        {
            get { return _thousandsSeparator; }
            set { _thousandsSeparator = value; }
          
        }

        /// <summary>
        /// Contains the separator symbol for decimals.
        /// </summary>
        public char DecimalsSeparator
        {
            get { return _decimalsSeparator; }
            set { _decimalsSeparator = value; }
        }

        /// <summary>
        /// Indicates the total places for decimal values.
        /// </summary>
        public int DecimalPlaces
        {
            get { return _decimalPlaces; }
            set { _decimalPlaces = value; }
        }

        /// <summary>
        /// Formats the entered text.
        /// </summary>
        /// <returns></returns>
        public string formatText(string input)
        {
			if (input == null || input == "")
				input = this.Text;

			//The WorkingText without thousands separator, but with decimal
			//separator
			this.WorkingText = input.Replace((_preFix != "") ? _preFix : " ", String.Empty)
										.Replace((_thousandsSeparator.ToString() != "") ? _thousandsSeparator.ToString() : " ", String.Empty).Trim();

			//A temp string for building decimal amounts
			StringBuilder tempStr = new StringBuilder();
			//A string array for splitting the input on decimal separator
			string[] workingTemp = new string[DecimalPlaces];

			//If WorkingText is empty, initialise it
			if (this.WorkingText.Equals(""))
			{
				this.WorkingText = "0";
			}

			//Give empty 0 values a value of 0+decimal separator (which may
			//be an empty "")
			if (!this.WorkingText.Contains(_decimalsSeparator.ToString()) && _decimalsSeparator != Char.MinValue)
			{
				if (double.Parse(this.WorkingText) == 0d)
				{
					this.WorkingText = "0" + _decimalsSeparator.ToString();
				}
			}

			//Remove leading 0s if there is a decimal separator character
			//OR if there isn't a separator but there are also no decimal
			//places expected.
			//Otherwise leading 0s are allowed
			if (_decimalsSeparator != Char.MinValue ||
				(_decimalsSeparator == Char.MinValue && DecimalPlaces <= 0))
			{
				this.WorkingText = this.WorkingText.TrimStart('0');
			}

			//If the first entered character is a decimal separator, a 0 is
			//inserted in front of it.
			if (this.WorkingText.Substring(0, 1).Equals(_decimalsSeparator.ToString()) && _decimalsSeparator != Char.MinValue)
			{
				this.WorkingText = "0" + this.WorkingText;
			}

			//If the WorkingText contains a decimal separator, which isn't
			//an empty "", split the WorkingText on it
			if (this.WorkingText.Contains(_decimalsSeparator.ToString()) && _decimalsSeparator != Char.MinValue)
			{
				workingTemp = this.WorkingText.Split(_decimalsSeparator);
			}
			//If it doesn't contain a decimal separator but should, add
			//trailing 0s
			else if ( (!this.WorkingText.Contains(_decimalsSeparator.ToString()) ) && _decimalsSeparator != Char.MinValue)
			{
				//Calculate maximum possible trailing 0s
				for (int i = 0; i < this.DecimalPlaces; i++)
				{
					tempStr.Append("0");
				}

				workingTemp[0] = this.WorkingText;
				workingTemp[1] = tempStr.ToString();
			}
			//If it the decimal separator is defined as as empty ""
			else
			{
				if(DecimalPlaces > 0)
				{
					workingTemp[0] = "";
					workingTemp[1] = this.WorkingText;
				}
				else
				{
					workingTemp[0] = this.WorkingText;
					workingTemp[1] = "";
				}
			}

			bool appendingAutocomplete = true;

			if(_decimalsSeparator == Char.MinValue && DecimalPlaces > 0)
			{
				appendingAutocomplete = false;
			}

			//The number before the decimal in workingTemp[0], and the decimal
			//part of the number is stored in workingTemp[1].  Further separators
			//are ignored.
			
			//If the number of decimal digits is not exact
			if(workingTemp[1].Length != this.DecimalPlaces)
			{
				//If there are too many decimal digits, crop to the specified amount (no rounding)
				if(workingTemp[1].Length > this.DecimalPlaces)
				{
					workingTemp[1] = workingTemp[1].Substring(0, this.DecimalPlaces);
				}
				//Otherwise add trailing 0s for missing decimals
				else if(workingTemp[1].Length < this.DecimalPlaces)
				{
					tempStr.Append(workingTemp[1]);
					for(int i = workingTemp[1].Length;i < this.DecimalPlaces;i++)
					{
						if(appendingAutocomplete)
							tempStr.Append("0");
						else
							tempStr.Insert(0, "0");
					}
					workingTemp[1] = tempStr.ToString();
				}
			}

			//Put the two parts of WorkText back together with decimal separator
			if (_decimalsSeparator != Char.MinValue && DecimalPlaces > 0)
			{
				this.WorkingText = workingTemp[0] + _decimalsSeparator.ToString() + workingTemp[1];
			}
			else if(_decimalsSeparator != Char.MinValue && DecimalPlaces == 0)
			{
				this.WorkingText = workingTemp[0];
			}
			else
			{
				this.WorkingText = workingTemp[1];
			}

			//Since all decimal handling is above, the original algorithm has been
			//changed to only add thousand separators.
			int counter = 1;
			char[] charArrayTemp = this.WorkingText.ToCharArray();
			StringBuilder str = new StringBuilder();
			int expectedDecimalPositon = charArrayTemp.Length - 1 - this.DecimalPlaces;

			//This for loop cycles backwards through the character array of
			//WorkingText adding it character by character to the StringBuilder str.
			//It inserts thousand separators where needed.
			for (int i = charArrayTemp.Length - 1; i >= 0; i--)
            {
				str.Insert(0, charArrayTemp.GetValue(i));

				if (counter % 3 == 0 && i < expectedDecimalPositon && i > 0)
                {
                    if (_thousandsSeparator != Char.MinValue)
                        str.Insert(0, _thousandsSeparator);
                }

				if (i < expectedDecimalPositon)
					counter++;
            }

			//WorkingText is changed to the formatted string minus the prefix
			//(the actual representation), while the textbox Text contains the
			//prefix.  
			string formattedString = (this._preFix != "" && str.ToString() != "") ? _preFix + " " + str.ToString() : (str.ToString() != "") ? str.ToString() : "";			
			this.WorkingText = formattedString.Replace((_preFix != "") ? _preFix : " ", String.Empty);

            return (formattedString);
        }

		/// <summary>
		/// Upon losing focus the currencyTextBox text is formatted
		/// </summary>
        protected override void OnLostFocus(EventArgs e)
        {
            this.Text = formatText("");
            base.OnLostFocus(e);
        }

		/// <summary>
		/// Upon gaining focus the text becomes the working text (no monetary sign
		/// or thousands separators)
		/// </summary>
        protected override void OnGotFocus(EventArgs e)
        {
            this.Text = this.WorkingText;
            base.OnGotFocus(e);
        }

		/// <summary>
		/// On key press if the characters is NOT a number or a decimal separator,
		/// that key is erased (backspace).
		/// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
			//Decimal separators and numbers are allowed to be enterd by the user
			if (!Char.IsDigit(e.KeyChar)
				&& !(e.KeyChar.Equals(_decimalsSeparator)))
            {
                if (!(e.KeyChar == Convert.ToChar(Keys.Back)))
                    e.Handled = true;
            }
            base.OnKeyPress(e);
        }
    }
}
