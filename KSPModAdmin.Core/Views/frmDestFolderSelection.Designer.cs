namespace KSPModAdmin.Core.Views
{
    partial class frmDestFolderSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDestFolderSelection));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblSelectedSource = new System.Windows.Forms.Label();
            this.CB_Source = new System.Windows.Forms.ComboBox();
            this.lblSelectDestination = new System.Windows.Forms.Label();
            this.cbDestination = new System.Windows.Forms.ComboBox();
            this.lblSelectDestinationNote = new System.Windows.Forms.Label();
            this.rbCopyContentToDestination = new System.Windows.Forms.RadioButton();
            this.rbCopySourceToDestination = new System.Windows.Forms.RadioButton();
            this.cbListFoldersOnly = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Image = global::KSPModAdmin.Core.Properties.Resources.component_into;
            this.btnOK.Location = new System.Drawing.Point(238, 173);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 25);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::KSPModAdmin.Core.Properties.Resources.delete2;
            this.btnCancel.Location = new System.Drawing.Point(354, 173);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 25);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblSelectedSource
            // 
            this.lblSelectedSource.AutoSize = true;
            this.lblSelectedSource.Location = new System.Drawing.Point(3, 3);
            this.lblSelectedSource.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblSelectedSource.Name = "lblSelectedSource";
            this.lblSelectedSource.Size = new System.Drawing.Size(93, 13);
            this.lblSelectedSource.TabIndex = 0;
            this.lblSelectedSource.Text = "Select the source:";
            // 
            // CB_Source
            // 
            this.CB_Source.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CB_Source.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Source.FormattingEnabled = true;
            this.CB_Source.Location = new System.Drawing.Point(23, 25);
            this.CB_Source.Name = "CB_Source";
            this.CB_Source.Size = new System.Drawing.Size(428, 21);
            this.CB_Source.TabIndex = 1;
            // 
            // lblSelectDestination
            // 
            this.lblSelectDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectDestination.AutoSize = true;
            this.lblSelectDestination.Location = new System.Drawing.Point(12, 104);
            this.lblSelectDestination.Name = "lblSelectDestination";
            this.lblSelectDestination.Size = new System.Drawing.Size(112, 13);
            this.lblSelectDestination.TabIndex = 4;
            this.lblSelectDestination.Text = "Select the destination:";
            // 
            // cbDestination
            // 
            this.cbDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDestination.FormattingEnabled = true;
            this.cbDestination.Location = new System.Drawing.Point(23, 120);
            this.cbDestination.Name = "cbDestination";
            this.cbDestination.Size = new System.Drawing.Size(428, 21);
            this.cbDestination.TabIndex = 5;
            this.cbDestination.SelectedIndexChanged += new System.EventHandler(this.CB_Dest_SelectedIndexChanged);
            // 
            // lblSelectDestinationNote
            // 
            this.lblSelectDestinationNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectDestinationNote.AutoSize = true;
            this.lblSelectDestinationNote.Location = new System.Drawing.Point(7, 155);
            this.lblSelectDestinationNote.Name = "lblSelectDestinationNote";
            this.lblSelectDestinationNote.Size = new System.Drawing.Size(460, 13);
            this.lblSelectDestinationNote.TabIndex = 6;
            this.lblSelectDestinationNote.Text = "Note: If you choose a folder as source the destination will be set to all its sub" +
    "folders and files too.";
            // 
            // rbCopyContentToDestination
            // 
            this.rbCopyContentToDestination.AutoSize = true;
            this.rbCopyContentToDestination.Location = new System.Drawing.Point(39, 75);
            this.rbCopyContentToDestination.Name = "rbCopyContentToDestination";
            this.rbCopyContentToDestination.Size = new System.Drawing.Size(204, 17);
            this.rbCopyContentToDestination.TabIndex = 3;
            this.rbCopyContentToDestination.Text = "Copy content of source to destination.";
            this.rbCopyContentToDestination.UseVisualStyleBackColor = true;
            // 
            // rbCopySourceToDestination
            // 
            this.rbCopySourceToDestination.AutoSize = true;
            this.rbCopySourceToDestination.Checked = true;
            this.rbCopySourceToDestination.Location = new System.Drawing.Point(39, 52);
            this.rbCopySourceToDestination.Name = "rbCopySourceToDestination";
            this.rbCopySourceToDestination.Size = new System.Drawing.Size(196, 17);
            this.rbCopySourceToDestination.TabIndex = 2;
            this.rbCopySourceToDestination.TabStop = true;
            this.rbCopySourceToDestination.Text = "Copy selected source to destination.";
            this.rbCopySourceToDestination.UseVisualStyleBackColor = true;
            // 
            // cbListFoldersOnly
            // 
            this.cbListFoldersOnly.AutoSize = true;
            this.cbListFoldersOnly.Checked = true;
            this.cbListFoldersOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbListFoldersOnly.Location = new System.Drawing.Point(102, 3);
            this.cbListFoldersOnly.Name = "cbListFoldersOnly";
            this.cbListFoldersOnly.Size = new System.Drawing.Size(98, 17);
            this.cbListFoldersOnly.TabIndex = 1;
            this.cbListFoldersOnly.Text = "List folders only";
            this.cbListFoldersOnly.UseVisualStyleBackColor = true;
            this.cbListFoldersOnly.CheckedChanged += new System.EventHandler(this.CB_ListFoldersOnly_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblSelectedSource, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbListFoldersOnly, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(441, 24);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // frmDestFolderSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 206);
            this.Controls.Add(this.CB_Source);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.rbCopySourceToDestination);
            this.Controls.Add(this.rbCopyContentToDestination);
            this.Controls.Add(this.cbDestination);
            this.Controls.Add(this.lblSelectDestination);
            this.Controls.Add(this.lblSelectDestinationNote);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDestFolderSelection";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Install folder selection";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblSelectedSource;
        private System.Windows.Forms.ComboBox CB_Source;
        private System.Windows.Forms.Label lblSelectDestination;
        private System.Windows.Forms.ComboBox cbDestination;
        private System.Windows.Forms.Label lblSelectDestinationNote;
        private System.Windows.Forms.RadioButton rbCopyContentToDestination;
        private System.Windows.Forms.RadioButton rbCopySourceToDestination;
        private System.Windows.Forms.CheckBox cbListFoldersOnly;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}