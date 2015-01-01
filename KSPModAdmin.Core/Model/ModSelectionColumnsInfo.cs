using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using KSPModAdmin.Core.Config;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.NodeControls;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Core.Model
{
    public class ModSelectionColumnsInfo
    {
        public const string COLUMNMOD = "Mod";
        public const string COLUMNVERSION = "Version";
        public const string COLUMNVERSIONCHECK = "VersionCheck";
        public const string COLUMNKSPVERSION = "KSPVersion";
        public const string COLUMNAUTHOR = "Author";
        public const string COLUMNOUTDATED = "Outdated";
        public const string COLUMNRATING = "Rating";
        public const string COLUMNDOWNLOAD = "Downloads";
        public const string COLUMNNOTE = "Note";
        public const string COLUMNCONFLICTS = "Conflicts";
        public const string COLUMNINSTALLED = "Installed";
        public const string COLUMNCREATIONDATE = "CreationDate";
        public const string COLUMNCHANGEDATE = "ChangeDate";
        public const string COLUMNARCHIVEPATH = "ArchivePath";


        public List<ColumnData> Columns { get; set; }

        public static List<ColumnData> DefaultColumns
        {
            get
            {
                List<ColumnData> columns = new List<ColumnData>()
                {
                    new ColumnData()
                    {
                        Name = COLUMNMOD,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_00"], //"Mod",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 230,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeCheckBox,
                                DataPropertyName = "Checked",
                                EditEnabled = true,
                                LeftMargin = 0
                            }, 
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeIcon,
                                DataPropertyName = "Icon",
                                LeftMargin = 1,
                                ImageScaleMode = ImageScaleMode.Clip
                            }, 
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Name",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3,
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = COLUMNVERSION,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_5"], //"Version",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 80,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Version",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = COLUMNVERSIONCHECK,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_3"], //"VersionCheck",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 80,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "SiteHandlerNameUI",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                };
                return columns;
            }
        }

        public static List<ColumnData> AllDefaultColumns
        {
            get
            {
                List<ColumnData> columns = new List<ColumnData>();
                columns.AddRange(DefaultColumns);
                columns.AddRange(new []
                {
                    new ColumnData()
                    {
                        Name = COLUMNKSPVERSION,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_6"], //"KSPVersion",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 80,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "KSPVersion",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = COLUMNAUTHOR,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_7"], //"Author",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 80,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Author",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = COLUMNOUTDATED,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_10"], //"Outdated",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 80,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeCheckBox,
                                DataPropertyName = "IsOutdated",
                                EditEnabled = false,
                                LeftMargin = 3
                                //Type = ColumnItemType.NodeTextBox,
                                //DataPropertyName = "IsOutdated",
                                //IncrementalSearchEnabled = true,
                                //LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = COLUMNRATING,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_11"], //"Rating",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 80,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Rating",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = COLUMNDOWNLOAD,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_12"], //"Downloads",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 80,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Downloads",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = COLUMNNOTE,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_13"], //"Note",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 80,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Note",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = COLUMNCONFLICTS,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_16"], //"Conflicts",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 80,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeCheckBox,
                                DataPropertyName = "HasCollision",
                                EditEnabled = false,
                                LeftMargin = 3
                                //Type = ColumnItemType.NodeTextBox,
                                //DataPropertyName = "HasCollision",
                                //IncrementalSearchEnabled = true,
                                //LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = COLUMNINSTALLED,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_17"], //"Installed",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 80,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeCheckBox,
                                DataPropertyName = "IsInstalled",
                                EditEnabled = false,
                                LeftMargin = 3
                                //Type = ColumnItemType.NodeTextBox,
                                //DataPropertyName = "IsInstalled",
                                //IncrementalSearchEnabled = true,
                                //LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = COLUMNCREATIONDATE,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_8"], //"CreationDate",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 80,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "CreationDate",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = COLUMNCHANGEDATE,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_9"], //"ChangeDate",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 80,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "ChangeDate",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = COLUMNARCHIVEPATH,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_2"], //"Archive path",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 80,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "ArchivePath",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    }
                });
                return columns;
            }
        }


        public ModSelectionColumnsInfo()
        {
            Columns = DefaultColumns;
        }

        public ModSelectionColumnsInfo(XmlDocument doc)
        {
            FromXml2(doc);
        }

        public ModSelectionColumnsInfo(TreeViewAdv treeViewAdv)
        {
            FromTreeViewAdv(treeViewAdv);
        }


        public void FromTreeViewAdv(TreeViewAdv treeViewAdv)
        {
            Dictionary<int, ColumnData> columns = new Dictionary<int, ColumnData>();
            foreach (TreeColumn col in treeViewAdv.Columns)
            {
                ModSelectionTreeColumn column = col as ModSelectionTreeColumn;
                if (column == null)
                    continue;

                columns.Add(column.Index, new ColumnData()
                {
                    Name = column.Name,
                    Header = column.Header,
                    SortOrder = column.SortOrder,
                    TooltipText = column.TooltipText,
                    Width = column.Width
                });
            }

            foreach (NodeControl nodeControl in treeViewAdv.NodeControls)
            {
                ColumnData column = columns[nodeControl.ParentColumn.Index];
                var columnItem = new ColumnItemData();
                if (nodeControl.GetType() == typeof(NodeCheckBox))
                    columnItem.Type = ColumnItemType.NodeCheckBox;
                else if (nodeControl.GetType() == typeof(NodeIcon))
                    columnItem.Type = ColumnItemType.NodeIcon;
                else if (nodeControl.GetType() == typeof(NodeTextBox))
                    columnItem.Type = ColumnItemType.NodeTextBox;

                switch (columnItem.Type)
                {
                    case ColumnItemType.NodeCheckBox:
                        columnItem.DataPropertyName = ((NodeCheckBox)nodeControl).DataPropertyName;
                        columnItem.EditEnabled = ((NodeCheckBox)nodeControl).EditEnabled;
                        columnItem.LeftMargin = ((NodeCheckBox)nodeControl).LeftMargin;
                        break;

                    case ColumnItemType.NodeIcon:
                        columnItem.DataPropertyName = ((NodeIcon)nodeControl).DataPropertyName;
                        columnItem.LeftMargin = ((NodeIcon)nodeControl).LeftMargin;
                        columnItem.ImageScaleMode = ((NodeIcon)nodeControl).ScaleMode;
                        break;

                    case ColumnItemType.NodeTextBox:
                        columnItem.DataPropertyName = ((NodeTextBox)nodeControl).DataPropertyName;
                        columnItem.IncrementalSearchEnabled = ((NodeTextBox)nodeControl).IncrementalSearchEnabled;
                        columnItem.LeftMargin = ((NodeTextBox)nodeControl).LeftMargin;
                        break;
                }

                column.Items.Add(columnItem);
            }

            Columns = columns.Values.ToList();
        }

        public void FromXml(XmlDocument doc)
        {
            Columns = new List<ColumnData>();
            XmlNodeList colWidths = doc.GetElementsByTagName(Constants.TREEVIEWADVCOLUMNSINFO);
            if (colWidths.Count >= 1)
            {
                foreach (XmlNode columnNode in colWidths[0].ChildNodes)
                {
                    ColumnData columnData = new ColumnData();
                    foreach (XmlAttribute att in columnNode.Attributes)
                    {
                        if (att.Name == Constants.NAME && !string.IsNullOrEmpty(att.Value))
                            columnData.Header = att.Value;
                        else if (att.Name == Constants.SORTORDER && !string.IsNullOrEmpty(att.Value))
                            columnData.SortOrder = (SortOrder)int.Parse(att.Value);
                        else if (att.Name == Constants.TOOLTIPTEXT && !string.IsNullOrEmpty(att.Value))
                            columnData.TooltipText = att.Value;
                        else if (att.Name == Constants.WIDTH && !string.IsNullOrEmpty(att.Value))
                            columnData.Width = int.Parse(att.Value);
                    }

                    foreach (XmlNode columnItemNode in columnNode.ChildNodes)
                    {
                        ColumnItemData columnItem = new ColumnItemData();
                        foreach (XmlAttribute att in columnItemNode.Attributes)
                        {
                            if (att.Name == Constants.TYPE && !string.IsNullOrEmpty(att.Value))
                                columnItem.Type = (ColumnItemType)int.Parse(att.Value);
                            else if (att.Name == Constants.DATAPROPERTYNAME && !string.IsNullOrEmpty(att.Value))
                                columnItem.DataPropertyName = (att.Value == "SiteHandlerName") ? "SiteHandlerNameUI" : att.Value;
                            else if (att.Name == Constants.EDITENABLED && !string.IsNullOrEmpty(att.Value))
                                columnItem.EditEnabled = att.Value.Equals(Constants.TRUE, StringComparison.CurrentCultureIgnoreCase);
                            else if (att.Name == Constants.INCREMENTALSEARCHENABLED && !string.IsNullOrEmpty(att.Value))
                                columnItem.IncrementalSearchEnabled = att.Value.Equals(Constants.TRUE, StringComparison.CurrentCultureIgnoreCase);
                            else if (att.Name == Constants.LEFTMARGIN && !string.IsNullOrEmpty(att.Value))
                                columnItem.LeftMargin = int.Parse(att.Value);
                            else if (att.Name == Constants.SCALEMODE && !string.IsNullOrEmpty(att.Value))
                                columnItem.ImageScaleMode = (ImageScaleMode)int.Parse(att.Value);
                        }

                        columnData.Items.Add(columnItem);
                    }

                    Columns.Add(columnData);
                }
            }
        }

        public void FromXml2(XmlDocument doc)
        {
            Columns = new List<ColumnData>();
            XmlNodeList colWidths = doc.GetElementsByTagName(Constants.TREEVIEWADVCOLUMNSINFO);
            if (colWidths.Count >= 1)
            {
                foreach (XmlNode columnNode in colWidths[0].ChildNodes)
                {
                    ColumnData columnData = new ColumnData();
                    foreach (XmlAttribute att in columnNode.Attributes)
                    {
                        if (att.Name == Constants.NAME && !string.IsNullOrEmpty(att.Value))
                            columnData.Name = att.Value;
                        else if (att.Name == Constants.SORTORDER && !string.IsNullOrEmpty(att.Value))
                            columnData.SortOrder = (SortOrder)int.Parse(att.Value);
                        else if (att.Name == Constants.WIDTH && !string.IsNullOrEmpty(att.Value))
                            columnData.Width = int.Parse(att.Value);
                    }

                    ColumnData cData = GetColumn(columnData.Name);
                    if (cData == null)
                        continue;

                    cData.SortOrder = columnData.SortOrder;
                    cData.Width = columnData.Width;
                    Columns.Add(cData);
                }
            }
        }

        public static ColumnData GetColumn(string name)
        {
            foreach (ColumnData column in AllDefaultColumns)
            {
                if (column.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                    return column;
            }

            return null;
        }

        public void ToTreeViewAdv(TreeViewAdv treeViewAdv)
        {
            treeViewAdv.Columns.Clear();
            treeViewAdv.NodeControls.Clear();
            foreach (ColumnData columnData in Columns)
            {
                ModSelectionTreeColumn treeColumn = new ModSelectionTreeColumn();
                treeColumn.Name = columnData.Name;
                treeColumn.Header = columnData.Header;
                treeColumn.SortOrder = columnData.SortOrder;
                treeColumn.TooltipText = columnData.TooltipText;
                treeColumn.Width = columnData.Width;
                treeViewAdv.Columns.Add(treeColumn);

                foreach (var columnItem in columnData.Items)
                {
                    switch (columnItem.Type)
                    {
                        case ColumnItemType.NodeCheckBox:
                            NodeCheckBox nodeCheckBox = new NodeCheckBox();
                            nodeCheckBox.DataPropertyName = columnItem.DataPropertyName;
                            nodeCheckBox.EditEnabled = columnItem.EditEnabled;
                            nodeCheckBox.LeftMargin = columnItem.LeftMargin;
                            nodeCheckBox.ParentColumn = treeColumn;
                            treeViewAdv.NodeControls.Add(nodeCheckBox);
                            break;

                        case ColumnItemType.NodeIcon:
                            NodeIcon nodeIcon = new NodeIcon();
                            nodeIcon.DataPropertyName = columnItem.DataPropertyName;
                            nodeIcon.LeftMargin = columnItem.LeftMargin;
                            nodeIcon.ScaleMode = columnItem.ImageScaleMode;
                            nodeIcon.ParentColumn = treeColumn;
                            treeViewAdv.NodeControls.Add(nodeIcon);
                            break;

                        case ColumnItemType.NodeTextBox:
                            NodeTextBox nodeTextBox = new NodeTextBox();
                            nodeTextBox.DataPropertyName = columnItem.DataPropertyName;
                            nodeTextBox.IncrementalSearchEnabled = columnItem.IncrementalSearchEnabled;
                            nodeTextBox.LeftMargin = columnItem.LeftMargin;
                            nodeTextBox.ParentColumn = treeColumn;
                            treeViewAdv.NodeControls.Add(nodeTextBox);
                            break;
                    }
                }
            }
        }

        public void ToXml(XmlNode root)
        {
            XmlNode infoNode = root.OwnerDocument.CreateElement(Constants.TREEVIEWADVCOLUMNSINFO);
            root.AppendChild(infoNode);

            foreach (ColumnData column in Columns)
            {
                XmlNode columnNode = ConfigHelper.CreateConfigNode(root.OwnerDocument, Constants.COLUMN, new string[,]
                    {
                        { Constants.NAME, column.Header },
                        { Constants.SORTORDER, ((int)column.SortOrder).ToString() },
                        { Constants.TOOLTIPTEXT, column.TooltipText },
                        { Constants.WIDTH, column.Width.ToString() }
                    });
                infoNode.AppendChild(columnNode);

                foreach (ColumnItemData item in column.Items)
                {
                    XmlNode itemNode = ConfigHelper.CreateConfigNode(root.OwnerDocument, "ColumnItem", new string[,]
                    {
                        { Constants.TYPE, ((int)item.Type).ToString() },
                        { Constants.DATAPROPERTYNAME, item.DataPropertyName },
                        { Constants.EDITENABLED, item.EditEnabled.ToString() },
                        { Constants.INCREMENTALSEARCHENABLED, item.IncrementalSearchEnabled.ToString() },
                        { Constants.LEFTMARGIN, item.LeftMargin.ToString() },
                        { Constants.SCALEMODE, ((int)item.ImageScaleMode).ToString() }
                    });
                    columnNode.AppendChild(itemNode);
                }
            }
        }

        public void ToXml2(XmlNode root)
        {
            XmlNode infoNode = root.OwnerDocument.CreateElement(Constants.TREEVIEWADVCOLUMNSINFO);
            root.AppendChild(infoNode);

            foreach (ColumnData column in Columns)
            {
                XmlNode columnNode = ConfigHelper.CreateConfigNode(root.OwnerDocument, Constants.COLUMN, new string[,]
                    {
                        { Constants.NAME, column.Name },
                        { Constants.SORTORDER, ((int)column.SortOrder).ToString() },
                        { Constants.WIDTH, column.Width.ToString() }
                    });
                infoNode.AppendChild(columnNode);
            }
        }
    }

    public enum ColumnItemType
    {
        NodeCheckBox,
        NodeIcon,
        NodeTextBox
    }

    public class ColumnData
    {
        public string Name { get; set; }
        public string Header { get; set; }
        public SortOrder SortOrder { get; set; }
        public string TooltipText { get; set; }
        public int Width { get; set; }
        public List<ColumnItemData> Items { get; set; }

        public ColumnData()
        {
            Name = string.Empty;
            Header = string.Empty;
            SortOrder = SortOrder.None;
            TooltipText = string.Empty;
            Width = 80;
            Items = new List<ColumnItemData>();
        }
    }

    public class ColumnItemData
    {
        public ColumnItemType Type { get; set; }
        public string DataPropertyName { get; set; }
        public bool EditEnabled { get; set; }
        public bool IncrementalSearchEnabled { get; set; }
        public int LeftMargin { get; set; }
        public ImageScaleMode ImageScaleMode { get; set; }

        public ColumnItemData()
        {
            Type = ColumnItemType.NodeTextBox;
            DataPropertyName = string.Empty;
            EditEnabled = false;
            IncrementalSearchEnabled = false;
            LeftMargin = 0;
            ImageScaleMode = ImageScaleMode.Clip;
        }
    }
}
