using System;
using Newtonsoft.Json;

namespace CVESite2
{
    public class Constants
    {
        public const string Name = "Vanilla Tweaks Fabric";
        public const string ProfileVersion = "0.0.0";
        public const string MinecraftVersion = "1.16.1";
        public const string ForgeLoader = "forge-32.0.99";
        public const string FabricLoader = "fabric-0.9.2+build.206";
        public const string FabricInstaller = "0.6.1.51";
        public const string Author = "TrueCP5";
        public const Formatting DefaultFormatting = Formatting.Indented;

        public const string CORSAnywhere = "https://cors-anywhere.herokuapp.com/";
        public const string BaseAPIURL = CORSAnywhere + "https://addons-ecs.forgesvc.net/api/v2/addon/";
        public static string FabricInstallerURL => CORSAnywhere + $"{"https"}://maven.fabricmc.net/net/fabricmc/fabric-installer/{FabricInstaller}/fabric-installer-{FabricInstaller}.jar";

        public static readonly string[] VanillaPurist = new string[] {
            "client-optimisations",
            "server-optimisations",
            "tweakZoom",
            "appleskin",
            "inventory-management",
            "chat-up!",
            "inventory-preview",
            "tweakPotionWarning",
            "tweakPrintDeathCoordinates",
            "disableWallUnsprint",
            "tweakItemUnstackingProtection",
            "swap-almost-broken-tools",
            "not-enough-crashes",
            "tweakCustomFlatPresets",
            "craftpresence",
            "tweakCommandBlockExtraFields",
            "pling",
            "offlineskins",
            "screenshot-to-clipboard",
            "tweakTabCompleteCoordinate",
            "tweakSpectatorTeleport",
            "lamdacontrols",
            "data-loader",
            "fabric-chunk-pregenerator",
            "chatcalc",
            "client-fixes",
            "sheep-consistency",
            "horse-stats-vanilla",
            "beenfo",
            "mod-menu"
        };
    }
}
