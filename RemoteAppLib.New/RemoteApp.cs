namespace RemoteAppLib.New
{
    public class RemoteApp
    {
        public string Name;
        public string FullName;
        public string Path;
        public string VPath;
        public string IconPath;
        public int IconIndex;
        public string CommandLine = string.Empty;
        public int CommandLineOption = 1;
        public bool TSWA;
        public FileTypeAssociationCollection FileTypeAssociations;
    }
}