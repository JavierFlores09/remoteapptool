using Microsoft.Win32;
using System;

namespace RemoteAppLib
{
    public class SystemRemoteApps : IDisposable
    {
        private bool _legacy32Bit;
        private string _registryPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications";
        private bool _disposedValue;
        private RegistryKey _baseKey;
        private RegistryKey _baseKeyWrite;

        public SystemRemoteApps()
        {
        }

        /// <summary>
        ///     Read handle, opened on demand. The key only exists after <see cref="Init"/> has run on a
        ///     machine that never had a RemoteApp, so it must not be cached at construction time.
        /// </summary>
        private RegistryKey BaseKey
        {
            get
            {
                if (_baseKey == null) _baseKey = Registry.LocalMachine.OpenSubKey(_registryPath);
                return _baseKey;
            }
        }

        /// <summary>
        ///     Writable handle, opened on demand. Null when the process is not elevated.
        /// </summary>
        private RegistryKey BaseKeyWrite
        {
            get
            {
                if (_baseKeyWrite == null) _baseKeyWrite = Registry.LocalMachine.OpenSubKey(_registryPath, true);
                return _baseKeyWrite;
            }
        }

        public void Init()
        {
            const string registryPathCv = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\";
            _ = Registry.LocalMachine.OpenSubKey(registryPathCv, true)
                .CreateSubKey("Terminal Server")
                .CreateSubKey("TSAppAllowList")
                .CreateSubKey("Applications");

            // The handles cached before the key existed are useless now; reopen them.
            CloseKeys();
        }

        public RemoteAppCollection GetAll()
        {
            var SystemAppCollection = new RemoteAppCollection();
            var baseKey = BaseKey;
            if (baseKey == null) return SystemAppCollection;

            foreach (var App in baseKey.GetSubKeyNames())
            {
                var RemoteApp = GetApp(App);
                if (RemoteApp != null) SystemAppCollection.Add(RemoteApp);
            }

            return SystemAppCollection;
        }

        public RemoteApp GetApp(string name)
        {
            var App = new RemoteApp();
            var baseKey = BaseKey;
            if (baseKey == null) return null;

            var AppKey = baseKey.OpenSubKey(name);
            if (AppKey == null) return null;
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
            var baseKeyWrite = BaseKeyWrite;
            if (baseKeyWrite == null)
            {
                throw new InvalidOperationException(
                    "Cannot write RemoteApps: the TSAppAllowList registry key is not writable. Run RemoteApp Tool as administrator.");
            }

            baseKeyWrite.CreateSubKey(remoteApp.Name);
            var AppKey = BaseKey.OpenSubKey(remoteApp.Name, true);
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

        /// <summary>
        ///     Creates a copy of an existing RemoteApp under the next free "&lt;name&gt; copy" alias.
        /// </summary>
        public void DuplicateApp(string name)
        {
            var NewApp = GetApp(name);
            if (NewApp == null) return;

            var NewName = NewApp.Name;
            while (GetApp(NewName) != null) NewName += " copy";

            NewApp.Name = NewName;
            SaveApp(NewApp);
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
            BaseKeyWrite?.DeleteSubKeyTree(name);
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
                CloseKeys();
            }
        }

        private void CloseKeys()
        {
            _baseKey?.Close();
            _baseKeyWrite?.Close();
            _baseKey = null;
            _baseKeyWrite = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                CloseKeys();
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