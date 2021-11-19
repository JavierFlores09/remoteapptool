using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace RemoteApp.Tool.New
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
            Text = $"About {Assembly.GetExecutingAssembly().GetName().Name}";
            TitleLabel.Text = Assembly.GetExecutingAssembly().GetName().Name;
            VersionLabel.Text = $"Version {Assembly.GetExecutingAssembly().GetName().Version}";
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