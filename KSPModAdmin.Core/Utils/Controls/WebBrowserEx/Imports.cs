using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace KSPModAdmin.Core.Utils.Controls
{
    #region Imports

    ///// <summary>
    ///// Provides the primary means by which an embedded object obtains information 
    ///// about the location and extent of its display site, its moniker, its user 
    ///// interface, and other resources provided by its container. An object server
    /////  calls IOleClientSite to request services from the container. A container 
    ///// must provide one instance of IOleClientSite for every compound-document object it contains.
    ///// </summary>
    //[ComImport]
    //[Guid("00000118-0000-0000-C000-000000000046")]
    //[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //[CLSCompliant(false)]
    //public interface IOleClientSite
    //{
    //    /// <summary>
    //    /// Saves the embedded object associated with the client site.
    //    /// </summary>
    //    void SaveObject();
    //    /// <summary>
    //    /// Retrieves a moniker for the object's client site.
    //    /// </summary>
    //    /// <param name="dwAssign"></param>
    //    /// <param name="dwWhichMoniker"></param>
    //    /// <param name="ppmk"></param>
    //    void GetMoniker(uint dwAssign, uint dwWhichMoniker, ref object ppmk);
    //    /// <summary>
    //    /// Retrieves a pointer to the object's container.
    //    /// </summary>
    //    /// <param name="ppContainer">the container.</param>
    //    void GetContainer(ref object ppContainer);
    //    /// <summary>
    //    ///  	Asks a container to display its object to the user.
    //    /// </summary>
    //    void ShowObject();
    //    /// <summary>
    //    /// Notifies a container when an embedded object's window
    //    /// is about to become visible or invisible.
    //    /// </summary>
    //    /// <param name="fShow"><c>true</c> if showing.</param>
    //    void OnShowWindow(bool fShow);
    //    /// <summary>
    //    /// Asks a container to resize the display site for embedded objects.
    //    /// </summary>
    //    void RequestNewObjectLayout();
    //}

    //#pragma warning disable 1591
    //#region IHttpNegotiate Interface
    ////MIDL_INTERFACE("79EAC9D2-BAF9-11CE-8C82-00AA004BA90B")
    ////IHttpNegotiate : public IUnknown
    ////{
    ////public:
    ////    virtual HRESULT STDMETHODCALLTYPE BeginningTransaction( 
    ////        /* [in] */ LPCWSTR szURL,
    ////        /* [unique][in] */ LPCWSTR szHeaders,
    ////        /* [in] */ DWORD dwReserved,
    ////        /* [out] */ LPWSTR *pszAdditionalHeaders) = 0;

    ////    virtual HRESULT STDMETHODCALLTYPE OnResponse( 
    ////        /* [in] */ DWORD dwResponseCode,
    ////        /* [unique][in] */ LPCWSTR szResponseHeaders,
    ////        /* [unique][in] */ LPCWSTR szRequestHeaders,
    ////        /* [out] */ LPWSTR *pszAdditionalRequestHeaders) = 0;
    ////}
    //[ComImport, ComVisible(true)]
    //[Guid("79EAC9D2-BAF9-11CE-8C82-00AA004BA90B")]
    //[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //[CLSCompliant(false)]
    //public interface IHttpNegotiate
    //{
    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int BeginningTransaction(
    //        [In, MarshalAs(UnmanagedType.LPWStr)] string szURL,
    //        [In, MarshalAs(UnmanagedType.LPWStr)] string szHeaders,
    //        [In, MarshalAs(UnmanagedType.U4)] UInt32 dwReserved,
    //        [Out] out IntPtr pszAdditionalHeaders);

    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int OnResponse(
    //        [In, MarshalAs(UnmanagedType.U4)] UInt32 dwResponseCode,
    //        [In, MarshalAs(UnmanagedType.LPWStr)] string szResponseHeaders,
    //        [In, MarshalAs(UnmanagedType.LPWStr)] string szRequestHeaders,
    //        [Out] out IntPtr pszAdditionalRequestHeaders);
    //}
    //#endregion

    //#region IBindStatusCallback Interface
    //[ComImport, ComVisible(true),
    //Guid("79EAC9C1-BAF9-11CE-8C82-00AA004BA90B"),
    //InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //[CLSCompliant(false)]
    //public interface IBindStatusCallback
    //{
    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int OnStartBinding(
    //        [In] uint dwReserved,
    //        [In, MarshalAs(UnmanagedType.Interface)] IBinding pib);

    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int GetPriority(out int pnPriority);

    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int OnLowResource([In] uint reserved);

    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int OnProgress(
    //        [In] uint ulProgress,
    //        [In] uint ulProgressMax,
    //        //[In] BINDSTATUS ulStatusCode,
    //        [In, MarshalAs(UnmanagedType.U4)] uint ulStatusCode,
    //        [In, MarshalAs(UnmanagedType.LPWStr)] string szStatusText);

    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int OnStopBinding(
    //        [In, MarshalAs(UnmanagedType.Error)] uint hresult,
    //        [In, MarshalAs(UnmanagedType.LPWStr)] string szError);

    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int GetBindInfo(
    //        //out BINDF grfBINDF,
    //        [In, Out, MarshalAs(UnmanagedType.U4)] ref uint grfBINDF,
    //        [In, Out, MarshalAs(UnmanagedType.Struct)] ref BINDINFO pbindinfo);

    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int OnDataAvailable(
    //        [In, MarshalAs(UnmanagedType.U4)] uint grfBSCF,
    //        [In, MarshalAs(UnmanagedType.U4)] uint dwSize,
    //        [In, MarshalAs(UnmanagedType.Struct)] ref FORMATETC pFormatetc,
    //        [In, MarshalAs(UnmanagedType.Struct)] ref STGMEDIUM pStgmed);

    //    [return: MarshalAs(UnmanagedType.I4)]
    //    [PreserveSig]
    //    int OnObjectAvailable(
    //        [In] ref Guid riid,
    //        [In, MarshalAs(UnmanagedType.IUnknown)] object punk);
    //}
    //#endregion

    //[StructLayout(LayoutKind.Sequential)]
    //[CLSCompliant(false)]
    //public struct BINDINFO
    //{
    //    [MarshalAs(UnmanagedType.U4)]
    //    public uint cbSize;
    //    [MarshalAs(UnmanagedType.LPWStr)]
    //    public string szExtraInfo;
    //    [MarshalAs(UnmanagedType.Struct)]
    //    public STGMEDIUM stgmedData;
    //    [MarshalAs(UnmanagedType.U4)]
    //    public uint grfBindInfoF;
    //    [MarshalAs(UnmanagedType.U4)]
    //    public uint dwBindVerb;
    //    [MarshalAs(UnmanagedType.LPWStr)]
    //    public string szCustomVerb;
    //    [MarshalAs(UnmanagedType.U4)]
    //    public uint cbstgmedData;
    //    [MarshalAs(UnmanagedType.U4)]
    //    public uint dwOptions;
    //    [MarshalAs(UnmanagedType.U4)]
    //    public uint dwOptionsFlags;
    //    [MarshalAs(UnmanagedType.U4)]
    //    public uint dwCodePage;
    //    [MarshalAs(UnmanagedType.Struct)]
    //    public SECURITY_ATTRIBUTES securityAttributes;
    //    public Guid iid;
    //    [MarshalAs(UnmanagedType.IUnknown)]
    //    public object punk;
    //    [MarshalAs(UnmanagedType.U4)]
    //    public uint dwReserved;
    //}

    //[StructLayout(LayoutKind.Sequential)]
    //[CLSCompliant(false)]
    //public struct SECURITY_ATTRIBUTES
    //{
    //    [MarshalAs(UnmanagedType.U4)]
    //    public uint nLength;
    //    public IntPtr lpSecurityDescriptor;
    //    [MarshalAs(UnmanagedType.Bool)]
    //    public bool bInheritHandle;
    //}


    //[ComImport(), ComVisible(true),
    //Guid("79EAC9C0-BAF9-11CE-8C82-00AA004BA90B"),
    //InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //[CLSCompliant(false)]
    //public interface IBinding
    //{
    //    void Abort();
    //    void Suspend();
    //    void Resume();
    //    void SetPriority([In] int nPriority);
    //    void GetPriority(out int pnPriority);
    //    void GetBindResult(out Guid pclsidProtocol,
    //         out uint pdwResult,
    //        [MarshalAs(UnmanagedType.LPWStr)] out string pszResult,
    //        [In, Out] ref uint dwReserved);
    //}

    /// <summary>
    /// Interface for COM service provider
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("6d5140c1-7436-11ce-8034-00aa006009fa")]
    public interface IServiceProvider
    {
        /// <summary>
        /// Queries for a service
        /// </summary>
        /// <param name="guidService">the service GUID</param>
        /// <param name="riid"></param>
        /// <param name="ppvObject"></param>
        /// <returns></returns>
        [PreserveSig]
        int QueryService(ref Guid guidService, ref Guid riid, out IntPtr ppvObject);
    }

    /// <summary>
    /// Interface to intercept the download
    /// </summary>
    [ComVisible(false), ComImport]
    [Guid("988934A4-064B-11D3-BB80-00104B35E7F9")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
   // [CLSCompliant(false)]
    public interface IDownloadManager
    {
        /// <summary>
        /// Download
        /// </summary>
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Download(
            [In, MarshalAs(UnmanagedType.Interface)] IMoniker pmk,
            [In, MarshalAs(UnmanagedType.Interface)] IBindCtx pbc,
            [In, MarshalAs(UnmanagedType.U4)] UInt32 dwBindVerb,
            [In] int grfBINDF,
            [In] IntPtr pBindInfo,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszHeaders,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszRedir,
            [In, MarshalAs(UnmanagedType.U4)] uint uiCP);
    }    
    #endregion
}
