using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace Morphine.Patches
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player), "Awake")]
    public class OnGameLoaded
    {
        public static void Prefix()
        {
            Debug.Log("( Morphine Free ) : GorillaLocomotion.Player : Awake");
        }
    }
}
