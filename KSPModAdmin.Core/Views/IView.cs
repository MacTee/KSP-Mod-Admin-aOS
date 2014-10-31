using System.Windows.Forms;

namespace KSPModAdmin.Core.Views
{
    public interface IView
    {
        Control.ControlCollection Controls { get; }

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
