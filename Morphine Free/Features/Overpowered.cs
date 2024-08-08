using HarmonyLib;
using Morphine.Framework;
using Morphine.Framework.Helpers;
using Morphine.Modules;
using Photon.Pun;
using Photon.Voice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR;

namespace Morphine.Features
{
    public class Overpowered
    {
        public static float DelayTime;

        public static void Delay(float time, Action action)
        {
            if (Time.time >= DelayTime + time)
            {
                DelayTime = Time.time;
                action();
            }
        }

        public static void PlaySplashEffect(Vector3 pos, Quaternion rot, float scale, float rad, bool bigsplash, bool enteringWater)
        {
            GorillaTagger.Instance.myVRRig.RPC("PlaySplashEffect", Photon.Pun.RpcTarget.All, new object[]
            {
                pos,
                rot,
                scale,
                rad,
                bigsplash,
                enteringWater
            });
            GorillaNot.instance.rpcCallLimit = int.MaxValue;
            GorillaNot.instance.rpcErrorMax = int.MaxValue;
            GorillaNot.instance.logErrorMax = int.MaxValue;
            PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
        }

        public static void PlaySound(int soundIndex, bool isLeftHand, float tapVolume)
        {
            Delay(.07f, delegate
            {
                GorillaTagger.Instance.myVRRig.RPC("PlayHandTap", RpcTarget.All, new object[]
                {
                    soundIndex,
                    isLeftHand,
                    tapVolume
                });
            });
        }

        public static void GetBalloonOwnership()
        {
            foreach (BalloonHoldable balloon in UnityEngine.Object.FindObjectsOfType(typeof(BalloonHoldable)))
            {
                PhotonView photonView = balloon.photonView;
                photonView.OwnerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
                photonView.ControllerActorNr = PhotonNetwork.LocalPlayer.ActorNumber;
                Traverse.Create(balloon).Field("photonView").SetValue(GorillaTagger.Instance.myVRRig);
                photonView.RequestOwnership();
            }
        }

        public static void SplashGun()
        {
            GunTemplate.StartNormalGun(pointer =>
            {
                PlaySplashEffect(pointer.transform.position, pointer.transform.rotation, 25f, 1f, true, false);
                PlaySplashEffect(pointer.transform.position, pointer.transform.rotation, 25f, 1f, false, false);
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = pointer.transform.position - new Vector3(0f, 1f, 0f);
            }, delegate { GorillaTagger.Instance.offlineVRRig.enabled = true; }, true);
        }

        public static void SplashSelf()
        {
            if (ControllerInputPoller.instance.rightControllerSecondaryButton)
            {
                PlaySplashEffect(GorillaLocomotion.Player.Instance.bodyCollider.transform.position, Quaternion.identity, 25f, 1f, true, false);
            }
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                PlaySplashEffect(GorillaLocomotion.Player.Instance.bodyCollider.transform.position, Quaternion.identity, 25f, 1f, false, false);
            }
        }

        public static void SplashBarrage()
        {
            Vector3 pos = GorillaLocomotion.Player.Instance.bodyCollider.transform.position + new Vector3(
                UnityEngine.Random.Range(-1.25f, 1.25f),
                UnityEngine.Random.Range(-.5f, 2f),
                UnityEngine.Random.Range(-1.0f, 1.5f)
            );
            PlaySplashEffect(pos, Quaternion.identity, 25f, 1f, true, false);
            PlaySplashEffect(pos, Quaternion.identity, 25f, 1f, false, false);
        }

