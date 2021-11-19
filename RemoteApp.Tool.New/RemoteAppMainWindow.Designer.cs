using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RemoteApp.Tool.New
{
    public partial class RemoteAppMainWindow : RemoteAppMainWindowBase
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is object)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteAppMainWindow));
            _AppList = new ListView();
            _AppList.DoubleClick += new EventHandler(AppList_DoubleClick);
            _AppList.SelectedIndexChanged += new EventHandler(AppList_SelectedIndexChanged);
            ShortName = new ColumnHeader();
            Title = new ColumnHeader();
            Path = new ColumnHeader();
            VPath = new ColumnHeader();
            RequiredCommandLine = new ColumnHeader();
            CommandLineSetting = new ColumnHeader();
            IconPath = new ColumnHeader();
            IconIndex = new ColumnHeader();
            SecurityDescriptor = new ColumnHeader();
            ShowInTSWA = new ColumnHeader();
            SmallIcons = new ImageList(components);
            SmallerIcons = new ImageList(components);
            NoAppsLabel = new Label();
            _CreateButton = new Button();
            _CreateButton.Click += new EventHandler(CreateButton_Click);
            _DeleteButton = new Button();
            _DeleteButton.Click += new EventHandler(DeleteButton_Click);
            _EditButton = new Button();
            _EditButton.Click += new EventHandler(EditButton_Click);
            _CreateClientConnection = new Button();
            _CreateClientConnection.Click += new EventHandler(CreateClientConnection_Click);
            ToolsMenuStrip = new MenuStrip();
            FileToolStripMenuItem = new ToolStripMenuItem();
            _NewRemoteAppadvancedToolStripMenuItem = new ToolStripMenuItem();
            _NewRemoteAppadvancedToolStripMenuItem.Click += new EventHandler(NewRemoteAppadvancedToolStripMenuItem_Click);
            ToolStripSeparator2 = new ToolStripSeparator();
            _ExitToolStripMenuItem = new ToolStripMenuItem();
            _ExitToolStripMenuItem.Click += new EventHandler(ExitToolStripMenuItem_Click);
            ToolsToolStripMenuItem = new ToolStripMenuItem();
            _HostOptionsToolStripMenuItem = new ToolStripMenuItem();
            _HostOptionsToolStripMenuItem.Click += new EventHandler(HostOptionsToolStripMenuItem_Click);
            ToolStripSeparator3 = new ToolStripSeparator();
            _RemoveUnusedFileTypeAssociationsToolStripMenuItem = new ToolStripMenuItem();
            _RemoveUnusedFileTypeAssociationsToolStripMenuItem.Click += new EventHandler(RemoveUnusedFileTypeAssociationsToolStripMenuItem_Click);
            _BackupAllRemoteAppsToolStripMenuItem = new ToolStripMenuItem();
            _BackupAllRemoteAppsToolStripMenuItem.Click += new EventHandler(BackupAllRemoteAppsToolStripMenuItem_Click);
            HelpToolStripMenuItem = new ToolStripMenuItem();
            _WebsiteToolStripMenuItem = new ToolStripMenuItem();
            _WebsiteToolStripMenuItem.Click += new EventHandler(WebsiteToolStripMenuItem_Click);
            ToolStripSeparator1 = new ToolStripSeparator();
            _AboutToolStripMenuItem = new ToolStripMenuItem();
            _AboutToolStripMenuItem.Click += new EventHandler(AboutToolStripMenuItem_Click);
            PictureBox1 = new PictureBox();
            BackupSaveFileDialog = new SaveFileDialog();
            ToolsMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // AppList
            // 
            _AppList.Alignment = ListViewAlignment.Default;
            _AppList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _AppList.BackColor = Color.White;
            _AppList.BorderStyle = BorderStyle.None;
            _AppList.Columns.AddRange(new ColumnHeader[] { ShortName, Title, Path, VPath, RequiredCommandLine, CommandLineSetting, IconPath, IconIndex, SecurityDescriptor, ShowInTSWA });
            _AppList.GridLines = true;
            _AppList.HideSelection = false;
            _AppList.LargeImageList = SmallIcons;
            _AppList.Location = new Point(12, 28);
            _AppList.MultiSelect = false;
            _AppList.Name = "_AppList";
            _AppList.Size = new Size(423, 189);
            _AppList.SmallImageList = SmallIcons;
            _AppList.TabIndex = 1;
            _AppList.TileSize = new Size(200, 36);
            _AppList.UseCompatibleStateImageBehavior = false;
            _AppList.View = View.Tile;
            // 
            // ShortName
            // 
            ShortName.Text = "ShortName";
            ShortName.Width = 200;
            // 
            // Title
            // 
            Title.Text = "Name";
            // 
            // Path
            // 
            Path.Text = "Path";
            // 
            // VPath
            // 
            VPath.Text = "VPath";
            // 
            // RequiredCommandLine
            // 
            RequiredCommandLine.Text = "RequiredCommandLine";
            // 
            // CommandLineSetting
            // 
            CommandLineSetting.Text = "CommandLineSetting";
            // 
            // IconPath
            // 
            IconPath.Text = "IconPath";
            // 
            // IconIndex
            // 
            IconIndex.Text = "IconIndex";
            // 
            // SecurityDescriptor
            // 
            SecurityDescriptor.Text = "SecurityDescriptor";
            // 
            // ShowInTSWA
            // 
            ShowInTSWA.Text = "ShowInTSWA";
            // 
            // SmallIcons
            // 
            SmallIcons.ImageStream = (ImageListStreamer)resources.GetObject("SmallIcons.ImageStream");
            SmallIcons.TransparentColor = Color.Transparent;
            SmallIcons.Images.SetKeyName(0, "smallicons.ico");
            // 
            // SmallerIcons
            // 
            SmallerIcons.ImageStream = (ImageListStreamer)resources.GetObject("SmallerIcons.ImageStream");
            SmallerIcons.TransparentColor = Color.Transparent;
            SmallerIcons.Images.SetKeyName(0, "tools_16x16.png");
            SmallerIcons.Images.SetKeyName(1, "help_16x16.png");
            SmallerIcons.Images.SetKeyName(2, "folder_16x16.png");
            SmallerIcons.Images.SetKeyName(3, "msi small.ico");
            SmallerIcons.Images.SetKeyName(4, "properties.ico");
            SmallerIcons.Images.SetKeyName(5, "plus.ico");
            SmallerIcons.Images.SetKeyName(6, "minus.ico");
            // 
            // NoAppsLabel
            // 
            NoAppsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            NoAppsLabel.ForeColor = Color.DarkGray;
            NoAppsLabel.Location = new Point(12, 91);
            NoAppsLabel.Name = "NoAppsLabel";
            NoAppsLabel.Size = new Size(410, 58);
            NoAppsLabel.TabIndex = 0;
            NoAppsLabel.Text = "There are no RemoteApps hosted on this computer." + '\r' + '\n' + "Click + to add one.";
            NoAppsLabel.TextAlign = ContentAlignment.MiddleCenter;
            NoAppsLabel.Visible = false;
            // 
            // CreateButton
            // 
            _CreateButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _CreateButton.AutoSize = true;
            _CreateButton.BackColor = Color.Transparent;
            _CreateButton.FlatAppearance.BorderSize = 0;
            _CreateButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            _CreateButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            _CreateButton.FlatStyle = FlatStyle.Flat;
            _CreateButton.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            _CreateButton.ImageIndex = 5;
            _CreateButton.ImageList = SmallerIcons;
            _CreateButton.Location = new Point(12, 227);
            _CreateButton.Name = "_CreateButton";
            _CreateButton.Size = new Size(22, 22);
            _CreateButton.TabIndex = 2;
            _CreateButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _CreateButton.UseVisualStyleBackColor = false;
            // 
            // DeleteButton
            // 
            _DeleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _DeleteButton.AutoSize = true;
            _DeleteButton.BackColor = Color.Transparent;
            _DeleteButton.Enabled = false;
            _DeleteButton.FlatAppearance.BorderSize = 0;
            _DeleteButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            _DeleteButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            _DeleteButton.FlatStyle = FlatStyle.Flat;
            _DeleteButton.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            _DeleteButton.ImageIndex = 6;
            _DeleteButton.ImageList = SmallerIcons;
            _DeleteButton.Location = new Point(40, 227);
            _DeleteButton.Name = "_DeleteButton";
            _DeleteButton.Size = new Size(22, 22);
            _DeleteButton.TabIndex = 3;
            _DeleteButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _DeleteButton.UseVisualStyleBackColor = false;
            // 
            // EditButton
            // 
            _EditButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _EditButton.AutoSize = true;
            _EditButton.BackColor = Color.Transparent;
            _EditButton.Enabled = false;
            _EditButton.FlatAppearance.BorderSize = 0;
            _EditButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            _EditButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            _EditButton.FlatStyle = FlatStyle.Flat;
            _EditButton.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            _EditButton.ForeColor = SystemColors.ControlText;
            _EditButton.ImageIndex = 4;
            _EditButton.ImageList = SmallerIcons;
            _EditButton.Location = new Point(68, 227);
            _EditButton.Name = "_EditButton";
            _EditButton.Size = new Size(22, 22);
            _EditButton.TabIndex = 4;
            _EditButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _EditButton.UseVisualStyleBackColor = false;
            // 
            // CreateClientConnection
            // 
            _CreateClientConnection.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _CreateClientConnection.AutoSize = true;
            _CreateClientConnection.BackColor = Color.WhiteSmoke;
            _CreateClientConnection.Enabled = false;
            _CreateClientConnection.Font = new Font("Segoe UI", 9.0f);
            _CreateClientConnection.ImageIndex = 3;
            _CreateClientConnection.ImageList = SmallerIcons;
            _CreateClientConnection.Location = new Point(239, 224);
            _CreateClientConnection.Name = "_CreateClientConnection";
            _CreateClientConnection.Size = new Size(183, 29);
            _CreateClientConnection.TabIndex = 6;
            _CreateClientConnection.Text = "Create Client Connection...";
            _CreateClientConnection.TextImageRelation = TextImageRelation.ImageBeforeText;
            _CreateClientConnection.UseVisualStyleBackColor = false;
            // 
            // ToolsMenuStrip
            // 
            ToolsMenuStrip.AllowMerge = false;
            ToolsMenuStrip.AutoSize = false;
            ToolsMenuStrip.BackColor = Color.Transparent;
            ToolsMenuStrip.Items.AddRange(new ToolStripItem[] { FileToolStripMenuItem, ToolsToolStripMenuItem, HelpToolStripMenuItem });
            ToolsMenuStrip.Location = new Point(0, 0);
            ToolsMenuStrip.Name = "ToolsMenuStrip";
            ToolsMenuStrip.ShowItemToolTips = true;
            ToolsMenuStrip.Size = new Size(434, 24);
            ToolsMenuStrip.TabIndex = 5;
            // 
            // FileToolStripMenuItem
            // 
            FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _NewRemoteAppadvancedToolStripMenuItem, ToolStripSeparator2, _ExitToolStripMenuItem });
            FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            FileToolStripMenuItem.Size = new Size(37, 20);
            FileToolStripMenuItem.Text = "File";
            // 
            // NewRemoteAppadvancedToolStripMenuItem
            // 
            _NewRemoteAppadvancedToolStripMenuItem.Name = "_NewRemoteAppadvancedToolStripMenuItem";
            _NewRemoteAppadvancedToolStripMenuItem.Size = new Size(235, 22);
            _NewRemoteAppadvancedToolStripMenuItem.Text = "New RemoteApp (advanced)...";
            // 
            // ToolStripSeparator2
            // 
            ToolStripSeparator2.Name = "ToolStripSeparator2";
            ToolStripSeparator2.Size = new Size(232, 6);
            // 
            // ExitToolStripMenuItem
            // 
            _ExitToolStripMenuItem.Name = "_ExitToolStripMenuItem";
            _ExitToolStripMenuItem.Size = new Size(235, 22);
            _ExitToolStripMenuItem.Text = "Exit";
            // 
            // ToolsToolStripMenuItem
            // 
            ToolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _HostOptionsToolStripMenuItem, ToolStripSeparator3, _RemoveUnusedFileTypeAssociationsToolStripMenuItem, _BackupAllRemoteAppsToolStripMenuItem });
            ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
            ToolsToolStripMenuItem.Size = new Size(46, 20);
            ToolsToolStripMenuItem.Text = "&Tools";
            // 
            // HostOptionsToolStripMenuItem
            // 
            _HostOptionsToolStripMenuItem.Name = "_HostOptionsToolStripMenuItem";
            _HostOptionsToolStripMenuItem.Size = new Size(280, 22);
            _HostOptionsToolStripMenuItem.Text = "Host Options...";
            // 
            // ToolStripSeparator3
            // 
            ToolStripSeparator3.Name = "ToolStripSeparator3";
            ToolStripSeparator3.Size = new Size(277, 6);
            // 
            // RemoveUnusedFileTypeAssociationsToolStripMenuItem
            // 
            _RemoveUnusedFileTypeAssociationsToolStripMenuItem.Name = "_RemoveUnusedFileTypeAssociationsToolStripMenuItem";
            _RemoveUnusedFileTypeAssociationsToolStripMenuItem.Size = new Size(280, 22);
            _RemoveUnusedFileTypeAssociationsToolStripMenuItem.Text = "Remove unused file type associations...";
            // 
            // BackupAllRemoteAppsToolStripMenuItem
            // 
            _BackupAllRemoteAppsToolStripMenuItem.Name = "_BackupAllRemoteAppsToolStripMenuItem";
            _BackupAllRemoteAppsToolStripMenuItem.Size = new Size(280, 22);
            _BackupAllRemoteAppsToolStripMenuItem.Text = "Backup all RemoteApps...";
            // 
            // HelpToolStripMenuItem
            // 
            HelpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _WebsiteToolStripMenuItem, ToolStripSeparator1, _AboutToolStripMenuItem });
            HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            HelpToolStripMenuItem.Size = new Size(44, 20);
            HelpToolStripMenuItem.Text = "Help";
            // 
            // WebsiteToolStripMenuItem
            // 
            _WebsiteToolStripMenuItem.Name = "_WebsiteToolStripMenuItem";
            _WebsiteToolStripMenuItem.Size = new Size(116, 22);
            _WebsiteToolStripMenuItem.Text = "Website";
            // 
            // ToolStripSeparator1
            // 
            ToolStripSeparator1.Name = "ToolStripSeparator1";
            ToolStripSeparator1.Size = new Size(113, 6);
            // 
            // AboutToolStripMenuItem
            // 
            _AboutToolStripMenuItem.Name = "_AboutToolStripMenuItem";
            _AboutToolStripMenuItem.Size = new Size(116, 22);
            _AboutToolStripMenuItem.Text = "About...";
            // 
            // PictureBox1
            // 
            PictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            PictureBox1.BorderStyle = BorderStyle.FixedSingle;
            PictureBox1.Location = new Point(-1, 27);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(435, 191);
            PictureBox1.TabIndex = 7;
            PictureBox1.TabStop = false;
            // 
            // BackupSaveFileDialog
            // 
            BackupSaveFileDialog.DefaultExt = "reg";
            BackupSaveFileDialog.Filter = "Windows Registry file|*.reg";
            // 
            // RemoteAppMainWindow
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(434, 261);
            Controls.Add(NoAppsLabel);
            Controls.Add(_CreateClientConnection);
            Controls.Add(_CreateButton);
            Controls.Add(_DeleteButton);
            Controls.Add(_EditButton);
            Controls.Add(ToolsMenuStrip);
            Controls.Add(_AppList);
            Controls.Add(PictureBox1);
            Font = new Font("Segoe UI", 9.0f);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = ToolsMenuStrip;
            MinimumSize = new Size(450, 300);
            Name = "RemoteAppMainWindow";
            SizeGripStyle = SizeGripStyle.Show;
            Text = "RemoteApp Tool";
            ToolsMenuStrip.ResumeLayout(false);
            ToolsMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
            Disposed += new EventHandler(RemoteAppMainWindow_Disposed);
            Load += new EventHandler(RemoteAppMainWindow_Load);
            ResumeLayout(false);
            PerformLayout();
        }

        private ListView _AppList;

        internal ListView AppList
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _AppList;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_AppList != null)
                {
                    _AppList.DoubleClick -= AppList_DoubleClick;
                    _AppList.SelectedIndexChanged -= AppList_SelectedIndexChanged;
                }

                _AppList = value;
                if (_AppList != null)
                {
                    _AppList.DoubleClick += AppList_DoubleClick;
                    _AppList.SelectedIndexChanged += AppList_SelectedIndexChanged;
                }
            }
        }

        internal ColumnHeader ShortName;
        internal ColumnHeader Title;
        internal ColumnHeader Path;
        internal ColumnHeader VPath;
        internal ColumnHeader RequiredCommandLine;
        internal ColumnHeader CommandLineSetting;
        internal ColumnHeader IconPath;
        internal ColumnHeader IconIndex;
        internal ColumnHeader SecurityDescriptor;
        internal ColumnHeader ShowInTSWA;
        internal ImageList SmallIcons;
        private Button _CreateButton;

        internal Button CreateButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _CreateButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CreateButton != null)
                {
                    _CreateButton.Click -= CreateButton_Click;
                }

                _CreateButton = value;
                if (_CreateButton != null)
                {
                    _CreateButton.Click += CreateButton_Click;
                }
            }
        }

        internal ImageList SmallerIcons;
        private Button _DeleteButton;

        internal Button DeleteButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _DeleteButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_DeleteButton != null)
                {
                    _DeleteButton.Click -= DeleteButton_Click;
                }

                _DeleteButton = value;
                if (_DeleteButton != null)
                {
                    _DeleteButton.Click += DeleteButton_Click;
                }
            }
        }

        private Button _EditButton;

        internal Button EditButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _EditButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_EditButton != null)
                {
                    _EditButton.Click -= EditButton_Click;
                }

                _EditButton = value;
                if (_EditButton != null)
                {
                    _EditButton.Click += EditButton_Click;
                }
            }
        }

        internal Label NoAppsLabel;
        private Button _CreateClientConnection;

        internal Button CreateClientConnection
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _CreateClientConnection;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CreateClientConnection != null)
                {
                    _CreateClientConnection.Click -= CreateClientConnection_Click;
                }

                _CreateClientConnection = value;
                if (_CreateClientConnection != null)
                {
                    _CreateClientConnection.Click += CreateClientConnection_Click;
                }
            }
        }

        internal MenuStrip ToolsMenuStrip;
        internal ToolStripMenuItem ToolsToolStripMenuItem;
        private ToolStripMenuItem _HostOptionsToolStripMenuItem;

        internal ToolStripMenuItem HostOptionsToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _HostOptionsToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_HostOptionsToolStripMenuItem != null)
                {
                    _HostOptionsToolStripMenuItem.Click -= HostOptionsToolStripMenuItem_Click;
                }

                _HostOptionsToolStripMenuItem = value;
                if (_HostOptionsToolStripMenuItem != null)
                {
                    _HostOptionsToolStripMenuItem.Click += HostOptionsToolStripMenuItem_Click;
                }
            }
        }

        internal ToolStripMenuItem FileToolStripMenuItem;
        private ToolStripMenuItem _ExitToolStripMenuItem;

        internal ToolStripMenuItem ExitToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ExitToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ExitToolStripMenuItem != null)
                {
                    _ExitToolStripMenuItem.Click -= ExitToolStripMenuItem_Click;
                }

                _ExitToolStripMenuItem = value;
                if (_ExitToolStripMenuItem != null)
                {
                    _ExitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
                }
            }
        }

        internal ToolStripMenuItem HelpToolStripMenuItem;
        private ToolStripMenuItem _AboutToolStripMenuItem;

        internal ToolStripMenuItem AboutToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _AboutToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_AboutToolStripMenuItem != null)
                {
                    _AboutToolStripMenuItem.Click -= AboutToolStripMenuItem_Click;
                }

                _AboutToolStripMenuItem = value;
                if (_AboutToolStripMenuItem != null)
                {
                    _AboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
                }
            }
        }

        private ToolStripMenuItem _WebsiteToolStripMenuItem;

        internal ToolStripMenuItem WebsiteToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _WebsiteToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_WebsiteToolStripMenuItem != null)
                {
                    _WebsiteToolStripMenuItem.Click -= WebsiteToolStripMenuItem_Click;
                }

                _WebsiteToolStripMenuItem = value;
                if (_WebsiteToolStripMenuItem != null)
                {
                    _WebsiteToolStripMenuItem.Click += WebsiteToolStripMenuItem_Click;
                }
            }
        }

        internal ToolStripSeparator ToolStripSeparator1;
        internal PictureBox PictureBox1;
        private ToolStripMenuItem _RemoveUnusedFileTypeAssociationsToolStripMenuItem;

        internal ToolStripMenuItem RemoveUnusedFileTypeAssociationsToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _RemoveUnusedFileTypeAssociationsToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_RemoveUnusedFileTypeAssociationsToolStripMenuItem != null)
                {
                    _RemoveUnusedFileTypeAssociationsToolStripMenuItem.Click -= RemoveUnusedFileTypeAssociationsToolStripMenuItem_Click;
                }

                _RemoveUnusedFileTypeAssociationsToolStripMenuItem = value;
                if (_RemoveUnusedFileTypeAssociationsToolStripMenuItem != null)
                {
                    _RemoveUnusedFileTypeAssociationsToolStripMenuItem.Click += RemoveUnusedFileTypeAssociationsToolStripMenuItem_Click;
                }
            }
        }

        private ToolStripMenuItem _NewRemoteAppadvancedToolStripMenuItem;

        internal ToolStripMenuItem NewRemoteAppadvancedToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _NewRemoteAppadvancedToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_NewRemoteAppadvancedToolStripMenuItem != null)
                {
                    _NewRemoteAppadvancedToolStripMenuItem.Click -= NewRemoteAppadvancedToolStripMenuItem_Click;
                }

                _NewRemoteAppadvancedToolStripMenuItem = value;
                if (_NewRemoteAppadvancedToolStripMenuItem != null)
                {
                    _NewRemoteAppadvancedToolStripMenuItem.Click += NewRemoteAppadvancedToolStripMenuItem_Click;
                }
            }
        }

        internal ToolStripSeparator ToolStripSeparator2;
        internal ToolStripSeparator ToolStripSeparator3;
        private ToolStripMenuItem _BackupAllRemoteAppsToolStripMenuItem;

        internal ToolStripMenuItem BackupAllRemoteAppsToolStripMenuItem
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _BackupAllRemoteAppsToolStripMenuItem;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BackupAllRemoteAppsToolStripMenuItem != null)
                {
                    _BackupAllRemoteAppsToolStripMenuItem.Click -= BackupAllRemoteAppsToolStripMenuItem_Click;
                }

                _BackupAllRemoteAppsToolStripMenuItem = value;
                if (_BackupAllRemoteAppsToolStripMenuItem != null)
                {
                    _BackupAllRemoteAppsToolStripMenuItem.Click += BackupAllRemoteAppsToolStripMenuItem_Click;
                }
            }
        }

        internal SaveFileDialog BackupSaveFileDialog;
    }
}