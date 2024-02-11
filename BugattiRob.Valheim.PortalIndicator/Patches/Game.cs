using HarmonyLib;

namespace BugattiRob.Valheim.PortalIndicator.Patches
{
    [HarmonyPatch(typeof(Game), nameof(Game.Start))]
    static class Game_Start
    {
        /// <summary>
        /// The game has started!
        /// </summary>
        static void Postfix()
        {
            PortalIndicator.GameStarted();
        }
    }
}