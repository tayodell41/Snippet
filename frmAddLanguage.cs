/**********************************************************
* frmAddLanguage.cs
*
* This form handles the creation of a new folder for 
*   snippet organization.
*
* Author: Taylor O'Dell
* Date Created: 11/28/17
* Last Modified by: Taylor O'Dell
* Date Last Modified: 11/28/17
* Part of: Snippet
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snippet
{
    public partial class frmAddLanguage : Form
    {
        /*------------------------------------------------------------------------------------------
         * Form Initializers
         -----------------------------------------------------------------------------------------*/
        private static String parentDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Snippet");
        private static String langFilePath = Path.Combine(parentDir, ".languages.txt");
        private frmNewFile fnf;

        public frmAddLanguage(frmNewFile frmNewFile)
        {
            InitializeComponent();
            fnf = frmNewFile;
        }

        /*------------------------------------------------------------------------------------------
         * Button Handlers
         -----------------------------------------------------------------------------------------*/
        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbAddLanguage.Text == "")
            {
                MessageBox.Show("Please enter a language name.", "Alert", MessageBoxButtons.OK);
            }
            else
            {
                using (StreamWriter sw = File.AppendText(langFilePath))
                {
                    sw.WriteLine(tbAddLanguage.Text);
                }
                fnf.loadLangList();
                Close();
            }
        }

        /*------------------------------------------------------------------------------------------
         * Other Handlers
         -----------------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------
         * Other Functions
         -----------------------------------------------------------------------------------------*/
    }
}
