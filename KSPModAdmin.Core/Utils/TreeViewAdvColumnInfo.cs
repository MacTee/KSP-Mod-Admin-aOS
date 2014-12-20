using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using KSPModAdmin.Core.Config;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.NodeControls;

namespace KSPModAdmin.Core.Utils
{
    public enum ColumnItemType
    {
        NodeCheckBox,
        NodeIcon,
        NodeTextBox
    }

    public class TreeViewAdvColumnsInfo
    {
        public List<ColumnData> Columns { get; set; }

        public static List<ColumnData> DefaultColumns
        {
            get
            {
                List<ColumnData> columns = new List<ColumnData>()
                {
                    new ColumnData()
                    {
                        Header = "Mod",
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
                        Header = "Version",
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
                        Header = "VersionCheck",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 80,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "SiteHandlerName",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                };
                return columns;
            }
        }


        public TreeViewAdvColumnsInfo()
        {
            Columns = DefaultColumns;
        }

        public TreeViewAdvColumnsInfo(XmlDocument doc)
        {
            FromXml(doc);
        }

        public TreeViewAdvColumnsInfo(TreeViewAdv treeViewAdv)
        {
            FromTreeViewAdv(treeViewAdv);
        }


        public void FromTreeViewAdv(TreeViewAdv treeViewAdv)
        {
            Dictionary<int, ColumnData> columns = new Dictionary<int, ColumnData>();
            foreach (TreeColumn column in treeViewAdv.Columns)
            {
                columns.Add(column.Index, new ColumnData()
                {
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
                                columnItem.DataPropertyName = att.Value;
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

        public void ToTreeViewAdv(TreeViewAdv treeViewAdv)
        {
            treeViewAdv.Columns.Clear();
            treeViewAdv.NodeControls.Clear();
            foreach (ColumnData columnData in Columns)
            {
                TreeColumn treeColumn = new TreeColumn();
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
    }

    public class ColumnData
    {
        public string Header { get; set; }
        public SortOrder SortOrder { get; set; }
        public string TooltipText { get; set; }
        public int Width { get; set; }
        public List<ColumnItemData> Items { get; set; }

        public ColumnData()
        {
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
