using System;
using System.Configuration;

namespace RemoteApp.Tool
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

        private UserSettings()
        {
            var userSettingsCollection = ConfigurationManager.AppSettings;

            MainWindowWidth = int.Parse(userSettingsCollection["MainWindowWidth"]);

            MainWindowHeight = int.Parse(userSettingsCollection["MainWindowHeight"]);

            SavedConnectionModeMSI = bool.Parse(userSettingsCollection["SavedConnectionModeMSI"]);

            SavedOpenWithNotepad = bool.Parse(userSettingsCollection["SavedOpenWithNotepad"]);

            SavedServerAddress = userSettingsCollection["SavedServerAddress"];

            SavedServerPort = int.Parse(userSettingsCollection["SavedServerPort"]);

            SavedAltServerAddress = userSettingsCollection["SavedAltServerAddress"];

            SavedUseRdGateway = bool.Parse(userSettingsCollection["SavedUseRDGateway"]);

            SavedRdGatewayAddress = userSettingsCollection["SavedRDGatewayAddress"];

            SavedAttemptDirectRdGateway = bool.Parse(userSettingsCollection["SavedAttemptDirectRDGateway"]);

            SavedMSIShortcutDesktop = bool.Parse(userSettingsCollection["SavedMSIShortcutDesktop"]);

            SavedMSIShortcutStart = bool.Parse(userSettingsCollection["SavedMSIShortcutStart"]);

            SavedMSIShortcutStartTopLevel = bool.Parse(userSettingsCollection["SavedMSIShortcutStartTopLevel"]);

            SavedUseShortcutTag = bool.Parse(userSettingsCollection["SavedUseShortcutTag"]);

            SavedShortcutTag = userSettingsCollection["SavedShortcutTag"];

            SavedClientConnectionOptions = bool.Parse(userSettingsCollection["SavedClientConnectionOptions"]);

            SavedCreateRaWebIcon = bool.Parse(userSettingsCollection["SavedCreateRAWebIcon"]);

            SavedMSIPerUser = bool.Parse(userSettingsCollection["SavedMSIPerUser"]);

            SavedDisableFta = bool.Parse(userSettingsCollection["SavedDisableFTA"]);

            SavedSignRDP = bool.Parse(userSettingsCollection["SavedSignRDP"]);

            SavedSignedAndUnsigned = bool.Parse(userSettingsCollection["SavedSignedAndUnsigned"]);

            SavedCertSelected = int.Parse(userSettingsCollection["SavedCertSelected"]);
        }
    }
}
