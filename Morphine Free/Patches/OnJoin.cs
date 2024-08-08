using HarmonyLib;
using Morphine.Framework.Helpers;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Morphine.Patches
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerEnteredRoom")]
    public class OnJoin : MonoBehaviour
    {
        public static Photon.Realtime.Player player;

        public static void Prefix(Photon.Realtime.Player newPlayer)
        {
            if (newPlayer != player)
            {
                Notifications.SendNotification($"{newPlayer.NickName}", "JOIN");
                player = newPlayer;
            }
        }
    }
}
