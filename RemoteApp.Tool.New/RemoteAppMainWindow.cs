using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RemoteAppLib;
using System.IO;

namespace RemoteApp.Tool.New
{
    public partial class RemoteAppMainWindow
    {
        public RemoteAppMainWindow()
        {
            InitializeComponent();
            _AppList.Name = "AppList";
            _CreateButton.Name = "CreateButton";
            _DeleteButton.Name = "DeleteButton";
            _EditButton.Name = "EditButton";
            _CreateClientConnection.Name = "CreateClientConnection";
            _NewRemoteAppadvancedToolStripMenuItem.Name = "NewRemoteAppadvancedToolStripMenuItem";
            _ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            _HostOptionsToolStripMenuItem.Name = "HostOptionsToolStripMenuItem";
            _RemoveUnusedFileTypeAssociationsToolStripMenuItem.Name = "RemoveUnusedFileTypeAssociationsToolStripMenuItem";
            _BackupAllRemoteAppsToolStripMenuItem.Name = "BackupAllRemoteAppsToolStripMenuItem";
            _WebsiteToolStripMenuItem.Name = "WebsiteToolStripMenuItem";
            _AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
        }

        private void RemoteAppMainWindow_Disposed(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                return;
            }
            UserSettings.Instance.MainWindowWidth = Width;
            UserSettings.Instance.MainWindowHeight = Height;
        }

        private void RemoteAppMainWindow_Load(object sender, EventArgs e)
        {
            new SystemRemoteApps().Init();
            try
            {
                IconModule.TestIconLib();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("IconLib.dll is unavailable. Please add it to the RemoteApp Tool folder.");
                Environment.Exit(0);
            }

            Text = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name} {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version} ({System.Net.Dns.GetHostName()})";
            if (Control.ModifierKeys != Keys.Shift)
            {
                if (UserSettings.Instance.MainWindowWidth >= MinimumSize.Width)
                    Width = UserSettings.Instance.MainWindowWidth;
                if (UserSettings.Instance.MainWindowHeight >= MinimumSize.Height)
                    Height = UserSettings.Instance.MainWindowHeight;
            }

            HelpSystem.SetupTips(this);
            AddSysMenuItems();
            ReloadApps();
        }

        public override void ReloadApps()
        {
            AppList.Clear();
            RemoteAppCollection Apps = new SystemRemoteApps().GetAll();
            foreach (RemoteAppLib.RemoteApp App in Apps)
            {
                SmallIcons.Images.RemoveByKey(App.Name);
                Image TheBitmap = RemoteAppFunction.GetAppBitmap(App.Name);
                var AppItem = new ListViewItem(App.Name)
                {
                    ToolTipText = App.FullName,
                    ImageIndex = 0
                };
                SmallIcons.Images.Add(App.Name, TheBitmap);
                AppItem.ImageKey = App.Name;
                AppList.Items.Add(AppItem);
            }

            NoAppsLabel.Visible = Apps.Count == 0;

            EditButton.Enabled = false;
            DeleteButton.Enabled = false;
            CreateClientConnection.Enabled = false;
        }

        private void AppList_DoubleClick(object sender, EventArgs e)
        {
            if (AppList.SelectedItems.Count == 1)
            {
                EditRemoteApp(AppList.SelectedItems[0].Text);
            }
        }

        private void AppList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AppList.SelectedItems.Count == 1)
            {
                EditButton.Enabled = true;
                DeleteButton.Enabled = true;
                CreateClientConnection.Enabled = true;
            }
            else
            {
                EditButton.Enabled = false;
                DeleteButton.Enabled = false;
                CreateClientConnection.Enabled = false;
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (AppList.SelectedItems.Count == 1)
            {
                EditRemoteApp(AppList.SelectedItems[0].Text);
            }
        }

        private void EditRemoteApp(string AppName)
        {
            var sra = new SystemRemoteApps();
            Forms.RemoteAppEditWindow.EditRemoteApp(sra.GetApp(AppName));
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteRemoteApp(AppList.SelectedItems[0].Text);
            ReloadApps();
        }

        private void DeleteRemoteApp(string AppName)
        {
            if (MessageBox.Show("Are you sure you want to remove " + AppName + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                new SystemRemoteApps().DeleteApp(AppName);
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            Forms.RemoteAppEditWindow.CreateRemoteApp();
            ReloadApps();
        }

        // System Menu Code (for about box)
        [DllImport("user32.dll", EntryPoint = "AppendMenuA")]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, IntPtr uIDNewItem, string lpNewItem);
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        private const int MF_STRING = 0x0;
        private const int MF_SEPARATOR = 0x800;
        private const int WM_SYSCOMMAND = 0x112;

        private void AddSysMenuItems()
        {
            // Get the System Menus Handle.
            var hSysMenu = GetSystemMenu(Handle, false);
            // Add a standard Separator Item.
            AppendMenu(hSysMenu, MF_SEPARATOR, (IntPtr)1000, null);
            // Add an About Menu Item.
            _ = AppendMenu(hSysMenu, MF_STRING, (IntPtr)1001, "About...");
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_SYSCOMMAND)
            {
                switch (m.WParam.ToInt32())
                {
                    case 1001:
                        {
                            Forms.RemoteAppAboutWindow.ShowDialog();
                            break;
                        }
                }
            }
        }

        private void CreateClientConnection_Click(object sender, EventArgs e)
        {
            var sra = new SystemRemoteApps();
            Forms.RemoteAppCreateClientConnection.CreateClientConnection(sra.GetApp(AppList.SelectedItems[0].Text));
        }

        private void HostOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.RemoteAppHostOptions.SetValues();
            Forms.RemoteAppHostOptions.ShowDialog();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.RemoteAppAboutWindow.ShowDialog();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void WebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/kimmknight/remoteapptool");
        }

        private void RemoveUnusedFileTypeAssociationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalFtaModule.RemoveUnusedFTAs();
        }

        private void NewRemoteAppadvancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.RemoteAppEditWindow.CreateRemoteApp(true);
            ReloadApps();
        }

        private void BackupAllRemoteAppsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupSaveFileDialog.FileName = $"{System.Net.Dns.GetHostName()} RemoteApps Backup {DateTime.Now:yyyy-MM-dd}.reg";
            DialogResult = BackupSaveFileDialog.ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                const string RemoteAppRegPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications";
                string BackupSavePath = BackupSaveFileDialog.FileName;
                var StartInfo = new ProcessStartInfo("reg.exe", $"export \"{RemoteAppRegPath}\" \"{BackupSavePath}\" /y")
                {
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process.Start(StartInfo);
            }
        }
    }
}