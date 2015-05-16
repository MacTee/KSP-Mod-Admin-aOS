using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Localization;
using KSPModAdmin.Plugin.FlagsTab.Views;

namespace KSPModAdmin.Plugin.FlagsTab.Controller
{
    /// <summary>
    /// Controller class for the Translation view.
    /// </summary>
    public class FlagsViewController
    {
        #region Members

        public const string FILTER_ALL = "All";
        public const string FILTER_MYFLAG = "MyFlag";
        public const string FLAGS = "Flags";
        public const string MYFLAGS = FILTER_MYFLAG;
        public const int FLAG_WIDTH = 256;
        public const int FLAG_HEIGHT = 160;

        /// <summary>
        /// List of all flags by group.
        /// </summary>
        private static List<KeyValuePair<string, ListViewItem>> flags = new List<KeyValuePair<string, ListViewItem>>();

        /// <summary>
        /// Flag to determine if a filter index change should be ignored.
        /// </summary>
        private static bool ignoreIndexChange = false;

        private static FlagsViewController instance = null;

        #endregion

        /// <summary>
        /// Gets the singleton of this class.
        /// </summary>
        protected static FlagsViewController Instance
        {
            get { return instance ?? (instance = new FlagsViewController()); }
        }

        /// <summary>
        /// Gets or sets the view of the controller.
        /// </summary>
        public static ucFlagsView View { get; protected set; }

        public static List<KeyValuePair<string, ListViewItem>> Flags { get { return flags; } }

        public static string MyFlagsFullPath
        {
            get
            {
                return Path.Combine(KSPPathHelper.GetPath(KSPPaths.GameData), MYFLAGS, FLAGS);
            }
        }

        public static string MyFlagsPath
        {
            get
            {
                return Path.Combine(KSPPathHelper.GetPath(KSPPaths.GameData), MYFLAGS);
            }
        }

        internal static void Initialize(ucFlagsView view)
        {
            View = view;

            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;
            EventDistributor.KSPRootChanged += KSPRootChanged;

            // Add your stuff to initialize here.
        }

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
        /// Callback function for the KSPRootChanged event.
        /// This is the place to handle a change of the selected KSP installation path..
        /// </summary>
        private static void KSPRootChanged(string kspPath)
        {
            // TODO: ...
        }

        #endregion

        #region Refresh

