namespace KSPModAdmin.Plugin.BackupTab.Views
{
    partial class frmEditNote
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
            this.lblBackupName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblBackupNote = new System.Windows.Forms.Label();
            this.tbNote = new System.Windows.Forms.TextBox();
            this.btnChangeBackupNote = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblBackupName
            // 
            this.lblBackupName.AutoSize = true;
            this.lblBackupName.Location = new System.Drawing.Point(12, 15);
            this.lblBackupName.Name = "lblBackupName";
            this.lblBackupName.Size = new System.Drawing.Size(76, 13);
            this.lblBackupName.TabIndex = 0;
            this.lblBackupName.Text = "Backup name:";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Enabled = false;
            this.tbName.Location = new System.Drawing.Point(94, 12);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(363, 20);
            this.tbName.TabIndex = 1;
            this.tbName.TabStop = false;
            // 
            // lblBackupNote
            // 
            this.lblBackupNote.AutoSize = true;
            this.lblBackupNote.Location = new System.Drawing.Point(55, 41);
            this.lblBackupNote.Name = "lblBackupNote";
            this.lblBackupNote.Size = new System.Drawing.Size(33, 13);
            this.lblBackupNote.TabIndex = 2;
            this.lblBackupNote.Text = "Note:";
            // 
            // tbNote
            // 
            this.tbNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNote.Location = new System.Drawing.Point(94, 38);
            this.tbNote.Name = "tbNote";
            this.tbNote.Size = new System.Drawing.Size(363, 20);
            this.tbNote.TabIndex = 3;
            // 
            // btnChangeBackupNote
            // 
            this.btnChangeBackupNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeBackupNote.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnChangeBackupNote.Image = global::KSPModAdmin.Plugin.BackupTab.Properties.Resources.note_floppy_disk_16x16;
            this.btnChangeBackupNote.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnChangeBackupNote.Location = new System.Drawing.Point(262, 64);
            this.btnChangeBackupNote.Name = "btnChangeBackupNote";
            this.btnChangeBackupNote.Size = new System.Drawing.Size(93, 23);
            this.btnChangeBackupNote.TabIndex = 4;
            this.btnChangeBackupNote.Text = "Save";
            this.btnChangeBackupNote.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnChangeBackupNote.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::KSPModAdmin.Plugin.BackupTab.Properties.Resources.delete2;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(361, 64);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmEditNote
            // 
            this.AcceptButton = this.btnChangeBackupNote;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(469, 95);
            this.Controls.Add(this.btnChangeBackupNote);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbNote);
            this.Controls.Add(this.lblBackupNote);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lblBackupName);
            this.Name = "frmEditNote";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit note";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBackupName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblBackupNote;
        private System.Windows.Forms.TextBox tbNote;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnChangeBackupNote;
    }
}