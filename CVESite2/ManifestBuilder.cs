using System.Linq;

namespace CVESite2
{
    public class ManifestObject
    {
        public string manifestType = "minecraftModpack";
        public string overrides = "overrides";
        public int manifestVersion = 1;

        public string name;
        public string version;
        public string author;

        public ModManifestObject[] files;
        public MinecraftObject minecraft;

        public ManifestObject(string name, string version, string author, string minecraftVersion, string modLoader, CurseforgeMod[] mods)
        {
            this.name = name;
            this.version = version;
            this.author = author;
            minecraft = new MinecraftObject(modLoader, minecraftVersion);
            files = mods.Select(m => m.GetManifestObject()).ToArray();
        }
    }

    public class MinecraftObject
    {
        public string version;
        public ModLoaderObject[] modLoaders;

        public MinecraftObject(string modLoader, string minecraftVersion)
        {
            modLoaders = new ModLoaderObject[] { new ModLoaderObject(modLoader) };
            version = minecraftVersion;
        }
    }

    public class ModLoaderObject
    {
        public string id;
        public string yarn;
        public bool primary = true;

        public ModLoaderObject(string modLoader)
        {
            if (modLoader.StartsWith("fabric-"))
            {
                id = "fabric";
                yarn = modLoader.Substring("fabric-".Length);
            }
            else
            {
                id = modLoader;
            }
        }
    }
}
