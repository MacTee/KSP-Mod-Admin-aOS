using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using KSPModAdmin.Core.Utils;
using Newtonsoft.Json;
using SharpCompress.Archive;

namespace KSPMODAdmin.Core.Utils.Ckan
{
    /// <summary>
    /// Helper class to manage the Ckan Repositories.
    /// </summary>
    public class CkanRepoManager
    {
        /// <summary>
        /// URL to the default Ckan Repository.
        /// </summary>
        public static readonly Uri DefaultRepoURL = new Uri("https://github.com/KSP-CKAN/CKAN-meta/archive/master.zip");

        /// <summary>
        /// URL to the Ckan Repository list of available repositories.
        /// </summary>
        public static readonly Uri MasterRepoListURL = new Uri("http://api.ksp-ckan.org/mirrors");


        /// <summary>
        /// Downloads the list of Ckan Repositories from the passed URL.
        /// </summary>
        /// <param name="repoListURL">The URL to get the Ckan Repositories from.</param>
        /// <returns>The list of Ckan Repositories from the passed URL.</returns>
        public static CkanRepositories GetRepositoryList(Uri repoListURL = null)
        {
            CkanRepositories repos;
            if (repoListURL != null)
            {
                // load repositories from repoListURL
                Messenger.AddInfo($"Downloading repository list from \"{repoListURL.AbsoluteUri}\"...");

                var content = Www.Load(repoListURL.AbsoluteUri);
                repos = JsonConvert.DeserializeObject<CkanRepositories>(content);

                Messenger.AddInfo($"Downloading repository list done. {repos.repositories.Length} repositories found.");
            }
            else
            {
                // create default repository
                repos = new CkanRepositories { repositories = new [] { CkanRepository.GitHubRepository } };
            }

            return repos;
        }

