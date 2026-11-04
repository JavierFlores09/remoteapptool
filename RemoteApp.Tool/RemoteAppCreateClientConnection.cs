using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace RemoteApp.Tool
{
    public partial class RemoteAppCreateClientConnection
    {
        public RemoteAppCreateClientConnection()
        {
            InitializeComponent();
            _EditAfterSave.Name = "EditAfterSave";
            _CreateButton.Name = "CreateButton";
            _ServerPort.Name = "ServerPort";
            _AltServerAddress.Name = "AltServerAddress";
            _ServerAddress.Name = "ServerAddress";
            _UseRDGatewayCheckBox.Name = "UseRDGatewayCheckBox";
            _RDPRadioButton.Name = "RDPRadioButton";
            _FTAButton.Name = "FTAButton";
            _ShortcutStartCheckBox.Name = "ShortcutStartCheckBox";
            _ShortcutTagCheckBox.Name = "ShortcutTagCheckBox";
            _ResetButton.Name = "ResetButton";
            _SaveButton.Name = "SaveButton";
            _DisabledFTACheckBox.Name = "DisabledFTACheckBox";
            _CheckBoxCreateSignedAndUnsigned.Name = "CheckBoxCreateSignedAndUnsigned";
            _CheckBoxSignRDPEnabled.Name = "CheckBoxSignRDPEnabled";
        }

        private RemoteAppLib.RemoteApp _remoteApp = new RemoteAppLib.RemoteApp();

        public void CreateClientConnection(RemoteAppLib.RemoteApp selectedRemoteApp)
        {
            _remoteApp = selectedRemoteApp;
            var rdpSign = new RDPSign.RDPSign();
            var RemoteAppShortName = _remoteApp.Name;
            Text = "Create Client Connection for " + RemoteAppShortName;
            RdpsignErrorLabel.Text = "";
            CertificateComboBox.Items.AddRange(rdpSign.GetCertificateFriendlyName().ToArray());
            SetCcWindowSettings();
            if (string.IsNullOrEmpty(ServerAddress.Text))
                ServerAddress.Text = System.Net.Dns.GetHostName();
            if (string.IsNullOrEmpty(AltServerAddress.Text))
                AltServerAddress.Text = ServerAddress.Text;
            if (ServerPort.Text == "0")
                ServerPort.Text = "3389";
            if (!File.Exists(_remoteApp.IconPath))
            {
                CreateRAWebIcon.Checked = false;
                CreateRAWebIcon.Enabled = false;
            }
            else
            {
                CreateRAWebIcon.Enabled = RDPRadioButton.Checked;
            }

            var wx = new RDP2MSIlib.RDP();
            if (!wx.WixInstalled())
            {
                RDPRadioButton.Checked = true;
                MSIRadioButton.Enabled = false;
                MSIRadioButton.Text = "MSI installer (requires WiX Toolset)";
            }

            if (!File.Exists(rdpSign.GetRdpsignExeLocation()))
            {
                SigningTabPage.Enabled = false;
                RdpsignErrorLabel.Text += " * Requires rdpsign.exe.";
                SigningTabPage.Tag = "noexe";
                CheckBoxSignRDPEnabled.Checked = false;
                CheckBoxCreateSignedAndUnsigned.Checked = false;
                CertificateComboBox.Text = "";
            }

            if (_remoteApp.FileTypeAssociations != null)
                FTACountLabel.Text = "Count: " + _remoteApp.FileTypeAssociations.Count;
            RDPRadioButton.Focus();
            HelpSystem.SetupTips(this);
            ShowDialog();
            Forms.RemoteAppMainWindow.ReloadApps();
            //Dispose();
        }

        public void ResetCcWindowSettings()
        {
            UserSettings.Instance.SavedConnectionModeMSI = false;
            UserSettings.Instance.SavedOpenWithNotepad = false;
            UserSettings.Instance.SavedServerAddress = System.Net.Dns.GetHostName();
            UserSettings.Instance.SavedServerPort = 3389;
            UserSettings.Instance.SavedAltServerAddress = UserSettings.Instance.SavedServerAddress;
            UserSettings.Instance.SavedUseRdGateway = false;
            UserSettings.Instance.SavedRdGatewayAddress = "";
            UserSettings.Instance.SavedAttemptDirectRdGateway = false;
            UserSettings.Instance.SavedMSIShortcutDesktop = true;
            UserSettings.Instance.SavedMSIShortcutStart = true;
            UserSettings.Instance.SavedMSIShortcutStartTopLevel = false;
            UserSettings.Instance.SavedUseShortcutTag = true;
            UserSettings.Instance.SavedShortcutTag = "remote";
            UserSettings.Instance.SavedClientConnectionOptions = false;
            UserSettings.Instance.SavedCreateRaWebIcon = false;
            UserSettings.Instance.SavedMSIPerUser = false;
            UserSettings.Instance.SavedDisableFta = false;
            UserSettings.Instance.SavedSignRDP = false;
            UserSettings.Instance.SavedSignedAndUnsigned = false;
            UserSettings.Instance.SavedCertSelected = 0;
            UserSettings.Instance.SavedRdpOptions = FlattenOptions(RDPOptionsWindow.RecommendedDefaults);
            _additionalOptions = RDPOptionsWindow.RecommendedDefaults;
        }

        public void SaveCcWindowSettings()
        {
            UserSettings.Instance.SavedConnectionModeMSI = MSIRadioButton.Checked;
            UserSettings.Instance.SavedOpenWithNotepad = EditAfterSave.Checked;
            UserSettings.Instance.SavedServerAddress = ServerAddress.Text;
            UserSettings.Instance.SavedServerPort = (int)Math.Round(double.Parse(ServerPort.Text));
            UserSettings.Instance.SavedAltServerAddress = AltServerAddress.Text;
            UserSettings.Instance.SavedUseRdGateway = UseRDGatewayCheckBox.Checked;
            UserSettings.Instance.SavedRdGatewayAddress = GatewayAddress.Text;
            UserSettings.Instance.SavedAttemptDirectRdGateway = AttemptDirectCheckBox.Checked;
            UserSettings.Instance.SavedMSIShortcutDesktop = ShortcutDesktopCheckBox.Checked;
            UserSettings.Instance.SavedMSIShortcutStart = ShortcutStartCheckBox.Checked;
            UserSettings.Instance.SavedMSIShortcutStartTopLevel = TopLevelRadioButton.Checked;
            UserSettings.Instance.SavedUseShortcutTag = ShortcutTagCheckBox.Checked;
            UserSettings.Instance.SavedShortcutTag = ShortcutTagTextBox.Text;
            UserSettings.Instance.SavedCreateRaWebIcon = CreateRAWebIcon.Checked;
            UserSettings.Instance.SavedMSIPerUser = PerUserRadioButton.Checked;
            UserSettings.Instance.SavedDisableFta = DisabledFTACheckBox.Checked;
            UserSettings.Instance.SavedSignRDP = CheckBoxSignRDPEnabled.Checked;
            UserSettings.Instance.SavedSignedAndUnsigned = CheckBoxCreateSignedAndUnsigned.Checked;
            UserSettings.Instance.SavedCertSelected = CertificateComboBox.SelectedIndex;
            UserSettings.Instance.SavedRdpOptions = FlattenOptions(_additionalOptions);
            UserSettings.Instance.Save();
        }

        public void SetCcWindowSettings()
        {
            if (UserSettings.Instance.SavedConnectionModeMSI)
            {
                RDPRadioButton.Checked = false;
                MSIRadioButton.Checked = true;
            }
            else
            {
                RDPRadioButton.Checked = true;
                MSIRadioButton.Checked = false;
            }

            EditAfterSave.Checked = UserSettings.Instance.SavedOpenWithNotepad;
            ServerAddress.Text = UserSettings.Instance.SavedServerAddress;
            ServerPort.Text = UserSettings.Instance.SavedServerPort.ToString();
            AltServerAddress.Text = UserSettings.Instance.SavedAltServerAddress;
            UseRDGatewayCheckBox.Checked = UserSettings.Instance.SavedUseRdGateway;
            GatewayAddress.Text = UserSettings.Instance.SavedRdGatewayAddress;
            AttemptDirectCheckBox.Checked = UserSettings.Instance.SavedAttemptDirectRdGateway;
            ShortcutDesktopCheckBox.Checked = UserSettings.Instance.SavedMSIShortcutDesktop;
            ShortcutStartCheckBox.Checked = UserSettings.Instance.SavedMSIShortcutStart;
            if (UserSettings.Instance.SavedMSIShortcutStartTopLevel)
            {
                SubfolderRadioButton.Checked = false;
                TopLevelRadioButton.Checked = true;
            }
            else
            {
                SubfolderRadioButton.Checked = true;
                TopLevelRadioButton.Checked = false;
            }

            ShortcutTagCheckBox.Checked = UserSettings.Instance.SavedUseShortcutTag;
            ShortcutTagTextBox.Text = UserSettings.Instance.SavedShortcutTag;
            MSIOptionsTabPage.Enabled = MSIRadioButton.Checked;
            CreateRAWebIcon.Checked = UserSettings.Instance.SavedCreateRaWebIcon;
            DisabledFTACheckBox.Checked = UserSettings.Instance.SavedDisableFta;
            if (!UserSettings.Instance.SavedMSIPerUser)
            {
                PerMachineRadioButton.Checked = true;
                PerUserRadioButton.Checked = false;
            }
            else
            {
                PerMachineRadioButton.Checked = false;
                PerUserRadioButton.Checked = true;
            }

            CheckBoxSignRDPEnabled.Checked = UserSettings.Instance.SavedSignRDP;
            CertificateComboBox.Enabled = UserSettings.Instance.SavedSignRDP;
            CheckBoxCreateSignedAndUnsigned.Checked = UserSettings.Instance.SavedSignedAndUnsigned;
            if (CertificateComboBox.Items.Count >= UserSettings.Instance.SavedCertSelected + 1)
            {
                CertificateComboBox.SelectedIndex = UserSettings.Instance.SavedCertSelected;
            }
            else if (CertificateComboBox.Items.Count > 0)
            {
                CertificateComboBox.SelectedIndex = 0;
            }
            else if (!string.Equals((string)SigningTabPage.Tag, "noexe"))
            {
                RdpsignErrorLabel.Text += " No certificates found.";
                SigningTabPage.Enabled = false;
                CheckBoxSignRDPEnabled.Checked = false;
                CheckBoxCreateSignedAndUnsigned.Checked = false;
                CertificateComboBox.Text = string.Empty;
            }

            // Restore the additional .rdp options that were saved with the other window settings.
            // On a first run there are none, so fall back to the recommended defaults, which is what
            // the hard coded options in CreateRdpFile used to be.
            _additionalOptions = string.IsNullOrEmpty(UserSettings.Instance.SavedRdpOptions)
                ? RDPOptionsWindow.RecommendedDefaults
                : UnflattenOptions(UserSettings.Instance.SavedRdpOptions);
        }

        private void UseRDGatewayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (UseRDGatewayCheckBox.Checked)
            {
                GatewayAddress.Enabled = true;
                RDGWLabel.Enabled = true;
                AttemptDirectCheckBox.Enabled = true;
            }
            else
            {
                GatewayAddress.Enabled = false;
                RDGWLabel.Enabled = false;
                AttemptDirectCheckBox.Enabled = false;
            }
        }

        private void RDPRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            MSIOptionsTabPage.Enabled = MSIRadioButton.Checked;
            EditAfterSave.Enabled = RDPRadioButton.Checked;
            CreateRAWebIcon.Enabled = RDPRadioButton.Checked;
            if (RDPRadioButton.Checked)
            {
                CreateButton.ImageIndex = 6;
                CheckBoxCreateSignedAndUnsigned.Enabled = true;
            }
            else
            {
                CreateButton.ImageIndex = 1;
                CheckBoxCreateSignedAndUnsigned.Enabled = false;
                CheckBoxCreateSignedAndUnsigned.Checked = false;
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            var MSIPath = string.Empty;
            if (CheckBoxSignRDPEnabled.Checked && string.IsNullOrEmpty(CertificateComboBox.SelectedItem as string))
            {
                MessageBox.Show("You must select a certificate to sign the RDP file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DisabledFTACheckBox.Checked && _remoteApp.FileTypeAssociations != null)
                _remoteApp.FileTypeAssociations.Clear();
            string RDPPath;
            if (RDPRadioButton.Checked)
            {
                FileSaveRDP.FileName = _remoteApp.Name;
                if (FileSaveRDP.ShowDialog() != DialogResult.OK)
                    return;
                RDPPath = FileSaveRDP.FileName;
            }
            else
            {
                FileSaveMSI.FileName = _remoteApp.Name;
                if (FileSaveMSI.ShowDialog() != DialogResult.OK)
                    return;
                MSIPath = FileSaveMSI.FileName;
                RDPPath = Environment.GetEnvironmentVariable("TEMP") + @"\" + _remoteApp.Name + ".rdp";
                _ = Environment.GetEnvironmentVariable("TEMP") + @"\" + _remoteApp.Name + ".msi";
            }

            if (UseRDGatewayCheckBox.Checked)
            {
                _ = GatewayAddress.Text;
                _ = AttemptDirectCheckBox.Checked;
            }

            if (RDPRadioButton.Checked)
            {
                CreateRDPFile(RDPPath, _remoteApp);
                // !!!!!!! If it's an RDP file
                if (EditAfterSave.Checked)
                {
                    var CmdLine = RemoteAppFunction.GetSysDir() + @"\notepad.exe";
                    Process.Start(CmdLine, FileSaveRDP.FileName);
                }

                if (CreateRAWebIcon.Checked)
                {
                    var IconFilePath = RDPPath.Substring(0, RDPPath.Length - 4) + ".ico";
                    if (!IconModule.ExtractToIco(_remoteApp.IconPath, _remoteApp.IconIndex, IconFilePath))
                    {
                        MessageBox.Show("Icon could not be created the RemoteApp. RDP file will still be created.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    // Check if there are file type associations before trying to work with the file type association icons
                    if (_remoteApp.FileTypeAssociations != null)
                    {
                        foreach (RemoteAppLib.FileTypeAssociation fta in _remoteApp.FileTypeAssociations)
                        {
                            var ProductFileName = RDPPath.Substring(0, RDPPath.Length - 4);
                            ExtractFtIcon(ProductFileName, fta);
                        }
                    }
                }

                Close();
            }
            else
            {
                // !!!!!!!  If it's an MSI
                var RDP = new RDP2MSIlib.RDP();
                CreateRDPFile(RDPPath, _remoteApp);
                RDP.RDPFilePath = RDPPath;
                RDP.ShortcutOnDesktop = ShortcutDesktopCheckBox.Checked;
                RDP.ShortcutInStart = ShortcutStartCheckBox.Checked;
                RDP.ShortcutSubfolderInStart = SubfolderRadioButton.Checked;
                RDP.ProductRemoteTag = ShortcutTagTextBox.Text;
                RDP.PerUser = PerUserRadioButton.Checked;
                var FilesToDelete = new List<string>();
                var ProductFileName = RDPPath.Substring(0, RDPPath.Length - 4);
                var IconFilePath = ProductFileName + ".ico";
                FilesToDelete.Add(RDPPath);
                if (!IconModule.ExtractToIco(_remoteApp.IconPath, _remoteApp.IconIndex, IconFilePath))
                {
                    MessageBox.Show("There was an error loading icon:" + Environment.NewLine + _remoteApp.IconPath + "," + _remoteApp.IconIndex + Environment.NewLine + "The MSI will still be created but the main icon will be missing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    FilesToDelete.Add(IconFilePath);
                }

                if (_remoteApp.FileTypeAssociations != null)
                {
                    foreach (RemoteAppLib.FileTypeAssociation fta in _remoteApp.FileTypeAssociations)
                    {
                        ExtractFtIcon(ProductFileName, fta);
                        FilesToDelete.Add(ProductFileName + "." + fta.Extension + ".ico");
                    }

                    RDP.FlatFileTypes = _remoteApp.FileTypeAssociations.GetFlatFileTypes();
                }

                RDP.CreateMSI(MSIPath);
                RemoteAppFunction.DeleteFiles(FilesToDelete);
                Close();
            }
        }

        private void ExtractFtIcon(string productFileName, RemoteAppLib.FileTypeAssociation fta)
        {
            // Extract icon for filetype
            if (!IconModule.ExtractToIco(fta.IconPath, int.Parse(fta.IconIndex), productFileName + "." + fta.Extension + ".ico"))
            {
                // If filetype icon fails to extract, then grab the default document icon from Shell32.dll
                IconModule.ExtractToIco(RemoteAppFunction.GetSysDir() + @"\shell32.dll", 0, productFileName + "." + fta.Extension + ".ico");
                // Possibly show an error here??
            }
        }

        private void CreateRDPFile(string rdpPath, RemoteAppLib.RemoteApp remoteApp)
        {
            // Dim App As New RemoteAppLib.RemoteApp = RemoteApp
            // App = RemoteApp
            var ServerAddress = this.ServerAddress.Text;
            var AltServerAddress = this.AltServerAddress.Text;
            var ServerPort = this.ServerPort.Text;
            var FlatFileTypes = "";
            if (remoteApp.FileTypeAssociations != null)
                FlatFileTypes = remoteApp.FileTypeAssociations.GetFlatFileTypes();
            var rdPfile = new RDPFileLib.RDPFile()
            {
                FullAddress = ServerAddress,
                AlternateFullAddress = AltServerAddress,
                ServerPort = (int)Math.Round(double.Parse(ServerPort)),
                RemoteApplicationName = remoteApp.FullName,
                RemoteApplicationProgram = "||" + remoteApp.Name,
                RemoteApplicationMode = 1,
                AlternateShell = "rdpinit.exe",
                AdditionalOptions = ExportAdditionalOptionsRdpString()
            };
            if (UseRDGatewayCheckBox.Checked)
            {
                rdPfile.GatewayHostname = GatewayAddress.Text;
                rdPfile.GatewayUsageMethod = AttemptDirectCheckBox.Checked ? 2 : 1;
                rdPfile.GatewayProfileUsageMethod = 1;
            }

            // These defaults moved into RDPOptionsWindow.RecommendedDefaults, which are written
            // through AdditionalOptions below (upstream issue #100).
            rdPfile.RemoteApplicationFileExtensions = FlatFileTypes;
            rdPfile.SaveRdpFile(rdpPath);
            if (CheckBoxSignRDPEnabled.Checked)
            {
                var rdpSign = new RDPSign.RDPSign();
                var Thumbprint = rdpSign.GetThumbprint(CertificateComboBox.Text);
                rdpSign.SignRDP(Thumbprint, rdpPath, CheckBoxCreateSignedAndUnsigned.Checked);
            }
        }

        private string GetFlatFileTypesList(string appName, string delim = ",")
        {
            var BaseKeyName = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\" + appName + @"\Filetypes";
            var flatFtList = "";
            try
            {
                var BaseKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(BaseKeyName);
                foreach (var ft in BaseKey.GetSubKeyNames())
                    flatFtList += "." + ft + delim;
                flatFtList = flatFtList.TrimEnd(delim.ToCharArray());
            }
            catch (Exception)
            {
            }

            return flatFtList;
        }

        private void FTAButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Changes made here to File Type Associations are for this client connection only and will not be saved for next time." + Environment.NewLine + Environment.NewLine + "To make permanent changes to the File Type Associations for this RemoteApp, edit the RemoteApp.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _remoteApp = Forms.RemoteAppFileTypeAssociation.EditFileTypes(_remoteApp);
            if (_remoteApp.FileTypeAssociations != null)
                FTACountLabel.Text = "Count: " + _remoteApp.FileTypeAssociations.Count;
        }

        private void ShortcutStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SubfolderRadioButton.Enabled = ShortcutStartCheckBox.Checked;
            TopLevelRadioButton.Enabled = ShortcutStartCheckBox.Checked;
        }

        private void ShortcutTagCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ShortcutTagTextBox.Enabled = ShortcutTagCheckBox.Checked;
        }

        private void ServerAddress_TextChanged(object sender, EventArgs e)
        {
            RemoteAppFunction.ValidateDnsName(ServerAddress);
        }

        private void AltServerAddress_TextChanged(object sender, EventArgs e)
        {
            RemoteAppFunction.ValidateDnsName(AltServerAddress);
        }

        private void ServerPort_TextChanged(object sender, EventArgs e)
        {
            RemoteAppFunction.ValidatePort(ServerPort);
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetCcWindowSettings();
            SetCcWindowSettings();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveCcWindowSettings();
        }

        private void DisabledFTACheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DisabledFTACheckBox.Checked)
            {
                FTAButton.Enabled = false;
                FTACountLabel.Enabled = false;
            }
            else
            {
                FTAButton.Enabled = true;
                FTACountLabel.Enabled = true;
            }
        }

        private void CheckBoxSignRDPEnabled_CheckedChanged(object sender, EventArgs e)
        {
            CertificateComboBox.Enabled = CheckBoxSignRDPEnabled.Checked;
            if (EditAfterSave.Checked && CheckBoxSignRDPEnabled.Checked)
            {
                CheckBoxSignRDPEnabled.Checked = MessageBox.Show("You have selected \"Sign RDP file\" and \"Manually edit RDP file\"." + Environment.NewLine + Environment.NewLine + "If you save any changes to a signed RDP file it will stop working." + Environment.NewLine + Environment.NewLine + "Are you sure you want the RDP file to be signed?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            }
        }

        private void EditAfterSave_CheckedChanged(object sender, EventArgs e)
        {
            if (EditAfterSave.Checked && CheckBoxSignRDPEnabled.Checked)
            {
                EditAfterSave.Checked = MessageBox.Show("You have selected \"Sign RDP file\" and \"Manually edit RDP file\"." + Environment.NewLine + Environment.NewLine + "If you save any changes to a signed RDP file it will stop working." + Environment.NewLine + Environment.NewLine + "Are you sure you want to edit after saving?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
            }
        }

        private void CheckBoxCreateSignedAndUnsigned_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxCreateSignedAndUnsigned.Checked)
            {
                CheckBoxSignRDPEnabled.Checked = true;
            }
        }

        // Additional .rdp options edited through RDPOptionsWindow, as [name, key, type, value] rows.
        private string[,] _additionalOptions = new string[0, 4];

        private void RDPOptionsButton_Click(object sender, EventArgs e)
        {
            _additionalOptions = Forms.RDPOptionsWindow.EditAdditionalOptions(_additionalOptions);
        }

        /// <summary>
        ///     Renders the selected options as the "key:type:value" lines appended to the .rdp file.
        /// </summary>
        private string ExportAdditionalOptionsRdpString()
        {
            if (_additionalOptions == null || _additionalOptions.GetLength(0) == 0)
            {
                return string.Empty;
            }

            var optionsString = string.Empty;
            for (var row = 0; row < _additionalOptions.GetLength(0); row++)
            {
                optionsString += _additionalOptions[row, 1] + ":";
                optionsString += _additionalOptions[row, 2] + ":";
                optionsString += _additionalOptions[row, 3] + Environment.NewLine;
            }

            return optionsString.Trim();
        }

        /// <summary>
        ///     Serialises the option table into the per-user settings value.
        /// </summary>
        internal static string FlattenOptions(string[,] options)
        {
            if (options == null || options.GetLength(0) == 0)
            {
                return string.Empty;
            }

            var lines = new List<string>();
            for (var row = 0; row < options.GetLength(0); row++)
            {
                var lineValues = new List<string>();
                for (var col = 0; col < options.GetLength(1); col++)
                {
                    lineValues.Add(options[row, col]);
                }

                lines.Add(string.Join("|", lineValues));
            }

            return string.Join(Environment.NewLine, lines);
        }

        /// <summary>
        ///     Restores the option table from the per-user settings value.
        /// </summary>
        internal static string[,] UnflattenOptions(string flattened)
        {
            var lines = flattened.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0)
            {
                return new string[0, 4];
            }

            var columns = lines[0].Split('|');
            var result = new string[lines.Length, columns.Length];
            for (var row = 0; row < lines.Length; row++)
            {
                var lineValues = lines[row].Split('|');
                for (var col = 0; col < columns.Length && col < lineValues.Length; col++)
                {
                    result[row, col] = lineValues[col];
                }
            }

            return result;
        }
    }
}