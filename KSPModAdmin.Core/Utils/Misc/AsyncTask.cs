using System;
using System.ComponentModel;
using System.Net;


/// <summary>
/// The function of the tasks.
/// </summary>
/// <typeparam name="T_RETURN_VALUE">Type of the return value of the run function.</typeparam>
/// <returns>The result of the task function.</returns>
public delegate T_RETURN_VALUE AsyncHandler<T_RETURN_VALUE>();

/// <summary>
/// The finish callback function.
/// </summary>
/// <typeparam name="T_RETURN_VALUE">Type of the return value of the run function.</typeparam>
/// <param name="result">The return value of the task function.</param>
/// <param name="ex">A exception if execution of the task function fails or null.</param>
public delegate void AsyncResultHandler<T_RETURN_VALUE>(T_RETURN_VALUE result, Exception ex);

/// <summary>
/// The progress changed callback function.
/// </summary>
/// <param name="percentage">The percentage of the execution of the task function. (Remember you have to set the PercentFinished property within your task function to the proper value)</param>
public delegate void AsyncProgressChangedHandler(int percentage);


/// <summary>
/// The AsyncTask class wraps the System.ComponentModel.BackgroundWorker class to perform asynchrony tasks (Sample on bottom of src).
/// </summary>
/// <typeparam name="T_RETURN_VALUE">Type of the return value of the run function.</typeparam>
public class AsyncTask<T_RETURN_VALUE>
{
    #region Members

    /// <summary>
    /// The function of the tasks.
    /// </summary>
    private AsyncHandler<T_RETURN_VALUE> mRunCall;

    /// <summary>
    /// The finish callback function.
    /// </summary>
    private AsyncResultHandler<T_RETURN_VALUE> mResultCall;

    /// <summary>
    /// The progress changed callback function.
    /// </summary>
    private AsyncProgressChangedHandler mProgressChangedCall;

    /// <summary>
    ///  The Exception that occurred during execution of the task function.
    /// </summary>
    private Exception mException = null;

    /// <summary>
    /// The workhorse.
    /// </summary>
    private BackgroundWorker mWorker = null;

    /// <summary>
    /// The download url.
    /// </summary>
    private string mURL = string.Empty;

    /// <summary>
    /// The download path.
    /// </summary>
    private string mDownloadPath = string.Empty;

    /// <summary>
    /// The finish callback function.
    /// </summary>
    private AsyncResultHandler<bool> mDownloadFinished;

    /// <summary>
    /// The WebClient for downloading tasks.
    /// </summary>
    private WebClient mWebClient = null;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the underlying BackgroundWorker.
    /// </summary>
    public BackgroundWorker Worker
    {
        get
        {
            return mWorker;
        }
    }

    /// <summary>
    /// Gets the underlying WebClient.
    /// </summary>
    public WebClient WebClient
    {
        get
        {
            return mWebClient;
        }
    }

    /// <summary>
    /// Set this property within your task function it will call the progress changed callback function with the passed value.
    /// </summary>
    public int PercentFinished
    {
        set
        {
            if (mWorker != null)
                mWorker.ReportProgress(value);
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
    /// Sets the callback function of the BackgroundWorker.
    /// </summary>
    /// <param name="task">The callback function that contains the task that should be performed asynchronously.</param>
    /// <param name="finished">The callback function that should be called with the result of the task function.</param>
    /// <param name="progressChanged">The callback function for the progress changed handling.</param>
    /// <param name="supportCancellation">Flag to determine if the asynchronous task could be canceled.</param>
    public void SetCallbackFunctions(AsyncHandler<T_RETURN_VALUE> task, AsyncResultHandler<T_RETURN_VALUE> finished, AsyncProgressChangedHandler progressChanged = null, bool supportCancellation = false)
    {
        if (task == null) throw new ArgumentNullException();

        mResultCall = finished;
        mRunCall = task;
        mProgressChangedCall = progressChanged;
        mException = null;

        mWorker = new BackgroundWorker();
        mWorker.WorkerSupportsCancellation = supportCancellation;

        if (mResultCall != null)
            mWorker.DoWork += new DoWorkEventHandler(Run);
        if (mResultCall != null)
            mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Finish);

        if (progressChanged != null)
        {
            mWorker.WorkerReportsProgress = true;
            mWorker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
        }
    }

    /// <summary>
    /// Sets the download callback function of the BackgroundWorker.
    /// </summary>
    /// <param name="url">Download link</param>
    /// <param name="downloadPath">Path and filename to download to.</param>
    /// <param name="finished">The finished callback function. (A function with the signature "void FunctionName&lt;bool&gt;(bool result, Exception ex)")</param>
    /// <param name="progressChanged">The progress change callback function. (A function with the signature "void FunctionName(int percentage)")</param>
    public void SetDownloadCallbackFunctions(string url, string downloadPath, AsyncResultHandler<bool> finished, AsyncProgressChangedHandler progressChanged = null)
    {
        mURL = url;
        mDownloadPath = downloadPath;
        mDownloadFinished = finished;
        mProgressChangedCall = progressChanged;
        mException = null;

        mWebClient = new WebClient();
        mWebClient.Credentials = CredentialCache.DefaultCredentials;

        if (finished != null)
            mWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFinished);

        if (progressChanged != null)
            mWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
    }

