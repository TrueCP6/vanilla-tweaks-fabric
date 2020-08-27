using System;

namespace CVESite2
{
    public class CurseforgeMod
    {
        public string Reference;
        public int ProjectID;
        public int FileID;
        public CurseforgeMod[] Dependencies;
        public Func<string[], (string File, byte[] Content)[]> ApplyTweaks;

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
