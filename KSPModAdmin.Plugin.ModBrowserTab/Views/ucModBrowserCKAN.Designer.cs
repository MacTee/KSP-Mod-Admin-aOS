namespace KSPModAdmin.Plugin.ModBrowserTab.Views
{
    partial class UcModBrowserCkan
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
            this.tsModBrowserCkan = new System.Windows.Forms.ToolStrip();
            this.tsbModBrowserCkanRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslModBrowserCkanRepository = new System.Windows.Forms.ToolStripLabel();
            this.cbModBrowserCkanRepository = new System.Windows.Forms.ToolStripComboBox();
            this.tvCkanRepositories = new KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.TreeViewAdv();
            this.lblModBrowserCkanCount = new System.Windows.Forms.Label();
            this.ttModBrowserCkan = new System.Windows.Forms.ToolTip(this.components);
            this.cbModBrowserCkanJustAdd = new System.Windows.Forms.CheckBox();
            this.btnModBrowserCkanProcessChanges = new System.Windows.Forms.Button();
            this.lblModBrowserCkanSelectRepository = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tsModBrowserCkan.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsModBrowserCkan
            // 
            this.tsModBrowserCkan.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbModBrowserCkanRefresh,
            this.toolStripSeparator1,
            this.tslModBrowserCkanRepository,
            this.cbModBrowserCkanRepository});
            this.tsModBrowserCkan.Location = new System.Drawing.Point(0, 0);
            this.tsModBrowserCkan.Name = "tsModBrowserCkan";
            this.tsModBrowserCkan.Size = new System.Drawing.Size(675, 31);
            this.tsModBrowserCkan.TabIndex = 0;
            this.tsModBrowserCkan.Text = "toolStrip1";
            // 
            // tsbModBrowserCkanRefresh
            // 
            this.tsbModBrowserCkanRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbModBrowserCkanRefresh.Image = global::KSPModAdmin.Plugin.ModBrowserTab.Properties.Resources.refresh_24x24;
            this.tsbModBrowserCkanRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbModBrowserCkanRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModBrowserCkanRefresh.Name = "tsbModBrowserCkanRefresh";
            this.tsbModBrowserCkanRefresh.Size = new System.Drawing.Size(28, 28);
            this.tsbModBrowserCkanRefresh.Text = "toolStripButton1";
            this.tsbModBrowserCkanRefresh.ToolTipText = "Refresh^Refreshes the selected repository and redownloads the repository archive." +
    "";
            this.tsbModBrowserCkanRefresh.Click += new System.EventHandler(this.tsbModBrowserCkanRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tslModBrowserCkanRepository
            // 
            this.tslModBrowserCkanRepository.Name = "tslModBrowserCkanRepository";
            this.tslModBrowserCkanRepository.Size = new System.Drawing.Size(66, 28);
            this.tslModBrowserCkanRepository.Text = "Repository:";
            // 
            // cbModBrowserCkanRepository
            // 
            this.cbModBrowserCkanRepository.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModBrowserCkanRepository.Name = "cbModBrowserCkanRepository";
            this.cbModBrowserCkanRepository.Size = new System.Drawing.Size(200, 31);
            this.cbModBrowserCkanRepository.DropDown += new System.EventHandler(this.cbModBrowserCkanRepository_DropDown);
            this.cbModBrowserCkanRepository.SelectedIndexChanged += new System.EventHandler(this.cbModBrowserCkanRepository_SelectedIndexChanged);
            // 
            // tvCkanRepositories
            // 
            this.tvCkanRepositories.AllowColumnSort = true;
            this.tvCkanRepositories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvCkanRepositories.BackColor = System.Drawing.SystemColors.Window;
            this.tvCkanRepositories.DefaultToolTipProvider = null;
            this.tvCkanRepositories.DragDropMarkColor = System.Drawing.Color.Black;
            this.tvCkanRepositories.GridLineStyle = ((KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.GridLineStyle)((KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.GridLineStyle.Horizontal | KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.GridLineStyle.Vertical)));
            this.tvCkanRepositories.LineColor = System.Drawing.SystemColors.ControlDark;
            this.tvCkanRepositories.Location = new System.Drawing.Point(0, 31);
            this.tvCkanRepositories.Model = null;
            this.tvCkanRepositories.Name = "tvCkanRepositories";
            this.tvCkanRepositories.SelectedNode = null;
            this.tvCkanRepositories.Size = new System.Drawing.Size(675, 345);
            this.tvCkanRepositories.TabIndex = 1;
            this.tvCkanRepositories.Text = "treeViewAdv1";
            this.tvCkanRepositories.UseColumns = true;
            this.tvCkanRepositories.DrawControl += new System.EventHandler<KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.NodeControls.DrawEventArgs>(this.tvCkanRepositories_DrawControl);
            // 
            // lblModBrowserCkanCount
            // 
            this.lblModBrowserCkanCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblModBrowserCkanCount.AutoSize = true;
            this.lblModBrowserCkanCount.Location = new System.Drawing.Point(3, 382);
            this.lblModBrowserCkanCount.Name = "lblModBrowserCkanCount";
            this.lblModBrowserCkanCount.Size = new System.Drawing.Size(47, 13);
            this.lblModBrowserCkanCount.TabIndex = 27;
            this.lblModBrowserCkanCount.Text = "Count: 0";
            // 
            // cbModBrowserCkanJustAdd
            // 
            this.cbModBrowserCkanJustAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cbModBrowserCkanJustAdd.AutoSize = true;
            this.cbModBrowserCkanJustAdd.Location = new System.Drawing.Point(230, 3);
            this.cbModBrowserCkanJustAdd.Name = "cbModBrowserCkanJustAdd";
            this.cbModBrowserCkanJustAdd.Size = new System.Drawing.Size(219, 15);
            this.cbModBrowserCkanJustAdd.TabIndex = 29;
            this.cbModBrowserCkanJustAdd.Text = "Just add to or remove from ModSelection";
            this.ttModBrowserCkan.SetToolTip(this.cbModBrowserCkanJustAdd, "When checked KMA² will only add a mod during processing and won\'t install it.^To " +
        "insatll these mods you have to switch to the ModSelection tab.");
            this.cbModBrowserCkanJustAdd.UseVisualStyleBackColor = true;
            // 
            // btnModBrowserCkanProcessChanges
            // 
            this.btnModBrowserCkanProcessChanges.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModBrowserCkanProcessChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModBrowserCkanProcessChanges.Image = global::KSPModAdmin.Plugin.ModBrowserTab.Properties.Resources.components_gearwheel_24x24;
            this.btnModBrowserCkanProcessChanges.Location = new System.Drawing.Point(0, 400);
            this.btnModBrowserCkanProcessChanges.Name = "btnModBrowserCkanProcessChanges";
            this.btnModBrowserCkanProcessChanges.Size = new System.Drawing.Size(675, 39);
            this.btnModBrowserCkanProcessChanges.TabIndex = 28;
            this.btnModBrowserCkanProcessChanges.Text = "Process changes";
            this.btnModBrowserCkanProcessChanges.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnModBrowserCkanProcessChanges.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttModBrowserCkan.SetToolTip(this.btnModBrowserCkanProcessChanges, "Process changes^Starts the processing of the changes made in the selection.^Adds " +
        "(and installs) the new checked mods and removes the unchecked ones.");
            this.btnModBrowserCkanProcessChanges.UseVisualStyleBackColor = true;
            this.btnModBrowserCkanProcessChanges.Click += new System.EventHandler(this.btnModBrowserCkanProcessChanges_Click);
            // 
            // lblModBrowserCkanSelectRepository
            // 
            this.lblModBrowserCkanSelectRepository.AutoSize = true;
            this.lblModBrowserCkanSelectRepository.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblModBrowserCkanSelectRepository.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModBrowserCkanSelectRepository.Location = new System.Drawing.Point(228, 0);
            this.lblModBrowserCkanSelectRepository.Name = "lblModBrowserCkanSelectRepository";
            this.lblModBrowserCkanSelectRepository.Size = new System.Drawing.Size(207, 40);
            this.lblModBrowserCkanSelectRepository.TabIndex = 27;
            this.lblModBrowserCkanSelectRepository.Text = "Please select a Repository.";
            this.lblModBrowserCkanSelectRepository.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblModBrowserCkanSelectRepository, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 69);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(663, 40);
            this.tableLayoutPanel1.TabIndex = 30;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.cbModBrowserCkanJustAdd, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(220, 376);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(452, 21);
            this.tableLayoutPanel2.TabIndex = 31;
            // 
            // ucModBrowserCKAN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnModBrowserCkanProcessChanges);
            this.Controls.Add(this.lblModBrowserCkanCount);
            this.Controls.Add(this.tvCkanRepositories);
            this.Controls.Add(this.tsModBrowserCkan);
            this.Name = "UcModBrowserCkan";
            this.Size = new System.Drawing.Size(675, 442);
            this.Load += new System.EventHandler(this.ucModBrowserCKAN_Load);
            this.tsModBrowserCkan.ResumeLayout(false);
            this.tsModBrowserCkan.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsModBrowserCkan;
        private System.Windows.Forms.ToolStripLabel tslModBrowserCkanRepository;
        private System.Windows.Forms.ToolStripButton tsbModBrowserCkanRefresh;
        private System.Windows.Forms.ToolStripComboBox cbModBrowserCkanRepository;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Core.Utils.Controls.Aga.Controls.Tree.TreeViewAdv tvCkanRepositories;
        private System.Windows.Forms.Label lblModBrowserCkanCount;
        private System.Windows.Forms.ToolTip ttModBrowserCkan;
        private System.Windows.Forms.Button btnModBrowserCkanProcessChanges;
        private System.Windows.Forms.CheckBox cbModBrowserCkanJustAdd;
        private System.Windows.Forms.Label lblModBrowserCkanSelectRepository;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
