using System.Management;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Helper class that wraps screen related functions.
    /// </summary>
    public class ScreenHelper
    {
        /// <summary>
        /// Gets all possible screen resolution.
        /// </summary>
        /// <returns>Array of possible screen resolutions.</returns>
        public static string[] GetScreenResolutions()
        {
            List<string> resolutions = new List<string>();
#if !MONO
            var scope = new ManagementScope();
            var query = new ObjectQuery("SELECT * FROM CIM_VideoControllerResolution");

            using (var searcher = new ManagementObjectSearcher(scope, query))
            {
                foreach (var result in searcher.Get())
                {
                    string newResolution = string.Format("{0}x{1}", result["HorizontalResolution"], result["VerticalResolution"]);

                    if (!resolutions.Contains(newResolution))
                        resolutions.Add(newResolution);
                }
            }
#else
            // TODO: Get screen resolutions for MONO build
            string newResolution = string.Format("{0}x{1}", Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            if (!resolutions.Contains(newResolution))
                resolutions.Add(newResolution);
#endif

            return resolutions.ToArray();
        }
    }
}
