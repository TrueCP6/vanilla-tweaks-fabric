﻿using System;
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
        Optimisation,
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
        public CurseforgeMod Mod;
        public TweakType Type;
        public TweakCategory Category;
        public string[] Incompatibilites;

        public Tweak(string reference, string name, string description, CurseforgeMod mod, TweakType type, TweakCategory category, params string[] incompatibilites)
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
                if (Mods.All.Any(m => items[3] == m.Reference && m.FileID != 0)) //if the file id is 0 the mod does not have an updated version and thus the tweak will not be added
                {
                    output.Add(new Tweak(
                        items[0],
                        items[1],
                        items[2],
                        Mods.All.First(m => items[3] == m.Reference),
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

        public static void Toggle(string reference) =>
            Dict[reference] = !Dict[reference];

        public static string GetStyle(string reference) =>
            Dict[reference] ? "background-color: #264d00;" : "background-color: #800000;";

        public static Tweak[] GetAllEnabled()
        {
            List<Tweak> chosen = new List<Tweak>();
            foreach (Tweak t in All)
                if (Dict[t.Reference])
                    chosen.Add(t);
            return chosen.ToArray();
        }
    }
}