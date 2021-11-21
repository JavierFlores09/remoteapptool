using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace RDP2MSIlib.New
{
    public class RDP
    {
        private string rdpFilePath;

        public string RDPFilePath
        {
            get
            {
                return rdpFilePath;
            }

            set
            {
                rdpFilePath = value;
                ReadRDPfile();
            }
        }

        private string ProductName; // Full remoteapp name as read from RDP file
        public string FlatFileTypes = "";
        public bool PerUser = false;
        private string ProductBaseFileName; // RDP filename minus the file extension
        public string ProductPublisher;
        public string ProductVersion = "1.0.0.0";
        public bool ProductUpgradeRandom = false;
        public string ProductRemoteTag = "remote";
        public bool ShortcutInStart = true;
        public bool ShortcutSubfolderInStart = true;
        public bool ShortcutOnDesktop = true;
        private string rdpFileContents;
        private bool hasIcon;
        private bool rdpInTemp;

        public void CreateMSI(string DestinationPath = "")
        {
            // Check for wix, exit if not available
            if (!WixInstalled())
            {
                return;
            }

            // Get RDP file parent folder path
            string rdpParentFolder = Path.GetDirectoryName(rdpFilePath);

            // Get the RDP filename and filename minus extension
            var fileName = new FileInfo(rdpFilePath).Name;
            ProductBaseFileName = Path.GetFileNameWithoutExtension(fileName);

            // If DestinationPath not defined then set the path to the same as the rdp file
            if (string.IsNullOrEmpty(DestinationPath))
            {
                DestinationPath = rdpParentFolder + @"\" + ProductBaseFileName + ".msi";
            }

            // Set iconpath (whether it exists or not)
            string IconFilePath = rdpParentFolder + @"\" + ProductBaseFileName + ".ico";

            // Check for icon, set "HasIcon" to true if found
            hasIcon = File.Exists(IconFilePath);

            // Get RemoteApp names (short and full) from RDP file contents
            string RemoteAppFullName = ReadRDPProperty("remoteapplicationname");
            string RemoteAppShortName = ReadRDPProperty("remoteapplicationprogram");

            // Define wix temp file paths
            string TempPath = Environment.GetEnvironmentVariable("TEMP");
            string wxsPath = TempPath + @"\" + ProductBaseFileName + ".wxs";
            string wixobjPath = TempPath + @"\" + ProductBaseFileName + ".wixobj";
            string wixpdbPath = TempPath + @"\" + ProductBaseFileName + ".wixpdb";
            string msiPath = TempPath + @"\" + ProductBaseFileName + ".msi";
            string rdpTempPath = TempPath + @"\" + ProductBaseFileName + ".rdp";
            string icoTempPath = TempPath + @"\" + ProductBaseFileName + ".ico";

            // Define temp files to delete
            var FilesToDelete = new List<string>(new string[] { wxsPath, wixobjPath, wixpdbPath });
            var LockCheck = new LockChecker.New.LockChecker();
            string FileLocked;
            bool SkipFile = false;
            // Check if rdp file is already in TEMP folder
            if ((rdpParentFolder ?? "") == (TempPath ?? ""))
            {
                rdpInTemp = true;
            }

            // if RDP file not in temp, copy to temp
            if (!rdpInTemp)
            {
                FileLocked = LockCheck.CheckLock(rdpTempPath);
                while (FileLocked != "No locks")
                {
                    if (MessageBox.Show($"The file {rdpTempPath} is currently locked.  Lock information:{FileLocked}{Environment.NewLine}Do you want to try again?", "File Locked", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        FileLocked = LockCheck.CheckLock(rdpTempPath);
                    }
                    else
                    {
                        MessageBox.Show($"The following file will not be deleted:{Environment.NewLine}{rdpTempPath}");
                        SkipFile = true;
                        FileLocked = "No locks";
                    }
                }

                if (!SkipFile)
                {
                    File.Copy(rdpFilePath, rdpTempPath, true);
                }

                FilesToDelete.Add(rdpTempPath);
                if (hasIcon)
                {
                    FileLocked = LockCheck.CheckLock(icoTempPath);
                    while (FileLocked != "No locks")
                    {
                        if (MessageBox.Show($"The file {icoTempPath} is currently locked.  Lock information:{FileLocked}{Environment.NewLine}Do you want to try again?", "File Locked", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            FileLocked = LockCheck.CheckLock(icoTempPath);
                        }
                        else
                        {
                            MessageBox.Show($"The following file will not be deleted:{Environment.NewLine}{icoTempPath}");
                            SkipFile = true;
                            FileLocked = "No locks";
                        }
                    }

                    if (!SkipFile)
                    {
                        File.Copy(IconFilePath, icoTempPath, true);
                    }

                    FilesToDelete.Add(icoTempPath);
                }
            }

            // Save WXS file containing generated WXS string
            File.WriteAllText(wxsPath, GenerateWXSString());
            var CandlePath = Path.Combine(WixPath(), "candle.exe ");
            var LightPath = Path.Combine(WixPath(), "light.exe ");

            // Run Candle.exe and Light.exe to process wxs file
            int CandleExitCode = RunWait(CandlePath, $"-out \"{wixobjPath}\" \"{wxsPath}\"");
            // If Not CandleExitCode = 0 Then Exit Sub
            if (CandleExitCode != 0)
            {
                throw new Exception("Could not start candle.exe");
            }
            int LightExitCode = RunWait(LightPath, $"-out \"{msiPath}\" \"{wixobjPath}\"");
            // If Not LightExitCode = 0 Then Exit Sub
            if (LightExitCode != 0)
            {
                throw new Exception("Could not start light.exe");
            }

            // Move MSI file to destination and delete temp files
            FileLocked = LockCheck.CheckLock(DestinationPath);
            while (FileLocked != "No locks")
            {
                if (MessageBox.Show($"The file {DestinationPath} is currently locked.  Lock information:{FileLocked}{Environment.NewLine}Do you want to try again?", "File Locked", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FileLocked = LockCheck.CheckLock(DestinationPath);
                }
                else
                {
                    _ = MessageBox.Show($"The following file will not be deleted:{Environment.NewLine}{DestinationPath}");
                    SkipFile = true;
                    FileLocked = "No locks";
                }
            }

            if (!SkipFile)
            {
                File.Move(msiPath, DestinationPath);
            }

            DeleteFiles(FilesToDelete);
        }

        public bool WixInstalled()
        {
            return !string.IsNullOrEmpty(WixPath());
        }

        private string WixPath()
        {
            const string searchExe = "candle.exe";
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WIX")))
            {
                return $"{Environment.GetEnvironmentVariable("WIX")}bin";
            }
            else if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wix", searchExe)))
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "wix");
            }
            else if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wix", "bin", searchExe)))
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "wix", "bin");
            }

            return string.Empty;
        }

        private void DeleteFiles(List<string> filesToDelete)
        {
            foreach (var dFile in filesToDelete)
            {
                var LockCheck = new LockChecker.New.LockChecker();
                string FileLocked;
                bool SkipFile = false;
                FileLocked = LockCheck.CheckLock(dFile);
                while (FileLocked != "No locks")
                {
                    if (MessageBox.Show($"The file {dFile} is currently locked.  Lock information:{FileLocked}{Environment.NewLine}Do you want to try again?", "File Locked", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

        private void ReadRDPfile()
        {
            // Check if RDP file exists
            if (!File.Exists(rdpFilePath))
            {
                return;
            }

            // Check that RDP file is an RDP file
            if (!rdpFilePath.EndsWith(".rdp", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            // Read RDP file into variable
            rdpFileContents = File.ReadAllText(rdpFilePath);

            // Check RDP file contains a remoteapplicationname value
            // If ReadRDPProperty("remoteapplicationname") = "" Then Exit Sub

            // Check if RDP file contains a server address
            if (string.IsNullOrEmpty(ReadRDPProperty("full address")))
            {
                return;
            }

            // Read variables
            ProductName = ReadRDPProperty("remoteapplicationname");
            if (string.IsNullOrEmpty(ProductName))
            {
                ProductName = Path.GetFileNameWithoutExtension(rdpFilePath);
            }

            if (ProductPublisher == null)
            {
                ProductPublisher = ProductName;
            }
        }

        public string ProductUpgradeCode()
        {
            // random or generated - maybe allow caller to define?

            // Check if ProductUpgradeRandom is true, create a random productcode if so, otherwise generate it from the productname
            return ProductUpgradeRandom ? GenerateGUIDfromString(new Random().Next().ToString()) : GenerateGUIDfromString(ProductName);
        }

        public string MakeProgID(string AppName)
        {
            var rx = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_]");
            return rx.Replace(AppName, "_");
        }

        private string GenerateWXSString()
        {
            if (string.IsNullOrEmpty(ProductPublisher))
            {
                ProductPublisher = ProductName;
            }

            string RegRoot = "HKLM";
            if (PerUser)
            {
                RegRoot = "HKCU";
            }

            var AppFilesGuid = GenerateGUIDfromString($"AppFiles{ProductUpgradeCode()}");
            var AppStartShortcutsGuid = GenerateGUIDfromString($"AppStartShortcuts{ProductUpgradeCode()}");
            var AppDeskShortcutsGuid = GenerateGUIDfromString($"AppDeskShortcuts{ProductUpgradeCode()}");
            string ProgID = MakeProgID(ProductBaseFileName);
            var wxsString = new StringBuilder();
            wxsString.AppendLine("<?xml version=\"1.0\"?>");
            wxsString.Append("<?define ProductVersion = \"").Append(ProductVersion).AppendLine("\"?>");
            wxsString.Append("<?define ProductUpgradeCode = \"").Append(ProductUpgradeCode()).AppendLine("\"?>");
            wxsString.Append("<?define AppFilesGuid = \"").Append(AppFilesGuid).AppendLine("\"?>");
            wxsString.Append("<?define AppStartShortcutsGuid = \"").Append(AppStartShortcutsGuid).AppendLine("\"?>");
            wxsString.Append("<?define AppDeskShortcutsGuid = \"").Append(AppDeskShortcutsGuid).AppendLine("\"?>");
            wxsString.Append("<?define ProductName = \"").Append(ProductName).AppendLine("\"?>");
            wxsString.Append("<?define ProductPublisher = \"").Append(ProductPublisher).AppendLine("\"?>");
            wxsString.Append("<?define ProductBaseFileName = \"").Append(ProductBaseFileName).AppendLine("\"?>");
            wxsString.Append("<?define RegRoot = \"").Append(RegRoot).AppendLine("\"?>");
            if (!string.IsNullOrEmpty(FlatFileTypes))
            {
                wxsString.Append("<?define ProductProgID = \"").Append(ProgID).AppendLine("\"?>");
            }

            if (!string.IsNullOrEmpty(ProductRemoteTag))
            {
                ProductRemoteTag = " (" + ProductRemoteTag + ")";
            }

            wxsString.Append("<?define ProductRemoteTag = \"").Append(ProductRemoteTag).AppendLine("\"?>");
            wxsString.AppendLine("<Wix xmlns=\"http://schemas.microsoft.com/wix/2006/wi\">");
            wxsString.AppendLine("   <Product Id=\"*\" UpgradeCode=\"$(var.ProductUpgradeCode)\" ");
            wxsString.AppendLine("            Name=\"$(var.ProductName)$(var.ProductRemoteTag)\" Version=\"$(var.ProductVersion)\" Manufacturer=\"$(var.ProductPublisher)\" Language=\"1033\">");
            wxsString.Append("      <Package InstallerVersion=\"200\" Compressed=\"yes\" Comments=\"Windows Installer Package\"");
            if (!PerUser)
            {
                wxsString.Append(" InstallScope=\"perMachine\"");
            }
            else
            {
                wxsString.Append(" InstallPrivileges=\"limited\"");
            }

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
            if (!PerUser)
            {
                wxsString.AppendLine("         <Directory Id=\"ProgramFilesFolder\">");
            }
            else
            {
                wxsString.AppendLine("         <Directory Id=\"LocalAppDataFolder\">");
            }

            wxsString.AppendLine("            <Directory Id=\"INSTALLDIR\" Name=\"RemotePackages\">");
            wxsString.AppendLine("               <Component Id=\"ApplicationFiles\" Guid=\"$(var.AppFilesGuid)\">");
            wxsString.AppendLine("                  <File Id=\"rdpFile1\" Source=\"$(var.ProductBaseFileName).rdp\"/>");
            if (hasIcon)
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
                    // wxsString += "                 <RegistryValue Root=""$(var.RegRoot)"" Key=""SOFTWARE\Classes\remote.$(var.ProductProgID)." & FileType & "file"" Name=""FriendlyTypeName"" Value=""$(var.ProductName) " & FileType & " file"" Type=""string"" />"
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
                if (hasIcon)
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
                if (hasIcon)
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
            wxsString.AppendLine(" ");
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
            if (hasIcon)
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
            var rdpFileLines = rdpFileContents.Split(Environment.NewLine[0]);
            string rdpValue = "";
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

        private string GenerateGUIDfromString(string TheString)
        {
            string TheHash = GetMD5Hash(TheString);
            var MyGuid = new Guid(TheHash);
            return MyGuid.ToString();
        }

        private string GetMD5Hash(string strToHash)
        {
            var md5Obj = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var bytesToHash = System.Text.Encoding.ASCII.GetBytes(strToHash);
            bytesToHash = md5Obj.ComputeHash(bytesToHash);
            string strResult = "";
            foreach (var b in bytesToHash)
            {
                strResult += b.ToString("x2");
            }

            return strResult;
        }

        private int RunWait(string App, string Parameters)
        {
            var proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.FileName = App;
            proc.StartInfo.Arguments = Parameters;
            proc.Start();
            proc.WaitForExit();
            return proc.ExitCode;
        }
    }
}