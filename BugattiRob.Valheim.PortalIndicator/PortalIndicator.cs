using BepInEx;
using Jotunn;
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
        public const string PluginGUID = "bugattirob.valheim.portalindicator";
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

            MinimapManager.OnVanillaMapAvailable += MinimapManager_OnVanillaMapDataLoaded;

            Patches.Patcher.Patch();
        }

        private static void MinimapManager_OnVanillaMapDataLoaded()
        {
            Log.Info("MiniMapManager.OnVanillaMapDataLoaded");
            // Ask the server to send us the portals
            var myId = ZDOMan.GetSessionID();
            var myName = Game.instance.GetPlayerProfile().GetName();
            RPC.Client.RequestSync($"{myName} ({myId}) has joined the game");
        }

        public static void GameStarted()
        {
            Log.Info("Registering RPC handlers");
            RPC.RPCManager.Register();
        }

        public static void UpdateFromPackage(ZPackage pkg)
        {
            var portalOverlay = MinimapManager.Instance.GetMapDrawing("PortalOverlay");
            Log.Info("Here, we should update the client's map with all portal pins");
            var count = pkg.ReadInt();

            Log.Info($"Received {count} portals from server");

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var portalPkg = pkg.ReadPackage();
                    var tag = portalPkg.ReadString();
                    var location = portalPkg.ReadVector3();
                    Log.Info($"Client writing {tag} to {location}");
                    // var pos = MinimapManager.Instance.WorldToOverlayCoords(location, portalOverlay.TextureSize);

                    Minimap.instance.AddPin(location, Minimap.PinType.Icon4, tag, true, false);
                    //portalOverlay.MainTex.SetPixels((int)pos.x, (int)pos.y, squareSize, squareSize, colorPixels);


                    //portalOverlay.MainTex.Apply();
                    //portalOverlay.FogFilter.Apply();
                    //portalOverlay.ForestFilter.Apply();
                    //portalOverlay.HeightFilter.Apply();
                }
            }
        }

        public static void ProcessSyncRequest()
        {
            Log.Info("Responding to sync request");
            RPC.Server.ResponseToSyncRequest(Package());
        }

        public static ZPackage Package()
        {
            var portals = ZDOMan.instance.GetPortals();

            var pkg = new ZPackage();
            pkg.Write(portals.Count);

            foreach (var portal in portals)
            {
                pkg.Write(PackagePortal(portal));
            }

            return pkg;
        }

        public static ZPackage PackagePortal(ZDO portal)
        {
            var pkg = new ZPackage();
            pkg.Write(portal.GetString("tag"));
            pkg.Write(portal.GetPosition());
            return pkg;
        }
    }
}



