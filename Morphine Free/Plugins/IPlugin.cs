using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphine.Plugins
{
    public interface IPlugin
    {
        string Name { get; }
        bool IsToggle { get; set; }
        bool NeedsMaster { get; set; }
        void OnEnable();
        void OnDisable();
    }
}
