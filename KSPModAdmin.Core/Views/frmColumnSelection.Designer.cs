namespace KSPModAdmin.Core.Views
{
    partial class frmColumnSelection
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
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Mod");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Version");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("VersionControl");
            this.lblColSelectDisplayed = new System.Windows.Forms.Label();
            this.lblColSelectAvailable = new System.Windows.Forms.Label();
            this.lvDisplayedColumns = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvAvailableColumns = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblColSelectDescription = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblColSelectDisplayed
            // 
            this.lblColSelectDisplayed.AutoSize = true;
            this.lblColSelectDisplayed.Location = new System.Drawing.Point(12, 45);
            this.lblColSelectDisplayed.Name = "lblColSelectDisplayed";
            this.lblColSelectDisplayed.Size = new System.Drawing.Size(135, 13);
            this.lblColSelectDisplayed.TabIndex = 0;
            this.lblColSelectDisplayed.Text = "Displayed in ModSelection:";
            // 
            // lblColSelectAvailable
            // 
            this.lblColSelectAvailable.AutoSize = true;
            this.lblColSelectAvailable.Location = new System.Drawing.Point(181, 45);
            this.lblColSelectAvailable.Name = "lblColSelectAvailable";
            this.lblColSelectAvailable.Size = new System.Drawing.Size(95, 13);
            this.lblColSelectAvailable.TabIndex = 0;
            this.lblColSelectAvailable.Text = "Available columns:";
            // 
            // lvDisplayedColumns
            // 
            this.lvDisplayedColumns.AllowDrop = true;
            this.lvDisplayedColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvDisplayedColumns.FullRowSelect = true;
            this.lvDisplayedColumns.GridLines = true;
            this.lvDisplayedColumns.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.lvDisplayedColumns.Location = new System.Drawing.Point(15, 61);
            this.lvDisplayedColumns.Name = "lvDisplayedColumns";
            this.lvDisplayedColumns.Size = new System.Drawing.Size(163, 200);
            this.lvDisplayedColumns.TabIndex = 1;
            this.lvDisplayedColumns.UseCompatibleStateImageBehavior = false;
            this.lvDisplayedColumns.View = System.Windows.Forms.View.Details;
            this.lvDisplayedColumns.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView_ItemDrag);
            this.lvDisplayedColumns.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView_DragDrop);
            this.lvDisplayedColumns.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView_DragEnter);
            this.lvDisplayedColumns.DragOver += new System.Windows.Forms.DragEventHandler(this.listView_DragOver);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Column";
            this.columnHeader1.Width = 157;
            // 
            // lvAvailableColumns
            // 
            this.lvAvailableColumns.AllowDrop = true;
            this.lvAvailableColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lvAvailableColumns.FullRowSelect = true;
            this.lvAvailableColumns.GridLines = true;
            this.lvAvailableColumns.Location = new System.Drawing.Point(184, 61);
            this.lvAvailableColumns.Name = "lvAvailableColumns";
            this.lvAvailableColumns.Size = new System.Drawing.Size(163, 200);
            this.lvAvailableColumns.TabIndex = 1;
            this.lvAvailableColumns.UseCompatibleStateImageBehavior = false;
            this.lvAvailableColumns.View = System.Windows.Forms.View.Details;
            this.lvAvailableColumns.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView_ItemDrag);
            this.lvAvailableColumns.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView_DragDrop);
            this.lvAvailableColumns.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView_DragEnter);
            this.lvAvailableColumns.DragOver += new System.Windows.Forms.DragEventHandler(this.listView_DragOver);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Column";
            this.columnHeader2.Width = 157;
            // 
            // lblColSelectDescription
            // 
            this.lblColSelectDescription.Location = new System.Drawing.Point(12, 9);
            this.lblColSelectDescription.Name = "lblColSelectDescription";
            this.lblColSelectDescription.Size = new System.Drawing.Size(335, 26);
            this.lblColSelectDescription.TabIndex = 0;
            this.lblColSelectDescription.Text = "Drag && Drop to reorder columns\r\nMove columns between lists to show or hide them." +
    "";
            // 
            // btnApply
            // 
            this.btnApply.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Image = global::KSPModAdmin.Core.Properties.Resources.tick;
            this.btnApply.Location = new System.Drawing.Point(121, 268);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(110, 25);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply";
            this.btnApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Image = global::KSPModAdmin.Core.Properties.Resources.delete2;
            this.btnCancel.Location = new System.Drawing.Point(237, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmColumnSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 305);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lvAvailableColumns);
            this.Controls.Add(this.lvDisplayedColumns);
            this.Controls.Add(this.lblColSelectAvailable);
            this.Controls.Add(this.lblColSelectDescription);
            this.Controls.Add(this.lblColSelectDisplayed);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmColumnSelection";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Column selection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblColSelectDisplayed;
        private System.Windows.Forms.Label lblColSelectAvailable;
        private System.Windows.Forms.ListView lvDisplayedColumns;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView lvAvailableColumns;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label lblColSelectDescription;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
    }
}