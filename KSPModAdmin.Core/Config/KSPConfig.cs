using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace KSPModAdmin.Core.Config
{
    /// <summary>
    /// The config of all installed MODs for the specified KSP installation.
    /// </summary>
    public static class KSPConfig
    {
        private static string mVersion = "v1.0";

        #region Load

        /// <summary>
        /// Loads the config.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="modNodes"></param>
        /// <returns></returns>
        public static void Load(string path, ref List<ModNode> modNodes)
        {
            ModNode root = new ModNode() { Key = Constants.ROOT };
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNodeList moVersion = doc.GetElementsByTagName(Constants.VERSION);
                if (moVersion.Count > 0)
                {
                    switch (moVersion[0].InnerText.ToLower())
                    {
                        case "v1.0":
                            root = LoadV1_0(doc);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Messenger.AddError(string.Format("Error during KSPMods.cfg. \"{0}\"", ex.Message), ex);
            }

            modNodes.AddRange(root.Nodes.Cast<ModNode>());
        }

        /// <summary>
        /// v1.0 load function.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static ModNode LoadV1_0(XmlDocument doc)
        {
            ModNode result = new ModNode() { Key = Constants.ROOT };
            XmlNodeList nodeList = doc.GetElementsByTagName(Constants.DOWNLOAD_PATH);
            if (nodeList.Count >= 1 && nodeList[0].Attributes != null)
            {
                foreach (XmlAttribute att in nodeList[0].Attributes)
                {
                    if (att.Name == Constants.NAME)
                        OptionsController.DownloadPath = att.Value;
                }
            }

            nodeList = doc.GetElementsByTagName(Constants.LAUNCHPARAMETER);
            if (nodeList.Count >= 1 && nodeList[0].Attributes != null)
            {
                foreach (XmlAttribute att in nodeList[0].Attributes)
                {
                    if (att.Name == Constants.USE64BIT)
                        MainController.LaunchPanel.Use64Bit = att.Value.Equals(Constants.TRUE, StringComparison.CurrentCultureIgnoreCase);
                    else if (att.Name == Constants.FORCEOPENGL)
                        MainController.LaunchPanel.ForceOpenGL = att.Value.Equals(Constants.TRUE, StringComparison.CurrentCultureIgnoreCase);
                }
            }

            //nodeList = doc.GetElementsByTagName(Constants.OVERRRIDE);
            //if (nodeList.Count >= 1 && nodeList[0].Attributes != null)
            //{
            //    foreach (XmlAttribute att in nodeList[0].Attributes)
            //    {
            //        if (att.Name == Constants.VALUE)
            //            mOverride = false; // mOverride = (att.Value.ToLower() == Constants.TRUE);
            //    }
            //}

            //nodeList = doc.GetElementsByTagName(Constants.KSPSTARTUPOPTIONS);
            //if (nodeList.Count >= 1 && nodeList[0].Attributes != null)
            //{
            //    foreach (XmlAttribute att in nodeList[0].Attributes)
            //    {
            //        if (att.Name == Constants.BORDERLESSWINDOW)
            //            mStartWithBorderlessWindow = (att.Value.ToLower() == Constants.TRUE);
            //    }
            //}

            result.Nodes.Clear();
            XmlNodeList mods = doc.GetElementsByTagName(Constants.MOD);
            foreach (XmlNode mod in mods)
            {
                ModNode newNode = new ModNode();
                result.Nodes.Add(newNode);
                FillModTreeNode(mod, ref newNode);
            }

            return result;
        }

        /// <summary>
        /// Creates a TreeNode for the XmlNode information.
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private static void FillModTreeNode(XmlNode mod, ref ModNode node)
        {
            string kspForumURL = string.Empty;
            string curseForgeURL = string.Empty;
            node.AddDate = DateTime.Now.ToString();
            foreach (XmlAttribute att in mod.Attributes)
            {
                if (att.Name == Constants.NAME)
                    node.Name = att.Value;
                else if (att.Name == Constants.KEY)
                    node.Key = att.Value;
                else if (att.Name == Constants.ADDDATE)
                    node.AddDate = att.Value;
                else if (att.Name == Constants.VERSION)
                    node.Version = att.Value;
                else if (att.Name == Constants.GAMEVERSION)
                    node.KSPVersion = att.Value;
                else if (att.Name == Constants.NOTE)
                    node.Note = att.Value;
                else if (att.Name == Constants.PRODUCTID)
                    node.ProductID = att.Value;
                else if (att.Name == Constants.CREATIONDATE)
                    node.CreationDate = att.Value;
                else if (att.Name == Constants.CHANGEDATE)
                    node.ChangeDate = att.Value;
                else if (att.Name == Constants.AUTHOR)
                    node.Author = att.Value;
                else if (att.Name == Constants.RATING)
                    node.Rating = att.Value;
                else if (att.Name == Constants.DOWNLOADS)
                    node.Downloads = att.Value;
                else if (att.Name == Constants.MODURL)
                    node.ModURL = att.Value;
                else if (att.Name == Constants.AVCURL)
                    node.AvcURL = att.Value;
                else if (att.Name == Constants.ADDITIONALURL)
                    node.AdditionalURL = att.Value;
                else if (att.Name == Constants.CHECKED)
                    node.SetChecked((att.Value.Equals(Constants.TRUE, StringComparison.CurrentCultureIgnoreCase)), true);
                else if (att.Name == Constants.NODETYPE)
                    node.NodeType = (NodeType)int.Parse(att.Value);
                else if (att.Name == Constants.DESTINATION)
                    node.Destination = att.Value;
                else if (att.Name == Constants.FORUMURL)
                    kspForumURL = att.Value;
                else if (att.Name == Constants.CURSEFORGEURL)
                    curseForgeURL = att.Value;
                else if (att.Name == Constants.VERSIONCONTROLERNAME)
                {
                    node.SiteHandlerName = att.Value;

                    switch (att.Value)
                    {
                        case "KSPForum": // KSPForum
                            if (string.IsNullOrEmpty(node.ModURL))
                                node.ModURL = kspForumURL;
                            if (string.IsNullOrEmpty(node.AdditionalURL))
                                node.AdditionalURL = curseForgeURL;
                            break;
                        case "CurseForge": // CurseForge
                            if (string.IsNullOrEmpty(node.ModURL))
                                node.ModURL = curseForgeURL;
                            if (string.IsNullOrEmpty(node.AdditionalURL))
                                node.AdditionalURL = kspForumURL;
                            break;
                    }
                }
            }

            foreach (XmlNode modEntry in mod.ChildNodes)
            {
                ModNode newNode = new ModNode();
                node.Nodes.Add(newNode);
                FillModTreeNode(modEntry, ref newNode);
            }
        }

        #endregion

        #region Save

        /// <summary>
        /// Saves the config.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="nodeArray"></param>
        /// <returns></returns>
        public static bool Save(string path, ModNode[] nodeArray)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode root = doc.CreateElement(Constants.ROOTNODE);
            doc.AppendChild(root);

            XmlNode versionNode = doc.CreateElement(Constants.VERSION);
            versionNode.InnerText = mVersion;
            root.AppendChild(versionNode);

            XmlNode generalNode = doc.CreateElement(Constants.GENERAL);
            root.AppendChild(generalNode);

            XmlNode node = ConfigHelper.CreateConfigNode(doc, Constants.DOWNLOAD_PATH, Constants.NAME, OptionsController.DownloadPath);
            generalNode.AppendChild(node);

            node = ConfigHelper.CreateConfigNode(doc, Constants.LAUNCHPARAMETER, new string[,]
            {
                { Constants.USE64BIT, MainController.LaunchPanel.Use64Bit.ToString() }, 
                { Constants.FORCEOPENGL, MainController.LaunchPanel.ForceOpenGL.ToString() }
            });
            generalNode.AppendChild(node);

            //node = CreateKSPConfigNode(doc, Constants.OVERRRIDE, Constants.VALUE, mOverride.ToString());
            //generalNode.AppendChild(node);

            //node = CreateKSPConfigNode(doc, Constants.KSPSTARTUPOPTIONS, Constants.BORDERLESSWINDOW, mStartWithBorderlessWindow.ToString());
            //generalNode.AppendChild(node);

            XmlNode modsNode = doc.CreateElement(Constants.MODS);
            root.AppendChild(modsNode);

            foreach (ModNode mod in nodeArray)
                modsNode.AppendChild(CreateXmlNode(Constants.MOD, mod, modsNode));

            doc.Save(path);

            return true;
        }

        /// <summary>
        /// Saves the node and all its child nodes.
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="child">The node to create the XmlNode from.</param>
        /// <param name="parent">The parent XmlNode of the new created XmlNode.</param>
        private static XmlNode CreateXmlNode(string nodeName, ModNode child, XmlNode parent)
        {
            XmlDocument doc = parent.OwnerDocument;
            XmlNode modNode = doc.CreateElement(nodeName);
            modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.KEY, child.Key));
            modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.NAME, child.Name));
            modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.NODETYPE, ((int)child.NodeType).ToString()));
            modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.CHECKED, child.Checked.ToString()));

            if (nodeName == Constants.MOD)
            { 
                if (!string.IsNullOrEmpty(child.AddDate))
                    modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.ADDDATE, child.AddDate));
                if (!string.IsNullOrEmpty(child.Version))
                    modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.VERSION, child.Version));
                if (!string.IsNullOrEmpty(child.KSPVersion))
                    modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.GAMEVERSION, child.KSPVersion));
                if (!string.IsNullOrEmpty(child.Note))
                    modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.NOTE, child.Note));
                if (!string.IsNullOrEmpty(child.ProductID))
                    modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.PRODUCTID, child.ProductID));
                if (!string.IsNullOrEmpty(child.CreationDate))
                    modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.CREATIONDATE, child.CreationDate));
                if (!string.IsNullOrEmpty(child.ChangeDate))
                    modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.CHANGEDATE, child.ChangeDate));
                if (!string.IsNullOrEmpty(child.Author))
                    modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.AUTHOR, child.Author));
                if (!string.IsNullOrEmpty(child.Rating))
                    modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.RATING, child.Rating));
                if (!string.IsNullOrEmpty(child.Downloads))
                    modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.DOWNLOADS, child.Downloads));
                if (!string.IsNullOrEmpty(child.ModURL))
                    modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.MODURL, child.ModURL));
                if (!string.IsNullOrEmpty(child.AvcURL))
                    modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.AVCURL, child.AvcURL));
                if (!string.IsNullOrEmpty(child.AdditionalURL))
                    modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.ADDITIONALURL, child.AdditionalURL));
                if (!string.IsNullOrEmpty(child.SiteHandlerName))
                    modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.VERSIONCONTROLERNAME, child.SiteHandlerName));
            }

            if (!string.IsNullOrEmpty(child.Destination))
                modNode.Attributes.Append(ConfigHelper.CreateXMLAttribute(doc, Constants.DESTINATION, child.Destination));

            foreach (ModNode childchild in child.Nodes)
                modNode.AppendChild(CreateXmlNode(Constants.MOD_ENTRY, childchild, modNode));

            return modNode;
        }

        #endregion
    }
}
