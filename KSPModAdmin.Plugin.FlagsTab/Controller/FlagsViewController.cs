using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Plugin.FlagsTab.Properties;
using KSPModAdmin.Plugin.FlagsTab.Views;

namespace KSPModAdmin.Plugin.FlagsTab.Controller
{

    /// <summary>
    /// Controller class for the Translation view.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class FlagsViewController
    {
        #region Members

        public const string FILTER_ALL = "All";
        public const string FILTER_MYFLAG = "MyFlag";
        public const string FLAGS = "Flags";
        public const string MYFLAGS = FILTER_MYFLAG;
        public const string FLAG_FILENAME = "KMA2_Flag.png";
        public const string KMA2 = "KSP Mod Admin v2";
        public const int FLAG_WIDTH = 256;
        public const int FLAG_HEIGHT = 160;
        public const string EXTENSION_PNG = ".png";
        public const string EXTENSION_DDS = ".dds";

        /// <summary>
        /// List of all available flags (group, ListViewItem).
        /// </summary>
        private static List<KeyValuePair<string, ListViewItem>> flags = new List<KeyValuePair<string, ListViewItem>>();

        /// <summary>
        /// Flag to determine if a filter index change should be ignored.
        /// </summary>
        private static bool ignoreIndexChange = false;

        private static FlagsViewController instance = null;

        #endregion

        #region Properties

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

        /// <summary>
        /// List of all available flags (group, ListViewItem).
        /// </summary>
        public static List<KeyValuePair<string, ListViewItem>> Flags { get { return flags; } }

        /// <summary>
        /// Gets the full path to the MyFlags/Flags folder of the current selected KSP installation.
        /// </summary>
        public static string MyFlagsFullPath
        {
            get
            {
                return Path.Combine(KSPPathHelper.GetPath(KSPPaths.GameData), MYFLAGS, FLAGS);
            }
        }

        /// <summary>
        /// Gets the full path to the MyFlags folder of the current selected KSP installation.
        /// </summary>
        public static string MyFlagsPath
        {
            get
            {
                return Path.Combine(KSPPathHelper.GetPath(KSPPaths.GameData), MYFLAGS);
            }
        }

        /// <summary>
        /// Array of supported flag image extensions.
        /// </summary>
        public static string[] Exctensions { get { return new[] { EXTENSION_PNG, EXTENSION_DDS }; } }

        [DefaultValue(true)]
        public static bool CreateKMAFlag { get; set; }

        #endregion

        internal static void Initialize(ucFlagsView view)
        {
            CreateKMAFlag = true;

            View = view;

            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;
            EventDistributor.KSPRootChanged += KSPRootChanged;

            // Add your stuff to initialize here.
            View.AddActionKey(VirtualKey.VK_DELETE, DeleteFlag);
            View.AddActionKey(VirtualKey.VK_BACK, DeleteFlag);
        }

        #region Event handling.

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

        /// <summary>
        /// Deletes the selected Flag.
        /// </summary>
        /// <returns>Returns true cause we have handled the key.</returns>
        private static bool DeleteFlag(ActionKeyInfo keyState)
        {
            DeleteSelectedFlag();
            return true;
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

            Messenger.AddInfo(Messages.MSG_FLAG_SCAN_STARTED);

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
                        Messenger.AddError(Messages.MSG_ERROR_DURING_FLAG_SCAN, ex);
                    else
                        Messenger.AddInfo(Core.Messages.MSG_DONE);

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
        /// Searches the dir and all sub directories for flags.
        /// </summary>
        /// <param name="dir">The directory to search.</param>
        private static void SearchDir4FlagsDirs(string dir)
        {
            if (dir == string.Empty) 
                return;

                if (dir.ToLower().EndsWith(FLAGS.ToLower()))
                    SearchFlags(dir);

                string[] subdirs = Directory.GetDirectories(dir);
                foreach (string subdir in subdirs)
                    SearchDir4FlagsDirs(subdir);
        }

        /// <summary>
        /// Searches the directory for *.PNG.
        /// </summary>
        /// <param name="dir">The directory to search.</param>
        private static void SearchFlags(string dir)
        {
            foreach (string file in Directory.GetFiles(dir))
            {
                if (IsSupportedFormat(Path.GetExtension(file)))
                    AddFlagToList(file);
            }
        }

        /// <summary>
        /// Returns true when the passed fileExtension is a supported image format.
        /// </summary>
        /// <param name="fileExtension">The fileExtension to check.</param>
        /// <returns>True when the passed fileExtension is a supported image format, otherwise false.</returns>
        private static bool IsSupportedFormat(string fileExtension)
        {
            return Exctensions.Any(ext => ext.Equals(fileExtension, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Adds a Flag to the internal list of flags.
        /// </summary>
        /// <param name="file">Full path to the Flag file.</param>
        private static void AddFlagToList(string file)
        {
            try
            {
                if (File.Exists(file))
                {
                    Image image = null;

                    string extension = Path.GetExtension(file);
                    if (extension.Equals(EXTENSION_DDS, StringComparison.CurrentCultureIgnoreCase))
                        image = DdsToBitMap(file);
                    else
                        image = Image.FromFile(file);

                    if (image == null)
                        return;

                    string groupName = GetGroupName(file);
                    string flagname = Path.GetFileNameWithoutExtension(file);

                    if (flagname.Equals(Path.GetFileNameWithoutExtension(FLAG_FILENAME), StringComparison.CurrentCultureIgnoreCase))
                        groupName = KMA2;

                    var item = View.CreateNewFlagItem(flagname, groupName, image, file);
                    flags.Add(new KeyValuePair<string, ListViewItem>(groupName, item));
                    Messenger.AddInfo(string.Format(Messages.MSG_FLAG_0_ADDED, flagname));
                }
            }
            catch (Exception ex)
            {
                Messenger.AddError(string.Format(Messages.MSG_ERROR_FLAG_0_ADD_FAILED, file), ex);
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
            result = result.Substring(0, result.ToLower().Replace(Path.DirectorySeparatorChar + FLAGS.ToLower(), string.Empty).Length);
            result = result.Substring(result.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            return result;
        }

        private static Image DdsToBitMap(string file)
        {
            Image image = null;

            try
            {
                image = Resources.Cant_Display_DDS;
                // TODO: Load DDS to BitMap
            }
            catch (Exception ex)
            {
                image = Resources.Cant_Display_DDS;
                Messenger.AddError(string.Format("Error while reading dds image \"{0}\"", file), ex);
            }

            return image;
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

                try
                {
                    CreateNeededDirectories();

                    // delete file with same name.
                    string savePath = Path.Combine(MyFlagsFullPath, Path.GetFileNameWithoutExtension(filename) + EXTENSION_PNG);
                    if (File.Exists(savePath))
                    {
                        Messenger.AddInfo(string.Format(Messages.MSG_DELETE_EXISTING_FLAG_0, savePath));
                        File.Delete(savePath);
                    }

                    // save image with max flag size to gamedata/myflags/flags/.
                    using (var image = Image.FromFile(filename))
                    {
                        if (image.Size.Width != FLAG_WIDTH || image.Size.Height != FLAG_HEIGHT)
                        {
                            Messenger.AddInfo(Messages.MSG_ADJUSTING_FLAG_SIZE);
                            Bitmap newImage = new Bitmap(FLAG_WIDTH, FLAG_HEIGHT);
                            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
                            {
                                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                graphicsHandle.DrawImage(image, 0, 0, FLAG_WIDTH, FLAG_HEIGHT);
                            }
                            Messenger.AddInfo(string.Format(Messages.MSG_SAVING_FLAG_0, savePath));
                            newImage.Save(savePath, ImageFormat.Png);
                        }
                        else
                        {
                            Messenger.AddInfo(string.Format(Messages.MSG_COPY_FLAG_0, savePath));
                            image.Save(savePath, ImageFormat.Png);
                        }
                    }

                    AddFlagToList(savePath);
                }
                catch (Exception ex)
                {
                    Messenger.AddError(Messages.MSG_ERROR_FLAG_CREATION_FAILED, ex);
                }

                RefreshFlagTab();
            }
        }

        private static void CreateNeededDirectories()
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
                Messenger.AddInfo(string.Format(Messages.MSG_CREATING_DIR_0, path));
                Directory.CreateDirectory(path);
            }

            // Create .../MyFlgas/Flags is not exist.
            path = MyFlagsFullPath;
            if (!Directory.Exists(path))
            {
                Messenger.AddInfo(string.Format(Messages.MSG_CREATING_DIR_0, path));
                path = MyFlagsPath;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // Forlder must be named like "Flags" case sensitive!!!!
                Directory.CreateDirectory(MyFlagsFullPath);
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
                            // Esteregg #1
                            if (AskUser(Path.GetFileNameWithoutExtension(filename)) || 
                                (Path.GetFileName(filename).Equals(FLAG_FILENAME, StringComparison.CurrentCultureIgnoreCase) && AskUser2()))
                                return;

                            // delete file (try twice sometimes the file is still in use).
                            bool firstTry = true;
                            bool failed = false;
                            while (firstTry)
                            {
                                try
                                {
                                    File.Delete(filename);
                                    Messenger.AddInfo(string.Format(Messages.MSG_FLAG_0_DELETED, filename));
                                    firstTry = false;
                                }
                                catch (Exception ex)
                                {
                                    if (!firstTry)
                                    {
                                        Messenger.AddError(string.Format(Messages.MSG_ERROR_DELETE_FLAG_0_FAILED, filename), ex);
                                        failed = true;
                                    }
                                    else
                                        Thread.Sleep(300);

                                    firstTry = false;
                                }
                            }

                            if (failed) 
                                continue;

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
                            ModSelectionController.RefreshCheckedStateOfNodeByDestination(filename);
                        }
                    }
                    catch (Exception ex)
                    {
                        Messenger.AddError(string.Format(Messages.MSG_ERROR_DELETE_FLAG_0_FAILED, filename), ex);
                    }
                }

                View.FillListView(flags);
            }
        }

        private static bool AskUser(string flagName)
        {
            return MessageBox.Show(View.ParentForm, string.Format(Messages.MSG_REALY_DELETE_FLAG_0, flagName), Core.Messages.MSG_TITLE_ATTENTION, MessageBoxButtons.YesNo) == DialogResult.No;
        }

        private static bool AskUser2()
        {
            if (MessageBox.Show(View.ParentForm, "I thinks it's pretty awesome. Can i abort the delete?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                return true;

            if (MessageBox.Show(View.ParentForm, string.Format("Can't delete KMA2 flag its used by KSP Mod Admin aOS!{0}Try again?", Environment.NewLine), "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
            {
                MessageBox.Show(View.ParentForm, "=)", string.Empty, MessageBoxButtons.OK);
                return true;
            }

            MessageBox.Show(View.ParentForm, "Ok ok, well then ... good bye =(", string.Empty, MessageBoxButtons.OK);

            return false;
        }

        #endregion

        public static void CreateKMA2Flag()
        {
            // TODO: Get CreateKMAFlag from AppConfig.
            if (!CreateKMAFlag)
                return;

            try
            {
                var fullpath = Path.Combine(MyFlagsFullPath, FLAG_FILENAME);
                if (!File.Exists(fullpath))
                {
                    // Create the folder if it does not exist
                    if (!Directory.Exists(MyFlagsFullPath))
                        Directory.CreateDirectory(MyFlagsFullPath);
                    
                    Image image = Resources.KMA2_Flag;
                    image.Save(fullpath);
                    image.Dispose();
                    Messenger.AddInfo(string.Format(Messages.MSG_FLAG_0_ADDED, Path.GetFileNameWithoutExtension(fullpath)));
                }
            }
            catch (Exception ex)
            {
                Messenger.AddError("Error! Can't create KMA² flag.", ex);
            }
        }
    }
}
