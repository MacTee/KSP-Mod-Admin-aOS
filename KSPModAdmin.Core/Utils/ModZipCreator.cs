using System;
using System.Collections.Generic;
using System.IO;
using KSPModAdmin.Core.Model;
using SharpCompress.Archive;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Class to create zip archives of mods and crafts.
    /// </summary>
    public class ModZipCreator
    {
        #region Constants

        private const string TYPE = "type = ";
        private const string SHIPSPH = "Ships\\SPH\\";
        private const string SHIPVAB = "Ships\\VAB\\";

        #endregion

        /// <summary>
        /// Creates a zip file of the craft and adds it to the ModSelection.
        /// </summary>
        /// <param name="fullpath">Full path of the craft-file.</param>
        /// <returns>Path to the created zip archive.</returns>
        public static string CreateZipOfCraftFile(string fullpath)
        {
            string zipPath = string.Empty;

            try
            {
                string dir = string.Empty;
                using (StreamReader sr = new StreamReader(fullpath))
                {
                    string line = sr.ReadToEnd();
                    int index = line.IndexOf(TYPE);
                    if (index != -1)
                    {
                        string shipType = line.Substring(index + TYPE.Length, 3);
                        if (shipType.Equals(Constants.SPH, StringComparison.CurrentCultureIgnoreCase))
                            dir = SHIPSPH;
                        else
                            dir = SHIPVAB;
                    }
                }

                zipPath = Path.Combine(Path.GetDirectoryName(fullpath), Path.GetFileNameWithoutExtension(fullpath) + Constants.EXT_ZIP);
                using (var archive = ZipArchive.Create())
                {
                    archive.AddEntry(Path.Combine(dir, Path.GetFileName(fullpath)), fullpath);
                    archive.SaveTo(zipPath, CompressionType.Deflate);
                }

                try
                {
                    File.Delete(fullpath);
                }
                catch (Exception ex)
                {
                    Messenger.AddError(string.Format(Messages.MSG_MOD_ERROR_CANT_DELETE_0, fullpath), ex);
                    zipPath = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Messenger.AddError(Messages.MSG_MOD_ERROR_ZIP_CREATION_FAILED, ex);
                zipPath = string.Empty;
            }

            return zipPath;
        }

        /// <summary>
        /// Creates a zip for each root node in the passed node list.
        /// </summary>
        /// <param name="nodes">List of root nodes to create zips for.</param>
        /// <param name="filePath">the path (folder) where the new Zip should be saved to.</param>
        /// <returns>True on success.</returns>
        public static bool CreateZip(List<ModNode> nodes, string filePath)
        {
            // create the zip
            List<string> done = new List<string>();
            foreach (ModNode node in nodes)
            {
                ModNode root = node.ZipRoot;
                if (root != null && !File.Exists(root.LocalPath) && !done.Contains(root.LocalPath))
                {
                    string zipPath = Path.Combine(filePath, Path.GetFileNameWithoutExtension(root.Name) + Constants.EXT_ZIP);
                    using (var zip = ZipArchive.Create())
                    {
                        int nodecount = 0;
                        foreach (ModNode child in root.Nodes)
                            nodecount = CreateZipEntry(zip, child, nodecount);

                        zip.SaveTo(zipPath, CompressionType.None);
                    }
                    Messenger.AddInfo(string.Format(Messages.MSG_ZIP_0_CREATED, root.Name));
                    done.Add(root.Name);
                    root.Key = zipPath;
                }
            }

            return true;
        }

        /// <summary>
        /// Creates a ZipEntry of the passed node and its childs to the passed zip file.
        /// </summary>
        /// <param name="zip">The zip file to add the new zip entry to.</param>
        /// <param name="node">The node to create a zip entry for.</param>
        /// <param name="processedNodeCount">Count of the processed nodes (for recursive calls only).</param>
        /// <returns>Count of the processed nodes.</returns>
        private static int CreateZipEntry(ZipArchive zip, ModNode node, int processedNodeCount = 0)
        {
            string absPath = KSPPathHelper.GetAbsolutePath(node.Destination);
            string gameDataPath = KSPPathHelper.GetPath(KSPPaths.GameData);
            string path = absPath.Replace(gameDataPath + Path.DirectorySeparatorChar, string.Empty);
            if (node.IsFile && node.IsInstalled)
                zip.AddEntry(path, absPath);

            foreach (ModNode child in node.Nodes)
                processedNodeCount = CreateZipEntry(zip, child, processedNodeCount);

            return processedNodeCount;
        }
    }
}
