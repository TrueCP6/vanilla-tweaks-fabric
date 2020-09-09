using System;

namespace CVESite2
{
    public class Mods
    {
        private static readonly Func<string[], (string File, byte[] Content)[]> DefaultConfig = t => ModConfig.NoConfig;

        public static readonly CurseforgeMod Jumploader =
            new CurseforgeMod("jumploader", 361988, 3023837, DefaultConfig);

        private static readonly CurseforgeMod FabricAPI =
            new CurseforgeMod("fabric-api", 306612, 3026258, DefaultConfig);

        private static readonly CurseforgeMod MaLiLib =
            new CurseforgeMod("malilib", 303119, 2993139, DefaultConfig);

        private static readonly CurseforgeMod ModMenu =
            new CurseforgeMod("mod-menu", 308702, 3006672, DefaultConfig, FabricAPI);

        private static readonly CurseforgeMod DataLoader =
            new CurseforgeMod("data-loader", 318894, 2989927, DefaultConfig, FabricAPI);

        public static CurseforgeMod[] All = new CurseforgeMod[] {
            ModMenu, DataLoader,
            new CurseforgeMod("mouse-wheelie", 317514, 2996961, DefaultConfig),
            new CurseforgeMod("worldedit", 225608, 3028436, DefaultConfig),
            new CurseforgeMod("not-enough-crashes", 353890, 3027715, DefaultConfig),
            new CurseforgeMod("lithium", 360438, 3000628, DefaultConfig),
            new CurseforgeMod("phosphor", 372124, 2987621, DefaultConfig),
            new CurseforgeMod("gamemodeoverhaul", 314612, 2996579, DefaultConfig),
            new CurseforgeMod("chat-up!", 367561, 2986682, DefaultConfig),
            new CurseforgeMod("sodium", 394468, 3003093, DefaultConfig),
            new CurseforgeMod("infinity-fix", 315804, 2763186, DefaultConfig),
            new CurseforgeMod("craftpresence", 297038, 3023906, DefaultConfig),
            new CurseforgeMod("pling", 365521, 3036795, DefaultConfig),
            new CurseforgeMod("colorful-campfire-smoke", 352791, 2987175, DefaultConfig, FabricAPI),
            new CurseforgeMod("appleskin", 248787, 2987255, DefaultConfig, FabricAPI),
            new CurseforgeMod("beenfo", 357256, 3022492, DefaultConfig, FabricAPI),
            new CurseforgeMod("horse-stats-vanilla", 351393, 3028735, DefaultConfig, FabricAPI),
            new CurseforgeMod("bamboo-tweaks", 333141, 2989935, DefaultConfig, FabricAPI),
            new CurseforgeMod("lambdacontrols", 354231, 3008555, DefaultConfig, FabricAPI),
            new CurseforgeMod("offlineskins", 382582, 3010507, DefaultConfig, FabricAPI),
            new CurseforgeMod("fabric-chunk-pregenerator", 361585, 2986509, DefaultConfig, FabricAPI),
            new CurseforgeMod("sheep-consistency", 386175, 2986644, DefaultConfig, FabricAPI),
            new CurseforgeMod("command-macros", 331956, 2987168, DefaultConfig, FabricAPI, ModMenu),
            new CurseforgeMod("where-is-it", 378036, 3009070, DefaultConfig, FabricAPI),
            new CurseforgeMod("dynamic-fps", 335493, 2960100, DefaultConfig, FabricAPI),
            new CurseforgeMod("screenshot-to-clipboard", 327154, 2952091, DefaultConfig, FabricAPI),
            new CurseforgeMod("convenient-mobgriefing", 374048, 3030376, DefaultConfig, FabricAPI),
            new CurseforgeMod("toggle-toggle", 367941, 2990989, DefaultConfig, FabricAPI),
            new CurseforgeMod("dynamic-sound-filters", 392101, 2997837, DefaultConfig, FabricAPI),
            new CurseforgeMod("euclid", 335863, 2996712, DefaultConfig, FabricAPI),
            new CurseforgeMod("litematica", 308892, 3002065, DefaultConfig, MaLiLib),
            new CurseforgeMod("i-am-very-smart", 318163, 2992677, t => ModConfig.IAmVerySmart(t), FabricAPI, DataLoader),
            new CurseforgeMod("chatcalc", 374453, 2992187, t => ModConfig.ChatCalc(t), FabricAPI, ModMenu),
            new CurseforgeMod("tweakeroo", 297344, 2993146, t => ModConfig.Tweakeroo(t), MaLiLib),
            new CurseforgeMod("minihud", 244260, 2993429, t => ModConfig.MiniHUD(t), MaLiLib)
        };
    }
}