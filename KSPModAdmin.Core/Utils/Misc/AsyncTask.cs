using System;
using System.ComponentModel;
using System.Net;


/// <summary>
/// The function of the tasks.
/// </summary>
/// <typeparam name="T_RETURN_VALUE">Type of the return value of the run function.</typeparam>
/// <returns></returns>
public delegate T_RETURN_VALUE AsyncHandler<T_RETURN_VALUE>();

/// <summary>
/// The finish callback function.
/// </summary>
/// <typeparam name="T_RETURN_VALUE">Type of the return value of the run function.</typeparam>
/// <param name="exception">A exception if execution of the task function fails or null.</param>
/// <param name="result">The return value of the task function.</param>
/// <remarks></remarks>
public delegate void AsyncResultHandler<T_RETURN_VALUE>(T_RETURN_VALUE result, Exception ex);

/// <summary>
/// The progress changed callback function.
/// </summary>
/// <param name="percentage">The percentage of the execution of the task function. (Remember you have to set the PercentFinished property within your task function to the propper value)</param>
public delegate void AsyncProgressChangedHandler(int percentage);


/// <summary>
/// The AsyncTask class wraps the System.ComponentModel.BackgroundWorker class to perform asynchronye tasks (Sample on bottom of src).
/// </summary>
/// <typeparam name="T_RETURN_VALUE">Type of the return value of the run function.</typeparam>
/// <remarks></remarks>
public class AsyncTask<T_RETURN_VALUE>
{
    #region Members

    /// <summary>
    /// The function of the tasks.
    /// </summary>
    private AsyncHandler<T_RETURN_VALUE> m_RunCall;

    /// <summary>
    /// The finish callback function.
    /// </summary>
    private AsyncResultHandler<T_RETURN_VALUE> m_ResultCall;

    /// <summary>
    /// The progress changed callback function.
    /// </summary>
    private AsyncProgressChangedHandler m_ProgressChangedCall;

    /// <summary>
    ///  The Exception that occurred during execution of the task function.
    /// </summary>
    private Exception m_Exception = null;

    /// <summary>
    /// The workhorse.
    /// </summary>
    private BackgroundWorker m_Worker = null;

    /// <summary>
    /// The download url.
    /// </summary>
    private string m_URL = string.Empty;

    /// <summary>
    /// The download path.
    /// </summary>
    private string m_DownloadPath = string.Empty;

    /// <summary>
    /// The finish callback function.
    /// </summary>
    private AsyncResultHandler<bool> m_DownloadFinished;

    /// <summary>
    /// The WebClient for downloading tasks.
    /// </summary>
    private WebClient m_WebClient = null;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the underlying BackgroundWorker.
    /// </summary>
    public BackgroundWorker Worker
    {
        get
        {
            return m_Worker;
        }
    }

    /// <summary>
    /// Gets the underlying WebClient.
    /// </summary>
    public WebClient WebClient
    {
        get
        {
            return m_WebClient;
        }
    }

    /// <summary>
    /// Set this property within your task function it will call the progress changed callback function with the passed value.
    /// </summary>
    public int PercentFinished
    {
        set
        {
            if (m_Worker != null)
                m_Worker.ReportProgress(value);
        }
    }

    #endregion

    #region Static

    /// <summary>
    /// This function calls the passed task function asynchronously.
    /// </summary>
    /// <param name="task">The task function to execute asynchrony. (A function with the signature "T_RETURN_VALUE FunctionName&lt;T_RETURN_VALUE&gt;()")</param>
    /// <param name="finished">The finished callback function. (A function with the signature "void FunctionName&lt;T_RETURN_VALUE&gt;(T_RETURN_VALUE result, Exception ex)")</param>
    /// <param name="progressChanged">The progress change callback function. (A function with the signature "void FunctionName(int percentage)")</param>
    public static void DoWork(AsyncHandler<T_RETURN_VALUE> task, AsyncResultHandler<T_RETURN_VALUE> finished = null, AsyncProgressChangedHandler progressChanged = null)
    {
        AsyncTask<T_RETURN_VALUE> asyncTask = new AsyncTask<T_RETURN_VALUE>(task, finished, progressChanged);
        asyncTask.Run();
    }

    /// <summary>
    /// Starts a asynchrony download.
    /// </summary>
    /// <param name="url">Download link</param>
    /// <param name="downloadPath">Path and filename to download to.</param>
    /// <param name="finished">The finished callback function. (A function with the signature "void FunctionName&lt;bool&gt;(bool result, Exception ex)")</param>
    /// <param name="progressChanged">The progress change callback function. (A function with the signature "void FunctionName(int percentage)")</param>
    public static void RunDownload(string url, string downloadPath, AsyncResultHandler<bool> finished = null, AsyncProgressChangedHandler progressChanged = null)
    {
        AsyncTask<bool> asyncTask = new AsyncTask<bool>(url, downloadPath, finished, progressChanged);
        asyncTask.RunDownload();
    }

