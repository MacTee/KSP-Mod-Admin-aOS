using System;
using System.Collections.Generic;
using System.Linq;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// The ModRegister class takes track of all file destinations of all added mods.
    /// If two mod files have the same destination they will be marked as colliding.
    /// To solve collisions the ModRegister class will remove the destination of all colliding mods files except the chosen one.
    /// </summary>
    public static class ModRegister
    {
        /// <summary>
        /// Flag to turn conflict detection on or off.
        /// </summary>
        public static bool ConflictDetectionOnOff { get; set; }

        /// <summary>
        /// Gets the flag if there are any known conflicts.
        /// </summary>
        public static bool HasConflicts
        {
            get
            {
                foreach (var entry in mRegisterdModFiles.Values)
                {
                    if (OptionsController.ConflictDetectionOnOff && entry.Count > 1 && entry.Any(node => node.IsFile))
                        return true;
                }

                return false;
            }
        }


        /// <summary>
        /// Dictionary of all registered mod file destinations.
        /// For destination collision detection.
        /// </summary>
        private static Dictionary<string, List<ModNode>> mRegisterdModFiles = new Dictionary<string, List<ModNode>>();

        
        /// <summary>
        /// Dictionary of all registered mod file destinations.
        /// For destination collision detection.
        /// </summary>
        public static Dictionary<string, List<ModNode>> RegisterdModFiles
        {
            get { return mRegisterdModFiles; }
            set { mRegisterdModFiles = value; }
        }


        /// <summary>
        /// Registers all mod files that have a destination.
        /// </summary>
        /// <param name="modRoot">The root node of the mod to register the file nodes from.</param>
        /// <returns>True if a collision with another mod was detected.</returns>
        public static bool RegisterMod(ModNode modRoot)
        {
            bool collisionDetected = false;
            List<ModNode> fileNodes = GetAllNodesWithDestination(modRoot);
            foreach (ModNode fileNode in fileNodes)
                if (RegisterModFile(fileNode))
                    collisionDetected = true;

            return collisionDetected;
        }

        /// <summary>
        /// Registers the mod file if it has a destination.
        /// </summary>
        /// <param name="fileNode">The file node to register.</param>
        /// <returns>True if a collision with another mod was detected.</returns>
        public static bool RegisterModFile(ModNode fileNode)
        {
            if (string.IsNullOrEmpty(fileNode.Destination))
                return false;

            if (!mRegisterdModFiles.ContainsKey(fileNode.Destination.ToLower()))
            {
                // Add fileNode to register
                List<ModNode> list = new List<ModNode> { fileNode };
                mRegisterdModFiles.Add(fileNode.Destination.ToLower(), list);
            }
            else
            {
                if (!mRegisterdModFiles[fileNode.Destination.ToLower()].Contains(fileNode)) ////!AlreadyKnown(fileNode))
                {
                    mRegisterdModFiles[fileNode.Destination.ToLower()].Add(fileNode);

                    // Set collision flag
                    if (fileNode.IsFile || fileNode.HasChildCollision)
                    {
                        foreach (ModNode node in mRegisterdModFiles[fileNode.Destination.ToLower()])
                            node.HasCollision = true;
                    }
                    // Ignore folders for now ...
                    ////else
                    ////{
                    ////    if (HaveCollisionsSameRoot(fileNode) || fileNode.Text.Trim().Equals(Constants.GAMEDATA, StringComparison.CurrentCultureIgnoreCase))
                    ////        return false;

                    ////    foreach (ModNode node in mRegisterdModFiles[fileNode.Destination.ToLower()])
                    ////        node.HasCollision = true;
                    ////}

                    // Only true wenn collision detection is on
                    return ConflictDetectionOnOff;
                }
            }

            return false;
        }

        /// <summary>
        /// Clears the complete mod register.
        /// </summary>
        public static void Clear()
        {
            List<ModNode> toUnregister = new List<ModNode>();
            foreach (var dest in mRegisterdModFiles)
                toUnregister.AddRange(dest.Value);

            foreach (var file in toUnregister)
                RemoveRegisteredModFile(file);

            mRegisterdModFiles.Clear();
        }

        /// <summary>
        /// Removes the registered mod files of a mod.
        /// </summary>
        /// <param name="modRoot">The root node of the mod from which the files should be unregistered.</param>
        public static void RemoveRegisteredMod(ModNode modRoot)
        {
            RemoveRegisteredModFile(modRoot);
            foreach (ModNode child in modRoot.Nodes)
                RemoveRegisteredMod(child);
        }

        /// <summary>
        /// Removes the mod file from the registration.
        /// </summary>
        /// <param name="fileNode">The file node to unregister.</param>
        public static void RemoveRegisteredModFile(ModNode fileNode)
        {
            if (!string.IsNullOrEmpty(fileNode.Destination) && mRegisterdModFiles.ContainsKey(fileNode.Destination.ToLower()) &&
                mRegisterdModFiles[fileNode.Destination.ToLower()].Contains(fileNode))
            {
                mRegisterdModFiles[fileNode.Destination.ToLower()].Remove(fileNode);
                fileNode.HasCollision = false;

                if (mRegisterdModFiles[fileNode.Destination.ToLower()].Count == 0)
                    mRegisterdModFiles.Remove(fileNode.Destination.ToLower());
                else if (mRegisterdModFiles[fileNode.Destination.ToLower()].Count == 1)
                    mRegisterdModFiles[fileNode.Destination.ToLower()][0].HasCollision = false;
                else if (mRegisterdModFiles[fileNode.Destination.ToLower()].Count > 1)
                {
                    if (HaveCollisionsSameRoot(fileNode))
                        mRegisterdModFiles[fileNode.Destination.ToLower()][0].HasCollision = false;
                }
            }
        }

        /// <summary>
        /// Solves the destination collision for a Mod.
        /// Removes the destination of all mod files registered to the destination of 
        /// </summary>
        /// <param name="modRoot">The mod to keep the destination.</param>
        public static void SolveCollisions(ModNode modRoot)
        {
            var collidingNodes = GetAllCollisionNodes(modRoot);
            foreach (ModNode collidingNode in collidingNodes)
            {
                if (string.IsNullOrEmpty(collidingNode.Destination) || !mRegisterdModFiles.ContainsKey(collidingNode.Destination.ToLower()))
                {
                    collidingNode.HasCollision = false;
                    continue;
                }

                List<ModNode> removeDestinationNodes = mRegisterdModFiles[collidingNode.Destination.ToLower()].Where(node => node != collidingNode).ToList();
                foreach (ModNode delNode in removeDestinationNodes)
                {
                    RemoveRegisteredModFile(delNode);

                    // TODO:
                    ////TreeViewEx.ChangeCheckedState(delNode, false, true, true); 

                    if (!delNode.IsFile && delNode.IsInstalled)
                        ModSelectionController.ProcessMods(new ModNode[] { delNode }, true);

                    delNode.SetDestinationPaths(string.Empty);
                }
            }
        }

        /// <summary>
        /// Returns a flat list of all colliding modNodes for all fileNodes of the passed node.
        /// </summary>
        /// <returns>A flat list of all colliding modNodes for all fileNodes of the passed node.</returns>
        public static List<ModNode> GetCollisionModFiles(ModNode modNode)
        {
            List<ModNode> result = new List<ModNode>();
            if (!ConflictDetectionOnOff)
                return result;

            if (modNode.IsFile)
            {
                if (mRegisterdModFiles.ContainsKey(modNode.Destination.ToLower()))
                    result.AddRange(mRegisterdModFiles[modNode.Destination.ToLower()].Where(node => node != modNode));
            }
            else
            {
                foreach (var fileNode in modNode.ZipRoot.GetAllFileNodes())
                {
                    if (mRegisterdModFiles.ContainsKey(fileNode.Destination.ToLower()))
                        result.AddRange(mRegisterdModFiles[fileNode.Destination.ToLower()].Where(regFileNode => regFileNode != modNode));
                }
            }
            return result;
        }

        /// <summary>
        /// Returns all colliding mods (ZipRoots) that collides with the passed mod.
        /// </summary>
        /// <param name="modNode">The mod to get the collision mods for.</param>
        /// <returns>All colliding mods (ZipRoots) that collides with the passed mod.</returns>
        public static List<ModNode> GetCollisionModsByCollisionMod(ModNode modNode)
        {
            List<ModNode> result = new List<ModNode>();
            if (!ConflictDetectionOnOff)
                return result;

            foreach (var fileNode in modNode.ZipRoot.GetAllFileNodes())
            {
                if (mRegisterdModFiles.ContainsKey(fileNode.Destination.ToLower()))
                {
                    foreach (var regFileNode in mRegisterdModFiles[fileNode.Destination.ToLower()])
                    {
                        if (regFileNode != modNode && !result.Contains(regFileNode.ZipRoot))
                            result.Add(regFileNode.ZipRoot);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a list of ModNode that have a colliding destination.
        /// </summary>
        /// <param name="node">The node to start the search from.</param>
        /// <param name="fileNodes">For recursive calls! List of already found file nodes.</param>
        /// <returns>A list of ModNode that have a colliding destination.</returns>
        public static List<ModNode> GetAllCollisionNodes(ModNode node, List<ModNode> fileNodes = null)
        {
            if (fileNodes == null)
                fileNodes = new List<ModNode>();

            if (!ConflictDetectionOnOff)
                return fileNodes;

            if (node.HasCollision)
                fileNodes.Add(node);

            foreach (ModNode childNode in node.Nodes)
                GetAllCollisionNodes(childNode, fileNodes);

            return fileNodes;
        }

        /// <summary>
        /// Gets the conflicting mod files as a list of ConflictInfoNode.
        /// </summary>
        /// <returns>A list of ConflictInfoNode</returns>
        public static List<ConflictInfoNode> GetConflictInfos()
        {
            List<ConflictInfoNode> result = new List<ConflictInfoNode>();

            foreach (var registerdModFile in mRegisterdModFiles)
            {
                var files = registerdModFile.Value.Where(x => x.IsFile).ToList();
                if (files.Count > 1)
                    result.Add(new ConflictInfoNode(files));
            }

            return result;
        }

        /// <summary>
        /// Checks if one of the registered ModNodes with the fileNode destination have the same ZipRoot as the passed fileNode.
        /// </summary>
        /// <param name="fileNode">The ModNode to check.</param>
        /// <returns>True if one of the registered ModNodes have the same ZipRoot.</returns>
        private static bool HaveCollisionsSameRoot(ModNode fileNode)
        {
            bool differentRootFound = false;

            ModNode zipRoot = fileNode.ZipRoot;
            foreach (ModNode node in mRegisterdModFiles[fileNode.Destination.ToLower()])
            {
                if (node.ZipRoot == zipRoot)
                    continue;

                differentRootFound = true;
                break;
            }

            return !differentRootFound;
        }

        /// <summary>
        /// Returns all ModNods with a destination.
        /// </summary>
        /// <param name="modNode">The ModNode to start with.</param>
        /// <param name="list">For recursive calls.</param>
        /// <returns>A list of all ModNods with a destination.</returns>
        private static List<ModNode> GetAllNodesWithDestination(ModNode modNode, List<ModNode> list = null)
        {
            if (list == null)
                list = new List<ModNode>();

            if (modNode.HasDestination)
                list.Add(modNode);

            foreach (ModNode n in modNode.Nodes)
                GetAllNodesWithDestination(n, list);

            return list;
        }
    }
}
