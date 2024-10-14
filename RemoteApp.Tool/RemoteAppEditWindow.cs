using System;
using System.IO;
using System.Windows.Forms;
using RemoteAppLib;

namespace RemoteApp.Tool
{
    public partial class RemoteAppEditWindow
    {
        private readonly SystemRemoteApps sra;
        public RemoteAppEditWindow()
        {
            InitializeComponent();
            _CancelEditButton.Name = "CancelEditButton";
            _SaveButton.Name = "SaveButton";
            _FTAButton.Name = "FTAButton";
            _ShortNameText.Name = "ShortNameText";
            _BrowseIconPath.Name = "BrowseIconPath";
            _IconResetButton.Name = "IconResetButton";
            _IconIndexText.Name = "IconIndexText";
            _BrowsePath.Name = "BrowsePath";

            sra = new SystemRemoteApps();
        }

        private RemoteAppLib.RemoteApp _remoteApp = new RemoteAppLib.RemoteApp();

        public RemoteAppLib.RemoteApp EditRemoteApp(RemoteAppLib.RemoteApp selectedRemoteApp)
        {
            HelpSystem.SetupTips(this);
            _remoteApp = selectedRemoteApp;
            Text = "Properties of " + _remoteApp.Name;
            Size = MinimumSize;
            HelpSystem.SetupTips(this);
            LoadValues();
            _ = ShowDialog();
            // The caller (main window) reloads the list and its icon, see upstream issue #136
            //Forms.RemoteAppMainWindow.ReloadApps();
            //Dispose();
            return _remoteApp;
        }

        public RemoteAppLib.RemoteApp CreateRemoteApp(bool advanced = false)
        {
            Text = "New RemoteApp";
            Size = MinimumSize;
            HelpSystem.SetupTips(this);
            CommandLineOptionCombo.SelectedIndex = 1;
            TSWAbox.SelectedIndex = 0;
            FTAButton.Enabled = false;
            if (advanced)
            {
                ShowDialog();
            }
            else if (DoBrowsePath())
            {
                SaveAndCloseOrWindow();
            }

            //Dispose();
            return _remoteApp;
        }

        private void SetToolTips()
        {
            var toolTip1 = new ToolTip
            {
                // Set up the delays for the ToolTip.
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 500
            };
            toolTip1.SetToolTip(BrowseIconPath, "Select an icon");
            toolTip1.SetToolTip(BrowsePath, "Browse for application");
            toolTip1.SetToolTip(IconResetButton, "Reset icon to application default");
        }

        public void LoadValues()
        {
            ShortNameText.Text = _remoteApp.Name;
            CommandLineOptionCombo.SelectedIndex = _remoteApp.CommandLineOption;
            FullNameText.Text = _remoteApp.FullName;
            PathText.Text = _remoteApp.Path;
            CommandLineText.Text = _remoteApp.CommandLine;
            CommandLineOptionCombo.SelectedIndex = _remoteApp.CommandLineOption;
            IconPathText.Text = _remoteApp.IconPath;
            IconIndexText.Text = _remoteApp.IconIndex.ToString();
            var tswa = 0;
            if (_remoteApp.TSWA)
                tswa = 1;
            TSWAbox.SelectedIndex = tswa;
            var TheIcon = IconModule.ReturnIcon(IconPathText.Text, (int)Math.Round(double.Parse(IconIndexText.Text)), true);
            Icon = TheIcon ?? Forms.RemoteAppMainWindow.Icon;
        }

        private void CancelEditButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IconResetButton_Click(object sender, EventArgs e)
        {
            IconPathText.Text = PathText.Text;
            IconIndexText.Text = 0.ToString();
        }

        private void BrowseIconPath_Click(object sender, EventArgs e)
        {
            IconPick();
        }

        private void IconPick()
        {
            var StartPath = RemoteAppFunction.GetSysDir() + @"\shell32.dll";
            var StartIndex = (int)Math.Round(double.Parse(IconIndexText.Text));
            if (File.Exists(IconPathText.Text))
                StartPath = IconPathText.Text;
            var PickedIcon = Forms.RemoteAppIconPicker.PickIcon(StartPath, StartIndex);
            if (PickedIcon.IconPath != null)
            {
                IconPathText.Text = PickedIcon.IconPath;
                IconIndexText.Text = PickedIcon.IconIndex;
            }
        }

        private void BrowsePath_Click(object sender, EventArgs e)
        {
            DoBrowsePath();
        }

