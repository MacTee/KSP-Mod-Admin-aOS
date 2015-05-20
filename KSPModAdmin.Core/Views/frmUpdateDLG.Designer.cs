namespace KSPModAdmin.Core.Views
{
    partial class frmUpdateDLG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateDLG));
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.tbDownloadPath = new System.Windows.Forms.TextBox();
            this.lblDownloadPath = new System.Windows.Forms.Label();
            this.lblPostDownloadAction = new System.Windows.Forms.Label();
            this.cbPostDownloadAction = new System.Windows.Forms.ComboBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnDownloadPath = new System.Windows.Forms.Button();
            this.ttUpdateDLG = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = global::KSPModAdmin.Core.Properties.Resources.delete2;
            this.btnCancel.Location = new System.Drawing.Point(502, 261);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttUpdateDLG.SetToolTip(this.btnCancel, "Abort and close the dialog.");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbMessage
            // 
            this.tbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessage.Location = new System.Drawing.Point(1, 1);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.ReadOnly = true;
            this.tbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMessage.Size = new System.Drawing.Size(622, 214);
            this.tbMessage.TabIndex = 0;
            this.tbMessage.TabStop = false;
            this.tbMessage.WordWrap = false;
            // 
            // tbDownloadPath
            // 
            this.tbDownloadPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDownloadPath.Location = new System.Drawing.Point(128, 3);
            this.tbDownloadPath.Name = "tbDownloadPath";
            this.tbDownloadPath.ReadOnly = true;
            this.tbDownloadPath.Size = new System.Drawing.Size(433, 20);
            this.tbDownloadPath.TabIndex = 1;
            // 
            // lblDownloadPath
            // 
            this.lblDownloadPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDownloadPath.Location = new System.Drawing.Point(3, 0);
            this.lblDownloadPath.Name = "lblDownloadPath";
            this.lblDownloadPath.Size = new System.Drawing.Size(119, 25);
            this.lblDownloadPath.TabIndex = 0;
            this.lblDownloadPath.Text = "Download path:";
            this.lblDownloadPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPostDownloadAction
            // 
            this.lblPostDownloadAction.AutoSize = true;
            this.lblPostDownloadAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPostDownloadAction.Location = new System.Drawing.Point(3, 0);
            this.lblPostDownloadAction.Name = "lblPostDownloadAction";
            this.lblPostDownloadAction.Size = new System.Drawing.Size(119, 25);
            this.lblPostDownloadAction.TabIndex = 0;
            this.lblPostDownloadAction.Text = "Post download action:";
            this.lblPostDownloadAction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbPostDownloadAction
            // 
            this.cbPostDownloadAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPostDownloadAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPostDownloadAction.FormattingEnabled = true;
            this.cbPostDownloadAction.Items.AddRange(new object[] {
            "Ignore finished download",
            "Ask for auto install",
            "Auto install after download"});
            this.cbPostDownloadAction.Location = new System.Drawing.Point(128, 3);
            this.cbPostDownloadAction.Name = "cbPostDownloadAction";
            this.cbPostDownloadAction.Size = new System.Drawing.Size(236, 21);
            this.cbPostDownloadAction.TabIndex = 1;
            this.ttUpdateDLG.SetToolTip(this.cbPostDownloadAction, "The action that should be executed after a update.");
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Image = global::KSPModAdmin.Core.Properties.Resources.download;
            this.btnDownload.Location = new System.Drawing.Point(388, 261);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(108, 25);
            this.btnDownload.TabIndex = 4;
            this.btnDownload.Text = "Download";
            this.btnDownload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttUpdateDLG.SetToolTip(this.btnDownload, "Start the download of the KSP MA update.");
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnDownloadPath
            // 
            this.btnDownloadPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownloadPath.Image = global::KSPModAdmin.Core.Properties.Resources.folder_view;
            this.btnDownloadPath.Location = new System.Drawing.Point(585, 226);
            this.btnDownloadPath.Name = "btnDownloadPath";
            this.btnDownloadPath.Size = new System.Drawing.Size(25, 23);
            this.btnDownloadPath.TabIndex = 2;
            this.ttUpdateDLG.SetToolTip(this.btnDownloadPath, "Opens a folder browser to select a download folder.");
            this.btnDownloadPath.UseVisualStyleBackColor = true;
            this.btnDownloadPath.Click += new System.EventHandler(this.btnDownloadPath_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tbDownloadPath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDownloadPath, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 224);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(564, 25);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.cbPostDownloadAction, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblPostDownloadAction, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(15, 261);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(367, 25);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // frmUpdateDLG
            // 
            this.AcceptButton = this.btnDownload;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 295);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnDownloadPath);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(550, 300);
            this.Name = "frmUpdateDLG";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "KSP MA Update";
            this.Load += new System.EventHandler(this.UpdateDLG_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.TextBox tbDownloadPath;
        private System.Windows.Forms.Button btnDownloadPath;
        private System.Windows.Forms.Label lblDownloadPath;
        private System.Windows.Forms.Label lblPostDownloadAction;
        private System.Windows.Forms.ComboBox cbPostDownloadAction;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.ToolTip ttUpdateDLG;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}