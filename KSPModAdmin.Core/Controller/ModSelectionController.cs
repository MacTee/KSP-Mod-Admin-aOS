using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using FolderSelect;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.Helper;
using KSPModAdmin.Core.Views;
using SharpCompress.Archive;

namespace KSPModAdmin.Core.Controller
{
    /// <summary>
    /// Controller for the ucModSelection.
    /// </summary>
    public class ModSelectionController
    {
        #region Member variables

        /// <summary>
        /// List of known mods.
        /// </summary>
        private static ModSelectionTreeModel mModel = new ModSelectionTreeModel();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton of this class.
        /// </summary>
        protected static ModSelectionController Instance { get { return mInstance ?? (mInstance = new ModSelectionController()); } }
        private static ModSelectionController mInstance = null;

        /// <summary>
        /// Gets or sets the view of the controller.
        /// </summary>
        public static ucModSelection View { get; protected set; }

        /// <summary>
        /// Gets the list of all known mods of the ModSelection.
        /// </summary>
        public static ModSelectionTreeModel Model
        {
            get { return mModel; }
        }

        /// <summary>
        /// Gets all mods of the ModSelection.
        /// </summary>
        public static ModNode[] Mods { get { return Model.Nodes.Cast<ModNode>().ToArray(); } }

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor (use static function only).
        /// </summary>
        private ModSelectionController()
        {
        }

        /// <summary>
        /// Static constructor. Creates a singleton of this class.
        /// </summary>
        static ModSelectionController()
        {
            if (mInstance == null)
                mInstance = new ModSelectionController();
        }

        #endregion


        /// <summary>
        /// This method gets called when your Controller should be initialized.
        /// Perform additional initialization of your UserControl here.
        /// </summary>
        internal static void Initialize(ucModSelection view)
        {
            View = view;

            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;
            EventDistributor.LanguageChanged += LanguageChanged;
            EventDistributor.KSPRootChanged += KSPRootChanged;

            ModSelectionTreeModel.BeforeCheckedChange += BeforeCheckedChange;

            View.AddActionKey(VirtualKey.VK_DELETE, DeleteMod);
            View.AddActionKey(VirtualKey.VK_BACK, DeleteMod);
        }


        #region Event callback functions

        #region EventDistributor callback functions.

        /// <summary>
        /// Callback function for the AsyncTaskStarted event.
        /// Should disable all controls of the BaseView.
        /// </summary>
        protected static void AsyncTaskStarted(object sender)
        {
            View.SetEnabledOfAllControls(false);
        }

        /// <summary>
        /// Callback function for the AsyncTaskDone event.
        /// Should enable all controls of the BaseView.
        /// </summary>
        protected static void AsyncTaskDone(object sender)
        {
            View.SetEnabledOfAllControls(true);
        }

        /// <summary>
        /// Callback function for the LanguageChanged event.
        /// Translates all controls of the BaseView.
        /// </summary>
        protected static void LanguageChanged(object sender)
        {
            View.LanguageChanged();
        }

        /// <summary>
        /// Callback of the OptionsController for known KSP install path changes.
        /// </summary>
        /// <param name="kspPath">The new KSP install paths.</param>
        protected static void KSPRootChanged(string kspPath)
        {
            // MainController loads the new KSPMAMod.cfg and populates the TreeView.
            ////View.tvModSelection.SelectedNode = null;
        }

        #endregion

        /// <summary>
        /// Callback of the ModSelectionTreeModel when a checked state of a ModNode is changing.
        /// </summary>
        /// <param name="sender">Invoker of the BeforeCheckedChange event.</param>
        /// <param name="args">The BeforeCheckedChangeEventArgs.</param>
        protected static void BeforeCheckedChange(object sender, BeforeCheckedChangeEventArgs args)
        {
            var argNode = args.Node as ModNode;
            if (argNode == null)
                return;

            if (!argNode.ZipExists)
            {
                if (!args.NewValue)
                    args.Cancel = (DialogResult.Yes != MessageBox.Show(View.ParentForm, Messages.MSG_UNCHECK_NO_ZIPARCHIVE_WARNING, Messages.MSG_TITLE_ATTENTION, MessageBoxButtons.YesNo));
                else
                {
                    if (argNode.IsInstalled)
                        return;

                    MessageBox.Show(View.ParentForm, Messages.MSG_CHECK_NO_ZIPARCHIVE_WARNING, Messages.MSG_TITLE_ATTENTION);
                    args.Cancel = true;
                }
            }
            else if (args.NewValue)
            {
                if (!argNode.HasDestination || argNode.HasChildesWithoutDestination)
                {
                    string msg = string.Format(Messages.MSG_0_HAS_CHILDES_WITHOUT_DESTINATION_WARNING, argNode.Name);
                    MessageBox.Show(View.ParentForm, msg, Messages.MSG_TITLE_ATTENTION);
                    if (argNode.IsFile || (!argNode.IsFile && !argNode.HasDestinationForChilds))
                        args.NewValue = false;
                }
            }
        }

        /// <summary>
        /// ActionKey callback function to handle Delete and Back key.
        /// Deletes the selected mod(s).
        /// </summary>
        /// <returns>True, cause we have handled the key.</returns>
        protected static bool DeleteMod(ActionKeyInfo keyState)
        {
            RemoveMod(View.SelectedMods.ToArray());
            return true;
        }

        #endregion


        /// <summary>
        /// Forces the view to redraw.
        /// </summary>
        public static void InvalidateView()
        {
            View.InvalidateView();
        }

        #region Add Mod

        /// <summary>
        /// Opens the add dialog to add mods via CurseForge, KSP Forum or path.
        /// </summary>
        public static void OpenAddModDialog()
        {
            frmAddMod dlg = new frmAddMod();
            dlg.ShowDialog();
            View.InvokeIfRequired(() => { InvalidateView(); });
        }

        /// <summary>
        /// Opens a OpenFileDialog to add mods path.
        /// </summary>
        public static void OpenAddModFileDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog { Multiselect = true, Filter = Constants.ADD_DLG_FILTER };
            if (dlg.ShowDialog() == DialogResult.OK)
                AddModsAsync(dlg.FileNames);
            else
                Messenger.AddInfo(Messages.MSG_ADDING_MOD_FAILED);
        }

        /// <summary>
        /// Adds a mod from HD with given ModInfos.
        /// </summary>
        /// <param name="modInfo">The ModInfos of the mod to add.</param>
        /// <param name="installAfterAdd">Flag that determines if the mod should be installed after adding to the ModSelection.</param>
        /// <returns>The new added mod (maybe null).</returns>
        public static ModNode HandleModAddViaModInfo(ModInfo modInfo, bool installAfterAdd)
        {
            ModNode newMod = null;
            List<ModNode> addedMods = AddMods(new ModInfo[] { modInfo }, true, null);
            if (addedMods.Count > 0 && !string.IsNullOrEmpty(modInfo.Name))
                addedMods[0].Text = modInfo.Name;

            if (installAfterAdd)
                ProcessMods(addedMods.ToArray());

            if (addedMods.Count > 0)
                newMod = addedMods[0];

            return newMod;
        }

        /// <summary>
        /// Adds a mod from HD.
        /// </summary>
        /// <param name="modPath">Path to the mod.</param>
        /// <param name="modName">Name of the mod (leave blank for auto fill).</param>
        /// <param name="installAfterAdd">Flag that determines if the mod should be installed after adding to the ModSelection.</param>
        /// <returns>The new added mod (maybe null).</returns>
        public static ModNode HandleModAddViaPath(string modPath, string modName, bool installAfterAdd)
        {
            return HandleModAddViaModInfo(new ModInfo { LocalPath = modPath, Name = string.IsNullOrEmpty(modName) ? Path.GetFileNameWithoutExtension(modPath) : modName }, installAfterAdd);
        }

