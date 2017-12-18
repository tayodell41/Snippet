namespace Snippet
{
    partial class frmSnippet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSnippet));
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tbSnipName = new Snippet.ReadOnlyTextBox();
            this.tbSnipBody = new Snippet.ReadOnlyTextBox();
            this.ntvFileDisplay = new Snippet.NativeTreeView();
            this.SuspendLayout();
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuit.Location = new System.Drawing.Point(922, 501);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(87, 25);
            this.btnQuit.TabIndex = 3;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(817, 501);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(87, 25);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Enabled = false;
            this.btnCopy.Location = new System.Drawing.Point(712, 501);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(87, 25);
            this.btnCopy.TabIndex = 1;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNew.Location = new System.Drawing.Point(233, 501);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(87, 25);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New...";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(335, 501);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(87, 25);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tbSnipName
            // 
            this.tbSnipName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSnipName.BackColor = System.Drawing.Color.Gainsboro;
            this.tbSnipName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbSnipName.Font = new System.Drawing.Font("Consolas", 9.25F);
            this.tbSnipName.Location = new System.Drawing.Point(233, 16);
            this.tbSnipName.MinimumSize = new System.Drawing.Size(402, 22);
            this.tbSnipName.Name = "tbSnipName";
            this.tbSnipName.ReadOnly = true;
            this.tbSnipName.Size = new System.Drawing.Size(408, 22);
            this.tbSnipName.TabIndex = 0;
            this.tbSnipName.TabSize = 4;
            this.tbSnipName.TabStop = false;
            // 
            // tbSnipBody
            // 
            this.tbSnipBody.AcceptsTab = true;
            this.tbSnipBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSnipBody.BackColor = System.Drawing.Color.Gainsboro;
            this.tbSnipBody.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbSnipBody.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSnipBody.Location = new System.Drawing.Point(233, 57);
            this.tbSnipBody.Multiline = true;
            this.tbSnipBody.Name = "tbSnipBody";
            this.tbSnipBody.ReadOnly = true;
            this.tbSnipBody.Size = new System.Drawing.Size(775, 427);
            this.tbSnipBody.TabIndex = 4;
            this.tbSnipBody.TabSize = 4;
            this.tbSnipBody.WordWrap = false;
            this.tbSnipBody.TextChanged += new System.EventHandler(this.txtSnipBody_TextChanged);
            // 
            // ntvFileDisplay
            // 
            this.ntvFileDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ntvFileDisplay.BackColor = System.Drawing.Color.Gainsboro;
            this.ntvFileDisplay.Indent = 19;
            this.ntvFileDisplay.Location = new System.Drawing.Point(17, 16);
            this.ntvFileDisplay.Name = "ntvFileDisplay";
            this.ntvFileDisplay.ShowLines = false;
            this.ntvFileDisplay.Size = new System.Drawing.Size(198, 509);
            this.ntvFileDisplay.TabIndex = 0;
            this.ntvFileDisplay.TabStop = false;
            this.ntvFileDisplay.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ntvFileDisplay_NodeMouseClick);
            // 
            // frmSnippet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnQuit;
            this.ClientSize = new System.Drawing.Size(1031, 540);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.tbSnipName);
            this.Controls.Add(this.tbSnipBody);
            this.Controls.Add(this.ntvFileDisplay);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnQuit);
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(774, 320);
            this.Name = "frmSnippet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Snippet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSnippet_FormClosing);
            this.Load += new System.EventHandler(this.frmSnippet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnNew;
        private NativeTreeView ntvFileDisplay;
        private ReadOnlyTextBox tbSnipBody;
        private ReadOnlyTextBox tbSnipName;
        private System.Windows.Forms.Button btnDelete;
    }
}

