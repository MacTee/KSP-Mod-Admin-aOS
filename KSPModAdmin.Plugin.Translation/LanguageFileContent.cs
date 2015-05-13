using System.Collections.Generic;
using System.IO;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using KSPModAdmin.Core;

namespace KSPModAdmin.Plugin.Translation
{
    /// <summary>
    /// Class that holds all entries of a language.
    /// </summary>
    public class LanguageFileContent
    {
        /// <summary>
        /// Root XmlNode of the language file.
        /// </summary>
        public XmlNode XmlNode
        {
            get { return mXmlNode; }
            set
            {
                mXmlNode = value;

                if (mXmlNode != null && mXmlNode.Attributes != null)
                {
                    foreach (XmlAttribute att in mXmlNode.Attributes)
                    {
                        if (att.Name == Constants.NAME)
                            mShortNameAttribute = att;
                        if (att.Name == Constants.LONGNAME)
                            mNameAttribute = att;
                    }
                }
            }
        }
        private XmlNode mXmlNode = null;

        /// <summary>
        /// The name of the language.
        /// </summary>
        public string Name
        {
            get
            {
                return (mNameAttribute != null) ? mNameAttribute.Value : string.Empty;
            }
            set
            {
                if (mNameAttribute != null)
                    mNameAttribute.Value = value;
            }
        }
        private XmlAttribute mNameAttribute = null;

        /// <summary>
        /// The short name of the language.
        /// </summary>
        public string ShortName
        {
            get
            {
                return (mShortNameAttribute != null) ? mShortNameAttribute.Value : string.Empty;
            }
            set
            {
                if (mShortNameAttribute != null) 
                    mShortNameAttribute.Value = value;
            }
        }
        private XmlAttribute mShortNameAttribute = null;

        /// <summary>
        /// The file name of the language file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The path to the language file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Full path (Path + file name) of the language file.
        /// </summary>
        public string FullPath { get { return Path.Combine(FilePath, FileName); } }

        /// <summary>
        /// All language file entries.
        /// </summary>
        public List<LanguageEntry> Entries { get; set; }


        /// <summary>
        /// Creates a new instance of a LanguageFileContent class.
        /// </summary>
        public LanguageFileContent(string fullPath, XmlNode node)
        {
            FileName = Path.GetFileName(fullPath);
            FilePath = Path.GetDirectoryName(fullPath);
            XmlNode = node;

            Entries = new List<LanguageEntry>();
        }


        /// <summary>
        /// Saves the LanguageFileContent to a file (FullPath will be used).
        /// </summary>
        public void Save()
        {
            if (XmlNode != null && XmlNode.OwnerDocument != null)
                XmlNode.OwnerDocument.Save(FullPath);
        }
    }
}