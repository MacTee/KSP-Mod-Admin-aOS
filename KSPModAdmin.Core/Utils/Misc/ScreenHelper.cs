using System;
using System.Linq;
using System.Management;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using KSPModAdmin.Core.Utils.Logging;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Helper class that wraps screen related functions.
    /// </summary>
    public class ScreenHelper
    {
        /// <summary>
        /// Display settings properties class.
        /// </summary>
        public class DisplaySettings
        {
            #region Properties

            /// <summary>
            /// Gets or sets the bits per pixel.
            /// </summary>
            /// <value>The bits per pixel.</value>
            public int BitsPerPixel { get; set; }

            /// <summary>
            /// Gets or sets the width.
            /// </summary>
            /// <value>The width.</value>
            public int Width { get; set; }

            /// <summary>
            /// Gets or sets the height.
            /// </summary>
            /// <value>The height.</value>
            public int Height { get; set; }

            /// <summary>
            /// Gets or sets the flags.
            /// </summary>
            /// <value>The flags.</value>
            public int Flags { get; set; }

            /// <summary>
            /// Gets or sets the frequency.
            /// </summary>
            /// <value>The frequency.</value>
            public int Frequency { get; set; }

            #endregion


            /// <summary>
            /// Creates a string of the format "bpp:{0};w:{1};h:{2};f:{3};freq:{4}".
            /// </summary>
            /// <returns>The "bpp:{0};w:{1};h:{2};f:{3};freq:{4}" formated string.</returns>
            public override string ToString()
            {
                return string.Format("bpp:{0};w:{1};h:{2};f:{3};freq:{4}", BitsPerPixel, Width, Height, Flags, Frequency);
            }
        }

#if !__MonoCS__
        /// <summary>
        /// Gets all available display settings.
        /// </summary>
        /// <returns>Array of possible screen resolutions.</returns>
        public static List<DisplaySettings> GetDisplaySettings()
        {
            var result = new Dictionary<string, DisplaySettings>();
            var devMode = new NativeMethods.DEVMODE();
            var modeNumber = 0;
            while (NativeMethods.EnumDisplaySettings(null, modeNumber++, ref devMode))
            {
                var ds = new DisplaySettings()
                   {
                       BitsPerPixel = devMode.dmBitsPerPel,
                       Flags = devMode.dmDisplayFlags,
                       Frequency = devMode.dmDisplayFrequency,
                       Height = devMode.dmPelsHeight,
                       Width = devMode.dmPelsWidth
                   };

                if (!result.ContainsKey(ds.ToString()))
                    result.Add(ds.ToString(), ds);
            }

            return result.Values.ToList();
        }
#endif

        /// <summary>
        /// Gets all possible screen resolution.
        /// </summary>
        /// <returns>Array of possible screen resolutions.</returns>
        public static string[] GetScreenResolutions()
        {
            List<string> resolutions = new List<string>();
            if (PlatformHelper.GetPlatform() == Platform.Linux)
                resolutions = GetResolutionsOnLinux();
            else if (PlatformHelper.GetPlatform() == Platform.Win)
                resolutions = GetResolutionsViaNativeMethods();
            else
                resolutions = GetResolutionsViaWMI();

            if (resolutions.Count == 0)
            {
                string newResolution = GetResolutionString(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                if (!resolutions.Contains(newResolution))
                    resolutions.Add(newResolution);
            }

            return resolutions.ToArray();
        }

        /// <summary>
        /// Gets a resolution string with the format "{width}x{height}".
        /// </summary>
        /// <param name="width">Width of the resolution.</param>
        /// <param name="height">Height of the resolution.</param>
        /// <returns>A resolution string with the format "{width}x{height}".</returns>
        public static string GetResolutionString(int width, int height)
        {
            return string.Format("{0}x{1}", width, height);
        }

        /// <summary>
        /// Gets the width and height of a resolutions string with the format "{width}x{height}".
        /// </summary>
        /// <param name="resolutionString">The resolution string to parse.</param>
        /// <param name="width">Out: The parsed width of the resolution string.</param>
        /// <param name="height">Out: The parsed height of the resolution string.</param>
        /// <returns>True if the values of width and height are > 0, otherwise false.</returns>
        public static bool GetResolutionFromString(string resolutionString, ref int width, ref int height)
        {
            width = height = 0;

            if (!string.IsNullOrEmpty(resolutionString))
            {
                string[] values = resolutionString.Trim().Split('x');
                if (values.Length == 2)
                {
                    width = int.Parse(values[0].Trim());
                    height = int.Parse(values[1].Trim());
                }
            }

            return width > 0 && height > 0;
        }


        private static List<string> GetResolutionsViaWMI()
        {
            List<string> resolutions = new List<string>();

            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM CIM_VideoControllerResolution"))
                {
                    ManagementObjectCollection results = searcher.Get();
                    foreach (var result in results)
                    {
                        string newResolution = GetResolutionString(unchecked((int)((uint)result["HorizontalResolution"])), unchecked((int)((uint)result["VerticalResolution"])));
                        if (!resolutions.Contains(newResolution))
                            resolutions.Add(newResolution);
                    }
                }
            }
            catch (Exception ex) 
            {
                Log.AddErrorS("Error in ScreenHelper.GetResolutionsViaWMI()", ex);
            }

            return resolutions;
        }

        private static List<string> GetResolutionsViaNativeMethods()
        {
            List<string> resolutions = new List<string>();

#if !__MonoCS__
            var list = GetDisplaySettings();

            foreach (DisplaySettings displaySettings in list)
            {
                string newResolution = GetResolutionString(displaySettings.Width, displaySettings.Height);
                if (!resolutions.Contains(newResolution))
                    resolutions.Add(newResolution);
            }
#endif
            return resolutions;
        }

        private static List<string> GetResolutionsOnLinux()
        {
            List<string> resolutions = new List<string>();

            var output = GetXrandrOutput();
            var matches = Regex.Matches(output, @"   (\d+)x(\d+)  ");
            if (matches.Count > 0)
            { 
                foreach (Match match in matches)
                {
                    string newResolution = GetResolutionString(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                    if (!resolutions.Contains(newResolution))
                        resolutions.Add(newResolution);
                }
            }
            else
            {
                var match = Regex.Match(output, @"(\d+)x(\d+)\+0\+0");
                if (match.Success)
                    resolutions.Add(GetResolutionString(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
            }
      
            return resolutions;
        }

        private static string GetXrandrOutput()
        {
            // Use xrandr to get size of screen located at offset (0,0).
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "xrandr";
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output;
            
////            return @"
////Screen 0: minimum 640 x 400, current 1600 x 1200, maximum 1600 x 1200
////default connected 1600x1200+0+0 0mm x 0mm
////   1600x1200       0.0* 
////   1280x1024       0.0  
////   1152x864        0.0  
////   1024x768        0.0  
////   800x600         0.0  
////   640x480         0.0  
////   720x400         0.0";
        }
    }
}
