using System;
using System.IO;
using System.Text;

namespace KSPModAdmin.Core.Utils.Logging
{
    /// <summary>
    /// Enum to determine the level of the log message.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// For info messages.
        /// </summary>
        Info = 0,

        /// <summary>
        /// For debug messages.
        /// </summary>
        Debug = 1,

        /// <summary>
        /// For waning messages.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// For error messages.
        /// </summary>
        Error = 3
    }

    /// <summary>
    /// Enum to determine witch log messages should be logged.
    /// </summary>
    public enum LogMode
    {
        /// <summary>
        /// Turns the logging off.
        /// </summary>
        None = 0,

        /// <summary>
        /// Writes all log messages from all levels (Info, Debug, Warning and Error).
        /// </summary>
        All = 1,

        /// <summary>
        /// Writes the Debug, Warning and Error log messages.
        /// </summary>
        DebugWarningsAndErrors = 2,

        /// <summary>
        /// Writes the Warning and Error log messages.
        /// </summary>
        WarningsAndErrors = 3,

        /// <summary>
        /// Writes the Error log messages only.
        /// </summary>
        Errors = 4
    }
    
    /// <summary>
    /// Enum to determine the destination to log to.
    /// </summary>
    public enum LogDestination
    {
        /// <summary>
        /// Writes the log entries to a memory list.
        /// </summary>
        Memory = 0,

        /// <summary>
        /// Writes the log entries immediately to a file.
        /// </summary>
        File = 1

        ////SystemLog = 2
    }

    /// <summary>
    /// Simple class for logging in 4 depth level (Info, Debug, Warning and Error).
    /// The LogMode determines witch LogLevel messages should be logged.
    /// The LogDestination determines if the log should be written to a file or into the memory.
    /// </summary>
    public class Log
    {
        #region Members

        /// <summary>
        /// Singleton instance of a Log that logs all messages to the memory.
        /// To change the depth of logging set GlobalInstance.LogMode to the wanted value.
        /// </summary>
        private static Log mGlobalInstance = null;

        /// <summary>
        /// Full path where the Log should be saved.
        /// </summary>
        private string mPath = string.Empty;

        /// <summary>
        /// Mode of the Log. Determines witch log messages should be saved.
        /// </summary>
        private LogMode mLogMode = LogMode.All;

        /// <summary>
        /// Destination of the log (file or memory).
        /// </summary>
        private LogDestination mLogDestination = LogDestination.Memory;

        /// <summary>
        /// The memory log messages (List for LogMode.Memory).
        /// </summary>
        private StringBuilder mLogList = new StringBuilder();

        #endregion

        #region Properties

        /// <summary>
        /// Singleton instance of a Log that logs all messages to the memory.
        /// To change the depth of logging set GlobalInstance.LogMode property to the wanted value.
        /// To change the LogDestination set the GlobalInstance.FullPath property.
        /// </summary>
        public static Log GlobalInstance
        {
            get
            {
                if (mGlobalInstance == null)
                    mGlobalInstance = new Log();

                return mGlobalInstance;
            }
        }

        /// <summary>
        /// Mode of the Log. Determines witch log messages should be saved.
        /// </summary>
        public LogMode LogMode
        {
            get
            {
                return mLogMode;
            }
            set
            {
                mLogMode = value;
            }
        }
        
        /// <summary>
        /// Destination of the log (file or memory).
        /// </summary>
        public LogDestination LogDestination
        {
            get
            {
                return mLogDestination;
            }
        }

        /// <summary>
        /// Sets the full path of the file to log to.
        /// If set to a none Empty/Null value a Log file will be created (if necessary) and the LogDestination will be changed to LogDestination.File.
        /// </summary>
        public string FullPath
        {
            get
            {
                return mPath;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    mLogDestination = LogDestination.Memory;
                else
                {
                    mLogDestination = LogDestination.File;

                    if (mPath != value)
                        CreateLogFile(value);
                }

                mPath = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a Log instance that logs All LogLevel messages to the memory.
        /// </summary>
        public Log()
        {
            mLogMode = LogMode.All;
            mLogDestination = LogDestination.Memory;
        }

        /// <summary>
        /// Creates a Log instance that logs the wanted LogLevel messages to the memory.
        /// </summary>
        public Log(LogMode logMode)
        {
            mLogMode = logMode;
            mLogDestination = LogDestination.Memory;
        }

        /// <summary>
        /// Creates a Log instance that logs the wanted LogLevel messages immediately to a file.
        /// </summary>
        /// <param name="path">Full path to the file to write the log messages to.</param>
        /// <param name="logMode">The LogMode.</param>
        public Log(string path, LogMode logMode = LogMode.All)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            CreateLogFile(path);

            mPath = path;
            mLogMode = logMode;
            mLogDestination = LogDestination.File;
        }

        #endregion

        #region Static

        /// <summary>
        /// Saves the global memory log to the passed path.
        /// </summary>
        /// <param name="path">The full path to write the log to.</param>
        public static void SaveS(string path)
        {
            Log.GlobalInstance.Save(path);
        }

        /// <summary>
        /// Adds a simple message to the global memory log.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void AddMessageS(string message)
        {
            Log.GlobalInstance.AddMessage(message);
        }

        /// <summary>
        /// Adds a info message to the global memory log.
        /// </summary>
        /// <param name="infoMessage">The info message to log.</param>
        public static void AddInfoS(string infoMessage)
        {
            Log.GlobalInstance.AddInfo(infoMessage);
        }

        /// <summary>
        /// Adds a debug message to the global memory log.
        /// </summary>
        /// <param name="debugMessage">The debug message to log.</param>
        public static void AddDebugS(string debugMessage)
        {
            Log.GlobalInstance.AddDebug(debugMessage);
        }

        /// <summary>
        /// Adds a warning message to the global memory log.
        /// </summary>
        /// <param name="warningMessage">The warning message to log.</param>
        public static void AddWarningS(string warningMessage)
        {
            Log.GlobalInstance.AddWarning(warningMessage);
        }

        /// <summary>
        /// Adds a error message to the global memory log.
        /// </summary>
        /// <param name="errorMessage">The error message to log.</param>
        /// <param name="ex">The exception to log.</param>
        public static void AddErrorS(string errorMessage, Exception ex = null)
        {
            Log.GlobalInstance.AddError(errorMessage, ex);
        }

        /// <summary>
        /// Returns "[ExceptionType] Message: [ExceptionMessage] StackTrace: [ExceptionStackTrace]"
        /// </summary>
        /// <param name="ex">The exception to convert to a string.</param>
        /// <returns>"[ExceptionType] Message: [ExceptionMessage] StackTrace: [ExceptionStackTrace]"</returns>
        public static string Exception2String(Exception ex)
        {
            return string.Format("Exception: {0}{3}Message: {1}{3}StackTrace:{3}{2}", ex.GetType(), ex.Message, ex.StackTrace, Environment.NewLine); ////.Replace("\r", "").Replace("\n", " "));
        }

        /// <summary>
        /// Returns the file length in bytes
        /// </summary>
        /// <returns>The file length in bytes</returns>
        public static long GetLogFileSizeS()
        {
            return Log.GlobalInstance.GetLogFileSize();
        }


        /// <summary>
        /// Deletes the log file. If the ifFileSizeReached parameter is set to a positive value the Log file will only be deleted if the file size is reached.
        /// </summary>
        /// <param name="ifFileSizeReached">The max file size of the Log in kbyte.</param>
        public static void DeleteLogFileS(long ifFileSizeReached = -2)
        {
            Log.GlobalInstance.DeleteLogFile(ifFileSizeReached);
        }

        /////// <summary>
        /////// Returns the error message with the format:
        /////// [{DateTime.Now}] ERROR   : {errorMessage}
        /////// or
        /////// [{DateTime.Now}] ERROR   : {errorMessage} Exception: [ex.GetType] Message: [ex.Message] StackTrace: [ex.StackTrace]
        /////// </summary>
        /////// <param name="errorMessage">The error message to log.</param>
        /////// <param name="ex">The exception to log.</param>
        /////// <returns>
        /////// Returns the error message with the format:
        /////// [{DateTime.Now}] ERROR   : {errorMessage}
        /////// or
        /////// [{DateTime.Now}] ERROR   : {errorMessage} Exception: [ex.GetType] Message: [ex.Message] StackTrace: [ex.StackTrace]
        /////// </returns>
        ////private static string GetErrorMessage(string errorMessage, Exception ex = null)
        ////{
        ////    if (ex == null)
        ////        return string.Format("[{0}] ERROR   : {1}", DateTime.Now, errorMessage);
        ////    else
        ////        return string.Format("[{0}] ERROR   : {1} Exception: {2}", DateTime.Now, errorMessage, Exception2String(ex));
        ////}

        #endregion

        #region Public

        /// <summary>
        /// Saves the memory log to the passed path.
        /// </summary>
        /// <param name="path">The full path to write the log to.</param>
        public void Save(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Parameter is null or empty.", "path");

            File.AppendAllText(path, mLogList.ToString());
        }

        /// <summary>
        /// Adds a simple message to the log.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void AddMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                try
                {
                    // throw the exception but catch it immediately to get the StackTrace.
                    throw new ArgumentException("Parameter is null or empty.", "message");
                }
                catch (Exception ex)
                {
                    AddError("Log.AddMessage failed!", ex);
                }
                
                return;
            }

            if (mLogDestination == LogDestination.Memory || string.IsNullOrEmpty(mPath))
                mLogList.AppendLine(message);
            else
            {
                try
                {
                    File.AppendAllText(mPath, message);
                }
                catch (Exception ex)
                {
                    var msg = string.Format("Log.AddMessage to file \"{0}\" failed! Exception: {1}", mPath, Exception2String(ex));
                    mLogList.AppendLine(string.Format("[{0}] [{1}] Error : {2}", DateTime.Now, System.Threading.Thread.CurrentThread.ManagedThreadId, msg));
                    mLogList.AppendLine(message);
                }
            }
        }

        /// <summary>
        /// Adds a info message to the log.
        /// </summary>
        /// <param name="infoMessage">The info message to log.</param>
        public void AddInfo(string infoMessage)
        {
            if (!LogModeContains(LogLevel.Info)) return;

            AddMessage(infoMessage + Environment.NewLine, LogLevel.Info);
        }

        /// <summary>
        /// Adds a debug message to the log.
        /// </summary>
        /// <param name="debugMessage">The debug message to log.</param>
        public void AddDebug(string debugMessage)
        {
            if (!LogModeContains(LogLevel.Debug)) return;

            AddMessage(debugMessage + Environment.NewLine, LogLevel.Debug);
        }

        /// <summary>
        /// Adds a warning message to the log.
        /// </summary>
        /// <param name="warningMessage">The warning message to log.</param>
        public void AddWarning(string warningMessage)
        {
            if (!LogModeContains(LogLevel.Warning)) return;

            AddMessage(warningMessage + Environment.NewLine, LogLevel.Warning);
        }

        /// <summary>
        /// Adds a error message to the log.
        /// </summary>
        /// <param name="errorMessage">The error message to log.</param>
        /// <param name="ex">The exception.</param>
        public void AddError(string errorMessage, Exception ex = null)
        {
            if (!LogModeContains(LogLevel.Error)) return;

            if (ex == null)
                AddMessage(errorMessage + Environment.NewLine, LogLevel.Error);
            else
			{
				string msg = string.Format("{0}{1}{2}{1}", errorMessage, Environment.NewLine, Exception2String(ex));
				AddMessage(msg, LogLevel.Error);
			}
		}

        /// <summary>
        /// Returns the file length in bytes
        /// </summary>
        /// <returns>The file length in bytes</returns>
        public long GetLogFileSize()
        {
            try
            {
                if (!File.Exists(mPath))
                    return -1;

                FileInfo f = new FileInfo(mPath);
                return f.Length;
            }
            catch (Exception ex)
            {
                AddError("Log.GetLogFileSize failed!", ex);
            }

            return -1;
        }

        /// <summary>
        /// Deletes the log file. If the ifFileSizeReached parameter is set to a positive value the Log file will only be deleted if the file size is reached.
        /// </summary>
        /// <param name="ifFileSizeReached">The max file size of the Log in kbyte.</param>
        public void DeleteLogFile(long ifFileSizeReached = -2)
        {
            try
            {
                if (!File.Exists(mPath))
                    return;

                if (ifFileSizeReached > -2)
                {
                    if (ifFileSizeReached <= (long)(GetLogFileSize() / 1024))
                        File.Delete(mPath);
                }
                else
                {
                    File.Delete(mPath);   
                }
            }
            catch (Exception ex)
            {
                AddError("Log.DeleteLogFile failed!", ex);
            }
        }

        #endregion

        #region Private

        /// <summary>
        /// Creates a new file if necessary.
        /// </summary>
        /// <param name="path">The full path of the file to create.</param>
        private void CreateLogFile(string path)
        {
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                throw new DirectoryNotFoundException(string.Format("Directory not found. \"{0}\"", dir));

            if (!File.Exists(path))
                File.Create(path).Close();
        }

        /// <summary>
        /// Adds a formatted message to the log, determined by the LogLevel.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="logLevel">The LogLevel of the message.</param>
        private void AddMessage(string message, LogLevel logLevel)
        {
            string msg = string.Empty;
            string time = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff");
            int threadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
            switch (logLevel)
            {
                case LogLevel.Info:
                    msg = string.Format("[{0}] [{1}] Info    : {2}", time, threadID, message);
                    break;
                case LogLevel.Debug:
                    msg = string.Format("[{0}] [{1}] Debug   : {2}", time, threadID, message);
                    break;
                case LogLevel.Warning:
                    msg = string.Format("[{0}] [{1}] Warning : {2}", time, threadID, message);
                    break;
                case LogLevel.Error:
                    msg = string.Format("[{0}] [{1}] Error   : {2}", time, threadID, message);
                    break;
            }

            if (LogModeContains(logLevel))
                AddMessage(msg);
        }

        /// <summary>
        /// Checks if the current LogMode contains the passed LogLevel.
        /// </summary>
        /// <param name="logLevel">The LogLevel to check.</param>
        /// <returns>True, if the current LogMode contains the passed LogLevel, otherwise false.</returns>
        private bool LogModeContains(LogLevel logLevel)
        {
            if (mLogMode == LogMode.None) return false;

            switch (logLevel)
            {
                case LogLevel.Info:
                    return (mLogMode == LogMode.All);
                case LogLevel.Debug:
                    return (mLogMode == LogMode.All || mLogMode == LogMode.DebugWarningsAndErrors);
                case LogLevel.Warning:
                    return (mLogMode == LogMode.All || mLogMode == LogMode.DebugWarningsAndErrors || mLogMode == LogMode.WarningsAndErrors);
                case LogLevel.Error:
                    return (mLogMode != LogMode.None);
            }

            return false;
        }

        #endregion
    }
}
