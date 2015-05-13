using System.Xml;
using KSPModAdmin.Core;

namespace KSPModAdmin.Plugin.Translation
{
    /// <summary>
    /// Container class for a XmlNode of a language file .
    /// </summary>
    public class LanguageEntry
    {
        /// <summary>
        /// The XmlNode.
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
                            mNameAttribute = att;
                        if (att.Name == Constants.VALUE)
                            mValueAttribute = att;
                    }
                }
            }
        }
        private XmlNode mXmlNode = null;

        /// <summary>
        /// Key - Value of the "Name" XmlAttribute of the XmlNode.
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
        /// Value - Value of the "Value" XmlAttribute of the XmlNode.
        /// </summary>
        public string Value
        {
            get
            {
                return (mValueAttribute != null) ? mValueAttribute.Value : string.Empty;
            }
            set
            {
                if (mValueAttribute != null)
                    mValueAttribute.Value = value;
            }
        }
        private XmlAttribute mValueAttribute = null;


        /// <summary>
        /// Creates a new instance of a LanguageEntry from a XmlNode.
        /// </summary>
        public LanguageEntry(XmlNode node)
        {
            XmlNode = node;
        }
    }
}