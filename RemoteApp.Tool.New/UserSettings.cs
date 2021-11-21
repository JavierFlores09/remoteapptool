using System;
using System.Collections.Specialized;
using System.Configuration;

namespace RemoteApp.Tool.New
{
    internal sealed class UserSettings : ConfigurationSection
    {
        public int MainWindowWidth { get; set; }

        public int MainWindowHeight { get; set; }

        public bool SavedConnectionModeMSI { get; set; }

        public bool SavedOpenWithNotepad { get; set; }

        public string SavedServerAddress { get; set; }

        public int SavedServerPort { get; set; }

        public string SavedAltServerAddress { get; set; }

        public bool SavedUseRdGateway { get; set; }

        public string SavedRdGatewayAddress { get; set; }

        public bool SavedAttemptDirectRdGateway { get; set; }

        public bool SavedMSIShortcutDesktop { get; set; }

        public bool SavedMSIShortcutStart { get; set; }

        public bool SavedMSIShortcutStartTopLevel { get; set; }

        public bool SavedUseShortcutTag { get; set; }

        public string SavedShortcutTag { get; set; }

        public bool SavedClientConnectionOptions { get; set; }

        public bool SavedCreateRaWebIcon { get; set; }

        public bool SavedMSIPerUser { get; set; }

        public bool SavedDisableFta { get; set; }

        public bool SavedSignRDP { get; set; }

        public bool SavedSignedAndUnsigned { get; set; }

        public int SavedCertSelected { get; set; }

        public static UserSettings Instance => Lazy.Value;
        private static readonly Lazy<UserSettings> Lazy = new Lazy<UserSettings>(() => new UserSettings());

        private static NameValueCollection _userSettingsCollection;
        private UserSettings()
        {
            _userSettingsCollection = ConfigurationManager.AppSettings;

            MainWindowWidth = int.Parse(_userSettingsCollection["MainWindowWidth"]);

            MainWindowHeight = int.Parse(_userSettingsCollection["MainWindowHeight"]);

            SavedConnectionModeMSI = bool.Parse(_userSettingsCollection["SavedConnectionModeMSI"]);

            SavedOpenWithNotepad = bool.Parse(_userSettingsCollection["SavedOpenWithNotepad"]);

            SavedServerAddress = _userSettingsCollection["SavedServerAddress"];

            SavedServerPort = int.Parse(_userSettingsCollection["SavedServerPort"]);

            SavedAltServerAddress = _userSettingsCollection["SavedAltServerAddress"];

            SavedUseRdGateway = bool.Parse(_userSettingsCollection["SavedUseRDGateway"]);

            SavedRdGatewayAddress = _userSettingsCollection["SavedRDGatewayAddress"];

            SavedAttemptDirectRdGateway = bool.Parse(_userSettingsCollection["SavedAttemptDirectRDGateway"]);

            SavedMSIShortcutDesktop = bool.Parse(_userSettingsCollection["SavedMSIShortcutDesktop"]);

            SavedMSIShortcutStart = bool.Parse(_userSettingsCollection["SavedMSIShortcutStart"]);

            SavedMSIShortcutStartTopLevel = bool.Parse(_userSettingsCollection["SavedMSIShortcutStartTopLevel"]);

            SavedUseShortcutTag = bool.Parse(_userSettingsCollection["SavedUseShortcutTag"]);

            SavedShortcutTag = _userSettingsCollection["SavedShortcutTag"];

            SavedClientConnectionOptions = bool.Parse(_userSettingsCollection["SavedClientConnectionOptions"]);

            SavedCreateRaWebIcon = bool.Parse(_userSettingsCollection["SavedCreateRAWebIcon"]);

            SavedMSIPerUser = bool.Parse(_userSettingsCollection["SavedMSIPerUser"]);

            SavedDisableFta = bool.Parse(_userSettingsCollection["SavedDisableFTA"]);

            SavedSignRDP = bool.Parse(_userSettingsCollection["SavedSignRDP"]);

            SavedSignedAndUnsigned = bool.Parse(_userSettingsCollection["SavedSignedAndUnsigned"]);

            SavedCertSelected = int.Parse(_userSettingsCollection["SavedCertSelected"]);
        }
    }
}
