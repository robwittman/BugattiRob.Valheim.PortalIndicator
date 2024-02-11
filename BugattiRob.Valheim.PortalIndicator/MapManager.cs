using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugattiRob.Valheim.PortalIndicator
{
    internal static class MapManager
    {
        public static void RPC_SyncPortal(long sender, ZPackage pkg) 
        {
            Jotunn.Logger.LogInfo("PortalIndicator received RPC_SyncPortal event");
        }

        public static void RPC_Resync(long sender, ZPackage pkg) 
        {
            Jotunn.Logger.LogInfo("PortalIndicator received RPC_Resync event");
        }
    }
}
