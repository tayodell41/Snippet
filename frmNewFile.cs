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
    public partial class frmNewFile : Form
    {
        /*------------------------------------------------------------------------------------------
         * Form Initializers
         -----------------------------------------------------------------------------------------*/
        private static String parentDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Snippet");
        private static String langFilePath = Path.Combine(parentDir, ".languages.txt");
        private List<String> langList = new List<String>();

        public frmNewFile()
        {
            InitializeComponent();
        }

        /*------------------------------------------------------------------------------------------
         * Form Handlers
        -----------------------------------------------------------------------------------------*/
        private void frmNewFile_Load(object sender, EventArgs e)
        {
            loadLangList();
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
                createFile();
            }
        }

        /*------------------------------------------------------------------------------------------
        * Other Handlers
        -----------------------------------------------------------------------------------------*/
        private void tbName_MouseDown(object sender, MouseEventArgs e)
        {
            tbName.Text = "";
        }

        private void cbLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLanguages.SelectedIndex == cbLanguages.Items.Count - 1)
            {
                frmAddLanguage fal = new frmAddLanguage(this);
                fal.ShowDialog();
            }
        }

        /*------------------------------------------------------------------------------------------
         * Other Functions
         -----------------------------------------------------------------------------------------*/
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

        private void createFile()
        {
            String dirPath = Path.Combine(parentDir, cbLanguages.Text);
            String filePath = Path.Combine(dirPath, tbName.Text + ".txt");
            Directory.CreateDirectory(dirPath);
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
                Close();
            }
            else
            {
                if (MessageBox.Show("File already exists. Overwrite?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    File.Delete(filePath);
                    File.Create(filePath);
                    Close();
                }
                else
                {
                    tbName.Focus();
                }
            }
        }
    }
}
