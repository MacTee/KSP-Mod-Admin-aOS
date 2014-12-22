namespace KSPModAdmin.Core
{
    /// <summary>
    /// Enum of possible destination detection types.
    /// </summary>
    public enum DestinationDetectionType
    {
        /// <summary>
        /// Tries to find KSP similar folder structures in the mod archive and sets the destination accordingly.
        /// </summary>
        SmartDetection,

        /// <summary>
        /// Just creates a folder in GameData (with the mod archive name) and copies the whole archive into it.
        /// </summary>
        SimpleDump
    }

    /// <summary>
    /// Enum of possible ksp paths.
    /// </summary>
    public enum KSPPaths
    {
        KSPRoot,
        KSPExe,
        KSPX64Exe,
        AppConfig,
        KSPConfig,
        Saves,
        Parts,
        Plugins,
        PluginData,
        Resources,
        Ships,
        VAB,
        SPH,
        Internals,
        KSPData,
        GameData,
        LanguageFolder,
        KSPMA_Plugins
    }

    /// <summary>
    /// Possible actions after a download of KSPModAdmin.
    /// </summary>
    public enum PostDownloadAction
    {
        Ignore,
        Ask,
        AutoUpdate
    }

    /// <summary>
    /// The possible intervals of mod updating.
    /// </summary>
    public enum ModUpdateInterval
    {
        Manualy = 0,
        OnStartup = 1,
        OnceADay = 2,
        EveryTwoDays = 3,
        OnceAWeek = 4
    }

    /// <summary>
    /// Enum of possible behaviors that will occur when a mod update is detected.
    /// </summary>
    public enum ModUpdateBehavior
    {
        RemoveAndAdd = 0,
        CopyDestination = 1,
        CopyCheckedState = 2,
        Manualy = 3
    }

    /// <summary>
    /// Enum of possible ModNode types.
    /// </summary>
    public enum NodeType
    {
        ZipRoot,
        KSPFolder,
        KSPFolderInstalled,
        UnknownFolder,
        UnknownFolderInstalled,
        UnknownFile,
        UnknownFileInstalled
    }

    /// <summary>
    /// Enum of possible sort types.
    /// </summary>
    public enum SortType
    {
        // Sort by name
        ByName = 0,

        // Sort by added date
        ByAddDate,

        // Sort by state
        ByState,

        // Sort by Version
        ByVersion
    }
}
