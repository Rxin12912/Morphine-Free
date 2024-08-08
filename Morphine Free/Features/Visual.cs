using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Morphine.Features
{
    public class Visual
    {
        public static bool IsPlayerInfected(VRRig vrrig)
        {
            if (vrrig.mainSkin.material.name.Contains("fected"))
                return true;
            return false;
        }

        public static void EnableChams()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig)
                    continue;

                Material material = new Material(Shader.Find("GUI/Text Shader"));
                if (IsPlayerInfected(vrrig))
                    material.color = Color.red;
                else
                {
                    material.color = Color.green;
                }
                vrrig.mainSkin.material = material;
            }
        }

        public static void DisableChams()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig)
                    continue;

                vrrig.ChangeMaterialLocal(vrrig.currentMatIndex);
            }
        }
    }
}
