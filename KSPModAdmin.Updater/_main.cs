using System;
using System.Diagnostics;
using System.IO;
using SharpCompress.Archive;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;
using SharpCompress.Reader;

namespace KSPModAdmin.Updater
{
    /// <summary>
    /// Entry point.
    /// </summary>
    public class Program
    {
        private const string BACKUPFILE = "Update_Backup.zip";
        private const string KSPMODADMINFILE = "KSPModAdmin.exe";
        private const string SHARPCOMPRESSFILE = "SharpCompress.dll";
        private const string BACKUPEXTENSION = "_BACKUP";


        private static string mVersion = string.Empty;

        private static string mProcessName = string.Empty;

        private static bool mValidArchivePath = false;
        private static string mArchivePath = string.Empty;

        private static bool mValidDestinationPath = false;
        private static string mDestinationPath = string.Empty;


        /// <summary>
        /// Main entry point for the KSP MA updater.
        /// </summary>
        public static void Main(string[] args)
        {
            Console.WriteLine("KSPModAdmin Updater v1.0");

            if (!ParseArguments(args))
                return;

            if (!ValidateParameter())
                return;

            if (!WaitTillShutDown())
                return;

            if (!Backup())
                return;

            try
            {
                if (!Update())
                {
                    RevertFromBackup();
                }
                else
                {
                    DeleteBackup();
                    Console.WriteLine("Successful updated.");
                    RestartKSP();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Done. (Press any key to quit)");
            Console.ReadKey();
        }


        private static bool ParseArguments(string[] args)
        {
            Console.Write("Parsing parameter ...");

            bool result = true;
            if (args.Length != 4)
            {
                Console.WriteLine();
                Console.WriteLine("Error: Invalid argument count");
                Console.WriteLine("Abort update!");
                result = false;
            }

            foreach (var argValuePair in args)
            {
                Console.Write(".");
                string[] arg = argValuePair.Trim(new[] { '\"' }).Split('=');
                if (arg.Length != 2)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: Invalid parameter count");
                    Console.WriteLine("Abort update!");
                    result = false;
                    break;
                }

                switch (arg[0].ToLower())
                {
                    case "version":
                        mVersion = arg[1];
                        break;

                    case "process":
                        mProcessName = arg[1];
                        break;

                    case "archive":
                        try
                        {
                            mArchivePath = string.Empty;
                            mValidArchivePath = false;

                            if (System.IO.File.Exists(arg[1]))
                            {
                                mArchivePath = arg[1];
                                mValidArchivePath = true;
                            }
                        }
                        catch { }
                        break;

                    case "dest":
                        try
                        {
                            mDestinationPath = string.Empty;
                            mValidDestinationPath = false;

                            if (System.IO.Directory.Exists(arg[1]))
                            {
                                mDestinationPath = arg[1];
                                mValidDestinationPath = true;
                            }
                        }
                        catch { }
                        break;

                    default:
                        result = false;
                            break;
                }
            }

            if (!result)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("KSPModAdmin_Udpater.exe version=\"<Version x.x.x>\" process=\"<Process name of KSPModAdmin>\" archive=\"<Path to the new KSPModAdmin archive>\" dest=\"<KSPModAdmin install path>\"");
                Console.ReadKey();
            }
            else
                Console.WriteLine(" Done");

            return result;
        }

        private static bool ValidateParameter()
        {
            Console.Write("Validating parameter ...");

            bool result = true;
            if (!mValidArchivePath)
            {
                Console.WriteLine();
                Console.WriteLine("Error: Archive path invalid!");
                result = false;
            }

            if (!mValidDestinationPath)
            {
                if (result)
                    Console.WriteLine();
                Console.WriteLine("Error: Destination path invalid!");
                result = false;
            }

            if (mProcessName == string.Empty)
            {
                if (result)
                    Console.WriteLine();
                Console.WriteLine("Error: Argument \"ProcessName\" is missing!");
                result = false;
            }

            if (mVersion == string.Empty)
            {
                if (result)
                    Console.WriteLine();
                Console.WriteLine("Error: Argument \"Version\" is missing!");
                result = false;
            }

            if (!result)
                Console.ReadKey();
            else
                Console.WriteLine(" Done");

            return result;
        }

        private static bool WaitTillShutDown()
        {
            Console.Write("Wait till " + mProcessName + " turns off .");
            var exists = System.Diagnostics.Process.GetProcessesByName(mProcessName); 
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            while (exists.Length > 0)
            {
                System.Threading.Thread.Sleep(500);
                Console.Write(".");
                if (watch.ElapsedMilliseconds > 50000)
                {
                    Console.WriteLine();
                    Console.WriteLine(mProcessName + " taks to long to shutdown. Update aborted.");
                    Console.ReadKey();
                    return false;
                }

                exists = System.Diagnostics.Process.GetProcessesByName(mProcessName); 
            } 

            Console.WriteLine();
            Console.WriteLine(mProcessName + " is off.");

            return true;
        }

