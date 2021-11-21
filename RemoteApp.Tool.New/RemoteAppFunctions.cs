using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RemoteApp.Tool.New
{
    internal static class RemoteAppFunction
    {
        public static void ValidateInteger(TextBox theTextBox)
        {
            var cloc = theTextBox.SelectionStart;
            theTextBox.Text = double.Parse(theTextBox.Text).ToString(CultureInfo.InvariantCulture);
            theTextBox.Select(theTextBox.Text.Length, 0);
            theTextBox.Select(cloc, 0);
        }

        public static void ValidatePort(TextBox theTextBox)
        {
            var cloc = theTextBox.SelectionStart;
            if (double.Parse(theTextBox.Text) > 65535d)
            {
                theTextBox.Text = "65535";
                cloc = theTextBox.Text.Length;
            }

            theTextBox.Text = double.Parse(theTextBox.Text).ToString(CultureInfo.InvariantCulture);
            theTextBox.Select(theTextBox.Text.Length, 0);
            theTextBox.Select(cloc, 0);
        }

        public static void ValidateSeconds(TextBox theTextBox)
        {
            var cloc = theTextBox.SelectionStart;
            if (double.Parse(theTextBox.Text) > 2147483d)
            {
                theTextBox.Text = 2147483.ToString();
                cloc = theTextBox.Text.Length;
            }

            theTextBox.Text = double.Parse(theTextBox.Text).ToString(CultureInfo.InvariantCulture);
            theTextBox.Select(theTextBox.Text.Length, 0);
            theTextBox.Select(cloc, 0);
        }

        public static void ValidateAppName(TextBox theTextBox)
        {
            ValidateTextBoxR(theTextBox, @"[^\p{L}0-9\-_"" ""]");
        }

        private static void ValidateTextBoxR(TextBox theTextBox, string regex)
        {
            var cloc = theTextBox.SelectionStart;
            var rx = new Regex(regex);
            if (rx.IsMatch(theTextBox.Text))
            {
                theTextBox.Text = rx.Replace(theTextBox.Text, "");
                theTextBox.Select(cloc - 1, 0);
            }
            else
            {
                theTextBox.Select(cloc, 0);
            }
        }

        public static object FixShortAppName(string theText)
        {
            if (string.IsNullOrEmpty(theText)) return theText.Trim();
            var rx = new Regex(@"[^\p{L}0-9\-_"" ""]");
            if (rx.IsMatch(theText))
            {
                theText = rx.Replace(theText, "");
            }

            return theText.Trim();
        }

        public static void ValidateDnsName(TextBox theTextBox)
        {
            // pattern matches any character that is NOT A-Z (allows upper and lower case alphabets)
            var rx = new Regex(@"[^\p{L}LlUu0-9\-\._]");
            if (!rx.IsMatch(theTextBox.Text)) return;
            theTextBox.Text = rx.Replace(theTextBox.Text, "");
            theTextBox.Select(theTextBox.Text.Length, 0);
        }

        public static void ValidateFileType(TextBox theTextBox)
        {
            // pattern matches any character that is NOT A-Z (allows upper and lower case alphabets)
            var rx = new Regex(@"[^\p{L}0-9\-_\!\@\#\$\%\^\&\(\)\{\}\[\]\+\=\;\,\']");
            if (!rx.IsMatch(theTextBox.Text)) return;
            theTextBox.Text = rx.Replace(theTextBox.Text, "");
            theTextBox.Select(theTextBox.Text.Length, 0);
        }

        public static System.Drawing.Bitmap GetAppBitmap(string remoteAppShortName)
        {
            // FIX : Potential exception.
            var appKey = $@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\{remoteAppShortName}";
            var registryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(appKey);
            var icon = IconModule.ReturnIcon("", 0).ToBitmap();
            if (registryKey is null)
            {
                return icon;
            }
            var iconPath = registryKey.GetValue("IconPath", "");
            var iconIndex = (int)registryKey.GetValue("IconIndex", "");
            return IconModule.ReturnIcon((string)iconPath, iconIndex).ToBitmap();
        }

        [DllImport("kernel32", EntryPoint = "GetSystemDirectoryA")]
        private static extern long GetSystemDirectory(string lpBuffer, long nSize);

        public static string GetSysDir()
        {
            return Environment.SystemDirectory;
        }

        public static void DeleteFiles(List<string> filesArray)
        {
            foreach (var dFile in filesArray)
            {
                var lockCheck = new LockChecker.New.LockChecker();
                var skipFile = false;
                var fileLocked = lockCheck.CheckLock(dFile);
                while (fileLocked != "No locks")
                {
                    var message = $"The file {dFile} is currently locked. Lock information: {fileLocked}{Environment.NewLine}Do you want to try again?";
                    if (MessageBox.Show(message, "File Locked", MessageBoxButtons.YesNo)
                        == DialogResult.Yes)
                    {
                        fileLocked = lockCheck.CheckLock(dFile);
                    }
                    else
                    {
                        _ = MessageBox.Show($"The following file will not be deleted:{Environment.NewLine}{dFile}");
                        skipFile = true;
                        fileLocked = "No locks";
                    }
                }

                if (!skipFile && File.Exists(dFile))
                {
                    File.Delete(dFile);
                }
            }
        }

        /// <summary>
        ///   Reads File Description. If it does not exist, returns the name of the executable.
        /// </summary>
        /// <param name="exePath">full path of the executable.</param>
        /// <returns>File description or file name</returns>
        public static string GetExeTitle(string exePath)
        {
            var executableTitle = FileVersionInfo.GetVersionInfo(exePath).FileDescription;
            if (string.IsNullOrEmpty(executableTitle))
            {
                executableTitle = Path.GetFileNameWithoutExtension(exePath);
            }

            return executableTitle;
        }
    }
}