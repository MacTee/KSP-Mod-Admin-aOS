using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using KSPModAdmin.Core.Config;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.NodeControls;

namespace KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.Helper
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public static class TreeViewAdvColumnHelper
    {
        public static List<ColumnData> GetColumnData(TreeViewAdv treeViewAdv)
        {
            Dictionary<int, ColumnData> columns = new Dictionary<int, ColumnData>();
            foreach (TreeColumn col in treeViewAdv.Columns)
            {
                NamedTreeColumn column = col as NamedTreeColumn;
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

            return columns.Values.ToList();
        }

        public static List<ColumnData> GetColumnData(XmlDocument doc)
        {
            var columns = new List<ColumnData>();
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

                    columns.Add(columnData);
                }
            }

            return columns;
        }

        public static List<ColumnData> GetColumnData2(XmlDocument doc, List<ColumnData> defaultColumns)
        {
            var columns = new List<ColumnData>();
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

                    ColumnData cData = GetColumn(defaultColumns, columnData.Name);
                    if (cData == null)
                        continue;

                    cData.SortOrder = columnData.SortOrder;
                    cData.Width = columnData.Width;
                    columns.Add(cData);
                }
            }

            return columns;
        }

        public static void ColumnsToTreeViewAdv(TreeViewAdv treeViewAdv, List<ColumnData> columns)
        {
            treeViewAdv.Columns.Clear();
            treeViewAdv.NodeControls.Clear();
            foreach (ColumnData columnData in columns)
            {
                NamedTreeColumn treeColumn = new NamedTreeColumn();
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
                            nodeTextBox.UseCompatibleTextRendering = true;
                            treeViewAdv.NodeControls.Add(nodeTextBox);
                            break;
                    }
                }
            }
        }

        public static void ColumnsToXml(XmlNode root, List<ColumnData> columns)
        {
            XmlNode infoNode = root.OwnerDocument.CreateElement(Constants.TREEVIEWADVCOLUMNSINFO);
            root.AppendChild(infoNode);

            foreach (ColumnData column in columns)
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

        public static void ColumnsToXml2(XmlNode root, List<ColumnData> columns)
        {
            XmlNode infoNode = root.OwnerDocument.CreateElement(Constants.TREEVIEWADVCOLUMNSINFO);
            root.AppendChild(infoNode);

            foreach (ColumnData column in columns)
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

        public static ColumnData GetColumn(List<ColumnData> columns, string name)
        {
            foreach (ColumnData column in columns)
            {
                if (column.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                    return column;
            }

            return null;
        }
    }
}