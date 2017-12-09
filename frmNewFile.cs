/**********************************************************
* frmNewFile.cs
*
* This is form handles adding snippets to the program.
*   Users can specify the folder for the snippet to go 
*   in and a snippet name. They can also create a new
*   folder from the combobox
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
    public partial class frmNewFile : Form
    {
        /*------------------------------------------------------------------------------------------
         * Form Initializers
         -----------------------------------------------------------------------------------------*/
        private static String parentDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Snippet");
        private static String langFilePath = Path.Combine(parentDir, ".languages.txt");
        private List<String> langList = new List<String>();
        private frmSnippet fs;

        public frmNewFile(frmSnippet frmSnippet)
        {
            InitializeComponent();
            fs = frmSnippet;
        }

        /*------------------------------------------------------------------------------------------
         * Form Handlers
        -----------------------------------------------------------------------------------------*/
        /* frmNewFile_Load
         * This function loads the list of languages upon loading of the form
         */
        private void frmNewFile_Load(object sender, EventArgs e)
        {
            loadLangList();
        }

        /*------------------------------------------------------------------------------------------
         * Button Handlers
         -----------------------------------------------------------------------------------------*/
        /* btnSave_Click
         * Creates the file as long as a language has been selected
         *   from the dropdown list and a name has been entered
         * Handles if language has not been selected or name not entered
         */
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "" || tbName.Text == "Snippet Name...")
            {
                MessageBox.Show("Please enter a name for your new Snippet", "Alert", MessageBoxButtons.OK);
            }
            else if (string.IsNullOrEmpty(cbLanguages.Text))
            {
                MessageBox.Show("Please select a language", "Alert", MessageBoxButtons.OK);
            }
            else
            {
                if (createFile())
                {
                    fs.loadFileDisplay(); // call loadFileDisplay() for the main form so that it is repopulated with new langs/snips
                    fs.clearForm();       // clear out previous snip from main form
                    Close();
                }
                else
                {
                    MessageBox.Show("An error occurred. Please try again.", "Error", MessageBoxButtons.OK);
                }
            }
        }

        /* btnQuit_Click
         * Closes the program
         */
        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /*------------------------------------------------------------------------------------------
        * Other Handlers
        -----------------------------------------------------------------------------------------*/
        /* tbName_MouseDown
         * Clears the text of the textbox when the user enters it
         *   for the first time
         */
        private void tbName_MouseDown(object sender, MouseEventArgs e)
        {
            if (tbName.Text == "Snippet Name...")
            {
                tbName.Text = "";
            }
        }

        /* cbLanguages_SelectedIndexChanged
         * "Add Language" option is always the last on the list, and if it is
         *   selected then create and show an instance of the Add Language form
         */
        private void cbLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLanguages.SelectedIndex == cbLanguages.Items.Count - 1)
            {
                frmAddLanguage fal = new frmAddLanguage(this, langList);
                fal.ShowDialog();
            }
        }

        /*------------------------------------------------------------------------------------------
         * Other Functions
         -----------------------------------------------------------------------------------------*/
        /* loadLangList
         * Reads the .languages.txt file and populates the dropdown
         *   list, adds "Add New Language" to the end of the list
         */
        public void loadLangList()
        {
            var languages = File.ReadLines(langFilePath);
            langList.Clear();
            foreach (String lang in languages)
            {
                langList.Add(lang);
            }
            langList.Sort();
            langList.Add("Add New Language");
            cbLanguages.Items.Clear();
            foreach (String lang in langList)
            {
                cbLanguages.Items.Add(lang);
            }
        }

        /* createFile
         * Creates the file to store the snippet. If the file already exists,
         *   user is asked to confirm or cancel the overwrite
         */
        private bool createFile()
        {
            String dirPath = Path.Combine(parentDir, cbLanguages.Text);
            String filePath = Path.Combine(dirPath, tbName.Text.ToString() + ".txt");
            Directory.CreateDirectory(dirPath); // creates folder for selected lang if not exists
            if (!File.Exists(filePath))
            {
                try
                {
                    var myFile = File.Create(filePath); // create the file
                    myFile.Close();                     // free up the file
                }
                catch (Exception e)
                {
                    MessageBox.Show("An error occurred. Please try again.\n" + e, "Error", MessageBoxButtons.OK);
                    return false;
                }
                return true;
            }
            else 
            {
                if (MessageBox.Show("File already exists. Overwrite?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(filePath);              // delete old file
                        var myFile = File.Create(filePath); // create new one
                        myFile.Close();                     // free it up
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("An error occurred. Please try again.\n" + e, "Error", MessageBoxButtons.OK);
                        return false;
                    }
                    return true;
                }
                else
                {
                    tbName.Focus(); // if they don't want to overwrite their file, send them back to enter new name
                    return false;
                }
            }
        }
    }
}
