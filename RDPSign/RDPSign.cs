using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

namespace RDPSign
{
    public class RDPSign
    {
        public int ErrorNumber;
        public string ErrorString = string.Empty;

        /// <summary>
        /// Get all Certificate friendly names
        /// </summary>
        /// <returns>String array of Certificate friendly names</returns>
        public IEnumerable<string> GetCertificateFriendlyName()
        {
            var certStoreLm = GetCertificateStoreLm();
            var certStoreCu = GetCertificateStoreCu();
            var FriendlyNames = new string[certStoreLm.Certificates.Count + certStoreCu.Certificates.Count + 1];
            var Counter = 0;
            foreach (var Certificate in certStoreLm.Certificates)
            {
                if (ReferenceEquals(Certificate.FriendlyName, string.Empty)) continue;
                FriendlyNames[Counter] = Certificate.FriendlyName;
                Counter++;
            }

            foreach (var Certificate in certStoreCu.Certificates)
            {
                if (ReferenceEquals(Certificate.FriendlyName, string.Empty)) continue;
                FriendlyNames[Counter] = Certificate.FriendlyName;
                Counter++;
            }

            Array.Resize(ref FriendlyNames, Counter);
            return FriendlyNames;
        }

        /// <summary>
        /// Open the Local Machine certificate store and return it to the calling function/sub
        /// </summary>
        /// <returns>Certificate Store</returns>
        public X509Store GetCertificateStoreLm()
        {
            var CertStore = new X509Store(StoreLocation.LocalMachine);
            CertStore.Open(OpenFlags.ReadOnly);
            return CertStore;
        }

        /// <summary>
        /// Open the Current User certificate store and return it to the calling function/sub
        /// </summary>
        /// <returns>Certificate Store</returns>
        public X509Store GetCertificateStoreCu()
        {
            var CertStore = new X509Store(StoreLocation.CurrentUser);
            CertStore.Open(OpenFlags.ReadOnly);
            return CertStore;
        }

        /// <summary>
        /// Given a friendly name, find and return the associated thumbprint
        /// </summary>
        /// <param name="friendlyName">String of the Friendly Name of a certificate</param>
        /// <returns>String of the thumbprint of the certificate</returns>
        public string GetThumbprint(string friendlyName)
        {
            string Thumbprint;
            foreach (var certificate in GetCertificateStoreLm().Certificates)
            {
                if ((certificate.FriendlyName ?? string.Empty) != (friendlyName ?? string.Empty)) continue;
                Thumbprint = certificate.Thumbprint;
                GetCertificateStoreLm().Close();
                return Thumbprint;
            }
            GetCertificateStoreLm().Close();
            foreach (var certificate in GetCertificateStoreCu().Certificates)
            {
                if ((certificate.FriendlyName ?? string.Empty) != (friendlyName ?? string.Empty)) continue;
                Thumbprint = certificate.Thumbprint;
                GetCertificateStoreCu().Close();
                return Thumbprint;
            }
            // We could get here if something went wrong such as Certificate was removed from certificate store after it was loaded into the application
            // return an invalid thumbprint
            return "0000";
        }

        /// <summary>
        /// Sign an RDP file and make a backup of the unsigned one if requested
        /// </summary>
        /// <param name="thumbprint">Thumbprint used to sign RDP file</param>
        /// <param name="rdpFileLocation">Location of RDP file</param>
        /// <param name="createBackup">Boolean indicating if a backup should be created or not</param>
        public void SignRDP(string thumbprint, string rdpFileLocation, bool createBackup)
        {
            if (thumbprint == "0000")
            {
                // Invalid thumbprint, this should be handled on the application side, but just as a safety, return without doing any work if invalid thumbprint sent
                return;
            }

            if (createBackup)
            {
                var BackupFile = Path.Combine(Path.GetDirectoryName(rdpFileLocation), $"{Path.GetFileNameWithoutExtension(rdpFileLocation)}-Unsigned.rdp");
                var LockCheck = new LockChecker.LockChecker();
                var SkipFile = false;
                var FileLocked = LockCheck.CheckLock(BackupFile);
                while (FileLocked != "No locks")
                {
                    // TODO: Add an exception or another method to check the result here so that the consumer executable can decide the GUI elements.
                    if (MessageBox.Show($"The file {BackupFile} is currently locked.  Lock information:{FileLocked}{Environment.NewLine}Do you want to try again?", "File Locked", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        FileLocked = LockCheck.CheckLock(BackupFile);
                    }
                    else
                    {
                        _ = MessageBox.Show($"The following file will not be copied:{Environment.NewLine}{BackupFile}");
                        SkipFile = true;
                        FileLocked = "No locks";
                    }
                }

                if (!SkipFile)
                {
                    File.Copy(rdpFileLocation, BackupFile, true); // backup file with overwrite
                }
            }

            // If we get here, we should be good to run the command to sign the RDP file.

            // Grab the rdpsign.exe location and then verify that it exists

            var Command = GetRdpsignExeLocation();
            if (File.Exists(Command))
            {
                string Arguments;
                var FileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(Command);
                // On my windows 10 computer, the argument is /sha256 instead of /sha1.  /sha1 doesn't work.
                // On my windows 10 computer, the Product parts come in at 10.0.18362.1
                // On a Windows Server 2008 R2 server I have access to, the argument is /sha1.
                // On a Windows Server 2008 R2 server I have access to, the Product parts come in at 6.1.7601.17514 which is lower than the windows 10 ones.
                // I do not have other versions of windows to test, so will need external testing for this.
                // Not sure where the version number switches over, but also not sure how to determine which method to use otherwise
                Arguments = FileVersionInfo.ProductMajorPart >= 10 ? $" /sha256 {thumbprint} \"{rdpFileLocation}\"" : $" /sha1 {thumbprint} \"{rdpFileLocation}\"";

                var StartInfo = new ProcessStartInfo
                {
                    FileName = Command,
                    Arguments = Arguments,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process.Start(StartInfo);
            }
            else
            {
                MessageBox.Show($"RDPSign executable not found:{Environment.NewLine}{Environment.NewLine}{Command}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Find the full path to rdpsign.exe from a list of possible locations
        /// </summary>
        /// <returns>A path to rdpsign.exe or an empty string</returns>
        public string GetRdpsignExeLocation()
        {
            // Each path to check for rdpsign.exe is added to this array.
            // If it is found in more than one location, the lowest in the list will be selected.
            var possibleRdpSignPaths = new string[] { Environment.SystemDirectory, Directory.GetCurrentDirectory() };
            var finalRdpSignPath = string.Empty;
            foreach (var rdpSignPath in possibleRdpSignPaths)
            {
                var newPath = Path.Combine(rdpSignPath, "rdpsign.exe");
                if (File.Exists(newPath))
                {
                    finalRdpSignPath = newPath;
                }
            }

            return finalRdpSignPath;
        }

        [DllImport("kernel32", EntryPoint = "GetSystemDirectoryA")]
        private static extern long GetSystemDirectory(string lpBuffer, long nSize);
    }
}
