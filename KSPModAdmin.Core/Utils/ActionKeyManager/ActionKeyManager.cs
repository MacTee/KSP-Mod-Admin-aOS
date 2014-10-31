using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Signature of the callback function when a key gets in KeyDown state.
    /// </summary>
    /// <param name="keyState">key state information.</param>
    /// <returns>True if key was handled (In this case no other callback functions will be called).</returns>
    public delegate bool ActionKeyHandler(ActionKeyInfo keyState);

    /// <summary>
    /// List of possible modifier keys that must be pressed to fire ActionKey callback.
    /// </summary>
    public enum ModifierKey
    {
        /// <summary>
        /// Any shift key must be pressed.
        /// </summary>
        AnyShift,

        ///// <summary>
        ///// Left shift key must be pressed.
        ///// </summary>
        //LShift,

        ///// <summary>
        ///// Right shift key must be pressed.
        ///// </summary>
        //RShift,

        /// <summary>
        /// Any control key must be pressed.
        /// </summary>
        AnyControl,

        /// <summary>
        /// Left control key must be pressed.
        /// </summary>
        LControl,

        /// <summary>
        /// Right control key must be pressed.
        /// </summary>
        RControl
        //,

        ///// <summary>
        ///// Any alt key must be pressed.
        ///// </summary>
        //AnyAlt,

        ///// <summary>
        ///// Left alt key must be pressed.
        ///// </summary>
        //LAlt,

        ///// <summary>
        ///// Right alt key must be pressed.
        ///// </summary>
        //RAlt
    }

    /// <summary>
    /// Wraper class to handle actions for keys.
    /// This class checks the Window Message for a WM_KEYDOWN.
    /// If the Manager has a Entry for the pressed key the callback will be called 
    /// to perform the wanted action for that key.
    /// </summary>
    public class ActionKeyManager
    {
        #region Members

        /// <summary>
        /// Windows message loop parameter for the KeyDown event.
        /// </summary>
        private const int WM_KEYUP = 0x0101;
        private const int WM_KEYDOWN = 0x0100;

        private const int WM_LBUTTONUP = 0x0202; // left mouse up
        private const int WM_MBUTTONUP = 0x0208; // middle mouse up
        private const int WM_RBUTTONUP = 0x0205; // right mouse up

        /// <summary>
        /// Dictionary of all added action keys.
        /// </summary>
        private Dictionary<VirtualKey, List<ActionKeyInfo>> m_ActionKeys = new Dictionary<VirtualKey, List<ActionKeyInfo>>();

        /// <summary>
        /// States of the control keys.
        /// </summary>
        private ControlKeysState m_ControlKeysState = new ControlKeysState();

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the ActionKeyManager class.
        /// </summary>
        public ActionKeyManager()
        {
        }

        #endregion

        #region Public

        /// <summary>
        /// Checks if the passed Windows Message is the KeyDown message and
        /// calls the callback function of the ActionKey.
        /// </summary>
        /// <param name="msg">The Windows Message to handle.</param>
        /// <returns>True if key was handled (In this case no other callback functions will be called).</returns>
        public bool HandleKeyMessage(ref Message msg)
        {
            if (msg.Msg != WM_KEYDOWN && msg.Msg != WM_KEYUP)
                return false;

            VirtualKey key = VirtualKey.VK_CANCEL;
            KeyDownLParamHelper kdLPHelper = null;
            try
            {
                key = (VirtualKey)msg.WParam.ToInt32();
                kdLPHelper = new KeyDownLParamHelper(msg.LParam.ToInt32());
            }
            catch (Exception)
            {
                return false;
            }

            if (msg.Msg == WM_KEYDOWN)
                CaptureControlKeysState(key, true, kdLPHelper.Extended);

            if (msg.Msg == WM_KEYDOWN && m_ActionKeys.ContainsKey(key))
            {
                foreach (ActionKeyInfo actionKeyInfo in m_ActionKeys[key])
                {
                    actionKeyInfo.ControlKeysState = m_ControlKeysState;
                    if (actionKeyInfo.CallBack != null && actionKeyInfo.DoModifierKeysMatch() && (!actionKeyInfo.Once || !kdLPHelper.Prev))
                        if (actionKeyInfo.CallBack(actionKeyInfo))
                            return true;
                }
            }

            else if (msg.Msg == WM_KEYUP)
                CaptureControlKeysState(key, false, kdLPHelper.Extended);

            return false;
        }

        /// <summary>
        /// Adds a ActionKey to the list.
        /// </summary>
        /// <param name="key">The key that invokes the action.</param>
        /// <param name="callback">The callback function that should be called when the action key comes down.</param>
        /// <param name="modifierKeys">A list of modifier keys that must be pressed to fire callback.</param>
        /// <param name="once">Fires only once for keydown, the key has to be released and pressed again to fire the callback again.</param>
        public void AddActionKey(VirtualKey key, ActionKeyHandler callback, ModifierKey[] modifierKeys = null, bool once = false)
        {
            if (m_ActionKeys.ContainsKey(key))
                m_ActionKeys[key].Add(new ActionKeyInfo(key, callback, modifierKeys));
            else
                m_ActionKeys.Add(key, new List<ActionKeyInfo>(new ActionKeyInfo[] { new ActionKeyInfo(key, callback, modifierKeys, once) }));
        }
        
        /// <summary>
        /// Removes a ActionKey from the list.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        public void RemoveActionKey(VirtualKey key)
        {
            if (m_ActionKeys.ContainsKey(key))
                m_ActionKeys.Remove(key);
        }

        /// <summary>
        /// Removes all ActionKey from the list.
        /// </summary>
        public void RemoveAllActionKeys()
        {
            m_ActionKeys.Clear();
        }

        #endregion

        #region Private

        /// <summary>
        /// Saves the current key state of the passed control key.
        /// </summary>
        /// <param name="key">The controlkey to save the state for.</param>
        /// <param name="pressState">The state of control key.</param>
        private void CaptureControlKeysState(VirtualKey key, bool pressState, bool right = false)
        {
            switch (key)
            {
                case VirtualKey.VK_SHIFT:
                    m_ControlKeysState.Shift = pressState;
                    break;

                //case VirtualKey.VK_LSHIFT:
                //    m_ControlKeysState.LShift = pressState;
                //    break;
                
                //case VirtualKey.VK_RSHIFT:
                //    m_ControlKeysState.RShift = pressState;
                //    break;

                case VirtualKey.VK_CONTROL:
                    if (right)
                        m_ControlKeysState.RControl = pressState;
                    else
                        m_ControlKeysState.LControl = pressState;
                    break;
                
                //case VirtualKey.VK_LCONTROL:
                //    m_ControlKeysState.LControl = pressState;
                //    break;

                //case VirtualKey.VK_RCONTROL:
                //    m_ControlKeysState.RControl = pressState;
                //    break;

                //case VirtualKey.VK_MENU:
                //    m_ControlKeysState.Alt = pressState;
                //    break;
            }
        }

        #endregion
    }

    public class ControlKeysState
    {
        public bool Shift = false;
        //public bool LShift = false;
        //public bool RShift = false;
        public bool LControl = false;
        public bool RControl = false;
        //public bool Alt = false;
    }

    public class ActionKeyInfo
    {
        #region Members

        /// <summary>
        /// The key that invokes the action.
        /// </summary>
        private VirtualKey m_ActionKey;

        /// <summary>
        /// A list of modifier keys that must be pressed to fire callback.
        /// </summary>
        private ModifierKey[] m_ModifierKeys = null;

        /// <summary>
        /// The callback function.
        /// </summary>
        private ActionKeyHandler m_CallBack = null;

        /// <summary>
        /// Fires only once for keydown, the key has to be released and pressed again to fire the callback again.
        /// </summary>
        private bool m_Once = false;
        
        #endregion

        #region Properties

        /// <summary>
        /// The key that invokes the action.
        /// </summary>
        public VirtualKey ActionKey { get { return m_ActionKey; } }

        /// <summary>
        /// A list of modifier keys that must be pressed to fire callback.
        /// </summary>
        public ModifierKey[] ModifierKeys { get { return m_ModifierKeys; } }

        /// <summary>
        /// The callback function.
        /// </summary>
        public ActionKeyHandler CallBack { get { return m_CallBack; } }

        /// <summary>
        /// Fires only once for keydown, the key has to be released and pressed again to fire the callback again.
        /// </summary>
        public bool Once { get { return m_Once; } }

        /// <summary>
        /// States of the control keys.
        /// </summary>
        public ControlKeysState ControlKeysState { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the ActionKeyInfo class.
        /// </summary>
        /// <param name="key">The key that invokes the action.</param>
        /// <param name="callback">The callback function that should be called when the action key comes down.</param>
        /// <param name="modifierKeys">A list of modifier keys that must be pressed to fire callback.</param>
        /// <param name="once">Fires only once for keydown, the key has to be released and pressed again to fire the callback again.</param>
        public ActionKeyInfo(VirtualKey actionKey, ActionKeyHandler callBack, ModifierKey[] modifierKeys = null, bool once = false)
        {
            m_ActionKey = actionKey;
            m_ModifierKeys = modifierKeys;
            m_CallBack = callBack;
            m_Once = once;

            ControlKeysState = new ControlKeysState();
        }

        #endregion

        #region Public

        /// <summary>
        /// Checks if the required modifier keys ar pressed.
        /// </summary>
        /// <returns>True if the required modifier keys ar pressed, otherwise false.</returns>
        public bool DoModifierKeysMatch()
        {
            if (m_ModifierKeys == null)
                return true;

            bool result = true;
            foreach (ModifierKey modifierKey in m_ModifierKeys)
            {
                switch (modifierKey)
                {
                    case ModifierKey.AnyShift:
                        //if (!Keyboard.IsKeyDown(Keys.LShiftKey) && !Keyboard.IsKeyDown(Keys.RShiftKey))
                        if (!ControlKeysState.Shift)
                            result = false;
                        break;
                    //case ModifierKey.AnyShift:
                    //    //if (!Keyboard.IsKeyDown(Keys.LShiftKey) && !Keyboard.IsKeyDown(Keys.RShiftKey))
                    //    if (!ControlKeysState.LShift && !ControlKeysState.RShift)
                    //        result = false;
                    //    break;
                    //case ModifierKey.LShift:
                    //    //if (!Keyboard.IsKeyDown(Keys.LShiftKey))
                    //    if (!ControlKeysState.LShift)
                    //        result = false;
                    //    break;
                    //case ModifierKey.RShift:
                    //    //if (!Keyboard.IsKeyDown(Keys.RShiftKey))
                    //    if (!ControlKeysState.RShift)
                    //        result = false;
                    //    break;
                    case ModifierKey.AnyControl:
                        //if (!Keyboard.IsKeyDown(Keys.LControlKey) && !Keyboard.IsKeyDown(Keys.RControlKey))
                        if (!ControlKeysState.LControl && !ControlKeysState.RControl)
                            result = false;
                        break;
                    case ModifierKey.LControl:
                        //if (!Keyboard.IsKeyDown(Keys.LControlKey))
                        if (!ControlKeysState.LControl)
                            result = false;
                        break;
                    case ModifierKey.RControl:
                        //if (!Keyboard.IsKeyDown(Keys.RControlKey))
                        if (!ControlKeysState.RControl)
                            result = false;
                        break;
                    //case ModifierKey.AnyAlt:
                    //    //if (!Keyboard.IsKeyDown(Keys.Alt))
                    //    if (!ControlKeysState.Alt)
                    //        result = false;
                    //    break;
                    //case ModifierKey.LAlt:
                    //    //if (!(Keyboard.IsKeyDown(Keys.Alt) && !Keyboard.IsKeyDown(Keys.LControlKey) && !Keyboard.IsKeyDown(Keys.RControlKey)))
                    //    if (!ControlKeysState.Alt && !ControlKeysState.LControl && !ControlKeysState.RControl)
                    //            result = false;
                    //    break;
                    //case ModifierKey.RAlt:
                    //    //if (!(Keyboard.IsKeyDown(Keys.Alt) && (Keyboard.IsKeyDown(Keys.LControlKey) || Keyboard.IsKeyDown(Keys.RControlKey))))
                    //    if (!ControlKeysState.Alt && (ControlKeysState.LControl || ControlKeysState.RControl))
                    //        result = false;
                    //    break;
                }

                if (!result)
                    break;
            }

            return result;
        }

        #endregion
    }

    /// <summary>
    /// Helper class to get KeyState parameter from the LParam of the Windows Message.
    /// </summary>
    public class KeyDownLParamHelper
    {
        #region Members

        /// <summary>
        /// The Windows Message lParam.
        /// </summary>
        private int m_LParam = 0;

        /// <summary>
        /// The lParam as a BitArray.
        /// </summary>
        private BitArray m_BitArray = null;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Windows Message lParam.
        /// </summary>
        public int LParam { get { return m_LParam; } }

        /// <summary>
        /// Gets the Repeat Count.
        /// </summary>
        public UInt16 RepeatCount
        {
            get
            {
                return BitConverter.ToUInt16(System.BitConverter.GetBytes(m_LParam), 0);
            }
        }

        /// <summary>
        /// Gets the ScanCode.
        /// </summary>
        public byte ScanCode
        {
            get
            {
                byte[] bArray = new byte[4];
                m_BitArray.CopyTo(bArray, 0);

                return bArray[2];
            }
        }

        /// <summary>
        /// Gets the Extended.
        /// </summary>
        public bool Extended
        {
            get
            {
                return m_BitArray[24];
                //return (m_LParam & 0x25) == 1;
            }
        }

        /// <summary>
        /// Gets the Reserved.
        /// </summary>
        public BitArray Reserved
        {
            get
            {
                BitArray larrBits = new BitArray(System.BitConverter.GetBytes(m_LParam));
                BitArray bArray = new BitArray(4);
                bArray[0] = larrBits[25];
                bArray[1] = larrBits[26];
                bArray[2] = larrBits[27];
                bArray[3] = larrBits[28];

                return bArray;
            }
        }

        /// <summary>
        /// Gets the Context.
        /// </summary>
        public bool Context
        {
            get
            {
                return m_BitArray[29];
                //return (m_LParam & 0x30) == 1;
            }
        }

        /// <summary>
        /// Gets the Prev.
        /// </summary>
        public bool Prev
        {
            get
            {
                return m_BitArray[30];
                //return (m_LParam & 0x31) == 1;
            }
        }

        /// <summary>
        /// Gets the Trans.
        /// </summary>
        public bool Trans
        {
            get
            {
                return m_BitArray[31];
                //return (m_LParam & 0x32) == 1;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the KeyDownLParamHelper class.
        /// </summary>
        /// <param name="lParam">The Windows Message lParam.</param>
        /// <param name="modifierKeys">The mofidier key states.</param>
        public KeyDownLParamHelper(int lParam)
        {
            m_LParam = lParam;
            m_BitArray = new BitArray(System.BitConverter.GetBytes(m_LParam));
        }

        #endregion
    }
}

/* Sample usage:
 * 
 * private ActionKeyManager m_ActionKeyManager = null;
 * 
 * // Add a ActionKey with callback
 * protected void InitStuff()
 * {
 *      m_ActionKeyManager = new ActionKeyManager();
 *      m_ActionKeyManager.AddActionKey(VirtualKey.VK_DELETE, Handle_DeleteKey);
 * }
 * 
 * // Override the WndProc function of your control and add the handling function of the ActionKeyManager.
 * protected override void WndProc(ref System.Windows.Forms.Message msg)
 * {
 *     if (m_ActionKeyManager != null && m_ActionKeyManager.HandleKeyMessage(ref msg))
 *         return;
 *
 *     base.WndProc(ref msg);
 * }
 * 
 * // Declaration of the callback function.
 * protected bool Handle_DeleteKey(ActionKeyInfo actionKeyInfo)
 * {
 *     // performe the action you want.
 * }
 *
 */
