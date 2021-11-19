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

        public bool SavedUseRDGateway { get; set; }

        public string SavedRDGatewayAddress { get; set; }

        public bool SavedAttemptDirectRDGateway { get; set; }

        public bool SavedMSIShortcutDesktop { get; set; }

        public bool SavedMSIShortcutStart { get; set; }

        public bool SavedMSIShortcutStartTopLevel { get; set; }

        public bool SavedUseShortcutTag { get; set; }

        public string SavedShortcutTag { get; set; }

        public bool SavedClientConnectionOptions { get; set; }

        public bool SavedCreateRAWebIcon { get; set; }

        public bool SavedMSIPerUser { get; set; }

        public bool SavedDisableFTA { get; set; }

        public bool SavedSignRDP { get; set; }

        public bool SavedSignedAndUnsigned { get; set; }

        public int SavedCertSelected { get; set; }

        public static UserSettings Instance => Lazy.Value;
        private static readonly Lazy<UserSettings> Lazy = new Lazy<UserSettings>(() => new UserSettings());

        private static NameValueCollection userSettingsCollection;
        private UserSettings()
        {
            userSettingsCollection = ConfigurationManager.AppSettings;

            MainWindowWidth = int.Parse(userSettingsCollection["MainWindowWidth"]);

            MainWindowHeight = int.Parse(userSettingsCollection["MainWindowHeight"]);

            SavedConnectionModeMSI = bool.Parse(userSettingsCollection["SavedConnectionModeMSI"]);

            SavedOpenWithNotepad = bool.Parse(userSettingsCollection["SavedOpenWithNotepad"]);

            SavedServerAddress = userSettingsCollection["SavedServerAddress"];

            SavedServerPort = int.Parse(userSettingsCollection["SavedServerPort"]);

            SavedAltServerAddress = userSettingsCollection["SavedAltServerAddress"];

            SavedUseRDGateway = bool.Parse(userSettingsCollection["SavedUseRDGateway"]);

            SavedRDGatewayAddress = userSettingsCollection["SavedRDGatewayAddress"];

            SavedAttemptDirectRDGateway = bool.Parse(userSettingsCollection["SavedAttemptDirectRDGateway"]);

            SavedMSIShortcutDesktop = bool.Parse(userSettingsCollection["SavedMSIShortcutDesktop"]);

            SavedMSIShortcutStart = bool.Parse(userSettingsCollection["SavedMSIShortcutStart"]);

            SavedMSIShortcutStartTopLevel = bool.Parse(userSettingsCollection["SavedMSIShortcutStartTopLevel"]);

            SavedUseShortcutTag = bool.Parse(userSettingsCollection["SavedUseShortcutTag"]);

            SavedShortcutTag = userSettingsCollection["SavedShortcutTag"];

            SavedClientConnectionOptions = bool.Parse(userSettingsCollection["SavedClientConnectionOptions"]);

            SavedCreateRAWebIcon = bool.Parse(userSettingsCollection["SavedCreateRAWebIcon"]);

            SavedMSIPerUser = bool.Parse(userSettingsCollection["SavedMSIPerUser"]);

            SavedDisableFTA = bool.Parse(userSettingsCollection["SavedDisableFTA"]);

            SavedSignRDP = bool.Parse(userSettingsCollection["SavedSignRDP"]);

            SavedSignedAndUnsigned = bool.Parse(userSettingsCollection["SavedSignedAndUnsigned"]);

            SavedCertSelected = int.Parse(userSettingsCollection["SavedCertSelected"]);
        }
    }
}
