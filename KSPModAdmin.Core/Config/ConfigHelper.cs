using System.Xml;

namespace KSPModAdmin.Core.Config
{
    /// <summary>
    /// Helper class to create XmlNodes and -Attributes for KSP- and AppConfig files.
    /// </summary>
    public static class ConfigHelper
    {
        /// <summary>
        /// Creates a XmlNode with the specified name and one attribute.
        /// </summary>
        /// <returns>A XmlNode with the specified name and one attribute.</returns>
        public static XmlNode CreateConfigNode(XmlDocument doc, string nodeName, string attName, string attValue)
        {
            XmlNode node = doc.CreateElement(nodeName);
            node.Attributes.Append(CreateXMLAttribute(doc, attName, attValue));

            return node;
        }

        /// <summary>
        /// Creates a XmlNode with the specified name and attributes.
        /// </summary>
        /// <returns>The new created XmlNode.</returns>
        public static XmlNode CreateConfigNode(XmlDocument doc, string nodeName, string[,] attributes)
        {
            XmlNode node = doc.CreateElement(nodeName);

            for (int i = 0; i < attributes.Length / 2; ++i)
                node.Attributes.Append(CreateXMLAttribute(doc, attributes[i, 0], attributes[i, 1]));

            return node;
        }

        /// <summary>
        /// Creates a XmlAttribute with the passed name and value.
        /// </summary>
        /// <returns>A XmlAttribute with the passed name and value.</returns>
        public static XmlAttribute CreateXMLAttribute(XmlDocument doc, string attName, string value)
        {
            XmlAttribute attribute = doc.CreateAttribute(attName);
            attribute.Value = value;
            return attribute;
        }
    }
}
