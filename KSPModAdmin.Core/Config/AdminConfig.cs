using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Localization;
using KSPModAdmin.Core.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace KSPModAdmin.Core.Config
{
    /// <summary>
    /// The config for all needed infos of the KSP MOD Admin.
    /// </summary>
    public static class AdminConfig
    {
        private static string mVersion = "v1.0";

        #region Load

        /// <summary>
        /// Loads the config.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Load(string path)
        {
            bool result = false;
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
                            result = LoadV1_0(doc);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Messenger.AddError(string.Format("Error during KSPModAdmin.cfg. \"{0}\"", ex.Message), ex);
            }

            return result;
        }

        /// <summary>
        /// v1.0 load function.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static bool LoadV1_0(XmlDocument doc)
        {
            XmlNodeList language = doc.GetElementsByTagName(Constants.LANGUAGE);
            Localizer.GlobalInstance.CurrentLanguage = (language.Count >= 1) ? language[0].Attributes[Constants.NAME].Value : Localizer.DEFAULT_LANGUAGE;

            XmlNodeList maxim = doc.GetElementsByTagName(Constants.WINDOWSTATE);
            if (maxim.Count >= 1)
            {
                foreach (XmlAttribute att in maxim[0].Attributes)
                {
                    if (att.Name == Constants.MAXIM && att.Value != null)
                        MainController.View.WindowState = (att.Value.Equals(Constants.TRUE, StringComparison.CurrentCultureIgnoreCase) ? FormWindowState.Maximized : FormWindowState.Normal);
                }
            }

            XmlNodeList size = doc.GetElementsByTagName(Constants.SIZE);
            if (size.Count >= 1 && MainController.View.WindowState != FormWindowState.Maximized)
            {
                MainController.View.Width = int.Parse(size[0].Attributes[Constants.WIDTH].Value);
                MainController.View.Height = int.Parse(size[0].Attributes[Constants.HEIGHT].Value);
            }

            XmlNodeList pos = doc.GetElementsByTagName(Constants.POSITION);
            if (pos.Count >= 1)
            {
                int x = 0;
                int y = 0;
                foreach (XmlAttribute att in pos[0].Attributes)
                {
                    if (att.Name == Constants.X)
                        int.TryParse(att.Value, out x);
                    else if (att.Name == Constants.Y)
                        int.TryParse(att.Value, out y);
                }

                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;

                Point position = new Point(x, y);
                if (position.X < 0 || position.Y < 0 ||
                    position.X > width || position.X + MainController.View.Width > width ||
                    position.Y > height || position.Y + MainController.View.Height > height)
                    position = Point.Empty;

                if (position != Point.Empty)
                    MainController.View.Location = position;
            }

            ModSelectionViewInfo vInfo = new ModSelectionViewInfo();
            vInfo.ModSelectionColumnsInfo = new ModSelectionColumnsInfo(doc);

            XmlNodeList colWidths = doc.GetElementsByTagName(Constants.MODINFOCOLUMNS);
            if (colWidths.Count >= 1)
            {
                var columns = colWidths[0];
                foreach (XmlNode col in columns.ChildNodes)
                {
                    int id = -1;
                    int width = 0;
                    foreach (XmlAttribute att in col.Attributes)
                    {
                        if (att.Name == Constants.ID && !string.IsNullOrEmpty(att.Value))
                            id = int.Parse(att.Value);
                        else if (att.Name == Constants.WIDTH && !string.IsNullOrEmpty(att.Value))
                            width = int.Parse(att.Value);
                    }

                    if (width > 0)
                        vInfo.ModInfosColumnWidths.Add(width);
                }
            }

            XmlNodeList splitterPos = doc.GetElementsByTagName(Constants.MODINFOSSPLITTERPOS);
            if (splitterPos.Count >= 1)
            {
                foreach (XmlAttribute att in splitterPos[0].Attributes)
                {
                    if (att.Name == Constants.POSITION && att.Value != null)
                        vInfo.ModInfosSplitterPos = double.Parse(att.Value);
                }
            }

            if (!vInfo.IsEmpty)
                ModSelectionController.View.SetModSelectionViewInfo(vInfo);

            XmlNodeList destinationDetectionNode = doc.GetElementsByTagName(Constants.DESTINATIONDETECTIONOPTIONS);
            if (destinationDetectionNode.Count >= 1)
            {
                foreach (XmlAttribute att in destinationDetectionNode[0].Attributes)
                {
                    if (att.Name == Constants.TYPE && att.Value != null)
                        OptionsController.DestinationDetectionType = (DestinationDetectionType)int.Parse(att.Value);
                    else if (att.Name == Constants.FALLBACK && att.Value != null)
                        OptionsController.CopyToGameData = (att.Value.Equals(Constants.TRUE, StringComparison.CurrentCultureIgnoreCase));
                }
            }

            XmlNodeList ttOptions = doc.GetElementsByTagName(Constants.TOOLTIPOPTIONS);
            if (ttOptions.Count >= 1)
            {
                foreach (XmlAttribute att in ttOptions[0].Attributes)
                {
                    if (att.Name == Constants.ONOFF && att.Value != null)
                        OptionsController.ToolTipOnOff = (att.Value.Equals(Constants.TRUE, StringComparison.CurrentCultureIgnoreCase));
                    else if (att.Name == Constants.DELAY && att.Value != null)
                        OptionsController.ToolTipDelay = decimal.Parse(att.Value);
                    else if (att.Name == Constants.DISPLAYTIME && att.Value != null)
                        OptionsController.ToolTipDisplayTime = decimal.Parse(att.Value);
                }
            }

            XmlNodeList conflictDetectionOnnOff = doc.GetElementsByTagName(Constants.CONFLICTDETECTIONOPTIONS);
            if (conflictDetectionOnnOff.Count >= 1)
            {
                foreach (XmlAttribute att in conflictDetectionOnnOff[0].Attributes)
                {
                    if (att.Name == Constants.ONOFF && att.Value != null)
                        OptionsController.ConflictDetectionOnOff = (att.Value.Equals(Constants.TRUE, StringComparison.CurrentCultureIgnoreCase));
                    else if (att.Name == Constants.SHOWCONFLICTSOLVER && att.Value != null)
                        OptionsController.ShowConflictSolver = (att.Value.Equals(Constants.TRUE, StringComparison.CurrentCultureIgnoreCase));
                }
            }

            XmlNodeList colorDestinationDetected = doc.GetElementsByTagName(Constants.DESTINATIONDETECTED);
            if (colorDestinationDetected.Count >= 1)
            {
                foreach (XmlAttribute att in colorDestinationDetected[0].Attributes)
                {
                    if (att.Name == Constants.COLOR && att.Value != null)
                        OptionsController.ColorDestinationDetected = GetColor(att.Value);
                }
            }

            XmlNodeList colorDestinationMissing = doc.GetElementsByTagName(Constants.DESTINATIONMISSING);
            if (colorDestinationMissing.Count >= 1)
            {
                foreach (XmlAttribute att in colorDestinationMissing[0].Attributes)
                {
                    if (att.Name == Constants.COLOR && att.Value != null)
                        OptionsController.ColorDestinationMissing = GetColor(att.Value);
                }
            }

            XmlNodeList colorDestinationConflict = doc.GetElementsByTagName(Constants.DESTINATIONCONFLICT);
            if (colorDestinationConflict.Count >= 1)
            {
                foreach (XmlAttribute att in colorDestinationConflict[0].Attributes)
                {
                    if (att.Name == Constants.COLOR && att.Value != null)
                        OptionsController.ColorDestinationConflict = GetColor(att.Value);
                }
            }

            XmlNodeList colorModInstalled = doc.GetElementsByTagName(Constants.MODINSTALLED);
            if (colorModInstalled.Count >= 1)
            {
                foreach (XmlAttribute att in colorModInstalled[0].Attributes)
                {
                    if (att.Name == Constants.COLOR && att.Value != null)
                        OptionsController.ColorModInstalled = GetColor(att.Value);
                }
            }

            XmlNodeList colorModArchiveMissing = doc.GetElementsByTagName(Constants.MODARCHIVEMISSING);
            if (colorModArchiveMissing.Count >= 1)
            {
                foreach (XmlAttribute att in colorModArchiveMissing[0].Attributes)
                {
                    if (att.Name == Constants.COLOR && att.Value != null)
                        OptionsController.ColorModArchiveMissing = GetColor(att.Value);
                }
            }

            XmlNodeList colorModOutdated = doc.GetElementsByTagName(Constants.MODOUTDATED);
            if (colorModOutdated.Count >= 1)
            {
                foreach (XmlAttribute att in colorModOutdated[0].Attributes)
                {
                    if (att.Name == Constants.COLOR && att.Value != null)
                        OptionsController.ColorModOutdated = GetColor(att.Value);
                }
            }

            XmlNodeList nodes = doc.GetElementsByTagName(Constants.KNOWN_KSP_PATH);
            if (nodes.Count >= 1)
            {
                List<NoteNode> knownPaths = new List<NoteNode>();
                foreach (XmlNode node in nodes)
                {
                    string kspPath = string.Empty;
                    string noteValue = string.Empty;
                    foreach (XmlAttribute att in node.Attributes)
                    {
                        if (att.Name == Constants.FULLPATH)
                            kspPath = att.Value;
                        else if (att.Name == Constants.NOTE)
                            noteValue = att.Value;
                    }
                    if (KSPPathHelper.IsKSPInstallFolder(kspPath))
                        knownPaths.Add(new NoteNode(kspPath, kspPath, noteValue));
                }

                if (knownPaths.Count > 0)
                    OptionsController.KnownKSPPaths = knownPaths;
            }

            nodes = doc.GetElementsByTagName(Constants.KSP_PATH);
            if (nodes.Count >= 1)
            {
                foreach (XmlAttribute att in nodes[0].Attributes)
                {
                    if (att.Name == Constants.NAME && !string.IsNullOrEmpty(att.Value))
                    {
                        if (KSPPathHelper.IsKSPInstallFolder(att.Value) && OptionsController.SelectedKSPPath != att.Value)
                            OptionsController.SelectedKSPPath = att.Value;
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(OptionsController.SelectedKSPPath) && OptionsController.KnownKSPPaths.Count > 0)
                OptionsController.SelectedKSPPath = OptionsController.KnownKSPPaths[0].FullPath;

            nodes = doc.GetElementsByTagName(Constants.POSTDOWNLOADACTION);
            if (nodes.Count >= 1)
            {
                foreach (XmlAttribute att in nodes[0].Attributes)
                {
                    if (att.Name == Constants.VALUE)
                    {
                        try
                        {
                            switch (int.Parse(att.Value))
                            {
                                case (int)PostDownloadAction.Ask:
                                    OptionsController.PostDownloadAction = PostDownloadAction.Ask;
                                    break;
                                case (int)PostDownloadAction.AutoUpdate:
                                    OptionsController.PostDownloadAction = PostDownloadAction.AutoUpdate;
                                    break;
                                case (int)PostDownloadAction.Ignore:
                                    OptionsController.PostDownloadAction = PostDownloadAction.Ignore;
                                    break;
                            }
                        }
                        catch
                        {
                            OptionsController.PostDownloadAction = PostDownloadAction.Ask;
                        }
                    }
                }
            }

            nodes = doc.GetElementsByTagName(Constants.CHECKFORUPDATES);
            if (nodes.Count >= 1)
            {
                foreach (XmlAttribute att in nodes[0].Attributes)
                {
                    if ((att.Name == Constants.VALUE || att.Name == Constants.CHECKFORUPDATES) && att.Value != null)
                        OptionsController.VersionCheck = (att.Value.Equals(Constants.TRUE, StringComparison.CurrentCultureIgnoreCase));
                }
            }

            nodes = doc.GetElementsByTagName(Constants.LASTMODUPDATETRY);
            if (nodes.Count >= 1)
            {
                foreach (XmlAttribute att in nodes[0].Attributes)
                {
                    if (att.Name == Constants.VALUE && att.Value != null)
                        try { OptionsController.LastModUpdateTry = DateTime.Parse(att.Value); }
                        catch { }
                }
            }

            nodes = doc.GetElementsByTagName(Constants.MODUPDATEINTERVAL);
            if (nodes.Count >= 1)
            {
                foreach (XmlAttribute att in nodes[0].Attributes)
                {
                    if (att.Name == Constants.VALUE && att.Value != null)
                        try { OptionsController.ModUpdateInterval = (ModUpdateInterval)int.Parse(att.Value); }
                        catch { }
                }
            }

            nodes = doc.GetElementsByTagName(Constants.MODUPDATEBEHAVIOR);
            if (nodes.Count >= 1)
            {
                foreach (XmlAttribute att in nodes[0].Attributes)
                {
                    if (att.Name == Constants.VALUE && att.Value != null)
                        try { OptionsController.ModUpdateBehavior = (ModUpdateBehavior)int.Parse(att.Value); }
                        catch { }
                }
            }

            return true;
        }

        private static Color GetColor(string colorAsString)
        {
            string[] rgb = colorAsString.Split(';');
            return Color.FromArgb(255, int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
        }

        #endregion

        #region Save

        /// <summary>
        /// Saves the config.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Save(string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode root = doc.CreateElement(Constants.ROOTNODE);
            doc.AppendChild(root);

            XmlNode node = doc.CreateElement(Constants.VERSION);
            node.InnerText = mVersion;
            root.AppendChild(node);

            XmlNode generalNode = doc.CreateElement(Constants.GENERAL);
            root.AppendChild(generalNode);

            // Language
            node = ConfigHelper.CreateConfigNode(doc, Constants.LANGUAGE, Constants.NAME, Localizer.GlobalInstance.CurrentLanguage);
            generalNode.AppendChild(node);

            // Window position
            node = ConfigHelper.CreateConfigNode(doc, Constants.POSITION, new string[,]
            {
                { Constants.X, MainController.View.Location.X.ToString() },
                { Constants.Y, MainController.View.Location.Y.ToString() }
            });
            generalNode.AppendChild(node);

            // Window size
            node = ConfigHelper.CreateConfigNode(doc, Constants.SIZE, new string[,]
            {
                { Constants.WIDTH, MainController.View.Width.ToString() },
                { Constants.HEIGHT, MainController.View.Height.ToString() }
            });
            generalNode.AppendChild(node);

            // Window maximize state
            node = ConfigHelper.CreateConfigNode(doc, Constants.WINDOWSTATE, Constants.MAXIM, (MainController.View.WindowState == FormWindowState.Maximized).ToString());
            generalNode.AppendChild(node);

            // ModSelection column widths
            var vInfo = ModSelectionController.View.GetModSelectionViewInfo();
            if (vInfo.ModSelectionColumnsInfo != null)
                vInfo.ModSelectionColumnsInfo.ToXml2(generalNode);

            // ModInfo column widths
            int i = 0;
            node = doc.CreateElement(Constants.MODINFOCOLUMNS);
            foreach (var colWidth in vInfo.ModInfosColumnWidths)
            {
                XmlNode columnNode = ConfigHelper.CreateConfigNode(doc, Constants.COLUMN, new string[,]
                {
                    { Constants.ID, i.ToString() },
                    { Constants.WIDTH, colWidth.ToString() }
                });
                node.AppendChild(columnNode);
                
                i++;
            }
            generalNode.AppendChild(node);

            // ModSelection splitter position
            node = ConfigHelper.CreateConfigNode(doc, Constants.MODINFOSSPLITTERPOS, Constants.POSITION, vInfo.ModInfosSplitterPos.ToString());
            generalNode.AppendChild(node);

            // Destination detection options
            node = ConfigHelper.CreateConfigNode(doc, Constants.DESTINATIONDETECTIONOPTIONS, new string[,]
            {
                { Constants.TYPE, ((int)OptionsController.DestinationDetectionType).ToString() },
                { Constants.FALLBACK, OptionsController.CopyToGameData.ToString() }
            });
            generalNode.AppendChild(node);

            // ToolTip options
            node = ConfigHelper.CreateConfigNode(doc, Constants.TOOLTIPOPTIONS, new string[,]
            {
                { Constants.ONOFF, OptionsController.ToolTipOnOff.ToString() },
                { Constants.DELAY, OptionsController.ToolTipDelay.ToString() },
                { Constants.DISPLAYTIME, OptionsController.ToolTipDisplayTime.ToString() }
            });
            generalNode.AppendChild(node);

            // Conflict detection options
            node = ConfigHelper.CreateConfigNode(doc, Constants.CONFLICTDETECTIONOPTIONS, new string[,]
            {
                { Constants.ONOFF, OptionsController.ConflictDetectionOnOff.ToString() },
                { Constants.SHOWCONFLICTSOLVER, OptionsController.ShowConflictSolver.ToString() }
            });
            generalNode.AppendChild(node);

            // ModSelection TreeNode colors
            node = doc.CreateElement(Constants.NODECOLORS);
            generalNode.AppendChild(node);

            // Destination detected
            XmlNode childNode = ConfigHelper.CreateConfigNode(doc, Constants.DESTINATIONDETECTED, Constants.COLOR,
                string.Format("{0};{1};{2}", OptionsController.ColorDestinationDetected.R, OptionsController.ColorDestinationDetected.G, OptionsController.ColorDestinationDetected.B));
            node.AppendChild(childNode);

            // Destination missing
            childNode = ConfigHelper.CreateConfigNode(doc, Constants.DESTINATIONMISSING, Constants.COLOR,
                string.Format("{0};{1};{2}", OptionsController.ColorDestinationMissing.R, OptionsController.ColorDestinationMissing.G, OptionsController.ColorDestinationMissing.B));
            node.AppendChild(childNode);

            // Destination conflict
            childNode = ConfigHelper.CreateConfigNode(doc, Constants.DESTINATIONCONFLICT, Constants.COLOR,
                string.Format("{0};{1};{2}", OptionsController.ColorDestinationConflict.R, OptionsController.ColorDestinationConflict.G, OptionsController.ColorDestinationConflict.B));
            node.AppendChild(childNode);

            // Mod installed
            childNode = ConfigHelper.CreateConfigNode(doc, Constants.MODINSTALLED, Constants.COLOR,
                string.Format("{0};{1};{2}", OptionsController.ColorModInstalled.R, OptionsController.ColorModInstalled.G, OptionsController.ColorModInstalled.B));
            node.AppendChild(childNode);

            // Mod archive missing
            childNode = ConfigHelper.CreateConfigNode(doc, Constants.MODARCHIVEMISSING, Constants.COLOR,
                string.Format("{0};{1};{2}", OptionsController.ColorModArchiveMissing.R, OptionsController.ColorModArchiveMissing.G, OptionsController.ColorModArchiveMissing.B));
            node.AppendChild(childNode);

            // Mod is outdated
            childNode = ConfigHelper.CreateConfigNode(doc, Constants.MODOUTDATED, Constants.COLOR,
                string.Format("{0};{1};{2}", OptionsController.ColorModOutdated.R, OptionsController.ColorModOutdated.G, OptionsController.ColorModOutdated.B));
            node.AppendChild(childNode);

            // Selected KSP path.
            node = ConfigHelper.CreateConfigNode(doc, Constants.KSP_PATH, Constants.NAME, OptionsController.SelectedKSPPath);
            generalNode.AppendChild(node);

            // Known KSP paths.
            XmlNode pathNodes = doc.CreateElement(Constants.KNOWN_KSP_PATHS);
            foreach (NoteNode info in OptionsController.KnownKSPPaths)
            {
                XmlNode pathNode = ConfigHelper.CreateConfigNode(doc, Constants.KNOWN_KSP_PATH, new string[,]
                {
                    { Constants.FULLPATH, info.Name },
                    { Constants.NOTE, info.Note }
                });
                pathNodes.AppendChild(pathNode);
            }
            generalNode.AppendChild(pathNodes);

            // KSP MA post download action
            node = ConfigHelper.CreateConfigNode(doc, Constants.POSTDOWNLOADACTION, Constants.VALUE, ((int)OptionsController.PostDownloadAction).ToString());
            generalNode.AppendChild(node);

            // KSP MA check for updates
            node = ConfigHelper.CreateConfigNode(doc, Constants.CHECKFORUPDATES, Constants.VALUE, OptionsController.VersionCheck.ToString());
            generalNode.AppendChild(node);

            // Last mod update check DateTime
            node = ConfigHelper.CreateConfigNode(doc, Constants.LASTMODUPDATETRY, Constants.VALUE, OptionsController.LastModUpdateTry.ToString());
            generalNode.AppendChild(node);

            // Mod update check interval
            node = ConfigHelper.CreateConfigNode(doc, Constants.MODUPDATEINTERVAL, Constants.VALUE, ((int)OptionsController.ModUpdateInterval).ToString());
            generalNode.AppendChild(node);

            // Mod update behavior
            node = ConfigHelper.CreateConfigNode(doc, Constants.MODUPDATEBEHAVIOR, Constants.VALUE, ((int)OptionsController.ModUpdateBehavior).ToString());
            generalNode.AppendChild(node);

            doc.Save(path);

            return true;
        }

        #endregion
    }
}
