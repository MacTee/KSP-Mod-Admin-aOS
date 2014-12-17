using System;
using System.IO;

namespace KSPModAdmin.Core.Model
{
	public class ModInfo
	{
		public string Name { get; set; }

		public string LocalPath { get; set; }

        public string SiteHandlerName { get; set; }

	    public bool HasSiteHandler
	    {
	        get { return (SiteHandlerName != Messages.NONE && !string.IsNullOrEmpty(SiteHandlerName)); }
	    }

        public string ModURL { get; set; }

        public string AvcURL { get; set; }

		public string AdditionalURL { get; set; }

		public string DownloadDate { get; set; }

		public string Version { get; set; }

		public String KSPVersion { get; set; }

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

		public string CreationDate { get; set; }

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

		public string ChangeDate { get; set; }

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

		public string Rating { get; set; }

		public string Downloads { get; set; }

		public string Author { get; set; }

		public string ProductID { get; set; }

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

		public bool IsCraft
		{
			get
			{
				return LocalPath.ToLower().EndsWith(Constants.EXT_CRAFT);
			}
		}


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
		}
    }
}