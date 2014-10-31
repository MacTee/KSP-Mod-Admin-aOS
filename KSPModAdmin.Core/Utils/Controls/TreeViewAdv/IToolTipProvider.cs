using System;
using System.Collections.Generic;
using System.Text;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.NodeControls;

namespace KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree
{
	public interface IToolTipProvider
	{
		string GetToolTip(TreeNodeAdv node, NodeControl nodeControl);
	}
}
