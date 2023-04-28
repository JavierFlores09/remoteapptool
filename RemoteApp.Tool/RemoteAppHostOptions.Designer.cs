using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RemoteApp.Tool
{
    public partial class RemoteAppHostOptions : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteAppHostOptions));
            _TimeoutDisconnectedCheckBox = new CheckBox();
            _TimeoutDisconnectedCheckBox.CheckedChanged += new EventHandler(TimeoutDisconnectedCheckBox_CheckedChanged);
            DisableAllowListCheckBox = new CheckBox();
            _DisconnectTimeTextBox = new TextBox();
            _DisconnectTimeTextBox.TextChanged += new EventHandler(DisconnectTimeTextBox_TextChanged);
            Label1 = new Label();
            _TimeoutIdleCheckBox = new CheckBox();
            _TimeoutIdleCheckBox.CheckedChanged += new EventHandler(TimeoutIdleCheckBox_CheckedChanged);
            _IdleTimeTextBox = new TextBox();
            _IdleTimeTextBox.TextChanged += new EventHandler(IdleTimeTextBox_TextChanged);
            Label2 = new Label();
            LogoffWhenTimoutCheckBox = new CheckBox();
            _SaveButton = new Button();
            _SaveButton.Click += new EventHandler(SaveButton_Click);
            SmallerIcons = new ImageList(components);
            Label3 = new Label();
            _CancelEditButton = new Button();
            _CancelEditButton.Click += new EventHandler(CancelEditButton_Click);
            AllowUnlistedRemoteProgramsCheckBox = new CheckBox();
            SuspendLayout();
            // 
            // TimeoutDisconnectedCheckBox
            // 
            _TimeoutDisconnectedCheckBox.AutoSize = true;
            _TimeoutDisconnectedCheckBox.Location = new Point(14, 64);
            _TimeoutDisconnectedCheckBox.Name = "_TimeoutDisconnectedCheckBox";
            _TimeoutDisconnectedCheckBox.Size = new Size(211, 19);
            _TimeoutDisconnectedCheckBox.TabIndex = 2;
            _TimeoutDisconnectedCheckBox.Text = "Timeout for disconnected sessions:";
            _TimeoutDisconnectedCheckBox.UseVisualStyleBackColor = true;
            // 
            // DisableAllowListCheckBox
            // 
            DisableAllowListCheckBox.AutoSize = true;
            DisableAllowListCheckBox.Location = new Point(14, 14);
            DisableAllowListCheckBox.Name = "DisableAllowListCheckBox";
            DisableAllowListCheckBox.Size = new Size(137, 19);
            DisableAllowListCheckBox.TabIndex = 0;
            DisableAllowListCheckBox.Text = "Disable AppAllowList";
            DisableAllowListCheckBox.UseVisualStyleBackColor = true;
            // 
            // DisconnectTimeTextBox
            // 
            _DisconnectTimeTextBox.Location = new Point(245, 62);
            _DisconnectTimeTextBox.MaxLength = 8;
            _DisconnectTimeTextBox.Name = "_DisconnectTimeTextBox";
            _DisconnectTimeTextBox.Size = new Size(83, 23);
            _DisconnectTimeTextBox.TabIndex = 3;
            _DisconnectTimeTextBox.Text = "0";
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.Location = new Point(336, 66);
            Label1.Name = "Label1";
            Label1.Size = new Size(50, 15);
            Label1.TabIndex = 4;
            Label1.Text = "seconds";
            // 
            // TimeoutIdleCheckBox
            // 
            _TimeoutIdleCheckBox.AutoSize = true;
            _TimeoutIdleCheckBox.Location = new Point(14, 91);
            _TimeoutIdleCheckBox.Name = "_TimeoutIdleCheckBox";
            _TimeoutIdleCheckBox.Size = new Size(159, 19);
            _TimeoutIdleCheckBox.TabIndex = 5;
            _TimeoutIdleCheckBox.Text = "Timeout for idle sessions:";
            _TimeoutIdleCheckBox.UseVisualStyleBackColor = true;
            // 
            // IdleTimeTextBox
            // 
            _IdleTimeTextBox.Location = new Point(189, 89);
            _IdleTimeTextBox.MaxLength = 8;
            _IdleTimeTextBox.Name = "_IdleTimeTextBox";
            _IdleTimeTextBox.Size = new Size(83, 23);
            _IdleTimeTextBox.TabIndex = 6;
            _IdleTimeTextBox.Text = "0";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Location = new Point(280, 91);
            Label2.Name = "Label2";
            Label2.Size = new Size(50, 15);
            Label2.TabIndex = 7;
            Label2.Text = "seconds";
            // 
            // LogoffWhenTimoutCheckBox
            // 
            LogoffWhenTimoutCheckBox.AutoSize = true;
            LogoffWhenTimoutCheckBox.Location = new Point(14, 117);
            LogoffWhenTimoutCheckBox.Name = "LogoffWhenTimoutCheckBox";
            LogoffWhenTimoutCheckBox.Size = new Size(262, 19);
            LogoffWhenTimoutCheckBox.TabIndex = 8;
            LogoffWhenTimoutCheckBox.Text = "Logoff sessions when time limits are reached";
            LogoffWhenTimoutCheckBox.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            _SaveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _SaveButton.BackColor = Color.WhiteSmoke;
            _SaveButton.FlatAppearance.BorderSize = 0;
            _SaveButton.ImageIndex = 0;
            _SaveButton.ImageList = SmallerIcons;
            _SaveButton.Location = new Point(334, 190);
            _SaveButton.Name = "_SaveButton";
            _SaveButton.Size = new Size(67, 29);
            _SaveButton.TabIndex = 11;
            _SaveButton.Text = "Save";
            _SaveButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _SaveButton.UseVisualStyleBackColor = false;
            // 
            // SmallerIcons
            // 
            SmallerIcons.ImageStream = (ImageListStreamer)resources.GetObject("SmallerIcons.ImageStream");
            SmallerIcons.TransparentColor = Color.Transparent;
            SmallerIcons.Images.SetKeyName(0, "favorites_16x16.png");
            SmallerIcons.Images.SetKeyName(1, "cross.ico");
            // 
            // Label3
            // 
            Label3.Font = new Font("Segoe UI", 9.0f);
            Label3.ForeColor = Color.Maroon;
            Label3.Location = new Point(11, 145);
            Label3.Name = "Label3";
            Label3.Size = new Size(385, 33);
            Label3.TabIndex = 9;
            Label3.Text = "Note: Settings here will be overridden by local policy and group policy. Some set" + "tings require a reboot.";
            // 
            // CancelEditButton
            // 
            _CancelEditButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _CancelEditButton.BackColor = Color.WhiteSmoke;
            _CancelEditButton.DialogResult = DialogResult.Cancel;
            _CancelEditButton.ImageIndex = 1;
            _CancelEditButton.ImageList = SmallerIcons;
            _CancelEditButton.Location = new Point(253, 190);
            _CancelEditButton.Name = "_CancelEditButton";
            _CancelEditButton.Size = new Size(75, 29);
            _CancelEditButton.TabIndex = 10;
            _CancelEditButton.Text = "Cancel";
            _CancelEditButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            _CancelEditButton.UseVisualStyleBackColor = false;
            // 
            // AllowUnlistedRemoteProgramsCheckBox
            // 
            AllowUnlistedRemoteProgramsCheckBox.AutoSize = true;
            AllowUnlistedRemoteProgramsCheckBox.Location = new Point(14, 39);
            AllowUnlistedRemoteProgramsCheckBox.Name = "AllowUnlistedRemoteProgramsCheckBox";
            AllowUnlistedRemoteProgramsCheckBox.Size = new Size(200, 19);
            AllowUnlistedRemoteProgramsCheckBox.TabIndex = 1;
            AllowUnlistedRemoteProgramsCheckBox.Text = "Allow Unlisted Remote Programs";
            AllowUnlistedRemoteProgramsCheckBox.UseVisualStyleBackColor = true;
            // 
            // RemoteAppHostOptions
            // 
            AcceptButton = _SaveButton;
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = _CancelEditButton;
            ClientSize = new Size(413, 231);
            Controls.Add(_SaveButton);
            Controls.Add(_CancelEditButton);
            Controls.Add(Label3);
            Controls.Add(Label2);
            Controls.Add(Label1);
            Controls.Add(_IdleTimeTextBox);
            Controls.Add(_DisconnectTimeTextBox);
            Controls.Add(AllowUnlistedRemoteProgramsCheckBox);
            Controls.Add(DisableAllowListCheckBox);
            Controls.Add(LogoffWhenTimoutCheckBox);
            Controls.Add(_TimeoutIdleCheckBox);
            Controls.Add(_TimeoutDisconnectedCheckBox);
            Font = new Font("Segoe UI", 9.0f);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RemoteAppHostOptions";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Host Options";
            ResumeLayout(false);
            PerformLayout();
        }

        private CheckBox _TimeoutDisconnectedCheckBox;

        internal CheckBox TimeoutDisconnectedCheckBox
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _TimeoutDisconnectedCheckBox;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_TimeoutDisconnectedCheckBox != null)
                {
                    _TimeoutDisconnectedCheckBox.CheckedChanged -= TimeoutDisconnectedCheckBox_CheckedChanged;
                }

                _TimeoutDisconnectedCheckBox = value;
                if (_TimeoutDisconnectedCheckBox != null)
                {
                    _TimeoutDisconnectedCheckBox.CheckedChanged += TimeoutDisconnectedCheckBox_CheckedChanged;
                }
            }
        }

        internal CheckBox DisableAllowListCheckBox;
        private TextBox _DisconnectTimeTextBox;

        internal TextBox DisconnectTimeTextBox
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _DisconnectTimeTextBox;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_DisconnectTimeTextBox != null)
                {
                    _DisconnectTimeTextBox.TextChanged -= DisconnectTimeTextBox_TextChanged;
                }

                _DisconnectTimeTextBox = value;
                if (_DisconnectTimeTextBox != null)
                {
                    _DisconnectTimeTextBox.TextChanged += DisconnectTimeTextBox_TextChanged;
                }
            }
        }

        internal Label Label1;
        private CheckBox _TimeoutIdleCheckBox;

        internal CheckBox TimeoutIdleCheckBox
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _TimeoutIdleCheckBox;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_TimeoutIdleCheckBox != null)
                {
                    _TimeoutIdleCheckBox.CheckedChanged -= TimeoutIdleCheckBox_CheckedChanged;
                }

                _TimeoutIdleCheckBox = value;
                if (_TimeoutIdleCheckBox != null)
                {
                    _TimeoutIdleCheckBox.CheckedChanged += TimeoutIdleCheckBox_CheckedChanged;
                }
            }
        }

        private TextBox _IdleTimeTextBox;

        internal TextBox IdleTimeTextBox
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _IdleTimeTextBox;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_IdleTimeTextBox != null)
                {
                    _IdleTimeTextBox.TextChanged -= IdleTimeTextBox_TextChanged;
                }

                _IdleTimeTextBox = value;
                if (_IdleTimeTextBox != null)
                {
                    _IdleTimeTextBox.TextChanged += IdleTimeTextBox_TextChanged;
                }
            }
        }

        internal Label Label2;
        internal CheckBox LogoffWhenTimoutCheckBox;
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

        internal ImageList SmallerIcons;
        internal Label Label3;
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

        internal CheckBox AllowUnlistedRemoteProgramsCheckBox;
    }
}