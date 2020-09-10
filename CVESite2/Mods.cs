using System;

namespace CVESite2
{
    public class Mods
    {
        private static readonly Func<string[], (string File, byte[] Content)[]> DefaultConfig = t => ModConfig.NoConfig;

        public static readonly CurseforgeMod Jumploader =
            new CurseforgeMod("jumploader", 361988, 3023837, DefaultConfig);

        private static readonly CurseforgeMod FabricAPI =
            new CurseforgeMod("fabric-api", 306612, 3049174, DefaultConfig);

        private static readonly CurseforgeMod MaLiLib =
            new CurseforgeMod("malilib", 303119, 3030379, DefaultConfig);

        private static readonly CurseforgeMod ModMenu =
            new CurseforgeMod("mod-menu", 308702, 3023985, DefaultConfig, FabricAPI);

        private static readonly CurseforgeMod DataLoader =
            new CurseforgeMod("data-loader", 318894, 2989927, DefaultConfig, FabricAPI);

        public static CurseforgeMod[] All = new CurseforgeMod[] {
            ModMenu, DataLoader,
            new CurseforgeMod("mouse-wheelie", 317514, 3039537, DefaultConfig),
            new CurseforgeMod("worldedit", 225608, 3039223, DefaultConfig),
            new CurseforgeMod("not-enough-crashes", 353890, 3038498, DefaultConfig),
            new CurseforgeMod("lithium", 360438, 3046951, DefaultConfig),
            new CurseforgeMod("phosphor", 372124, 2987621, DefaultConfig),
            new CurseforgeMod("gamemodeoverhaul", 314612, 3040642, DefaultConfig),
            new CurseforgeMod("chat-up!", 367561, 2986682, DefaultConfig),
            //new CurseforgeMod("sodium", 394468, 3003093, DefaultConfig),
            new CurseforgeMod("bow-infinity-fix", 224713, 3036054, DefaultConfig),
            new CurseforgeMod("craftpresence", 297038, 3029540, DefaultConfig),
            new CurseforgeMod("pling", 365521, 3036795, DefaultConfig),
            new CurseforgeMod("colorful-campfire-smoke", 352791, 2987175, DefaultConfig, FabricAPI),
            new CurseforgeMod("appleskin", 248787, 2987255, DefaultConfig, FabricAPI),
            //new CurseforgeMod("beenfo", 357256, 3041598, DefaultConfig, FabricAPI),
            new CurseforgeMod("horse-stats-vanilla", 351393, 3047600, DefaultConfig, FabricAPI),
            new CurseforgeMod("bamboo-tweaks", 333141, 2989935, DefaultConfig, FabricAPI),
            new CurseforgeMod("lambdacontrols", 354231, 3047242, DefaultConfig, FabricAPI),
            new CurseforgeMod("offlineskins", 382582, 3033353, DefaultConfig, FabricAPI),
            new CurseforgeMod("fabric-chunk-pregenerator", 361585, 2986509, DefaultConfig, FabricAPI),
            new CurseforgeMod("sheep-consistency", 386175, 2986644, DefaultConfig, FabricAPI),
            new CurseforgeMod("command-macros", 331956, 3051860, DefaultConfig, FabricAPI, ModMenu),
            new CurseforgeMod("where-is-it", 378036, 3029852, DefaultConfig, FabricAPI),
            new CurseforgeMod("dynamic-fps", 335493, 3032511, DefaultConfig, FabricAPI),
            new CurseforgeMod("screenshot-to-clipboard", 327154, 3015566, DefaultConfig, FabricAPI),
            new CurseforgeMod("convenient-mobgriefing", 374048, 3030376, DefaultConfig, FabricAPI),
            new CurseforgeMod("toggle-toggle", 367941, 2990989, DefaultConfig, FabricAPI),
            new CurseforgeMod("dynamic-sound-filters", 392101, 3029490, DefaultConfig, FabricAPI),
            new CurseforgeMod("euclid", 335863, 2996712, DefaultConfig, FabricAPI),
            new CurseforgeMod("litematica", 308892, 3040505, DefaultConfig, MaLiLib),
            new CurseforgeMod("i-am-very-smart", 318163, 2992677, t => ModConfig.IAmVerySmart(t), FabricAPI, DataLoader),
            new CurseforgeMod("chatcalc", 374453, 2992187, t => ModConfig.ChatCalc(t), FabricAPI, ModMenu),
            new CurseforgeMod("tweakeroo", 297344, 3048530, t => ModConfig.Tweakeroo(t), MaLiLib),
            new CurseforgeMod("minihud", 244260, 3036289, t => ModConfig.MiniHUD(t), MaLiLib)
        };
    }
}