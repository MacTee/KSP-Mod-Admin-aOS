using System.Collections.Generic;
using System.IO;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using KSPModAdmin.Core;

namespace KSPModAdmin.Translation.Plugin
{
    public class LanguageFileContent
    {
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
        public XmlAttribute mNameAttribute = null;

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
        public XmlAttribute mShortNameAttribute = null;

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FullPath { get { return Path.Combine(FilePath, FileName); } }

        public List<LanguageEntry> Entries { get; set; }


        public LanguageFileContent(string fullPath, XmlNode node)
        {
            FileName = Path.GetFileName(fullPath);
            FilePath = Path.GetDirectoryName(fullPath);
            XmlNode = node;

            Entries = new List<LanguageEntry>();
        }


        public void Save()
        {
            if (XmlNode != null && XmlNode.OwnerDocument != null)
                XmlNode.OwnerDocument.Save(FullPath);
        }
    }
}