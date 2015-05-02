using System.Collections.Generic;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// String extension class.
    /// </summary>
    public static class ExtensionsString
    {
        /// <summary>
        /// New overloading of the generic string function Split that normally takes a char as separator.
        /// This function splits takes a string as argument.
        /// Usage:
        /// string str = "bla..bla";
        /// string[] splitStr = str.Split("..");
        /// </summary>
        /// <param name="str">Instance of the string to split.</param>
        /// <param name="separator">The separator string.</param>
        /// <returns>A array of strings.</returns>
        public static string[] Split(this string str, string separator)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(separator))
                return null;

            int sepLength = separator.Length;
            if (sepLength == 1) 
                return str.Split(separator[0]);

            int myLength = str.Length;
            List<string> list = new List<string>();
            string temp = string.Empty;
            for (int startIndex = 0; startIndex < myLength; ++startIndex)
            {
                if (sepLength <= myLength - startIndex)
                {
                    temp = str.Substring(startIndex, sepLength);
                    if (temp == separator)
                    {
                        list.Add(str.Substring(0, startIndex));
                        str = str.Substring(startIndex + sepLength);
                        myLength = str.Length;
                        startIndex = -1;
                    }
                }
            }

            return list.ToArray();
        }
    }  
}
