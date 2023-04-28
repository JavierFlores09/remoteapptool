using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RemoteApp.Tool
{
    public partial class RemoteAppFileTypeAssociation : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteAppFileTypeAssociation));
            _FTAListView = new ListView();
            _FTAListView.DoubleClick += new EventHandler(FTAListView_DoubleClick);
            _FTAListView.SelectedIndexChanged += new EventHandler(FTAListView_SelectedIndexChanged);
            FileExtension = new ColumnHeader();
            IconPath = new ColumnHeader();
            IconIndex = new ColumnHeader();
            Associated = new ColumnHeader();
            _CreateButton = new Button();
            _CreateButton.Click += new EventHandler(CreateButton_Click);
            SmallerIcons = new ImageList(components);
            _DeleteButton = new Button();
            _DeleteButton.Click += new EventHandler(DeleteButton_Click);
            _EditButton = new Button();
            _EditButton.Click += new EventHandler(EditButton_Click);
            _CloseButton = new Button();
            _CloseButton.Click += new EventHandler(CloseButton_Click);
            _SetAssociationButton = new Button();
            _SetAssociationButton.Click += new EventHandler(SetAssociationButton_Click);
            SmallerIcons2 = new ImageList(components);
            SuspendLayout();
            // 
            // FTAListView
            // 
            _FTAListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            _FTAListView.BorderStyle = BorderStyle.FixedSingle;
            _FTAListView.Columns.AddRange(new ColumnHeader[] { FileExtension, IconPath, IconIndex, Associated });
            _FTAListView.FullRowSelect = true;
            _FTAListView.GridLines = true;
            _FTAListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            _FTAListView.HideSelection = false;
            _FTAListView.Location = new Point(-1, -1);
            _FTAListView.MultiSelect = false;
            _FTAListView.Name = "_FTAListView";
            _FTAListView.Size = new Size(496, 164);
            _FTAListView.Sorting = SortOrder.Ascending;
            _FTAListView.TabIndex = 0;
            _FTAListView.UseCompatibleStateImageBehavior = false;
            _FTAListView.View = View.Details;
            // 
            // FileExtension
            // 
            FileExtension.Text = "Extension";
            FileExtension.Width = 64;
            // 
            // IconPath
            // 
            IconPath.Text = "Icon Path";
            IconPath.Width = 305;
            // 
            // IconIndex
            // 
            IconIndex.Text = "Index";
            IconIndex.Width = 51;
            // 
            // Associated
            // 
            Associated.Text = "Association";
            Associated.Width = 76;
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
            _CreateButton.ImageIndex = 1;
            _CreateButton.ImageList = SmallerIcons;
            _CreateButton.Location = new Point(12, 169);
            _CreateButton.Name = "_CreateButton";
            _CreateButton.Size = new Size(29, 29);
            _CreateButton.TabIndex = 1;
            _CreateButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _CreateButton.UseVisualStyleBackColor = false;
            // 
            // SmallerIcons
            // 
            SmallerIcons.ImageStream = (ImageListStreamer)resources.GetObject("SmallerIcons.ImageStream");
            SmallerIcons.TransparentColor = Color.Transparent;
            SmallerIcons.Images.SetKeyName(0, "properties.ico");
            SmallerIcons.Images.SetKeyName(1, "plus.ico");
            SmallerIcons.Images.SetKeyName(2, "minus.ico");
            SmallerIcons.Images.SetKeyName(3, "tick.ico");
            SmallerIcons.Images.SetKeyName(4, "flag2-add.ico");
            SmallerIcons.Images.SetKeyName(5, "settings-16.ico");
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
            _DeleteButton.ImageIndex = 2;
            _DeleteButton.ImageList = SmallerIcons;
            _DeleteButton.Location = new Point(48, 169);
            _DeleteButton.Name = "_DeleteButton";
            _DeleteButton.Size = new Size(29, 29);
            _DeleteButton.TabIndex = 2;
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
            _EditButton.ImageIndex = 0;
            _EditButton.ImageList = SmallerIcons;
            _EditButton.Location = new Point(84, 169);
            _EditButton.Name = "_EditButton";
            _EditButton.Size = new Size(29, 29);
            _EditButton.TabIndex = 3;
            _EditButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _EditButton.UseVisualStyleBackColor = false;
            // 
            // CloseButton
            // 
            _CloseButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _CloseButton.BackColor = Color.WhiteSmoke;
            _CloseButton.DialogResult = DialogResult.OK;
            _CloseButton.Font = new Font("Segoe UI", 9.0f);
            _CloseButton.ImageIndex = 3;
            _CloseButton.ImageList = SmallerIcons;
            _CloseButton.Location = new Point(415, 169);
            _CloseButton.Name = "_CloseButton";
            _CloseButton.Size = new Size(67, 29);
            _CloseButton.TabIndex = 6;
            _CloseButton.Text = "OK";
            _CloseButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _CloseButton.UseVisualStyleBackColor = false;
            // 
            // SetAssociationButton
            // 
            _SetAssociationButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _SetAssociationButton.AutoSize = true;
            _SetAssociationButton.BackColor = Color.Transparent;
            _SetAssociationButton.Enabled = false;
            _SetAssociationButton.FlatAppearance.BorderSize = 0;
            _SetAssociationButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            _SetAssociationButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            _SetAssociationButton.FlatStyle = FlatStyle.Flat;
            _SetAssociationButton.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            _SetAssociationButton.ForeColor = SystemColors.ControlText;
            _SetAssociationButton.ImageIndex = 4;
            _SetAssociationButton.ImageList = SmallerIcons;
            _SetAssociationButton.Location = new Point(119, 169);
            _SetAssociationButton.Name = "_SetAssociationButton";
            _SetAssociationButton.Size = new Size(29, 29);
            _SetAssociationButton.TabIndex = 4;
            _SetAssociationButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _SetAssociationButton.UseVisualStyleBackColor = false;
            // 
            // SmallerIcons2
            // 
            SmallerIcons2.ImageStream = (ImageListStreamer)resources.GetObject("SmallerIcons2.ImageStream");
            SmallerIcons2.TransparentColor = Color.Transparent;
            SmallerIcons2.Images.SetKeyName(0, "cross.ico");
            // 
            // RemoteAppFileTypeAssociation
            // 
            AcceptButton = _CloseButton;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(494, 208);
            Controls.Add(_SetAssociationButton);
            Controls.Add(_CloseButton);
            Controls.Add(_CreateButton);
            Controls.Add(_DeleteButton);
            Controls.Add(_EditButton);
            Controls.Add(_FTAListView);
            Font = new Font("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(510, 247);
            Name = "RemoteAppFileTypeAssociation";
            StartPosition = FormStartPosition.CenterParent;
            Text = "File Type Associations";
            ResumeLayout(false);
            PerformLayout();
        }

        private ListView _FTAListView;

        internal ListView FTAListView
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _FTAListView;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_FTAListView != null)
                {
                    _FTAListView.DoubleClick -= FTAListView_DoubleClick;
                    _FTAListView.SelectedIndexChanged -= FTAListView_SelectedIndexChanged;
                }

                _FTAListView = value;
                if (_FTAListView != null)
                {
                    _FTAListView.DoubleClick += FTAListView_DoubleClick;
                    _FTAListView.SelectedIndexChanged += FTAListView_SelectedIndexChanged;
                }
            }
        }

        internal ColumnHeader FileExtension;
        internal ColumnHeader IconPath;
        internal ColumnHeader IconIndex;
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

        private Button _CloseButton;

        internal Button CloseButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _CloseButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_CloseButton != null)
                {
                    _CloseButton.Click -= CloseButton_Click;
                }

                _CloseButton = value;
                if (_CloseButton != null)
                {
                    _CloseButton.Click += CloseButton_Click;
                }
            }
        }

        internal ColumnHeader Associated;
        private Button _SetAssociationButton;

        internal Button SetAssociationButton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _SetAssociationButton;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_SetAssociationButton != null)
                {
                    _SetAssociationButton.Click -= SetAssociationButton_Click;
                }

                _SetAssociationButton = value;
                if (_SetAssociationButton != null)
                {
                    _SetAssociationButton.Click += SetAssociationButton_Click;
                }
            }
        }

        internal ImageList SmallerIcons;
        internal ImageList SmallerIcons2;
    }
}