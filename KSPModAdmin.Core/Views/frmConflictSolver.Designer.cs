namespace KSPModAdmin.Core.Views
{
    partial class frmConflictSolver
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConflictSolver));
            this.treeViewAdv = new KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.TreeViewAdv();
            this.CmsConflictSolver = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSolveAllConflictsWithThisMod = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSolve = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblConflictSolverDesc = new System.Windows.Forms.Label();
            this.CmsConflictSolver.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewAdv
            // 
            this.treeViewAdv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewAdv.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv.ContextMenuStrip = this.CmsConflictSolver;
            this.treeViewAdv.DefaultToolTipProvider = null;
            this.treeViewAdv.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv.Location = new System.Drawing.Point(12, 64);
            this.treeViewAdv.Model = null;
            this.treeViewAdv.Name = "treeViewAdv";
            this.treeViewAdv.SelectedNode = null;
            this.treeViewAdv.Size = new System.Drawing.Size(910, 331);
            this.treeViewAdv.TabIndex = 1;
            this.treeViewAdv.Text = "treeViewAdv1";
            this.treeViewAdv.UseColumns = true;
            // 
            // CmsConflictSolver
            // 
            this.CmsConflictSolver.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSolveAllConflictsWithThisMod});
            this.CmsConflictSolver.Name = "CmsConflictSolver";
            this.CmsConflictSolver.Size = new System.Drawing.Size(242, 26);
            this.CmsConflictSolver.Opening += new System.ComponentModel.CancelEventHandler(this.CmsConflictSolver_Opening);
            // 
            // tsmiSolveAllConflictsWithThisMod
            // 
            this.tsmiSolveAllConflictsWithThisMod.Name = "tsmiSolveAllConflictsWithThisMod";
            this.tsmiSolveAllConflictsWithThisMod.Size = new System.Drawing.Size(241, 22);
            this.tsmiSolveAllConflictsWithThisMod.Text = "Solve all conflicts with this mod";
            this.tsmiSolveAllConflictsWithThisMod.ToolTipText = "Solve all conflicts with this mod.\r\nAll remaining conflicts related to this mod w" +
    "ill be fixed with this mod.\r\n(This mod will keep its file destinations).\r\n";
            this.tsmiSolveAllConflictsWithThisMod.Click += new System.EventHandler(this.tsmiSolveAllConflictsWithThisMod_Click);
            // 
            // btnSolve
            // 
            this.btnSolve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSolve.Location = new System.Drawing.Point(736, 401);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(105, 23);
            this.btnSolve.TabIndex = 2;
            this.btnSolve.Text = "Solve conflicts";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(847, 401);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblConflictSolverDesc
            // 
            this.lblConflictSolverDesc.AutoSize = true;
            this.lblConflictSolverDesc.Location = new System.Drawing.Point(21, 9);
            this.lblConflictSolverDesc.Name = "lblConflictSolverDesc";
            this.lblConflictSolverDesc.Size = new System.Drawing.Size(458, 52);
            this.lblConflictSolverDesc.TabIndex = 0;
            this.lblConflictSolverDesc.Text = resources.GetString("lblConflictSolverDesc.Text");
            // 
            // frmConflictSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 436);
            this.Controls.Add(this.lblConflictSolverDesc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.treeViewAdv);
            this.MinimizeBox = false;
            this.Name = "frmConflictSolver";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Conflict Solver";
            this.Load += new System.EventHandler(this.frmConflictSolver_Load);
            this.CmsConflictSolver.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Utils.Controls.Aga.Controls.Tree.TreeViewAdv treeViewAdv;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblConflictSolverDesc;
        private System.Windows.Forms.ContextMenuStrip CmsConflictSolver;
        private System.Windows.Forms.ToolStripMenuItem tsmiSolveAllConflictsWithThisMod;
    }
}