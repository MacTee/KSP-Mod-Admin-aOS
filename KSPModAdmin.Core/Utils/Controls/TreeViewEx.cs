using System.Windows.Forms;

namespace KSPModAdmin.Core.Utils.Controls
{
    /// <summary>
    /// Extended TreeView with ActionKeyManager support.
    /// </summary>
    public class TreeViewExAkm : TreeView
    {
        #region ActionKeyManager

        private ActionKeyManager actionKeyManager = new ActionKeyManager();

        /// <summary>
        /// Add a ActionKey CallbackFunction binding to the flag ListView.
        /// </summary>
        /// <param name="key">The action key that raises the callback.</param>
        /// <param name="callback">The callback function with the action that should be called.</param>
        /// <param name="modifierKeys">Required state of the modifier keys to get the callback function called.</param>
        /// <param name="once">Flag to determine if the callback function should only be called once.</param>
        public void AddActionKey(VirtualKey key, ActionKeyHandler callback, ModifierKey[] modifierKeys = null, bool once = false)
        {
            actionKeyManager.AddActionKey(key, callback, modifierKeys, once);
        }

        /// <summary>
        /// Removes all ActionKey CallbackFunction binding for the passed VirtualKey.
        /// </summary>
        /// <param name="key">The action key that raises the callbacks.</param>
        public void RemoveActionKey(VirtualKey key)
        {
            actionKeyManager.RemoveActionKey(key);
        }

        /// <summary>
        /// Removes all ActionKey CallbackFunction binding.
        /// </summary>
        public void RemoveAllActionKeys()
        {
            actionKeyManager.RemoveAllActionKeys();
        }

        /// <summary>
        /// Redirect the message loop messages to the ActionKeyManager.
        /// </summary>
        protected override void WndProc(ref Message msg)
        {
            if (actionKeyManager != null && actionKeyManager.HandleKeyMessage(ref msg))
                return;

            base.WndProc(ref msg);
        }

        #endregion
    }
}
