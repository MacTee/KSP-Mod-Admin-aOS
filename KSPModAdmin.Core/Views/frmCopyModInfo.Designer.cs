namespace KSPModAdmin.Core.Views
{
    partial class frmCopyModInfo
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
            this.ttEditModInfo = new System.Windows.Forms.ToolTip(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.lblCopyModInfosModName = new System.Windows.Forms.Label();
            this.tbSourceMod = new System.Windows.Forms.TextBox();
            this.lblCopyModInfosTo = new System.Windows.Forms.Label();
            this.cbDestMod = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Image = global::KSPModAdmin.Core.Properties.Resources.delete2;
            this.btnCancel.Location = new System.Drawing.Point(262, 127);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.Image = global::KSPModAdmin.Core.Properties.Resources.components_into;
            this.btnCopy.Location = new System.Drawing.Point(146, 127);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(110, 25);
            this.btnCopy.TabIndex = 4;
            this.btnCopy.Text = "Copy";
            this.btnCopy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // lblCopyModInfosModName
            // 
            this.lblCopyModInfosModName.AutoSize = true;
            this.lblCopyModInfosModName.Location = new System.Drawing.Point(23, 21);
            this.lblCopyModInfosModName.Name = "lblCopyModInfosModName";
            this.lblCopyModInfosModName.Size = new System.Drawing.Size(122, 13);
            this.lblCopyModInfosModName.TabIndex = 0;
            this.lblCopyModInfosModName.Text = "Copy the ModInfos from:";
            // 
            // tbSourceMod
            // 
            this.tbSourceMod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSourceMod.Location = new System.Drawing.Point(26, 42);
            this.tbSourceMod.Name = "tbSourceMod";
            this.tbSourceMod.ReadOnly = true;
            this.tbSourceMod.Size = new System.Drawing.Size(329, 20);
            this.tbSourceMod.TabIndex = 1;
            // 
            // lblCopyModInfosTo
            // 
            this.lblCopyModInfosTo.AutoSize = true;
            this.lblCopyModInfosTo.Location = new System.Drawing.Point(23, 69);
            this.lblCopyModInfosTo.Name = "lblCopyModInfosTo";
            this.lblCopyModInfosTo.Size = new System.Drawing.Size(23, 13);
            this.lblCopyModInfosTo.TabIndex = 2;
            this.lblCopyModInfosTo.Text = "To:";
            // 
            // cbDestMod
            // 
            this.cbDestMod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDestMod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDestMod.FormattingEnabled = true;
            this.cbDestMod.ItemHeight = 13;
            this.cbDestMod.Location = new System.Drawing.Point(26, 90);
            this.cbDestMod.Name = "cbDestMod";
            this.cbDestMod.Size = new System.Drawing.Size(329, 21);
            this.cbDestMod.TabIndex = 3;
            // 
            // frmCopyModInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 162);
            this.Controls.Add(this.cbDestMod);
            this.Controls.Add(this.tbSourceMod);
            this.Controls.Add(this.lblCopyModInfosTo);
            this.Controls.Add(this.lblCopyModInfosModName);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCopyModInfo";
            this.Text = "Copy ModInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttEditModInfo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label lblCopyModInfosModName;
        private System.Windows.Forms.TextBox tbSourceMod;
        private System.Windows.Forms.Label lblCopyModInfosTo;
        private System.Windows.Forms.ComboBox cbDestMod;
    }
}