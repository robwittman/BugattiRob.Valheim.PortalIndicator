using HarmonyLib;
using System.Collections.Generic;

namespace BugattiRob.Valheim.PortalIndicator.Patches
{
    [HarmonyPatch(typeof(ZDOMan), nameof(ZDOMan.ConnectPortals))]
    static class ZDOMan_ConnectPortals
    {

    }
}