        /// <summary>
        /// Adds the ModNodes to the mod selection tree.
        /// </summary>
        /// <param name="modNode">The nodes to add.</param>
        /// <returns>List of added mods.</returns>
        internal static List<ModNode> AddMods(ModNode[] modNode, bool showCollisionDialog = true)
        {
            List<ModNode> addedMods = new List<ModNode>();

            foreach (ModNode node in modNode)
            {
                try
                {
                    Model.AddMod(node);
                    ModNodeHandler.SetToolTips(node);
                    ////ModNodeHandler.CheckNodesWithDestination(node);
                    addedMods.Add(node);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(View, ex.Message, Messages.MSG_TITLE_ERROR);
                    Messenger.AddError(string.Format(Messages.MSG_ADD_MOD_FAILED_0, node.Name), ex);
                }
            }

            InvalidateView();

            return addedMods;
        }

        /// <summary>
        /// Adds a MOD to the TreeView.
        /// </summary>
        /// <param name="fileNames">Paths to the Zip-Files of the KSP mods.</param>
        /// <param name="showCollisionDialog">Flag to show/hide the collision dialog.</param>
        internal static void AddModsAsync(string[] fileNames, bool showCollisionDialog = true)
        {
            if (fileNames.Length > 0)
            {
                ModInfo[] modInfos = new ModInfo[fileNames.Length];
                for (int i = 0; i < fileNames.Length; ++i)
                    modInfos[i] = new ModInfo { LocalPath = fileNames[i], Name = Path.GetFileNameWithoutExtension(fileNames[i]) };

                AddModsAsync(modInfos, showCollisionDialog);
            }
            else
            {
                Messenger.AddError(Messages.MSG_ADD_MODS_FAILED_PARAM_EMPTY_FILENAMES);
            }
        }

        /// <summary>
        /// Creates nodes from the ModInfos and adds the nodes to the ModSelection.
        /// </summary>
        /// <param name="modInfos">The nodes to add.</param>
        internal static void AddModsAsync(ModInfo[] modInfos, bool showCollisionDialog = true)
        {
            if (modInfos.Length <= 0)
            {
                Messenger.AddError(Messages.MSG_ADD_MODS_FAILED_PARAM_EMPTY_MODINFOS);
                return;
            }

            EventDistributor.InvokeAsyncTaskStarted(Instance);
            View.SetEnabledOfAllControls(false);
            View.SetProgressBarStates(true, modInfos.Length, 0);

            AsyncTask<List<ModNode>> asnyJob = new AsyncTask<List<ModNode>>();
            asnyJob.SetCallbackFunctions(() =>
            {
                return AddMods(modInfos, showCollisionDialog, asnyJob);
            },
                (result, ex) =>
                {
                    EventDistributor.InvokeAsyncTaskDone(Instance);
                    View.SetEnabledOfAllControls(true);
                    View.SetProgressBarStates(false);
                },
                (percentage) =>
                {
                    View.SetProgressBarStates(true, modInfos.Length, percentage);
                });
            asnyJob.Run();
        }

        /// <summary>
        /// Creates nodes from the ModInfos and adds the nodes to the ModSelection.
        /// </summary>
        /// <param name="modInfos">The nodes to add.</param>
        /// <returns>List of added mods.</returns>
        internal static List<ModNode> AddMods(ModInfo[] modInfos, bool showCollisionDialog, AsyncTask<List<ModNode>> asyncJob = null)
        {
            int doneCount = 0;
            List<ModNode> addedMods = new List<ModNode>();

            foreach (ModInfo modInfo in modInfos)
            {
                Messenger.AddInfo(Constants.SEPARATOR);
                Messenger.AddInfo(string.Format(Messages.MSG_START_ADDING_0, modInfo.Name));
                Messenger.AddInfo(Constants.SEPARATOR);

                try
                {
                    // already added?
                    ModNode newNode = null;
                    ModNode mod = (string.IsNullOrEmpty(modInfo.ProductID)) ? null : Model[modInfo.ProductID, modInfo.SiteHandlerName];
                    if (mod == null && !Model.ContainsLocalPath(modInfo.LocalPath))
                    {
                        try
                        {
                            if (modInfo.LocalPath.EndsWith(Constants.EXT_CRAFT, StringComparison.CurrentCultureIgnoreCase) && File.Exists(modInfo.LocalPath))
                                modInfo.LocalPath = ModZipCreator.CreateZipOfCraftFile(modInfo.LocalPath);

                            newNode = ModNodeHandler.CreateModNode(modInfo);
                            if (newNode != null)
                            {
                                Model.AddMod(newNode);
                                Messenger.AddInfo(string.Format(Messages.MSG_MOD_ADDED_0, newNode.Text));
                            }
                        }
                        catch (Exception ex)
                        {
                            Messenger.AddError(string.Format(Messages.MSG_MOD_ERROR_WHILE_READ_ZIP_0_ERROR_MSG_1, string.Empty, ex.Message), ex);
                        }
                    }
                    else if (mod != null && (mod.IsOutdated || modInfo.CreationDateAsDateTime > mod.CreationDateAsDateTime) &&
                             OptionsController.ModUpdateBehavior != ModUpdateBehavior.Manualy)
                    {
                        newNode = UpdateMod(modInfo, mod);
                    }
                    else
                    {
                        View.InvokeIfRequired(() =>
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine(string.Format(Messages.MSG_MOD_ALREADY_ADDED, modInfo.Name));
                            sb.AppendLine();
                            sb.AppendLine(Messages.MSG_SHOULD_MOD_REPLACED);
                            if (MessageBox.Show(View, sb.ToString(), Messages.MSG_TITLE_ATTENTION, MessageBoxButtons.YesNo) ==
                                DialogResult.Yes)
                            {
                                ModNode outdatedMod = Model[modInfo.LocalPath];
                                Messenger.AddInfo(string.Format(Messages.MSG_REPLACING_MOD_0, outdatedMod.Text));

                                newNode = UpdateMod(modInfo, outdatedMod);
                                Messenger.AddInfo(string.Format(Messages.MSG_MOD_0_REPLACED, newNode.Text));
                            }
                        });
                    }

                    if (newNode != null)
                        addedMods.Add(newNode);

                    newNode = null;

                    if (asyncJob != null)
                        asyncJob.PercentFinished = doneCount += 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(View, ex.Message, Messages.MSG_TITLE_ERROR);
                    Messenger.AddError(string.Format(Messages.MSG_ADD_MOD_FAILED_0, modInfo.Name), ex);
                }

                InvalidateView();

                Messenger.AddInfo(Constants.SEPARATOR);
            }