        /// <summary>
        /// Refreshes the Flags tab. 
        /// Searches the KSP install dir for flags and adds them to the ListView.
        /// </summary>
        public static void RefreshFlagTab()
        {
            if (ignoreIndexChange)
                return;

            Messenger.AddInfo("Refreshing flag tab ...");

            ignoreIndexChange = true;
            string lastFilter = View.SelectedFilter;
            View.ClearAll();
            flags.Clear();

            // add default Filter
            View.AddFilter(FILTER_ALL);
            View.AddFilter(FILTER_MYFLAG);

            View.ShowProcessingIcon = true;
            EventDistributor.InvokeAsyncTaskStarted(Instance);

            AsyncTask<bool>.DoWork(() =>
                {
                    SearchDir4FlagsDirs(KSPPathHelper.GetPath(KSPPaths.KSPRoot));
                    return true;
                },
                (bool result, Exception ex) =>
                {
                    View.ShowProcessingIcon = false;
                    EventDistributor.InvokeAsyncTaskDone(Instance);

                    if (ex != null)
                        MessageBox.Show(View.ParentForm, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (lastFilter != null &&
                        (lastFilter == FILTER_ALL || lastFilter == FILTER_MYFLAG || View.GetGroup(lastFilter) != null))
                        View.SelectedFilter = lastFilter;
                    else
                        View.SelectedFilter = FILTER_ALL;

                    ignoreIndexChange = false;

                    View.FillListView(flags);
                });
        }

        /// <summary>
        /// Searches the dir and all subdirs for flags.
        /// </summary>
        /// <param name="dir">The directory to search.</param>
        private static void SearchDir4FlagsDirs(string dir)
        {
            if (dir == string.Empty) 
                return;

            try
            {
                if (dir.ToLower().EndsWith("flags"))
                    SearchFlags(dir);

                string[] subdirs = Directory.GetDirectories(dir);
                foreach (string subdir in subdirs)
                    SearchDir4FlagsDirs(subdir);
            }
            catch (Exception ex)
            {
                Messenger.AddError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Searches the directory for *.PNG.
        /// </summary>
        /// <param name="dir">The directory to search.</param>
        private static void SearchFlags(string dir)
        {
            foreach (string file in Directory.GetFiles(dir))
            {
                if (Path.GetExtension(file).ToLower() == ".png")
                    AddFlagToList(file);
            }
        }

        /// <summary>
        /// Adds a Flag to the internal list of flags.
        /// </summary>
        /// <param name="file">Full path to the Flag file.</param>
        private static void AddFlagToList(string file)
        {
            if (File.Exists(file))
            {
                Image image = Image.FromFile(file);
                int index = View.AddImageListImage(image);

                ListViewItem item = new ListViewItem();
                item.ImageIndex = index;
                item.Text = Path.GetFileNameWithoutExtension(file);
                item.Tag = file;
                flags.Add(new KeyValuePair<string, ListViewItem>(GetGroupName(file), item));
            }
        }

        /// <summary>
        /// Gets the name of the group from filename.
        /// (The Name of the Parent.Parent directory of the file)
        /// </summary>
        /// <param name="file">Full path to the Flag file.</param>
        /// <returns>The group name.</returns>
        private static string GetGroupName(string file)
        {
            string result = Path.GetDirectoryName(file);
            result = result.Substring(0, result.ToLower().Replace("\\flags", string.Empty).Length);
            result = result.Substring(result.LastIndexOf("\\") + 1);
            return result;
        }

        #endregion

        #region ImportFlag

        /// <summary>
        /// Starts the Import of a new flag.
        /// </summary>
        public static void ImportFlag()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = Constants.IMAGE_FILTER;
            if (dlg.ShowDialog(View.ParentForm) == DialogResult.OK)
            {
                string filename = dlg.FileName;

                Image image = null;
                try
                {
                    string path = KSPPathHelper.GetPath(KSPPaths.GameData);
                    if (path == string.Empty)
                    {
                        Messenger.AddInfo("Invalid KSP path.");
                        return;
                    }

                    // Create .../GameData if not exist.
                    if (!Directory.Exists(path))
                    {
                        Messenger.AddInfo("Creating directory \".../GameData\".");
                        Directory.CreateDirectory(path);
                    }

                    // Create .../MyFlgas/Flags is not exist.
                    path = MyFlagsFullPath;
                    if (!Directory.Exists(path))
                    {
                        Messenger.AddInfo("Creating directory \".../MyFlgas/Flags\".");
                        path = MyFlagsPath;
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        // Forlder must be named like "Flags" case sensitive!!!!
                        Directory.CreateDirectory(MyFlagsFullPath);
                    }

                    // delete file with same name.
                    string savePath = Path.Combine(MyFlagsFullPath, Path.GetFileNameWithoutExtension(filename) + ".png");
                    if (File.Exists(savePath))
                    {
                        Messenger.AddInfo(string.Format("Deleting existing flag \"{0}\".", savePath));
                        File.Delete(savePath);
                    }

                    // save image with max flag size to gamedata/myflags/flags/.
                    image = Image.FromFile(filename);
                    if (image.Size.Width != FLAG_WIDTH || image.Size.Height != FLAG_HEIGHT)
                    {
                        Messenger.AddInfo("Adjusting flag size ...");
                        Bitmap newImage = new Bitmap(FLAG_WIDTH, FLAG_HEIGHT);
                        using (Graphics graphicsHandle = Graphics.FromImage(newImage))
                        {
                            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphicsHandle.DrawImage(image, 0, 0, FLAG_WIDTH, FLAG_HEIGHT);
                        }
                        Messenger.AddInfo("Saving flag ...");
                        newImage.Save(savePath, ImageFormat.Png);
                        image = newImage;
                    }
                    else
                    {
                        Messenger.AddInfo("Saving flag ...");
                        image.Save(savePath, ImageFormat.Png);
                    }
                }
                catch (Exception ex)
                {
                    image = null;
                    // TODO:
                    MessageBox.Show(View.ParentForm, "Error during flag creation. \"" + ex.Message + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (image != null)
                {
                    string flagname = Path.GetFileNameWithoutExtension(filename);
                    flags.Add(new KeyValuePair<string, ListViewItem>("MyFlags", View.CreateNewFlagItem(flagname, image, filename)));
                }

                RefreshFlagTab();
            }
        }

        #endregion

        #region DeleteFlag

        /// <summary>
        /// Deletes selected flags.
        /// </summary>
        public static void DeleteSelectedFlag()
        {
            if (View.SelectedFlags.Count > 0)
            {
                foreach (ListViewItem item in View.SelectedFlags)
                {
                    string filename = (string)item.Tag;
                    try
                    {
                        if (File.Exists(filename))
                        {
                            // delete file (try twice sometimes the file is still in use).
                            bool firstTry = true;
                            while (firstTry)
                            {
                                try
                                {
                                    File.Delete(filename);
                                    firstTry = false;
                                }
                                catch (Exception ex)
                                {
                                    if (!firstTry)
                                    {
                                        // TODO: Messenger only...
                                        MessageBox.Show(View.ParentForm, "Error while deleting file. " + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        continue;
                                    }

                                    Thread.Sleep(300);
                                    firstTry = false;
                                }
                            }

                            // remove pic from ListView
                            var pair2Del = new KeyValuePair<string, ListViewItem>(string.Empty, null);
                            foreach (KeyValuePair<string, ListViewItem> pair in flags)
                            {
                                if (((string)pair.Value.Tag) == filename)
                                {
                                    pair2Del = pair;

                                    // will be removed with next refresh.
                                    ////ilFlags.Images.RemoveAt(pair.Value.ImageIndex);
                                    break;
                                }
                            }

                            if (pair2Del.Value != null)
                                flags.Remove(pair2Del);

                            // remove from mod selection
                            ModSelectionController.UpdateNodeByDestination(filename);
                        }
                    }
                    catch (Exception ex)
                    {
                        // TODO:
                        MessageBox.Show(View.ParentForm, "Error while deleting file. " + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                View.FillListView(flags);
            }
        }

        #endregion
    }
}
