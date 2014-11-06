using System;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Views
{
    public partial class ucBase : UserControl, IView
    {

        /// <summary>
        /// Constructor for VS Designer only!
        /// </summary>
        public ucBase()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Invokes the passed function if required.
        /// </summary>
        /// <param name="action">Function that should be invoked if required.</param>
        public void InvokeIfRequired(MethodInvoker action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        /// <summary>
        /// Forces the view to redraw.
        /// </summary>
        public virtual void InvalidateView() { }

        /// <summary>
        /// Gets the Name for the parent TabPage.
        /// </summary>
        /// <returns>The Name for the parent TabPage.</returns>
        public virtual string GetTabName()
        {
            throw new NotImplementedException("Implement GetTabName for derived classes of ucBase!");
        }
    }
}
