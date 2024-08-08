using BepInEx;
using Morphine.Framework.Elements;
using Morphine.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Morphine.Plugins
{
    public class PluginLoader
    {
        public List<IPlugin> Plugins = new List<IPlugin>();

        public static PluginLoader Instance = new PluginLoader();

        public void StartPlugins()
        {
            string pluginsPath = Path.Combine(Paths.PluginPath, "Morphine Free\\Plugins");
            if (!Directory.Exists(pluginsPath))
            {
                Directory.CreateDirectory(pluginsPath);
            }

            var pluginFiles = Directory.GetFiles(pluginsPath, "*.dll");
            foreach (var file in pluginFiles)
            {
                try
                {
                    var assembly = Assembly.LoadFile(file);
                    var pluginTypes = assembly.GetTypes().Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                    foreach (var type in pluginTypes)
                    {
                        var plugin = (IPlugin)Activator.CreateInstance(type);
                        Plugins.Add(plugin);
                        ButtonInfo button = new ButtonInfo(plugin.Name, Framework.MenuBase.Category.Plugins, plugin.IsToggle, false, ()=>plugin.OnEnable(), ()=>plugin.OnDisable(), plugin.NeedsMaster);
                        Menu.Buttons.Add(button);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Failed to load plugin from {file}: {ex.Message}");
                }
            }
        }
    }
}
