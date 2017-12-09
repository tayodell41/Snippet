/**********************************************************
* NativeTreeView.cs
*
* The base of this class is to give our treeview the 
*   appearance similar to the Windows File Explorer.
*   This class comes from David Heffernen from this thread:
*   https://stackoverflow.com/questions/5131534/how-to-get-windows-native-look-for-the-net-treeview
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
    public class NativeTreeView : System.Windows.Forms.TreeView
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName,
                                                string pszSubIdList);

        protected override void CreateHandle()
        {
            base.CreateHandle();
            SetWindowTheme(this.Handle, "explorer", null);
        }

        /* The rest of the code in this class comes from Microsoft and allows a single click
         *   to show children or select a file rather than a double click. 
         *   source: https://msdn.microsoft.com/en-us/library/system.windows.forms.treeview.indent(v=vs.110).aspx 
         */
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            // Confirm that the user initiated the selection.
            // This prevents the first node from expanding when it is
            // automatically selected during the initialization of 
            // the TreeView control.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.IsExpanded)
                {
                    e.Node.Collapse();
                }
                else
                {
                    e.Node.Expand();
                }
            }

            // Remove the selection. This allows the same node to be
            // clicked twice in succession to toggle the expansion state.
            if (e.Node.Parent == null)
            {
                SelectedNode = null;
            }            
        }
    }
}
