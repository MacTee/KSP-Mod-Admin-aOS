using System;
using System.IO;

namespace KSPModAdmin.Core.Model
{
    /// <summary>
    /// Class that holds mod related infos.
    /// </summary>
    public class ModInfo
    {
        /// <summary>
        /// Name of the mod.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// LocalPath of the mod.
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// The name of the SiteHandler.
        /// </summary>
        public string SiteHandlerName { get; set; }

        /// <summary>
        /// Flag that indicates if the mod has a SiteHandler
        /// </summary>
        public bool HasSiteHandler
        {
            get { return (SiteHandlerName != Messages.NONE && !string.IsNullOrEmpty(SiteHandlerName)); }
        }

        /// <summary>
        /// The URL to the mod.
        /// </summary>
        public string ModURL { get; set; }

        /// <summary>
        /// The URL to the AVC file.
        /// </summary>
        public string AvcURL { get; set; }

        /// <summary>
        /// A additional (user edited) URL.
        /// </summary>
        public string AdditionalURL { get; set; }

        /// <summary>
        /// Date of the download as string.
        /// </summary>
        public string DownloadDate { get; set; }

        /// <summary>
        /// Date of the download as DateTime object.
        /// </summary>
        public DateTime DownloadDateAsDateTime
        {
            get
            {
                if (string.IsNullOrEmpty(DownloadDate))
                    return DateTime.MinValue;
                else
                {
                    DateTime value = DateTime.MinValue;
                    if (DateTime.TryParse(DownloadDate, out value))
                        return value;
                    else
                        return DateTime.MinValue;
                }
            }
            set
            {
                if (value == DateTime.MinValue)
                    DownloadDate = string.Empty;
                else
                    DownloadDate = value.ToString();
            }
        }

        /// <summary>
        /// Version of the mod.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Minimum KSP Version.
        /// </summary>
        public string KSPVersion { get; set; }

        /// <summary>
        /// Date of creation of the mod as string.
        /// </summary>
        public string CreationDate { get; set; }

        /// <summary>
        /// Date of creation of the mod as DateTime object.
        /// </summary>
        public DateTime CreationDateAsDateTime
        {
            get
            {
                if (string.IsNullOrEmpty(CreationDate))
                    return DateTime.MinValue;
                else
                {
                    DateTime value = DateTime.MinValue;
                    if (DateTime.TryParse(CreationDate, out value))
                        return value;
                    else
                        return DateTime.MinValue;
                }
            }
            set
            {
                if (value == DateTime.MinValue)
                    CreationDate = string.Empty;
                else
                    CreationDate = value.ToString();
            }
        }

        /// <summary>
        /// Date of change of the mod as string.
        /// </summary>
        public string ChangeDate { get; set; }

        /// <summary>
        /// Date of change of the mod as DateTime object.
        /// </summary>
        public DateTime ChangeDateAsDateTime
        {
            get
            {
                if (string.IsNullOrEmpty(ChangeDate))
                    return DateTime.MinValue;
                else
                {
                    DateTime value = DateTime.MinValue;
                    if (DateTime.TryParse(ChangeDate, out value))
                        return value;
                    else
                        return DateTime.MinValue;
                }
            }
            set
            {
                if (value == DateTime.MinValue)
                    ChangeDate = string.Empty;
                else
                    ChangeDate = value.ToString();
            }
        }

        /// <summary>
        /// The rating for the mod.
        /// </summary>
        public string Rating { get; set; }

        /// <summary>
        /// The download count of the mod.
        /// </summary>
        public string Downloads { get; set; }

        /// <summary>
        /// The author of the mod.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The product ID of the mod (WebSite related)
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// The notes of the mod
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Flag to determine if mod is a archive.
        /// </summary>
        public bool IsArchive
        {
            get
            {
                if (LocalPath.ToLower().EndsWith(Constants.EXT_ZIP) ||
                    LocalPath.ToLower().EndsWith(Constants.EXT_7ZIP) ||
                    LocalPath.ToLower().EndsWith(Constants.EXT_RAR))
                    return true;

                return false;
            }
        }

        /// <summary>
        /// Flag to determine if mod is a craft.
        /// </summary>
        public bool IsCraft
        {
            get
            {
                return LocalPath.ToLower().EndsWith(Constants.EXT_CRAFT);
            }
        }


        /// <summary>
        /// Creates a new instance of the class ModInfos.
        /// </summary>
        public ModInfo()
        {
            Name = string.Empty;

            LocalPath = string.Empty;

            SiteHandlerName = Messages.NONE;

            ModURL = string.Empty;
            AdditionalURL = string.Empty;
            AvcURL = string.Empty;

            DownloadDate = DateTime.Now.ToString();
            CreationDate = string.Empty;

            Rating = "0 (0)";

            Downloads = "0";

            Author = string.Empty;

            ProductID = string.Empty;

            Version = string.Empty;
            KSPVersion = string.Empty;

            Note = string.Empty;
        }

    }
}