using System;

namespace KSPModAdmin.Plugin.PartsAndCraftsTab.Helper
{
    /// <summary>
    /// Helper class that contains functions to manipulate part.cfg and *.craft.
    /// </summary>
    public class CfgFileHelper
    {
        /// <summary>
        /// Gets the index of the parameter / name combination within the passed text.
        /// The Index points to the beginning of the parameter value.
        /// </summary>
        /// <param name="text">The text to search in.</param>
        /// <param name="parameterName">The parameter name to search with.</param>
        /// <param name="value">The parameter value to search with.</param>
        /// <param name="startIndex">Start index to start the search from.</param>
        /// <returns>The index that is points to the beginning of the value or -1.</returns>
        public static int GetIndexOfParameter(string text, string parameterName, string value, int startIndex = 0, bool behindMatch = true)
        {
            int index = GetIndexOf(text, string.Format("{0} = {1}", parameterName, value), startIndex, behindMatch);
            if (behindMatch && index >= 0)
                return index - value.Length;

            if (index < 0)
                index = GetIndexOf(text, string.Format("{0} ={1}", parameterName, value), startIndex, behindMatch);
            if (behindMatch && index >= 0)
                return index - value.Length;

            if (index < 0)
                index = GetIndexOf(text, string.Format("{0}= {1}", parameterName, value), startIndex, behindMatch);
            if (behindMatch && index >= 0)
                return index - value.Length;

            if (index < 0)
                index = GetIndexOf(text, string.Format("{0}={1}", parameterName, value), startIndex, behindMatch);
            if (behindMatch && index >= 0)
                return index - value.Length;

            return index;
        }

        /// <summary>
        /// Gets the index behind the search text.
        /// </summary>
        /// <param name="text">The text to search in.</param>
        /// <param name="searchString">The string to search for.</param>
        /// <param name="startIndex">Start index to start the search from.</param>
        /// <returns>The index behind the searchText or -1.</returns>
        public static int GetIndexOf(string text, string searchString, int startIndex = 0, bool behindMatch = true)
        {
            int index = text.IndexOf(searchString, startIndex, StringComparison.CurrentCultureIgnoreCase);
            if (behindMatch && index >= 0)
                index += searchString.Length;

            return index;
        }
    }
}