using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils;

namespace KSPModAdmin.Core.Views
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class frmImExport : frmBase
    {
        /// <summary>
        /// String constant for the ModPack file name template.
        /// </summary>
        public const string MODPACK_FILENAME_TEMPLATE = "ModPack_{0}.modpack";


        /// <summary>
        /// Creates a new instance of the frmImExport class.
        /// </summary>
        public frmImExport()
        {
            InitializeComponent();
        }


        #region Event handling

        private void frmImExport_Load(object sender, EventArgs e)
        {
            lblCurrentAction.Text = string.Empty;

            tvImportExportModSelection.Nodes.Clear();
            foreach (ModNode mod in ModSelectionController.Mods)
                tvImportExportModSelection.Nodes.Add(new TreeNode(mod.Name) { Tag = mod, Checked = true });
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Export();
        }

        private void rbExportSelectedOnly_CheckedChanged(object sender, EventArgs e)
        {
            tvImportExportModSelection.Enabled = rbExportSelectedOnly.Checked;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Import();
        }

        private void cbDownloadIfNeeded_CheckedChanged(object sender, EventArgs e)
        {
            rbInstall.Enabled = (cbExtract.Checked) && (cbDownloadIfNeeded.Checked);
            rbAddOnly.Enabled = rbInstall.Enabled;
            btnImport.Enabled = rbInstall.Enabled;
        }

        private void cbExtract_CheckedChanged(object sender, EventArgs e)
        {
            rbInstall.Enabled = (cbExtract.Checked) && (cbDownloadIfNeeded.Checked);
            rbAddOnly.Enabled = rbInstall.Enabled;
            btnImport.Enabled = rbInstall.Enabled;
        }

        #endregion

        #region Export

        private void Export()
        {
            List<ModNode> modsToExport = GetModsToExport();
            if (modsToExport.Count <= 0)
            {
                MessageBox.Show(this, Messages.MSG_NO_MODS_TO_EXPORT, Messages.MSG_TITLE_ATTENTION);
                Messenger.AddInfo(Messages.MSG_NO_MODS_TO_EXPORT);
            }
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.InitialDirectory = OptionsController.DownloadPath;
                dlg.FileName = RemoveInvalidCharsFromPath(string.Format(MODPACK_FILENAME_TEMPLATE, DateTime.Now.ToShortDateString()));
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    pbExport.Visible = true;
                    AddMessage(string.Format(Messages.MSG_EXPORT_TO_0, dlg.FileName));
                    AsyncTask<bool>.DoWork(() =>
                        {
                            ModPackHandler.MessageCallbackFunction = AddMessage;
                            ModPackHandler.Export(modsToExport, dlg.FileName, cbIncludeMods.Checked);
                            ModPackHandler.MessageCallbackFunction = null;
                            return true;
                        },
                        (b, ex) =>
                        {
                            pbExport.Visible = false;
                            if (ex != null)
                            {
                                AddMessage(string.Format(Messages.MSG_ERROR_EXPORT_FAILED_0, ex.Message), true, ex);
                                MessageBox.Show(this, string.Format(Messages.MSG_ERROR_EXPORT_FAILED_0, ex.Message), Messages.MSG_TITLE_ERROR);
                            }
                            else
                            {
                                AddMessage(Messages.MSG_EXPORT_DONE);
                                Close();
                            }
                        });
                }
                else
                    AddMessage(Messages.MSG_EXPORT_ABORTED);
            }
        }

        private string RemoveInvalidCharsFromPath(string path)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                if (path.Contains(c))
                    path = path.Replace(c, '_');
            }

            return path;
        }

        private List<ModNode> GetModsToExport()
        {
            if (rbExportAll.Checked)
                return ModSelectionController.Mods.ToList();
            else if (rbExportSelectedOnly.Checked)
            {
                var result = (from e in tvImportExportModSelection.Nodes.Cast<TreeNode>()
                              where e.Tag as ModNode != null && e.Checked
                              select e.Tag as ModNode).ToList();
                return result;
            }

            return new List<ModNode>();
        }

        #endregion

        #region Import

        private void Import()
        {
            AddMessage(Messages.MSG_IMPORT_STARTED);
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = OptionsController.DownloadPath;
            dlg.Filter = Constants.MODPACK_FILTER;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pbImport.Visible = true;
                new AsyncTask<bool>(() =>
                    {
                        if (cbClearModSelection.Checked)
                        {
                            AddMessage(Messages.MSG_CLEARING_MODSELECTION);
                            InvokeIfRequired(() => ModSelectionController.RemoveAllMods());
                        }

                        AddMessage(string.Format(Messages.MSG_IMPORTING_FROM_0, dlg.FileName));
                        ModPackHandler.MessageCallbackFunction = AddMessage;
                        ModPackHandler.Import(dlg.FileName, OptionsController.DownloadPath, cbExtract.Checked, cbDownloadIfNeeded.Checked, rbCopyDestination.Checked, rbAddOnly.Checked);
                        ModPackHandler.MessageCallbackFunction = null;
                        return true;
                    },
                    (b, ex) =>
                    {
                        pbImport.Visible = false;
                        if (ex != null)
                        {
                            AddMessage(Messages.MSG_IMPORTING_FAILED, true, ex); 
                            MessageBox.Show(this, ex.Message, Messages.MSG_TITLE_ERROR);
                        }
                        else
                        {
                            AddMessage(Messages.MSG_IMPORTING_DONE);
                            Close();
                        }
                    }).Run();
            }
            else
                AddMessage(Messages.MSG_IMPORTING_ABORTED);
        }

        #endregion

        private void AddMessage(string msg, bool error = false, Exception ex = null)
        {
            _AddMessage(msg);
        }

        private void AddMessage(object sender, string msg)
        {
            _AddMessage(msg);
        }

        private void _AddMessage(string msg, bool error = false, Exception ex = null)
        {
            InvokeIfRequired(() => lblCurrentAction.Text = msg);
            if (!error)
                Messenger.AddInfo(msg);
            else if (ex == null)
                Messenger.AddError(msg);
            else 
                Messenger.AddError(msg, ex);
        }
    }
}