    #endregion

    #region Public

    /// <summary>
    /// Instantiates the AsyncTask class
    /// </summary>
    public AsyncTask()
    {
    }
    
    /// <summary>
    /// Instantiates the AsyncTask class
    /// </summary>
    /// <param name="task">The task function to execute asynchrony. (A function with the signature "T_RETURN_VALUE FunctionName&lt;T_RETURN_VALUE&gt;()")</param>
    /// <param name="finished">The finished callback function. (A function with the signature "void FunctionName&lt;T_RETURN_VALUE&gt;(T_RETURN_VALUE result, Exception ex)")</param>
    /// <param name="progressChanged">The progress change callback function. (A function with the signature "void FunctionName(int percentage)")</param>
    /// <param name="supportCancellation">Flag to determine if cancellation is needed.</param>
    public AsyncTask(AsyncHandler<T_RETURN_VALUE> task, AsyncResultHandler<T_RETURN_VALUE> finished, AsyncProgressChangedHandler progressChanged = null, bool supportCancellation = false)
    {
        SetCallbackFunctions(task, finished, progressChanged, supportCancellation);
    }

    /// <summary>
    /// Instantiates the AsyncTask class for a async download.
    /// </summary>
    /// <param name="url">Download link</param>
    /// <param name="downloadPath">Path and filename to download to.</param>
    /// <param name="finished">The finished callback function. (A function with the signature "void FunctionName&lt;bool&gt;(bool result, Exception ex)")</param>
    /// <param name="progressChanged">The progress change callback function. (A function with the signature "void FunctionName(int percentage)")</param>
    public AsyncTask(string url, string downloadPath, AsyncResultHandler<bool> finished, AsyncProgressChangedHandler progressChanged)
    {
        SetDownloadCallbackFunctions(url, downloadPath, finished, progressChanged);
    }

    /// <summary>
    /// Sets the callback function of the BackgroudWorker.
    /// </summary>
    /// <param name="task">The callback function that contains the task that should be performed asynchronously.</param>
    /// <param name="finished">The callback function that should be called with the result of the task function.</param>
    /// <param name="progressChanged">The callback function for the progress changed handling.</param>
    /// <param name="supportCancellation">Flag to determine if the asynchronous task could be canceled.</param>
    public void SetCallbackFunctions(AsyncHandler<T_RETURN_VALUE> task, AsyncResultHandler<T_RETURN_VALUE> finished, AsyncProgressChangedHandler progressChanged = null, bool supportCancellation = false)
    {
        if (task == null) throw new ArgumentNullException();

        m_ResultCall = finished;
        m_RunCall = task;
        m_ProgressChangedCall = progressChanged;
        m_Exception = null;

        m_Worker = new BackgroundWorker();
        m_Worker.WorkerSupportsCancellation = supportCancellation;

        if (m_ResultCall != null)
            m_Worker.DoWork += new DoWorkEventHandler(Run);
        if (m_ResultCall != null)
            m_Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Finish);

