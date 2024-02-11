using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BugattiRob.Valheim.PortalIndicator
{
    internal static class RPCManager
    {
        internal const string RPC_SYNCPORTAL = "PortalIndicator_SyncPortal";
        internal const string RPC_RESYNC = "PortalIndicator_Resync";

        internal const string RPC_SYNCREQUEST = "XPortal_SyncRequest";

        public static void Register()
        {
            // Attach the XPortal RPC events to our event handler
            ZRoutedRpc.instance.Register(RPC_SYNCPORTAL, new Action<long, ZPackage>(MapManager.RPC_SyncPortal));
            ZRoutedRpc.instance.Register(RPC_RESYNC, new Action<long, ZPackage>(MapManager.RPC_Resync));
        }

        public static void SyncRequest()
        {
            Jotunn.Logger.LogInfo("Asking XPortal to resync portals");
            ZRoutedRpc.instance.InvokeRoutedRPC(ZRoutedRpc.instance.GetServerPeerID(), RPCManager.RPC_SYNCREQUEST, "please");
        }

    }
}
