using Morphine.Features;
using Morphine.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace Morphine.Framework
{
    public class Settings
    {
        public static bool PlayerGunLock = false;
        public static bool ShowCham = false;
        public static bool AlwaysShowCham = false;

        // Theme
        public static int CurrentThemeIndex = 0;

        public static void SwitchTheme()
        {
            if (CurrentThemeIndex > 3)
            {
                CurrentThemeIndex = 0;
            }
            else
            {
                CurrentThemeIndex++;
            }

            if (CurrentThemeIndex == 0)
            {
                Notifications.SendNotification("Set Theme Purple");
                MenuColors.BackgroundColor = new Color32(108, 66, 245, 255);
                MenuColors.BackgroundColor2 = new Color32(0, 0, 0, 255);

                MenuColors.PageButtonColors = new Color32(32, 32, 32, 255);
                MenuColors.ButtonDisabledColor = new Color32(32, 32, 32, 255);
                MenuColors.ButtonEnabledColor = new Color32(108, 66, 245, 255);

                MenuColors.MenuPointerColor = new Color32(108, 66, 245, 255);
                MenuColors.PlatformColor = new Color32(108, 66, 245, 255);
                MenuColors.TransparentRigColor = new Color32(108, 66, 245, 100);

                GunTemplate.PointerColor = new Color32(108, 66, 245, 255);
                GunTemplate.LineColor = new Color32(108, 66, 245, 255);
            }
            if (CurrentThemeIndex == 1)
            {
                Notifications.SendNotification("Set Theme GorillaWare");
                MenuColors.BackgroundColor = new Color32(235, 52, 122, 255);
                MenuColors.BackgroundColor2 = new Color32(128, 52, 235, 255);

                MenuColors.PageButtonColors = new Color32(25, 25, 25, 255);
                MenuColors.ButtonDisabledColor = new Color32(25, 25, 25, 255);
                MenuColors.ButtonEnabledColor = new Color32(235, 52, 122, 255);

                MenuColors.MenuPointerColor = new Color32(235, 52, 122, 255);
                MenuColors.PlatformColor = new Color32(235, 52, 122, 255);
                MenuColors.TransparentRigColor = new Color32(235, 52, 122, 100);

                GunTemplate.PointerColor = new Color32(235, 52, 122, 255);
                GunTemplate.LineColor = new Color32(235, 52, 122, 255);
            }
            if (CurrentThemeIndex == 2)
            {
                Notifications.SendNotification("Set Theme Arctic");
                MenuColors.BackgroundColor = new Color32(32, 118, 199, 255);
                MenuColors.BackgroundColor2 = new Color32(0, 0, 0, 255);

                MenuColors.PageButtonColors = new Color32(25, 25, 25, 255);
                MenuColors.ButtonDisabledColor = new Color32(25, 25, 25, 255);
                MenuColors.ButtonEnabledColor = new Color32(32, 118, 199, 255);

                MenuColors.MenuPointerColor = new Color32(32, 118, 199, 255);
                MenuColors.PlatformColor = new Color32(32, 118, 199, 255);
                MenuColors.TransparentRigColor = new Color32(32, 118, 199, 255);

                GunTemplate.PointerColor = new Color32(32, 118, 199, 255);
                GunTemplate.LineColor = new Color32(32, 118, 199, 255);
            }
            if (CurrentThemeIndex == 3)
            {
                Notifications.SendNotification("Set Theme MonkeModMenu");
                MenuColors.BackgroundColor = new Color32(0, 0, 0, 255);
                MenuColors.BackgroundColor2 = new Color32(0, 0, 0, 255);

                MenuColors.PageButtonColors = Color.grey;
                MenuColors.ButtonDisabledColor = Color.red;
                MenuColors.ButtonEnabledColor = Color.green;

                MenuColors.MenuPointerColor = Color.white;
                MenuColors.PlatformColor = Color.black;
                MenuColors.TransparentRigColor = Color.white;

                GunTemplate.PointerColor = Color.white;
                GunTemplate.LineColor = Color.white;
            }
            MenuBase.RefreshMenu();
        }
    }
}
