namespace KSPModAdmin.Core.Views
{
    partial class frmAddMod
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddMod));
            this.btnAddAndClose = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblModName = new System.Windows.Forms.Label();
            this.tbModName = new System.Windows.Forms.TextBox();
            this.lblModPath = new System.Windows.Forms.Label();
            this.tbModPath = new System.Windows.Forms.TextBox();
            this.btnFolderSearch = new System.Windows.Forms.Button();
            this.ttAddMod = new System.Windows.Forms.ToolTip(this.components);
            this.cbInstallAfterAdd = new System.Windows.Forms.CheckBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddAndClose
            // 
            this.btnAddAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddAndClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAndClose.Image = global::KSPModAdmin.Core.Properties.Resources.component_add;
            this.btnAddAndClose.Location = new System.Drawing.Point(230, 203);
            this.btnAddAndClose.Name = "btnAddAndClose";
            this.btnAddAndClose.Size = new System.Drawing.Size(165, 25);
            this.btnAddAndClose.TabIndex = 8;
            this.btnAddAndClose.Text = "Add && Close";
            this.btnAddAndClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttAddMod.SetToolTip(this.btnAddAndClose, "Adds the mod to the ModSelection.");
            this.btnAddAndClose.UseVisualStyleBackColor = true;
            this.btnAddAndClose.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Image = global::KSPModAdmin.Core.Properties.Resources.delete2;
            this.btnClose.Location = new System.Drawing.Point(401, 203);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 25);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttAddMod.SetToolTip(this.btnClose, "Closes the dialog.");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblModName
            // 
            this.lblModName.AutoSize = true;
            this.lblModName.Location = new System.Drawing.Point(25, 21);
            this.lblModName.Name = "lblModName";
            this.lblModName.Size = new System.Drawing.Size(174, 13);
            this.lblModName.TabIndex = 0;
            this.lblModName.Text = "ModName (leave blank for auto fill):";
            // 
            // tbModName
            // 
            this.tbModName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbModName.Location = new System.Drawing.Point(28, 37);
            this.tbModName.Name = "tbModName";
            this.tbModName.Size = new System.Drawing.Size(467, 20);
            this.tbModName.TabIndex = 1;
            this.ttAddMod.SetToolTip(this.tbModName, "Enter a name for the mod or leave it blank for default name.");
            // 
            // lblModPath
            // 
            this.lblModPath.AutoSize = true;
            this.lblModPath.Location = new System.Drawing.Point(25, 69);
            this.lblModPath.Name = "lblModPath";
            this.lblModPath.Size = new System.Drawing.Size(157, 13);
            this.lblModPath.TabIndex = 2;
            this.lblModPath.Text = "Enter mod archive path or URL:";
            // 
            // tbModPath
            // 
            this.tbModPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbModPath.Location = new System.Drawing.Point(28, 85);
            this.tbModPath.Name = "tbModPath";
            this.tbModPath.Size = new System.Drawing.Size(436, 20);
            this.tbModPath.TabIndex = 3;
            this.ttAddMod.SetToolTip(this.tbModPath, "Enter a path to a mod archive, a craft or a URL to a mod.\r\n(URL with http:// or h" +
        "ttps://).");
            // 
            // btnFolderSearch
            // 
            this.btnFolderSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolderSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFolderSearch.Image = global::KSPModAdmin.Core.Properties.Resources.folder_view;
            this.btnFolderSearch.Location = new System.Drawing.Point(470, 83);
            this.btnFolderSearch.Name = "btnFolderSearch";
            this.btnFolderSearch.Size = new System.Drawing.Size(25, 24);
            this.btnFolderSearch.TabIndex = 4;
            this.btnFolderSearch.Tag = "\"Start\"";
            this.ttAddMod.SetToolTip(this.btnFolderSearch, "Opens a file browser to selecct a mod archive.");
            this.btnFolderSearch.UseVisualStyleBackColor = true;
            this.btnFolderSearch.Click += new System.EventHandler(this.btnFolderSearch_Click);
            // 
            // cbInstallAfterAdd
            // 
            this.cbInstallAfterAdd.AutoSize = true;
            this.cbInstallAfterAdd.Location = new System.Drawing.Point(28, 178);
            this.cbInstallAfterAdd.Name = "cbInstallAfterAdd";
            this.cbInstallAfterAdd.Size = new System.Drawing.Size(101, 17);
            this.cbInstallAfterAdd.TabIndex = 6;
            this.cbInstallAfterAdd.Text = "Install after add.";
            this.ttAddMod.SetToolTip(this.cbInstallAfterAdd, "Check to let KSP MA install the new mod right after adding.");
            this.cbInstallAfterAdd.UseVisualStyleBackColor = true;
            // 
            // lblNote
            // 
            this.lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNote.Location = new System.Drawing.Point(16, 118);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(495, 53);
            this.lblNote.TabIndex = 5;
            this.lblNote.Text = "NOTE: After KSP MA has added the mod you have to check / uncheck your wanted part" +
    "s of the mod and press \"Process All\" to install the mod.\r\nOr check the checkbox " +
    "below for immediate install of the mod.";
            // 
            // picLoading
            // 
            this.picLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picLoading.Image = ((System.Drawing.Image)(resources.GetObject("picLoading.Image")));
            this.picLoading.Location = new System.Drawing.Point(69, 206);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(18, 20);
            this.picLoading.TabIndex = 16;
            this.picLoading.TabStop = false;
            this.picLoading.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::KSPModAdmin.Core.Properties.Resources.component_add;
            this.btnAdd.Location = new System.Drawing.Point(98, 203);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(126, 25);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmAddMod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 238);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.cbInstallAfterAdd);
            this.Controls.Add(this.btnFolderSearch);
            this.Controls.Add(this.tbModPath);
            this.Controls.Add(this.tbModName);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.lblModPath);
            this.Controls.Add(this.lblModName);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnAddAndClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(539, 243);
            this.Name = "frmAddMod";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Adding mod";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAddMod_FormClosing);
            this.Load += new System.EventHandler(this.frmAddMod_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddAndClose;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblModName;
        private System.Windows.Forms.TextBox tbModName;
        private System.Windows.Forms.Label lblModPath;
        private System.Windows.Forms.TextBox tbModPath;
        private System.Windows.Forms.Button btnFolderSearch;
        private System.Windows.Forms.ToolTip ttAddMod;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.CheckBox cbInstallAfterAdd;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.Button btnAdd;
    }
}