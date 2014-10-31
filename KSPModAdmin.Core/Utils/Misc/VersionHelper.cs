using System.Diagnostics;
using System.Reflection;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Helper class to get the version from the executing assembly.
    /// </summary>
    public static class VersionHelper
    {
        /// <summary>
        /// Returns the FileVersion of the executing assembly.
        /// </summary>
        /// <param name="longVersionNumber">Flag to determine if the long or short version number should be returned.</param>
        /// <param name="entryAssembly">Flag to determine if the enter assembly or the executing assembly should be used to acquire the version from.</param>
        /// <returns>The FileVersion of the executing assembly.</returns>
        public static string GetAssemblyVersion(bool longVersionNumber = true, bool entryAssembly = true)
        {
            // Get assembly version.
            Assembly assembly = (entryAssembly) ? Assembly.GetEntryAssembly() : Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            if (longVersionNumber)
                return string.Format("{0}.{1}.{2}.{3}", fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart, fvi.FilePrivatePart);
            else
                return string.Format("{0}.{1}.{2}", fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart);
        }
    }
}
