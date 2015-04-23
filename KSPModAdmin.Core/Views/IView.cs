using System.Windows.Forms;

namespace KSPModAdmin.Core.Views
{
    /// <summary>
    /// Interface for KSP MA views.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Gets the Controls of the view.
        /// </summary>
        Control.ControlCollection Controls { get; }

        /// <summary>
        /// Gets or sets the Name of the view.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Invokes the passed function if required.
        /// </summary>
        /// <param name="action">Function that should be invoked if required.</param>
        void InvokeIfRequired(MethodInvoker action);

        /// <summary>
        /// Forces the view to redraw.
        /// </summary>
        void InvalidateView();
    }
}
