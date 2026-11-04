using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace RemoteApp.Tool
{
    /// <summary>
    ///     Per-user application settings.
    ///     Values are seeded from App.config and then overlaid with the user's own values from
    ///     %APPDATA%\RemoteApp Tool\settings.dat. This replaces VB's My.Settings, which the C# port
    ///     had dropped: without it every setting was lost when the application closed.
    /// </summary>
    internal sealed class UserSettings
    {
        private static readonly string SettingsDirectory =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RemoteApp Tool");

        private static readonly string SettingsFile = Path.Combine(SettingsDirectory, "settings.dat");

        private static readonly Lazy<UserSettings> Lazy = new Lazy<UserSettings>(() => new UserSettings());

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

        public string SavedRdpOptions { get; set; } = string.Empty;

        public static UserSettings Instance => Lazy.Value;

        private UserSettings()
        {
            LoadDefaults();
            LoadUserValues();
        }

        /// <summary>
        ///     Seeds every setting from App.config, which is what a first run sees.
        /// </summary>
        private void LoadDefaults()
        {
            var appSettings = ConfigurationManager.AppSettings;

            MainWindowWidth = ReadInt(appSettings, "MainWindowWidth");
            MainWindowHeight = ReadInt(appSettings, "MainWindowHeight");
            SavedConnectionModeMSI = ReadBool(appSettings, "SavedConnectionModeMSI");
            SavedOpenWithNotepad = ReadBool(appSettings, "SavedOpenWithNotepad");
            SavedServerAddress = appSettings["SavedServerAddress"] ?? string.Empty;
            SavedServerPort = ReadInt(appSettings, "SavedServerPort", 3389);
            SavedAltServerAddress = appSettings["SavedAltServerAddress"] ?? string.Empty;
            SavedUseRdGateway = ReadBool(appSettings, "SavedUseRDGateway");
            SavedRdGatewayAddress = appSettings["SavedRDGatewayAddress"] ?? string.Empty;
            SavedAttemptDirectRdGateway = ReadBool(appSettings, "SavedAttemptDirectRDGateway");
            SavedMSIShortcutDesktop = ReadBool(appSettings, "SavedMSIShortcutDesktop", true);
            SavedMSIShortcutStart = ReadBool(appSettings, "SavedMSIShortcutStart", true);
            SavedMSIShortcutStartTopLevel = ReadBool(appSettings, "SavedMSIShortcutStartTopLevel");
            SavedUseShortcutTag = ReadBool(appSettings, "SavedUseShortcutTag", true);
            SavedShortcutTag = appSettings["SavedShortcutTag"] ?? "remote";
            SavedClientConnectionOptions = ReadBool(appSettings, "SavedClientConnectionOptions");
            SavedCreateRaWebIcon = ReadBool(appSettings, "SavedCreateRAWebIcon");
            SavedMSIPerUser = ReadBool(appSettings, "SavedMSIPerUser");
            SavedDisableFta = ReadBool(appSettings, "SavedDisableFTA");
            SavedSignRDP = ReadBool(appSettings, "SavedSignRDP");
            SavedSignedAndUnsigned = ReadBool(appSettings, "SavedSignedAndUnsigned");
            SavedCertSelected = ReadInt(appSettings, "SavedCertSelected");
            SavedRdpOptions = appSettings["SavedRdpOptions"] ?? string.Empty;
        }

        /// <summary>
        ///     Applies the persisted values, if the user has run the application before.
        ///     The file is "name=base64(value)" lines so that values containing pipes, newlines
        ///     or commas survive a round trip.
        /// </summary>
        private void LoadUserValues()
        {
            if (!File.Exists(SettingsFile)) return;

            foreach (var line in File.ReadAllLines(SettingsFile))
            {
                var separator = line.IndexOf('=');
                if (separator <= 0) continue;

                var name = line.Substring(0, separator);
                string value;
                try
                {
                    value = Encoding.UTF8.GetString(Convert.FromBase64String(line.Substring(separator + 1)));
                }
                catch (FormatException)
                {
                    continue;
                }

                Apply(name, value);
            }
        }

        private void Apply(string name, string value)
        {
            switch (name)
            {
                case nameof(MainWindowWidth):
                    MainWindowWidth = ParseInt(value, MainWindowWidth);
                    break;
                case nameof(MainWindowHeight):
                    MainWindowHeight = ParseInt(value, MainWindowHeight);
                    break;
                case nameof(SavedConnectionModeMSI):
                    SavedConnectionModeMSI = ParseBool(value, SavedConnectionModeMSI);
                    break;
                case nameof(SavedOpenWithNotepad):
                    SavedOpenWithNotepad = ParseBool(value, SavedOpenWithNotepad);
                    break;
                case nameof(SavedServerAddress):
                    SavedServerAddress = value;
                    break;
                case nameof(SavedServerPort):
                    SavedServerPort = ParseInt(value, SavedServerPort);
                    break;
                case nameof(SavedAltServerAddress):
                    SavedAltServerAddress = value;
                    break;
                case nameof(SavedUseRdGateway):
                    SavedUseRdGateway = ParseBool(value, SavedUseRdGateway);
                    break;
                case nameof(SavedRdGatewayAddress):
                    SavedRdGatewayAddress = value;
                    break;
                case nameof(SavedAttemptDirectRdGateway):
                    SavedAttemptDirectRdGateway = ParseBool(value, SavedAttemptDirectRdGateway);
                    break;
                case nameof(SavedMSIShortcutDesktop):
                    SavedMSIShortcutDesktop = ParseBool(value, SavedMSIShortcutDesktop);
                    break;
                case nameof(SavedMSIShortcutStart):
                    SavedMSIShortcutStart = ParseBool(value, SavedMSIShortcutStart);
                    break;
                case nameof(SavedMSIShortcutStartTopLevel):
                    SavedMSIShortcutStartTopLevel = ParseBool(value, SavedMSIShortcutStartTopLevel);
                    break;
                case nameof(SavedUseShortcutTag):
                    SavedUseShortcutTag = ParseBool(value, SavedUseShortcutTag);
                    break;
                case nameof(SavedShortcutTag):
                    SavedShortcutTag = value;
                    break;
                case nameof(SavedClientConnectionOptions):
                    SavedClientConnectionOptions = ParseBool(value, SavedClientConnectionOptions);
                    break;
                case nameof(SavedCreateRaWebIcon):
                    SavedCreateRaWebIcon = ParseBool(value, SavedCreateRaWebIcon);
                    break;
                case nameof(SavedMSIPerUser):
                    SavedMSIPerUser = ParseBool(value, SavedMSIPerUser);
                    break;
                case nameof(SavedDisableFta):
                    SavedDisableFta = ParseBool(value, SavedDisableFta);
                    break;
                case nameof(SavedSignRDP):
                    SavedSignRDP = ParseBool(value, SavedSignRDP);
                    break;
                case nameof(SavedSignedAndUnsigned):
                    SavedSignedAndUnsigned = ParseBool(value, SavedSignedAndUnsigned);
                    break;
                case nameof(SavedCertSelected):
                    SavedCertSelected = ParseInt(value, SavedCertSelected);
                    break;
                case nameof(SavedRdpOptions):
                    SavedRdpOptions = value;
                    break;
            }
        }

        /// <summary>
        ///     Writes every setting to the per-user settings file.
        /// </summary>
        public void Save()
        {
            try
            {
                Directory.CreateDirectory(SettingsDirectory);
                var lines = new List<string>();
                foreach (var pair in Values())
                {
                    lines.Add(pair.Key + "=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(pair.Value ?? string.Empty)));
                }

                File.WriteAllLines(SettingsFile, lines);
            }
            catch (IOException)
            {
                // Losing the settings is not worth crashing the application over.
            }
            catch (UnauthorizedAccessException)
            {
                // Same as above: read-only profile or policy-restricted %APPDATA%.
            }
        }

        private IEnumerable<KeyValuePair<string, string>> Values()
        {
            yield return new KeyValuePair<string, string>(nameof(MainWindowWidth), MainWindowWidth.ToString());
            yield return new KeyValuePair<string, string>(nameof(MainWindowHeight), MainWindowHeight.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedConnectionModeMSI), SavedConnectionModeMSI.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedOpenWithNotepad), SavedOpenWithNotepad.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedServerAddress), SavedServerAddress);
            yield return new KeyValuePair<string, string>(nameof(SavedServerPort), SavedServerPort.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedAltServerAddress), SavedAltServerAddress);
            yield return new KeyValuePair<string, string>(nameof(SavedUseRdGateway), SavedUseRdGateway.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedRdGatewayAddress), SavedRdGatewayAddress);
            yield return new KeyValuePair<string, string>(nameof(SavedAttemptDirectRdGateway), SavedAttemptDirectRdGateway.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedMSIShortcutDesktop), SavedMSIShortcutDesktop.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedMSIShortcutStart), SavedMSIShortcutStart.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedMSIShortcutStartTopLevel), SavedMSIShortcutStartTopLevel.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedUseShortcutTag), SavedUseShortcutTag.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedShortcutTag), SavedShortcutTag);
            yield return new KeyValuePair<string, string>(nameof(SavedClientConnectionOptions), SavedClientConnectionOptions.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedCreateRaWebIcon), SavedCreateRaWebIcon.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedMSIPerUser), SavedMSIPerUser.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedDisableFta), SavedDisableFta.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedSignRDP), SavedSignRDP.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedSignedAndUnsigned), SavedSignedAndUnsigned.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedCertSelected), SavedCertSelected.ToString());
            yield return new KeyValuePair<string, string>(nameof(SavedRdpOptions), SavedRdpOptions);
        }

        private static int ReadInt(System.Collections.Specialized.NameValueCollection appSettings, string key, int fallback = 0)
        {
            return ParseInt(appSettings[key], fallback);
        }

        private static int ParseInt(string value, int fallback)
        {
            return int.TryParse(value, out var parsed) ? parsed : fallback;
        }

        private static bool ReadBool(System.Collections.Specialized.NameValueCollection appSettings, string key, bool fallback = false)
        {
            return ParseBool(appSettings[key], fallback);
        }

        private static bool ParseBool(string value, bool fallback)
        {
            return bool.TryParse(value, out var parsed) ? parsed : fallback;
        }
    }
}
