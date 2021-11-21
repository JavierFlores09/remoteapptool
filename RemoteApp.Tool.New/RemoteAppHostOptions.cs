using System;

namespace RemoteApp.Tool.New
{
    public partial class RemoteAppHostOptions
    {
        public RemoteAppHostOptions()
        {
            InitializeComponent();
            _TimeoutDisconnectedCheckBox.Name = "TimeoutDisconnectedCheckBox";
            _DisconnectTimeTextBox.Name = "DisconnectTimeTextBox";
            _TimeoutIdleCheckBox.Name = "TimeoutIdleCheckBox";
            _IdleTimeTextBox.Name = "IdleTimeTextBox";
            _SaveButton.Name = "SaveButton";
            _CancelEditButton.Name = "CancelEditButton";
        }

        private void DisconnectTimeTextBox_TextChanged(object sender, EventArgs e)
        {
            RemoteAppFunction.ValidateSeconds(DisconnectTimeTextBox);
        }

        private void IdleTimeTextBox_TextChanged(object sender, EventArgs e)
        {
            RemoteAppFunction.ValidateSeconds(IdleTimeTextBox);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            const string policyKeyStringMs = @"SOFTWARE\Policies\Microsoft";
            const string policyKeyString = @"SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services";
            Microsoft.Win32.RegistryKey PolicyKey;

            // Create policy reg keys
            var policyKeyMs = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(policyKeyStringMs, true);
            var policyKeyWnt = policyKeyMs.CreateSubKey("Windows NT");
            _ = policyKeyWnt.CreateSubKey("Terminal Services");
            PolicyKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(policyKeyString, true);
            if (DisableAllowListCheckBox.Checked)
            {
                Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList", "fDisabledAllowList", "1", Microsoft.Win32.RegistryValueKind.DWord);
            }
            else
            {
                Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList", "fDisabledAllowList", "0", Microsoft.Win32.RegistryValueKind.DWord);
            }

            if (AllowUnlistedRemoteProgramsCheckBox.Checked)
            {
                PolicyKey.SetValue("fAllowUnlistedRemotePrograms", 1, Microsoft.Win32.RegistryValueKind.DWord);
            }
            else
            {
                PolicyKey.DeleteValue("fAllowUnlistedRemotePrograms", false);
            }

            if (TimeoutDisconnectedCheckBox.Checked)
            {
                PolicyKey.SetValue("MaxDisconnectionTime", double.Parse(DisconnectTimeTextBox.Text) * 1000d, Microsoft.Win32.RegistryValueKind.DWord);
            }
            else
            {
                PolicyKey.DeleteValue("MaxDisconnectionTime", false);
            }

            if (TimeoutIdleCheckBox.Checked)
            {
                PolicyKey.SetValue("MaxIdleTime", double.Parse(IdleTimeTextBox.Text) * 1000d, Microsoft.Win32.RegistryValueKind.DWord);
            }
            else
            {
                PolicyKey.DeleteValue("MaxIdleTime", false);
            }

            if (LogoffWhenTimoutCheckBox.Checked)
            {
                PolicyKey.SetValue("fResetBroken", "1", Microsoft.Win32.RegistryValueKind.DWord);
            }
            else
            {
                PolicyKey.DeleteValue("fResetBroken", false);
            }

            Close();
        }

        public void SetValues()
        {
            // FIX: Potential exception
            const string policyKeyString = @"SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services";
            using (var PolicyKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(policyKeyString, false))
            {
                DisconnectTimeTextBox.Text = "0";
                IdleTimeTextBox.Text = "0";
                var tsAllowList = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList", "fDisabledAllowList", "") as string;
                if (!string.IsNullOrEmpty(tsAllowList))
                {
                    DisableAllowListCheckBox.Checked = double.Parse(tsAllowList) == 1d;
                }
                else
                {
                    // Log or debug here
                }

                var MaxDisconnectionTime = (int)PolicyKey.GetValue("MaxDisconnectionTime", -1);
                if (MaxDisconnectionTime != -1)
                {
                    TimeoutDisconnectedCheckBox.Checked = true;
                    DisconnectTimeTextBox.Text = (MaxDisconnectionTime / 1000d).ToString();
                }
                else
                {
                    TimeoutDisconnectedCheckBox.Checked = false;
                }
                var MaxIdleTime = (int)PolicyKey.GetValue("MaxIdleTime", -1);
                if (MaxIdleTime != -1)
                {
                    TimeoutIdleCheckBox.Checked = true;
                    IdleTimeTextBox.Text = (MaxIdleTime / 1000d).ToString();
                }
                else
                {
                    TimeoutIdleCheckBox.Checked = false;
                }

                var fResetBroken = PolicyKey.GetValue("fResetBroken");
                LogoffWhenTimoutCheckBox.Checked = fResetBroken != default;

                var fAllowUnlistedRemotePrograms = PolicyKey.GetValue("fAllowUnlistedRemotePrograms");
                AllowUnlistedRemoteProgramsCheckBox.Checked = fAllowUnlistedRemotePrograms != default;
            }

            DisconnectTimeTextBox.Enabled = TimeoutDisconnectedCheckBox.Checked;

            IdleTimeTextBox.Enabled = TimeoutIdleCheckBox.Checked;
        }

        private void TimeoutDisconnectedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DisconnectTimeTextBox.Enabled = TimeoutDisconnectedCheckBox.Checked;
        }

        private void TimeoutIdleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IdleTimeTextBox.Enabled = TimeoutIdleCheckBox.Checked;
        }

        private void CancelEditButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}