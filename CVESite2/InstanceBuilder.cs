using Ionic.Zip;
using Ionic.Zlib;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;

namespace CVESite2
{
    public class InstanceBuilder
    {
        public static async Task<byte[]> BuildServerPack(HttpClient client, string modLoader, params Tweak[] tweaks)
        {
            tweaks = tweaks.Where(t => t.Type != TweakType.Client && t.Type != TweakType.SingleplayerOnly).ToArray();
            CurseforgeMod[] mods = GetModList(modLoader, tweaks);
            List<(string File, byte[] Content)> entries = GetConfigEntries(tweaks, mods).ToList();
            Console.WriteLine("Added synchronous entries");

            Task<(string File, byte[] Content)[]>[] tasks = new[] {
                GetModEntries(client, mods),
                GetServerStartEntries(client)
            };
            foreach (var task in tasks)
                entries.AddRange(await task);
            Console.WriteLine("Added parallel entries");

            return CreateZip(entries.ToArray());
        }

        public static async Task<(string File, byte[] Content)[]> GetServerStartEntries(HttpClient client)
        {
            byte[] installer = await client.GetByteArrayAsync(Constants.FabricInstallerURL);
            byte[] setup = Encoding.UTF8.GetBytes($"java -jar fabric-installer.jar server -mcversion {Constants.MinecraftVersion} -loader {Constants.FabricLoader.Replace("fabric-", "")} -downloadMinecraft");
            string start = "java -jar fabric-server-launch.jar nogui";
            return new (string File, byte[] Content)[]
            {
                ("fabric-installer.jar", installer),
                ("initial-setup.sh", setup),
                ("initial-setup.bat", setup),
                ("start-server.sh", Encoding.UTF8.GetBytes(start)),
                ("start-server.bat", Encoding.UTF8.GetBytes(start + Environment.NewLine + "pause")),
                ("eula.txt", Encoding.UTF8.GetBytes("eula=true"))
            };
        }

        private static async Task<(string File, byte[] Content)[]> GetModEntries(HttpClient client, CurseforgeMod[] mods)
        {
            Task<(string File, byte[] Content)>[] tasks = new Task<(string File, byte[] Content)>[mods.Length];
            for (int i = 0; i < mods.Length; i++)
                tasks[i] = mods[i].GetEntry(client);
            (string File, byte[] Content)[] entries = new (string File, byte[] Content)[mods.Length];
            for (int i = 0; i < mods.Length; i++)
                entries[i] = await tasks[i];
            return entries;
        }

        public static byte[] Build(string modLoader, params Tweak[] tweaks)
        {
            CurseforgeMod[] mods = GetModList(modLoader, tweaks);
            List<(string File, byte[] Content)> entries = GetConfigEntries(tweaks, mods).Select(e => ("overrides/" + e.File, e.Content)).ToList();
            entries.Add(GetManifestEntry(modLoader, mods));

            return CreateZip(entries.ToArray());
        }

        private static byte[] CreateZip((string File, byte[] Content)[] entries)
        {
            ZipFile file = new ZipFile() {
                ParallelDeflateThreshold = -1,
                CompressionLevel = CompressionLevel.BestSpeed
            };
            foreach (var entry in entries)
                file.AddEntry(entry.File, entry.Content);
            MemoryStream stream = new MemoryStream();
            file.Save(stream);
            Console.WriteLine("Created zip");
            return stream.ToArray();
        }

        private static CurseforgeMod[] GetModList(string modLoader, Tweak[] tweaks)
        {
            List<CurseforgeMod> list = new List<CurseforgeMod>();
            foreach (Tweak tweak in tweaks)
                list.AddRange(tweak.Mod);
            foreach (CurseforgeMod mod in list.ToArray())
                list.AddRange(mod.Dependencies);
            if (modLoader.StartsWith("forge"))
                list.Add(Mods.Jumploader);
            return list.Distinct().Where(m => m.ProjectID != 0).ToArray(); //if the project id is 0 the mod does not need to be downloaded via curseforge (e.g. vanilla tweaks)
        }

        private static (string File, byte[] Content) GetManifestEntry(string modLoader, params CurseforgeMod[] mods) =>
            ("manifest.json", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new ManifestObject(Constants.Name, Constants.ProfileVersion, Constants.Author, Constants.MinecraftVersion, modLoader, mods), Constants.DefaultFormatting)));

        private static (string File, byte[] Content)[] GetConfigEntries(Tweak[] tweaks, CurseforgeMod[] mods)
        {
            List<(string File, byte[] Content)> entries = new List<(string File, byte[] Content)>();
            foreach (CurseforgeMod mod in mods)
                entries.AddRange(mod.ApplyTweaks(tweaks.Where(t => t.Mod.Contains(mod)).Select(t => t.Reference).ToArray()));
            return entries.ToArray();
        }
    }
}
