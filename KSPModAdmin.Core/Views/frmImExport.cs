using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils;

namespace KSPModAdmin.Core.Views
{
    public partial class frmImExport : frmBase
    {
        public const string MODPACK_FILENAME_TEMPLATE = "ModPack_{0}.modpack";


        public frmImExport()
        {
            InitializeComponent();
        }


        #region Event handling

        private void frmImExport_Load(object sender, EventArgs e)
        {
            lblCurrentAction.Text = string.Empty;

            foreach (ModNode mod in ModSelectionController.Mods)
            {
                cbModSelection.Items.Add(mod);
                cbModSelection.CheckBoxItems[cbModSelection.CheckBoxItems.Count - 1].Checked = true;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Export();
        }

        private void rbExportSelectedOnly_CheckedChanged(object sender, EventArgs e)
        {
            cbModSelection.Enabled = rbExportSelectedOnly.Checked;
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
                MessageBox.Show(this, Messages.MSG_NO_MODS_TO_EXPORT, Messages.MSG_TITLE_ATTENTION);
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.InitialDirectory = OptionsController.DownloadPath;
                dlg.FileName = RemoveInvalidCharsFromPath(string.Format(MODPACK_FILENAME_TEMPLATE, DateTime.Now.ToShortDateString()));
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    dlg.FileName = RemoveInvalidCharsFromPath(dlg.FileName);

                    AddMessage(string.Format(Messages.MSG_EXPORT_TO_0, dlg.FileName));
                    new AsyncTask<bool>(() =>
                                        {
                                            ModPackHandler.Export(modsToExport, dlg.FileName, cbIncludeMods.Checked);
                                            return true;
                                        },
                                        (b, ex) =>
                                        {
                                            if (ex != null) 
                                                MessageBox.Show(this, ex.Message, Messages.MSG_TITLE_ERROR);
                                        }).Run();
                    AddMessage(Messages.MSG_EXPORT_DONE);
                    Close();
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
                return (from e in cbModSelection.CheckBoxItems where e.Checked select (ModNode) e.ComboBoxItem).ToList();

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
                new AsyncTask<bool>(() =>
                                    {
                                        if (cbClearModSelection.Checked)
                                        {
                                            AddMessage(Messages.MSG_CLEARING_MODSELECTION);
                                            InvokeIfRequired(() => ModSelectionController.RemoveAllMods());
                                        }
                    //InvokeIfRequired(() => OptionsController.GetValidDownloadPath());
                                        AddMessage(string.Format(Messages.MSG_IMPORTING_FROM_0, dlg.FileName));
                                        ModPackHandler.Import(dlg.FileName, OptionsController.DownloadPath, cbExtract.Checked,
                                                              cbDownloadIfNeeded.Checked, rbCopyDestination.Checked, rbAddOnly.Checked,
                                                              (o, msg) => AddMessage(msg));
                                        return true;
                                    },
                                    (b, ex) =>
                                    {
                                        if (ex != null)
                                        {
                                            AddMessage(Messages.MSG_IMPORTING_FAILED); 
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

        private void AddMessage(string msg)
        {
            InvokeIfRequired(() => lblCurrentAction.Text = msg);
            Messenger.AddInfo(msg);
        }
    }
}
