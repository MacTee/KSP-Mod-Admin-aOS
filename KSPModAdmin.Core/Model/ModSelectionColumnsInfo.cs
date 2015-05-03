using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using System.Xml;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.Helper;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Core.Model
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
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
                        Header = Localizer.GlobalInstance["lvModSelection_Item_00"], // "Mod",
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
                        Header = Localizer.GlobalInstance["lvModSelection_Item_5"], // "Version",
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
                        Header = Localizer.GlobalInstance["lvModSelection_Item_3"], // "VersionCheck",
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
                columns.AddRange(new[]
                {
                    new ColumnData()
                    {
                        Name = COLUMNKSPVERSION,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_6"], // "KSPVersion",
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
                        Header = Localizer.GlobalInstance["lvModSelection_Item_7"], // "Author",
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
                        Header = Localizer.GlobalInstance["lvModSelection_Item_10"], // "Outdated",
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
                                ////Type = ColumnItemType.NodeTextBox,
                                ////DataPropertyName = "IsOutdated",
                                ////IncrementalSearchEnabled = true,
                                ////LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = COLUMNRATING,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_11"], // "Rating",
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
                        Header = Localizer.GlobalInstance["lvModSelection_Item_12"], // "Downloads",
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
                        Header = Localizer.GlobalInstance["lvModSelection_Item_13"], // "Note",
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
                        Header = Localizer.GlobalInstance["lvModSelection_Item_16"], // "Conflicts",
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
                                ////Type = ColumnItemType.NodeTextBox,
                                ////DataPropertyName = "HasCollision",
                                ////IncrementalSearchEnabled = true,
                                ////LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = COLUMNINSTALLED,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_17"], // "Installed",
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
                                ////Type = ColumnItemType.NodeTextBox,
                                ////DataPropertyName = "IsInstalled",
                                ////IncrementalSearchEnabled = true,
                                ////LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = COLUMNCREATIONDATE,
                        Header = Localizer.GlobalInstance["lvModSelection_Item_8"], // "CreationDate",
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
                        Header = Localizer.GlobalInstance["lvModSelection_Item_9"], // "ChangeDate",
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
                        Header = Localizer.GlobalInstance["lvModSelection_Item_2"], // "Archive path",
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
            Columns = TreeViewAdvColumnHelper.GetColumnData(treeViewAdv);
        }

        public void FromXml(XmlDocument doc)
        {
            Columns = TreeViewAdvColumnHelper.GetColumnData(doc);
        }

        public void FromXml2(XmlDocument doc)
        {
            Columns = TreeViewAdvColumnHelper.GetColumnData2(doc, AllDefaultColumns);
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
            TreeViewAdvColumnHelper.ColumnsToTreeViewAdv(treeViewAdv, Columns);
        }

        public void ToXml(XmlNode root)
        {
            TreeViewAdvColumnHelper.ColumnsToXml(root, Columns);
        }

        public void ToXml2(XmlNode root)
        {
            TreeViewAdvColumnHelper.ColumnsToXml2(root, Columns);
        }
    }
}
