using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RemoteApp.Tool
{
    public partial class RemoteAppAboutWindow
    {
        public RemoteAppAboutWindow()
        {
            InitializeComponent();
            _SiteLinkLabel.Name = "SiteLinkLabel";
            _IconLibLinkLabel.Name = "IconLibLinkLabel";
        }

        private void RemoteAppAboutWindow_Load(object sender, EventArgs e)
        {
            Text = $"About {Application.ProductName}";
            TitleLabel.Text = Application.ProductName;
            VersionLabel.Text = $"Version {Application.ProductVersion}";
        }

        private void SiteLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(SiteLinkLabel.Text);
        }

        private void IconLibLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(IconLibLinkLabel.Text);
        }
    }
}