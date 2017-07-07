namespace KSPModAdmin.Plugin.ModBrowserTab.Views
{
    partial class UcModBrowserKerbalStuff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcModBrowserKerbalStuff));
            this.tsModBrowserKs = new System.Windows.Forms.ToolStrip();
            this.tsbModBrowserKsRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbModBrowserKsBrowseNew = new System.Windows.Forms.ToolStripButton();
            this.tsbModBrowserKsBrowseFeatured = new System.Windows.Forms.ToolStripButton();
            this.tsbModBrowserKsBrowseTop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbModBrowserKsFirstPage = new System.Windows.Forms.ToolStripButton();
            this.tsbModBrowserKsPreviousPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.tsbModBrowserKsNextPage = new System.Windows.Forms.ToolStripButton();
            this.tsbModBrowserKsLastPage = new System.Windows.Forms.ToolStripButton();
            this.tslProcessing = new System.Windows.Forms.ToolStripLabel();
            this.tvKsRepositories = new KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.TreeViewAdv();
            this.lblModBrowserKsCount = new System.Windows.Forms.Label();
            this.ttModBrowserKs = new System.Windows.Forms.ToolTip(this.components);
            this.tsModBrowserKs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsModBrowserKs
            // 
            this.tsModBrowserKs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbModBrowserKsRefresh,
            this.toolStripSeparator1,
            this.tsbModBrowserKsBrowseNew,
            this.tsbModBrowserKsBrowseFeatured,
            this.tsbModBrowserKsBrowseTop,
            this.toolStripSeparator2,
            this.tsbModBrowserKsFirstPage,
            this.tsbModBrowserKsPreviousPage,
            this.toolStripLabel1,
            this.toolStripTextBox1,
            this.toolStripLabel2,
            this.toolStripTextBox2,
            this.tsbModBrowserKsNextPage,
            this.tsbModBrowserKsLastPage,
            this.tslProcessing});
            this.tsModBrowserKs.Location = new System.Drawing.Point(0, 0);
            this.tsModBrowserKs.Name = "tsModBrowserKs";
            this.tsModBrowserKs.Size = new System.Drawing.Size(675, 31);
            this.tsModBrowserKs.TabIndex = 0;
            this.tsModBrowserKs.Text = "toolStrip1";
            // 
            // tsbModBrowserKsRefresh
            // 
            this.tsbModBrowserKsRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbModBrowserKsRefresh.Image = global::KSPModAdmin.Plugin.ModBrowserTab.Properties.Resources.refresh_24x24;
            this.tsbModBrowserKsRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbModBrowserKsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModBrowserKsRefresh.Name = "tsbModBrowserKsRefresh";
            this.tsbModBrowserKsRefresh.Size = new System.Drawing.Size(28, 28);
            this.tsbModBrowserKsRefresh.Text = "Refresh";
            this.tsbModBrowserKsRefresh.ToolTipText = "Refresh";
            this.tsbModBrowserKsRefresh.Click += new System.EventHandler(this.tsbModBrowserKsRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbModBrowserKsBrowseNew
            // 
            this.tsbModBrowserKsBrowseNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbModBrowserKsBrowseNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbModBrowserKsBrowseNew.Image")));
            this.tsbModBrowserKsBrowseNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModBrowserKsBrowseNew.Name = "tsbModBrowserKsBrowseNew";
            this.tsbModBrowserKsBrowseNew.Size = new System.Drawing.Size(23, 28);
            this.tsbModBrowserKsBrowseNew.Text = "Browse new mods";
            this.tsbModBrowserKsBrowseNew.Click += new System.EventHandler(this.tsbModBrowserKsBrowseNew_Click);
            // 
            // tsbModBrowserKsBrowseFeatured
            // 
            this.tsbModBrowserKsBrowseFeatured.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbModBrowserKsBrowseFeatured.Image = ((System.Drawing.Image)(resources.GetObject("tsbModBrowserKsBrowseFeatured.Image")));
            this.tsbModBrowserKsBrowseFeatured.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModBrowserKsBrowseFeatured.Name = "tsbModBrowserKsBrowseFeatured";
            this.tsbModBrowserKsBrowseFeatured.Size = new System.Drawing.Size(23, 28);
            this.tsbModBrowserKsBrowseFeatured.Text = "Browse featured mods";
            this.tsbModBrowserKsBrowseFeatured.Click += new System.EventHandler(this.tsbModBrowserKsBrowseFeatured_Click);
            // 
            // tsbModBrowserKsBrowseTop
            // 
            this.tsbModBrowserKsBrowseTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbModBrowserKsBrowseTop.Image = ((System.Drawing.Image)(resources.GetObject("tsbModBrowserKsBrowseTop.Image")));
            this.tsbModBrowserKsBrowseTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModBrowserKsBrowseTop.Name = "tsbModBrowserKsBrowseTop";
            this.tsbModBrowserKsBrowseTop.Size = new System.Drawing.Size(23, 28);
            this.tsbModBrowserKsBrowseTop.Text = "Browse top mods";
            this.tsbModBrowserKsBrowseTop.Click += new System.EventHandler(this.tsbModBrowserKsBrowseTop_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbModBrowserKsFirstPage
            // 
            this.tsbModBrowserKsFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbModBrowserKsFirstPage.Image = global::KSPModAdmin.Plugin.ModBrowserTab.Properties.Resources.navigate_beginning;
            this.tsbModBrowserKsFirstPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModBrowserKsFirstPage.Name = "tsbModBrowserKsFirstPage";
            this.tsbModBrowserKsFirstPage.Size = new System.Drawing.Size(23, 28);
            this.tsbModBrowserKsFirstPage.Text = "toolStripButton4";
            this.tsbModBrowserKsFirstPage.Click += new System.EventHandler(this.tsbModBrowserKsFirstPage_Click);
            // 
            // tsbModBrowserKsPreviousPage
            // 
            this.tsbModBrowserKsPreviousPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbModBrowserKsPreviousPage.Image = global::KSPModAdmin.Plugin.ModBrowserTab.Properties.Resources.navigate_left;
            this.tsbModBrowserKsPreviousPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModBrowserKsPreviousPage.Name = "tsbModBrowserKsPreviousPage";
            this.tsbModBrowserKsPreviousPage.Size = new System.Drawing.Size(23, 28);
            this.tsbModBrowserKsPreviousPage.Text = "toolStripButton5";
            this.tsbModBrowserKsPreviousPage.Click += new System.EventHandler(this.tsbModBrowserKsPreviousPage_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 28);
            this.toolStripLabel1.Text = "Page:";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.ReadOnly = true;
            this.toolStripTextBox1.Size = new System.Drawing.Size(40, 31);
            this.toolStripTextBox1.Text = "1";
            this.toolStripTextBox1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(18, 28);
            this.toolStripLabel2.Text = "of";
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.ReadOnly = true;
            this.toolStripTextBox2.Size = new System.Drawing.Size(40, 31);
            this.toolStripTextBox2.Text = "1";
            this.toolStripTextBox2.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tsbModBrowserKsNextPage
            // 
            this.tsbModBrowserKsNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbModBrowserKsNextPage.Image = global::KSPModAdmin.Plugin.ModBrowserTab.Properties.Resources.navigate_right;
            this.tsbModBrowserKsNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModBrowserKsNextPage.Name = "tsbModBrowserKsNextPage";
            this.tsbModBrowserKsNextPage.Size = new System.Drawing.Size(23, 28);
            this.tsbModBrowserKsNextPage.Text = "toolStripButton6";
            this.tsbModBrowserKsNextPage.Click += new System.EventHandler(this.tsbModBrowserKsNextPage_Click);
            // 
            // tsbModBrowserKsLastPage
            // 
            this.tsbModBrowserKsLastPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbModBrowserKsLastPage.Image = global::KSPModAdmin.Plugin.ModBrowserTab.Properties.Resources.navigate_end;
            this.tsbModBrowserKsLastPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModBrowserKsLastPage.Name = "tsbModBrowserKsLastPage";
            this.tsbModBrowserKsLastPage.Size = new System.Drawing.Size(23, 28);
            this.tsbModBrowserKsLastPage.Text = "toolStripButton7";
            this.tsbModBrowserKsLastPage.Click += new System.EventHandler(this.tsbModBrowserKsLastPage_Click);
            // 
            // tslProcessing
            // 
            this.tslProcessing.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslProcessing.Image = global::KSPModAdmin.Plugin.ModBrowserTab.Properties.Resources.loader;
            this.tslProcessing.Name = "tslProcessing";
            this.tslProcessing.Size = new System.Drawing.Size(16, 28);
            this.tslProcessing.Visible = false;
            // 
            // tvKsRepositories
            // 
            this.tvKsRepositories.AllowColumnSort = true;
            this.tvKsRepositories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvKsRepositories.BackColor = System.Drawing.SystemColors.Window;
            this.tvKsRepositories.DefaultToolTipProvider = null;
            this.tvKsRepositories.DragDropMarkColor = System.Drawing.Color.Black;
            this.tvKsRepositories.GridLineStyle = ((KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.GridLineStyle)((KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.GridLineStyle.Horizontal | KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.GridLineStyle.Vertical)));
            this.tvKsRepositories.LineColor = System.Drawing.SystemColors.ControlDark;
            this.tvKsRepositories.Location = new System.Drawing.Point(0, 31);
            this.tvKsRepositories.Model = null;
            this.tvKsRepositories.Name = "tvKsRepositories";
            this.tvKsRepositories.SelectedNode = null;
            this.tvKsRepositories.Size = new System.Drawing.Size(675, 395);
            this.tvKsRepositories.TabIndex = 1;
            this.tvKsRepositories.Text = "treeViewAdv1";
            this.tvKsRepositories.UseColumns = true;
            this.tvKsRepositories.DrawControl += new System.EventHandler<KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.NodeControls.DrawEventArgs>(this.tvKsRepositories_DrawControl);
            // 
            // lblModBrowserKsCount
            // 
            this.lblModBrowserKsCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblModBrowserKsCount.AutoSize = true;
            this.lblModBrowserKsCount.Location = new System.Drawing.Point(3, 429);
            this.lblModBrowserKsCount.Name = "lblModBrowserKsCount";
            this.lblModBrowserKsCount.Size = new System.Drawing.Size(47, 13);
            this.lblModBrowserKsCount.TabIndex = 27;
            this.lblModBrowserKsCount.Text = "Count: 0";
            // 
            // UcModBrowserKerbalStuff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblModBrowserKsCount);
            this.Controls.Add(this.tvKsRepositories);
            this.Controls.Add(this.tsModBrowserKs);
            this.Name = "UcModBrowserKerbalStuff";
            this.Size = new System.Drawing.Size(675, 442);
            this.Load += new System.EventHandler(this.ucModBrowserKerbalStuff_Load);
            this.tsModBrowserKs.ResumeLayout(false);
            this.tsModBrowserKs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsModBrowserKs;
        private System.Windows.Forms.ToolStripButton tsbModBrowserKsRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Core.Utils.Controls.Aga.Controls.Tree.TreeViewAdv tvKsRepositories;
        private System.Windows.Forms.Label lblModBrowserKsCount;
        private System.Windows.Forms.ToolTip ttModBrowserKs;
        private System.Windows.Forms.ToolStripButton tsbModBrowserKsBrowseNew;
        private System.Windows.Forms.ToolStripButton tsbModBrowserKsBrowseFeatured;
        private System.Windows.Forms.ToolStripButton tsbModBrowserKsBrowseTop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbModBrowserKsFirstPage;
        private System.Windows.Forms.ToolStripButton tsbModBrowserKsPreviousPage;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripButton tsbModBrowserKsNextPage;
        private System.Windows.Forms.ToolStripButton tsbModBrowserKsLastPage;
        private System.Windows.Forms.ToolStripLabel tslProcessing;
    }
}
