/**********************************************************
* frmSnippet.cs
*
* This is the main application form where users can browse 
*   their saved snippets and copy/edit the code.
*
* Author: Taylor O'Dell
* Date Created: 11/22/17
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
    public partial class frmSnippet : Form
    {
        /*------------------------------------------------------------------------------------------
         * Form Initializers
         -----------------------------------------------------------------------------------------*/
        private static String rootDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Snippet");
        private frmSnippet fs;              // reference to pass to child form
        private bool busy = false;          // used in txtSnipBody_KeyPress
        private bool editOn = false;        // determines the functionality of btnEdit
        private bool fileChanged = false;   // lets us know if the currently displayed snippet has been changed

        public frmSnippet()
        {
            InitializeComponent();
            fs = this;
        }

        /*------------------------------------------------------------------------------------------
         * Form Handlers
         -----------------------------------------------------------------------------------------*/
        /* frmSnippet_Load
         * On form load, create the file structure if necessary
         *   and populate the TreeView
         */
        private void frmSnippet_Load(object sender, EventArgs e)
        {
            createNecessaryFiles();
            loadFileDisplay();
        }

        /* frmSnippet_FormClosing
         * When user attempts to close the form, if they've changed a snippet but
         *   not yet saved, they will be prompted to confirm exit or go back and save
         */
        private void frmSnippet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (fileChanged)
            {
                if (MessageBox.Show("Close without saving?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
        /*------------------------------------------------------------------------------------------
         * Button Handlers
         -----------------------------------------------------------------------------------------*/
        /* btnAdd_Click
         * Creates an instance of the New File form and displays it
         *   Used answer from Idle_Mind on this thread:
         *   https://stackoverflow.com/questions/27304874/creating-a-dark-background-when-a-new-form-appears
         *   to give form a dark, disabled look
         */
        private void btnNew_Click(object sender, EventArgs e)
        {
            // take a screenshot of the form and darken it:
            Bitmap bmp = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                G.CopyFromScreen(this.PointToScreen(new Point(0, 0)), new Point(0, 0), this.ClientRectangle.Size);
                double percent = 0.60;
                Color darken = Color.FromArgb((int)(255 * percent), Color.Black);
                using (Brush brsh = new SolidBrush(darken))
                {
                    G.FillRectangle(brsh, this.ClientRectangle);
                }
            }

            using (Panel p = new Panel())
            {
                p.Location = new Point(0, 0);
                p.Size = this.ClientRectangle.Size;
                p.BackgroundImage = bmp;
                this.Controls.Add(p);
                p.BringToFront();

                // display your dialog somehow:
                frmNewFile nf = new frmNewFile(fs);
                nf.ShowDialog();
            } // panel will be disposed and the form will "lighten" again...
        }

        /* btnEdit_Click
         * If editOn then the Edit button has been clicked and the
         *   current snippet displayed is editable. Upon pressing 
         *   the button, the button text, textbox color, and textbox
         *   functionality are changed and then the snippet is saved.
         * Else the current snippet displayed is not editable, and 
         *   upon button press we change button text, textbox color,
         *   and textbox functionality to make snippet editable
         */
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (editOn)
            {
                editOn = false;
                fileChanged = false;
                tbSnipBody.ReadOnly = true;
                tbSnipBody.BackColor = Color.Gainsboro;
                btnEdit.Text = "Edit";
                saveSnippet();
            }
            else
            {
                editOn = true;
                tbSnipBody.ReadOnly = false;
                tbSnipBody.BackColor = SystemColors.Window;
                tbSnipBody.Focus();
                btnEdit.Text = "Save";
            }            
        }

        /* btnCopy_Click
         * Copies the text of the currently displayed snippet to
         *   user's clipboard and uses a timer to change the button
         *   text to confirm a successful copy
         */
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (tbSnipBody.Text != null)
            {
                Clipboard.SetText(tbSnipBody.Text);
                btnCopy.Text = "Copied!";
                /* source for timer function: 
                 * https://stackoverflow.com/questions/15951689/show-label-text-as-warning-message-and-hide-it-after-a-few-seconds
                 */
                var t = new Timer
                {
                    Interval = 3000
                };
                t.Tick += (s, ev) =>
                {
                    btnCopy.Text = "Copy";
                    t.Stop();
                };
                t.Start();
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
        /* ntvFileDisplay_NodeMouseClick
         * If the click event is associated with a Node and that node has a parent node, then
         *   set the selected node to the current node
         *  Then check if there is a selected node, and if not disable the edit and Copy buttons
         */
        private void ntvFileDisplay_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null)
            {
                if (e.Node.Parent != null)
                {
                    ntvFileDisplay.SelectedNode = e.Node;
                    displaySnippet();
                }
            }
            if (ntvFileDisplay.SelectedNode == null)
            {
                btnEdit.Enabled = false;
                btnCopy.Enabled = false;
            }
            else
            {
                btnEdit.Enabled = true;
                btnCopy.Enabled = true;
            }
        }

        /* txtSnipBody_TextChanged
         * This function is a slightly modified one from the version given by Shimmy Weitzhandler here:
         *   https://social.msdn.microsoft.com/Forums/windows/en-US/cdd2dd8e-3ce9-46c7-85f5-6c99c25003ad/how-do-i-show-textboxscrollbars-only-when-needed?forum=winformsdesigner
         * The intention is to dynamically check and see if vertical, horizontal, or both scrollbars need
         *   to be added depending on the length of the text in the snippet
         * The top line of code was added by me to track whether or not a change had been made to the code yet
         */
        private void txtSnipBody_TextChanged(object sender, EventArgs e)
        {
            if (editOn) fileChanged = true;
            if (busy) return;
            busy = true;
            TextBox tb = sender as TextBox;
            Size tS = TextRenderer.MeasureText(tb.Text, tb.Font);
            bool Hsb = tb.ClientSize.Height < tS.Height + Convert.ToInt32(tb.Font.Size);
            bool Vsb = tb.ClientSize.Width < tS.Width;

            if (Hsb && Vsb)
                tb.ScrollBars = ScrollBars.Both;
            else if (!Hsb && !Vsb)
                tb.ScrollBars = ScrollBars.None;
            else if (Hsb && !Vsb)
                tb.ScrollBars = ScrollBars.Vertical;
            else if (!Hsb && Vsb)
                tb.ScrollBars = ScrollBars.Horizontal;

            sender = tb as object;
            busy = false;
        }

        /*------------------------------------------------------------------------------------------
         * Other Functions
         -----------------------------------------------------------------------------------------*/
        /* createNecessaryFiles
         * This function creates the root directory and initializes the file that stores
         *   the list of languages. If the directory or languages file already exist,
         *   this function does nothing.
         */
        private void createNecessaryFiles()
        {
            Directory.CreateDirectory(rootDir);
            List<String> initLanguages = new List<string>(new String[] { "Java", "C", "Python" });
            String langFilePath = Path.Combine(rootDir, ".languages.txt");
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

        /* loadFileDisplay
         * This function clears all nodes from the TreeView and repopulates
         *   it based on the folders/files in the root directory
         * This function is based on Alex Aza's response on this thread:
         *   https://stackoverflow.com/questions/6239544/populate-treeview-with-file-system-directory-structure
         */
        public void loadFileDisplay()
        {
            ntvFileDisplay.Nodes.Clear();
            // get folders from parentDir
            var parentDirInfo = new DirectoryInfo(rootDir);
            foreach (var dir in parentDirInfo.GetDirectories())
            {
                var dirInfo = new DirectoryInfo(Path.Combine(rootDir, dir.Name));
                ntvFileDisplay.Nodes.Add(CreateDirectoryNode(dirInfo));
            }
        }

        /* CreateDirectoryNode
         * This function implements additional functionality to make
         *   loadFileDisplay() work
         * This function is based on Alex Aza's response on this thread:
         *   https://stackoverflow.com/questions/6239544/populate-treeview-with-file-system-directory-structure
         */
        private TreeNode CreateDirectoryNode(DirectoryInfo dirInfo)
        {
            var dirNode = new TreeNode(dirInfo.Name);
            foreach (var dir in dirInfo.GetDirectories())
            {
                dirNode.Nodes.Add(CreateDirectoryNode(dir));
            }
            foreach (var file in dirInfo.GetFiles())
            {
                String name = file.Name;
                dirNode.Nodes.Add(new TreeNode(name.Substring(0, name.Length - 4))); // remove ".txt" of display
            }
            return dirNode;
        }

        /* displaySnippet
         * This function reads the file that holds the selected snippet
         *   and displays it in the large text box, along with displaying 
         *   the snippet name in the smallert text box
         */
        private void displaySnippet()
        {
            if (ntvFileDisplay.SelectedNode != null)
            {
                String parentDir = ntvFileDisplay.SelectedNode.Parent.Text;
                String fileName = ntvFileDisplay.SelectedNode.Text + ".txt";
                String filePath = Path.Combine(rootDir, parentDir, fileName);
                tbSnipName.Text = ntvFileDisplay.SelectedNode.Text;
                tbSnipBody.Text = File.ReadAllText(filePath);
            }
        }

        /* saveSnippet
         * This function writes the text from the text box to the snippet file
         *   Previous file is overwritten
         */
        private void saveSnippet()
        {
            // If user has the ability to edit the file, the SelectedNode should
            //   NEVER be null, but just in case...
            if (ntvFileDisplay.SelectedNode != null)
            {
                TreeNode node = ntvFileDisplay.SelectedNode;
                String filePath = Path.Combine(rootDir, node.Parent.Text.ToString(), node.Text.ToString() + ".txt");

                File.WriteAllText(filePath, tbSnipBody.Text.ToString());
            }
        }

        /* clearForm
         * Clears form so that no snippet is displayed
         *   Used by frmNewFile when a new snippet is added
         */
        public void clearForm()
        {
            ntvFileDisplay.SelectedNode = null;
            tbSnipName.Text = "";
            tbSnipBody.Text = "";
        }
    }
}