        private bool DoBrowsePath()
        {
            var DoBrowsePathRet = false;
            if (File.Exists(PathText.Text))
                FileBrowserPath.InitialDirectory = Path.GetDirectoryName(PathText.Text);
            if (FileBrowserPath.ShowDialog() == DialogResult.OK)
            {
                var FilePath = FileBrowserPath.FileName;
                PathText.Text = FilePath;
                _remoteApp.Path = FilePath;
                if (string.IsNullOrEmpty(IconPathText.Text))
                    _remoteApp.IconPath = FilePath;
                if (FilePath.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                {
                    var title = RemoteAppFunction.GetExeTitle(FilePath);
                    if (string.IsNullOrEmpty(ShortNameText.Text))
                        _remoteApp.Name = (string)RemoteAppFunction.FixShortAppName(title);
                    if (string.IsNullOrEmpty(FullNameText.Text))
                        _remoteApp.FullName = title;
                }

                LoadValues();
                DoBrowsePathRet = true;
            }

            return DoBrowsePathRet;
        }

        private void SaveRemoteApp(string shortName, string fullName, string path, string vPath, string commandLine, int commandLineSetting, string iconPath, int iconIndex, int showInTswa)
        {
            if (_remoteApp.Name != null && Text != "New RemoteApp")
            {
                if ((_remoteApp.Name ?? "") != (shortName ?? ""))
                    sra.RenameApp(_remoteApp.Name, shortName);
            }

            _remoteApp.Name = shortName;
            _remoteApp.FullName = fullName;
            _remoteApp.Path = path;
            _remoteApp.VPath = vPath;
            _remoteApp.IconPath = iconPath;
            _remoteApp.IconIndex = iconIndex;
            _remoteApp.CommandLine = commandLine;
            _remoteApp.CommandLineOption = commandLineSetting;
            _remoteApp.TSWA = showInTswa != 0;
            sra.SaveApp(_remoteApp);
        }

        private bool DoesAppExist(string appName)
        {
            var AppExists = false;
            var sra = new SystemRemoteApps();
            foreach (RemoteAppLib.RemoteApp App in sra.GetAll())
            {
                if ((App.Name ?? "") == (appName ?? ""))
                    AppExists = true;
            }

            return AppExists;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveAndClose();
        }

        private void SaveAndCloseOrWindow()
        {
            if (string.IsNullOrEmpty(ShortNameText.Text))
            {
                MessageBox.Show("Name must not be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ShowDialog();
            }
            else if (string.IsNullOrEmpty(FullNameText.Text))
            {
                MessageBox.Show("Full name must not be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ShowDialog();
            }
            else if (string.IsNullOrEmpty(PathText.Text))
            {
                MessageBox.Show("Path must not be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ShowDialog();
            }
            else if ((ShortNameText.Text ?? "") != (_remoteApp.Name ?? "") && DoesAppExist(ShortNameText.Text))
            {
                MessageBox.Show("A RemoteApp with the same name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ShowDialog();
            }
            else if (Text == "New RemoteApp" && DoesAppExist(ShortNameText.Text))
            {
                MessageBox.Show("A RemoteApp with the same name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ShowDialog();
            }
            else
            {
                SaveRemoteApp(ShortNameText.Text.Trim(), FullNameText.Text, PathText.Text, PathText.Text, CommandLineText.Text, CommandLineOptionCombo.SelectedIndex, IconPathText.Text, (int)Math.Round(double.Parse(IconIndexText.Text)), TSWAbox.SelectedIndex);
                Close();
            }
        }

        private void SaveAndClose()
        {
            if (string.IsNullOrEmpty(ShortNameText.Text))
            {
                MessageBox.Show("Name must not be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (string.IsNullOrEmpty(FullNameText.Text))
            {
                MessageBox.Show("Full name must not be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (string.IsNullOrEmpty(PathText.Text))
            {
                MessageBox.Show("Path must not be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if ((ShortNameText.Text ?? "") != (_remoteApp.Name ?? "") && DoesAppExist(ShortNameText.Text))
            {
                MessageBox.Show("A RemoteApp with the same name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (Text == "New RemoteApp" && DoesAppExist(ShortNameText.Text))
            {
                MessageBox.Show("A RemoteApp with the same name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                SaveRemoteApp(ShortNameText.Text.Trim(), FullNameText.Text, PathText.Text, PathText.Text, CommandLineText.Text, CommandLineOptionCombo.SelectedIndex, IconPathText.Text, (int)Math.Round(double.Parse(IconIndexText.Text)), TSWAbox.SelectedIndex);
                Close();
            }
        }

        private void IconIndexText_TextChanged(object sender, EventArgs e)
        {
            RemoteAppFunction.ValidateInteger(IconIndexText);
        }

        private void FTAButton_Click(object sender, EventArgs e)
        {
            Forms.RemoteAppFileTypeAssociation.EditFileTypes(_remoteApp);
        }

        private void ShortNameText_TextChanged(object sender, EventArgs e)
        {
            RemoteAppFunction.ValidateAppName(ShortNameText);
        }
    }
}