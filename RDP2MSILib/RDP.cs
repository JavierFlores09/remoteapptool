using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace RDP2MSIlib
{
    public class RDP
    {
        private string _rdpFilePath;

        public string RDPFilePath
        {
            get
            {
                return _rdpFilePath;
            }

            set
            {
                _rdpFilePath = value;
                ReadRdpFile();
            }
        }

        private string _productName; // Full remoteapp name as read from RDP file
        public string FlatFileTypes = "";
        public bool PerUser = false;
        private string _productBaseFileName; // RDP filename minus the file extension
        public string ProductPublisher;
        public string ProductVersion = "1.0.0.0";
        public bool ProductUpgradeRandom = false;
        public string ProductRemoteTag = "remote";
        public bool ShortcutInStart = true;
        public bool ShortcutSubfolderInStart = true;
        public bool ShortcutOnDesktop = true;
        private string _rdpFileContents;
        private bool _hasIcon;
        private bool _rdpInTemp;

        public void CreateMSI(string destinationPath = "")
        {
            // Check for wix, exit if not available
            if (!WixInstalled())
            {
                return;
            }

            // Get RDP file parent folder path
            var rdpParentFolder = Path.GetDirectoryName(_rdpFilePath);

            // Get the RDP filename and filename minus extension
            var fileName = new FileInfo(_rdpFilePath).Name;
            _productBaseFileName = Path.GetFileNameWithoutExtension(fileName);

            // If DestinationPath not defined then set the path to the same as the rdp file
            if (string.IsNullOrEmpty(destinationPath))
            {
                destinationPath = $@"{rdpParentFolder}\{_productBaseFileName}.msi";
            }

            // Set iconpath (whether it exists or not)
            var IconFilePath = $@"{rdpParentFolder}\{_productBaseFileName}.ico";

            // Check for icon, set "HasIcon" to true if found
            _hasIcon = File.Exists(IconFilePath);

            // Get RemoteApp names (short and full) from RDP file contents
            //var RemoteAppFullName = ReadRDPProperty("remoteapplicationname");
            //var RemoteAppShortName = ReadRDPProperty("remoteapplicationprogram");

            // Define wix temp file paths
            var TempPath = Environment.GetEnvironmentVariable("TEMP");
            var wxsPath = $@"{TempPath}\{_productBaseFileName}.wxs";
            var wixobjPath = $@"{TempPath}\{_productBaseFileName}.wixobj";
            var wixpdbPath = $@"{TempPath}\{_productBaseFileName}.wixpdb";
            var msiPath = $@"{TempPath}\{_productBaseFileName}.msi";

            // Define temp files to delete
            var FilesToDelete = new List<string>(new[] { wxsPath, wixobjPath, wixpdbPath });
            string FileLocked;
            var SkipFile = false;
            // Check if rdp file is already in TEMP folder
            if ((rdpParentFolder ?? "") == (TempPath ?? ""))
            {
                _rdpInTemp = true;
            }

            // if RDP file not in temp, copy to temp
            if (!_rdpInTemp)
            {
                FileLocked = LockChecker.LockChecker.CheckLock($@"{TempPath}\{_productBaseFileName}.rdp");
                while (FileLocked != "No locks")
                {
                    if (MessageBox.Show($"The file {$@"{TempPath}\{_productBaseFileName}.rdp"} is currently locked.  Lock information:{FileLocked}{Environment.NewLine}Do you want to try again?", "File Locked", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        FileLocked = LockChecker.LockChecker.CheckLock($@"{TempPath}\{_productBaseFileName}.rdp");
                    }
                    else
                    {
                        _ = MessageBox.Show($"The following file will not be deleted:{Environment.NewLine}{$@"{TempPath}\{_productBaseFileName}.rdp"}");
                        SkipFile = true;
                        FileLocked = "No locks";
                    }
                }

                if (!SkipFile)
                {
                    File.Copy(_rdpFilePath, $@"{TempPath}\{_productBaseFileName}.rdp", true);
                }

                FilesToDelete.Add($@"{TempPath}\{_productBaseFileName}.rdp");
                if (_hasIcon)
                {
                    FileLocked = LockChecker.LockChecker.CheckLock($@"{TempPath}\{_productBaseFileName}.ico");
                    while (FileLocked != "No locks")
                    {
                        if (MessageBox.Show($"The file {$@"{TempPath}\{_productBaseFileName}.ico"} is currently locked.  Lock information:{FileLocked}{Environment.NewLine}Do you want to try again?", "File Locked", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            FileLocked = LockChecker.LockChecker.CheckLock($@"{TempPath}\{_productBaseFileName}.ico");
                        }
                        else
                        {
                            _ = MessageBox.Show($"The following file will not be deleted:{Environment.NewLine}{$@"{TempPath}\{_productBaseFileName}.ico"}");
                            SkipFile = true;
                            FileLocked = "No locks";
                        }
                    }

                    if (!SkipFile)
                    {
                        File.Copy(IconFilePath, $@"{TempPath}\{_productBaseFileName}.ico", true);
                    }

                    FilesToDelete.Add($@"{TempPath}\{_productBaseFileName}.ico");
                }
            }

            // Save WXS file containing generated WXS string
            File.WriteAllText(wxsPath, GenerateWxsString());
            var CandlePath = Path.Combine(GetWixPath(), "candle.exe ");
            var LightPath = Path.Combine(GetWixPath(), "light.exe ");

            // Run Candle.exe and Light.exe to process wxs file
            var CandleExitCode = RunWait(CandlePath, $"-out \"{wixobjPath}\" \"{wxsPath}\"");
            // If Not CandleExitCode = 0 Then Exit Sub
            if (CandleExitCode != 0)
            {
                throw new Exception("Could not start candle.exe");
            }
            var LightExitCode = RunWait(LightPath, $"-out \"{msiPath}\" \"{wixobjPath}\"");
            // If Not LightExitCode = 0 Then Exit Sub
            if (LightExitCode != 0)
            {
                throw new Exception("Could not start light.exe");
            }

            // Move MSI file to destination and delete temp files
            FileLocked = LockChecker.LockChecker.CheckLock(destinationPath);
            while (FileLocked != "No locks")
            {
                if (MessageBox.Show($"The file {destinationPath} is currently locked.  Lock information:{FileLocked}{Environment.NewLine}Do you want to try again?", "File Locked", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FileLocked = LockChecker.LockChecker.CheckLock(destinationPath);
                }
                else
                {
                    _ = MessageBox.Show($"The following file will not be deleted:{Environment.NewLine}{destinationPath}");
                    SkipFile = true;
                    FileLocked = "No locks";
                }
            }

            if (!SkipFile)
            {
                File.Move(msiPath, destinationPath);
            }

            DeleteFiles(FilesToDelete);
        }

        public bool WixInstalled()
        {
            return !string.IsNullOrEmpty(GetWixPath());
        }

        private static string GetWixPath()
        {
            const string searchExe = "candle.exe";
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WIX")))
            {
                return $"{Environment.GetEnvironmentVariable("WIX")}bin";
            }
            // candle.exe is a file, not a directory (upstream fix for WiX detection, PR #84)
            else if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wix", searchExe)))
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "wix");
            }
            else if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wix", "bin", searchExe)))
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "wix", "bin");
            }

            return string.Empty;
        }

        private static void DeleteFiles(List<string> filesToDelete)
        {
            foreach (var dFile in filesToDelete)
            {
                var SkipFile = false;
                var FileLocked = LockChecker.LockChecker.CheckLock(dFile);
                while (FileLocked != "No locks")
                {
                    if (MessageBox.Show($"The file {dFile} is currently locked.  Lock information:{FileLocked}{Environment.NewLine}Do you want to try again?", "File Locked", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        FileLocked = LockChecker.LockChecker.CheckLock(dFile);
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

        private void ReadRdpFile()
        {
            // Check if RDP file exists
            if (!File.Exists(_rdpFilePath))
            {
                return;
            }

            // Check that RDP file is an RDP file
            if (!_rdpFilePath.EndsWith(".rdp", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            // Read RDP file into variable
            _rdpFileContents = File.ReadAllText(_rdpFilePath);

            // Check RDP file contains a remoteapplicationname value
            // If ReadRDPProperty("remoteapplicationname") = "" Then Exit Sub

            // Check if RDP file contains a server address
            if (string.IsNullOrEmpty(ReadRDPProperty("full address")))
            {
                return;
            }

            // Read variables
            _productName = ReadRDPProperty("remoteapplicationname");
            if (string.IsNullOrEmpty(_productName))
            {
                _productName = Path.GetFileNameWithoutExtension(_rdpFilePath);
            }

            if (ProductPublisher == null)
            {
                ProductPublisher = _productName;
            }
        }

        public string ProductUpgradeCode()
        {
            // random or generated - maybe allow caller to define?

            // Check if ProductUpgradeRandom is true, create a random productcode if so, otherwise generate it from the productname
            return ProductUpgradeRandom ? GenerateGuidFromString(new Random().Next().ToString()) : GenerateGuidFromString(_productName);
        }

        public string MakeProgId(string appName)
        {
            var rx = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_]");
            return rx.Replace(appName, "_");
        }

        private string GenerateWxsString()
        {
            if (string.IsNullOrEmpty(ProductPublisher))
            {
                ProductPublisher = _productName;
            }

            var RegRoot = "HKLM";
            if (PerUser)
            {
                RegRoot = "HKCU";
            }

            var AppFilesGuid = GenerateGuidFromString($"AppFiles{ProductUpgradeCode()}");
            var AppStartShortcutsGuid = GenerateGuidFromString($"AppStartShortcuts{ProductUpgradeCode()}");
            var AppDeskShortcutsGuid = GenerateGuidFromString($"AppDeskShortcuts{ProductUpgradeCode()}");
            var wxsString = new StringBuilder(400);
            wxsString.AppendLine("<?xml version=\"1.0\"?>");
            wxsString.Append("<?define ProductVersion = \"").Append(ProductVersion).AppendLine("\"?>");
            wxsString.Append("<?define ProductUpgradeCode = \"").Append(ProductUpgradeCode()).AppendLine("\"?>");
            wxsString.Append("<?define AppFilesGuid = \"").Append(AppFilesGuid).AppendLine("\"?>");
            wxsString.Append("<?define AppStartShortcutsGuid = \"").Append(AppStartShortcutsGuid).AppendLine("\"?>");
            wxsString.Append("<?define AppDeskShortcutsGuid = \"").Append(AppDeskShortcutsGuid).AppendLine("\"?>");
            wxsString.Append("<?define ProductName = \"").Append(_productName).AppendLine("\"?>");
            wxsString.Append("<?define ProductPublisher = \"").Append(ProductPublisher).AppendLine("\"?>");
            wxsString.Append("<?define ProductBaseFileName = \"").Append(_productBaseFileName).AppendLine("\"?>");
            wxsString.Append("<?define RegRoot = \"").Append(RegRoot).AppendLine("\"?>");
            if (!string.IsNullOrEmpty(FlatFileTypes))
            {
                wxsString.Append("<?define ProductProgID = \"").Append(MakeProgId(_productBaseFileName)).AppendLine("\"?>");
            }

            if (!string.IsNullOrEmpty(ProductRemoteTag))
            {
                ProductRemoteTag = $" ({ProductRemoteTag})";
            }

            wxsString.Append("<?define ProductRemoteTag = \"").Append(ProductRemoteTag).AppendLine("\"?>");
            wxsString.AppendLine("<Wix xmlns=\"http://schemas.microsoft.com/wix/2006/wi\">");
            wxsString.AppendLine("   <Product Id=\"*\" UpgradeCode=\"$(var.ProductUpgradeCode)\" ");
            wxsString.AppendLine("            Name=\"$(var.ProductName)$(var.ProductRemoteTag)\" Version=\"$(var.ProductVersion)\" Manufacturer=\"$(var.ProductPublisher)\" Language=\"1033\">");
            wxsString.Append("      <Package InstallerVersion=\"200\" Compressed=\"yes\" Comments=\"Windows Installer Package\"");
            wxsString.Append(!PerUser ? " InstallScope=\"perMachine\"" : " InstallPrivileges=\"limited\"");

            wxsString.AppendLine("/>");
            wxsString.AppendLine("      <Media Id=\"1\" Cabinet=\"rdp2msi.cab\" EmbedCab=\"yes\"/>");
            if (!PerUser)
            {
                wxsString.AppendLine("      <Property Id=\"AllUSERS\" Value=\"1\"/>");
            }

            wxsString.AppendLine("      <Upgrade Id=\"$(var.ProductUpgradeCode)\">");
            wxsString.AppendLine("         <UpgradeVersion Minimum=\"$(var.ProductVersion)\" OnlyDetect=\"yes\" Property=\"NEWERVERSIONDETECTED\"/>");
            wxsString.AppendLine("         <UpgradeVersion Minimum=\"0.0.0\" Maximum=\"$(var.ProductVersion)\" IncludeMinimum=\"yes\" IncludeMaximum=\"no\" ");
            wxsString.AppendLine("                         Property=\"OLDERVERSIONBEINGUPGRADED\"/>   ");
            wxsString.AppendLine("      </Upgrade>");
            wxsString.AppendLine("      <Condition Message=\"A newer version of this software is already installed.\">NOT NEWERVERSIONDETECTED</Condition>");
            wxsString.AppendLine("      <Property Id=\"MstscProperty\" Value=\"mstsc.exe\"/>");
            wxsString.AppendLine("      <Directory Id=\"TARGETDIR\" Name=\"SourceDir\">");
            wxsString.AppendLine(!PerUser
                ? "         <Directory Id=\"ProgramFilesFolder\">"
                : "         <Directory Id=\"LocalAppDataFolder\">");

            wxsString.AppendLine("            <Directory Id=\"INSTALLDIR\" Name=\"RemotePackages\">");
            wxsString.AppendLine("               <Component Id=\"ApplicationFiles\" Guid=\"$(var.AppFilesGuid)\">");
            wxsString.AppendLine("                  <File Id=\"rdpFile1\" Source=\"$(var.ProductBaseFileName).rdp\"/>");
            if (_hasIcon)
            {
                wxsString.AppendLine("                <File Id=\"rdpIcon1\" Source=\"$(var.ProductBaseFileName).ico\"/>");
            }
            // # Begin filetype association code
            if (!string.IsNullOrEmpty(FlatFileTypes))
            {
                foreach (var FileType in FlatFileTypes.Replace(".", "").Split(','))
                {
                    wxsString.Append("                <File Id=\"$(var.ProductProgID).").Append(FileType).Append(".ico\" Source=\"$(var.ProductBaseFileName).").Append(FileType).AppendLine(".ico\" />");
                    wxsString.Append("                <ProgId Id=\"remote.$(var.ProductProgID).").Append(FileType).Append("file\" Description=\"$(var.ProductName) ").Append(FileType).Append(" file\" Icon=\"$(var.ProductProgID).").Append(FileType).AppendLine(".ico\">");
                    wxsString.Append("                <Extension Id=\"").Append(FileType).Append("\" ContentType=\"application/").Append(FileType).AppendLine("\">");
                    wxsString.AppendLine("                <Verb Id=\"open\" Command=\"Open\" TargetProperty='MstscProperty' Argument='/REMOTEFILE:\"%1\" \"[INSTALLDIR]$(var.ProductBaseFileName).rdp\"' />");
                    wxsString.AppendLine("                </Extension>");
                    wxsString.AppendLine("                </ProgId>");
                }
            }
            // # End FTA code
            wxsString.AppendLine("               </Component>");
            wxsString.AppendLine("            </Directory>");
            wxsString.AppendLine("      </Directory>");
            if (ShortcutInStart)
            {
                wxsString.AppendLine("         <Directory Id=\"ProgramMenuFolder\">");
                if (ShortcutSubfolderInStart)
                {
                    wxsString.AppendLine("            <Directory Id=\"ProgramMenuSubfolder\" Name=\"$(var.ProductName)$(var.ProductRemoteTag)\">");
                }

                wxsString.AppendLine("               <Component Id=\"ApplicationStartShortcuts\" Guid=\"$(var.AppStartShortcutsGuid)\">");
                wxsString.AppendLine("                  <Shortcut Id=\"rdpStartShortcut1\" Name=\"$(var.ProductName)$(var.ProductRemoteTag)\" Description=\"$(var.ProductName)$(var.ProductRemoteTag)\" ");
                wxsString.Append("                            Target=\"[INSTALLDIR]$(var.ProductBaseFileName).rdp\" WorkingDirectory=\"INSTALLDIR\"");
                if (_hasIcon)
                {
                    wxsString.AppendLine(" Icon=\"rdpStartIcon.rdp\" IconIndex=\"0\">");
                    wxsString.AppendLine("                          <Icon Id=\"rdpStartIcon.rdp\" SourceFile=\"$(var.ProductBaseFileName).ico\" />");
                    wxsString.AppendLine("                </Shortcut>");
                }
                else
                {
                    wxsString.AppendLine("/>");
                }

                wxsString.AppendLine(@"                  <RegistryValue Root=""$(var.RegRoot)"" Key=""Software\RDP2MSI\$(var.ProductName)"" ");
                wxsString.AppendLine("                            Name=\"installed\" Type=\"integer\" Value=\"1\" KeyPath=\"yes\"/>");
                wxsString.AppendLine("                  <RemoveFolder Id=\"ProgramMenuSubfolder\" On=\"uninstall\"/>");
                wxsString.AppendLine("               </Component>");
                if (ShortcutSubfolderInStart)
                {
                    wxsString.AppendLine("            </Directory>");
                }

                wxsString.AppendLine("         </Directory>");
            }

            if (ShortcutOnDesktop)
            {
                wxsString.AppendLine("       <Directory Id=\"DesktopFolder\">");
                wxsString.AppendLine("         <Component Id=\"ApplicationDesktopShortcuts\" Guid=\"$(var.AppDeskShortcutsGuid)\">");
                wxsString.AppendLine("            <Shortcut Id=\"rdpDesktopShortcut1\" Name=\"$(var.ProductName)$(var.ProductRemoteTag)\" Description=\"$(var.ProductName)$(var.ProductRemoteTag)\" ");
                wxsString.Append("                      Target=\"[INSTALLDIR]$(var.ProductBaseFileName).rdp\" WorkingDirectory=\"INSTALLDIR\"");
                if (_hasIcon)
                {
                    wxsString.AppendLine(" Icon=\"rdpDeskIcon.rdp\" IconIndex=\"0\">");
                    wxsString.AppendLine("                      <Icon Id=\"rdpDeskIcon.rdp\" SourceFile=\"$(var.ProductBaseFileName).ico\" />");
                    wxsString.AppendLine("            </Shortcut>");
                }
                else
                {
                    wxsString.AppendLine("/>");
                }

                wxsString.AppendLine(@"           <RegistryValue Root=""$(var.RegRoot)"" Key=""Software\RDP2MSI\$(var.ProductName)"" ");
                wxsString.AppendLine("                      Name=\"installed\" Type=\"integer\" Value=\"1\" KeyPath=\"yes\"/>");
                wxsString.AppendLine("         </Component>");
                wxsString.AppendLine("         </Directory>");
            }

            wxsString.AppendLine("      </Directory>");
            wxsString.AppendLine("      <InstallExecuteSequence>");
            wxsString.AppendLine("         <RemoveExistingProducts After=\"InstallValidate\"/>");
            wxsString.AppendLine("      </InstallExecuteSequence>");
            wxsString.Append(' ').AppendLine();
            wxsString.AppendLine("      <Feature Id=\"DefaultFeature\" Level=\"1\">");
            wxsString.AppendLine("         <ComponentRef Id=\"ApplicationFiles\"/>");
            if (ShortcutInStart)
            {
                wxsString.AppendLine("         <ComponentRef Id=\"ApplicationStartShortcuts\"/>");
            }

            if (ShortcutOnDesktop)
            {
                wxsString.AppendLine("       <ComponentRef Id=\"ApplicationDesktopShortcuts\"/>");
            }

            wxsString.AppendLine("      </Feature>");
            if (_hasIcon)
            {
                wxsString.AppendLine("<Icon Id=\"rdpARPIcon.rdp\" SourceFile=\"$(var.ProductBaseFileName).ico\" />");
                wxsString.AppendLine("<Property Id=\"ARPPRODUCTICON\" Value=\"rdpARPIcon.rdp\" />");
            }

            wxsString.AppendLine("   </Product>");
            wxsString.AppendLine("</Wix>");
            return wxsString.ToString();
        }

        public string ReadRDPProperty(string rdpProperty)
        {
            var rdpFileLines = _rdpFileContents.Split(Environment.NewLine[0]);
            var rdpValue = "";
            foreach (var rdpLine in rdpFileLines)
            {
                var newLine = rdpLine.Replace("|", string.Empty);
                var rdpLineSplit = newLine.Split(':');

                if (rdpLineSplit.Length > 3)
                {
                    throw new Exception("Incorrect RDP text in file.");
                }

                if ((rdpLineSplit[0] ?? "") == (rdpProperty ?? ""))
                {
                    rdpValue = rdpLineSplit[2];
                }
            }

            return rdpValue;
        }

        private static string GenerateGuidFromString(string theString)
        {
            var TheHash = GetMd5Hash(theString);
            var MyGuid = new Guid(TheHash);
            return MyGuid.ToString();
        }

        private static string GetMd5Hash(string strToHash)
        {
            using (var md5Obj = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                var bytesToHash = Encoding.ASCII.GetBytes(strToHash);
                bytesToHash = md5Obj.ComputeHash(bytesToHash);
                var strResult = "";
                foreach (var b in bytesToHash)
                {
                    strResult += b.ToString("x2");
                }

                return strResult;
            }
        }

        private static int RunWait(string app, string parameters)
        {
            var proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.FileName = app;
            proc.StartInfo.Arguments = parameters;
            proc.Start();
            proc.WaitForExit();
            return proc.ExitCode;
        }
    }
}