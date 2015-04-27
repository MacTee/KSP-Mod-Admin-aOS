using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using KSPModAdmin.Core.Properties;

namespace KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.NodeControls
{
    public class NodeStateIcon : NodeIcon
    {
        private Image _leaf;
        private Image _opened;
        private Image _closed;

        public NodeStateIcon()
        {
            _leaf = MakeTransparent(Resources.page);
            _opened = MakeTransparent(Resources.folder1);
            _closed = MakeTransparent(Resources.folder1);
        }

        private static Image MakeTransparent(Bitmap bitmap)
        {
            bitmap.MakeTransparent(bitmap.GetPixel(0, 0));
            return bitmap;
        }

        protected override Image GetIcon(TreeNodeAdv node)
        {
            Image icon = base.GetIcon(node);
            if (icon != null)
                return icon;
            else if (node.IsLeaf)
                return _leaf;
            else if (node.CanExpand && node.IsExpanded)
                return _opened;
            else
                return _closed;
        }
    }
}
