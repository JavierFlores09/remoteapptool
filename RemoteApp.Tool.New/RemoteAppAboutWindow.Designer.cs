using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RemoteApp.Tool.New
{
    public partial class RemoteAppAboutWindow : Form
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteAppAboutWindow));
            TitleLabel = new Label();
            VersionLabel = new Label();
            CopyrightLabel = new Label();
            _SiteLinkLabel = new LinkLabel();
            _SiteLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(SiteLinkLabel_LinkClicked);
            IconLibCreatedByLabel = new Label();
            IconLibLabel = new Label();
            _IconLibLinkLabel = new LinkLabel();
            _IconLibLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(IconLibLinkLabel_LinkClicked);
            IconLibLicenceTextLabel = new Label();
            RemoteAppToolLicenceTextLabel = new Label();
            SuspendLayout();
            // 
            // TitleLabel
            // 
            TitleLabel.Anchor = AnchorStyles.Top;
            TitleLabel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            TitleLabel.Location = new Point(17, 9);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(263, 30);
            TitleLabel.TabIndex = 0;
            TitleLabel.Text = "RemoteApp Tool";
            TitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VersionLabel
            // 
            VersionLabel.Anchor = AnchorStyles.Top;
            VersionLabel.Location = new Point(17, 39);
            VersionLabel.Name = "VersionLabel";
            VersionLabel.Size = new Size(263, 30);
            VersionLabel.TabIndex = 0;
            VersionLabel.Text = "Version 0.0.0.0";
            VersionLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CopyrightLabel
            // 
            CopyrightLabel.Anchor = AnchorStyles.Top;
            CopyrightLabel.Location = new Point(17, 69);
            CopyrightLabel.Name = "CopyrightLabel";
            CopyrightLabel.Size = new Size(263, 30);
            CopyrightLabel.TabIndex = 0;
            CopyrightLabel.Text = "Kim Knight, Brian Gale";
            CopyrightLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SiteLinkLabel
            // 
            _SiteLinkLabel.Anchor = AnchorStyles.Top;
            _SiteLinkLabel.Location = new Point(15, 129);
            _SiteLinkLabel.Name = "_SiteLinkLabel";
            _SiteLinkLabel.Size = new Size(271, 30);
            _SiteLinkLabel.TabIndex = 1;
            _SiteLinkLabel.TabStop = true;
            _SiteLinkLabel.Text = "https://github.com/kimmknight/remoteapptool";
            _SiteLinkLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // IconLibCreatedByLabel
            // 
            IconLibCreatedByLabel.Anchor = AnchorStyles.Top;
            IconLibCreatedByLabel.Location = new Point(17, 189);
            IconLibCreatedByLabel.Name = "IconLibCreatedByLabel";
            IconLibCreatedByLabel.Size = new Size(263, 30);
            IconLibCreatedByLabel.TabIndex = 4;
            IconLibCreatedByLabel.Text = "Created by CastorTiu";
            IconLibCreatedByLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // IconLibLabel
            // 
            IconLibLabel.Anchor = AnchorStyles.Top;
            IconLibLabel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            IconLibLabel.Location = new Point(17, 159);
            IconLibLabel.Name = "IconLibLabel";
            IconLibLabel.Size = new Size(263, 30);
            IconLibLabel.TabIndex = 5;
            IconLibLabel.Text = "IconLib";
            IconLibLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // IconLibLinkLabel
            // 
            _IconLibLinkLabel.Anchor = AnchorStyles.Top;
            _IconLibLinkLabel.Location = new Point(12, 249);
            _IconLibLinkLabel.Name = "_IconLibLinkLabel";
            _IconLibLinkLabel.Size = new Size(274, 30);
            _IconLibLinkLabel.TabIndex = 1;
            _IconLibLinkLabel.TabStop = true;
            _IconLibLinkLabel.Text = "https://creativecommons.org/licenses/by-sa/3.0/";
            _IconLibLinkLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // IconLibLicenceTextLabel
            // 
            IconLibLicenceTextLabel.Anchor = AnchorStyles.Top;
            IconLibLicenceTextLabel.Location = new Point(17, 219);
            IconLibLicenceTextLabel.Name = "IconLibLicenceTextLabel";
            IconLibLicenceTextLabel.Size = new Size(263, 30);
            IconLibLicenceTextLabel.TabIndex = 4;
            IconLibLicenceTextLabel.Text = "Licensed under a Creative Commons" + '\r' + '\n' + "Attribution-Share Alike 3.0 Unported License";
            IconLibLicenceTextLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RemoteAppToolLicenceTextLabel
            // 
            RemoteAppToolLicenceTextLabel.Anchor = AnchorStyles.Top;
            RemoteAppToolLicenceTextLabel.Location = new Point(17, 99);
            RemoteAppToolLicenceTextLabel.Name = "RemoteAppToolLicenceTextLabel";
            RemoteAppToolLicenceTextLabel.Size = new Size(263, 30);
            RemoteAppToolLicenceTextLabel.TabIndex = 6;
            RemoteAppToolLicenceTextLabel.Text = "Licensed under The MIT License";
            RemoteAppToolLicenceTextLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RemoteAppAboutWindow
            // 
            AutoScaleDimensions = new SizeF(7.0f, 15.0f);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(298, 296);
            Controls.Add(RemoteAppToolLicenceTextLabel);
            Controls.Add(IconLibLicenceTextLabel);
            Controls.Add(IconLibCreatedByLabel);
            Controls.Add(IconLibLabel);
            Controls.Add(_IconLibLinkLabel);
            Controls.Add(_SiteLinkLabel);
            Controls.Add(CopyrightLabel);
            Controls.Add(VersionLabel);
            Controls.Add(TitleLabel);
            Font = new Font("Segoe UI", 9.0f);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RemoteAppAboutWindow";
            StartPosition = FormStartPosition.CenterParent;
            Text = "RemoteAppAboutWindow";
            Load += new EventHandler(RemoteAppAboutWindow_Load);
            ResumeLayout(false);
        }

        internal Label TitleLabel;
        internal Label VersionLabel;
        internal Label CopyrightLabel;
        private LinkLabel _SiteLinkLabel;

        internal LinkLabel SiteLinkLabel
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _SiteLinkLabel;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_SiteLinkLabel != null)
                {
                    _SiteLinkLabel.LinkClicked -= SiteLinkLabel_LinkClicked;
                }

                _SiteLinkLabel = value;
                if (_SiteLinkLabel != null)
                {
                    _SiteLinkLabel.LinkClicked += SiteLinkLabel_LinkClicked;
                }
            }
        }

        internal Label IconLibCreatedByLabel;
        internal Label IconLibLabel;
        private LinkLabel _IconLibLinkLabel;

        internal LinkLabel IconLibLinkLabel
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _IconLibLinkLabel;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_IconLibLinkLabel != null)
                {
                    _IconLibLinkLabel.LinkClicked -= IconLibLinkLabel_LinkClicked;
                }

                _IconLibLinkLabel = value;
                if (_IconLibLinkLabel != null)
                {
                    _IconLibLinkLabel.LinkClicked += IconLibLinkLabel_LinkClicked;
                }
            }
        }

        internal Label IconLibLicenceTextLabel;
        internal Label RemoteAppToolLicenceTextLabel;
    }
}