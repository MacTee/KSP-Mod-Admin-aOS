using System;
using System.Collections.Generic;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Global message distributor that delivers the messages to its listeners.
    /// </summary>
    public static class Messenger
    {
        /// <summary>
        /// List of all listeners.
        /// </summary>
        static List<IMessageReceiver> mListeners = new List<IMessageReceiver>(); 


        /// <summary>
        /// Static constructor.
        /// </summary>
        static Messenger()
        {
            
        }


        /// <summary>
        /// Adds a new listener to the receiver list.
        /// </summary>
        /// <param name="receiver">The new receiver to add.</param>
        public static void AddListener(IMessageReceiver receiver)
        {
            mListeners.Add(receiver);
        }

        /// <summary>
        /// Removes a listener from the receiver list.
        /// </summary>
        /// <param name="receiver">The receiver to remove.</param>
        public static void RemoveListener(IMessageReceiver receiver)
        {
            mListeners.Remove(receiver);
        }


        /// <summary>
        /// Sends a message to all receivers.
        /// </summary>
        /// <param name="msg">The message to send.</param>
        public static void AddMessage(string msg)
        {
            foreach (var listener in mListeners)
                listener.AddMessage(msg);
        }

        /// <summary>
        /// Sends a info message to all receivers.
        /// </summary>
        /// <param name="msg">The info message to send.</param>
        public static void AddInfo(string msg)
        {
            foreach (var listener in mListeners)
                listener.AddInfo(msg);
        }

        /// <summary>
        /// Sends a debug message to all receivers.
        /// </summary>
        /// <param name="msg">The debug message to send.</param>
        public static void AddDebug(string msg)
        {
            foreach (var listener in mListeners)
                listener.AddDebug(msg);
        }

        /// <summary>
        /// Sends a warning message to all receivers.
        /// </summary>
        /// <param name="msg">The warning message to send.</param>
        public static void AddWarning(string msg)
        {
            foreach (var listener in mListeners)
                listener.AddWarning(msg);
        }

        /// <summary>
        /// Sends a error message to all receivers.
        /// </summary>
        /// <param name="msg">The error message to send.</param>
        public static void AddError(string msg, Exception ex = null)
        {
            foreach (var listener in mListeners)
                listener.AddError(msg, ex);
        }
    }

    /// <summary>
    /// The interface a message receiver has to implement to hook to the Messenger and to receive messages from it.
    /// </summary>
    public interface IMessageReceiver
    {
        /// <summary>
        /// Callback of the message receiver for messages.
        /// </summary>
        /// <param name="msg">The message.</param>
        void AddMessage(string msg);

        /// <summary>
        /// Callback of the message receiver for info messages.
        /// </summary>
        /// <param name="msg">The info message.</param>
        void AddInfo(string msg);

        /// <summary>
        /// Callback of the message receiver for debug messages.
        /// </summary>
        /// <param name="msg">The debug message.</param>
        void AddDebug(string msg);

        /// <summary>
        /// Callback of the message receiver for warning messages.
        /// </summary>
        /// <param name="msg">The warning message.</param>
        void AddWarning(string msg);

        /// <summary>
        /// Callback of the message receiver for error messages.
        /// </summary>
        /// <param name="msg">The error message.</param>
        void AddError(string msg, Exception ex = null);
    }
}
