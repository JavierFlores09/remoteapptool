using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace RemoteApp.Tool.New
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

        private RemoteAppLib.New.RemoteApp RemoteApp = new RemoteAppLib.New.RemoteApp();

        public void CreateClientConnection(RemoteAppLib.New.RemoteApp SelectedRemoteApp)
        {
            RemoteApp = SelectedRemoteApp;
            var rdpSign = new RDPSign.New.RDPSign();
            string RemoteAppShortName = RemoteApp.Name;
            Text = "Create Client Connection for " + RemoteAppShortName;
            RdpsignErrorLabel.Text = "";
            CertificateComboBox.Items.AddRange(rdpSign.GetCertificateFriendlyName().ToArray());
            SetCCWindowSettings();
            if (string.IsNullOrEmpty(ServerAddress.Text))
                ServerAddress.Text = System.Net.Dns.GetHostName();
            if (string.IsNullOrEmpty(AltServerAddress.Text))
                AltServerAddress.Text = ServerAddress.Text;
            if (ServerPort.Text == "0")
                ServerPort.Text = "3389";
            if (!File.Exists(RemoteApp.IconPath))
            {
                CreateRAWebIcon.Checked = false;
                CreateRAWebIcon.Enabled = false;
            }
            else
            {
                CreateRAWebIcon.Enabled = RDPRadioButton.Checked;
            }

            var wx = new RDP2MSIlib.New.RDP();
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

            if (RemoteApp.FileTypeAssociations != null)
                FTACountLabel.Text = "Count: " + RemoteApp.FileTypeAssociations.Count;
            RDPRadioButton.Focus();
            HelpSystem.SetupTips(this);
            ShowDialog();
            Forms.RemoteAppMainWindow.ReloadApps();
            //Dispose();
        }

        public void ResetCCWindowSettings()
        {
            UserSettings.Instance.SavedConnectionModeMSI = false;
            UserSettings.Instance.SavedOpenWithNotepad = false;
            UserSettings.Instance.SavedServerAddress = System.Net.Dns.GetHostName();
            UserSettings.Instance.SavedServerPort = 3389;
            UserSettings.Instance.SavedAltServerAddress = UserSettings.Instance.SavedServerAddress;
            UserSettings.Instance.SavedUseRDGateway = false;
            UserSettings.Instance.SavedRDGatewayAddress = "";
            UserSettings.Instance.SavedAttemptDirectRDGateway = false;
            UserSettings.Instance.SavedMSIShortcutDesktop = true;
            UserSettings.Instance.SavedMSIShortcutStart = true;
            UserSettings.Instance.SavedMSIShortcutStartTopLevel = false;
            UserSettings.Instance.SavedUseShortcutTag = true;
            UserSettings.Instance.SavedShortcutTag = "remote";
            UserSettings.Instance.SavedClientConnectionOptions = false;
            UserSettings.Instance.SavedCreateRAWebIcon = false;
            UserSettings.Instance.SavedMSIPerUser = false;
            UserSettings.Instance.SavedDisableFTA = false;
            UserSettings.Instance.SavedSignRDP = false;
            UserSettings.Instance.SavedSignedAndUnsigned = false;
            UserSettings.Instance.SavedCertSelected = 0;
        }

        public void SaveCCWindowSettings()
        {
            UserSettings.Instance.SavedConnectionModeMSI = MSIRadioButton.Checked;
            UserSettings.Instance.SavedOpenWithNotepad = EditAfterSave.Checked;
            UserSettings.Instance.SavedServerAddress = ServerAddress.Text;
            UserSettings.Instance.SavedServerPort = (int)Math.Round(double.Parse(ServerPort.Text));
            UserSettings.Instance.SavedAltServerAddress = AltServerAddress.Text;
            UserSettings.Instance.SavedUseRDGateway = UseRDGatewayCheckBox.Checked;
            UserSettings.Instance.SavedRDGatewayAddress = GatewayAddress.Text;
            UserSettings.Instance.SavedAttemptDirectRDGateway = AttemptDirectCheckBox.Checked;
            UserSettings.Instance.SavedMSIShortcutDesktop = ShortcutDesktopCheckBox.Checked;
            UserSettings.Instance.SavedMSIShortcutStart = ShortcutStartCheckBox.Checked;
            UserSettings.Instance.SavedMSIShortcutStartTopLevel = TopLevelRadioButton.Checked;
            UserSettings.Instance.SavedUseShortcutTag = ShortcutTagCheckBox.Checked;
            UserSettings.Instance.SavedShortcutTag = ShortcutTagTextBox.Text;
            UserSettings.Instance.SavedCreateRAWebIcon = CreateRAWebIcon.Checked;
            UserSettings.Instance.SavedMSIPerUser = PerUserRadioButton.Checked;
            UserSettings.Instance.SavedDisableFTA = DisabledFTACheckBox.Checked;
            UserSettings.Instance.SavedSignRDP = CheckBoxSignRDPEnabled.Checked;
            UserSettings.Instance.SavedSignedAndUnsigned = CheckBoxCreateSignedAndUnsigned.Checked;
            UserSettings.Instance.SavedCertSelected = CertificateComboBox.SelectedIndex;
        }

        public void SetCCWindowSettings()
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
            UseRDGatewayCheckBox.Checked = UserSettings.Instance.SavedUseRDGateway;
            GatewayAddress.Text = UserSettings.Instance.SavedRDGatewayAddress;
            AttemptDirectCheckBox.Checked = UserSettings.Instance.SavedAttemptDirectRDGateway;
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
            CreateRAWebIcon.Checked = UserSettings.Instance.SavedCreateRAWebIcon;
            DisabledFTACheckBox.Checked = UserSettings.Instance.SavedDisableFTA;
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
            string MSIPath = string.Empty;
            if (CheckBoxSignRDPEnabled.Checked && string.IsNullOrEmpty(CertificateComboBox.SelectedItem as string))
            {
                MessageBox.Show("You must select a certificate to sign the RDP file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DisabledFTACheckBox.Checked && RemoteApp.FileTypeAssociations != null)
                RemoteApp.FileTypeAssociations.Clear();
            string RDPPath;
            if (RDPRadioButton.Checked)
            {
                FileSaveRDP.FileName = RemoteApp.Name;
                if (FileSaveRDP.ShowDialog() != DialogResult.OK)
                    return;
                RDPPath = FileSaveRDP.FileName;
            }
            else
            {
                FileSaveMSI.FileName = RemoteApp.Name;
                if (FileSaveMSI.ShowDialog() != DialogResult.OK)
                    return;
                MSIPath = FileSaveMSI.FileName;
                RDPPath = Environment.GetEnvironmentVariable("TEMP") + @"\" + RemoteApp.Name + ".rdp";
                _ = Environment.GetEnvironmentVariable("TEMP") + @"\" + RemoteApp.Name + ".msi";
            }

            if (UseRDGatewayCheckBox.Checked)
            {
                _ = GatewayAddress.Text;
                _ = AttemptDirectCheckBox.Checked;
            }

            if (RDPRadioButton.Checked)
            {
                CreateRDPFile(RDPPath, RemoteApp);
                // !!!!!!! If it's an RDP file
                if (EditAfterSave.Checked)
                {
                    string CmdLine = RemoteAppFunction.GetSysDir() + @"\notepad.exe";
                    Process.Start(CmdLine, FileSaveRDP.FileName);
                }

                if (CreateRAWebIcon.Checked)
                {
                    string IconFilePath = RDPPath.Substring(0, RDPPath.Length - 4) + ".ico";
                    if (!IconModule.ExtractToIco(RemoteApp.IconPath, RemoteApp.IconIndex, IconFilePath))
                    {
                        MessageBox.Show("Icon could not be created the RemoteApp. RDP file will still be created.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    // Check if there are file type associations before trying to work with the file type association icons
                    if (RemoteApp.FileTypeAssociations != null)
                    {
                        foreach (RemoteAppLib.New.FileTypeAssociation FTA in RemoteApp.FileTypeAssociations)
                        {
                            string ProductFileName = RDPPath.Substring(0, RDPPath.Length - 4);
                            ExtractFTIcon(ProductFileName, FTA);
                        }
                    }
                }

                Close();
            }
            else
            {
                // !!!!!!!  If it's an MSI
                var RDP = new RDP2MSIlib.New.RDP();
                CreateRDPFile(RDPPath, RemoteApp);
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
                if (!IconModule.ExtractToIco(RemoteApp.IconPath, RemoteApp.IconIndex, IconFilePath))
                {
                    MessageBox.Show("There was an error loading icon:" + Environment.NewLine + RemoteApp.IconPath + "," + RemoteApp.IconIndex + Environment.NewLine + "The MSI will still be created but the main icon will be missing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    FilesToDelete.Add(IconFilePath);
                }

                if (RemoteApp.FileTypeAssociations != null)
                {
                    foreach (RemoteAppLib.New.FileTypeAssociation FTA in RemoteApp.FileTypeAssociations)
                    {
                        ExtractFTIcon(ProductFileName, FTA);
                        FilesToDelete.Add(ProductFileName + "." + FTA.Extension + ".ico");
                    }

                    RDP.FlatFileTypes = RemoteApp.FileTypeAssociations.GetFlatFileTypes();
                }

                RDP.CreateMSI(MSIPath);
                RemoteAppFunction.DeleteFiles(FilesToDelete);
                Close();
            }
        }

        private void ExtractFTIcon(string ProductFileName, RemoteAppLib.New.FileTypeAssociation FTA)
        {
            // Extract icon for filetype
            if (!IconModule.ExtractToIco(FTA.IconPath, int.Parse(FTA.IconIndex), ProductFileName + "." + FTA.Extension + ".ico"))
            {
                // If filetype icon fails to extract, then grab the default document icon from Shell32.dll
                IconModule.ExtractToIco(RemoteAppFunction.GetSysDir() + @"\shell32.dll", 0, ProductFileName + "." + FTA.Extension + ".ico");
                // Possibly show an error here??
            }
        }

        private void CreateRDPFile(string RDPPath, RemoteAppLib.New.RemoteApp RemoteApp)
        {
            // Dim App As New RemoteAppLib.New.RemoteApp = RemoteApp
            // App = RemoteApp
            string ServerAddress = this.ServerAddress.Text;
            string AltServerAddress = this.AltServerAddress.Text;
            string ServerPort = this.ServerPort.Text;
            string FlatFileTypes = "";
            if (RemoteApp.FileTypeAssociations != null)
                FlatFileTypes = RemoteApp.FileTypeAssociations.GetFlatFileTypes();
            var RDPfile = new RDPFileLib.New.RDPFile()
            {
                full_address = ServerAddress,
                alternate_full_address = AltServerAddress,
                server_port = (int)Math.Round(double.Parse(ServerPort)),
                remoteapplicationname = RemoteApp.FullName,
                remoteapplicationprogram = "||" + RemoteApp.Name,
                remoteapplicationmode = 1,
                disableremoteappcapscheck = 1,
                alternate_shell = "rdpinit.exe"
            };
            if (UseRDGatewayCheckBox.Checked)
            {
                RDPfile.gatewayhostname = GatewayAddress.Text;
                RDPfile.gatewayusagemethod = AttemptDirectCheckBox.Checked ? 2 : 1;
                RDPfile.gatewayprofileusagemethod = 1;
            }

            RDPfile.prompt_for_credentials_on_client = 1;
            RDPfile.promptcredentialonce = 0;
            RDPfile.devicestoredirect = "*";
            RDPfile.drivestoredirect = "*";
            RDPfile.redirectcomports = 1;
            RDPfile.redirectdrives = 1;
            RDPfile.allow_desktop_composition = 1;
            RDPfile.allow_font_smoothing = 1;
            RDPfile.span_monitors = 1;
            RDPfile.use_multimon = 1;
            RDPfile.remoteapplicationfileextensions = FlatFileTypes;
            RDPfile.SaveRDPfile(RDPPath);
            if (CheckBoxSignRDPEnabled.Checked)
            {
                var rdpSign = new RDPSign.New.RDPSign();
                string Thumbprint = rdpSign.GetThumbprint(CertificateComboBox.Text);
                rdpSign.SignRDP(Thumbprint, RDPPath, CheckBoxCreateSignedAndUnsigned.Checked);
            }
        }

        private string GetFlatFileTypesList(string AppName, string Delim = ",")
        {
            string BaseKeyName = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\" + AppName + @"\Filetypes";
            string FlatFTList = "";
            try
            {
                var BaseKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(BaseKeyName);
                foreach (string FT in BaseKey.GetSubKeyNames())
                    FlatFTList += "." + FT + Delim;
                FlatFTList = FlatFTList.TrimEnd(Delim.ToCharArray());
            }
            catch (Exception)
            {
            }

            return FlatFTList;
        }

        private void FTAButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Changes made here to File Type Associations are for this client connection only and will not be saved for next time." + Environment.NewLine + Environment.NewLine + "To make permanent changes to the File Type Associations for this RemoteApp, edit the RemoteApp.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RemoteApp = Forms.RemoteAppFileTypeAssociation.EditFileTypes(RemoteApp);
            if (RemoteApp.FileTypeAssociations != null)
                FTACountLabel.Text = "Count: " + RemoteApp.FileTypeAssociations.Count;
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
            RemoteAppFunction.ValidateDNSname(ServerAddress);
        }

        private void AltServerAddress_TextChanged(object sender, EventArgs e)
        {
            RemoteAppFunction.ValidateDNSname(AltServerAddress);
        }

        private void ServerPort_TextChanged(object sender, EventArgs e)
        {
            RemoteAppFunction.ValidatePort(ServerPort);
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetCCWindowSettings();
            SetCCWindowSettings();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveCCWindowSettings();
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
    }
}