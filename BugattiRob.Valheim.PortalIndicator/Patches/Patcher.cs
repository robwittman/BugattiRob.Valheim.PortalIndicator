using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace BugattiRob.Valheim.PortalIndicator.Patches
{
    internal static class Patcher
    {
        private static readonly Harmony patcher = new Harmony("BugattiBoys.Valheim.BugattiBoys.harmony");
        public static void Patch()
        {
            patcher.PatchAll(typeof(Game_Start));
        }

        public static void Unpatch() => patcher?.UnpatchSelf();
    }
}
