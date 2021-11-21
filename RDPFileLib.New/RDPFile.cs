using System;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace RDPFileLib.New
{
    public class RDPFile
    {
        public int AdministrativeSession;
        public int AllowDesktopComposition;
        public int AllowFontSmoothing;
        public string AlternateFullAddress = string.Empty;
        public string AlternateShell = string.Empty;
        public int Audiocapturemode;
        public int Audiomode;
        public int Audioqualitymode;
        public int AuthenticationLevel = 2;
        public int AutoreconnectMaxRetries = 20;
        public int AutoreconnectionEnabled = 1;
        public int Bandwidthautodetect = 1;
        public int Bitmapcachepersistenable = 1;
        public int Bitmapcachesize = 1500;
        public int Compression = 1;
        public int ConnectToConsole;
        public int ConnectionType = 2;
        public int DesktopSizeId;
        public int Desktopheight = 600;
        public int Desktopwidth = 800;
        public string Devicestoredirect = string.Empty;
        public int DisableCtrlAltDel = 1;
        public int DisableFullWindowDrag = 1;
        public int DisableMenuAnims = 1;
        public int DisableThemes;
        public int DisableWallpaper = 1;
        public int Disableconnectionsharing;
        public int Disableremoteappcapscheck;
        public int Displayconnectionbar = 1;
        public string Domain = string.Empty;
        public string Drivestoredirect = string.Empty;
        public int Enablecredsspsupport = 1;
        public int Enablesuperpan;
        public string FullAddress = string.Empty;
        public int Gatewaycredentialssource = 4;
        public string Gatewayhostname = string.Empty;
        public int Gatewayprofileusagemethod;
        public int Gatewayusagemethod = 4;
        public int Keyboardhook = 2;
        public int NegotiateSecurityLayer = 1;
        public int Networkautodetect = 1;
        // Public password_51 As Binary
        public int Pinconnectionbar = 1;
        public int PromptForCredentials;
        public int PromptForCredentialsOnClient;
        public int Promptcredentialonce = 1;
        public int PublicMode;
        public int Redirectclipboard = 1;
        public int Redirectcomports;
        public int Redirectdirectx = 1;
        public int Redirectdrives;
        public int Redirectposdevices;
        public int Redirectprinters = 1;
        public int Redirectsmartcards = 1;
        public string Remoteapplicationcmdline = string.Empty;
        public int Remoteapplicationexpandcmdline = 1;
        public int Remoteapplicationexpandworkingdir;
        public string Remoteapplicationfile = string.Empty;
        public string Remoteapplicationfileextensions = string.Empty;
        public string Remoteapplicationicon = string.Empty;
        public int Remoteapplicationmode;
        public string Remoteapplicationname = string.Empty;
        public string Remoteapplicationprogram = string.Empty;
        public int ScreenModeId = 2;
        public int ServerPort = 3389;
        public int SessionBpp = 32;
        public string ShellWorkingDirectory = string.Empty;
        public int SmartSizing;
        public int SpanMonitors;
        public int Superpanaccelerationfactor = 1;
        public string Usbdevicestoredirect = string.Empty;
        public int UseMultimon;
        public string Username = string.Empty;
        public int Videoplaybackmode = 1;
        public string Winposstr = "0,3,0,0,800,600";

        public void SaveRdPfile(string filePath, bool saveDefaultSettings = false)
        {
            var LockCheck = new LockChecker.New.LockChecker();
            string FileLocked;
            var SkipFile = false;
            FileLocked = LockCheck.CheckLock(filePath);
            while (FileLocked != "No locks")
            {
                if (MessageBox.Show($"The file {filePath} is currently locked.  Lock information:{FileLocked}{Environment.NewLine}Do you want to try again?", "File Locked", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FileLocked = LockCheck.CheckLock(filePath);
                }
                else
                {
                    _ = MessageBox.Show($"The following file will not be copied:{Environment.NewLine}{filePath}");
                    SkipFile = true;
                    FileLocked = "No locks";
                }
            }

            if (!SkipFile)
            {
                File.WriteAllText(filePath, GetRdPstring(saveDefaultSettings));
            }
        }

        public string GetRdPstring(bool saveDefaultSettings = false)
        {
            var rdpString = new StringBuilder();
            var DefaultRDP = new RDPFile();
            if (saveDefaultSettings || DefaultRDP.AdministrativeSession != AdministrativeSession)
            {
                rdpString = rdpString.Append("administrative session:").Append(AdministrativeSession.GetTypeNameForRdp()).Append(':').AppendLine(AdministrativeSession.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.AllowDesktopComposition != AllowDesktopComposition)
            {
                rdpString = rdpString.Append("allow desktop composition:").Append(AllowDesktopComposition.GetTypeNameForRdp()).Append(':').AppendLine(AllowDesktopComposition.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.AllowFontSmoothing != AllowFontSmoothing)
            {
                rdpString = rdpString.Append("allow font smoothing:").Append(AllowFontSmoothing.GetTypeNameForRdp()).Append(':').AppendLine(AllowFontSmoothing.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.AlternateFullAddress ?? "") != (AlternateFullAddress ?? ""))
            {
                rdpString = rdpString.Append("alternate full address:").Append(AlternateFullAddress.GetTypeNameForRdp()).Append(':').AppendLine(AlternateFullAddress);
            }

            if (saveDefaultSettings || (DefaultRDP.AlternateShell ?? "") != (AlternateShell ?? ""))
            {
                rdpString = rdpString.Append("alternate shell:").Append(AlternateShell.GetTypeNameForRdp()).Append(':').AppendLine(AlternateShell);
            }

            if (saveDefaultSettings || DefaultRDP.Audiocapturemode != Audiocapturemode)
            {
                rdpString = rdpString.Append("audiocapturemode:").Append(Audiocapturemode.GetTypeNameForRdp()).Append(':').AppendLine(Audiocapturemode.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Audiomode != Audiomode)
            {
                rdpString = rdpString.Append("audiomode:").Append(Audiomode.GetTypeNameForRdp()).Append(':').AppendLine(Audiomode.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Audioqualitymode != Audioqualitymode)
            {
                rdpString = rdpString.Append("audioqualitymode:").Append(Audioqualitymode.GetTypeNameForRdp()).Append(':').AppendLine(Audioqualitymode.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.AuthenticationLevel != AuthenticationLevel)
            {
                rdpString = rdpString.Append("authentication level:").Append(AuthenticationLevel.GetTypeNameForRdp()).Append(':').AppendLine(AuthenticationLevel.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.AutoreconnectMaxRetries != AutoreconnectMaxRetries)
            {
                rdpString = rdpString.Append("autoreconnect max retries:").Append(AutoreconnectMaxRetries.GetTypeNameForRdp()).Append(':').AppendLine(AutoreconnectMaxRetries.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.AutoreconnectionEnabled != AutoreconnectionEnabled)
            {
                rdpString = rdpString.Append("autoreconnection enabled:").Append(AutoreconnectionEnabled.GetTypeNameForRdp()).Append(':').AppendLine(AutoreconnectionEnabled.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Bandwidthautodetect != Bandwidthautodetect)
            {
                rdpString = rdpString.Append("bandwidthautodetect:").Append(Bandwidthautodetect.GetTypeNameForRdp()).Append(':').AppendLine(Bandwidthautodetect.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Bitmapcachepersistenable != Bitmapcachepersistenable)
            {
                rdpString = rdpString.Append("bitmapcachepersistenable:").Append(Bitmapcachepersistenable.GetTypeNameForRdp()).Append(':').AppendLine(Bitmapcachepersistenable.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Bitmapcachesize != Bitmapcachesize)
            {
                rdpString = rdpString.Append("bitmapcachesize:").Append(Bitmapcachesize.GetTypeNameForRdp()).Append(':').AppendLine(Bitmapcachesize.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Compression != Compression)
            {
                rdpString = rdpString.Append("compression:").Append(Compression.GetTypeNameForRdp()).Append(':').AppendLine(Compression.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.ConnectToConsole != ConnectToConsole)
            {
                rdpString = rdpString.Append("connect to console:").Append(ConnectToConsole.GetTypeNameForRdp()).Append(':').AppendLine(ConnectToConsole.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.ConnectionType != ConnectionType)
            {
                rdpString = rdpString.Append("connection type:").Append(ConnectionType.GetTypeNameForRdp()).Append(':').AppendLine(ConnectionType.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.DesktopSizeId != DesktopSizeId)
            {
                rdpString = rdpString.Append("desktop size id:").Append(DesktopSizeId.GetTypeNameForRdp()).Append(':').AppendLine(DesktopSizeId.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Desktopheight != Desktopheight)
            {
                rdpString = rdpString.Append("desktopheight:").Append(Desktopheight.GetTypeNameForRdp()).Append(':').AppendLine(Desktopheight.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Desktopwidth != Desktopwidth)
            {
                rdpString = rdpString.Append("desktopwidth:").Append(Desktopwidth.GetTypeNameForRdp()).Append(':').AppendLine(Desktopwidth.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.Devicestoredirect ?? "") != (Devicestoredirect ?? ""))
            {
                rdpString = rdpString.Append("devicestoredirect:").Append(Devicestoredirect.GetTypeNameForRdp()).Append(':').AppendLine(Devicestoredirect);
            }

            if (saveDefaultSettings || DefaultRDP.DisableCtrlAltDel != DisableCtrlAltDel)
            {
                rdpString = rdpString.Append("disable ctrl+alt+del:").Append(DisableCtrlAltDel.GetTypeNameForRdp()).Append(':').AppendLine(DisableCtrlAltDel.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.DisableFullWindowDrag != DisableFullWindowDrag)
            {
                rdpString = rdpString.Append("disable full window drag:").Append(DisableFullWindowDrag.GetTypeNameForRdp()).Append(':').AppendLine(DisableFullWindowDrag.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.DisableMenuAnims != DisableMenuAnims)
            {
                rdpString = rdpString.Append("disable menu anims:").Append(DisableMenuAnims.GetTypeNameForRdp()).Append(':').AppendLine(DisableMenuAnims.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.DisableThemes != DisableThemes)
            {
                rdpString = rdpString.Append("disable themes:").Append(DisableThemes.GetTypeNameForRdp()).Append(':').AppendLine(DisableThemes.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.DisableWallpaper != DisableWallpaper)
            {
                rdpString = rdpString.Append("disable wallpaper:").Append(DisableWallpaper.GetTypeNameForRdp()).Append(':').AppendLine(DisableWallpaper.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Disableconnectionsharing != Disableconnectionsharing)
            {
                rdpString = rdpString.Append("disableconnectionsharing:").Append(Disableconnectionsharing.GetTypeNameForRdp()).Append(':').AppendLine(Disableconnectionsharing.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Disableremoteappcapscheck != Disableremoteappcapscheck)
            {
                rdpString = rdpString.Append("disableremoteappcapscheck:").Append(Disableremoteappcapscheck.GetTypeNameForRdp()).Append(':').AppendLine(Disableremoteappcapscheck.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Displayconnectionbar != Displayconnectionbar)
            {
                rdpString = rdpString.Append("displayconnectionbar:").Append(Displayconnectionbar.GetTypeNameForRdp()).Append(':').AppendLine(Displayconnectionbar.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.Domain ?? "") != (Domain ?? ""))
            {
                rdpString = rdpString.Append("domain:").Append(Domain.GetTypeNameForRdp()).Append(':').AppendLine(Domain);
            }

            if (saveDefaultSettings || (DefaultRDP.Drivestoredirect ?? "") != (Drivestoredirect ?? ""))
            {
                rdpString = rdpString.Append("drivestoredirect:").Append(Drivestoredirect.GetTypeNameForRdp()).Append(':').AppendLine(Drivestoredirect);
            }

            if (saveDefaultSettings || DefaultRDP.Enablecredsspsupport != Enablecredsspsupport)
            {
                rdpString = rdpString.Append("enablecredsspsupport:").Append(Enablecredsspsupport.GetTypeNameForRdp()).Append(':').AppendLine(Enablecredsspsupport.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Enablesuperpan != Enablesuperpan)
            {
                rdpString = rdpString.Append("enablesuperpan:").Append(Enablesuperpan.GetTypeNameForRdp()).Append(':').AppendLine(Enablesuperpan.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.FullAddress ?? "") != (FullAddress ?? ""))
            {
                rdpString = rdpString.Append("full address:").Append(FullAddress.GetTypeNameForRdp()).Append(':').AppendLine(FullAddress);
            }

            if (saveDefaultSettings || DefaultRDP.Gatewaycredentialssource != Gatewaycredentialssource)
            {
                rdpString = rdpString.Append("gatewaycredentialssource:").Append(Gatewaycredentialssource.GetTypeNameForRdp()).Append(':').AppendLine(Gatewaycredentialssource.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.Gatewayhostname ?? "") != (Gatewayhostname ?? ""))
            {
                rdpString = rdpString.Append("gatewayhostname:").Append(Gatewayhostname.GetTypeNameForRdp()).Append(':').AppendLine(Gatewayhostname);
            }

            if (saveDefaultSettings || DefaultRDP.Gatewayprofileusagemethod != Gatewayprofileusagemethod)
            {
                rdpString = rdpString.Append("gatewayprofileusagemethod:").Append(Gatewayprofileusagemethod.GetTypeNameForRdp()).Append(':').AppendLine(Gatewayprofileusagemethod.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Gatewayusagemethod != Gatewayusagemethod)
            {
                rdpString = rdpString.Append("gatewayusagemethod:").Append(Gatewayusagemethod.GetTypeNameForRdp()).Append(':').AppendLine(Gatewayusagemethod.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Keyboardhook != Keyboardhook)
            {
                rdpString = rdpString.Append("keyboardhook:").Append(Keyboardhook.GetTypeNameForRdp()).Append(':').AppendLine(Keyboardhook.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.NegotiateSecurityLayer != NegotiateSecurityLayer)
            {
                rdpString = rdpString.Append("negotiate security layer:").Append(NegotiateSecurityLayer.GetTypeNameForRdp()).Append(':').AppendLine(NegotiateSecurityLayer.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Networkautodetect != Networkautodetect)
            {
                rdpString = rdpString.Append("networkautodetect:").Append(Networkautodetect.GetTypeNameForRdp()).Append(':').AppendLine(Networkautodetect.ToString());
            }
            // If SaveDefaultSettings Or Not DefaultRDP.password_51 = password_51 Then RDPstring += "password 51" & ":" & password_51.GetType().ToString.Replace("System.", "").ToLower.Substring(0, 1) & ":" & password_51.ToString & vbCrLf
            if (saveDefaultSettings || DefaultRDP.Pinconnectionbar != Pinconnectionbar)
            {
                rdpString = rdpString.Append("pinconnectionbar:").Append(Pinconnectionbar.GetTypeNameForRdp()).Append(':').AppendLine(Pinconnectionbar.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.PromptForCredentials != PromptForCredentials)
            {
                rdpString = rdpString.Append("prompt for credentials:").Append(PromptForCredentials.GetTypeNameForRdp()).Append(':').AppendLine(PromptForCredentials.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.PromptForCredentialsOnClient != PromptForCredentialsOnClient)
            {
                rdpString = rdpString.Append("prompt for credentials on client:").Append(PromptForCredentialsOnClient.GetTypeNameForRdp()).Append(':').AppendLine(PromptForCredentialsOnClient.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Promptcredentialonce != Promptcredentialonce)
            {
                rdpString = rdpString.Append("promptcredentialonce:").Append(Promptcredentialonce.GetTypeNameForRdp()).Append(':').AppendLine(Promptcredentialonce.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.PublicMode != PublicMode)
            {
                rdpString = rdpString.Append("public mode:").Append(PublicMode.GetTypeNameForRdp()).Append(':').AppendLine(PublicMode.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Redirectclipboard != Redirectclipboard)
            {
                rdpString = rdpString.Append("redirectclipboard:").Append(Redirectclipboard.GetTypeNameForRdp()).Append(':').AppendLine(Redirectclipboard.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Redirectcomports != Redirectcomports)
            {
                rdpString = rdpString.Append("redirectcomports:").Append(Redirectcomports.GetTypeNameForRdp()).Append(':').AppendLine(Redirectcomports.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Redirectdirectx != Redirectdirectx)
            {
                rdpString = rdpString.Append("redirectdirectx:").Append(Redirectdirectx.GetTypeNameForRdp()).Append(':').AppendLine(Redirectdirectx.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Redirectdrives != Redirectdrives)
            {
                rdpString = rdpString.Append("redirectdrives:").Append(Redirectdrives.GetTypeNameForRdp()).Append(':').AppendLine(Redirectdrives.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Redirectposdevices != Redirectposdevices)
            {
                rdpString = rdpString.Append("redirectposdevices:").Append(Redirectposdevices.GetTypeNameForRdp()).Append(':').AppendLine(Redirectposdevices.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Redirectprinters != Redirectprinters)
            {
                rdpString = rdpString.Append("redirectprinters:").Append(Redirectprinters.GetTypeNameForRdp()).Append(':').AppendLine(Redirectprinters.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Redirectsmartcards != Redirectsmartcards)
            {
                rdpString = rdpString.Append("redirectsmartcards:").Append(Redirectsmartcards.GetTypeNameForRdp()).Append(':').AppendLine(Redirectsmartcards.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.Remoteapplicationcmdline ?? "") != (Remoteapplicationcmdline ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationcmdline:").Append(Remoteapplicationcmdline.GetTypeNameForRdp()).Append(':').AppendLine(Remoteapplicationcmdline);
            }

            if (saveDefaultSettings || DefaultRDP.Remoteapplicationexpandcmdline != Remoteapplicationexpandcmdline)
            {
                rdpString = rdpString.Append("remoteapplicationexpandcmdline:").Append(Remoteapplicationexpandcmdline.GetTypeNameForRdp()).Append(':').AppendLine(Remoteapplicationexpandcmdline.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Remoteapplicationexpandworkingdir != Remoteapplicationexpandworkingdir)
            {
                rdpString = rdpString.Append("remoteapplicationexpandworkingdir:").Append(Remoteapplicationexpandworkingdir.GetTypeNameForRdp()).Append(':').AppendLine(Remoteapplicationexpandworkingdir.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.Remoteapplicationfile ?? "") != (Remoteapplicationfile ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationfile:").Append(Remoteapplicationfile.GetTypeNameForRdp()).Append(':').AppendLine(Remoteapplicationfile);
            }

            if (saveDefaultSettings || (DefaultRDP.Remoteapplicationfileextensions ?? "") != (Remoteapplicationfileextensions ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationfileextensions:").Append(Remoteapplicationfileextensions.GetTypeNameForRdp()).Append(':').AppendLine(Remoteapplicationfileextensions);
            }

            if (saveDefaultSettings || (DefaultRDP.Remoteapplicationicon ?? "") != (Remoteapplicationicon ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationicon:").Append(Remoteapplicationicon.GetTypeNameForRdp()).Append(':').AppendLine(Remoteapplicationicon);
            }

            if (saveDefaultSettings || DefaultRDP.Remoteapplicationmode != Remoteapplicationmode)
            {
                rdpString = rdpString.Append("remoteapplicationmode:").Append(Remoteapplicationmode.GetTypeNameForRdp()).Append(':').AppendLine(Remoteapplicationmode.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.Remoteapplicationname ?? "") != (Remoteapplicationname ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationname:").Append(Remoteapplicationname.GetTypeNameForRdp()).Append(':').AppendLine(Remoteapplicationname);
            }

            if (saveDefaultSettings || (DefaultRDP.Remoteapplicationprogram ?? "") != (Remoteapplicationprogram ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationprogram:").Append(Remoteapplicationprogram.GetTypeNameForRdp()).Append(':').AppendLine(Remoteapplicationprogram);
            }

            if (saveDefaultSettings || DefaultRDP.ScreenModeId != ScreenModeId)
            {
                rdpString = rdpString.Append("screen mode id:").Append(ScreenModeId.GetTypeNameForRdp()).Append(':').AppendLine(ScreenModeId.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.ServerPort != ServerPort)
            {
                rdpString = rdpString.Append("server port:").Append(ServerPort.GetTypeNameForRdp()).Append(':').AppendLine(ServerPort.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.SessionBpp != SessionBpp)
            {
                rdpString = rdpString.Append("session bpp:").Append(SessionBpp.GetTypeNameForRdp()).Append(':').AppendLine(SessionBpp.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.ShellWorkingDirectory ?? "") != (ShellWorkingDirectory ?? ""))
            {
                rdpString = rdpString.Append("shell working directory:").Append(ShellWorkingDirectory.GetTypeNameForRdp()).Append(':').AppendLine(ShellWorkingDirectory);
            }

            if (saveDefaultSettings || DefaultRDP.SmartSizing != SmartSizing)
            {
                rdpString = rdpString.Append("smart sizing:").Append(SmartSizing.GetTypeNameForRdp()).Append(':').AppendLine(SmartSizing.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.SpanMonitors != SpanMonitors)
            {
                rdpString = rdpString.Append("span monitors:").Append(SpanMonitors.GetTypeNameForRdp()).Append(':').AppendLine(SpanMonitors.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.Superpanaccelerationfactor != Superpanaccelerationfactor)
            {
                rdpString = rdpString.Append("superpanaccelerationfactor:").Append(Superpanaccelerationfactor.GetTypeNameForRdp()).Append(':').AppendLine(Superpanaccelerationfactor.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.Usbdevicestoredirect ?? "") != (Usbdevicestoredirect ?? ""))
            {
                rdpString = rdpString.Append("usbdevicestoredirect:").Append(Usbdevicestoredirect.GetTypeNameForRdp()).Append(':').AppendLine(Usbdevicestoredirect);
            }

            if (saveDefaultSettings || DefaultRDP.UseMultimon != UseMultimon)
            {
                rdpString = rdpString.Append("use multimon:").Append(UseMultimon.GetTypeNameForRdp()).Append(':').AppendLine(UseMultimon.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.Username ?? "") != (Username ?? ""))
            {
                rdpString = rdpString.Append("username:").Append(Username.GetTypeNameForRdp()).Append(':').AppendLine(Username);
            }

            if (saveDefaultSettings || DefaultRDP.Videoplaybackmode != Videoplaybackmode)
            {
                rdpString = rdpString.Append("videoplaybackmode:").Append(Videoplaybackmode.GetTypeNameForRdp()).Append(':').AppendLine(Videoplaybackmode.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.Winposstr ?? "") != (Winposstr ?? ""))
            {
                rdpString = rdpString.Append("winposstr:").Append(Winposstr.GetTypeNameForRdp()).Append(':').AppendLine(Winposstr);
            }

            return rdpString.ToString();
        }

        public void LoadRdPfile(string filePath)
        {
            var sr = new StreamReader(filePath);
            while (sr.Peek() != -1)
            {
                var line = sr.ReadLine();
                var SplitLine = line.Split(':');
                if (string.IsNullOrEmpty(SplitLine[2]))
                {
                    continue;
                }
                if (SplitLine[0].Equals("administrative session"))
                {
                    AdministrativeSession = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("allow desktop composition"))
                {
                    AllowDesktopComposition = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("allow font smoothing"))
                {
                    AllowFontSmoothing = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("alternate full address"))
                {
                    AlternateFullAddress = SplitLine[2];
                }
                else if (SplitLine[0].Equals("alternate shell"))
                {
                    AlternateShell = SplitLine[2];
                }
                else if (SplitLine[0].Equals("audiocapturemode"))
                {
                    Audiocapturemode = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("audiomode"))
                {
                    Audiomode = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("audioqualitymode"))
                {
                    Audioqualitymode = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("authentication level"))
                {
                    AuthenticationLevel = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("autoreconnect max retries"))
                {
                    AutoreconnectMaxRetries = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("autoreconnection enabled"))
                {
                    AutoreconnectionEnabled = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("bandwidthautodetect"))
                {
                    Bandwidthautodetect = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("bitmapcachepersistenable"))
                {
                    Bitmapcachepersistenable = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("bitmapcachesize"))
                {
                    Bitmapcachesize = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("compression"))
                {
                    Compression = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("connect to console"))
                {
                    ConnectToConsole = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("connection type"))
                {
                    ConnectionType = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("desktop size id"))
                {
                    DesktopSizeId = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("desktopheight"))
                {
                    Desktopheight = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("desktopwidth"))
                {
                    Desktopwidth = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("devicestoredirect"))
                {
                    Devicestoredirect = SplitLine[2];
                }
                else if (SplitLine[0].Equals("disable ctrl+alt+del"))
                {
                    DisableCtrlAltDel = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("disable full window drag"))
                {
                    DisableFullWindowDrag = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("disable menu anims"))
                {
                    DisableMenuAnims = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("disable themes"))
                {
                    DisableThemes = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("disable wallpaper"))
                {
                    DisableWallpaper = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("disableconnectionsharing"))
                {
                    Disableconnectionsharing = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("disableremoteappcapscheck"))
                {
                    Disableremoteappcapscheck = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("displayconnectionbar"))
                {
                    Displayconnectionbar = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("domain"))
                {
                    Domain = SplitLine[2];
                }
                else if (SplitLine[0].Equals("drivestoredirect"))
                {
                    Drivestoredirect = SplitLine[2];
                }
                else if (SplitLine[0].Equals("enablecredsspsupport"))
                {
                    Enablecredsspsupport = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("enablesuperpan"))
                {
                    Enablesuperpan = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("full address"))
                {
                    FullAddress = SplitLine[2];
                }
                else if (SplitLine[0].Equals("gatewaycredentialssource"))
                {
                    Gatewaycredentialssource = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("gatewayhostname"))
                {
                    Gatewayhostname = SplitLine[2];
                }
                else if (SplitLine[0].Equals("gatewayprofileusagemethod"))
                {
                    Gatewayprofileusagemethod = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("gatewayusagemethod"))
                {
                    Gatewayusagemethod = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("keyboardhook"))
                {
                    Keyboardhook = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("negotiate security layer"))
                {
                    NegotiateSecurityLayer = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("networkautodetect"))
                {
                    Networkautodetect = int.Parse(SplitLine[2]);
                }
                // If SplitLine(0) = "password 51" Then password_51 = SplitLine(2)
                else if (SplitLine[0].Equals("pinconnectionbar"))
                {
                    Pinconnectionbar = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("prompt for credentials"))
                {
                    PromptForCredentials = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("prompt for credentials on client"))
                {
                    PromptForCredentialsOnClient = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("promptcredentialonce"))
                {
                    Promptcredentialonce = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("public mode"))
                {
                    PublicMode = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("redirectclipboard"))
                {
                    Redirectclipboard = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("redirectcomports"))
                {
                    Redirectcomports = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("redirectdirectx"))
                {
                    Redirectdirectx = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("redirectdrives"))
                {
                    Redirectdrives = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("redirectposdevices"))
                {
                    Redirectposdevices = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("redirectprinters"))
                {
                    Redirectprinters = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("redirectsmartcards"))
                {
                    Redirectsmartcards = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("remoteapplicationcmdline"))
                {
                    Remoteapplicationcmdline = SplitLine[2];
                }
                else if (SplitLine[0].Equals("remoteapplicationexpandcmdline"))
                {
                    Remoteapplicationexpandcmdline = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("remoteapplicationexpandworkingdir"))
                {
                    Remoteapplicationexpandworkingdir = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("remoteapplicationfile"))
                {
                    Remoteapplicationfile = SplitLine[2];
                }
                else if (SplitLine[0].Equals("remoteapplicationfileextensions"))
                {
                    Remoteapplicationfileextensions = SplitLine[2];
                }
                else if (SplitLine[0].Equals("remoteapplicationicon"))
                {
                    Remoteapplicationicon = SplitLine[2];
                }
                else if (SplitLine[0].Equals("remoteapplicationmode"))
                {
                    Remoteapplicationmode = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("remoteapplicationname"))
                {
                    Remoteapplicationname = SplitLine[2];
                }
                else if (SplitLine[0].Equals("remoteapplicationprogram"))
                {
                    Remoteapplicationprogram = SplitLine[2];
                }
                else if (SplitLine[0].Equals("screen mode id"))
                {
                    ScreenModeId = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("server port"))
                {
                    ServerPort = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("session bpp"))
                {
                    SessionBpp = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("shell working directory"))
                {
                    ShellWorkingDirectory = SplitLine[2];
                }
                else if (SplitLine[0].Equals("smart sizing"))
                {
                    SmartSizing = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("span monitors"))
                {
                    SpanMonitors = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("superpanaccelerationfactor"))
                {
                    Superpanaccelerationfactor = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("usbdevicestoredirect"))
                {
                    Usbdevicestoredirect = SplitLine[2];
                }
                else if (SplitLine[0].Equals("use multimon"))
                {
                    UseMultimon = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("username"))
                {
                    Username = SplitLine[2];
                }
                else if (SplitLine[0].Equals("videoplaybackmode"))
                {
                    Videoplaybackmode = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("winposstr"))
                {
                    Winposstr = SplitLine[2];
                }
            }
        }
    }
}