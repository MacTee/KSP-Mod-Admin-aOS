using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils;
using KSPMODAdmin.Core.Utils.Ckan;
using KSPModAdmin.Plugin.ModBrowserTab.Model;
using KSPModAdmin.Plugin.ModBrowserTab.Views;
using KSPModAdmin.Core.Controller;

namespace KSPModAdmin.Plugin.ModBrowserTab.Controller
{
    /// <summary>
    /// Controller class for the Translation view.
    /// </summary>
    public class ModBrowserCkanController
    {
        #region Member

        private const string CkanArchiveFolder = "CKAN_Archives";
        private static ModBrowserCkanController instance = null;
        private static CkanTreeModel model = new CkanTreeModel();
        private static Dictionary<string, CkanArchive> archives = new Dictionary<string, CkanArchive>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton of this class.
        /// </summary>
        protected static ModBrowserCkanController Instance
        {
            get { return instance ?? (instance = new ModBrowserCkanController()); }
        }

        /// <summary>
        /// Gets or sets the view of the controller.
        /// </summary>
        public static UcModBrowserCkan View { get; protected set; }

        #endregion

        internal static void Initialize(UcModBrowserCkan view)
        {
            View = view;

            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;
            EventDistributor.LanguageChanged += LanguageChanged;

//            ModSelectionController.CreateModNode += CreateModNode;

            // Add your stuff to initialize here.
            View.Model = model;
        }

//        private static ModNode CreateModNode(CreateModNodeEventArgs e)
//        {
//            return null;
//        }

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
        /// This is the place where you can translate non accessible controls.
        /// </summary>
        protected static void LanguageChanged(object sender)
        {
            View.LanguageChanged();
        }

        #endregion

