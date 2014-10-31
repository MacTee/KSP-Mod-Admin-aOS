using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Utils.Logging
{
/// <summary>
/// This is a simple performance logger.
/// The class is designed to masure the execution time of functions and its sub functions.
/// NOTE: The PerformanceLogger can't be used with threaded (sub) functions. 
///       To analyse functions running in another thread, create a new instance of the PerformanceLogger (for each tread).
///       A "How to use" sample is at the bottom of this code.
/// </summary>
/// <remarks></remarks>
public class PerformanceLogger
{
    #region "Members"
    /// <summary>
    /// Name for the root watch.
    /// </summary>
    /// <remarks></remarks>

    private const string ROOTNAME = "TimeLogger_RootWatch";
    /// <summary>
    /// Global instance of the PerformanceLogger.
    /// </summary>
    /// <remarks></remarks>

    private static PerformanceLogger m_Instance = null;
    /// <summary>
    /// The root watch.
    /// </summary>
    /// <remarks></remarks>

    private WatchInfo m_RootWatch = null;
    /// <summary>
    /// Stack of the last used (created) watches.
    /// </summary>
    /// <remarks></remarks>
    #endregion
    private Stack m_StackTable = new Stack();

    #region "Properties"
    /// <summary>
    /// Global instance of the PerformanceLogger.
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    public static PerformanceLogger Instance
    {
        get
        {
            if (((m_Instance == null)))
            {
                m_Instance = new PerformanceLogger();
            }
            return m_Instance;
        }
    }

    /// <summary>
    /// Turns off the timekeeping of the PerformanceLogger.
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    private bool TurnOff { get; set; }
    #endregion

    #region "Shared"
    /// <summary>
    /// Turns off the timekeeping of the global instance of the PerformanceLogger.
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
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
    /// <param name="stopAndResetChilds">Flag for stopping and reseting all child watches of the current watch.</param>
    /// <remarks></remarks>
    public static void Start(string name, bool stopAndResetChilds = false)
    {
        Instance.StartWatch(name, stopAndResetChilds);
    }

    /// <summary>
    /// Stops the current watch and pops it from the stack.
    /// </summary>
    /// <param name="stopChilds">Flag for stopping all child watches of the current watch.</param>
    /// <remarks></remarks>
    public static void Stop(bool stopChilds = false)
    {
        Instance.StopWatch(stopChilds);
    }

    /// <summary>
    /// Stops and resets the current watch and pops it from the stack.
    /// </summary>
    /// <param name="resetChilds">Flag for resetting all child watches of the current watch.</param>
    /// <remarks></remarks>
    public static void Reset(bool resetChilds = false)
    {
        Instance.ResetWatch(resetChilds);
    }

    /// <summary>
    /// Clears all watches.
    /// </summary>
    /// <remarks></remarks>
    public static void ClearAll()
    {
        Instance.ClearAllWatches();
    }

    /// <summary>
    /// Saves the logged times.
    /// </summary>
    /// <param name="fileName"></param>
    /// <remarks></remarks>
    public static void Save(string fileName)
    {
        Instance.SaveToFile(fileName);
    }

    /// <summary>
    /// Creates a string of the tree of watches.
    /// </summary>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string ToString_Shared()
    {
        return Instance.ToString();
    }

    /// <summary>
    /// Shows the logged times in a message box.
    /// </summary>
    /// <remarks></remarks>
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
    /// <param name="stopAndResetChilds">Flag for stopping and reseting all child watches of the current timer.</param>
    /// <remarks></remarks>
    public void StartWatch(string name, bool stopAndResetChilds = false)
    {
        if ((TurnOff))
            return;

        if ((m_StackTable.Count == 0))
        {
            m_StackTable.Push(new WatchInfo(name, GetRoot()));
        }

        dynamic wInfo = (WatchInfo)m_StackTable.Peek();
        if ((wInfo.IsRunning))
        {
            dynamic newWInfo = new WatchInfo(name, wInfo);
            m_StackTable.Push(newWInfo);
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
    /// <remarks></remarks>
    public void StopWatch(bool stopChilds = false)
    {
        if ((TurnOff))
            return;

        if ((m_StackTable.Count > 0))
        {
            dynamic wInfo = (WatchInfo)m_StackTable.Peek();
            wInfo.StopWatch(stopChilds);
            m_StackTable.Pop();
        }
    }

    /// <summary>
    /// Stops and resets the current watch and pops it from the stack.
    /// </summary>
    /// <param name="resetChilds">Flag for resetting all child watches of the current watch.</param>
    /// <remarks></remarks>
    public void ResetWatch(bool resetChilds = false)
    {
        if ((TurnOff))
            return;

        if ((m_StackTable.Count > 0))
        {
            dynamic wInfo = (WatchInfo)m_StackTable.Peek();
            wInfo.ResetWatch(resetChilds);
            m_StackTable.Pop();
        }
    }

    /// <summary>
    /// Clears all watches.
    /// </summary>
    /// <remarks></remarks>
    public void ClearAllWatches()
    {
        if ((TurnOff))
            return;

        m_RootWatch = null;
        m_StackTable.Clear();
    }

    /// <summary>
    /// Saves the logged times.
    /// </summary>
    /// <param name="fileName"></param>
    /// <remarks></remarks>
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
    /// <returns></returns>
    /// <remarks></remarks>
    public new string ToString()
    {
        if ((m_RootWatch == null) || TurnOff)
            return string.Empty;

        bool first = true;
        StringBuilder sb = new StringBuilder();
        foreach (object entry in m_RootWatch.Childs)
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
    /// <returns></returns>
    /// <remarks></remarks>
    private WatchInfo GetRoot()
    {
        if (((m_RootWatch == null)))
        {
            m_RootWatch = new WatchInfo(ROOTNAME, null);
        }

        return m_RootWatch;
    }
    #endregion

    #region "WatchInfo - Class"
    public class WatchInfo
    {
        #region "Members"
        /// <summary>
        /// Name of the WatchInfo.
        /// </summary>
        /// <remarks></remarks>

        private string m_Name = string.Empty;
        /// <summary>
        /// The Stopwatch of the WatchInfo.
        /// </summary>
        /// <remarks></remarks>

        private Stopwatch m_Watch = null;
        /// <summary>
        /// The parent WatchInfo.
        /// </summary>
        /// <remarks></remarks>

        private WatchInfo m_Parent = null;
        /// <summary>
        /// The list of child WatchInfos.
        /// </summary>
        /// <remarks></remarks>
        #endregion
        private List<WatchInfo> m_Childs = new List<WatchInfo>();

        #region "Properties"
        /// <summary>
        /// Name of the WatchInfo.
        /// </summary>
        /// <remarks></remarks>
        public string Name
        {
            get { return m_Name; }
        }

        /// <summary>
        /// The Stopwatch of the WatchInfo.
        /// </summary>
        /// <remarks></remarks>
        public Stopwatch Watch
        {
            get { return m_Watch; }
        }

        /// <summary>
        /// The parent WatchInfo.
        /// </summary>
        /// <remarks></remarks>
        public WatchInfo Parent
        {
            get { return m_Parent; }
        }

        /// <summary>
        /// Flag that indicates if the StopWatch is running or not.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsRunning
        {
            get { return m_Watch.IsRunning; }
        }

        /// <summary>
        /// The list of child WatchInfos.
        /// </summary>
        /// <remarks></remarks>
        public List<WatchInfo> Childs
        {
            get { return m_Childs; }
        }
        #endregion

        #region "Constructor"
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        public WatchInfo()
        {
        }

        /// <summary>
        /// Creates a new instance of the WatchInfo class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <remarks></remarks>
        public WatchInfo(string name, WatchInfo parent)
        {
            m_Name = name;
            m_Watch = new Stopwatch();
            m_Parent = parent;

            if (((parent != null)))
                m_Parent.Childs.Add(this);
        }
        #endregion

        #region "Public"
        /// <summary>
        /// Starts the StopWatch.
        /// </summary>
        /// <param name="stopAndResetChilds">Flag for stopping and reseting all child watches of the current watch.</param>
        /// <remarks></remarks>
        public void RestartWatch(bool stopAndResetChilds = false)
        {
            if ((m_Watch.IsRunning))
                StopWatch(stopAndResetChilds);

            m_Watch.Restart();
            if ((stopAndResetChilds))
            {
                foreach (WatchInfo child in m_Childs)
                    child.ResetWatch(stopAndResetChilds);
            }
        }

        /// <summary>
        /// Stops the StopWatch.
        /// </summary>
        /// <param name="stopChilds">Flag for stopping all child watches of the current watch.</param>
        /// <remarks></remarks>
        public void StopWatch(bool stopChilds = false)
        {
            if ((m_Watch.IsRunning))
                m_Watch.Stop();

            if ((stopChilds))
            {
                foreach (WatchInfo child in m_Childs)
                    child.StopWatch(stopChilds);
            }
        }

        /// <summary>
        /// Stops and resets the StopWatch.
        /// </summary>
        /// <param name="resetChilds">Flag for resetting all child watches of the current watch.</param>
        /// <remarks></remarks>
        public void ResetWatch(bool resetChilds = false)
        {
            if ((m_Watch.IsRunning))
                m_Watch.Stop();

            m_Watch.Reset();
            if ((resetChilds))
            {
                foreach (WatchInfo child in m_Childs)
                    child.ResetWatch(resetChilds);
            }
        }

        /// <summary>
        /// Creates a string of itself and its child watches.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public object ToString(bool withChilds = true, long depth = 0)
        {
            StringBuilder sb = new StringBuilder();
            for (long i = 1; i <= depth; i++)
            {
                sb.Append("  ");
            }
            sb.Append(m_Name);
            if ((m_Childs.Count > 0 && withChilds))
            {
                sb.AppendLine();
                foreach (WatchInfo entry in m_Childs)
                    sb.Append(entry.ToString(withChilds, depth + 1));

                for (long i = 1; i <= depth; i++)
                    sb.Append("  ");

                dynamic temp = string.Format("{0} (end) : {1}", m_Name, m_Watch.Elapsed.ToString());
                sb.AppendLine(temp);
            }
            else
            {
                dynamic temp = string.Format(" : {0}", m_Watch.Elapsed.ToString());
                sb.AppendLine(temp);
            }
            return sb.ToString();
        }
        #endregion
    }
    #endregion
}


///'''''''''''''''
///'' SAMPLE '''''
///'''''''''''''''
//Sub Main()
//    PerformanceLogger.Start("FuncCall")
//    FuncCall()
//    PerformanceLogger.Stop()

//    PerformanceLogger.Start("FuncCall")
//    FuncCall()
//    PerformanceLogger.Stop()

//    PerformanceLogger.Start("FuncCall1")
//    FuncCall1()
//    PerformanceLogger.Stop()

//    PerformanceLogger.Start("FuncCall2")
//    FuncCall2()
//    PerformanceLogger.Stop()

//    PerformanceLogger.Start("FuncCall3")
//    FuncCall3()
//    PerformanceLogger.Stop()

//    'TimeLogger.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))

//    Console.Write(PerformanceLogger.ToString())
//    Console.WriteLine("-------")
//    Console.WriteLine("Press any key")
//    Console.ReadKey()
//End Sub

//#Region "Functions"
//Sub FuncCall()
//    PerformanceLogger.Start("For-Schleife")
//    Dim a As Integer = 0
//    For i = 0 To 10
//        a += 1
//    Next
//    PerformanceLogger.Stop()
//End Sub

//Sub FuncCall1()
//    PerformanceLogger.Start("FuncCall")
//    FuncCall()
//    PerformanceLogger.Stop()
//End Sub

//Sub FuncCall2()
//    PerformanceLogger.Start("FuncCall1")
//    FuncCall1()
//    PerformanceLogger.Stop()
//End Sub

//Sub FuncCall3()
//    PerformanceLogger.Start("FuncCall2")
//    FuncCall2()
//    PerformanceLogger.Stop()
//    PerformanceLogger.Start("FuncCall2")
//    FuncCall2()
//    PerformanceLogger.Stop()
//End Sub
//#End Region

///''''''''''''''''''''''''''''
///' OUTPUT OF SAMPLE CODE ''''
///''''''''''''''''''''''''''''
//FuncCall
//  For-Schleife : 00:00:00.0007416
//FuncCall (end) : 00:00:00.0009061
//FuncCall
//  For-Schleife : 00:00:00.0000014
//FuncCall (end) : 00:00:00.0000053
//FuncCall1
//  FuncCall
//    For-Schleife : 00:00:00.0000020
//  FuncCall (end) : 00:00:00.0000064
//FuncCall1 (end) : 00:00:00.0001208
//FuncCall2
//  FuncCall1
//    FuncCall
//      For-Schleife : 00:00:00.0000030
//    FuncCall (end) : 00:00:00.0000061
//  FuncCall1 (end) : 00:00:00.0000100
//FuncCall2 (end) : 00:00:00.0001690
//FuncCall3
//  FuncCall2
//    FuncCall1
//      FuncCall
//        For-Schleife : 00:00:00.0000021
//      FuncCall (end) : 00:00:00.0000067
//    FuncCall1 (end) : 00:00:00.0000109
//  FuncCall2 (end) : 00:00:00.0000159
//  FuncCall2
//    FuncCall1
//      FuncCall
//        For-Schleife : 00:00:00.0000025
//      FuncCall (end) : 00:00:00.0000071
//    FuncCall1 (end) : 00:00:00.0000118
//  FuncCall2 (end) : 00:00:00.0000166
//FuncCall3 (end) : 00:00:00.0001502
}