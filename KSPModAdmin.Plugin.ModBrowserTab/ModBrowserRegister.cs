using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSPModAdmin.Plugin.ModBrowserTab
{
    /// <summary>
    /// Class that contains all registered ModBrowsers.
    /// </summary>
    public class ModBrowserRegister
    {
        #region Properties

        /// <summary>
        /// Dictionary of all registered ModBrowser.
        /// </summary>
        public Dictionary<string, IKSPMAModBrowser> ModBrowserList { get; protected set; }

        /// <summary>
        /// Gets the ModBrowser with the matching name or null.
        /// </summary>
        /// <param name="modBrowserName">Name of the ModBrowser to get.</param>
        /// <returns>The ModBrowser with the matching name or null.</returns>
        public IKSPMAModBrowser this[string modBrowserName] { get { return GetModBrowser(modBrowserName); } }

        #endregion

        /// <summary>
        /// Creates a new instance of the class ModBrowserRegister.
        /// </summary>
        public ModBrowserRegister()
        {
            ModBrowserList = new Dictionary<string, IKSPMAModBrowser>();
        }

        /// <summary>
        /// Adds and registers the passed ModBrowser if it's not already added.
        /// </summary>
        /// <param name="modBrowser">The ModBrowser to add.</param>
        /// <returns>True if add was successful, otherwise false.</returns>
        public bool Add(IKSPMAModBrowser modBrowser)
        {
            if (!ModBrowserList.ContainsKey(modBrowser.ModBrowserName))
            {
                ModBrowserList.Add(modBrowser.ModBrowserName, modBrowser);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the ModBrowser with the matching name or null.
        /// </summary>
        /// <param name="modBrowserName">Name of the ModBrowser to get.</param>
        /// <returns>The ModBrowser with the matching name or null.</returns>
        public IKSPMAModBrowser GetModBrowser(string modBrowserName)
        {
            if (!ModBrowserList.ContainsKey(modBrowserName))
                return null;

            return ModBrowserList[modBrowserName];
        }

        /// <summary>
        /// Removes the passed ModBrowser from the register.
        /// </summary>
        /// <param name="modBrowser">The ModBrowser to remove.</param>
        /// <returns>True if remove was successful, otherwise false.</returns>
        public bool Remove(IKSPMAModBrowser modBrowser)
        {
            return RemoveByName(modBrowser.ModBrowserName);
        }

        /// <summary>
        /// Removes the ModBrowser with the passed name from the register.
        /// </summary>
        /// <param name="modBrowserName">The name of the ModBrowser to remove.</param>
        /// <returns>True if remove was successful, otherwise false.</returns>
        public bool RemoveByName(string modBrowserName)
        {
            var mb = GetModBrowser(modBrowserName);
            if (mb != null)
            {
                ModBrowserList.Remove(mb.ModBrowserName);
                return true;
            }

            return false;
        }
    }
}