    /// <summary>
    /// Runs the task function asynchrony (with a BackgroundWorker).
    /// </summary>
    public void Run()
    {
        if (mWorker != null)
            mWorker.RunWorkerAsync();
    }

    /// <summary>
    /// Runs the download asynchrony (with a WebClient).
    /// </summary>
    public void RunDownload()
    {
        if (mWebClient != null)
            mWebClient.DownloadFileAsync(new Uri(mURL), mDownloadPath);
    }

    /// <summary>
    /// Cancels the execution of the task function if possible.
    /// </summary>
    /// <returns>True if cancellation was successful otherwise false.</returns>
    public bool Cancel()
    {
        bool result = false;
        if (mWorker != null && mWorker.WorkerSupportsCancellation)
        {
            mWorker.CancelAsync();
            result = true;
        }

        if (mWebClient != null)
        {
            mWebClient.CancelAsync();
            result = true;
        }

        return result;
    }

    #endregion

    #region Private

    /// <summary>
    /// Run callback for the BackgroundWorker.
    /// </summary>
    private void Run(object sender,  DoWorkEventArgs e)
    {
        try
        {
            e.Result = mRunCall();
        }
        catch (Exception ex)
        {
            mException = ex;
        }
    }

    /// <summary>
    /// Finish callback for the BackgroundWorker.
    /// </summary>
    private void Finish(object sender,  RunWorkerCompletedEventArgs e)
    {
        if (mResultCall != null)
        {
            if (e.Result != null)
                mResultCall((T_RETURN_VALUE)e.Result, mException);
            else
                mResultCall(default(T_RETURN_VALUE), mException);
        }
    }

    /// <summary>
    /// Progress changed callback for the BackgroundWorker.
    /// </summary>
    public void ProgressChanged(object sender,  ProgressChangedEventArgs e)
    {
        if (mProgressChangedCall != null)
            mProgressChangedCall(e.ProgressPercentage);
    }

    /// <summary>
    /// Download finished callback for the WebClient.
    /// </summary>
    private void DownloadFinished(object sender, AsyncCompletedEventArgs e)
    {
        if (e.Error == null)
        {
            if (e.Cancelled)
                mDownloadFinished(false, new Exception("Download canceled."));
            else
                mDownloadFinished(true, null);
        }
        else
        {
            mDownloadFinished(false, e.Error);
        }

        if (mWebClient != null)
        {
            if (mDownloadFinished != null)
                mWebClient.DownloadFileCompleted -= new AsyncCompletedEventHandler(DownloadFinished);
            if (mProgressChangedCall != null)
                mWebClient.DownloadProgressChanged -= new DownloadProgressChangedEventHandler(DownloadProgressChanged);

            mWebClient.Dispose();
            mWebClient = null;
        }
    }

    /// <summary>
    /// Download progress changed callback for the WebClient.
    /// </summary>
    public void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        if (mProgressChangedCall != null)
            mProgressChangedCall(e.ProgressPercentage);
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