        /// <summary>
        /// Gets the named Ckan Repository from the Ckan Repository list.
        /// </summary>
        /// <param name="repositories">List of Ckan Repository to search in.</param>
        /// <param name="repoName">Name of the Ckan Repository to look for.</param>
        /// <returns>The named Ckan Repository from the Ckan Repository list or null.</returns>
        public static CkanRepository GetRepository(CkanRepositories repositories, string repoName)
        {
            return repositories.repositories.FirstOrDefault(x => x.name.Equals(repoName, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Downloads the Ckan Repository archive to full path.
        /// </summary>
        /// <param name="repo">The Ckan Repository to get the Ckan Repository archive for.</param>
        /// <param name="fullpath">The full path to write the downloaded file to.</param>
        /// <param name="onDownloadComplete">Callback function for the DownloadComplete event.</param>
        /// <param name="onDownloadProgressChanged">Callback function for the DownloadProgressChanged event.</param>
        /// <returns>The new created CkanArchive which was constructed from the downloaded Ckan Repository archive.</returns>
        public static bool DownloadRepositoryArchive(CkanRepository repo, string fullpath, AsyncCompletedEventHandler onDownloadComplete = null, DownloadProgressChangedEventHandler onDownloadProgressChanged = null)
        {
            var async = onDownloadComplete != null;
            Messenger.AddInfo($"Downloading repository archive \"{repo.name}\" from \"{repo.uri.AbsoluteUri}\"...");
            try
            {
                using (var client = new WebClient())
                {
                    if (onDownloadProgressChanged != null)
                        client.DownloadProgressChanged += onDownloadProgressChanged;

                    if (onDownloadComplete != null)
                        client.DownloadFileCompleted += (sender, args) =>
                        {
                            onDownloadComplete(sender, args);
                            Messenger.AddInfo($"Downloading repository archive \"{repo.name}\" done.");
                        };

                    if (async)
                        client.DownloadFileAsync(repo.uri, fullpath);
                    else
                        client.DownloadFile(repo.uri, fullpath);
                }
            }
            catch (Exception ex)
            {
                Messenger.AddError($"Error during downloading repository archive \"{repo.name}\" Error message: \"{ex.Message}\".", ex);
            }

            if (async)
                return false;

            Messenger.AddInfo($"Downloading repository archive \"{repo.name}\" done.");
            return File.Exists(fullpath);
        }

        /// <summary>
        /// Creates a CkanArchive from a Ckan Repository archive file.
        /// </summary>
        /// <param name="fullpath">The full path to the Ckan Repository archive.</param>
        /// <returns>The new created CkanArchive from a Ckan Repository archive file.</returns>
        public static CkanArchive CreateRepositoryArchive(string fullpath)
        {
            if (string.IsNullOrEmpty(fullpath) || !File.Exists(fullpath))
                return null;

            Messenger.AddInfo($"Reading repository archive \"{fullpath}\"...");
            var repoArchive = new CkanArchive { FullPath = fullpath };
            var errors = new List<string[]>();
            using (IArchive archive = ArchiveFactory.Open(repoArchive.FullPath))
            {
                foreach (IArchiveEntry entry in archive.Entries)
                {
                    if (Path.GetDirectoryName(entry.FilePath).Split(Path.DirectorySeparatorChar).Length != 2)
                    {
                        Messenger.AddInfo($"Archive entry \"{entry.FilePath}\" skipped.");
                        continue;
                    }

                    if (entry.IsDirectory)
                    {
                        try
                        {
                            var mod = CreateMod(entry);
                            repoArchive.Mods.Add(mod.Name, mod);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(new[] { string.Empty, entry.FilePath, ex.Message });
                        }
                    }
                    else
                    {
                        var name = GetDirectoryName(entry.FilePath);
                        if (!repoArchive.Mods.ContainsKey(name))
                            continue;

                        var mod = repoArchive.Mods[name];
                        try
                        {
                            mod.ModInfos.Add(CreateModInfos(entry, mod));
                        }
                        catch (Exception ex)
                        {
                            errors.Add(new[] { mod.Name, entry.FilePath, ex.Message });
                        }
                    }
                }
            }

            if (errors.Count > 0)
            {
                Messenger.AddInfo("Failed to read the following entries:");
                foreach (var error in errors)
                {
                    Messenger.AddInfo($"Mod: \"{error[0]}\", Path: \"{error[1]}\" Error:\"{error[2]}\".");
                }
            }

            Messenger.AddInfo($"Reading repository archive \"{fullpath}\" done.");

            return repoArchive;
        }

        private static CkanMod CreateMod(IArchiveEntry archiveEntry)
        {
            var mod = new CkanMod
            {
                ArchivePath = archiveEntry.FilePath,
                Name = GetDirectoryName(archiveEntry.FilePath)
            };

            Messenger.AddInfo($"Mod \"{mod.Name}\" created from \"{archiveEntry.FilePath}\"");

            return mod;
        }

        private static CkanModInfo CreateModInfos(IArchiveEntry archiveEntry, CkanMod ckanMod)
        {
            var ms = new MemoryStream();
            archiveEntry.WriteTo(ms);
            ms.Position = 0;
            using (var sr = new StreamReader(ms))
            {
                var content = sr.ReadToEnd();
                var modInfos = JsonConvert.DeserializeObject<CkanModInfo>(content);
                Messenger.AddInfo($"ModInfos \"{modInfos.name}\"-\"{modInfos.version}\" created from \"{archiveEntry.FilePath}\"");
                modInfos.Mod = ckanMod;
                modInfos.version = NormalizeVersion(modInfos.version);
                return modInfos;
            }
        }

        private static string GetDirectoryName(string dirPath)
        {
            if (string.IsNullOrEmpty(dirPath))
                return string.Empty;

            var dirs = Path.GetDirectoryName(dirPath).Split(Path.DirectorySeparatorChar);
            return dirs.Length > 0 ? dirs[dirs.Length - 1] : string.Empty;
        }

        private static string NormalizeVersion(string version)
        {
            if (version.StartsWith("v", StringComparison.CurrentCultureIgnoreCase))
                version = version.Substring(1);

            if (version.StartsWith("."))
                version = "0" + version;

            return version;
        }
    }
}