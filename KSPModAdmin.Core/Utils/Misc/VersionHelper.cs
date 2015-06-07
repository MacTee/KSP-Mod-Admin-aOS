using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

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

        /// <summary>
        /// Gets the last change date of the executing or entry assembly.
        /// Retrieves the LastWriteTime from the assembly FileInfo.
        /// </summary>
        /// <param name="entryAssembly">Flag to determine if the enter assembly or the executing assembly should be used to acquire the version from.</param>
        /// <returns>The last change date of the executing or entry assembly.</returns>
        public static DateTime GetChangeDate2(bool entryAssembly = true)
        {
            Assembly assembly = (entryAssembly) ? Assembly.GetEntryAssembly() : Assembly.GetExecutingAssembly();
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(assembly.Location);
            return fileInfo.LastWriteTime;
        }

        /// <summary>
        /// Gets the last change date of the executing or entry assembly.
        /// Retrieves linker TimeStamp from the assembly's PE header.
        /// </summary>
        /// <param name="entryAssembly">Flag to determine if the enter assembly or the executing assembly should be used to acquire the version from.</param>
        /// <returns>The last change date of the executing or entry assembly.</returns>
        public static DateTime GetChangeDate(bool entryAssembly = true)
        {
            string filePath = (entryAssembly) ? Assembly.GetEntryAssembly().Location : Assembly.GetExecutingAssembly().Location;
            const int PeHeaderOffset = 60;
            const int LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            System.IO.Stream s = null;

            try
            {
                s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                    s.Close();
            }

            int i = BitConverter.ToInt32(b, PeHeaderOffset);
            int secondsSince1970 = BitConverter.ToInt32(b, i + LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
            return dt;
        }
    }

    public enum Platform
    {
        Unknown,
        Win,
        Linux,
        OsX
    }

    public static class PlatformHelper
    {
        public static Platform GetPlatform()
        {
            if (Path.DirectorySeparatorChar == '\\')
                return Platform.Win;
            else if (DetectUnixKernal() == "Darwin")
                return Platform.OsX;
			else if (Environment.OSVersion.Platform == PlatformID.Unix)
                return Platform.Linux;
			else
                return Platform.Unknown;
        }

        //From Managed.Windows.Forms/XplatUI
        [DllImport("libc")]
        static extern int uname(IntPtr buf);

        private static string DetectUnixKernal()
        {
            IntPtr buf = IntPtr.Zero;
            try
            {
                buf = Marshal.AllocHGlobal(8192);
                // This is a hacktastic way of getting sysname from uname ()
                if (uname(buf) == 0)
                {
                    string os = Marshal.PtrToStringAnsi(buf);
                    return os;
                }
            }
            catch
            {
            }
            finally
            {
                if (buf != IntPtr.Zero)
                    Marshal.FreeHGlobal(buf);
            }

            return "Unknown";
        }
    }
}
