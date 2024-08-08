using Morphine.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Plugin_Example
{
    public class Plugin : IPlugin
    {
        public string Name => "Morphine Example Plugin";
        public bool NeedsMaster { get; set; } = false;
        public bool IsToggle { get; set; } = true;

        public void OnEnable()
        {
            Debug.Log($"Enabled {Name}");
        }

        public void OnDisable()
        {
            Debug.Log($"Disabled {Name}");
        }
    }
}