        public static void WaterBending()
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(UnityEngine.XR.XRNode.RightHand)))
            {
                PlaySplashEffect(GorillaLocomotion.Player.Instance.rightControllerTransform.position, GorillaLocomotion.Player.Instance.rightControllerTransform.rotation, 25f, 1f, true, false);
                PlaySplashEffect(GorillaLocomotion.Player.Instance.rightControllerTransform.position, GorillaLocomotion.Player.Instance.rightControllerTransform.rotation, 25f, 1f, false, false);
            }
            if (Controller.GetButton(ControllerInputPoller.GripFloat(UnityEngine.XR.XRNode.LeftHand)))
            {
                PlaySplashEffect(GorillaLocomotion.Player.Instance.leftControllerTransform.position, GorillaLocomotion.Player.Instance.leftControllerTransform.rotation, 25f, 1f, true, false);
                PlaySplashEffect(GorillaLocomotion.Player.Instance.leftControllerTransform.position, GorillaLocomotion.Player.Instance.leftControllerTransform.rotation, 25f, 1f, false, false);
            }
        }

        public static void WaterCum()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                PlaySplashEffect(GorillaLocomotion.Player.Instance.bodyCollider.transform.position - new Vector3(0f, .09f), GorillaLocomotion.Player.Instance.transform.rotation, 25f, 1f, false, false);
            }
        }

        public static void WaterIronMonke()
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                PlaySplashEffect(GorillaLocomotion.Player.Instance.rightControllerTransform.position, Quaternion.identity, 25f, 1f, false, false);
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(20f * GorillaLocomotion.Player.Instance.rightControllerTransform.right.x, 20f * GorillaLocomotion.Player.Instance.rightControllerTransform.right.y, 20f * GorillaLocomotion.Player.Instance.rightControllerTransform.right.z), ForceMode.Acceleration);
            }
            else if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.LeftHand)))
            {
                PlaySplashEffect(GorillaLocomotion.Player.Instance.leftControllerTransform.position, Quaternion.identity, 25f, 1f, false, false);
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(-20f * GorillaLocomotion.Player.Instance.leftControllerTransform.right.x, -20f * GorillaLocomotion.Player.Instance.leftControllerTransform.right.y, -20f * GorillaLocomotion.Player.Instance.leftControllerTransform.right.z), ForceMode.Acceleration);
            }
        }

        public static void HandTapSpam()
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                PlaySound(1, false, float.MaxValue);
            }
        }

        public static void WoodTapSpam()
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                PlaySound(10, false, float.MaxValue);
            }
        }

        public static void DuckSpam()
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                PlaySound(75, false, float.MaxValue);
            }
        }

        public static void RandomSpam1()
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                PlaySound(53, false, float.MaxValue);
            }
        }

        public static void RandomSpam2()
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                PlaySound(23, false, float.MaxValue);
            }
        }

        public static void RandomSpam3()
        {
            if (Controller.GetButton(ControllerInputPoller.GripFloat(XRNode.RightHand)))
            {
                PlaySound(67, false, float.MaxValue);
            }
        }

        public static void TagGun()
        {
            GunTemplate.StartPlayerGun(player =>
            {
                VRRig vrrig = GorillaGameManager.instance.FindPlayerVRRig(player);
                if (!Visual.IsPlayerInfected(vrrig))
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = vrrig.transform.position + new Vector3(0f, 1f, 0f);
                    GorillaLocomotion.Player.Instance.rightControllerTransform.position = vrrig.transform.position;
                    GorillaLocomotion.Player.Instance.leftControllerTransform.position = vrrig.transform.position;
                }

            }, delegate { GorillaTagger.Instance.offlineVRRig.enabled = true; }, Settings.PlayerGunLock);
        }

        public static void TagAll()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerListOthers)
            {
                VRRig vrrig = GorillaGameManager.instance.FindPlayerVRRig(player);
                if (!Visual.IsPlayerInfected(vrrig) && GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = vrrig.transform.position + new Vector3(0f, 1f, 0f);
                    GorillaLocomotion.Player.Instance.rightControllerTransform.position = vrrig.transform.position;
                    GorillaLocomotion.Player.Instance.leftControllerTransform.position = vrrig.transform.position;
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
        }

        public static void FlickTagAura()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerListOthers)
            {
                VRRig vrrig = GorillaGameManager.instance.FindPlayerVRRig(player);

                if (!Visual.IsPlayerInfected(vrrig) && GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
                {
                    if (Vector3.Distance(GorillaTagger.Instance.offlineVRRig.transform.position, vrrig.transform.position) >= GorillaGameManager.instance.tagDistanceThreshold)
                    {
                        GorillaLocomotion.Player.Instance.rightControllerTransform.position = vrrig.transform.position;
                    }
                }
            }
        }
    }
}
