using Microsoft.Win32;
using System;

namespace RemoteAppLib
{
    public class SystemRemoteApps : IDisposable
    {
        private bool _legacy32Bit;
        private string _registryPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications";
        private bool _disposedValue;
        private readonly RegistryKey _baseKey;
        private readonly RegistryKey _baseKeyWrite;

        public SystemRemoteApps()
        {
            _baseKey = Registry.LocalMachine.OpenSubKey(_registryPath);
            _baseKeyWrite = Registry.LocalMachine.OpenSubKey(_registryPath, true);
        }

        public void Init()
        {
            const string registryPathCv = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\";
            _ = Registry.LocalMachine.OpenSubKey(registryPathCv, true)
                .CreateSubKey("Terminal Server")
                .CreateSubKey("TSAppAllowList")
                .CreateSubKey("Applications");
        }

        public RemoteAppCollection GetAll()
        {
            var SystemAppCollection = new RemoteAppCollection();
            foreach (var App in _baseKey.GetSubKeyNames())
            {
                var RemoteApp = GetApp(App);
                SystemAppCollection.Add(RemoteApp);
            }

            return SystemAppCollection;
        }

        public RemoteApp GetApp(string name)
        {
            var App = new RemoteApp();
            var AppKey = _baseKey.OpenSubKey(name);
            App.Name = name;
            App.FullName = (string)AppKey.GetValue("Name", string.Empty);
            App.Path = (string)AppKey.GetValue("Path", string.Empty);
            App.VPath = (string)AppKey.GetValue("VPath", string.Empty);
            App.CommandLine = (string)AppKey.GetValue("RequiredCommandLine", string.Empty);
            App.CommandLineOption = int.Parse(AppKey.GetValue("CommandLineSetting", "1").ToString());
            App.IconPath = (string)AppKey.GetValue("IconPath", string.Empty);
            App.IconIndex = int.Parse(AppKey.GetValue("IconIndex", 0).ToString());        
            App.TSWA = AppKey.GetValue("ShowInTSWA", 0) != null && int.Parse(AppKey.GetValue("ShowInTSWA", 0).ToString()) == 1;

            var ftaKey = AppKey.OpenSubKey("Filetypes");
            if (ftaKey == null) return App;
            var ftaCol = new FileTypeAssociationCollection();
            foreach (var ftaValueName in ftaKey.GetValueNames())
            {
                var ftaValue = ftaKey.GetValue(ftaValueName).ToString().Split(',');
                var fta = new FileTypeAssociation
                {
                    Extension = ftaValueName,
                    IconPath = ftaValue[0],
                    IconIndex = ftaValue[1]
                };
                ftaCol.Add(fta);
            }

            App.FileTypeAssociations = ftaCol;

            return App;
        }

        public void SaveApp(RemoteApp remoteApp)
        {
            _baseKeyWrite.CreateSubKey(remoteApp.Name);
            var AppKey = _baseKey.OpenSubKey(remoteApp.Name, true);
            AppKey.SetValue("Name", remoteApp.FullName, RegistryValueKind.String);
            AppKey.SetValue("Path", remoteApp.Path, RegistryValueKind.String);
            AppKey.SetValue("VPath", remoteApp.VPath, RegistryValueKind.String);
            AppKey.SetValue("RequiredCommandLine", remoteApp.CommandLine, RegistryValueKind.String);
            AppKey.SetValue("CommandLineSetting", remoteApp.CommandLineOption, RegistryValueKind.DWord);
            AppKey.SetValue("IconPath", remoteApp.IconPath, RegistryValueKind.String);
            AppKey.SetValue("IconIndex", remoteApp.IconIndex, RegistryValueKind.DWord);
            AppKey.SetValue("ShowInTSWA", remoteApp.TSWA, RegistryValueKind.DWord);
            if (remoteApp.FileTypeAssociations == null) return;
            if (AppKey.OpenSubKey("Filetypes") != null)
                AppKey.DeleteSubKeyTree("Filetypes");
            AppKey.CreateSubKey("Filetypes");
            var ftaKey = AppKey.OpenSubKey("Filetypes", true);
            foreach (FileTypeAssociation fta in remoteApp.FileTypeAssociations)
                ftaKey.SetValue(fta.Extension, $"{fta.IconPath},{fta.IconIndex}", RegistryValueKind.String);
        }

        public void RenameApp(string remoteAppOldName, string remoteAppNewName)
        {
            var SystemApps = new SystemRemoteApps();
            var App = SystemApps.GetApp(remoteAppOldName);
            DeleteApp(remoteAppOldName);
            App.Name = remoteAppNewName;
            SaveApp(App);
        }

        public void DeleteApp(string name)
        {
            _baseKeyWrite.DeleteSubKeyTree(name);
        }

        public bool WoW6432Node
        {
            get
            {
                return _legacy32Bit;
            }

            set
            {
                _legacy32Bit = value;
                var PathStart = "SOFTWARE";
                if (_legacy32Bit)
                    PathStart = @"SOFTWARE\Wow6432Node";
                _registryPath = PathStart + @"\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications";
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                _baseKey.Close();
                _baseKeyWrite.Close();
            }

            _disposedValue = true;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}