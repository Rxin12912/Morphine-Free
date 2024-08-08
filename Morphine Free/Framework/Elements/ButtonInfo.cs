using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Morphine.Framework.MenuBase;

namespace Morphine.Framework.Elements
{
    public class ButtonInfo
    {
        public string buttonText { get; set; }
        public bool isToggle { get; set; }

        public bool NeedsMaster { get; set; }
        public bool Enabled { get; set; }
        public Action onEnable { get; set; }
        public Action onDisable { get; set; }

        public Category Page;

        public ButtonInfo(string lable, Category page, bool isToggle, bool isActive, Action OnClick, Action OnDisable = null, bool DoesNeedMaster = false)
        {
            buttonText = lable;
            this.isToggle = isToggle;
            Enabled = isActive;
            onEnable = OnClick;
            Page = page;
            this.onDisable = OnDisable;
            NeedsMaster = DoesNeedMaster;
        }
    }
}
