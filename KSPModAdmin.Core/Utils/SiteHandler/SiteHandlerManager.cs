using System.Collections.Generic;
using System.Linq;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Manages all available SiteHandlers.
    /// </summary>
    public static class SiteHandlerManager
    {
        /// <summary>
        /// Array of all available SiteHandler.
        /// </summary>
        public static ISiteHandler[] SiteHandlerArray
        {
            get { return mSiteHandlers.Values.ToArray(); }
        }

        /// <summary>
        /// Dictionary of all available SiteHandler. 
        /// </summary>
        public static Dictionary<string, ISiteHandler> SiteHandler
        {
            get { return mSiteHandlers; }
        }
        private static Dictionary<string, ISiteHandler> mSiteHandlers = new Dictionary<string, ISiteHandler>();


        /// <summary>
        /// Registers a SiteHandler to the Manager.
        /// </summary>
        /// <param name="handler">The SiteHandler to register.</param>
        public static void RegisterSiteHandler(ISiteHandler handler)
        {
            if (!mSiteHandlers.ContainsKey(handler.Name))
                mSiteHandlers.Add(handler.Name, handler);
        }

        /// <summary>
        /// Removes a SiteHandler from the Manager.
        /// </summary>
        /// <param name="handler">The SiteHandler to remove.</param>
        public static void RemoveSiteHandler(ISiteHandler handler)
        {
            RemoveSiteHandler(handler.Name);
        }

        /// <summary>
        /// Removes a SiteHandler from the Manager.
        /// </summary>
        /// <param name="handlerName">The name of the SiteHandler to remove.</param>
        public static void RemoveSiteHandler(string handlerName)
        {
            if (!mSiteHandlers.ContainsKey(handlerName))
                mSiteHandlers.Remove(handlerName);
        }

        /// <summary>
        /// Gets a SiteHandler by URL.
        /// </summary>
        /// <param name="url">The URL of the SiteHandler.</param>
        /// <returns>The SiteHandler corresponding to the URL.</returns>
        public static ISiteHandler GetSiteHandlerByURL(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            foreach (ISiteHandler handler in SiteHandlerArray)
            {
                if (handler.IsValidURL(url))
                    return handler;
            }

            return null;
        }

        /// <summary>
        /// Gets a SiteHandler by name.
        /// </summary>
        /// <param name="name">The name of a SiteHandler.</param>
        /// <returns>The SiteHandler corresponding to the name.</returns>
        public static ISiteHandler GetSiteHandlerByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            foreach (ISiteHandler handler in SiteHandlerArray)
            {
                if (handler.Name == name)
                    return handler;
            }

            return null;
        }
    }
}
