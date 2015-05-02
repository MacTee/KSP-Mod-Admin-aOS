using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KSPMODAdmin.Core.Utils
{
    /// <summary>
    /// Class for a easy way to retrieve key states of the keyboard.
    /// </summary>
    public abstract class Keyboard
    {
        [Flags]
        private enum KeyStates
        {
            None = 0,
            Down = 1,
            Toggled = 2
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        private static KeyStates GetKeyState(Keys key)
        {
            KeyStates state = KeyStates.None;

            short retVal = GetKeyState((int)key);

            // If the high-order bit is 1, the key is down
            // otherwise, it is up.
            if ((retVal & 0x8000) == 0x8000)
                state |= KeyStates.Down;

            // If the low-order bit is 1, the key is toggled.
            if ((retVal & 1) == 1)
                state |= KeyStates.Toggled;

            return state;
        }

        /// <summary>
        /// Checks if the passed key is down.
        /// </summary>
        /// <param name="key">The keyboard key to check for down state.</param>
        /// <returns>True if key is down.</returns>
        public static bool IsKeyDown(Keys key)
        {
            return KeyStates.Down == (GetKeyState(key) & KeyStates.Down);
        }

        /// <summary>
        /// Checks if the passed key was toggled.
        /// </summary>
        /// <param name="key">The keyboard key to check for down state.</param>
        /// <returns>True if key was toggled.</returns>
        public static bool IsKeyToggled(Keys key)
        {
            return KeyStates.Toggled == (GetKeyState(key) & KeyStates.Toggled);
        }
    }
}
