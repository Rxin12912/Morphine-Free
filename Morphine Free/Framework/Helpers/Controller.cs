using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphine.Framework.Helpers
{
    public class Controller
    {
        public static bool GetButton(float grabValue)
        {
            return grabValue >= 0.75f;
        }
    }
}
