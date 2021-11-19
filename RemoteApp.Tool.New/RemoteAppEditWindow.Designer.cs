using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RemoteApp.Tool.New
{
    public partial class RemoteAppEditWindow : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteAppEditWindow));
            SmallerIcons = new ImageList(components);
            TSWAbox = new ComboBox();
            CommandLineOptionCombo = new ComboBox();
            CommandLineText = new TextBox();
            Label10 = new Label();
            Label8 = new Label();
            Label6 = new Label();
            _CancelEditButton = new Button();
            _CancelEditButton.Click += new EventHandler(CancelEditButton_Click);
            FileBrowserPath = new OpenFileDialog();
            FileBrowserIcon = new OpenFileDialog();
            FileBrowserVPath = new OpenFileDialog();
            _SaveButton = new Button();
            _SaveButton.Click += new EventHandler(SaveButton_Click);
            _FTAButton = new Button();
            _FTAButton.Click += new EventHandler(FTAButton_Click);
            GroupBox1 = new GroupBox();
            FullNameText = new TextBox();
            _ShortNameText = new TextBox();
            _ShortNameText.TextChanged += new EventHandler(ShortNameText_TextChanged);
            Label3 = new Label();
            Label12 = new Label();
            GroupBox2 = new GroupBox();
            _BrowseIconPath = new Button();
            _BrowseIconPath.Click += new EventHandler(BrowseIconPath_Click);
            _IconResetButton = new Button();
            _IconResetButton.Click += new EventHandler(IconResetButton_Click);
            IconPathText = new TextBox();
            Label11 = new Label();
            Label7 = new Label();
            PathText = new TextBox();
            _IconIndexText = new TextBox();
            _IconIndexText.TextChanged += new EventHandler(IconIndexText_TextChanged);
            Label4 = new Label();
            _BrowsePath = new Button();
            _BrowsePath.Click += new EventHandler(BrowsePath_Click);
            GroupBox3 = new GroupBox();
            Label1 = new Label();
            GroupBox1.SuspendLayout();
            GroupBox2.SuspendLayout();
            GroupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // SmallerIcons
            // 
            SmallerIcons.ImageStream = (ImageListStreamer)resources.GetObject("SmallerIcons.ImageStream");
            SmallerIcons.TransparentColor = Color.Transparent;
            SmallerIcons.Images.SetKeyName(0, "favorites_16x16.png");
            SmallerIcons.Images.SetKeyName(1, "folder_16x16.png");
            SmallerIcons.Images.SetKeyName(2, "dotdotdot.ico");
            SmallerIcons.Images.SetKeyName(3, "inside_icons_azure_marker_map_socialize_base.ico");
            SmallerIcons.Images.SetKeyName(4, "pictures (1).ico");
            SmallerIcons.Images.SetKeyName(5, "pictures.ico");
            SmallerIcons.Images.SetKeyName(6, "arrows_line_connector_with_draw.png");
            SmallerIcons.Images.SetKeyName(7, "doc_file_document_manager_paper_phone.ico");
            SmallerIcons.Images.SetKeyName(8, "cross.ico");
            // 
            // TSWAbox
            // 
            TSWAbox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TSWAbox.BackColor = Color.WhiteSmoke;
            TSWAbox.DropDownStyle = ComboBoxStyle.DropDownList;
            TSWAbox.FormattingEnabled = true;
            TSWAbox.Items.AddRange(new object[] { "No", "Yes" });
            TSWAbox.Location = new Point(318, 22);
            TSWAbox.Name = "TSWAbox";
            TSWAbox.Size = new Size(81, 23);
            TSWAbox.TabIndex = 3;
            // 
            // CommandLineOptionCombo
            // 
            CommandLineOptionCombo.BackColor = Color.WhiteSmoke;
            CommandLineOptionCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            CommandLineOptionCombo.FormattingEnabled = true;
            CommandLineOptionCombo.Items.AddRange(new object[] { "Disabled", "Optional", "Enforced" });
            CommandLineOptionCombo.Location = new Point(139, 22);
            CommandLineOptionCombo.Name = "CommandLineOptionCombo";
            CommandLineOptionCombo.Size = new Size(81, 23);
            CommandLineOptionCombo.TabIndex = 1;
            // 
            // CommandLineText
            // 
            CommandLineText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CommandLineText.Font = new Font("Segoe UI", 9.0f);
            CommandLineText.Location = new Point(163, 53);
            CommandLineText.Name = "CommandLineText";
            CommandLineText.Size = new Size(236, 23);
            CommandLineText.TabIndex = 5;
            // 
            // Label10
            // 
            Label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label10.AutoSize = true;
            Label10.Location = new Point(229, 25);
            Label10.Name = "Label10";
            Label10.Size = new Size(82, 15);
            Label10.TabIndex = 2;
            Label10.Text = "TSWebAccess:";
            // 
            // Label8
            // 
            Label8.AutoSize = true;
            Label8.Location = new Point(6, 25);
            Label8.Name = "Label8";
            Label8.Size = new Size(127, 15);
            Label8.TabIndex = 0;
            Label8.Text = "Command line option:";
            // 
            // Label6
            // 
            Label6.AutoSize = true;
            Label6.Font = new Font("Segoe UI", 9.0f);
            Label6.Location = new Point(6, 56);
            Label6.Name = "Label6";
            Label6.Size = new Size(151, 15);
            Label6.TabIndex = 4;
            Label6.Text = "Command line parameters:";
            // 
            // CancelEditButton
            // 
            _CancelEditButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _CancelEditButton.BackColor = Color.WhiteSmoke;
            _CancelEditButton.DialogResult = DialogResult.Cancel;
            _CancelEditButton.Font = new Font("Segoe UI", 9.0f);
            _CancelEditButton.ImageIndex = 8;
            _CancelEditButton.ImageList = SmallerIcons;
            _CancelEditButton.Location = new Point(269, 356);
            _CancelEditButton.Name = "_CancelEditButton";
            _CancelEditButton.Size = new Size(75, 29);
            _CancelEditButton.TabIndex = 3;
            _CancelEditButton.Text = "Cancel";
            _CancelEditButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _CancelEditButton.UseVisualStyleBackColor = false;
            // 
            // FileBrowserPath
            // 
            FileBrowserPath.Filter = "Programs|*.exe;*.com;*.cmd;*.bat|All files|*.*";
            FileBrowserPath.Title = "Browse...";
            // 
            // FileBrowserIcon
            // 
            FileBrowserIcon.Filter = "Icons|*.exe;*.dll;*.ico|All files|*.*";
            FileBrowserIcon.Title = "Browse...";
            // 
            // FileBrowserVPath
            // 
            FileBrowserVPath.Filter = "Programs|*.exe;*.com;*.cmd;*.bat|All files|*.*";
            FileBrowserVPath.Title = "Browse...";
            // 
            // SaveButton
            // 
            _SaveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _SaveButton.BackColor = Color.WhiteSmoke;
            _SaveButton.Font = new Font("Segoe UI", 9.0f);
            _SaveButton.ImageIndex = 0;
            _SaveButton.ImageList = SmallerIcons;
            _SaveButton.Location = new Point(350, 356);
            _SaveButton.Name = "_SaveButton";
            _SaveButton.Size = new Size(67, 29);
            _SaveButton.TabIndex = 4;
            _SaveButton.Text = "Save";
            _SaveButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _SaveButton.UseVisualStyleBackColor = false;
            // 
            // FTAButton
            // 
            _FTAButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _FTAButton.BackColor = Color.WhiteSmoke;
            _FTAButton.ImageAlign = ContentAlignment.MiddleRight;
            _FTAButton.ImageIndex = 7;
            _FTAButton.ImageList = SmallerIcons;
            _FTAButton.Location = new Point(163, 82);
            _FTAButton.Name = "_FTAButton";
            _FTAButton.Size = new Size(236, 29);
            _FTAButton.TabIndex = 7;
            _FTAButton.Text = "Configure...";
            _FTAButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _FTAButton.UseVisualStyleBackColor = false;
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox1.Controls.Add(FullNameText);
            GroupBox1.Controls.Add(_ShortNameText);
            GroupBox1.Controls.Add(Label3);
            GroupBox1.Controls.Add(Label12);
            GroupBox1.Location = new Point(12, 12);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(405, 91);
            GroupBox1.TabIndex = 0;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "Title";
            // 
            // FullNameText
            // 
            FullNameText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            FullNameText.Location = new Point(72, 51);
            FullNameText.Name = "FullNameText";
            FullNameText.Size = new Size(327, 23);
            FullNameText.TabIndex = 3;
            // 
            // ShortNameText
            // 
            _ShortNameText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _ShortNameText.Location = new Point(72, 22);
            _ShortNameText.Name = "_ShortNameText";
            _ShortNameText.Size = new Size(327, 23);
            _ShortNameText.TabIndex = 1;
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Location = new Point(6, 54);
            Label3.Name = "Label3";
            Label3.Size = new Size(62, 15);
            Label3.TabIndex = 2;
            Label3.Text = "Full name:";
            // 
            // Label12
            // 
            Label12.AutoSize = true;
            Label12.Location = new Point(6, 25);
            Label12.Name = "Label12";
            Label12.Size = new Size(42, 15);
            Label12.TabIndex = 0;
            Label12.Text = "Name:";
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox2.Controls.Add(_BrowseIconPath);
            GroupBox2.Controls.Add(_IconResetButton);
            GroupBox2.Controls.Add(IconPathText);
            GroupBox2.Controls.Add(Label11);
            GroupBox2.Controls.Add(Label7);
            GroupBox2.Controls.Add(PathText);
            GroupBox2.Controls.Add(_IconIndexText);
            GroupBox2.Controls.Add(Label4);
            GroupBox2.Controls.Add(_BrowsePath);
            GroupBox2.Location = new Point(12, 109);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(405, 116);
            GroupBox2.TabIndex = 1;
            GroupBox2.TabStop = false;
            GroupBox2.Text = "Files";
            // 
            // BrowseIconPath
            // 
            _BrowseIconPath.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _BrowseIconPath.BackColor = Color.WhiteSmoke;
            _BrowseIconPath.Font = new Font("Segoe UI", 9.0f);
            _BrowseIconPath.ImageIndex = 4;
            _BrowseIconPath.ImageList = SmallerIcons;
            _BrowseIconPath.Location = new Point(372, 49);
            _BrowseIconPath.Name = "_BrowseIconPath";
            _BrowseIconPath.Size = new Size(27, 27);
            _BrowseIconPath.TabIndex = 5;
            _BrowseIconPath.UseVisualStyleBackColor = false;
            // 
            // IconResetButton
            // 
            _IconResetButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _IconResetButton.AutoSize = true;
            _IconResetButton.BackColor = Color.WhiteSmoke;
            _IconResetButton.Font = new Font("Segoe UI", 9.0f);
            _IconResetButton.ImageIndex = 6;
            _IconResetButton.ImageList = SmallerIcons;
            _IconResetButton.Location = new Point(312, 80);
            _IconResetButton.Name = "_IconResetButton";
            _IconResetButton.Size = new Size(87, 29);
            _IconResetButton.TabIndex = 8;
            _IconResetButton.Text = "Reset icon";
            _IconResetButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _IconResetButton.UseVisualStyleBackColor = false;
            // 
            // IconPathText
            // 
            IconPathText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            IconPathText.Font = new Font("Segoe UI", 9.0f);
            IconPathText.Location = new Point(72, 51);
            IconPathText.Name = "IconPathText";
            IconPathText.Size = new Size(294, 23);
            IconPathText.TabIndex = 4;
            // 
            // Label11
            // 
            Label11.AutoSize = true;
            Label11.Font = new Font("Segoe UI", 9.0f);
            Label11.Location = new Point(6, 85);
            Label11.Name = "Label11";
            Label11.Size = new Size(65, 15);
            Label11.TabIndex = 6;
            Label11.Text = "Icon Index:";
            // 
            // Label7
            // 
            Label7.AutoSize = true;
            Label7.Font = new Font("Segoe UI", 9.0f);
            Label7.Location = new Point(6, 54);
            Label7.Name = "Label7";
            Label7.Size = new Size(60, 15);
            Label7.TabIndex = 3;
            Label7.Text = "Icon path:";
            // 
            // PathText
            // 
            PathText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            PathText.Location = new Point(72, 22);
            PathText.Name = "PathText";
            PathText.Size = new Size(294, 23);
            PathText.TabIndex = 1;
            // 
            // IconIndexText
            // 
            _IconIndexText.Font = new Font("Segoe UI", 9.0f);
            _IconIndexText.Location = new Point(72, 80);
            _IconIndexText.Name = "_IconIndexText";
            _IconIndexText.Size = new Size(60, 23);
            _IconIndexText.TabIndex = 7;
            _IconIndexText.Text = "0";
            // 
            // Label4
            // 
            Label4.AutoSize = true;
            Label4.Location = new Point(6, 25);
            Label4.Name = "Label4";
            Label4.Size = new Size(59, 15);
            Label4.TabIndex = 0;
            Label4.Text = "App path:";
            // 
            // BrowsePath
            // 
            _BrowsePath.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _BrowsePath.BackColor = Color.WhiteSmoke;
            _BrowsePath.ImageIndex = 1;
            _BrowsePath.ImageList = SmallerIcons;
            _BrowsePath.Location = new Point(372, 20);
            _BrowsePath.Name = "_BrowsePath";
            _BrowsePath.Size = new Size(27, 27);
            _BrowsePath.TabIndex = 2;
            _BrowsePath.UseVisualStyleBackColor = false;
            // 
            // GroupBox3
            // 
            GroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox3.Controls.Add(Label8);
            GroupBox3.Controls.Add(Label1);
            GroupBox3.Controls.Add(Label6);
            GroupBox3.Controls.Add(Label10);
            GroupBox3.Controls.Add(_FTAButton);
            GroupBox3.Controls.Add(CommandLineText);
            GroupBox3.Controls.Add(CommandLineOptionCombo);
            GroupBox3.Controls.Add(TSWAbox);
            GroupBox3.Location = new Point(12, 231);
            GroupBox3.Name = "GroupBox3";
            GroupBox3.Size = new Size(405, 119);
            GroupBox3.TabIndex = 2;
            GroupBox3.TabStop = false;
            GroupBox3.Text = "Options";
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Font = new Font("Segoe UI", 9.0f);
            Label1.Location = new Point(6, 87);
            Label1.Name = "Label1";
            Label1.Size = new Size(121, 15);
            Label1.TabIndex = 6;
            Label1.Text = "File type associations:";
            // 
            // RemoteAppEditWindow
            // 
            AcceptButton = _SaveButton;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = _CancelEditButton;
            ClientSize = new Size(429, 391);
            Controls.Add(GroupBox3);
            Controls.Add(GroupBox2);
            Controls.Add(GroupBox1);
            Controls.Add(_CancelEditButton);
            Controls.Add(_SaveButton);
            Font = new Font("Segoe UI", 9.0f);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1500, 430);
            MinimizeBox = false;
            MinimumSize = new Size(445, 430);
            Name = "RemoteAppEditWindow";
            StartPosition = FormStartPosition.CenterParent;
            Text = "RemoteAppEditWindow";
            GroupBox1.ResumeLayout(false);
            GroupBox1.PerformLayout();
            GroupBox2.ResumeLayout(false);
            GroupBox2.PerformLayout();
            GroupBox3.ResumeLayout(false);
            GroupBox3.PerformLayout();
            ResumeLayout(false);
        }

        internal ComboBox TSWAbox;
        internal ComboBox CommandLineOptionCombo;
        internal TextBox CommandLineText;
        internal Label Label10;
        internal Label Label8;
        internal Label Label6;
        internal ImageList SmallerIcons;
        private Button _SaveButton;

        internal Button SaveButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _SaveButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_SaveButton != null)
                {
                    _SaveButton.Click -= SaveButton_Click;
                }

                _SaveButton = value;
                if (_SaveButton != null)
                {
                    _SaveButton.Click += SaveButton_Click;
                }
            }
        }

        private Button _CancelEditButton;

        internal Button CancelEditButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _CancelEditButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CancelEditButton != null)
                {
                    _CancelEditButton.Click -= CancelEditButton_Click;
                }

                _CancelEditButton = value;
                if (_CancelEditButton != null)
                {
                    _CancelEditButton.Click += CancelEditButton_Click;
                }
            }
        }

        internal OpenFileDialog FileBrowserPath;
        internal OpenFileDialog FileBrowserIcon;
        internal OpenFileDialog FileBrowserVPath;
        private Button _FTAButton;

        internal Button FTAButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _FTAButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_FTAButton != null)
                {
                    _FTAButton.Click -= FTAButton_Click;
                }

                _FTAButton = value;
                if (_FTAButton != null)
                {
                    _FTAButton.Click += FTAButton_Click;
                }
            }
        }

        internal GroupBox GroupBox1;
        internal TextBox FullNameText;
        private TextBox _ShortNameText;

        internal TextBox ShortNameText
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _ShortNameText;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_ShortNameText != null)
                {
                    _ShortNameText.TextChanged -= ShortNameText_TextChanged;
                }

                _ShortNameText = value;
                if (_ShortNameText != null)
                {
                    _ShortNameText.TextChanged += ShortNameText_TextChanged;
                }
            }
        }

        internal Label Label3;
        internal Label Label12;
        internal GroupBox GroupBox2;
        private Button _BrowseIconPath;

        internal Button BrowseIconPath
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _BrowseIconPath;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BrowseIconPath != null)
                {
                    _BrowseIconPath.Click -= BrowseIconPath_Click;
                }

                _BrowseIconPath = value;
                if (_BrowseIconPath != null)
                {
                    _BrowseIconPath.Click += BrowseIconPath_Click;
                }
            }
        }

        private Button _IconResetButton;

        internal Button IconResetButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _IconResetButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_IconResetButton != null)
                {
                    _IconResetButton.Click -= IconResetButton_Click;
                }

                _IconResetButton = value;
                if (_IconResetButton != null)
                {
                    _IconResetButton.Click += IconResetButton_Click;
                }
            }
        }

        internal TextBox IconPathText;
        internal Label Label11;
        internal Label Label7;
        internal TextBox PathText;
        private TextBox _IconIndexText;

        internal TextBox IconIndexText
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _IconIndexText;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_IconIndexText != null)
                {
                    _IconIndexText.TextChanged -= IconIndexText_TextChanged;
                }

                _IconIndexText = value;
                if (_IconIndexText != null)
                {
                    _IconIndexText.TextChanged += IconIndexText_TextChanged;
                }
            }
        }

        internal Label Label4;
        private Button _BrowsePath;

        internal Button BrowsePath
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _BrowsePath;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BrowsePath != null)
                {
                    _BrowsePath.Click -= BrowsePath_Click;
                }

                _BrowsePath = value;
                if (_BrowsePath != null)
                {
                    _BrowsePath.Click += BrowsePath_Click;
                }
            }
        }

        internal GroupBox GroupBox3;
        internal Label Label1;
    }
}