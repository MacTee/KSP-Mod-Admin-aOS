namespace KSPModAdmin.Core.Views
{
    partial class frmImExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImExport));
            this.gbExport = new System.Windows.Forms.GroupBox();
            this.tvImportExportModSelection = new System.Windows.Forms.TreeView();
            this.pbExport = new System.Windows.Forms.PictureBox();
            this.cbIncludeMods = new System.Windows.Forms.CheckBox();
            this.rbExportSelectedOnly = new System.Windows.Forms.RadioButton();
            this.rbExportAll = new System.Windows.Forms.RadioButton();
            this.lblExportInfo = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.gbImport = new System.Windows.Forms.GroupBox();
            this.gbDestinationOption = new System.Windows.Forms.GroupBox();
            this.rbCopyDestination = new System.Windows.Forms.RadioButton();
            this.rbUseAutoDestDetection = new System.Windows.Forms.RadioButton();
            this.gbInstallOptions = new System.Windows.Forms.GroupBox();
            this.rbInstall = new System.Windows.Forms.RadioButton();
            this.rbAddOnly = new System.Windows.Forms.RadioButton();
            this.cbClearModSelection = new System.Windows.Forms.CheckBox();
            this.pbImport = new System.Windows.Forms.PictureBox();
            this.cbExtract = new System.Windows.Forms.CheckBox();
            this.lblCurrentAction = new System.Windows.Forms.Label();
            this.cbDownloadIfNeeded = new System.Windows.Forms.CheckBox();
            this.lblImportInfo = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.ttModImExport = new System.Windows.Forms.ToolTip(this.components);
            this.gbExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExport)).BeginInit();
            this.gbImport.SuspendLayout();
            this.gbDestinationOption.SuspendLayout();
            this.gbInstallOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImport)).BeginInit();
            this.SuspendLayout();
            // 
            // gbExport
            // 
            this.gbExport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbExport.Controls.Add(this.tvImportExportModSelection);
            this.gbExport.Controls.Add(this.pbExport);
            this.gbExport.Controls.Add(this.cbIncludeMods);
            this.gbExport.Controls.Add(this.rbExportSelectedOnly);
            this.gbExport.Controls.Add(this.rbExportAll);
            this.gbExport.Controls.Add(this.lblExportInfo);
            this.gbExport.Controls.Add(this.btnExport);
            this.gbExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbExport.Location = new System.Drawing.Point(7, 3);
            this.gbExport.Name = "gbExport";
            this.gbExport.Size = new System.Drawing.Size(449, 233);
            this.gbExport.TabIndex = 0;
            this.gbExport.TabStop = false;
            this.gbExport.Text = "Export:";
            // 
            // tvImportExportModSelection
            // 
            this.tvImportExportModSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvImportExportModSelection.CheckBoxes = true;
            this.tvImportExportModSelection.Enabled = false;
            this.tvImportExportModSelection.Location = new System.Drawing.Point(28, 93);
            this.tvImportExportModSelection.Name = "tvImportExportModSelection";
            this.tvImportExportModSelection.ShowLines = false;
            this.tvImportExportModSelection.Size = new System.Drawing.Size(311, 125);
            this.tvImportExportModSelection.TabIndex = 4;
            // 
            // pbExport
            // 
            this.pbExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbExport.Image = ((System.Drawing.Image)(resources.GetObject("pbExport.Image")));
            this.pbExport.Location = new System.Drawing.Point(414, 167);
            this.pbExport.Name = "pbExport";
            this.pbExport.Size = new System.Drawing.Size(20, 20);
            this.pbExport.TabIndex = 13;
            this.pbExport.TabStop = false;
            this.pbExport.Visible = false;
            // 
            // cbIncludeMods
            // 
            this.cbIncludeMods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbIncludeMods.AutoSize = true;
            this.cbIncludeMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIncludeMods.Location = new System.Drawing.Point(304, 21);
            this.cbIncludeMods.Name = "cbIncludeMods";
            this.cbIncludeMods.Size = new System.Drawing.Size(130, 17);
            this.cbIncludeMods.TabIndex = 1;
            this.cbIncludeMods.Text = "Include mod archives.";
            this.cbIncludeMods.UseVisualStyleBackColor = true;
            // 
            // rbExportSelectedOnly
            // 
            this.rbExportSelectedOnly.AutoSize = true;
            this.rbExportSelectedOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbExportSelectedOnly.Location = new System.Drawing.Point(28, 70);
            this.rbExportSelectedOnly.Name = "rbExportSelectedOnly";
            this.rbExportSelectedOnly.Size = new System.Drawing.Size(117, 17);
            this.rbExportSelectedOnly.TabIndex = 3;
            this.rbExportSelectedOnly.Text = "Selected mods only";
            this.rbExportSelectedOnly.UseVisualStyleBackColor = true;
            this.rbExportSelectedOnly.CheckedChanged += new System.EventHandler(this.rbExportSelectedOnly_CheckedChanged);
            // 
            // rbExportAll
            // 
            this.rbExportAll.AutoSize = true;
            this.rbExportAll.Checked = true;
            this.rbExportAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbExportAll.Location = new System.Drawing.Point(28, 47);
            this.rbExportAll.Name = "rbExportAll";
            this.rbExportAll.Size = new System.Drawing.Size(155, 17);
            this.rbExportAll.TabIndex = 2;
            this.rbExportAll.TabStop = true;
            this.rbExportAll.Text = "Complete ModSelection list.";
            this.rbExportAll.UseVisualStyleBackColor = true;
            // 
            // lblExportInfo
            // 
            this.lblExportInfo.AutoSize = true;
            this.lblExportInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExportInfo.Location = new System.Drawing.Point(13, 22);
            this.lblExportInfo.Name = "lblExportInfo";
            this.lblExportInfo.Size = new System.Drawing.Size(235, 13);
            this.lblExportInfo.TabIndex = 0;
            this.lblExportInfo.Text = "Export a ModPack for sharing with other players.";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Image = global::KSPModAdmin.Core.Properties.Resources.components_package_into;
            this.btnExport.Location = new System.Drawing.Point(345, 193);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(89, 25);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // gbImport
            // 
            this.gbImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbImport.Controls.Add(this.gbDestinationOption);
            this.gbImport.Controls.Add(this.gbInstallOptions);
            this.gbImport.Controls.Add(this.cbClearModSelection);
            this.gbImport.Controls.Add(this.pbImport);
            this.gbImport.Controls.Add(this.cbExtract);
            this.gbImport.Controls.Add(this.lblCurrentAction);
            this.gbImport.Controls.Add(this.cbDownloadIfNeeded);
            this.gbImport.Controls.Add(this.lblImportInfo);
            this.gbImport.Controls.Add(this.btnImport);
            this.gbImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbImport.Location = new System.Drawing.Point(7, 242);
            this.gbImport.Name = "gbImport";
            this.gbImport.Size = new System.Drawing.Size(449, 245);
            this.gbImport.TabIndex = 1;
            this.gbImport.TabStop = false;
            this.gbImport.Text = "Import:";
            // 
            // gbDestinationOption
            // 
            this.gbDestinationOption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDestinationOption.Controls.Add(this.rbCopyDestination);
            this.gbDestinationOption.Controls.Add(this.rbUseAutoDestDetection);
            this.gbDestinationOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDestinationOption.Location = new System.Drawing.Point(16, 95);
            this.gbDestinationOption.Name = "gbDestinationOption";
            this.gbDestinationOption.Size = new System.Drawing.Size(418, 52);
            this.gbDestinationOption.TabIndex = 4;
            this.gbDestinationOption.TabStop = false;
            this.gbDestinationOption.Text = "Destination options:";
            // 
            // rbCopyDestination
            // 
            this.rbCopyDestination.AutoSize = true;
            this.rbCopyDestination.Checked = true;
            this.rbCopyDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCopyDestination.Location = new System.Drawing.Point(27, 19);
            this.rbCopyDestination.Name = "rbCopyDestination";
            this.rbCopyDestination.Size = new System.Drawing.Size(106, 17);
            this.rbCopyDestination.TabIndex = 0;
            this.rbCopyDestination.TabStop = true;
            this.rbCopyDestination.Text = "Copy destination.";
            this.rbCopyDestination.UseVisualStyleBackColor = true;
            // 
            // rbUseAutoDestDetection
            // 
            this.rbUseAutoDestDetection.AutoSize = true;
            this.rbUseAutoDestDetection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbUseAutoDestDetection.Location = new System.Drawing.Point(191, 19);
            this.rbUseAutoDestDetection.Name = "rbUseAutoDestDetection";
            this.rbUseAutoDestDetection.Size = new System.Drawing.Size(176, 17);
            this.rbUseAutoDestDetection.TabIndex = 1;
            this.rbUseAutoDestDetection.Text = "User Auto destination detection.";
            this.rbUseAutoDestDetection.UseVisualStyleBackColor = true;
            // 
            // gbInstallOptions
            // 
            this.gbInstallOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInstallOptions.Controls.Add(this.rbInstall);
            this.gbInstallOptions.Controls.Add(this.rbAddOnly);
            this.gbInstallOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbInstallOptions.Location = new System.Drawing.Point(16, 153);
            this.gbInstallOptions.Name = "gbInstallOptions";
            this.gbInstallOptions.Size = new System.Drawing.Size(418, 52);
            this.gbInstallOptions.TabIndex = 5;
            this.gbInstallOptions.TabStop = false;
            this.gbInstallOptions.Text = "Install options:";
            // 
            // rbInstall
            // 
            this.rbInstall.AutoSize = true;
            this.rbInstall.Checked = true;
            this.rbInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbInstall.Location = new System.Drawing.Point(27, 19);
            this.rbInstall.Name = "rbInstall";
            this.rbInstall.Size = new System.Drawing.Size(83, 17);
            this.rbInstall.TabIndex = 0;
            this.rbInstall.TabStop = true;
            this.rbInstall.Text = "Install mods.";
            this.rbInstall.UseVisualStyleBackColor = true;
            // 
            // rbAddOnly
            // 
            this.rbAddOnly.AutoSize = true;
            this.rbAddOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAddOnly.Location = new System.Drawing.Point(191, 19);
            this.rbAddOnly.Name = "rbAddOnly";
            this.rbAddOnly.Size = new System.Drawing.Size(169, 17);
            this.rbAddOnly.TabIndex = 1;
            this.rbAddOnly.Text = "Add mods only (manual install).";
            this.rbAddOnly.UseVisualStyleBackColor = true;
            // 
            // cbClearModSelection
            // 
            this.cbClearModSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbClearModSelection.AutoSize = true;
            this.cbClearModSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbClearModSelection.Location = new System.Drawing.Point(255, 72);
            this.cbClearModSelection.Name = "cbClearModSelection";
            this.cbClearModSelection.Size = new System.Drawing.Size(179, 17);
            this.cbClearModSelection.TabIndex = 3;
            this.cbClearModSelection.Text = "Clear ModSelection befor import.";
            this.cbClearModSelection.UseVisualStyleBackColor = true;
            // 
            // pbImport
            // 
            this.pbImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbImport.Image = ((System.Drawing.Image)(resources.GetObject("pbImport.Image")));
            this.pbImport.Location = new System.Drawing.Point(319, 216);
            this.pbImport.Name = "pbImport";
            this.pbImport.Size = new System.Drawing.Size(20, 20);
            this.pbImport.TabIndex = 13;
            this.pbImport.TabStop = false;
            this.pbImport.Visible = false;
            // 
            // cbExtract
            // 
            this.cbExtract.AutoSize = true;
            this.cbExtract.Checked = true;
            this.cbExtract.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbExtract.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbExtract.Location = new System.Drawing.Point(28, 72);
            this.cbExtract.Name = "cbExtract";
            this.cbExtract.Size = new System.Drawing.Size(164, 17);
            this.cbExtract.TabIndex = 2;
            this.cbExtract.Text = "Extract mods (if in ModPack).";
            this.cbExtract.UseVisualStyleBackColor = true;
            this.cbExtract.CheckedChanged += new System.EventHandler(this.cbExtract_CheckedChanged);
            // 
            // lblCurrentAction
            // 
            this.lblCurrentAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentAction.Location = new System.Drawing.Point(13, 208);
            this.lblCurrentAction.Name = "lblCurrentAction";
            this.lblCurrentAction.Size = new System.Drawing.Size(314, 34);
            this.lblCurrentAction.TabIndex = 6;
            this.lblCurrentAction.Text = "Current Action...";
            // 
            // cbDownloadIfNeeded
            // 
            this.cbDownloadIfNeeded.AutoSize = true;
            this.cbDownloadIfNeeded.Checked = true;
            this.cbDownloadIfNeeded.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDownloadIfNeeded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDownloadIfNeeded.Location = new System.Drawing.Point(28, 49);
            this.cbDownloadIfNeeded.Name = "cbDownloadIfNeeded";
            this.cbDownloadIfNeeded.Size = new System.Drawing.Size(199, 17);
            this.cbDownloadIfNeeded.TabIndex = 1;
            this.cbDownloadIfNeeded.Text = "Download needed mods (if possible).";
            this.cbDownloadIfNeeded.UseVisualStyleBackColor = true;
            this.cbDownloadIfNeeded.CheckedChanged += new System.EventHandler(this.cbDownloadIfNeeded_CheckedChanged);
            // 
            // lblImportInfo
            // 
            this.lblImportInfo.AutoSize = true;
            this.lblImportInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportInfo.Location = new System.Drawing.Point(13, 23);
            this.lblImportInfo.Name = "lblImportInfo";
            this.lblImportInfo.Size = new System.Drawing.Size(164, 13);
            this.lblImportInfo.TabIndex = 0;
            this.lblImportInfo.Text = "Import mods from a ModPack file.";
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Image = global::KSPModAdmin.Core.Properties.Resources.components_package_out;
            this.btnImport.Location = new System.Drawing.Point(345, 213);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(89, 25);
            this.btnImport.TabIndex = 7;
            this.btnImport.Text = "Import";
            this.btnImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // frmImExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 493);
            this.Controls.Add(this.gbImport);
            this.Controls.Add(this.gbExport);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 330);
            this.Name = "frmImExport";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ModPack im/exporter";
            this.Load += new System.EventHandler(this.frmImExport_Load);
            this.gbExport.ResumeLayout(false);
            this.gbExport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExport)).EndInit();
            this.gbImport.ResumeLayout(false);
            this.gbImport.PerformLayout();
            this.gbDestinationOption.ResumeLayout(false);
            this.gbDestinationOption.PerformLayout();
            this.gbInstallOptions.ResumeLayout(false);
            this.gbInstallOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.GroupBox gbExport;
        private System.Windows.Forms.GroupBox gbImport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label lblExportInfo;
        private System.Windows.Forms.Label lblImportInfo;
        private System.Windows.Forms.RadioButton rbExportSelectedOnly;
        private System.Windows.Forms.RadioButton rbExportAll;
        private System.Windows.Forms.CheckBox cbExtract;
        private System.Windows.Forms.CheckBox cbDownloadIfNeeded;
        private System.Windows.Forms.CheckBox cbIncludeMods;
        private System.Windows.Forms.PictureBox pbExport;
        private System.Windows.Forms.PictureBox pbImport;
        private System.Windows.Forms.RadioButton rbAddOnly;
        private System.Windows.Forms.RadioButton rbInstall;
        private System.Windows.Forms.GroupBox gbDestinationOption;
        private System.Windows.Forms.RadioButton rbCopyDestination;
        private System.Windows.Forms.RadioButton rbUseAutoDestDetection;
        private System.Windows.Forms.GroupBox gbInstallOptions;
        private System.Windows.Forms.CheckBox cbClearModSelection;
        private System.Windows.Forms.ToolTip ttModImExport;
        private System.Windows.Forms.Label lblCurrentAction;
        private System.Windows.Forms.TreeView tvImportExportModSelection;
    }
}