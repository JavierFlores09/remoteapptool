using System;
using System.IO;
using System.Windows.Forms;
using RemoteAppLib.New;

namespace RemoteApp.Tool.New
{
    public partial class RemoteAppEditWindow
    {
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
        }

        private RemoteAppLib.New.RemoteApp RemoteApp = new RemoteAppLib.New.RemoteApp();

        public RemoteAppLib.New.RemoteApp EditRemoteApp(RemoteAppLib.New.RemoteApp SelectedRemoteApp)
        {
            HelpSystem.SetupTips(this);
            RemoteApp = SelectedRemoteApp;
            Text = "Properties of " + RemoteApp.Name;
            Size = MinimumSize;
            HelpSystem.SetupTips(this);
            LoadValues();
            _ = ShowDialog();
            Forms.RemoteAppMainWindow.ReloadApps();
            //Dispose();
            return RemoteApp;
        }

        public RemoteAppLib.New.RemoteApp CreateRemoteApp(bool Advanced = false)
        {
            Text = "New RemoteApp";
            Size = MinimumSize;
            HelpSystem.SetupTips(this);
            CommandLineOptionCombo.SelectedIndex = 1;
            TSWAbox.SelectedIndex = 0;
            FTAButton.Enabled = false;
            if (Advanced)
            {
                ShowDialog();
            }
            else if (DoBrowsePath())
            {
                SaveAndCloseOrWindow();
            }

            //Dispose();
            return RemoteApp;
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
            ShortNameText.Text = RemoteApp.Name;
            CommandLineOptionCombo.SelectedIndex = RemoteApp.CommandLineOption;
            FullNameText.Text = RemoteApp.FullName;
            PathText.Text = RemoteApp.Path;
            CommandLineText.Text = RemoteApp.CommandLine;
            CommandLineOptionCombo.SelectedIndex = RemoteApp.CommandLineOption;
            IconPathText.Text = RemoteApp.IconPath;
            IconIndexText.Text = RemoteApp.IconIndex.ToString();
            var TSWA = 0;
            if (RemoteApp.TSWA)
                TSWA = 1;
            TSWAbox.SelectedIndex = TSWA;
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
            string StartPath = RemoteAppFunction.GetSysDir() + @"\shell32.dll";
            int StartIndex = (int)Math.Round(double.Parse(IconIndexText.Text));
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
            bool DoBrowsePathRet = false;
            if (File.Exists(PathText.Text))
                FileBrowserPath.InitialDirectory = Path.GetDirectoryName(PathText.Text);
            if (FileBrowserPath.ShowDialog() == DialogResult.OK)
            {
                string FilePath = FileBrowserPath.FileName;
                PathText.Text = FilePath;
                RemoteApp.Path = FilePath;
                if (string.IsNullOrEmpty(IconPathText.Text))
                    RemoteApp.IconPath = FilePath;
                if (FilePath.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                {
                    var title = RemoteAppFunction.GetEXETitle(FilePath);
                    if (string.IsNullOrEmpty(ShortNameText.Text))
                        RemoteApp.Name = (string)RemoteAppFunction.FixShortAppName(title);
                    if (string.IsNullOrEmpty(FullNameText.Text))
                        RemoteApp.FullName = (string)title;
                }

                LoadValues();
                DoBrowsePathRet = true;
            }

            return DoBrowsePathRet;
        }

        private void SaveRemoteApp(string ShortName, string FullName, string Path, string VPath, string CommandLine, int CommandLineSetting, string IconPath, int IconIndex, int ShowInTSWA)
        {
            var SysApps = new SystemRemoteApps();
            if (RemoteApp.Name != null && Text != "New RemoteApp")
            {
                if ((RemoteApp.Name ?? "") != (ShortName ?? ""))
                    SysApps.RenameApp(RemoteApp.Name, ShortName);
            }

            RemoteApp.Name = ShortName;
            RemoteApp.FullName = FullName;
            RemoteApp.Path = Path;
            RemoteApp.VPath = VPath;
            RemoteApp.IconPath = IconPath;
            RemoteApp.IconIndex = IconIndex;
            RemoteApp.CommandLine = CommandLine;
            RemoteApp.CommandLineOption = CommandLineSetting;
            RemoteApp.TSWA = ShowInTSWA != 0;
            SysApps.SaveApp(RemoteApp);
        }

        private bool DoesAppExist(string AppName)
        {
            bool AppExists = false;
            var sra = new SystemRemoteApps();
            foreach (RemoteAppLib.New.RemoteApp App in sra.GetAll())
            {
                if ((App.Name ?? "") == (AppName ?? ""))
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
            else if ((ShortNameText.Text ?? "") != (RemoteApp.Name ?? "") && DoesAppExist(ShortNameText.Text))
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
            else if ((ShortNameText.Text ?? "") != (RemoteApp.Name ?? "") && DoesAppExist(ShortNameText.Text))
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
            Forms.RemoteAppFileTypeAssociation.EditFileTypes(RemoteApp);
        }

        private void ShortNameText_TextChanged(object sender, EventArgs e)
        {
            RemoteAppFunction.ValidateAppName(ShortNameText);
        }
    }
}