            return addedMods;
        }

        #endregion

        #region Processing mods

        /// <summary>
        /// Processes all nodes of the ModSelection. (Adds/Removes the mods to/from the KSP install folders).
        /// </summary>
        public static void ProcessAllModsAsync(bool silent = false)
        {
            ProcessModsAsync(Mods, silent);
        }

        /// <summary>
        /// Processes all passed nodes. (Adds/Removes the MOD to/from the KSP install folders).
        /// </summary>
        /// <param name="nodeArray">The NodeArray to process.</param>
        /// <param name="silent">Determines if info messages should be added displayed.</param>
        public static void ProcessMods(ModNode[] nodeArray, bool silent = false)
        {
            try
            {
                int maxNodeCount = 0;
                int nodeCount = 0;
                foreach (ModNode mod in nodeArray)
                {
                    maxNodeCount += ModSelectionTreeModel.GetFullNodeCount(new ModNode[] { mod });
                    nodeCount += ModNodeHandler.ProcessMod(mod, silent, View.OverrideModFiles, (i) => { View.SetProgressBarStates(true, maxNodeCount, i); }, nodeCount) - nodeCount;
                }
            }
            catch (Exception ex)
            {
                Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_PROCESSING_MOD_0, ex), ex);
            }
            finally
            {
                View.SetProgressBarStates(false);
                InvalidateView();
            }
        }

        /// <summary>
        /// Processes all passed nodes. (Adds/Removes the MOD to/from the KSP install folders).
        /// Calls SolveConflicts if there are any conflicts.
        /// </summary>
        /// <param name="nodeArray">The NodeArray to process.</param>
        /// <param name="silent">Determines if info messages should be added displayed.</param>
        public static void ProcessModsAsync(ModNode[] nodeArray, bool silent = false)
        {
            if (ModRegister.HasConflicts)
            {
                if (!OpenConflictSolver())
                {
                    MessageBox.Show(View.ParentForm, Messages.MSG_PROCESSING_ABORDED_CONFLICTS_DETECTED, Messages.MSG_TITLE_CONFLICTS_DETECTED);
                    Messenger.AddInfo(Messages.MSG_PROCESSING_ABORDED_CONFLICTS_DETECTED);
                    return;
                }
            }

            EventDistributor.InvokeAsyncTaskStarted(Instance);
            View.SetEnabledOfAllControls(false);
            View.SetProgressBarStates(true, 1, 0);

            AsyncTask<bool> asyncJob = new AsyncTask<bool>();
            asyncJob.SetCallbackFunctions(() =>
            {
                ProcessMods(nodeArray, silent);

                return true;
            },
                (result, ex) =>
                {
                    EventDistributor.InvokeAsyncTaskDone(Instance);
                    View.SetEnabledOfAllControls(true);
                    View.SetProgressBarStates(false);

                    if (ex != null)
                    {
                        string msg = string.Format(Messages.MSG_ERROR_DURING_PROCESSING_MOD_0, ex.Message);
                        Messenger.AddError(msg, ex);
                        MessageBox.Show(View, msg, Messages.MSG_TITLE_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
            asyncJob.Run();
        }

        #endregion

        #region Removing Mod

        /// <summary>
        /// Clears the mod selection tree.
        /// </summary>
        public static void ClearMods()
        {
            Model.Nodes.Clear();
            ModRegister.Clear();
        }

        /// <summary>
        /// Uninstalls and removes the mods from the ModSelection.
        /// </summary>
        /// <param name="modsToRemove">The mods to remove.</param>
        public static void RemoveMod(ModNode[] modsToRemove)
        {
            if (modsToRemove == null || modsToRemove.Length == 0)
                return;

            List<ModNode> mods = new List<ModNode>();
            foreach (var mod in modsToRemove)
            {
                ModNode root = mod.ZipRoot;
                if (!mods.Contains(root))
                    mods.Add(root);
            }

            string msg = string.Empty;
            if (modsToRemove.Count() == 1)
                msg = string.Format(Messages.MSG_DELETE_MOD_0_QUESTION, mods[0].ZipRoot);
            else
                msg = string.Format(Messages.MSG_DELETE_MODS_0_QUESTION, Environment.NewLine + string.Join<ModNode>(Environment.NewLine, mods));

            if (DialogResult.Yes == MessageBox.Show(View.ParentForm, msg, Messages.MSG_TITLE_ATTENTION, MessageBoxButtons.YesNo))
                RemoveModsAsync(mods.ToArray(), false);
        }

        /// <summary>
        /// Uninstalls and removes the mods from the ModSelection in silent mode.
        /// </summary>
        /// <param name="modsToRemove">The mods to remove.</param>
        public static void RemoveModSilent(ModNode[] modsToRemove)
        {
            if (modsToRemove == null || modsToRemove.Length == 0)
                return;

            List<ModNode> mods = new List<ModNode>();
            foreach (var mod in modsToRemove)
            {
                ModNode root = mod.ZipRoot;
                if (!mods.Contains(root))
                    mods.Add(root);
            }

            RemoveModsAsync(mods.ToArray(), true);
        }

        /// <summary>
        /// Uninstalls and removes all mods in the ModSelection.
        /// </summary>
        public static void RemoveAllMods()
        {
            if (DialogResult.Yes == MessageBox.Show(View.ParentForm, Messages.MSG_DELETE_ALL_MODS_QUESTION, Messages.MSG_TITLE_ATTENTION, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2))
                RemoveModsAsync(Mods, false);
        }

        /// <summary>
        /// Uninstalls and removes all mods in the ModSelection in silent mode.
        /// </summary>
        public static void RemoveAllModsSilent()
        {
            RemoveModsAsync(Mods, true);
        }

        /// <summary>
        /// Uninstalls and removes the mods from the ModSelection.
        /// </summary>
        /// <param name="modsToRemove">The mods to remove.</param>
        protected static void RemoveModsAsync(ModNode[] modsToRemove, bool silent = false)
        {
            EventDistributor.InvokeAsyncTaskStarted(Instance);
            View.SetEnabledOfAllControls(false);
            View.SetProgressBarStates(true, modsToRemove.Length, 0);

            int doneCount = 0;
            AsyncTask<bool> asyncJob = new AsyncTask<bool>();
            asyncJob.SetCallbackFunctions(
                () =>
                {
                    foreach (ModNode mod in modsToRemove)
                    {
                        if (!silent)
                        {
                            Messenger.AddInfo(Constants.SEPARATOR);
                            Messenger.AddInfo(string.Format(Messages.MSG_REMOVING_MOD_0, mod.Name));
                            Messenger.AddInfo(Constants.SEPARATOR);
                        }

                        ModNode modToRemove = mod.ZipRoot;

                        try
                        {
                            // prepare to uninstall all mods
                            modToRemove.UncheckAll();

                            // uninstall all mods
                            ProcessMods(new ModNode[] { modToRemove }, silent);
                            ModRegister.RemoveRegisteredMod(modToRemove);

                            View.InvokeIfRequired(() => { Model.RemoveMod(modToRemove); });
                        }
                        catch (Exception ex)
                        {
                            Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_REMOVING_MOD_0, modToRemove.Name), ex);
                        }

                        asyncJob.PercentFinished = doneCount++;

                        Messenger.AddInfo(string.Format(Messages.MSG_MOD_RMODVED_0, modToRemove.Name));

                        if (!silent)
                            Messenger.AddInfo(Constants.SEPARATOR);
                    }

                    return true;
                },
                (result, ex) =>
                {
                    EventDistributor.InvokeAsyncTaskDone(Instance);
                    View.SetEnabledOfAllControls(true);
                    View.SetProgressBarStates(false);
                    View.ResetSelectedNode();

                    InvalidateView();

                    MainController.SaveKSPConfig();
                },
                (percentage) => { View.SetProgressBarStates(true, modsToRemove.Length, percentage); });
            asyncJob.Run();
        }

        /// <summary>
        /// Removes the outdated mode from disk and ModSelection and adds the new mod to the ModSelection.
        /// </summary>
        /// <param name="outdatedMod">The mod to remove from ModSelection and disk.</param>
        /// <param name="newMod">The new mod to add to the ModSelection.</param>
        private static void RemoveOutdatedAndAddNewMod(ModNode outdatedMod, ModNode newMod)
        {
            Messenger.AddInfo(string.Format(Messages.MSG_REMOVING_OUTDATED_MOD_0, outdatedMod.Text));
            ModRegister.RemoveRegisteredMod(outdatedMod);
            outdatedMod.UncheckAll();
            ProcessMods(new ModNode[] { outdatedMod }, true);
            View.InvokeIfRequired(() => Model.RemoveMod(outdatedMod));

            Messenger.AddInfo(string.Format(Messages.MSG_ADDING_UPDATED_MOD_0, newMod.Text));
            if (Model.AddMod(newMod) != null && OptionsController.DeleteOldArchivesAfterUpdate && File.Exists(outdatedMod.LocalPath))
                File.Delete(outdatedMod.LocalPath);
        }

        #endregion

        #region Edit/Copy ModInfos

        /// <summary>
        /// Opens the edit ModInfo dialog.
        /// </summary>
        public static void EditModInfos(ModNode modNode)
        {
            ModNode root = modNode.ZipRoot;
            frmEditModInfo dlg = new frmEditModInfo();
            dlg.ModZipRoot = root;
            if (dlg.ShowDialog(View.ParentForm) == DialogResult.OK)
            {
                if (!root.IsInstalled)
                    root.Text = dlg.ModName;

                root.AddDate = dlg.DownloadDate;
                root.Author = dlg.Author;
                root.CreationDate = dlg.CreationDate;
                root.ChangeDate = dlg.ChangeDate;
                root.Downloads = dlg.Downloads;
                root.Note = dlg.Note;
                root.ProductID = dlg.ProductID;
                root.SiteHandlerName = dlg.SiteHandlerName;
                root.ModURL = dlg.ModURL;
                root.AdditionalURL = dlg.AdditionalURL;
                root.Version = dlg.Version;
                root.KSPVersion = dlg.KSPVersion;
                root.IsOutdated = false;

                InvalidateView();
            }
        }

        /// <summary>
        /// Opens the copy ModInfo dialog.
        /// </summary>
        public static void CopyModInfos(ModNode modNode)
        {
            frmCopyModInfo dlg = new frmCopyModInfo();
            dlg.SourceMod = modNode.ZipRoot;
            dlg.Mods = Mods;
            dlg.ShowDialog(View.ParentForm);

            InvalidateView();
        }

        #endregion

        #region Destination

        /// <summary>
        /// Displays a dialog to select a new destination for the mod node.
        /// </summary>
        /// <param name="modNode">The mod node to change the destination.</param>
        public static void ChangeDestination(ModNode modNode)
        {
            if (modNode == null)
                return;

            if (modNode.IsInstalled || modNode.HasInstalledChilds)
                MessageBox.Show(View.ParentForm, Messages.MSG_FOLDER_INSTALLED_UNINSTALL_IT_TO_CHANGE_DESTINATION, Messages.MSG_TITLE_ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                SelectDestinationFolder(modNode);
                InvalidateView();
            }
        }

        /// <summary>
        /// Resets the destination of the modNode and all its childs.
        /// </summary>
        /// <param name="modNode">The mod node to reset the destination.</param>
        public static void ResetDestination(ModNode modNode)
        {
            if (modNode == null)
                return;

            if (modNode.IsInstalled || modNode.HasInstalledChilds)
                MessageBox.Show(View.ParentForm, Messages.MSG_FOLDER_INSTALLED_UNINSTALL_IT_TO_CHANGE_DESTINATION, Messages.MSG_TITLE_ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                ModNodeHandler.SetDestinationPaths(modNode, string.Empty);
                modNode.SetChecked(false);
                modNode.CheckAllChildes(false);
                InvalidateView();
            }
        }

        /// <summary>
        /// Displays a dialog to select a source and destination folder.
        /// </summary>
        /// <param name="node">The root node of the archive file.</param>
        /// <returns>True if dialog was quit with DialogResult.OK</returns>
        private static bool SelectDestinationFolder(ModNode node)
        {
            if (node == null)
                return false;

            string dest = node.Destination.Replace(Constants.KSPFOLDERTAG, string.Empty);
            if (dest.StartsWith(Path.DirectorySeparatorChar.ToString()))
                dest = dest.Substring(1);
            int index = dest.IndexOf(Path.DirectorySeparatorChar);
            if (index > -1)
                dest = dest.Substring(0, index);

            frmDestFolderSelection dlg = new frmDestFolderSelection();
            dlg.DestFolders = GetDefaultDestPaths();
            dlg.DestFolder = dest;
            dlg.SrcFolders = new ModNode[] { node };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string src = dlg.SrcFolder;
                if (dlg.SrcFolder.Contains(Path.DirectorySeparatorChar))
                    src = Path.GetFileName(dlg.SrcFolder);

                ModNode srcNode = ModSelectionTreeModel.SearchNode(src, node);
                if (srcNode == null)
                    srcNode = ModSelectionTreeModel.SearchNode(Path.GetFileNameWithoutExtension(src), node);

                if (srcNode != null)
                {
                    ModNodeHandler.SetDestinationPaths(srcNode, dlg.DestFolder, dlg.CopyContent);
                    InvalidateView();

                    return true;
                }
                else
                    Messenger.AddInfo(Messages.MSG_SOURCE_NODE_NOT_FOUND);
            }

            return false;
        }

        /// <summary>
        /// Returns a string array of possible destination paths.
        /// </summary>
        /// <returns>A string array of possible destination paths.</returns>
        private static string[] GetDefaultDestPaths()
        {
            List<string> destFolders = new List<string>();
            destFolders.AddRange(Constants.KSPFolders);

            for (int i = 0; i < destFolders.Count<string>(); ++i)
                destFolders[i] = KSPPathHelper.GetPathByName(destFolders[i]);

            return destFolders.ToArray();
        }

        #endregion

        #region Scan GameData

        /// <summary>
        /// Scans the KSP GameData directory for installed mods and adds them to the ModSelection.
        /// </summary>
        internal static void ScanGameData()
        {
            Messenger.AddDebug(Constants.SEPARATOR);
            Messenger.AddDebug(Messages.MSG_SCAN_GAMDATA_FOLDER_STARTED);
            Messenger.AddDebug(Constants.SEPARATOR);
            EventDistributor.InvokeAsyncTaskStarted(Instance);
            View.SetEnabledOfAllControls(false);
            View.ShowBusy = true;

            AsyncTask<bool> asyncJob = new AsyncTask<bool>();
            asyncJob.SetCallbackFunctions(() =>
            {
                string[] ignoreDirs = new string[] { "squad", "myflags", "nasamission" };
                List<ScanInfo> entries = new List<ScanInfo>();
                try
                {
                    string scanDir = KSPPathHelper.GetPath(KSPPaths.GameData);
                    string[] dirs = Directory.GetDirectories(scanDir);
                    foreach (string dir in dirs)
                    {
                        string dirname = dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                        if (!ignoreDirs.Contains(dirname.ToLower()))
                        {
                            Messenger.AddDebug(string.Format(Messages.MSG_DIRECTORY_0_FOUND, dirname));
                            ScanInfo scanInfo = new ScanInfo(dirname, dir, false);
                            entries.Add(scanInfo);
                            ScanDir(scanInfo);
                        }
                    }

                    List<ScanInfo> unknowns = GetUnknowenNodes(entries);
                    if (unknowns.Count > 0)
                    {
                        foreach (ScanInfo unknown in unknowns)
                        {
                            ModNode node = ScanInfoToKSPMA_TreeNode(unknown);
                            RefreshCheckedStateOfMods(new[] { node });
                            Model.Nodes.Add(node);
                            Messenger.AddInfo(string.Format(Messages.MSG_MOD_ADDED_0, node.Text));
                        }
                    }
                    else
                        Messenger.AddInfo(Messages.MSG_SCAN_NO_NEW_MODS_FOUND);
                }
                catch (Exception ex)
                {
                    Messenger.AddError(Messages.MSG_SCAN_ERROR_DURING_SCAN, ex);
                }

                return true;
            },
                (result, ex) =>
                {
                    Messenger.AddDebug(Constants.SEPARATOR);

                    EventDistributor.InvokeAsyncTaskDone(Instance);
                    View.SetEnabledOfAllControls(true);
                    View.ShowBusy = false;
                });
            asyncJob.Run();
        }

        /// <summary>
        /// Scans the passed dir for files and directories and creates a tree of ScanInfos from it.
        /// </summary>
        /// <param name="scanDir">The ScanInfo of the start directory.</param>
        private static void ScanDir(ScanInfo scanDir)
        {
            List<ScanInfo> entries = new List<ScanInfo>();
            foreach (string file in Directory.GetFiles(scanDir.Path))
            {
                Messenger.AddDebug(string.Format(Messages.MSG_FILE_0_FOUND, file));
                string filename = Path.GetFileName(file);
                ScanInfo scanInfo = new ScanInfo(filename, file, true, scanDir);
                scanInfo.Parent = scanDir;
            }

            string[] dirs = Directory.GetDirectories(scanDir.Path);
            foreach (string dir in dirs)
            {
                Messenger.AddDebug(string.Format(Messages.MSG_DIRECTORY_0_FOUND, dir));
                string dirname = dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                ScanInfo scanInfo = new ScanInfo(dirname, dir, false, scanDir);
                ScanDir(scanInfo);
            }
        }

        /// <summary>
        /// Searches the list of ScanInfo trees for unknown nodes.
        /// Searches the complete ModSelection for a matching node.
        /// </summary>
        /// <param name="scanInfos">A list of ScanInfos trees to search.</param>
        /// <returns>A list of scanInfo trees with unknown nodes.</returns>
        private static List<ScanInfo> GetUnknowenNodes(List<ScanInfo> scanInfos)
        {
            List<ScanInfo> entries = new List<ScanInfo>();
            foreach (ScanInfo entry in scanInfos)
            {
                bool found = false;
                foreach (ModNode node in Mods)
                {
                    if (CompareNodes(entry, node))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    entries.Add(entry);
                else
                    Messenger.AddDebug(string.Format(Messages.MSG_SKIPPING_0, entry.Path));
            }
            return entries;
        }

        /// <summary>
        /// Compares the ScanInfo to all known nodes (from parent).
        /// </summary>
        /// <param name="scanInfo">The ScanInfo to compare.</param>
        /// <param name="parent">The start node of the comparison.</param>
        /// <returns>True if a match was found, otherwise false.</returns>
        private static bool CompareNodes(ScanInfo scanInfo, ModNode parent)
        {
            if (scanInfo.Name.Equals(parent.Text, StringComparison.CurrentCultureIgnoreCase))
                return true;

            foreach (ModNode child in parent.Nodes)
            {
                if (child.Text.Equals(scanInfo.Name, StringComparison.CurrentCultureIgnoreCase))
                    return true;

                if (CompareNodes(scanInfo, child))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Creates a TreeNodeMod from the passed ScanInfo.
        /// </summary>
        /// <param name="unknown">The ScanInfo of the unknown node.</param>
        /// <returns>The new created TreeNodeMod.</returns>
        private static ModNode ScanInfoToKSPMA_TreeNode(ScanInfo unknown)
        {
            ModNode node = new ModNode();
            node.Key = unknown.Path;
            node.Name = unknown.Name;
            node.AddDate = DateTime.Now.ToString();
            node.Destination = KSPPathHelper.GetRelativePath(unknown.Path);
            node.NodeType = (unknown.IsFile) ? NodeType.UnknownFileInstalled : NodeType.UnknownFolderInstalled;
            node._Checked = true;

            Messenger.AddDebug(string.Format(Messages.MSG_MODNODE_0_CREATED, node.Key));

            foreach (ScanInfo si in unknown.Childs)
            {
                ModNode child = ScanInfoToKSPMA_TreeNode(si);
                node.Nodes.Add(child);
            }

            return node;
        }

        #endregion

        #region Checked state

        #region Refresh CheckedStated

        /// <summary>
        /// Traversing the complete tree and renews the checked state of all nodes.
        /// </summary>
        public static void RefreshCheckedStateAllModsAsync()
        {
            RefreshCheckedStateOfModsAsync(Mods);
        }

        /// <summary>
        /// Traversing the complete tree and renews the checked state of all nodes.
        /// </summary>
        public static void RefreshCheckedStateAllMods()
        {
            RefreshCheckedStateOfMods(Mods);
        }

        /// <summary>
        /// Traversing the complete tree and renews the checked state of all nodes.
        /// </summary>
        public static void RefreshCheckedStateOfModsAsync(ModNode[] mods)
        {
            EventDistributor.InvokeAsyncTaskStarted(Instance);
            View.SetEnabledOfAllControls(false);

            int maxCount = ModSelectionTreeModel.GetFullNodeCount(mods);
            View.SetProgressBarStates(true, maxCount, 0);

            int count = 0;
            AsyncTask<bool> asyncJob = new AsyncTask<bool>();
            asyncJob.SetCallbackFunctions(
                () =>
                {
                    foreach (ModNode mod in mods)
                    {
                        Messenger.AddDebug(string.Format(Messages.MSG_REFRESHING_CHECKEDSTATE_0, mod.Name));
                        ModNode rootNode = mod.ZipRoot;
                        RefreshCheckedState(rootNode, ref count, asyncJob);
                    }
                    return true;
                },
                (result, ex) =>
                {
                    EventDistributor.InvokeAsyncTaskDone(Instance);
                    View.SetEnabledOfAllControls(true);
                    View.SetProgressBarStates(false);
                    InvalidateView();

                    if (ex != null)
                        Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_REFRESH_CHECKED_STATE_0, ex.Message), ex);
                },
                (processedCount) =>
                {
                    View.SetProgressBarStates(true, maxCount, processedCount);
                });
            asyncJob.Run();
        }

        /// <summary>
        /// Traversing the complete tree and renews the checked state of all nodes.
        /// </summary>
        public static void RefreshCheckedStateOfMods(ModNode[] mods)
        {
            foreach (ModNode mod in mods)
            {
                Messenger.AddDebug(string.Format(Messages.MSG_REFRESHING_CHECKEDSTATE_0, mod.Name));
                int count = 0;
                RefreshCheckedState(mod.ZipRoot, ref count);
            }
            InvalidateView();
        }

        /// <summary>
        /// Traversing the complete tree and renews the checked state of all nodes.
        /// </summary>
        protected static void RefreshCheckedState(ModNode mod, ref int processedCount, AsyncTask<bool> asyncJob = null)
        {
            try
            {
                processedCount += 1;
                if (asyncJob != null)
                    asyncJob.ProgressChanged(null, new ProgressChangedEventArgs(processedCount, null));

                bool isInstalled = false;
                NodeType nodeType = NodeType.UnknownFolder;

                if (!mod.HasDestination)
                {
                    isInstalled = false;
                    nodeType = (mod.IsFile) ? NodeType.UnknownFile : NodeType.UnknownFolder;
                }
                else
                {
                    isInstalled = mod.IsInstalled = mod.IsModNodeInstalled();

                    if (mod.IsFile)
                    {
                        ////value = File.Exists(KSPPathHelper.GetDestinationPath(mod));
                        nodeType = (isInstalled) ? NodeType.UnknownFileInstalled : NodeType.UnknownFile;
                    }
                    else
                    {
                        ////bool isInstalled = Directory.Exists(KSPPathHelper.GetDestinationPath(mod));
                        bool hasInstalledChilds = mod.HasInstalledChilds;

                        bool isKSPDir = false;
                        isKSPDir = KSPPathHelper.IsKSPDir(KSPPathHelper.GetAbsolutePath(mod));

                        if (isKSPDir)
                        {
                            isInstalled = (isInstalled && hasInstalledChilds);
                            nodeType = (isInstalled) ? NodeType.KSPFolderInstalled : NodeType.KSPFolder;
                        }
                        else
                        {
                            isInstalled = (isInstalled || hasInstalledChilds);
                            nodeType = (isInstalled) ? NodeType.UnknownFolderInstalled : NodeType.UnknownFolder;
                        }
                    }
                }

                View.InvokeIfRequired(() =>
                {
                    mod._Checked = isInstalled;
                    mod.NodeType = nodeType;
                });
            }
            catch (Exception ex)
            {
                mod._Checked = false;
                Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_REFRESH_CHECKED_STATE_0, ex.Message), ex);
            }

            foreach (ModNode child in mod.Nodes)
                RefreshCheckedState(child, ref processedCount, asyncJob);
        }

        #endregion

        /// <summary>
        /// Unchecks all Mods from the ModSelection.
        /// </summary>
        public static void UncheckAllMods()
        {
            Messenger.AddDebug(Messages.MSG_UNCHECKING_ALL_MODS);

            EventDistributor.InvokeAsyncTaskStarted(Instance);
            View.SetEnabledOfAllControls(false);

            int maxCount = Mods.Length;
            View.SetProgressBarStates(true, maxCount, 0);

            int count = 0;
            AsyncTask<bool> asyncJob = new AsyncTask<bool>();
            asyncJob.SetCallbackFunctions(
                () =>
                {
                    foreach (ModNode mod in Mods)
                    {
                        Messenger.AddDebug(string.Format(Messages.MSG_UNCHECKING_MOD_0, mod.Name));
                        asyncJob.ProgressChanged(null, new ProgressChangedEventArgs(++count, null));
                        View.InvokeIfRequired(() => { mod.Checked = false; });
                    }

                    return true;
                },
                (result, ex) =>
                {
                    EventDistributor.InvokeAsyncTaskDone(Instance);
                    View.SetEnabledOfAllControls(true);
                    View.SetProgressBarStates(false);

                    InvalidateView();

                    if (ex != null)
                        Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_REFRESH_CHECKED_STATE_0, ex.Message), ex);
                },
                (processedCount) =>
                {
                    View.SetProgressBarStates(true, maxCount, processedCount);
                });
            asyncJob.Run();
        }

        /// <summary>
        /// Checks all Mods from the ModSelection.
        /// </summary>
        public static void CheckAllMods()
        {
            Messenger.AddDebug(Messages.MSG_CHECKING_ALL_MODS);

            EventDistributor.InvokeAsyncTaskStarted(Instance);
            View.SetEnabledOfAllControls(false);

            int maxCount = Mods.Length;
            View.SetProgressBarStates(true, maxCount, 0);

            int count = 0;
            AsyncTask<bool> asyncJob = new AsyncTask<bool>();
            asyncJob.SetCallbackFunctions(
                () =>
                {
                    foreach (ModNode mod in Mods)
                    {
                        Messenger.AddDebug(string.Format(Messages.MSG_CHECKING_MOD_0, mod.Name));
                        asyncJob.ProgressChanged(null, new ProgressChangedEventArgs(++count, null));
                        View.InvokeIfRequired(() => { mod.Checked = true; });
                    }

                    return true;
                },
                (result, ex) =>
                {
                    EventDistributor.InvokeAsyncTaskDone(Instance);
                    View.SetEnabledOfAllControls(true);
                    View.SetProgressBarStates(false);

                    InvalidateView();

                    if (ex != null)
                        Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_REFRESH_CHECKED_STATE_0, ex.Message), ex);
                },
                (processedCount) =>
                {
                    View.SetProgressBarStates(true, maxCount, processedCount);
                });
            asyncJob.Run();
        }

        #endregion

        #region Mod update

        /// <summary>
        /// Checks each mod of the ModSelection for updates.
        /// </summary>
        public static void CheckForUpdatesAllMods()
        {
            _CheckForModUpdates(Mods);
        }

        /// <summary>
        /// Checks each mod of the ModSelection for updates.
        /// </summary>
        public static void CheckForUpdatesAllModsAsync()
        {
            CheckForModUpdatesAsync(Mods);
        }

        /// <summary>
        /// Checks each mod for updates.
        /// </summary>
        /// <param name="mods">Array of mods to check for updates.</param>
        public static void CheckForModUpdates(ModNode[] mods)
        {
            _CheckForModUpdates(mods);
            View.InvalidateView();
        }

        /// <summary>
        /// Checks each mod for updates.
        /// </summary>
        /// <param name="mods">Array of mods to check for updates.</param>
        public static void CheckForModUpdatesAsync(ModNode[] mods)
        {
            EventDistributor.InvokeAsyncTaskStarted(Instance);
            View.SetEnabledOfAllControls(false);
            View.ShowBusy = true;

            AsyncTask<bool>.DoWork(
                () =>
                {
                    _CheckForModUpdates(mods);
                    return true;
                },
                (result, ex) =>
                {
                    EventDistributor.InvokeAsyncTaskDone(Instance);
                    View.SetEnabledOfAllControls(true);
                    View.ShowBusy = false;

                    if (ex != null)
                        Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_MOD_UPDATE_0, ex.Message), ex);

                    View.InvalidateView();
                });
        }

        /// <summary>
        /// Checks each mod for updates.
        /// </summary>
        /// <param name="mods">Array of mods to check for updates.</param>
        protected static void _CheckForModUpdates(ModNode[] mods)
        {
            foreach (ModNode mod in mods)
            {
                try
                {
                    ISiteHandler siteHandler = mod.SiteHandler;
                    if (siteHandler == null)
                        Messenger.AddInfo(string.Format(Messages.MSG_ERROR_0_NO_VERSIONCONTROL, mod.Name));
                    else
                    {
                        ModInfo newModinfo = null;
                        Messenger.AddInfo(string.Format(Messages.MSG_UPDATECHECK_FOR_MOD_0_VIA_1, mod.Name, mod.SiteHandlerName));
                        if (!siteHandler.CheckForUpdates(mod.ModInfo, ref newModinfo))
                        {
                            Messenger.AddInfo(string.Format(Messages.MSG_MOD_0_IS_UPTODATE, mod.Name));
                            mod.IsOutdated = false;
                        }
                        else
                        {
                            Messenger.AddInfo(string.Format(Messages.MSG_MOD_0_IS_OUTDATED, mod.Name));
                            mod.IsOutdated = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Messenger.AddInfo(string.Format(Messages.MSG_ERROR_DURING_UPDATECHECK_0_ERRORMSG_1, mod.Name, ex.Message));
                }
            }
        }

        /// <summary>
        /// Starts a update check for all mods and updates all outdated mods.
        /// </summary>
        public static void UpdateAllOutdatedMods()
        {
            _UpdateOutdatedMods(Mods);
        }

        /// <summary>
        /// Starts a update check for all mods and updates all outdated mods.
        /// </summary>
        public static void UpdateAllOutdatedModsAsync()
        {
            UpdateOutdatedModsAsync(Mods);
        }

        /// <summary>
        /// Starts a update check for the mod and updates it if it's outdated.
        /// </summary>
        /// <param name="mods">The mod of the mod to update.</param>
        public static void UpdateOutdatedMods(ModNode[] mods)
        {
            _UpdateOutdatedMods(mods);
        }

        /// <summary>
        /// Starts a update check for the mod and updates it if it's outdated.
        /// </summary>
        /// <param name="mods">The mod of the mod to update.</param>
        public static void UpdateOutdatedModsAsync(ModNode[] mods)
        {
            EventDistributor.InvokeAsyncTaskStarted(Instance);
            View.SetEnabledOfAllControls(false);
            View.ShowBusy = true;

            AsyncTask<bool> asyncJob = new AsyncTask<bool>();
            asyncJob.SetCallbackFunctions(() =>
            {
                _UpdateOutdatedMods(mods);
                return true;
            },
                (result, ex) =>
                {
                    EventDistributor.InvokeAsyncTaskDone(Instance);
                    View.SetEnabledOfAllControls(true);
                    View.ShowBusy = false;

                    if (ex != null)
                        Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_MOD_UPDATE_0, ex.Message), ex);
                });
            asyncJob.Run();
        }

        /// <summary>
        /// Starts a update check for the mod and updates it if it's outdated.
        /// </summary>
        /// <param name="mods">The mod of the mod to update.</param>
        protected static void _UpdateOutdatedMods(ModNode[] mods)
        {
            _CheckForModUpdates(mods);

            var outdatedMods = from e in mods where e.IsOutdated || !e.ZipExists select e;
            foreach (ModNode mod in outdatedMods)
            {
                try
                {
                    var handler = mod.SiteHandler;
                    if (handler != null)
                    {
                        Messenger.AddInfo(string.Format(Messages.MSG_DOWNLOADING_MOD_0, mod.Name));
                        ModInfo newModInfos = handler.GetModInfo(mod.ModURL);
                        if (handler.DownloadMod(ref newModInfos, (received, fileSize) => { View.SetProgressBarStates(true, (int)(fileSize / 1000), (int)(received / 1000)); }))
                            UpdateMod(newModInfos, mod);

                        View.SetProgressBarStates(false);
                    }
                }
                catch (Exception ex)
                {
                    Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_MODUPDATE_0_ERROR_1, mod.Name, ex.Message), ex);
                }
            }
        }

        /// <summary>
        /// Updates the outdated mod.
        /// Tries to copy the checked state and destination of a mod and its parts, then uninstalls the outdated mod and installs the new one.
        /// </summary>
        /// <param name="newModInfo">The ModeInfo of the new mod.</param>
        /// <param name="outdatedMod">The root ModNode of the outdated mod.</param>
        /// <returns>The updated mod.</returns>
        public static ModNode UpdateMod(ModInfo newModInfo, ModNode outdatedMod)
        {
            ModNode newMod = null;
            try
            {
                Messenger.AddInfo(string.Format(Messages.MSG_UPDATING_MOD_0, outdatedMod.Text));
                newMod = ModNodeHandler.CreateModNode(newModInfo);
                newMod.AdditionalURL = outdatedMod.AdditionalURL;
                newMod.AvcURL = outdatedMod.AvcURL;
                newMod.Note = outdatedMod.Note;
                if (OptionsController.ModUpdateBehavior == ModUpdateBehavior.RemoveAndAdd || (!outdatedMod.IsInstalled && !outdatedMod.HasInstalledChilds))
                {
                    RemoveOutdatedAndAddNewMod(outdatedMod, newMod);
                    View.InvokeIfRequired(() => { newMod._Checked = false; });
                }
                else
                {
                    // Find matching file nodes and copy destination from old to new mod.
                    if (ModNodeHandler.TryCopyDestToMatchingNodes(outdatedMod, newMod))
                    {
                        newMod.ModURL = outdatedMod.ModURL;
                        RemoveOutdatedAndAddNewMod(outdatedMod, newMod);
                        ProcessMods(new ModNode[] { newMod }, true);
                    }
                    else
                    {
                        // No match found -> user must handle update.
                        Model.AddMod(newMod);
                        View.InvokeIfRequired(() => MessageBox.Show(View.ParentForm, string.Format(Messages.MSG_ERROR_UPDATING_MOD_0_FAILED, outdatedMod.Text)));
                    }
                }

                Messenger.AddInfo(string.Format(Messages.MSG_MOD_0_UPDATED, newMod.Text));
            }
            catch (Exception ex)
            {
                Messenger.AddError(string.Format(Messages.MSG_ERROR_WHILE_UPDATING_MOD_0_ERROR_1, outdatedMod.Text, ex.Message), ex);
            }

            return newMod;
        }

        #endregion

        #region ModPack export/import

        /// <summary>
        /// Opens the ModPack Import/Export dialog.
        /// </summary>
        public static void OpenExportImportDialog()
        {
            frmImExport dlg = new frmImExport();
            dlg.ShowDialog(View.ParentForm);
        }

        #endregion

        #region Mod zipping

        /// <summary>
        /// Creates a zip for each root node in the passed node list.
        /// </summary>
        /// <param name="nodes">List of root nodes to create zips for.</param>
        public static void CreateZip(List<ModNode> nodes)
        {
            // get path for the zip
            if (!Directory.Exists(OptionsController.DownloadPath))
                OptionsController.SelectNewDownloadPath();

            if (!Directory.Exists(OptionsController.DownloadPath))
            {
                Messenger.AddInfo(Messages.MSG_ERROR_NO_DOWNLOAD_FOLDER_SELECTED);
                Messenger.AddInfo(Messages.MSG_ZIP_CREATION_ABORTED);
                return;
            }

            int nodeCount = ModSelectionTreeModel.GetFullNodeCount(nodes);

            // disable controls
            View.SetEnabledOfAllControls(false);
            EventDistributor.InvokeAsyncTaskStarted(Instance);

            AsyncTask<bool>.DoWork(
                () =>
                {
                    try
                    {
                        return ModZipCreator.CreateZip(nodes, OptionsController.DownloadPath);
                    }
                    catch (Exception ex)
                    {
                        Messenger.AddError(string.Format(Messages.MSG_ZIP_CREATION_FAILED_0, ex.Message));
                        return false;
                    }
                },
                (bool b, Exception ex) =>
                {
                    EventDistributor.InvokeAsyncTaskDone(Instance);
                    View.SetEnabledOfAllControls(true);
                },
                (int processedNodeCount) =>
                {
                    View.SetProgressBarStates(true, nodeCount, processedNodeCount);
                });
        }

        #endregion

        #region Relocate mod archive path

        /// <summary>
        /// Opens a FolderSelect dialog and sets the archive path of the selected mod to the "selected folder" + "mod archive filename" if the archive is found in the new folder.
        /// </summary>
        public static void RelocateArchivePath(ModNode selectedMod)
        {
            if (selectedMod == null)
                return;

            selectedMod = selectedMod.ZipRoot;

            var dlg = new FolderSelectDialog();
            dlg.Title = Messages.MSG_SELECT_NEW_ARCHIVE_PATH;
            dlg.InitialDirectory = OptionsController.DownloadPath;
            if (!dlg.ShowDialog(View.ParentForm.Handle))
                return;

            RelocateArchivePath(selectedMod, dlg.FileName);
        }

        /// <summary>
        /// Opens a FolderSelect dialog and sets the archive paths of all mods to the "selected folder" + "mod archive filename" if the archive is found in the new folder.
        /// </summary>
        public static void RelocateArchivePathAllMods()
        {
            var dlg = new FolderSelectDialog();
            dlg.Title = Messages.MSG_SELECT_NEW_ARCHIVE_PATH;
            dlg.InitialDirectory = OptionsController.DownloadPath;
            if (!dlg.ShowDialog(View.ParentForm.Handle))
                return;

            foreach (ModNode mod in Model.Nodes)
                RelocateArchivePath(mod, dlg.FileName);
        }

        private static void RelocateArchivePath(ModNode modNode, string newPath)
        {
            var newFullPath = Path.Combine(newPath, Path.GetFileName(modNode.Key));
            if (File.Exists(newFullPath))
            {
                modNode.Key = newFullPath;
                Messenger.AddInfo(string.Format(Messages.MSG_MOD_0_ARCHIVE_PATH_CHANGED_TO_1, modNode.Name, newFullPath));
            }
            else
            {
                Messenger.AddInfo(string.Format(Messages.MSG_MOD_ARCHIVE_0_NOT_FOUND_MOD_1, newFullPath, modNode.Name));
            }
        }

        #endregion

        /// <summary>
        /// Sorts the nodes of the ModSelection depending on the passed SortType.
        /// </summary>
        public static void SortModSelection()
        {
            // TODO: Find a better place for this method.
            NamedTreeColumn sortColumn = null;
            foreach (var column in View.tvModSelection.Columns)
            {
                if (column.SortOrder == SortOrder.None)
                    continue;

                sortColumn = column as NamedTreeColumn;
                break;
            }

            View.SortColumn(sortColumn);

            InvalidateView();
        }

        /// <summary>
        /// Opens the ConflictSolver dialog.
        /// </summary>
        /// <returns>True if all conflicts are resolved.</returns>
        public static bool OpenConflictSolver()
        {
            if (!ModRegister.HasConflicts)
                return true;

            frmConflictSolver frm = new frmConflictSolver();
            frm.ConflictData = ModRegister.GetConflictInfos();
            return frm.ShowDialog() == DialogResult.OK;
        }

        /// <summary>
        /// Opens the TextDisplayer dialog with the content of the passed node (if it is a representation of a file).
        /// </summary>
        /// <param name="node">The ModNode that contains the file information.</param>
        /// <param name="replaceNewLine">If true, the newline/linebreak chars will be replace to the one(s) that the OS normally uses.</param>
        public static void OpenTextDisplayer(ModNode node, bool replaceNewLine = true)
        {
            string content = string.Empty;

            if (node.IsInstalled)
                content = File.ReadAllText(KSPPathHelper.GetAbsolutePath(node.Destination));
            else if (node.ZipExists)
                content = TryReadFile(node);

            if (!string.IsNullOrEmpty(content))
            {
                if (replaceNewLine)
                {
                    content = content.Replace("\n", "{KSPPlaceholder}");
                    content = content.Replace("\r", "{KSPPlaceholder}");
                    content = content.Replace("{KSPPlaceholder}{KSPPlaceholder}", "{KSPPlaceholder}");
                    content = content.Replace("{KSPPlaceholder}", Environment.NewLine);
                }

                frmTextDisplayer frm = new frmTextDisplayer();
                frm.TextBox.Text = content;
                frm.ShowDialog(View.ParentForm);
            }
        }

        /// <summary>
        /// Opens the TreeView options dialog.
        /// </summary>
        public static void OpenTreeViewOptions()
        {
            frmColumnSelection dlg = new frmColumnSelection();
            dlg.ModSelectionColumns = View.GetModSelectionViewInfo().ModSelectionColumnsInfo;
            if (dlg.ShowDialog() == DialogResult.OK)
                dlg.ModSelectionColumns.ToTreeViewAdv(View.tvModSelection);
        }

        /// <summary>
        /// Opens the default browser with the KMA² Wiki url.
        /// </summary>
        public static void OpenWiki()
        {
            Process.Start(Constants.WIKIURL);
        }

        /// <summary>
        /// Tries to reads the content of the file that is represented by the passed ModNode.
        /// </summary>
        /// <param name="node">The ModNode that contains the File information.</param>
        /// <returns>The content of the file.</returns>
        private static string TryReadFile(ModNode node)
        {
            if (node == null || !node.IsFile) return string.Empty;

            ModNode root = node.ZipRoot;
            string fullpath = root.Key;
            try
            {
                if (File.Exists(fullpath))
                {
                    using (IArchive archiv = ArchiveFactory.Open(fullpath))
                    {
                        string fullPath = node.GetFullTreePath();
                        foreach (IArchiveEntry entry in archiv.Entries)
                        {
                            if (entry.IsDirectory)
                                continue;

                            if (fullPath.Contains(entry.FilePath))
                            {
                                using (MemoryStream memStream = new MemoryStream())
                                {
                                    entry.WriteTo(memStream);
                                    memStream.Position = 0;
                                    StreamReader reader = new StreamReader(memStream);
                                    return reader.ReadToEnd();
                                }
                            }
                        }
                    }
                }
                else
                    Messenger.AddInfo(string.Format(Messages.MSG_FILE_NOT_FOUND_0, fullpath));
            }
            catch (Exception ex)
            {
                Messenger.AddError(string.Format(Messages.MSG_ERROR_WHILE_READING_0, fullpath), ex);
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the ModSelection list as a string.
        /// </summary>
        /// <returns>The ModSelection list as a string.</returns>
        public static string GetModListAsText()
        {
            const int EXTRA_PAD = 5;

            var sb = new StringBuilder();
            int nameLength = EXTRA_PAD;
            int urlLength = 0;

            // Get the longest mod name
            foreach (var mod in Mods)
            {
                if (mod.Name.Length > nameLength)
                    nameLength = mod.Name.Length + EXTRA_PAD;
            }


            // Get the longest url
            foreach (var mod in Mods)
            {
                if (mod.ModURL == null) continue;
                if (mod.ModURL.Length > urlLength)
                    urlLength = mod.ModURL.Length + EXTRA_PAD;
            }


            // Create the column headers
            sb.Append("    Name".PadRight(nameLength + EXTRA_PAD - 1) + "URL");
            sb.Append(Environment.NewLine);


            foreach (var mod in Mods)
            {
                sb.Append(mod.Checked ? "[+] " : "[ ] ");
                sb.Append(mod.Name.PadRight(nameLength));
                sb.Append(mod.ModURL);
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Refreshes the CheckedState of the ModNodes with the fileDestination.
        /// </summary>
        /// <param name="fileDestination">Destination path of the file.</param>
        public static void RefreshCheckedStateOfNodeByDestination(string fileDestination)
        {
            var relativeDestination = KSPPathHelper.GetRelativePath(fileDestination).ToLower();
            if (!ModRegister.RegisterdModFiles.ContainsKey(relativeDestination))
                return;

            var nodes = ModRegister.RegisterdModFiles[relativeDestination];
            RefreshCheckedStateOfMods(nodes.ToArray());
        }
    }
}
