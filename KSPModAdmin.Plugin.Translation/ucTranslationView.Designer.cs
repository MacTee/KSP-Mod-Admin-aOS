namespace KSPModAdmin.Plugin.Translation
{
    partial class ucTranslationView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTranslationView));
            this.pbTransToolExport = new System.Windows.Forms.PictureBox();
            this.dgvTransToolLanguageFileEntries = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.languageEntryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gbTransToolLanguageFile = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tbTransToolLanguageShortName = new System.Windows.Forms.TextBox();
            this.lblTransToolLanguageShortName = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tbTransToolLanguageName = new System.Windows.Forms.TextBox();
            this.lblTransToolLanguageName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbTransToolFileName = new System.Windows.Forms.TextBox();
            this.lblTransToolFileName = new System.Windows.Forms.Label();
            this.btnTransToolSave = new System.Windows.Forms.Button();
            this.cbTransToolLanguages = new System.Windows.Forms.ComboBox();
            this.lblTransToolLanguages = new System.Windows.Forms.Label();
            this.gbTransToolLanguageFileEntries = new System.Windows.Forms.GroupBox();
            this.ttTransTool = new System.Windows.Forms.ToolTip(this.components);
            this.cbTransToolEdit = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pbTransToolExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransToolLanguageFileEntries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.languageEntryBindingSource)).BeginInit();
            this.gbTransToolLanguageFile.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbTransToolLanguageFileEntries.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbTransToolExport
            // 
            this.pbTransToolExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTransToolExport.Image = ((System.Drawing.Image)(resources.GetObject("pbTransToolExport.Image")));
            this.pbTransToolExport.Location = new System.Drawing.Point(-299, 8);
            this.pbTransToolExport.Name = "pbTransToolExport";
            this.pbTransToolExport.Size = new System.Drawing.Size(20, 20);
            this.pbTransToolExport.TabIndex = 24;
            this.pbTransToolExport.TabStop = false;
            this.pbTransToolExport.Visible = false;
            // 
            // dgvTransToolLanguageFileEntries
            // 
            this.dgvTransToolLanguageFileEntries.AllowUserToAddRows = false;
            this.dgvTransToolLanguageFileEntries.AllowUserToDeleteRows = false;
            this.dgvTransToolLanguageFileEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTransToolLanguageFileEntries.AutoGenerateColumns = false;
            this.dgvTransToolLanguageFileEntries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTransToolLanguageFileEntries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransToolLanguageFileEntries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn});
            this.dgvTransToolLanguageFileEntries.DataSource = this.languageEntryBindingSource;
            this.dgvTransToolLanguageFileEntries.Location = new System.Drawing.Point(6, 19);
            this.dgvTransToolLanguageFileEntries.Name = "dgvTransToolLanguageFileEntries";
            this.dgvTransToolLanguageFileEntries.RowHeadersVisible = false;
            this.dgvTransToolLanguageFileEntries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvTransToolLanguageFileEntries.Size = new System.Drawing.Size(489, 223);
            this.dgvTransToolLanguageFileEntries.TabIndex = 0;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 60;
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            this.valueDataGridViewTextBoxColumn.Width = 59;
            // 
            // languageEntryBindingSource
            // 
            this.languageEntryBindingSource.DataSource = typeof(KSPModAdmin.Plugin.Translation.LanguageEntry);
            // 
            // gbTransToolLanguageFile
            // 
            this.gbTransToolLanguageFile.Controls.Add(this.tableLayoutPanel3);
            this.gbTransToolLanguageFile.Controls.Add(this.tableLayoutPanel2);
            this.gbTransToolLanguageFile.Controls.Add(this.tableLayoutPanel1);
            this.gbTransToolLanguageFile.Enabled = false;
            this.gbTransToolLanguageFile.Location = new System.Drawing.Point(6, 36);
            this.gbTransToolLanguageFile.Name = "gbTransToolLanguageFile";
            this.gbTransToolLanguageFile.Size = new System.Drawing.Size(501, 104);
            this.gbTransToolLanguageFile.TabIndex = 3;
            this.gbTransToolLanguageFile.TabStop = false;
            this.gbTransToolLanguageFile.Text = "Language file:";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tbTransToolLanguageShortName, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblTransToolLanguageShortName, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(6, 70);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(489, 26);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // tbTransToolLanguageShortName
            // 
            this.tbTransToolLanguageShortName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTransToolLanguageShortName.Location = new System.Drawing.Point(132, 3);
            this.tbTransToolLanguageShortName.Name = "tbTransToolLanguageShortName";
            this.tbTransToolLanguageShortName.Size = new System.Drawing.Size(354, 20);
            this.tbTransToolLanguageShortName.TabIndex = 1;
            // 
            // lblTransToolLanguageShortName
            // 
            this.lblTransToolLanguageShortName.AutoSize = true;
            this.lblTransToolLanguageShortName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTransToolLanguageShortName.Location = new System.Drawing.Point(3, 0);
            this.lblTransToolLanguageShortName.Name = "lblTransToolLanguageShortName";
            this.lblTransToolLanguageShortName.Size = new System.Drawing.Size(123, 26);
            this.lblTransToolLanguageShortName.TabIndex = 0;
            this.lblTransToolLanguageShortName.Text = "Short lanugage name:";
            this.lblTransToolLanguageShortName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tbTransToolLanguageName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTransToolLanguageName, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 42);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(489, 27);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tbTransToolLanguageName
            // 
            this.tbTransToolLanguageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTransToolLanguageName.Location = new System.Drawing.Point(132, 3);
            this.tbTransToolLanguageName.Name = "tbTransToolLanguageName";
            this.tbTransToolLanguageName.Size = new System.Drawing.Size(354, 20);
            this.tbTransToolLanguageName.TabIndex = 1;
            // 
            // lblTransToolLanguageName
            // 
            this.lblTransToolLanguageName.AutoSize = true;
            this.lblTransToolLanguageName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTransToolLanguageName.Location = new System.Drawing.Point(3, 0);
            this.lblTransToolLanguageName.Name = "lblTransToolLanguageName";
            this.lblTransToolLanguageName.Size = new System.Drawing.Size(123, 27);
            this.lblTransToolLanguageName.TabIndex = 0;
            this.lblTransToolLanguageName.Text = "Lanugage name:";
            this.lblTransToolLanguageName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tbTransToolFileName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTransToolFileName, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 14);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(489, 27);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tbTransToolFileName
            // 
            this.tbTransToolFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTransToolFileName.Location = new System.Drawing.Point(132, 3);
            this.tbTransToolFileName.Name = "tbTransToolFileName";
            this.tbTransToolFileName.Size = new System.Drawing.Size(354, 20);
            this.tbTransToolFileName.TabIndex = 1;
            // 
            // lblTransToolFileName
            // 
            this.lblTransToolFileName.AutoSize = true;
            this.lblTransToolFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTransToolFileName.Location = new System.Drawing.Point(3, 0);
            this.lblTransToolFileName.Name = "lblTransToolFileName";
            this.lblTransToolFileName.Size = new System.Drawing.Size(123, 27);
            this.lblTransToolFileName.TabIndex = 0;
            this.lblTransToolFileName.Text = "Filename:";
            this.lblTransToolFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnTransToolSave
            // 
            this.btnTransToolSave.Enabled = false;
            this.btnTransToolSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransToolSave.Image = global::KSPModAdmin.Plugin.Translation.Properties.Resources.disk_blue;
            this.btnTransToolSave.Location = new System.Drawing.Point(407, 8);
            this.btnTransToolSave.Name = "btnTransToolSave";
            this.btnTransToolSave.Size = new System.Drawing.Size(100, 23);
            this.btnTransToolSave.TabIndex = 2;
            this.btnTransToolSave.Text = "Save";
            this.btnTransToolSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTransToolSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttTransTool.SetToolTip(this.btnTransToolSave, "Saves the changes.\r\nTo create a new language file just change the Filename and cl" +
        "ick save.");
            this.btnTransToolSave.UseVisualStyleBackColor = true;
            this.btnTransToolSave.Click += new System.EventHandler(this.btnTransToolSave_Click);
            // 
            // cbTransToolLanguages
            // 
            this.cbTransToolLanguages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbTransToolLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTransToolLanguages.FormattingEnabled = true;
            this.cbTransToolLanguages.Location = new System.Drawing.Point(102, 3);
            this.cbTransToolLanguages.Name = "cbTransToolLanguages";
            this.cbTransToolLanguages.Size = new System.Drawing.Size(186, 21);
            this.cbTransToolLanguages.TabIndex = 1;
            this.ttTransTool.SetToolTip(this.cbTransToolLanguages, "Select a language file to edit.");
            this.cbTransToolLanguages.SelectedIndexChanged += new System.EventHandler(this.cbTransToolLanguages_SelectedIndexChanged);
            // 
            // lblTransToolLanguages
            // 
            this.lblTransToolLanguages.AutoSize = true;
            this.lblTransToolLanguages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTransToolLanguages.Location = new System.Drawing.Point(3, 0);
            this.lblTransToolLanguages.Name = "lblTransToolLanguages";
            this.lblTransToolLanguages.Size = new System.Drawing.Size(93, 27);
            this.lblTransToolLanguages.TabIndex = 0;
            this.lblTransToolLanguages.Text = "Languages:";
            this.lblTransToolLanguages.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbTransToolLanguageFileEntries
            // 
            this.gbTransToolLanguageFileEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTransToolLanguageFileEntries.Controls.Add(this.dgvTransToolLanguageFileEntries);
            this.gbTransToolLanguageFileEntries.Location = new System.Drawing.Point(6, 146);
            this.gbTransToolLanguageFileEntries.Name = "gbTransToolLanguageFileEntries";
            this.gbTransToolLanguageFileEntries.Size = new System.Drawing.Size(501, 248);
            this.gbTransToolLanguageFileEntries.TabIndex = 4;
            this.gbTransToolLanguageFileEntries.TabStop = false;
            this.gbTransToolLanguageFileEntries.Text = "Languae file entries:";
            // 
            // cbTransToolEdit
            // 
            this.cbTransToolEdit.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbTransToolEdit.Image = global::KSPModAdmin.Plugin.Translation.Properties.Resources.pencil2_delete;
            this.cbTransToolEdit.Location = new System.Drawing.Point(303, 8);
            this.cbTransToolEdit.Name = "cbTransToolEdit";
            this.cbTransToolEdit.Size = new System.Drawing.Size(98, 23);
            this.cbTransToolEdit.TabIndex = 1;
            this.cbTransToolEdit.Text = "Edit";
            this.cbTransToolEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbTransToolEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttTransTool.SetToolTip(this.cbTransToolEdit, "Click to edit language file.");
            this.cbTransToolEdit.UseVisualStyleBackColor = true;
            this.cbTransToolEdit.CheckedChanged += new System.EventHandler(this.cbTransToolEdit_CheckedChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.31734F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.68266F));
            this.tableLayoutPanel4.Controls.Add(this.cbTransToolLanguages, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblTransToolLanguages, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(291, 27);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // ucTranslationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.cbTransToolEdit);
            this.Controls.Add(this.gbTransToolLanguageFileEntries);
            this.Controls.Add(this.pbTransToolExport);
            this.Controls.Add(this.gbTransToolLanguageFile);
            this.Controls.Add(this.btnTransToolSave);
            this.MinimumSize = new System.Drawing.Size(450, 400);
            this.Name = "ucTranslationView";
            this.Size = new System.Drawing.Size(514, 400);
            this.Load += new System.EventHandler(this.ucTranslationView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbTransToolExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransToolLanguageFileEntries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.languageEntryBindingSource)).EndInit();
            this.gbTransToolLanguageFile.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.gbTransToolLanguageFileEntries.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbTransToolExport;
        internal System.Windows.Forms.DataGridView dgvTransToolLanguageFileEntries;
        internal System.Windows.Forms.GroupBox gbTransToolLanguageFile;
        internal System.Windows.Forms.TextBox tbTransToolLanguageShortName;
        internal System.Windows.Forms.Label lblTransToolLanguageShortName;
        internal System.Windows.Forms.TextBox tbTransToolLanguageName;
        internal System.Windows.Forms.Label lblTransToolLanguageName;
        internal System.Windows.Forms.TextBox tbTransToolFileName;
        internal System.Windows.Forms.Label lblTransToolFileName;
        internal System.Windows.Forms.Button btnTransToolSave;
        internal System.Windows.Forms.ComboBox cbTransToolLanguages;
        internal System.Windows.Forms.Label lblTransToolLanguages;
        private System.Windows.Forms.BindingSource languageEntryBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.GroupBox gbTransToolLanguageFileEntries;
        private System.Windows.Forms.ToolTip ttTransTool;
        private System.Windows.Forms.CheckBox cbTransToolEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}
