using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Helper class to load DLLs that contains classes with a certain interface.
    /// </summary>
    public static class PluginLoader
    {
        /// <summary>
        /// Loads all DLLs from a folder that contains at least one class with a T_Interface interface.
        /// </summary>
        /// <typeparam name="T_Interface">The interface that at least one class with a T_Interface interface
        /// have to implement to load the DLL.</typeparam>
        /// <param name="path">The path to the folder with the DLLs to load.</param>
        /// <returns>A list of loaded DLLs that contains at least one class with a T_Interface interface.</returns>
        public static List<T_Interface> LoadPlugins<T_Interface>(string path)
        {
            if (!Directory.Exists(path))
                return new List<T_Interface>();

            string[] dllFileNames = Directory.GetFiles(path, "*.dll");

            List<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
            foreach (string dllFile in dllFileNames)
            {
                ////AssemblyName assemblyName = AssemblyName.GetAssemblyName(dllFile);
                Messenger.AddDebug("Loading plugin file: " + Path.GetFileName(dllFile));
                Assembly assembly = Assembly.LoadFile(dllFile);
                assemblies.Add(assembly);
            }

            return GetPlugins<T_Interface>(assemblies.ToArray());
        }

        /// <summary>
        /// Gets all wanted interfaces from the assembly list.
        /// </summary>
        /// <typeparam name="T_Interface">The type of the interfaces to get.</typeparam>
        /// <param name="assemblies">The assemblies to get the interfaces from.</param>
        /// <returns>List of all interfaces (of wanted type) found in the assemblies.</returns>
        public static List<T_Interface> GetPlugins<T_Interface>(Assembly[] assemblies)
        {
            Type pluginType = typeof(T_Interface);
            List<Type> pluginTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly == null) 
                    continue;

                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    // ignore interfaces and abstract classes.
                    if (type.IsInterface || type.IsAbstract)
                        continue;

                    if (type.GetInterface(pluginType.FullName) != null)
                        pluginTypes.Add(type);
                }
            }

            List<T_Interface> plugins = new List<T_Interface>(pluginTypes.Count);
            foreach (var type in pluginTypes)
                plugins.Add((T_Interface)Activator.CreateInstance(type));

            return plugins;
        }
    }
}
