/**********************************************************
* frmSnippet.cs
*
* This is the main application form where users can browse 
*   their saved snippets and copy/edit the code.
*
* Author: Taylor O'Dell
* Date Created: 11/22/17
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
    public partial class frmSnippet : Form
    {
        /*------------------------------------------------------------------------------------------
         * Form Initializers
         -----------------------------------------------------------------------------------------*/
        private static String parentDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Snippet");

        public frmSnippet()
        {
            InitializeComponent();
        }

        /*------------------------------------------------------------------------------------------
         * Form Handlers
         -----------------------------------------------------------------------------------------*/
        private void frmSnippet_Load(object sender, EventArgs e)
        {
            createNecessaryFiles();
        }

        /*------------------------------------------------------------------------------------------
         * Button Handlers
         -----------------------------------------------------------------------------------------*/
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNewFile nf = new frmNewFile();
            nf.ShowDialog();
        }

        /*------------------------------------------------------------------------------------------
         * Other Handlers
         -----------------------------------------------------------------------------------------*/

        /*------------------------------------------------------------------------------------------
         * Other Functions
         -----------------------------------------------------------------------------------------*/
        private void createNecessaryFiles()
        {
            Directory.CreateDirectory(parentDir);
            List<String> initLanguages = new List<string>(new String[] { "Java", "C", "Python" });
            String langFilePath = Path.Combine(parentDir, ".languages.txt");
            if (!File.Exists(langFilePath))
            {
                using (StreamWriter sw = File.AppendText(langFilePath))
                {
                    foreach (var name in initLanguages)
                    {
                        sw.WriteLine(name);
                    }
                }
            }
        }
    }
}
