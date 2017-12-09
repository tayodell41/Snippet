/**********************************************************
* ReadOnlyTextBox.cs
*
* This class gives us a custom textbox to use in our form
*   that can hide the caret and doesn't change the 
*   font color or BackColor. This class is a modified version
*   of the one given by Mikhail Semenov on this StackOverflow thread:
*   https://stackoverflow.com/questions/3730968/how-to-disable-cursor-in-textbox
*
* Author: Taylor O'Dell
* Date Created: 11/28/17
* Last Modified by: Taylor O'Dell
* Date Last Modified: 12/7/17
* Part of: Snippet
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snippet
{
    public class ReadOnlyTextBox : TextBox
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowCaret(IntPtr hWnd);

        public ReadOnlyTextBox()
        {
            this.ReadOnly = true;
            this.BackColor = System.Drawing.Color.White;
            this.GotFocus += TextBoxGotFocus;
            this.Cursor = Cursors.Arrow; // mouse cursor like in other controls
        }

        private void TextBoxGotFocus(object sender, EventArgs args)
        {
            if (this.ReadOnly == true)
            {
                HideCaret(this.Handle);
            }
            else
            {
                ShowCaret(this.Handle);
            }
        }
    }
}
