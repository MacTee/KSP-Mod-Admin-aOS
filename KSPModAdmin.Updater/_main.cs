using System;
using System.Diagnostics;
using System.IO;
using SharpCompress.Archive;
using SharpCompress.Archive.Zip;

namespace KSPModAdmin_Updater
{
    class Program
    {
        const string BACKUPFILE = "Update_Backup.zip";
        const string KSPMODADMINFILE = "KSPModAdmin.exe";
        const string SHARPCOMPRESSFILE = "SharpCompress.dll";
        const string BACKUPEXTENSION = "_BACKUP";


        static string Version = string.Empty;

        static string ProcessName = string.Empty;

        static bool ValidArchivePath = false;
        static string ArchivePath = string.Empty;

        static bool ValidDestinationPath = false;
        static string DestinationPath = string.Empty;


        static void Main(string[] args)
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
                    RevertFromBackup();
                else
                    DeleteBackup();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                RestartKSP();
            }

            Console.WriteLine();
        }


        static bool ParseArguments(string[] args)
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
                string[] arg = argValuePair.Split('=');
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
                        Version = arg[1];
                        break;

                    case "process":
                        ProcessName = arg[1];
                        break;

                    case "archive":
                        try
                        {
                            ArchivePath = string.Empty;
                            ValidArchivePath = false;

                            if (System.IO.File.Exists(arg[1]))
                            {
                                ArchivePath = arg[1];
                                ValidArchivePath = true;
                            }
                        }
                        catch { }
                        break;

                    case "dest":
                        try
                        {
                            DestinationPath = string.Empty;
                            ValidDestinationPath = false;

                            if (System.IO.Directory.Exists(arg[1]))
                            {
                                DestinationPath = arg[1];
                                ValidDestinationPath = true;
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

        static bool ValidateParameter()
        {
            Console.Write("Validating parameter ...");

            bool result = true;
            if (!ValidArchivePath)
            {
                Console.WriteLine();
                Console.WriteLine("Error: Archive path invalid!");
                result = false;
            }

            if (!ValidDestinationPath)
            {
                if (result)
                    Console.WriteLine();
                Console.WriteLine("Error: Destination path invalid!");
                result = false;
            }

            if (ProcessName == string.Empty)
            {
                if (result)
                    Console.WriteLine();
                Console.WriteLine("Error: Argument \"ProcessName\" is missing!");
                result = false;
            }

            if (Version == string.Empty)
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

        static bool WaitTillShutDown()
        {
            Console.Write("Wait till " + ProcessName + " turns off .");
            var exists = System.Diagnostics.Process.GetProcessesByName(ProcessName); 
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            while (exists.Length > 0)
            {
                System.Threading.Thread.Sleep(500);
                Console.Write(".");
                if (watch.ElapsedMilliseconds > 50000)
                {
                    Console.WriteLine();
                    Console.WriteLine(ProcessName + " taks to long to shutdown. Update aborted.");
                    Console.ReadKey();
                    return false;
                }

                exists = System.Diagnostics.Process.GetProcessesByName(ProcessName); 
            } 
            Console.WriteLine();
            Console.WriteLine(ProcessName + " is off.");

            return true;
        }

        static bool Backup()
        {
            Console.WriteLine("Backup current version.");

            bool result = true;
            try
            {
                if (File.Exists(Path.Combine(DestinationPath, KSPMODADMINFILE + BACKUPEXTENSION)))
                    File.Delete(Path.Combine(DestinationPath, KSPMODADMINFILE + BACKUPEXTENSION));
                File.Copy(Path.Combine(DestinationPath, KSPMODADMINFILE), Path.Combine(DestinationPath, KSPMODADMINFILE + BACKUPEXTENSION));

                //if (File.Exists(Path.Combine(DestinationPath, SHARPCOMPRESSFILE + BACKUPEXTENSION)))
                //    File.Delete(Path.Combine(DestinationPath, SHARPCOMPRESSFILE + BACKUPEXTENSION));
                //File.Copy(Path.Combine(DestinationPath, SHARPCOMPRESSFILE), Path.Combine(DestinationPath, SHARPCOMPRESSFILE + BACKUPEXTENSION));

                //if (File.Exists(Path.Combine(DestinationPath, BACKUPFILE)))
                //    File.Delete(Path.Combine(DestinationPath, BACKUPFILE));

                //using (var zip = ZipArchive.Create())
                //{
                //    zip.AddEntry(KSPMODADMINFILE, Path.Combine(DestinationPath, KSPMODADMINFILE));
                //    zip.AddEntry(SHARPCOMPRESSFILE, Path.Combine(DestinationPath, SHARPCOMPRESSFILE));
                //    zip.SaveTo(Path.Combine(DestinationPath, BACKUPFILE), CompressionType.None);
                //}
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

        static bool Update()
        {
            Console.WriteLine("Updating to version " + Version);

            if (!DeleteCurrentVersion())
                return false;

            if (!CopyNewVersion())
                return false;

            return true;
        }

        static bool DeleteCurrentVersion()
        {
            try
            {
                File.Delete(Path.Combine(DestinationPath, KSPMODADMINFILE));
                Console.WriteLine(KSPMODADMINFILE + " deleted.");
                //File.Delete(Path.Combine(DestinationPath, SHARPCOMPRESSFILE));
                //Console.WriteLine(SHARPCOMPRESSFILE + " deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            return true;
        }

        static bool CopyNewVersion()
        {
            try
            {
                ExtractKSPModAdmin();
                Console.WriteLine("KSPModAdmin is updated to version " + Version);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            return true;
        }

        static void ExtractKSPModAdmin()
        {
            using (var zip = ZipArchive.Open(ArchivePath))
            {
                foreach (var entry in zip.Entries)
                {
                    if (entry.FilePath.ToLower() == Version + "/kspmodadmin/kspmodadmin.exe" ||
                        entry.FilePath.ToLower() == Version + "\\kspmodadmin\\kspmodadmin.exe")
                    {
                        entry.WriteToFile(Path.Combine(DestinationPath, KSPMODADMINFILE));
                        Console.WriteLine(KSPMODADMINFILE + " extracted.");
                    }

                    //else if (entry.FilePath.ToLower() == Version + "/kspmodadmin/sharpcompress.dll" ||
                    //         entry.FilePath.ToLower() == Version + "\\kspmodaAdmin\\sharpcompress.dll")
                    //{
                    //    entry.WriteToFile(Path.Combine(DestinationPath, SHARPCOMPRESSFILE));
                    //    Console.WriteLine(SHARPCOMPRESSFILE + " extracted.");
                    //}
                }
            }
        }

        static void RevertFromBackup()
        {
            Console.WriteLine("Error: Reverting to last version.");
            ExtractKSPModAdmin();
        }

        static void DeleteBackup()
        {
            Console.WriteLine("Deleting backup files.");

            try
            {
                File.Delete(Path.Combine(DestinationPath, KSPMODADMINFILE + BACKUPEXTENSION));
                //File.Delete(Path.Combine(DestinationPath, SHARPCOMPRESSFILE + BACKUPEXTENSION));
                //File.Delete(Path.Combine(DestinationPath, BACKUPFILE));
                Console.WriteLine(BACKUPFILE + " deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void RestartKSP()
        {
            try
            {
                if (File.Exists(Path.Combine(DestinationPath, KSPMODADMINFILE)))
                {
                    Console.WriteLine("Restarting ...");

                    Process process = new Process();
                    process.StartInfo.FileName = Path.Combine(DestinationPath, KSPMODADMINFILE);
                    process.Start();
                }
                else
                {
                    Console.WriteLine("Sorry! Something went wrong.");
                    Console.WriteLine("Can't restart KSPModAdmin. Please extract the new version manualy.");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
