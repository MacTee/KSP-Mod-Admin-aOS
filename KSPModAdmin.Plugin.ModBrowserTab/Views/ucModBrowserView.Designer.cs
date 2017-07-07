namespace KSPModAdmin.Plugin.ModBrowserTab.Views
{
    partial class ucModBrowserView
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
            this.ttModBrowser = new System.Windows.Forms.ToolTip(this.components);
            this.tsModBrowser = new System.Windows.Forms.ToolStrip();
            this.tslModBrowser = new System.Windows.Forms.ToolStripLabel();
            this.cbModBrowser = new System.Windows.Forms.ToolStripComboBox();
            this.tspbModBrowserProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tslModBrowserProcessing = new System.Windows.Forms.ToolStripLabel();
            this.lblModBrowserTabPleaseSelectModBrowser = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tsModBrowser.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsModBrowser
            // 
            this.tsModBrowser.AutoSize = false;
            this.tsModBrowser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslModBrowser,
            this.cbModBrowser,
            this.tspbModBrowserProgressBar,
            this.tslModBrowserProcessing});
            this.tsModBrowser.Location = new System.Drawing.Point(0, 0);
            this.tsModBrowser.Name = "tsModBrowser";
            this.tsModBrowser.Size = new System.Drawing.Size(675, 39);
            this.tsModBrowser.TabIndex = 0;
            this.tsModBrowser.Text = "toolStrip1";
            // 
            // tslModBrowser
            // 
            this.tslModBrowser.Name = "tslModBrowser";
            this.tslModBrowser.Size = new System.Drawing.Size(80, 36);
            this.tslModBrowser.Text = "Mod Browser:";
            // 
            // cbModBrowser
            // 
            this.cbModBrowser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModBrowser.Name = "cbModBrowser";
            this.cbModBrowser.Size = new System.Drawing.Size(200, 39);
            this.cbModBrowser.SelectedIndexChanged += new System.EventHandler(this.cbModBrowser_SelectedIndexChanged);
            // 
            // tspbModBrowserProgressBar
            // 
            this.tspbModBrowserProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tspbModBrowserProgressBar.Name = "tspbModBrowserProgressBar";
            this.tspbModBrowserProgressBar.Size = new System.Drawing.Size(100, 36);
            this.tspbModBrowserProgressBar.Visible = false;
            // 
            // tslModBrowserProcessing
            // 
            this.tslModBrowserProcessing.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslModBrowserProcessing.Image = global::KSPModAdmin.Plugin.ModBrowserTab.Properties.Resources.loader;
            this.tslModBrowserProcessing.Name = "tslModBrowserProcessing";
            this.tslModBrowserProcessing.Size = new System.Drawing.Size(16, 36);
            this.tslModBrowserProcessing.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tslModBrowserProcessing.Visible = false;
            // 
            // lblModBrowserTabPleaseSelectModBrowser
            // 
            this.lblModBrowserTabPleaseSelectModBrowser.AutoSize = true;
            this.lblModBrowserTabPleaseSelectModBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblModBrowserTabPleaseSelectModBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModBrowserTabPleaseSelectModBrowser.Location = new System.Drawing.Point(224, 0);
            this.lblModBrowserTabPleaseSelectModBrowser.Name = "lblModBrowserTabPleaseSelectModBrowser";
            this.lblModBrowserTabPleaseSelectModBrowser.Size = new System.Drawing.Size(227, 442);
            this.lblModBrowserTabPleaseSelectModBrowser.TabIndex = 1;
            this.lblModBrowserTabPleaseSelectModBrowser.Text = "Please select a ModBrowser...";
            this.lblModBrowserTabPleaseSelectModBrowser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblModBrowserTabPleaseSelectModBrowser, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(675, 442);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // ucModBrowserView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tsModBrowser);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(450, 400);
            this.Name = "ucModBrowserView";
            this.Size = new System.Drawing.Size(675, 442);
            this.Load += new System.EventHandler(this.ucPluginView_Load);
            this.tsModBrowser.ResumeLayout(false);
            this.tsModBrowser.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttModBrowser;
        private System.Windows.Forms.ToolStrip tsModBrowser;
        private System.Windows.Forms.ToolStripLabel tslModBrowser;
        private System.Windows.Forms.ToolStripComboBox cbModBrowser;
        private System.Windows.Forms.ToolStripProgressBar tspbModBrowserProgressBar;
        private System.Windows.Forms.ToolStripLabel tslModBrowserProcessing;
        private System.Windows.Forms.Label lblModBrowserTabPleaseSelectModBrowser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
