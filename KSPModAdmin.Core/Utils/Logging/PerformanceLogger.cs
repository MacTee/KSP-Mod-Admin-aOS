using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Utils.Logging
{
/// <summary>
/// This is a simple performance logger.
/// The class is designed to measure the execution time of functions and its sub functions.
/// NOTE: The PerformanceLogger can't be used with threaded (sub) functions. 
///       To analyses functions running in another thread, create a new instance of the PerformanceLogger (for each tread).
///       A "How to use" sample is at the bottom of this code.
/// </summary>
public class PerformanceLogger
{
    #region "Members"
    /// <summary>
    /// Name for the root watch.
    /// </summary>
    private const string ROOTNAME = "TimeLogger_RootWatch";

    /// <summary>
    /// Global instance of the PerformanceLogger.
    /// </summary>
    private static PerformanceLogger mInstance = null;

    /// <summary>
    /// The root watch.
    /// </summary>
    private WatchInfo mRootWatch = null;

    /// <summary>
    /// Stack of the last used (created) watches.
    /// </summary>
    private Stack mStackTable = new Stack();
    #endregion

    #region "Properties"
    /// <summary>
    /// Global instance of the PerformanceLogger.
    /// </summary>
    public static PerformanceLogger Instance
    {
        get
        {
            if (((mInstance == null)))
            {
                mInstance = new PerformanceLogger();
            }
            return mInstance;
        }
    }

    /// <summary>
    /// Turns off the timekeeping of the PerformanceLogger.
    /// </summary>
    private bool TurnOff { get; set; }
    #endregion

    #region "Shared"
    /// <summary>
    /// Turns off the timekeeping of the global instance of the PerformanceLogger.
    /// </summary>
    public static bool TurnOff_Shared
    {
        get { return Instance.TurnOff; }
        set { Instance.TurnOff = value; }
    }

    /// <summary>
    /// Starts the current watch.
    /// If there is no current watch a new watch will be created and started.
    /// If the current watch is already running, a new watch will be created, started and added as a child,
    /// the current watch will be pushed on a stack and the created watch becomes the current watch.
    /// </summary>
    /// <param name="name">Name of the watch.</param>
    /// <param name="stopAndResetChilds">Flag for stopping and resetting all child watches of the current watch.</param>
    public static void Start(string name, bool stopAndResetChilds = false)
    {
        Instance.StartWatch(name, stopAndResetChilds);
    }

    /// <summary>
    /// Stops the current watch and pops it from the stack.
    /// </summary>
    /// <param name="stopChilds">Flag for stopping all child watches of the current watch.</param>
    public static void Stop(bool stopChilds = false)
    {
        Instance.StopWatch(stopChilds);
    }

    /// <summary>
    /// Stops and resets the current watch and pops it from the stack.
    /// </summary>
    /// <param name="resetChilds">Flag for resetting all child watches of the current watch.</param>
    public static void Reset(bool resetChilds = false)
    {
        Instance.ResetWatch(resetChilds);
    }

    /// <summary>
    /// Clears all watches.
    /// </summary>
    public static void ClearAll()
    {
        Instance.ClearAllWatches();
    }

    /// <summary>
    /// Saves the logged times.
    /// </summary>
    public static void Save(string fileName)
    {
        Instance.SaveToFile(fileName);
    }

    /// <summary>
    /// Creates a string of the tree of watches.
    /// </summary>
    /// <returns>A string of the tree of watches.</returns>
    public static string ToString_Shared()
    {
        return Instance.ToString();
    }

    /// <summary>
    /// Shows the logged times in a message box.
    /// </summary>
    public static void Show_MsgBox()
    {
        if ((TurnOff_Shared))
            return;

        MessageBox.Show(Instance.ToString());
    }
    #endregion

    #region "Public"
    /// <summary>
    /// Starts the current watch.
    /// If there is no current watch a new watch will be created and started.
    /// If the current watch is already running, a new watch will be created, started and added as a child,
    /// the current watch will be pushed on a stack and the created watch becomes the current watch.
    /// </summary>
    /// <param name="name">Name of the watch.</param>
    /// <param name="stopAndResetChilds">Flag for stopping and resetting all child watches of the current timer.</param>
    public void StartWatch(string name, bool stopAndResetChilds = false)
    {
        if ((TurnOff))
            return;

        if ((mStackTable.Count == 0))
        {
            mStackTable.Push(new WatchInfo(name, GetRoot()));
        }

        dynamic wInfo = (WatchInfo)mStackTable.Peek();
        if ((wInfo.IsRunning))
        {
            dynamic newWInfo = new WatchInfo(name, wInfo);
            mStackTable.Push(newWInfo);
            newWInfo.RestartWatch(stopAndResetChilds);
        }
        else
        {
            wInfo.RestartWatch(stopAndResetChilds);
        }
    }

    /// <summary>
    /// Stops the current watch and pops it from the stack.
    /// </summary>
    /// <param name="stopChilds">Flag for stopping all child watches of the current watch.</param>
    public void StopWatch(bool stopChilds = false)
    {
        if ((TurnOff))
            return;

        if ((mStackTable.Count > 0))
        {
            dynamic wInfo = (WatchInfo)mStackTable.Peek();
            wInfo.StopWatch(stopChilds);
            mStackTable.Pop();
        }
    }

    /// <summary>
    /// Stops and resets the current watch and pops it from the stack.
    /// </summary>
    /// <param name="resetChilds">Flag for resetting all child watches of the current watch.</param>
    public void ResetWatch(bool resetChilds = false)
    {
        if ((TurnOff))
            return;

        if ((mStackTable.Count > 0))
        {
            dynamic wInfo = (WatchInfo)mStackTable.Peek();
            wInfo.ResetWatch(resetChilds);
            mStackTable.Pop();
        }
    }

    /// <summary>
    /// Clears all watches.
    /// </summary>
    public void ClearAllWatches()
    {
        if ((TurnOff))
            return;

        mRootWatch = null;
        mStackTable.Clear();
    }

    /// <summary>
    /// Saves the logged times.
    /// </summary>
    public void SaveToFile(string fileName)
    {
        if ((TurnOff))
            return;

        System.IO.StreamWriter objWriter = new System.IO.StreamWriter(fileName);
        objWriter.Write(ToString());
        objWriter.Close();
    }

    /// <summary>
    /// Creates a string of the tree of watches.
    /// </summary>
    /// <returns>A string of the tree of watches.</returns>
    public new string ToString()
    {
        if ((mRootWatch == null) || TurnOff)
            return string.Empty;

        bool first = true;
        StringBuilder sb = new StringBuilder();
        foreach (object entry in mRootWatch.Childs)
        {
            if (first)
            {
                sb.AppendLine();
                first = false;
            }
            sb.Append(entry.ToString());
        }
        return sb.ToString();
    }
    #endregion

    #region "Private"
    /// <summary>
    /// Gets the root watch.
    /// </summary>
    /// <returns>The root watch.</returns>
    private WatchInfo GetRoot()
    {
        if (((mRootWatch == null)))
        {
            mRootWatch = new WatchInfo(ROOTNAME, null);
        }

        return mRootWatch;
    }
    #endregion

    #region "WatchInfo - Class"
    /// <summary>
    /// Class that holds watch informations.
    /// </summary>
    public class WatchInfo
    {
        #region "Members"
        /// <summary>
        /// Name of the WatchInfo.
        /// </summary>
        private string mName = string.Empty;

        /// <summary>
        /// The Stopwatch of the WatchInfo.
        /// </summary>
        private Stopwatch mWatch = null;

        /// <summary>
        /// The parent WatchInfo.
        /// </summary>
        private WatchInfo mParent = null;

        /// <summary>
        /// The list of child WatchInfo's.
        /// </summary>
        private List<WatchInfo> mChilds = new List<WatchInfo>();

        #endregion

        #region "Properties"
        /// <summary>
        /// Name of the WatchInfo.
        /// </summary>
        public string Name
        {
            get { return mName; }
        }

        /// <summary>
        /// The Stopwatch of the WatchInfo.
        /// </summary>
        public Stopwatch Watch
        {
            get { return mWatch; }
        }

        /// <summary>
        /// The parent WatchInfo.
        /// </summary>
        public WatchInfo Parent
        {
            get { return mParent; }
        }

        /// <summary>
        /// Flag that indicates if the StopWatch is running or not.
        /// </summary>
        public bool IsRunning
        {
            get { return mWatch.IsRunning; }
        }

        /// <summary>
        /// The list of child WatchInfos.
        /// </summary>
        public List<WatchInfo> Childs
        {
            get { return mChilds; }
        }
        #endregion

        #region "Constructor"
        /// <summary>
        /// Default constructor.
        /// </summary>
        public WatchInfo()
        {
        }

        /// <summary>
        /// Creates a new instance of the WatchInfo class.
        /// </summary>
        public WatchInfo(string name, WatchInfo parent)
        {
            mName = name;
            mWatch = new Stopwatch();
            mParent = parent;

            if (((parent != null)))
                mParent.Childs.Add(this);
        }
        #endregion

        #region "Public"
        /// <summary>
        /// Starts the StopWatch.
        /// </summary>
        /// <param name="stopAndResetChilds">Flag for stopping and resetting all child watches of the current watch.</param>
        public void RestartWatch(bool stopAndResetChilds = false)
        {
            if ((mWatch.IsRunning))
                StopWatch(stopAndResetChilds);

            mWatch.Restart();
            if ((stopAndResetChilds))
            {
                foreach (WatchInfo child in mChilds)
                    child.ResetWatch(stopAndResetChilds);
            }
        }

        /// <summary>
        /// Stops the StopWatch.
        /// </summary>
        /// <param name="stopChilds">Flag for stopping all child watches of the current watch.</param>
        public void StopWatch(bool stopChilds = false)
        {
            if ((mWatch.IsRunning))
                mWatch.Stop();

            if ((stopChilds))
            {
                foreach (WatchInfo child in mChilds)
                    child.StopWatch(stopChilds);
            }
        }

        /// <summary>
        /// Stops and resets the StopWatch.
        /// </summary>
        /// <param name="resetChilds">Flag for resetting all child watches of the current watch.</param>
        public void ResetWatch(bool resetChilds = false)
        {
            if ((mWatch.IsRunning))
                mWatch.Stop();

            mWatch.Reset();
            if ((resetChilds))
            {
                foreach (WatchInfo child in mChilds)
                    child.ResetWatch(resetChilds);
            }
        }

        /// <summary>
        /// Creates a string of itself and its child watches.
        /// </summary>
        /// <returns>A string of itself and its child watches.</returns>
        public object ToString(bool withChilds = true, long depth = 0)
        {
            StringBuilder sb = new StringBuilder();
            for (long i = 1; i <= depth; i++)
            {
                sb.Append("  ");
            }
            sb.Append(mName);
            if ((mChilds.Count > 0 && withChilds))
            {
                sb.AppendLine();
                foreach (WatchInfo entry in mChilds)
                    sb.Append(entry.ToString(withChilds, depth + 1));

                for (long i = 1; i <= depth; i++)
                    sb.Append("  ");

                dynamic temp = string.Format("{0} (end) : {1}", mName, mWatch.Elapsed.ToString());
                sb.AppendLine(temp);
            }
            else
            {
                dynamic temp = string.Format(" : {0}", mWatch.Elapsed.ToString());
                sb.AppendLine(temp);
            }
            return sb.ToString();
        }
        #endregion
    }
    #endregion
}


/// '''''''''''''''
/// '' SAMPLE '''''
/// '''''''''''''''
////Sub Main()
////    PerformanceLogger.Start("FuncCall")
////    FuncCall()
////    PerformanceLogger.Stop()

////    PerformanceLogger.Start("FuncCall")
////    FuncCall()
////    PerformanceLogger.Stop()

////    PerformanceLogger.Start("FuncCall1")
////    FuncCall1()
////    PerformanceLogger.Stop()

////    PerformanceLogger.Start("FuncCall2")
////    FuncCall2()
////    PerformanceLogger.Stop()

////    PerformanceLogger.Start("FuncCall3")
////    FuncCall3()
////    PerformanceLogger.Stop()

////    'TimeLogger.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))

////    Console.Write(PerformanceLogger.ToString())
////    Console.WriteLine("-------")
////    Console.WriteLine("Press any key")
////    Console.ReadKey()
////End Sub

////#Region "Functions"
////Sub FuncCall()
////    PerformanceLogger.Start("For-Schleife")
////    Dim a As Integer = 0
////    For i = 0 To 10
////        a += 1
////    Next
////    PerformanceLogger.Stop()
////End Sub

////Sub FuncCall1()
////    PerformanceLogger.Start("FuncCall")
////    FuncCall()
////    PerformanceLogger.Stop()
////End Sub

////Sub FuncCall2()
////    PerformanceLogger.Start("FuncCall1")
////    FuncCall1()
////    PerformanceLogger.Stop()
////End Sub

////Sub FuncCall3()
////    PerformanceLogger.Start("FuncCall2")
////    FuncCall2()
////    PerformanceLogger.Stop()
////    PerformanceLogger.Start("FuncCall2")
////    FuncCall2()
////    PerformanceLogger.Stop()
////End Sub
////#End Region

/// ''''''''''''''''''''''''''''
/// ' OUTPUT OF SAMPLE CODE ''''
/// ''''''''''''''''''''''''''''
////FuncCall
////  For-Schleife : 00:00:00.0007416
////FuncCall (end) : 00:00:00.0009061
////FuncCall
////  For-Schleife : 00:00:00.0000014
////FuncCall (end) : 00:00:00.0000053
////FuncCall1
////  FuncCall
////    For-Schleife : 00:00:00.0000020
////  FuncCall (end) : 00:00:00.0000064
////FuncCall1 (end) : 00:00:00.0001208
////FuncCall2
////  FuncCall1
////    FuncCall
////      For-Schleife : 00:00:00.0000030
////    FuncCall (end) : 00:00:00.0000061
////  FuncCall1 (end) : 00:00:00.0000100
////FuncCall2 (end) : 00:00:00.0001690
////FuncCall3
////  FuncCall2
////    FuncCall1
////      FuncCall
////        For-Schleife : 00:00:00.0000021
////      FuncCall (end) : 00:00:00.0000067
////    FuncCall1 (end) : 00:00:00.0000109
////  FuncCall2 (end) : 00:00:00.0000159
////  FuncCall2
////    FuncCall1
////      FuncCall
////        For-Schleife : 00:00:00.0000025
////      FuncCall (end) : 00:00:00.0000071
////    FuncCall1 (end) : 00:00:00.0000118
////  FuncCall2 (end) : 00:00:00.0000166
////FuncCall3 (end) : 00:00:00.0001502
}