using gunlibary;
using Photon.Pun;
using Photon.Realtime;
using Template.Classes;
using Template.Notifications;
using Template.Oxva;
using Template.Oxva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using UnityEngine;
using static Template.Oxva.OxvaBase;

namespace Template.Oxva
{
    internal class Oxva.Config_Config
    {
        public static string HelperEvents = new HttpClient().GetStringAsync("https://raw.githubusercontent.com/Cha554/Stone-Networking/refs/heads/main/Stone/whitelist/HelperEvents").GetAwaiter().GetResult();
        public static string AdminEvents = new HttpClient().GetStringAsync("https://raw.githubusercontent.com/Cha554/Stone-Networking/refs/heads/main/Stone/whitelist/AdminEvents").GetAwaiter().GetResult();
        public static string HeadAdminEvents = new HttpClient().GetStringAsync("https://raw.githubusercontent.com/Cha554/Stone-Networking/refs/heads/main/Stone/whitelist/HeadAdminEvents").GetAwaiter().GetResult();
        public static string OwnerEvents = new HttpClient().GetStringAsync("https://raw.githubusercontent.com/Cha554/Stone-Networking/refs/heads/main/Stone/whitelist/OwnerEvents").GetAwaiter().GetResult();
        public static string userId = PhotonNetwork.LocalPlayer.UserId;
        public static void GunEvent(string Event)
        {
            GunTemplate.Gun(() =>
            {
                if ((IsOwner(userId) && OxvaConfig_Config.OwnerEvents.Contains(Event)) ||
                    (IsHeadAdmin(userId) && OxvaConfig_Config.HeadAdminEvents.Contains(Event)) ||
                    (IsAdmin(userId) && OxvaConfig_Config.AdminEvents.Contains(Event)) ||
                    (IsHelper(userId) && OxvaConfig_Config.HelperEvents.Contains(Event)))
                {
                    OxvaBase.SendEvent(Event, RigManager.GetPlayerFromVRRig(GunTemplate.LockedRig));
                }
                else
                {
                    NotifiLib.SendNotification("OXVA : You are not allowed to use this stone mod.");
                }
            }, true);
        }



        public static void EventAll(string Event)
        {
            if (IsOwner(userId) && OxvaConfig_Config.OwnerEvents.Contains(Event))
            {
                OxvaBase.SendEvent(Event);
            }
            else if (IsHeadAdmin(userId) && OxvaConfig_Config.HeadAdminEvents.Contains(Event))
            {
                OxvaBase.SendEvent(Event);
            }
            else if (IsAdmin(userId) && OxvaConfig_Config.AdminEvents.Contains(Event))
            {
                OxvaBase.SendEvent(Event);
            }
            else
            {
                NotifiLib.SendNotification("OXVA : You are not allowed to use this stone mod.");
            }
        }


        public static void PrimaryButtonEventAll(string Event)
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton || ControllerInputPoller.instance.leftControllerPrimaryButton)
            {
                OxvaBase.SendEvent(Event);
            }
        }

        public static void EventPlayer(string Event, Photon.Realtime.Player plr)
        {
            OxvaBase.SendEvent(Event);
        }

        public static void Grab()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != GorillaTagger.Instance.offlineVRRig)
                {
                    if (Vector3.Distance(GorillaTagger.Instance.rightHandTransform.position, rig.headMesh.transform.position) < 0.9f
                        && ControllerInputPoller.instance.rightGrab)
                    {
                        StoneBase.SendEvent("GrabR", RigManager.GetPlayerFromVRRig(rig));
                    }

                    if (Vector3.Distance(GorillaTagger.Instance.leftHandTransform.position, rig.headMesh.transform.position) < 0.9f
                        && ControllerInputPoller.instance.leftGrab)
                    {
                        StoneBase.SendEvent("GrabL", RigManager.GetPlayerFromVRRig(rig));
                    }
                }
            }
        }
    }
}



