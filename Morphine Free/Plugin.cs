using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;
using BepInEx;
using HarmonyLib;
using Morphine.Modules;
using System.IO;
using Morphine.Plugins;
using Morphine.Framework;

namespace Morphine
{
    public struct Metadata
    {
        public const string GUID = "com.rxin.morphinefree";
        public const string Name = "Morphine Free";
        public const string Version = "1.0.0";
    }

    [BepInPlugin(Metadata.GUID, Metadata.Name, Metadata.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }

        void Awake()
        {
            // Setup Patch
            base.Logger.LogInfo("Setting Up Plugin.");
            Instance = this;
            var harmony = new Harmony(Metadata.GUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            // Create Needed Directories
            string PluginsDirectory = Paths.PluginPath;
            Directory.CreateDirectory(Path.Combine(PluginsDirectory, "Morphine Free"));
            Directory.CreateDirectory(Path.Combine(PluginsDirectory, "Morphine Free\\Plugins"));
            Directory.CreateDirectory(Path.Combine(PluginsDirectory, "Morphine Free\\Config"));

            // Load all Plugins
            PluginLoader.Instance.StartPlugins();
            base.Logger.LogInfo($"Successfully Loaded {PluginLoader.Instance.Plugins.Count} Plugins!");
        }

        void Update()
        {
            try
            {
                Menu.StartMenu();
                Passive.StartPassive();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }
        }
    }
}
