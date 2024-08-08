using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GorillaNetworking;
using HarmonyLib;

namespace Morphine.Patches
{
    [HarmonyPatch(typeof(GorillaServer), "UploadGorillanalytics", MethodType.Normal)]
    public class Analytics
    {
        public static bool Prefix(object uploadData)
        {
            return false;
        }
    }
}
