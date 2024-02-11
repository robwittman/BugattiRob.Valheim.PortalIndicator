using System;
using System.Collections;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BugattiRob.Valheim.PortalIndicator
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [BepInDependency("yay.spikehimself.xportal")]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class PortalIndicator : BaseUnityPlugin
    {
        public const string PluginGUID = "BugattiRob.Valheim.PortalIndicator";
        public const string PluginName = "PortalIndicator";
        public const string PluginVersion = "0.0.1";
        
        // Use this class to add your own localization to the game
        // https://valheim-modding.github.io/Jotunn/tutorials/localization.html
        public static CustomLocalization Localization = LocalizationManager.Instance.GetLocalization();

        private static bool mapAvailable = false;

        private static readonly Harmony patcher = new Harmony(PluginGUID + ".harmony");

        private void Awake()
        {
            // Jotunn comes with its own Logger class to provide a consistent Log style for all mods using it
            Jotunn.Logger.LogInfo("BugattiRob.Valheim.PortalIndicator has landed");

          
            MinimapManager.OnVanillaMapDataLoaded += markMapEnabled;

            patcher.PatchAll(typeof(Patches.Game_Start));
        }

        private void OnDestroy()
        {
            mapAvailable = false;
        }

       private void markMapEnabled() 
        {
            mapAvailable = true;
        }

        // [Error  : Unity Log] ArgumentException: An item with the same key has already been added. Key: 1683704696
        Stack trace:
System.Collections.Generic.Dictionary`2[TKey, TValue].TryInsert(TKey key, TValue value, System.Collections.Generic.InsertionBehavior behavior) (at<834b2ded5dad441e8c7a4287897d63c7>:0)
System.Collections.Generic.Dictionary`2[TKey, TValue].Add(TKey key, TValue value) (at<834b2ded5dad441e8c7a4287897d63c7>:0)
ZRoutedRpc.Register[T] (System.String name, System.Action`2[T1, T2] f) (at<dbd2a6fbcde9498cadcacfb37ef883e3>:0)
BugattiRob.Valheim.PortalIndicator.RPCManager.Register() (at C:/Users/robkw/Repos/robwittman/BugattiRob.Valheim.PortalIndicator/BugattiRob.Valheim.PortalIndicator/RPCManager.cs:17)
BugattiRob.Valheim.PortalIndicator.PortalIndicator.GameStarted() (at C:/Users/robkw/Repos/robwittman/BugattiRob.Valheim.PortalIndicator/BugattiRob.Valheim.PortalIndicator/BugattiRob.Valheim.PortalIndicator.cs:54)
BugattiRob.Valheim.PortalIndicator.Patches.Game_Start.Postfix() (at C:/Users/robkw/Repos/robwittman/BugattiRob.Valheim.PortalIndicator/BugattiRob.Valheim.PortalIndicator/Patches/Game.cs:13)
(wrapper dynamic-method) Game.DMD<Game::Start>(Game)
        internal static void GameStarted()
        {
            Jotunn.Logger.LogInfo("Registering RPCs");
            RPCManager.Register();
        }


        private void CreateMapDrawing()
        {
            // var pinOverlay = MinimapManager.Instance.GetMapDrawing("PinOverlay");
            // foreach (ZDO zdo in zdoMan.m_portalObjects)
            // {
            //    string portalTag = zdo.GetString(ZDOVars.s_tag, string.Empty);
            //}
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



