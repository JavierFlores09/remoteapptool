using Microsoft.Win32;
using System;

namespace RemoteAppLib.New
{
    public class SystemRemoteApps : IDisposable
    {
        private bool Legacy32bit;
        private string RegistryPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications";
        private bool disposedValue;
        private readonly RegistryKey BaseKey;
        private readonly RegistryKey BaseKeyWrite;

        public SystemRemoteApps()
        {
            BaseKey = Registry.LocalMachine.OpenSubKey(RegistryPath);
            BaseKeyWrite = Registry.LocalMachine.OpenSubKey(RegistryPath, true);
        }

        public void Init()
        {
            const string RegistryPathCV = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\";
            _ = Registry.LocalMachine.OpenSubKey(RegistryPathCV, true)
                .CreateSubKey("Terminal Server")
                .CreateSubKey("TSAppAllowList")
                .CreateSubKey("Applications");
        }

        public RemoteAppCollection GetAll()
        {
            var SystemAppCollection = new RemoteAppCollection();
            foreach (string App in BaseKey.GetSubKeyNames())
            {
                var RemoteApp = GetApp(App);
                SystemAppCollection.Add(RemoteApp);
            }

            return SystemAppCollection;
        }

        public RemoteApp GetApp(string Name)
        {
            var App = new RemoteApp();
            var AppKey = BaseKey.OpenSubKey(Name);
            App.Name = Name;
            App.FullName = (string)AppKey.GetValue("Name", string.Empty);
            App.Path = (string)AppKey.GetValue("Path", string.Empty);
            App.VPath = (string)AppKey.GetValue("VPath", string.Empty);
            App.CommandLine = (string)AppKey.GetValue("RequiredCommandLine", string.Empty);
            App.CommandLineOption = int.Parse(AppKey.GetValue("CommandLineSetting", "1").ToString());
            App.IconPath = (string)AppKey.GetValue("IconPath", string.Empty);
            App.IconIndex = int.Parse(AppKey.GetValue("IconIndex", 0).ToString());
            App.TSWA = (bool)AppKey.GetValue("ShowInTSWA", 0);
            var FTAKey = AppKey.OpenSubKey("Filetypes");
            if (FTAKey != null)
            {
                var FTACol = new FileTypeAssociationCollection();
                foreach (var FTAValueName in FTAKey.GetValueNames())
                {
                    var FTAValue = FTAKey.GetValue(FTAValueName).ToString().Split(',');
                    var FTA = new FileTypeAssociation
                    {
                        Extension = FTAValueName,
                        IconPath = FTAValue[0],
                        IconIndex = FTAValue[1]
                    };
                    FTACol.Add(FTA);
                }

                App.FileTypeAssociations = FTACol;
            }

            return App;
        }

        public void SaveApp(RemoteApp RemoteApp)
        {
            BaseKeyWrite.CreateSubKey(RemoteApp.Name);
            var AppKey = BaseKey.OpenSubKey(RemoteApp.Name, true);
            AppKey.SetValue("Name", RemoteApp.FullName, RegistryValueKind.String);
            AppKey.SetValue("Path", RemoteApp.Path, RegistryValueKind.String);
            AppKey.SetValue("VPath", RemoteApp.VPath, RegistryValueKind.String);
            AppKey.SetValue("RequiredCommandLine", RemoteApp.CommandLine, RegistryValueKind.String);
            AppKey.SetValue("CommandLineSetting", RemoteApp.CommandLineOption, RegistryValueKind.DWord);
            AppKey.SetValue("IconPath", RemoteApp.IconPath, RegistryValueKind.String);
            AppKey.SetValue("IconIndex", RemoteApp.IconIndex, RegistryValueKind.DWord);
            AppKey.SetValue("ShowInTSWA", RemoteApp.TSWA, RegistryValueKind.DWord);
            if (RemoteApp.FileTypeAssociations != null)
            {
                if (AppKey.OpenSubKey("Filetypes") != null)
                    AppKey.DeleteSubKeyTree("Filetypes");
                AppKey.CreateSubKey("Filetypes");
                var FTAKey = AppKey.OpenSubKey("Filetypes", true);
                foreach (FileTypeAssociation fta in RemoteApp.FileTypeAssociations)
                    FTAKey.SetValue(fta.Extension, $"{fta.IconPath},{fta.IconIndex}", RegistryValueKind.String);
            }
        }

        public void RenameApp(string RemoteAppOldName, string RemoteAppNewName)
        {
            var SystemApps = new SystemRemoteApps();
            var App = SystemApps.GetApp(RemoteAppOldName);
            DeleteApp(RemoteAppOldName);
            App.Name = RemoteAppNewName;
            SaveApp(App);
        }

        public void DeleteApp(string Name)
        {
            BaseKeyWrite.DeleteSubKeyTree(Name);
        }

        public bool WoW6432Node
        {
            get
            {
                return Legacy32bit;
            }

            set
            {
                Legacy32bit = value;
                string PathStart = "SOFTWARE";
                if (Legacy32bit)
                    PathStart = @"SOFTWARE\Wow6432Node";
                RegistryPath = PathStart + @"\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications";
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    BaseKey.Close();
                    BaseKeyWrite.Close();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}