        /// <summary>
        /// Downloads the CKAN Repositories from CkanRepoManager.MasterRepoListURL.
        /// And updates the View.
        /// </summary>
        /// <param name="finishedCallback">Optional callback function. Will be called after finishing the async get.</param>
        public static void RefreshCkanRepositories(Action finishedCallback = null)
        {
            var parent = ModBrowserViewController.View;
            if (parent != null)
                parent.ShowProcessing = true;

            Messenger.AddInfo(Messages.MSG_REFRESHING_REPOSITORIES);
            EventDistributor.InvokeAsyncTaskStarted(Instance);
            AsyncTask<CkanRepositories>.DoWork(() =>
                {
                    return CkanRepoManager.GetRepositoryList(); // CkanRepoManager.MasterRepoListURL);
                },
                (result, ex) =>
                {
                    EventDistributor.InvokeAsyncTaskDone(Instance);

                    if (parent != null)
                        parent.ShowProcessing = false;

                    if (ex != null)
                        Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_REFRESH_REPOSITORIES_0, ex.Message), ex);
                    else
                    { 
                        // CkanRepository last = View.SelectedRepository;
                        View.Repositories = result;
                        View.SelectedRepository = result.repositories.FirstOrDefault(); // last;
                    }

                    Messenger.AddInfo(Messages.MSG_REFRESHING_REPOSITORIES_DONE);

                    if (finishedCallback != null)
                        finishedCallback();
                });
        }

        /// <summary>
        /// Downloads the Ckan Repository archive if necessary, creates a CkanArchive from it and populates the view.
        /// </summary>
        /// <param name="repo">The Ckan Repository to get the Archive for.</param>
        /// <param name="forceDownload">If false the download will be skipped if a Ckan Repository archive file already exists.</param>
        /// <param name="finishedCallback">Optional callback function. Will be called after finishing the async get.</param>
        public static void RefreshCkanArchive(CkanRepository repo, bool forceDownload = false, Action finishedCallback = null)
        {
            model.Nodes.Clear();

            if (repo == null) 
                return;

            if (!OptionsController.HasValidDownloadPath)
            {
                Messenger.AddInfo(Messages.MSG_DOWNLOADPATH_MISSING);
                OptionsController.SelectNewDownloadPath();
                if (!OptionsController.HasValidDownloadPath)
                    return;
            }

            var parent = View.Parent as ucModBrowserView;
            if (parent != null)
                parent.ShowProcessing = true;

            EventDistributor.InvokeAsyncTaskStarted(Instance);
            Messenger.AddInfo(string.Format(Messages.MSG_REFRESHING_REPOSITORY_ARCHIVE_0, repo.name));

            AsyncTask<CkanArchive>.DoWork(() =>
                {
                    CkanArchive archive = null;
                    if (!forceDownload && archives.ContainsKey(repo.name))
                    {
                        Messenger.AddInfo(Messages.MSG_USING_CACHED_ARCHIVE);
                        archive = archives[repo.name];
                    }
                    else
                    {
                        var path = Path.Combine(OptionsController.DownloadPath, CkanArchiveFolder);
                        if (!Directory.Exists(path))
                        {
                            Messenger.AddInfo(Messages.MSG_CREATE_CKAN_ARCHIVE);
                            Directory.CreateDirectory(path);
                        }

                        var filename = string.Format("{0}_{1}", repo.name, Path.GetFileName(repo.uri.AbsolutePath));
                        var fullpath = Path.Combine(path, filename);

                        if (!forceDownload && File.Exists(fullpath))
                            archive = CkanRepoManager.CreateRepositoryArchive(fullpath);
                        else
                        {
                            // TODO: Separate download and create archive in different AsyncTasks.
                            if (CkanRepoManager.DownloadRepositoryArchive(repo, fullpath, null, OnDownloadProgressChanged))
                                archive = CkanRepoManager.CreateRepositoryArchive(fullpath);
                        }
                    }

                    return archive;
                },
                (newArchive, ex) =>
                {
                    if (parent != null)
                        parent.ShowProcessing = false;
                    ModBrowserViewController.View.ShowProgressBar(false, 0);
                    EventDistributor.InvokeAsyncTaskDone(Instance);
                    if (ex != null)
                    {
                        Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_REFRESH_REPOSITORY_ARCHIVE_0, ex.Message), ex);
                    }
                    else
                    {
                        if (newArchive != null)
                        {
                            newArchive.Repository = repo;

                            if (archives.ContainsKey(repo.name))
                                archives[repo.name] = newArchive;
                            else
                                archives.Add(repo.name, newArchive);

                            model.AddArchive(newArchive);
                            View.CountLabelText = string.Format(Messages.MSG_MODBROWSER_CKAN_COUNT_TEXT, newArchive.Mods.Count, model.Nodes.Count);
                        }
                        else
                        {
                            View.CountLabelText = string.Format(Messages.MSG_MODBROWSER_CKAN_COUNT_TEXT, 0, 0);                            
                        }
                    }

                    Messenger.AddInfo(Messages.MSG_REFRESH_REPOSITORY_DONE);

                    if (finishedCallback != null)
                        finishedCallback();
                });
        }

        private static void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs downloadProgressChangedEventArgs)
        {
            var bytesReceived = unchecked((int)downloadProgressChangedEventArgs.BytesReceived);
            var total = unchecked((int)downloadProgressChangedEventArgs.TotalBytesToReceive);
            ModBrowserViewController.View.ShowProgressBar(true, bytesReceived, total);
        }

        /// <summary>
        /// Processes all changes mods
        /// Installs or uninstalls them.
        /// </summary>
        public static void ProcessChanges()
        {
            Messenger.AddInfo(Messages.MSG_PROCESSING_STARTED);

            if (model.Nodes != null)
            {
                foreach (var mod in model.Nodes)
                {
                    foreach (var modInfo in mod.Nodes.Cast<CkanNode>().Where(modInfo => modInfo.Added != modInfo.Checked))
                    {
                        // TODO: Do the real work
                        modInfo.Added = modInfo.Checked;
                    }
                }
            }

            Messenger.AddInfo(Messages.MSG_PROCESSING_DONE);

            View.InvalidateView();
        }
    }
}
