namespace KSPModAdmin.Plugin.BackupTab
{
    partial class UcBackupView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ttPlugin = new System.Windows.Forms.ToolTip(this.components);
            this.tbBackupPath = new System.Windows.Forms.TextBox();
            this.tsBackups = new System.Windows.Forms.ToolStrip();
            this.tsbNewBackup = new System.Windows.Forms.ToolStripButton();
            this.tsbBackupSaves = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRemoveBackup = new System.Windows.Forms.ToolStripButton();
            this.tsbRemoveAllBackups = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tslProcessing = new System.Windows.Forms.ToolStripLabel();
            this.grbBackups = new System.Windows.Forms.GroupBox();
            this.tvBackups = new KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.TreeViewAdv();
            this.btnRecoverBackup = new System.Windows.Forms.Button();
            this.btnOpenBackupDir = new System.Windows.Forms.Button();
            this.btnBackupPath = new System.Windows.Forms.Button();
            this.tsBackups.SuspendLayout();
            this.grbBackups.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbBackupPath
            // 
            this.tbBackupPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBackupPath.Location = new System.Drawing.Point(6, 12);
            this.tbBackupPath.Name = "tbBackupPath";
            this.tbBackupPath.ReadOnly = true;
            this.tbBackupPath.Size = new System.Drawing.Size(603, 20);
            this.tbBackupPath.TabIndex = 27;
            this.tbBackupPath.TabStop = false;
            // 
            // tsBackups
            // 
            this.tsBackups.AutoSize = false;
            this.tsBackups.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewBackup,
            this.tsbBackupSaves,
            this.toolStripSeparator1,
            this.tsbRemoveBackup,
            this.tsbRemoveAllBackups,
            this.toolStripSeparator2,
            this.tslProcessing});
            this.tsBackups.Location = new System.Drawing.Point(3, 16);
            this.tsBackups.Name = "tsBackups";
            this.tsBackups.Size = new System.Drawing.Size(669, 25);
            this.tsBackups.TabIndex = 37;
            this.tsBackups.Text = "toolStrip2";
            // 
            // tsbNewBackup
            // 
            this.tsbNewBackup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNewBackup.Image = global::KSPModAdmin.Plugin.BackupTab.Properties.Resources.data_add;
            this.tsbNewBackup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewBackup.Name = "tsbNewBackup";
            this.tsbNewBackup.Size = new System.Drawing.Size(23, 22);
            this.tsbNewBackup.Text = "toolStripButton3";
            this.tsbNewBackup.Click += new System.EventHandler(this.tsbNewBackup_Click);
            // 
            // tsbBackupSaves
            // 
            this.tsbBackupSaves.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBackupSaves.Image = global::KSPModAdmin.Plugin.BackupTab.Properties.Resources.data_floppy_disk;
            this.tsbBackupSaves.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBackupSaves.Name = "tsbBackupSaves";
            this.tsbBackupSaves.Size = new System.Drawing.Size(23, 22);
            this.tsbBackupSaves.Text = "toolStripButton4";
            this.tsbBackupSaves.Click += new System.EventHandler(this.tsbBackupSaves_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRemoveBackup
            // 
            this.tsbRemoveBackup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRemoveBackup.Image = global::KSPModAdmin.Plugin.BackupTab.Properties.Resources.data_delete;
            this.tsbRemoveBackup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemoveBackup.Name = "tsbRemoveBackup";
            this.tsbRemoveBackup.Size = new System.Drawing.Size(23, 22);
            this.tsbRemoveBackup.Text = "toolStripButton5";
            this.tsbRemoveBackup.Click += new System.EventHandler(this.tsbRemoveBackup_Click);
            // 
            // tsbRemoveAllBackups
            // 
            this.tsbRemoveAllBackups.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRemoveAllBackups.Image = global::KSPModAdmin.Plugin.BackupTab.Properties.Resources.data_copy_delete;
            this.tsbRemoveAllBackups.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemoveAllBackups.Name = "tsbRemoveAllBackups";
            this.tsbRemoveAllBackups.Size = new System.Drawing.Size(23, 22);
            this.tsbRemoveAllBackups.Text = "toolStripButton6";
            this.tsbRemoveAllBackups.Click += new System.EventHandler(this.tsbRemoveAllBackups_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tslProcessing
            // 
            this.tslProcessing.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslProcessing.Image = global::KSPModAdmin.Plugin.BackupTab.Properties.Resources.loader;
            this.tslProcessing.Name = "tslProcessing";
            this.tslProcessing.Size = new System.Drawing.Size(16, 22);
            this.tslProcessing.Visible = false;
            // 
            // grbBackups
            // 
            this.grbBackups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbBackups.Controls.Add(this.tvBackups);
            this.grbBackups.Controls.Add(this.btnRecoverBackup);
            this.grbBackups.Controls.Add(this.tsBackups);
            this.grbBackups.Location = new System.Drawing.Point(0, 39);
            this.grbBackups.Name = "grbBackups";
            this.grbBackups.Size = new System.Drawing.Size(675, 468);
            this.grbBackups.TabIndex = 38;
            this.grbBackups.TabStop = false;
            this.grbBackups.Text = "Backups:";
            // 
            // tvBackups
            // 
            this.tvBackups.BackColor = System.Drawing.SystemColors.Window;
            this.tvBackups.DefaultToolTipProvider = null;
            this.tvBackups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvBackups.DragDropMarkColor = System.Drawing.Color.Black;
            this.tvBackups.LineColor = System.Drawing.SystemColors.ControlDark;
            this.tvBackups.Location = new System.Drawing.Point(3, 41);
            this.tvBackups.Model = null;
            this.tvBackups.Name = "tvBackups";
            this.tvBackups.SelectedNode = null;
            this.tvBackups.ShowLines = false;
            this.tvBackups.ShowPlusMinus = false;
            this.tvBackups.Size = new System.Drawing.Size(669, 384);
            this.tvBackups.TabIndex = 38;
            this.tvBackups.Text = "treeViewAdv1";
            this.tvBackups.UseColumns = true;
            this.tvBackups.SelectionChanged += new System.EventHandler(this.tvBackups_SelectionChanged);
            // 
            // btnRecoverBackup
            // 
            this.btnRecoverBackup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRecoverBackup.Enabled = false;
            this.btnRecoverBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecoverBackup.Image = global::KSPModAdmin.Plugin.BackupTab.Properties.Resources.disk_black_data_into;
            this.btnRecoverBackup.Location = new System.Drawing.Point(3, 425);
            this.btnRecoverBackup.Name = "btnRecoverBackup";
            this.btnRecoverBackup.Size = new System.Drawing.Size(669, 40);
            this.btnRecoverBackup.TabIndex = 34;
            this.btnRecoverBackup.Text = " Recover selected Backup";
            this.btnRecoverBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRecoverBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRecoverBackup.UseVisualStyleBackColor = true;
            this.btnRecoverBackup.Click += new System.EventHandler(this.btnRecoverBackup_Click);
            // 
            // btnOpenBackupDir
            // 
            this.btnOpenBackupDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenBackupDir.Image = global::KSPModAdmin.Plugin.BackupTab.Properties.Resources.folder1;
            this.btnOpenBackupDir.Location = new System.Drawing.Point(615, 10);
            this.btnOpenBackupDir.Name = "btnOpenBackupDir";
            this.btnOpenBackupDir.Size = new System.Drawing.Size(25, 24);
            this.btnOpenBackupDir.TabIndex = 35;
            this.btnOpenBackupDir.UseVisualStyleBackColor = true;
            this.btnOpenBackupDir.Click += new System.EventHandler(this.btnOpenBackupDir_Click);
            // 
            // btnBackupPath
            // 
            this.btnBackupPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackupPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackupPath.Image = global::KSPModAdmin.Plugin.BackupTab.Properties.Resources.folder_view;
            this.btnBackupPath.Location = new System.Drawing.Point(646, 9);
            this.btnBackupPath.Name = "btnBackupPath";
            this.btnBackupPath.Size = new System.Drawing.Size(25, 24);
            this.btnBackupPath.TabIndex = 29;
            this.btnBackupPath.UseVisualStyleBackColor = true;
            this.btnBackupPath.Click += new System.EventHandler(this.btnBackupPath_Click);
            // 
            // UcBackupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbBackups);
            this.Controls.Add(this.btnOpenBackupDir);
            this.Controls.Add(this.btnBackupPath);
            this.Controls.Add(this.tbBackupPath);
            this.MinimumSize = new System.Drawing.Size(450, 400);
            this.Name = "UcBackupView";
            this.Size = new System.Drawing.Size(675, 507);
            this.Load += new System.EventHandler(this.ucPluginView_Load);
            this.tsBackups.ResumeLayout(false);
            this.tsBackups.PerformLayout();
            this.grbBackups.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttPlugin;
        private System.Windows.Forms.Button btnOpenBackupDir;
        private System.Windows.Forms.Button btnRecoverBackup;
        public System.Windows.Forms.Button btnBackupPath;
        public System.Windows.Forms.TextBox tbBackupPath;
        private System.Windows.Forms.ToolStrip tsBackups;
        private System.Windows.Forms.ToolStripButton tsbNewBackup;
        private System.Windows.Forms.ToolStripButton tsbBackupSaves;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbRemoveBackup;
        private System.Windows.Forms.ToolStripButton tsbRemoveAllBackups;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel tslProcessing;
        private System.Windows.Forms.GroupBox grbBackups;
        private Core.Utils.Controls.Aga.Controls.Tree.TreeViewAdv tvBackups;
    }
}
