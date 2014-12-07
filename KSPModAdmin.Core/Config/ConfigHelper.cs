using System.Xml;

namespace KSPModAdmin.Core.Config
{
    public static class ConfigHelper
    {
        /// <summary>
        /// Creates a XmlNode with the specified name and one attribute.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="nodeName"></param>
        /// <param name="attName"></param>
        /// <param name="attValue"></param>
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
        /// <param name="doc"></param>
        /// <param name="nodeName"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
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
        /// <param name="doc"></param>
        /// <param name="attName"></param>
        /// <param name="value"></param>
        /// <returns>A XmlAttribute with the passed name and value.</returns>
        public static XmlAttribute CreateXMLAttribute(XmlDocument doc, string attName, string value)
        {
            XmlAttribute attribute = doc.CreateAttribute(attName);
            attribute.Value = value;
            return attribute;
        }
    }
}
