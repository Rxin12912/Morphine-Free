using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;
using Morphine.Framework.Elements;
using static Morphine.Framework.MenuBase;
using System.IO;
using BepInEx;
using Morphine.Framework;
using Photon.Pun;
using Morphine.Features;

namespace Morphine.Modules
{
    public class Menu
    {
        public static List<ButtonInfo> Buttons = new List<ButtonInfo>
        {
            // Menu Base
            new ButtonInfo("Current Room", Category.Base, false, false, ()=>ChangePage(Category.Room), null),
            new ButtonInfo("LocalPlayer", Category.Base, false, false, ()=>ChangePage(Category.LocalPlayer), null),
            new ButtonInfo("Fun", Category.Base, false, false, ()=>ChangePage(Category.Fun), null),
            new ButtonInfo("Plugins", Category.Base, false, false, ()=>ChangePage(Category.Plugins), null),
            new ButtonInfo("Exploits", Category.Base, false, false, ()=>ChangePage(Category.Exploits), null),
            new ButtonInfo("Visuals", Category.Base, false, false, ()=>ChangePage(Category.Visuals), null),
            new ButtonInfo("Settings", Category.Base, false, false, ()=>ChangePage(Category.Settings), null),

            // Current Room
            new ButtonInfo("Disconnect", Category.Room, false, false, ()=>PhotonNetwork.Disconnect()),
            new ButtonInfo("Quit Application", Category.Room, false, false, ()=>Environment.Exit(0)),

            // LocalPlayer
            new ButtonInfo("Platforms", Category.LocalPlayer, true, false, ()=>LocalPlayer.Platforms()),
            new ButtonInfo("Transform Flight", Category.LocalPlayer, true, false, ()=>LocalPlayer.TransformFlight()),
            new ButtonInfo("Noclip", Category.LocalPlayer, true, false, ()=>LocalPlayer.Noclip()),
            new ButtonInfo("Car Monke(T)", Category.LocalPlayer, true, false, ()=>LocalPlayer.CarMonke()),
            new ButtonInfo("Speed Boost", Category.LocalPlayer, true, false, ()=>LocalPlayer.ToggleSpeed(2.0f, 100.0f, true)),
            new ButtonInfo("Teleport Gun", Category.LocalPlayer, true, false, ()=>LocalPlayer.TeleportGun()),
            new ButtonInfo("Up & Down", Category.LocalPlayer, true, false, ()=>LocalPlayer.UpDown()),
            new ButtonInfo("Iron Monke", Category.LocalPlayer, true, false, ()=>LocalPlayer.IronMonke()),
            new ButtonInfo("Wall Walk(G)", Category.LocalPlayer, true, false, ()=>LocalPlayer.WallWalk()),
            new ButtonInfo("Comp Boost", Category.LocalPlayer, true, false, ()=>LocalPlayer.ToggleSpeed(1.05f, 7.6f, true)),
            new ButtonInfo("Checkpoint", Category.LocalPlayer, true, false, ()=>LocalPlayer.Checkpoint()),
            new ButtonInfo("No Gravity", Category.LocalPlayer, true, false, delegate
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.useGravity = false;
            }, delegate {GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.useGravity = true; }),
            new ButtonInfo("Ghost Monke", Category.LocalPlayer, true, false, ()=>LocalPlayer.GhostMonke()),
            new ButtonInfo("Invisible Monke", Category.LocalPlayer, true, false, ()=>LocalPlayer.InvisibleMonke()),

            // Fun
            new ButtonInfo("Spinbot", Category.Fun, true, false, ()=>Fun.Spinbot(), delegate { GorillaTagger.Instance.offlineVRRig.enabled = true; }),
            new ButtonInfo("Helicopter", Category.Fun, true, false, ()=>Fun.Helicopter(), delegate { GorillaTagger.Instance.offlineVRRig.enabled = true; }),
            new ButtonInfo("Namo Troll", Category.Fun, true, false, ()=>Fun.FakeNameTroll(), delegate { GorillaTagger.Instance.offlineVRRig.enabled = true; }),
            new ButtonInfo("T-Pose", Category.Fun, true, false, ()=>Fun.TPose(), delegate { GorillaTagger.Instance.offlineVRRig.enabled = true; }),

