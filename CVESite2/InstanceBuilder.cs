using Ionic.Zip;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CVESite2
{
    public class InstanceBuilder
    {
        public static byte[] Build(string name, string version, string author, string minecraftVersion, string modLoader, params Tweak[] tweaks)
        {
            CurseforgeMod[] mods = GetModList(modLoader, tweaks);
            List<(string File, byte[] Content)> entries = GetConfigEntries(tweaks, mods).Select(e => ("overrides/" + e.File, e.Content)).ToList();
            entries.Add(GetManifestEntry(name, version, author, minecraftVersion, modLoader, mods));

            return CreateZip(entries.ToArray());
        }

        private static byte[] CreateZip((string File, byte[] Content)[] entries)
        {
            ZipFile file = new ZipFile();
            foreach (var entry in entries)
                file.AddEntry(entry.File, entry.Content);
            MemoryStream stream = new MemoryStream();
            file.Save(stream);
            return stream.ToArray();
        }

        private static CurseforgeMod[] GetModList(string modLoader, Tweak[] tweaks)
        {
            List<CurseforgeMod> list = tweaks.Select(t => t.Mod).ToList();
            foreach (CurseforgeMod mod in list.ToArray())
                list.AddRange(mod.Dependencies);
            if (modLoader.StartsWith("forge"))
                list.Add(Mods.Jumploader);
            return list.Distinct().Where(m => m.ProjectID != 0).ToArray(); //if the project id is 0 the mod does not need to be downloaded via curseforge (e.g. vanilla tweaks)
        }

        private static (string File, byte[] Content) GetManifestEntry(string name, string version, string author, string minecraftVersion, string modLoader, CurseforgeMod[] mods) =>
            ("manifest.json", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new ManifestObject(name, version, author, minecraftVersion, modLoader, mods), Constants.DefaultFormatting)));

        private static (string File, byte[] Content)[] GetConfigEntries(Tweak[] tweaks, CurseforgeMod[] mods)
        {
            List<(string File, byte[] Content)> entries = new List<(string File, byte[] Content)>();
            foreach (CurseforgeMod mod in mods)
                entries.AddRange(mod.ApplyTweaks(tweaks.Where(t => mod.Reference == t.Mod.Reference).Select(t => t.Reference).ToArray()));
            return entries.ToArray();
        }
    }
}
