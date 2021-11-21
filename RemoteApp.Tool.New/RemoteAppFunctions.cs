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
        public static void ValidateInteger(TextBox theTextBox)
        {
            int cloc = theTextBox.SelectionStart;
            theTextBox.Text = double.Parse(theTextBox.Text).ToString();
            theTextBox.Select(theTextBox.Text.Length, 0);
            theTextBox.Select(cloc, 0);
        }

        public static void ValidatePort(TextBox theTextBox)
        {
            int cloc = theTextBox.SelectionStart;
            if (double.Parse(theTextBox.Text) > 65535d)
            {
                theTextBox.Text = "65535";
                cloc = theTextBox.Text.Length;
            }

            theTextBox.Text = double.Parse(theTextBox.Text).ToString();
            theTextBox.Select(theTextBox.Text.Length, 0);
            theTextBox.Select(cloc, 0);
        }

        public static void ValidateSeconds(TextBox theTextBox)
        {
            int cloc = theTextBox.SelectionStart;
            if (double.Parse(theTextBox.Text) > 2147483d)
            {
                theTextBox.Text = 2147483.ToString();
                cloc = theTextBox.Text.Length;
            }

            theTextBox.Text = double.Parse(theTextBox.Text).ToString();
            theTextBox.Select(theTextBox.Text.Length, 0);
            theTextBox.Select(cloc, 0);
        }

        public static void ValidateAppName(TextBox theTextBox)
        {
            ValidateTextBoxR(theTextBox, @"[^\p{L}0-9\-_"" ""]");
        }

        private static void ValidateTextBoxR(TextBox theTextBox, string regex)
        {
            int cloc = theTextBox.SelectionStart;
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
            if (!string.IsNullOrEmpty(theText))
            {
                var rx = new Regex(@"[^\p{L}0-9\-_"" ""]");
                if (rx.IsMatch(theText))
                {
                    theText = rx.Replace(theText, "");
                }
            }

            return theText.Trim();
        }

        public static void ValidateDnSname(TextBox theTextBox)
        {
            // pattern matches any character that is NOT A-Z (allows upper and lower case alphabets)
            var rx = new Regex(@"[^\p{L}LlUu0-9\-\._]");
            if (rx.IsMatch(theTextBox.Text))
            {
                theTextBox.Text = rx.Replace(theTextBox.Text, "");
                theTextBox.Select(theTextBox.Text.Length, 0);
            }
        }

        public static void ValidateFileType(TextBox theTextBox)
        {
            // pattern matches any character that is NOT A-Z (allows upper and lower case alphabets)
            var rx = new Regex(@"[^\p{L}0-9\-_\!\@\#\$\%\^\&\(\)\{\}\[\]\+\=\;\,\']");
            if (rx.IsMatch(theTextBox.Text))
            {
                theTextBox.Text = rx.Replace(theTextBox.Text, "");
                theTextBox.Select(theTextBox.Text.Length, 0);
            }
        }

        public static System.Drawing.Bitmap GetAppBitmap(string remoteAppShortName)
        {
            // FIX : Potential exception.
            var AppKey = $@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Terminal Server\TSAppAllowList\Applications\{remoteAppShortName}";
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

        public static void DeleteFiles(List<string> filesArray)
        {
            foreach (string dFile in filesArray)
            {
                var LockCheck = new LockChecker.New.LockChecker();
                string FileLocked;
                bool SkipFile = false;
                FileLocked = LockCheck.CheckLock(dFile);
                while (FileLocked != "No locks")
                {
                    var message = $"The file {dFile} is currently locked. Lock information: {FileLocked}{Environment.NewLine}Do you want to try again?";
                    if (MessageBox.Show(message, "File Locked", MessageBoxButtons.YesNo)
                        == DialogResult.Yes)
                    {
                        FileLocked = LockCheck.CheckLock(dFile);
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
        public static string GetExeTitle(string exePath)
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