        private static bool Backup()
        {
            Console.WriteLine("Backup current version.");

            bool result = true;
            try
            {
                string backupPath = Path.Combine(mDestinationPath, BACKUPFILE);
                if (File.Exists(backupPath))
                    File.Delete(backupPath);

                using (ZipArchive archive = ZipArchive.Create())
                {
                    archive.AddAllFromDirectory(mDestinationPath);
                    archive.SaveTo(backupPath, CompressionType.Deflate);
                }

                #region old code

                ////if (File.Exists(Path.Combine(DestinationPath, KSPMODADMINFILE + BACKUPEXTENSION)))
                ////    File.Delete(Path.Combine(DestinationPath, KSPMODADMINFILE + BACKUPEXTENSION));
                ////File.Copy(Path.Combine(DestinationPath, KSPMODADMINFILE), Path.Combine(DestinationPath, KSPMODADMINFILE + BACKUPEXTENSION));

                ////if (File.Exists(Path.Combine(DestinationPath, SHARPCOMPRESSFILE + BACKUPEXTENSION)))
                ////    File.Delete(Path.Combine(DestinationPath, SHARPCOMPRESSFILE + BACKUPEXTENSION));
                ////File.Copy(Path.Combine(DestinationPath, SHARPCOMPRESSFILE), Path.Combine(DestinationPath, SHARPCOMPRESSFILE + BACKUPEXTENSION));

                ////if (File.Exists(Path.Combine(DestinationPath, BACKUPFILE)))
                ////    File.Delete(Path.Combine(DestinationPath, BACKUPFILE));

                ////using (var zip = ZipArchive.Create())
                ////{
                ////    zip.AddEntry(KSPMODADMINFILE, Path.Combine(DestinationPath, KSPMODADMINFILE));
                ////    zip.AddEntry(SHARPCOMPRESSFILE, Path.Combine(DestinationPath, SHARPCOMPRESSFILE));
                ////    zip.SaveTo(Path.Combine(DestinationPath, BACKUPFILE), CompressionType.None);
                ////}
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error during backup: {0}", ex.Message));
                Console.WriteLine("Update aborted!");
                Console.ReadKey();
                result = false;
            }

            return result;
        }

        private static bool Update()
        {
            Console.WriteLine("Updating to version " + mVersion);

            ////if (!DeleteCurrentVersion())
            ////    return false;

            if (!CopyNewVersion())
                return false;

            return true;
        }

        private static bool DeleteCurrentVersion()
        {
            try
            {
                File.Delete(Path.Combine(mDestinationPath, KSPMODADMINFILE));
                Console.WriteLine(KSPMODADMINFILE + " deleted.");
                ////File.Delete(Path.Combine(DestinationPath, SHARPCOMPRESSFILE));
                ////Console.WriteLine(SHARPCOMPRESSFILE + " deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            return true;
        }

        private static bool CopyNewVersion()
        {
            try
            {
                ExtractKSPModAdmin(mArchivePath);
                Console.WriteLine("KSPModAdmin is updated to version " + mVersion);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            return true;
        }

        private static void ExtractKSPModAdmin(string path, bool revertBackup = false)
        {
            using (Stream stream = File.OpenRead(path))
            {
                var reader = ReaderFactory.Open(stream);
                while (reader.MoveToNextEntry())
                {
                    string versiondir1 = "v" + mVersion + "\\KSPModAdmin\\";
                    string versiondir2 = "v" + mVersion + "/KSPModAdmin/";
                    string versiondir3 = "v" + mVersion + "\\KSP Mod Admin\\";
                    string versiondir4 = "v" + mVersion + "/KSP Mod Admin/";

                    if (reader.Entry.IsDirectory)
                        continue;

                    if (!revertBackup &&
                        !reader.Entry.FilePath.Contains(versiondir1) &&
                        !reader.Entry.FilePath.Contains(versiondir2) &&
                        !reader.Entry.FilePath.Contains(versiondir3) &&
                        !reader.Entry.FilePath.Contains(versiondir4))
                    {
                        ////Console.WriteLine("DEBUG -> {0} skipped 1.", reader.Entry.FilePath);
                        continue;
                    }
                        
                    if (reader.Entry.FilePath.Contains("KSPModAdmin.Updater.exe") ||
                        reader.Entry.FilePath.Contains("SharpCompress.dll"))
                    {
                        ////Console.WriteLine("DEBUG -> {0} skipped 2.", reader.Entry.FilePath);
                        continue;
                    }
                        
                    ////Console.WriteLine("DEBUG -> FilePath {0}", reader.Entry.FilePath);
                    string relativePath = reader.Entry.FilePath.Replace(versiondir1, string.Empty).Replace(versiondir2, string.Empty).Replace(versiondir3, string.Empty).Replace(versiondir4, string.Empty);
                    ////Console.WriteLine("DEBUG -> relativePath {0}", relativePath);
                    string fullpath = Path.Combine(mDestinationPath, relativePath);
                    string pathOnly = Path.GetDirectoryName(fullpath);
                    if (!Directory.Exists(pathOnly))
                        Directory.CreateDirectory(pathOnly);
                    Console.WriteLine("Extracting {0}", relativePath);
                    reader.WriteEntryToFile(fullpath, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                }
            }
        }

        private static void RevertFromBackup()
        {
            Console.WriteLine("Error: Reverting to last version.");
            ExtractKSPModAdmin(Path.Combine(mDestinationPath, BACKUPFILE), true);
        }

        private static void DeleteBackup()
        {
            Console.WriteLine("Deleting backup files.");

            try
            {
                ////File.Delete(Path.Combine(DestinationPath, BACKUPFILE));
                Console.WriteLine(BACKUPFILE + " deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void RestartKSP()
        {
            try
            {
                if (File.Exists(Path.Combine(mDestinationPath, KSPMODADMINFILE)))
                {
                    Console.WriteLine("Restarting ...");

                    Process process = new Process();
                    process.StartInfo.FileName = Path.Combine(mDestinationPath, KSPMODADMINFILE);
                    process.Start();
                }
                else
                {
                    Console.WriteLine("Sorry! Something went wrong.");
                    Console.WriteLine("Can't restart KSPModAdmin. Please extract the new version manualy.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
