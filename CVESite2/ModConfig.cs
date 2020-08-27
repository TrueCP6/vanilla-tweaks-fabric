using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;

namespace CVESite2
{
    public class ModConfig
    {
        public static string TweakerooDefault;
        public static string MiniHUDDefault;

        public static (string File, byte[] Content)[] NoConfig => new (string File, byte[] Content)[0];

        public static (string File, byte[] Content)[] IAmVerySmart(string[] tweaks)
        {
            if (!tweaks.Contains("hide-tutorials"))
            {
                return new (string File, byte[] Content)[] {
                    ("config/iamverysmart.toml", Encoding.UTF8.GetBytes($"suppressTutorialNotification = false{Environment.NewLine}suppressRecipeNotification = false"))
                };
            }
            if (!tweaks.Contains("unlock-all-recipes"))
            {
                return new (string File, byte[] Content)[]
                {
                    ("config/datapacks/IAmVerySmartDisabler/data/iamverysmart/iamverysmart/include_recipes.json", Encoding.UTF8.GetBytes("[]")),
                    ("config/datapacks/IAmVerySmartDisabler/pack.mcmeta", Encoding.UTF8.GetBytes("{\"pack\":{\"pack_format\":5,\"description\":\"" + "This datapack disables the automatic unlocking of all recipes." + "\"}}"))
                };
            }
            return NoConfig;
        }

        public static (string File, byte[] Content)[] ChatCalc(string[] tweaks)
        {
            return new (string File, byte[] Content)[] {
                ("config/chatcalc.json", Encoding.UTF8.GetBytes("{\"decimalFormat\":\"###0.##\"}"))
            };
        }

        public static (string File, byte[] Content)[] Tweakeroo(string[] tweaks)
        {
            string[] file = TweakerooDefault.Split(Environment.NewLine);

            for (int i = 0; i < file.Length; i++)
                if (tweaks.Any(t => file[i].Contains(t)))
                    file[i] = file[i].Replace("false", "true");

            dynamic config = JsonConvert.DeserializeObject(string.Join(Environment.NewLine, file));

            if (tweaks.Contains("tweakFlySpeed"))
            {
                config.GenericHotkeys.flyPreset1.keys = "KP_1";
                config.GenericHotkeys.flyPreset2.keys = "KP_2";
                config.GenericHotkeys.flyPreset3.keys = "KP_3";
                config.GenericHotkeys.flyPreset4.keys = "KP_4";
            }
            if (tweaks.Contains("tweakHotbarScroll"))
                config.GenericHotkeys.hotbarScroll.keys = "DOWN";
            if (tweaks.Contains("tweakInventoryPreview"))
                config.GenericHotkeys.inventoryPreview.keys = "I";
            if (tweaks.Contains("tweakZoom"))
                config.GenericHotkeys.zoomActivate.keys = "Z";
            if (tweaks.Contains("tweakPlayerInventoryPeek"))
                config.GenericHotkeys.playerInventoryPeek.keys = "I";

            return new (string File, byte[] Content)[] {
                ("config/tweakeroo.json", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(config, Constants.DefaultFormatting)))
            };
        }

        public static (string File, byte[] Content)[] MiniHUD(string[] tweaks)
        {
            dynamic config = JsonConvert.DeserializeObject(MiniHUDDefault);

            if (tweaks.Contains("mini-f3"))
            {
                config.InfoTypeToggles.infoBiome = true;
                config.InfoTypeToggles.infoCoordinates = true;
                config.InfoTypeToggles.infoFPS = true;
                config.InfoTypeToggles.infoLightLevel = true;
                config.InfoTypeToggles.infoMemoryUsage = true;
                config.InfoTypeToggles.infoPing = true;
                config.InfoTypeToggles.infoServerTPS = true;
                config.InfoTypeToggles.infoSlimeChunk = true;
                config.InfoTypeToggles.infoSpeed = true;
                config.InfoTypeToggles.infoTimeDayModulo = true;
                config.Generic.enabled = true;
            }
            if (tweaks.Contains("structure-overlay"))
            {
                config.RendererHotkeys.overlayStructureMainToggle.keys = "F3,J";
                config.StructureToggles = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(config.StructureToggles).Replace("false", "true"));
            }
            if (tweaks.Contains("block-grid"))
                config.RendererHotkeys.overlayBlockGrid.keys = "F3,Y";
            if (tweaks.Contains("light-level"))
                config.RendererHotkeys.overlayLightLevel.keys = "F7";

            return new (string File, byte[] Content)[] {
                ("config/minihud.json", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(config, Constants.DefaultFormatting)))
            };
        }
    }
}
