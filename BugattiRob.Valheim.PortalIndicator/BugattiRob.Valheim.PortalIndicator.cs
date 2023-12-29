using BepInEx;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;

namespace BugattiRob.Valheim.PortalIndicator
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class PortalIndicator : BaseUnityPlugin
    {
        public const string PluginGUID = "robwittman/BugattiRob.Valheim.PortalIndicator";
        public const string PluginName = "BugattiRob.Valheim.PortalIndicator";
        public const string PluginVersion = "0.0.1";
        
        // Use this class to add your own localization to the game
        // https://valheim-modding.github.io/Jotunn/tutorials/localization.html
        public static CustomLocalization Localization = LocalizationManager.Instance.GetLocalization();

        private void Awake()
        {
            // Jotunn comes with its own Logger class to provide a consistent Log style for all mods using it
            Jotunn.Logger.LogInfo("BugattiRob.Valheim.PortalIndicator has landed");
            
            // To learn more about Jotunn's features, go to
            // https://valheim-modding.github.io/Jotunn/tutorials/overview.html

            MinimapManager.OnVanillaMapAvailable += CreateMapOverlay;
        }

        private void CreateMapOverlay()
        {
            Jotunn.Logger.LogInfo("BugattiRob.Valheim.PortalIndicator is running map Overlay");
        }
    }
}



