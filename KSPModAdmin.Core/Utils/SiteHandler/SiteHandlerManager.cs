using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSPModAdmin.Core.Utils
{
    public static class SiteHandlerManager
    {
        public static ISiteHandler[] SiteHandlerArray
        {
            get { return mSiteHandlers.Values.ToArray(); }
        }
        public static Dictionary<string, ISiteHandler> SiteHandler
        {
            get { return mSiteHandlers; }
        }
        private static Dictionary<string, ISiteHandler> mSiteHandlers = new Dictionary<string, ISiteHandler>();


        public static void RegisterSiteHandler(ISiteHandler handler)
        {
            if (!mSiteHandlers.ContainsKey(handler.Name))
                mSiteHandlers.Add(handler.Name, handler);
        }

        public static void RemoveSiteHandler(ISiteHandler handler)
        {
            RemoveSiteHandler(handler.Name);
        }

        public static void RemoveSiteHandler(string handlerName)
        {
            if (!mSiteHandlers.ContainsKey(handlerName))
                mSiteHandlers.Remove(handlerName);
        }

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
