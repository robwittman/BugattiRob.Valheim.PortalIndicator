using BepInEx;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;

namespace BugattiRob.Valheim.PortalIndicator
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class PortalIndicator : BaseUnityPlugin
    {
        public const string PluginGUID = "BugattiRob.Valheim.PortalIndicator";
        public const string PluginName = "PortalIndicator";
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
            // Get or create a map overlay instance by name
            var zoneOverlay = MinimapManager.Instance.GetMapOverlay("ZoneOverlay");

            // Create a Color array with space for every pixel of the map
            int mapSize = zoneOverlay.TextureSize * zoneOverlay.TextureSize;
            Color[] mainPixels = new Color[mapSize];

            // Iterate over the dimensions of the overlay and set a color for
            // every pixel in our mainPixels array wherever a zone boundary is
            Color color = Color.white;
            int zoneSize = 64;
            int index = 0;
            for (int x = 0; x < zoneOverlay.TextureSize; ++x)
            {
                for (int y = 0; y < zoneOverlay.TextureSize; ++y, ++index)
                {
                    if (x % zoneSize == 0 || y % zoneSize == 0)
                    {
                        mainPixels[index] = color;
                    }
                }
            }

            // Set the pixel array on the overlay texture
            // This is much faster than setting every pixel individually
            zoneOverlay.OverlayTex.SetPixels(mainPixels);

            // Apply the changes to the overlay
            // This also triggers the MinimapManager to display this overlay
            zoneOverlay.OverlayTex.Apply();
        }
    }
}



