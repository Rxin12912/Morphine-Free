using Morphine.Framework.Elements;
using Morphine.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphine.Features
{
    public class Utility
    {
        public static ButtonInfo GetButton(string name)
        {
            foreach (ButtonInfo button in Menu.Buttons)
            {
                if (button.buttonText.Contains(name))
                {
                    return button;
                }
            }
            return null;
        }
    }
}
