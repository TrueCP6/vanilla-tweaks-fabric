using System;
using System.Collections.Generic;
using System.Linq;

namespace CVESite2
{
    public enum TweakType
    {
        Server,
        Client,
        ServerAndClient,
        ClientButServerEnhanced,
        SingleplayerOnly
    }

    public enum TweakCategory
    {
        OptiFineReplacement,
        SurvivalQoL,
        GeneralQoL,
        Redstone,
        Tools,
        Aesthetic,
        VanillaPlus,
        Fixes
    }

    public class Tweak
    {
        public string Reference;
        public string Name;
        public string Description;
        public CurseforgeMod[] Mod;
        public TweakType Type;
        public TweakCategory Category;
        public string[] Incompatibilites;

        public Tweak(string reference, string name, string description, CurseforgeMod[] mod, TweakType type, TweakCategory category, params string[] incompatibilites)
        {
            Reference = reference;
            Name = name;
            Description = description;
            Mod = mod;
            Incompatibilites = incompatibilites;
            Type = type;
            Category = category;
        }

        public static Tweak[] ParseFromText(string text)
        {
            List<Tweak> output = new List<Tweak>();
            foreach (string t in text.Trim('`', '~').Split('~'))
            {
                string[] items = t.Split('`').Select(s => s.Trim('\r', '\n')).ToArray();
                CurseforgeMod[] mods =
                    items[3].Split(',')
                    .Where(s => Mods.All.Any(m => m.Reference == s))
                    .Select(s => Mods.All.First(m => m.Reference == s))
                    .ToArray();
                if (mods.Length > 0)
                {
                    output.Add(new Tweak(
                        items[0],
                        items[1],
                        items[2],
                        mods,
                        (TweakType)Convert.ToInt32(items[4]),
                        (TweakCategory)Convert.ToInt32(items[5]),
                        items.Length > 6 ? items[6].Split(',') : new string[0]
                    ));
                }
                else
                    Console.WriteLine("Could not find mod with reference: " + items[3]);
            }
            return output.ToArray();
        }

        public static Tweak[] All;
        public static Dictionary<string, bool> Dict = new Dictionary<string, bool>();

        public static void Toggle(string reference)
        {
            Dict[reference] = !Dict[reference];
            foreach (string incompat in All.First(t => t.Reference == reference).Incompatibilites)
                Dict[incompat] = false;
        }

        public static string GetStyle(string reference) =>
            Dict[reference] ? "background-color: #264d00;" : "background-color: #800000;";

        public static Tweak[] GetAllEnabled() => All.Where(t => Dict[t.Reference]).ToArray();

        public static void LoadPreset(string[] tweakReferences)
        {
            foreach (string reference in tweakReferences)
                Dict[reference] = true;
        }
    }
}