        if (progressChanged != null)
        {
            m_Worker.WorkerReportsProgress = true;
            m_Worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
        }
    }

    /// <summary>
    /// Sets the download callback function of the BackgroudWorker.
    /// </summary>
    /// <param name="url">Download link</param>
    /// <param name="downloadPath">Path and filename to download to.</param>
    /// <param name="finished">The finished callback function. (A function with the signature "void FunctionName&lt;bool&gt;(bool result, Exception ex)")</param>
    /// <param name="progressChanged">The progress change callback function. (A function with the signature "void FunctionName(int percentage)")</param>
    public void SetDownloadCallbackFunctions(string url, string downloadPath, AsyncResultHandler<bool> finished, AsyncProgressChangedHandler progressChanged = null)
    {
        m_URL = url;
        m_DownloadPath = downloadPath;
        m_DownloadFinished = finished;
        m_ProgressChangedCall = progressChanged;
        m_Exception = null;

        m_WebClient = new WebClient();
        m_WebClient.Credentials = CredentialCache.DefaultCredentials;

        if (finished != null)
            m_WebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFinished);

        if (progressChanged != null)
            m_WebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
    }

    /// <summary>
    /// Runs the task function asynchrony (with a BackgroundWorker).
    /// </summary>
    public void Run()
    {
        if (m_Worker != null)
            m_Worker.RunWorkerAsync();
    }

    /// <summary>
    /// Runs the download asynchrony (with a WebClient).
    /// </summary>
    public void RunDownload()
    {
        if (m_WebClient != null)
            m_WebClient.DownloadFileAsync(new Uri(m_URL), m_DownloadPath);
    }

    /// <summary>
    /// Cancels the execution of the task function if possible.
    /// </summary>
    /// <returns>True if cancellation was successful otherwise false.</returns>
    public bool Cancel()
    {
        bool result = false;
        if (m_Worker != null && m_Worker.WorkerSupportsCancellation)
        {
            m_Worker.CancelAsync();
            result = true;
        }

        if (m_WebClient != null)
        {
            m_WebClient.CancelAsync();
            result = true;
        }

        return result;
    }

    #endregion

    #region Private

    /// <summary>
    /// Run callback for the BackgroundWorker.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    private void Run(object sender,  DoWorkEventArgs e)
    {
        try
        {
            e.Result = m_RunCall();
        }
        catch (Exception ex)
        {
            m_Exception = ex;
        }
    }

    /// <summary>
    /// Finish callback for the BackgroundWorker.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    private void Finish(object sender,  RunWorkerCompletedEventArgs e)
    {
        if (m_ResultCall != null)
        {
            if (e.Result != null)
                m_ResultCall((T_RETURN_VALUE)e.Result, m_Exception);
            else
                m_ResultCall(default(T_RETURN_VALUE), m_Exception);
        }
    }

    /// <summary>
    /// Progress changed callback for the BackgroundWorker.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ProgressChanged(object sender,  ProgressChangedEventArgs e)
    {
        if (m_ProgressChangedCall != null)
            m_ProgressChangedCall(e.ProgressPercentage);
    }

    /// <summary>
    /// Download finished callback for the WebClient.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
    private void DownloadFinished(object sender, AsyncCompletedEventArgs e)
    {
        if (e.Error == null)
        {
            if (e.Cancelled)
                m_DownloadFinished(false, new Exception("Download canceled."));
            else
                m_DownloadFinished(true, null);
        }
        else
        {
            m_DownloadFinished(false, e.Error);
        }

        if (m_WebClient != null)
        {
            if (m_DownloadFinished != null)
                m_WebClient.DownloadFileCompleted -= new AsyncCompletedEventHandler(DownloadFinished);
            if (m_ProgressChangedCall != null)
                m_WebClient.DownloadProgressChanged -= new DownloadProgressChangedEventHandler(DownloadProgressChanged);

            m_WebClient.Dispose();
            m_WebClient = null;
        }
    }

    /// <summary>
    /// Download progress changed callback for the WebClient.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        if (m_ProgressChangedCall != null)
            m_ProgressChangedCall(e.ProgressPercentage);
    }

    #endregion
}


#region Samples

/*
 * Usage of the static Run function:
 * ---------------------------------
 * 
 * AsyncTask<bool>.Run(delegate()
 *                     {
 *                          // do your work here
 *                          // i will compare apples with pears =)
 *                          int apple = 0;
 *                          int pear = 1;
 *                          return (apple == pear);
 *                     },
 *                     delegate(bool result, Exception ex)
 *                     {
 *                          // check the exception to be sure you can use your result!
 *                          if (ex == null) // success
 *                          {
 *                              MessageBox.Show("Success! -> " + result.ToString());
 *                          }
 *                          else // failed
 *                          {
 *                              MessageBox.Show("Failed! -> " + ex.Message);
 *                          }
 *                     });
 *                     
 * NOTE: You can exchange the template type (here bool) by any type you want 
 *       but be sure to adjust the return type of the task function and the type of the first parameter of the finished callback function!
 *       
 * 
 * 
 * Normal instancing:
 * ------------------
 * 
 * AsyncTask<bool> task = new AsyncTask<bool>();
 * task.SetCallbackFunctions(delegate()
 *                           {
 *                               // do your work here ...
 *                               
 *                               // i just run through a for loop
 *                               int max = 100;
 *                               for (int i = 1; i <= max; ++i)
 *                               {
 *                                   // set percentage
 *                                   task.PercentFinished = (int)(((decimal)i / (decimal)max) * (decimal)100);
 *                               }
 *                               return true;
 *                           },
 *                           delegate(bool result, Exception ex)
 *                           {
 *                               // check the exception to be sure you can use your result!
 *                               if (ex == null) // success
 *                               {
 *                                   MessageBox.Show("Success! -> " + result.ToString());
 *                               }
 *                               else // failed
 *                               {
 *                                   MessageBox.Show("Failed! -> " + ex.Message);
 *                               }
 *                           },
 *                           delegate(int percentFinished)
 *                           {
 *                               // display the percentage of completed work
 *                               MessageBox.Show(this, string.Format("Completed work at {0}%", percentFinished));
 *                           }, true);
 * task.Run();
 *                     
 * NOTE: You can exchange the template type (here bool) by any type you want 
 *       but be sure to adjust the return type of the task function and the type of the first parameter of the finished callback function!
 *       
 */

#endregion