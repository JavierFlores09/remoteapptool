using System;
using System.Collections.Generic;
using System.IO;

namespace RDP2MSI.New
{
    public static class RDP2MSI
    {
        public static void Main(string[] args)
        {
            var cmdRdpPath = "";
            var cmdSwitches = "";
            LogText("RDP2MSI" + Environment.NewLine);
            if (args.Length == 0)
            {
                ShowUsage();
                Environment.Exit(0);
            }
            else if (args.Length == 1)
            {
                cmdRdpPath = args[0];
            }
            else if (args.Length == 2)
            {
                if (args[0].StartsWith("/"))
                {
                    cmdSwitches = args[0];
                    cmdRdpPath = args[1];
                }
                else
                {
                    cmdSwitches = args[1];
                    cmdRdpPath = args[0];
                }
            }
            else if (args.Length > 2)
            {
                LogText("Error: Too many parameters provided.", true);
            }

            var relCmdRdpPath = Path.Combine(Directory.GetCurrentDirectory(), cmdRdpPath);
            if (!cmdRdpPath.Contains(":"))
                cmdRdpPath = relCmdRdpPath;
            if (!File.Exists(cmdRdpPath))
                LogText("Error: Unable to find RDP file: " + cmdRdpPath, true);
            if (cmdSwitches.IndexOf("D", StringComparison.OrdinalIgnoreCase) < 0 && cmdSwitches.IndexOf("S", StringComparison.OrdinalIgnoreCase) < 0)
            {
                cmdSwitches += "DS";
            }

            PrepareRDP2MSI(cmdRdpPath, cmdSwitches.ToUpper());
        }

        public static void ShowUsage()
        {
            LogText("Usage: rdp2msi.exe [/DSNATU] rdpfile.rdp");
            LogText("");
            LogText("Switches:");
            LogText("");
            LogText("  /D     MSI will deploy desktop shortcut.");
            LogText("  /S     MSI will deploy shortcut in Start Menu > Programs > (AppName)");
            LogText("  /N     Requires /S. MSI will not create subfolder in Start Menu > Programs.");
            LogText("  /A     Generate upgrade code based on app name. By default, it is random.");
            LogText("  /T     Do not include the (remote) tag on deployed shortcuts.");
            LogText("  /U     MSI installs per-user. By default, MSI will install per-machine.");
            LogText("");
            LogText("If no switches are specified, /DS is implied.");
            LogText("");
            LogText("If an ICO file with the same name as the RDP file exists, it will be used.");
            LogText("");
        }

        private static void PrepareRDP2MSI(string rdpFilePath, string cmdSwitches = "DS")
        {
            if (!File.Exists(rdpFilePath))
                LogText("Error: Unable to find RDP file: " + rdpFilePath, true);
            if (!rdpFilePath.EndsWith(".rdp", StringComparison.OrdinalIgnoreCase))
                LogText("Error: Input file must be an RDP file.", true);
            var RDP = new RDP2MSIlib.New.RDP
            {
                RDPFilePath = rdpFilePath
            };
            if (!RDP.WixInstalled())
                LogText("Error: WiX Toolset not found. If you have just installed it, please reboot and try again.", true);
            LogText("RDP file: " + rdpFilePath);
            var RemoteAppFullName = RDP.ReadRDPProperty("remoteapplicationname");
            var RDPFullAddress = RDP.ReadRDPProperty("full address");
            if (string.IsNullOrEmpty(RDPFullAddress))
                LogText("Error: RDP file does not contain a Remote Desktop Server address.", true);
            if (string.IsNullOrEmpty(RemoteAppFullName))
            {
                // Full desktop connection
                LogText("RDP file type: Full desktop session");
            }
            else
            {
                // RemoteApp connection
                LogText("RDP file type: RemoteApp");
                LogText("App full name: " + RemoteAppFullName);
            }

            LogText("Command line switches: " + cmdSwitches.ToUpper());
            RDP.ShortcutInStart = cmdSwitches.Contains("S");
            RDP.ShortcutOnDesktop = cmdSwitches.Contains("D");
            RDP.ShortcutSubfolderInStart = !cmdSwitches.Contains("N");
            var ShortcutLocations = new List<string>();
            if (RDP.ShortcutInStart)
            {
                if (RDP.ShortcutSubfolderInStart)
                {
                    ShortcutLocations.Add("Start menu (subfolder)");
                }
                else
                {
                    ShortcutLocations.Add("Start menu (top level)");
                }
            }

            if (RDP.ShortcutOnDesktop)
                ShortcutLocations.Add("Desktop");
            LogText("Shortcut locations: " + string.Join(", ", ShortcutLocations));
            RDP.ProductUpgradeRandom = !cmdSwitches.Contains("A");
            LogText($"MSI upgrade code: {RDP.ProductUpgradeCode()}");
            if (cmdSwitches.Contains("T"))
                RDP.ProductRemoteTag = string.Empty;
            RDP.PerUser = cmdSwitches.Contains("U");
            if (RDP.PerUser)
            {
                LogText("MSI install context: User");
            }
            else
            {
                LogText("MSI install context: Machine");
            }

            var ShortcutTag = "NONE";
            if (!string.IsNullOrEmpty(RDP.ProductRemoteTag))
                ShortcutTag = "(" + RDP.ProductRemoteTag + ")";
            LogText("Shortcut tag: " + ShortcutTag);
            LogText("");
            LogText("Generating MSI...");
            RDP.CreateMSI();
        }

        private static void LogText(string theText, bool doExit = false)
        {
            Console.WriteLine(theText);
            if (doExit)
            {
                // CleanupTempFiles()
                Environment.Exit(0);
            }
        }
    }
}