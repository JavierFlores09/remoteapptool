using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RemoteApp.Tool
{
    public partial class RemoteAppIconPicker : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteAppIconPicker));
            _IconList = new ListView();
            _IconList.SelectedIndexChanged += new EventHandler(IconList_SelectedIndexChanged);
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
            _BrowseButton = new Button();
            _BrowseButton.Click += new EventHandler(BrowseButton_Click);
            SmallerIcons = new ImageList(components);
            IconPathTextBox = new TextBox();
            _IconIndexTextBox = new TextBox();
            _IconIndexTextBox.TextChanged += new EventHandler(IconIndexTextBox_TextChanged);
            FileBrowserIcon = new OpenFileDialog();
            CancelEditButton = new Button();
            OKButton = new Button();
            FileTypeLabel = new Label();
            _FileTypeTextBox = new TextBox();
            _FileTypeTextBox.TextChanged += new EventHandler(FileTypeTextBox_TextChanged);
            IconIndexLabel = new Label();
            SuspendLayout();
            // 
            // IconList
            // 
            _IconList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _IconList.Columns.AddRange(new ColumnHeader[] { ShortName, Title, Path, VPath, RequiredCommandLine, CommandLineSetting, IconPath, IconIndex, SecurityDescriptor, ShowInTSWA });
            _IconList.GridLines = true;
            _IconList.HideSelection = false;
            _IconList.LargeImageList = SmallIcons;
            _IconList.Location = new Point(16, 41);
            _IconList.MultiSelect = false;
            _IconList.Name = "_IconList";
            _IconList.Size = new Size(471, 143);
            _IconList.SmallImageList = SmallIcons;
            _IconList.TabIndex = 4;
            _IconList.UseCompatibleStateImageBehavior = false;
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
            // BrowseButton
            // 
            _BrowseButton.BackColor = Color.WhiteSmoke;
            _BrowseButton.Font = new Font("Segoe UI", 9.0f);
            _BrowseButton.ImageIndex = 0;
            _BrowseButton.ImageList = SmallerIcons;
            _BrowseButton.Location = new Point(14, 8);
            _BrowseButton.Name = "_BrowseButton";
            _BrowseButton.Size = new Size(82, 29);
            _BrowseButton.TabIndex = 0;
            _BrowseButton.Text = "Browse...";
            _BrowseButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _BrowseButton.UseVisualStyleBackColor = false;
            // 
            // SmallerIcons
            // 
            SmallerIcons.ImageStream = (ImageListStreamer)resources.GetObject("SmallerIcons.ImageStream");
            SmallerIcons.TransparentColor = Color.Transparent;
            SmallerIcons.Images.SetKeyName(0, "folder_16x16.png");
            SmallerIcons.Images.SetKeyName(1, "tick.ico");
            SmallerIcons.Images.SetKeyName(2, "cross.ico");
            SmallerIcons.Images.SetKeyName(3, "settings-16.ico");
            // 
            // IconPathTextBox
            // 
            IconPathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            IconPathTextBox.Location = new Point(103, 12);
            IconPathTextBox.Name = "IconPathTextBox";
            IconPathTextBox.ReadOnly = true;
            IconPathTextBox.Size = new Size(267, 23);
            IconPathTextBox.TabIndex = 1;
            // 
            // IconIndexTextBox
            // 
            _IconIndexTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _IconIndexTextBox.Location = new Point(420, 12);
            _IconIndexTextBox.Name = "_IconIndexTextBox";
            _IconIndexTextBox.ReadOnly = true;
            _IconIndexTextBox.Size = new Size(67, 23);
            _IconIndexTextBox.TabIndex = 3;
            _IconIndexTextBox.Visible = false;
            // 
            // FileBrowserIcon
            // 
            FileBrowserIcon.Filter = "Icons|*.exe;*.dll;*.ico|All files|*.*";
            FileBrowserIcon.Title = "Browse...";
            // 
            // CancelEditButton
            // 
            CancelEditButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CancelEditButton.BackColor = Color.WhiteSmoke;
            CancelEditButton.DialogResult = DialogResult.Cancel;
            CancelEditButton.Font = new Font("Segoe UI", 9.0f);
            CancelEditButton.ImageIndex = 2;
            CancelEditButton.ImageList = SmallerIcons;
            CancelEditButton.Location = new Point(339, 190);
            CancelEditButton.Name = "CancelEditButton";
            CancelEditButton.Size = new Size(75, 29);
            CancelEditButton.TabIndex = 7;
            CancelEditButton.Text = "Cancel";
            CancelEditButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            CancelEditButton.UseVisualStyleBackColor = false;
            // 
            // OKButton
            // 
            OKButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            OKButton.BackColor = Color.WhiteSmoke;
            OKButton.DialogResult = DialogResult.OK;
            OKButton.Font = new Font("Segoe UI", 9.0f);
            OKButton.ImageIndex = 1;
            OKButton.ImageList = SmallerIcons;
            OKButton.Location = new Point(420, 190);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(67, 29);
            OKButton.TabIndex = 8;
            OKButton.Text = "OK";
            OKButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            OKButton.UseVisualStyleBackColor = false;
            // 
            // FileTypeLabel
            // 
            FileTypeLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            FileTypeLabel.AutoSize = true;
            FileTypeLabel.Location = new Point(12, 197);
            FileTypeLabel.Name = "FileTypeLabel";
            FileTypeLabel.Size = new Size(66, 15);
            FileTypeLabel.TabIndex = 5;
            FileTypeLabel.Text = "File type:   .";
            FileTypeLabel.Visible = false;
            // 
            // FileTypeTextBox
            // 
            _FileTypeTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _FileTypeTextBox.Location = new Point(76, 194);
            _FileTypeTextBox.Name = "_FileTypeTextBox";
            _FileTypeTextBox.Size = new Size(80, 23);
            _FileTypeTextBox.TabIndex = 6;
            _FileTypeTextBox.Text = "xyz";
            _FileTypeTextBox.Visible = false;
            // 
            // IconIndexLabel
            // 
            IconIndexLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            IconIndexLabel.AutoSize = true;
            IconIndexLabel.Font = new Font("Segoe UI", 9.0f);
            IconIndexLabel.Location = new Point(376, 15);
            IconIndexLabel.Name = "IconIndexLabel";
            IconIndexLabel.Size = new Size(39, 15);
            IconIndexLabel.TabIndex = 2;
            IconIndexLabel.Text = "Index:";
            IconIndexLabel.Visible = false;
            // 
            // RemoteAppIconPicker
            // 
            AcceptButton = OKButton;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = CancelEditButton;
            ClientSize = new Size(499, 231);
            Controls.Add(IconIndexLabel);
            Controls.Add(CancelEditButton);
            Controls.Add(OKButton);
            Controls.Add(_IconIndexTextBox);
            Controls.Add(IconPathTextBox);
            Controls.Add(_BrowseButton);
            Controls.Add(_IconList);
            Controls.Add(_FileTypeTextBox);
            Controls.Add(FileTypeLabel);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(515, 270);
            Name = "RemoteAppIconPicker";
            StartPosition = FormStartPosition.CenterParent;
            Text = "RemoteAppIconPicker";
            ResumeLayout(false);
            PerformLayout();
        }

        private ListView _IconList;

        internal ListView IconList
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _IconList;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_IconList != null)
                {
                    _IconList.SelectedIndexChanged -= IconList_SelectedIndexChanged;
                }

                _IconList = value;
                if (_IconList != null)
                {
                    _IconList.SelectedIndexChanged += IconList_SelectedIndexChanged;
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
        private Button _BrowseButton;

        internal Button BrowseButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _BrowseButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_BrowseButton != null)
                {
                    _BrowseButton.Click -= BrowseButton_Click;
                }

                _BrowseButton = value;
                if (_BrowseButton != null)
                {
                    _BrowseButton.Click += BrowseButton_Click;
                }
            }
        }

        internal TextBox IconPathTextBox;
        private TextBox _IconIndexTextBox;

        internal TextBox IconIndexTextBox
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _IconIndexTextBox;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_IconIndexTextBox != null)
                {
                    _IconIndexTextBox.TextChanged -= IconIndexTextBox_TextChanged;
                }

                _IconIndexTextBox = value;
                if (_IconIndexTextBox != null)
                {
                    _IconIndexTextBox.TextChanged += IconIndexTextBox_TextChanged;
                }
            }
        }

        internal OpenFileDialog FileBrowserIcon;
        internal Button CancelEditButton;
        internal Button OKButton;
        internal Label FileTypeLabel;
        private TextBox _FileTypeTextBox;

        internal TextBox FileTypeTextBox
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _FileTypeTextBox;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_FileTypeTextBox != null)
                {
                    _FileTypeTextBox.TextChanged -= FileTypeTextBox_TextChanged;
                }

                _FileTypeTextBox = value;
                if (_FileTypeTextBox != null)
                {
                    _FileTypeTextBox.TextChanged += FileTypeTextBox_TextChanged;
                }
            }
        }

        internal ImageList SmallerIcons;
        internal Label IconIndexLabel;
        internal ImageList SmallIcons;
    }
}