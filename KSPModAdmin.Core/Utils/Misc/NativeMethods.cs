using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Wraps some Win32 stuff
    /// </summary>
    public class NativeMethods
    {
#if !MONOBUILD
        public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
#endif

        public const int HWND_BROADCAST = 0xffff,
                         WM_NCHITTEST = 0x0084,
                         WM_NCACTIVATE = 0x0086,
                         WS_EX_NOACTIVATE = 0x08000000,
                         HTTRANSPARENT = -1,
                         HTLEFT = 10,
                         HTRIGHT = 11,
                         HTTOP = 12,
                         HTTOPLEFT = 13,
                         HTTOPRIGHT = 14,
                         HTBOTTOM = 15,
                         HTBOTTOMLEFT = 16,
                         HTBOTTOMRIGHT = 17,
                         WM_USER = 0x0400,
                         WM_REFLECT = WM_USER + 0x1C00,
                         WM_COMMAND = 0x0111,
                         CBN_DROPDOWN = 7,
                         WM_GETMINMAXINFO = 0x0024;

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public Point reserved;
            public Size maxSize;
            public Point maxPosition;
            public Size minTrackSize;
            public Size maxTrackSize;
        }


        public static int HIWORD(int n)
        {
            return (n >> 16) & 0xffff;
        }

        public static int HIWORD(IntPtr n)
        {
            return HIWORD(unchecked((int)(long)n));
        }

        public static int LOWORD(int n)
        {
            return n & 0xffff;
        }

        public static int LOWORD(IntPtr n)
        {
            return LOWORD(unchecked((int)(long)n));
        }

#if !MONOBUILD
        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);
#endif
    }
}
