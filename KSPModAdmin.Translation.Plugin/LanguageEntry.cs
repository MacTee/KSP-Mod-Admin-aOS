using System.Xml;
using KSPModAdmin.Core;

namespace KSPModAdmin.Translation.Plugin
{
    public class LanguageEntry
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
                            mNameAttribute = att;
                        if (att.Name == Constants.VALUE)
                            mValueAttribute = att;
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
        private XmlAttribute mNameAttribute = null;

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


        public LanguageEntry(XmlNode node)
        {
            XmlNode = node;
        }
    }
}