using System.Collections.Generic;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Wrapper class for information of the GameData folder scan.
    /// </summary>
    internal class ScanInfo
    {
        /// <summary>
        /// Parent scan info of this instance.
        /// </summary>
        public ScanInfo Parent { get; set; }

        /// <summary>
        /// Child scan infos of this instance.
        /// </summary>
        public List<ScanInfo> Childs { get { return mChilds; } set { mChilds = value; } }
        private List<ScanInfo> mChilds = new List<ScanInfo>();

        /// <summary>
        /// File or directory name.
        /// </summary>
        public string Name { get { return mName; } set { mName = value; } }
        private string mName = string.Empty;

        /// <summary>
        /// Full path of the file/directory.
        /// </summary>
        public string Path { get { return mPath; } set { mPath = value; } }
        private string mPath = string.Empty;

        /// <summary>
        /// Flag that indicates whether this scan info is for a file or not.
        /// </summary>
        public bool IsFile { get; set; }


        /// <summary>
        /// Creates a new instance of the class ScanInfo.
        /// </summary>
        /// <param name="name">File or directory name.</param>
        /// <param name="path">Full path of the file/directory.</param>
        /// <param name="isFile">Flag that indicates whether this scan info is for a file or not.</param>
        /// <param name="parent">Parent of this instance.</param>
        public ScanInfo(string name, string path, bool isFile, ScanInfo parent = null)
        {
            Name = name;
            Path = path;
            IsFile = isFile;
            Parent = parent;

            if (Parent != null)
                Parent.Childs.Add(this);
        }
    }
}