            // Exploits
            new ButtonInfo("Water Gun", Category.Exploits, true, false, ()=>Overpowered.SplashGun()),
            new ButtonInfo("Water Self", Category.Exploits, true, false, ()=>Overpowered.SplashSelf()),
            new ButtonInfo("Water Barrage", Category.Exploits, true, false, ()=>Overpowered.SplashBarrage()),
            new ButtonInfo("Water Bender(G)", Category.Exploits, true, false, ()=>Overpowered.WaterBending()),
            new ButtonInfo("Bust(A)", Category.Exploits, true, false, ()=>Overpowered.WaterCum()),
            new ButtonInfo("Water Iron Monke", Category.Exploits, true, false, ()=>Overpowered.WaterIronMonke()),
            new ButtonInfo("Play Hand Tap(G)", Category.Exploits, true, false, ()=>Overpowered.HandTapSpam()),
            new ButtonInfo("Play Wood Tap(G)", Category.Exploits, true, false, ()=>Overpowered.WoodTapSpam()),
            new ButtonInfo("Play Duck Sound(G)", Category.Exploits, true, false, ()=>Overpowered.DuckSpam()),
            new ButtonInfo("Crystal Spam(G)", Category.Exploits, true, false, ()=>Overpowered.RandomSpam1()),
            new ButtonInfo("Glass Spam(G)", Category.Exploits, true, false, ()=>Overpowered.RandomSpam2()),
            new ButtonInfo("Button Spam(G)", Category.Exploits, true, false, ()=>Overpowered.RandomSpam3()),
            new ButtonInfo("Tag Gun", Category.Exploits, true, false, ()=>Overpowered.TagGun()),
            new ButtonInfo("Tag All", Category.Exploits, true, false, ()=>Overpowered.TagAll()),
            new ButtonInfo("Flick Tag Aura", Category.Exploits, true, false, ()=>Overpowered.FlickTagAura()),

            // Visuals
            new ButtonInfo("Chams", Category.Visuals, true, false, ()=>Visual.EnableChams(), ()=>Visual.DisableChams()),

            // Settings
            new ButtonInfo("Player Gun Lock", Category.Settings, true, false, delegate {Settings.PlayerGunLock = true; }, delegate {Settings.PlayerGunLock = false; }),
            new ButtonInfo("Show Transparent Rig", Category.Settings, true, true, delegate {Settings.ShowCham = true; }, delegate {Settings.ShowCham = false; }),
            new ButtonInfo("Always Show Transparent Rig", Category.Settings, true, false, delegate {Settings.AlwaysShowCham = true; }, delegate {Settings.AlwaysShowCham = false; }),
            new ButtonInfo("Change Menu Theme", Category.Settings, false, false, ()=>Settings.SwitchTheme()),
        };

        public static void StartMenu()
        {
            try
            {
                if (ControllerInputPoller.instance.leftControllerSecondaryButton && MenuObj == null)
                {
                    DrawMenu();
                    if (reference == null)
                    {
                        Material mat = new Material(Shader.Find("GUI/Text Shader"));
                        mat.color = MenuColors.MenuPointerColor;
                        reference = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        reference.transform.parent = GorillaLocomotion.Player.Instance.rightControllerTransform;
                        reference.GetComponent<Renderer>().material = mat;
                        reference.transform.localPosition = new Vector3(0f, -0.1f, 0f);
                        reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    }
                }
                else if (!ControllerInputPoller.instance.leftControllerSecondaryButton && MenuObj != null)
                {
                    GameObject.Destroy(MenuObj);
                    MenuObj = null;
                    GameObject.Destroy(reference);
                    reference = null;
                }
                if (ControllerInputPoller.instance.leftControllerSecondaryButton && MenuObj != null)
                {
                    MenuObj.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.transform.position;
                    MenuObj.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.transform.rotation;
                }
                if (btnCooldown > 0)
                {
                    if (Time.frameCount > btnCooldown)
                    {
                        btnCooldown = 0;
                        GameObject.Destroy(MenuObj);
                        MenuObj = null;
                        DrawMenu();
                    }
                }

                foreach (ButtonInfo btn in Buttons)
                {
                    if (btn.Enabled)
                    {
                        if (btn.onEnable != null)
                        {
                            btn.onEnable();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string PluginsDirectory = Paths.PluginPath;
                File.WriteAllText(Path.Combine(PluginsDirectory, "Morphine Free") + "\\Morphine Free.log", ex.ToString());
            }
        }
    }
}
