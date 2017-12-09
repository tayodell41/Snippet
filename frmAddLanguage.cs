/**********************************************************
* frmAddLanguage.cs
*
* This form handles the creation of a new folder for 
*   snippet organization.
*
* Author: Taylor O'Dell
* Date Created: 11/28/17
* Last Modified by: Taylor O'Dell
* Date Last Modified: 12/8/17
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
        private List<String> langList = new List<String>();
        private frmNewFile fnf;

        public frmAddLanguage(frmNewFile frmNewFile, List<String> languages)
        {
            InitializeComponent();
            fnf = frmNewFile;
            langList = languages;
        }

        /*------------------------------------------------------------------------------------------
         * Button Handlers
         -----------------------------------------------------------------------------------------*/
        /* btnQuit_Click
         * Close the program
         */
        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /* btnSave_Click
         * Writes user's new language entry to the file that stores languages
         *   then reloads the New File form dropdown box so that the new
         *   language is included
         */
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!(checkValidity()))
            {
                MessageBox.Show("Please enter a new language name.", "Alert", MessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(langFilePath))
                    {
                        sw.WriteLine(tbAddLanguage.Text.ToString());
                    }
                    fnf.loadLangList();
                    Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show("An error occurred. Please try again.\n" + err, "Error", MessageBoxButtons.OK);
                }
            }
        }

        /* checkValidity
         * Checks that user's input is not empty and that it does not already exist
         */
        private bool checkValidity()
        {
            String userString = tbAddLanguage.Text.ToString();
            if (userString == "") return false;
            foreach (String lang in langList)
            {
                if (userString.Equals(lang, StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
