using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace CVESite2
{
    public class CurseforgeMod
    {
        public string Reference;
        public int ProjectID;
        public int FileID;
        public CurseforgeMod[] Dependencies;
        public Func<string[], (string File, byte[] Content)[]> ApplyTweaks;
        public string ThumbnailURL = "";

        public CurseforgeMod(string reference, int projectID, int fileID, Func<string[], (string File, byte[] Content)[]> applyTweaks, params CurseforgeMod[] dependencies)
        {
            Reference = reference;
            ProjectID = projectID;
            FileID = fileID;
            ApplyTweaks = applyTweaks;
            Dependencies = dependencies;
        }

        public ModManifestObject GetManifestObject() =>
            new ModManifestObject(ProjectID, FileID);

        public static bool operator ==(CurseforgeMod left, CurseforgeMod right) => left.Reference == right.Reference;
        public static bool operator !=(CurseforgeMod left, CurseforgeMod right) => left.Reference != right.Reference;

        public async Task<(string Name, byte[] Content)> GetEntry(HttpClient client)
        {
            dynamic resource = JsonConvert.DeserializeObject(await client.GetStringAsync(Constants.BaseAPIURL + $"{ProjectID}/file/{FileID}"));
            return ("mods/" + (string)resource.fileName, await client.GetByteArrayAsync(Constants.CORSAnywhere + (string)resource.downloadUrl));
        }

        private async Task<string> GetThumbnailURL(HttpClient client)
        {
            dynamic resource = JsonConvert.DeserializeObject(await client.GetStringAsync(Constants.BaseAPIURL + ProjectID));
            //foreach (dynamic attachment in resource.attachments)
            //    if (!attachment.isDefault)
            //        return attachment.thumbnailUrl;
            return resource.attachments[0].thumbnailUrl;
        }

        public static async Task SetModThumbnails(HttpClient client)
        {
            Task<string>[] tasks = new Task<string>[Mods.All.Length];
            for (int i = 0; i < Mods.All.Length; i++)
                tasks[i] = Mods.All[i].GetThumbnailURL(client);
            for (int i = 0; i < Mods.All.Length; i++)
                Mods.All[i].ThumbnailURL = await tasks[i];
            Console.WriteLine("Set mod thumbnails");
        }
    }

    public class ModManifestObject
    {
        public int projectID;
        public int fileID;
        public bool required = true;

        public ModManifestObject(int projectID, int fileID)
        {
            this.projectID = projectID;
            this.fileID = fileID;
        }
    }
}
