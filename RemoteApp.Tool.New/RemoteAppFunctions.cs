using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RemoteApp.Tool.New
{
    internal static class RemoteAppFunction
    {
        public static void ValidateInteger(TextBox TheTextBox)
        {
            int cloc = TheTextBox.SelectionStart;
            TheTextBox.Text = double.Parse(TheTextBox.Text).ToString();
            TheTextBox.Select(TheTextBox.Text.Length, 0);
            TheTextBox.Select(cloc, 0);
        }

        public static void ValidatePort(TextBox TheTextBox)
        {
            int cloc = TheTextBox.SelectionStart;
            if (double.Parse(TheTextBox.Text) > 65535d)
            {
                TheTextBox.Text = "65535";
                cloc = TheTextBox.Text.Length;
            }

            TheTextBox.Text = double.Parse(TheTextBox.Text).ToString();
            TheTextBox.Select(TheTextBox.Text.Length, 0);
            TheTextBox.Select(cloc, 0);
        }

        public static void ValidateSeconds(TextBox TheTextBox)
        {
            int cloc = TheTextBox.SelectionStart;
            if (double.Parse(TheTextBox.Text) > 2147483d)
            {
                TheTextBox.Text = 2147483.ToString();
                cloc = TheTextBox.Text.Length;
            }

            TheTextBox.Text = double.Parse(TheTextBox.Text).ToString();
            TheTextBox.Select(TheTextBox.Text.Length, 0);
            TheTextBox.Select(cloc, 0);
        }

        public static void ValidateAppName(TextBox TheTextBox)
        {
            ValidateTextBoxR(TheTextBox, @"[^\p{L}0-9\-_"" ""]");
        }

        private static void ValidateTextBoxR(TextBox TheTextBox, string regex)
        {
            int cloc = TheTextBox.SelectionStart;
            var rx = new Regex(regex);
            if (rx.IsMatch(TheTextBox.Text))
            {
                TheTextBox.Text = rx.Replace(TheTextBox.Text, "");
                TheTextBox.Select(cloc - 1, 0);
            }
            else
            {
                TheTextBox.Select(cloc, 0);
            }
        }

        public static object FixShortAppName(string TheText)
        {
            if (!string.IsNullOrEmpty(TheText))
            {
                var rx = new Regex(@"[^\p{L}0-9\-_"" ""]");
                if (rx.IsMatch(TheText))
                {
                    TheText = rx.Replace(TheText, "");
                }
            }

            return TheText.Trim();
        }

        public static void ValidateDNSname(TextBox TheTextBox)
        {
            // pattern matches any character that is NOT A-Z (allows upper and lower case alphabets)
            var rx = new Regex(@"[^\p{L}LlUu0-9\-\._]");
            if (rx.IsMatch(TheTextBox.Text))
            {
                TheTextBox.Text = rx.Replace(TheTextBox.Text, "");
                TheTextBox.Select(TheTextBox.Text.Length, 0);
            }
        }

        public static void ValidateFileType(TextBox TheTextBox)
        {
            // pattern matches any character that is NOT A-Z (allows upper and lower case alphabets)
            var rx = new Regex(@"[^\p{L}0-9\-_\!\@\#\$\%\^\&\(\)\{\}\[\]\+\=\;\,\']");
            if (rx.IsMatch(TheTextBox.Text))
            {
                TheTextBox.Text = rx.Replace(TheTextBox.Text, "");
                TheTextBox.Select(TheTextBox.Text.Length, 0);
            }
        }

        public static System.Drawing.Bitmap GetAppBitmap(string RemoteAppShortName)
        {
            // FIX : Potential exception.
            var AppKey = $@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\{RemoteAppShortName}";
            var TheRegKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(AppKey);
            var TheIcon = IconModule.ReturnIcon("", 0).ToBitmap();
            if (TheRegKey is null)
            {
                return TheIcon;
            }
            var IconPath = TheRegKey.GetValue("IconPath", "");
            var IconIndex = (int)TheRegKey.GetValue("IconIndex", "");
            return IconModule.ReturnIcon((string)IconPath, IconIndex).ToBitmap();
        }

        [DllImport("kernel32", EntryPoint = "GetSystemDirectoryA")]
        private static extern long GetSystemDirectory(string lpBuffer, long nSize);

        public static string GetSysDir()
        {
            return Environment.SystemDirectory;
        }

        public static void DeleteFiles(List<string> FilesArray)
        {
            foreach (string dFile in FilesArray)
            {
                var LockCheck = new LockChecker.LockChecker();
                string FileLocked;
                bool SkipFile = false;
                FileLocked = (string)LockCheck.CheckLock(dFile);
                while (FileLocked != "No locks")
                {
                    var message = $"The file {dFile} is currently locked. Lock information: {FileLocked}{Environment.NewLine}Do you want to try again?";
                    if (MessageBox.Show(message, "File Locked", MessageBoxButtons.YesNo)
                        == DialogResult.Yes)
                    {
                        FileLocked = (string)LockCheck.CheckLock(dFile);
                    }
                    else
                    {
                        _ = MessageBox.Show($"The following file will not be deleted:{Environment.NewLine}{dFile}");
                        SkipFile = true;
                        FileLocked = "No locks";
                    }
                }

                if (!SkipFile && File.Exists(dFile))
                {
                    File.Delete(dFile);
                }
            }
        }

        /// <summary>
        ///   Reads File Description. If it does not exist, returns the name of the exacutable.
        /// </summary>
        /// <param name="exePath">full path of the executable.</param>
        /// <returns>File description or file name</returns>
        public static string GetEXETitle(string exePath)
        {
            string pname = FileVersionInfo.GetVersionInfo(exePath).FileDescription;
            if (string.IsNullOrEmpty(pname))
            {
                pname = Path.GetFileNameWithoutExtension(exePath);
            }

            return pname;
        }
    }
}