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
        public int AudioCaptureMode;
        public int AudioMode;
        public int AudioQualityMode;
        public int AuthenticationLevel = 2;
        public int AutoReconnectMaxRetries = 20;
        public int AutoReconnectionEnabled = 1;
        public int BandwidthAutodetect = 1;
        public int BitmapCachePersistEnable = 1;
        public int BitmapCacheSize = 1500;
        public int Compression = 1;
        public int ConnectToConsole;
        public int ConnectionType = 2;
        public int DesktopSizeId;
        public int DesktopHeight = 600;
        public int DesktopWidth = 800;
        public string DeviceStoreDirect = string.Empty;
        public int DisableCtrlAltDel = 1;
        public int DisableFullWindowDrag = 1;
        public int DisableMenuAnims = 1;
        public int DisableThemes;
        public int DisableWallpaper = 1;
        public int DisableConnectionSharing;
        public int DisableRemoteAppCapsCheck;
        public int DisplayConnectionBar = 1;
        public string Domain = string.Empty;
        public string DriveStoreDirect = string.Empty;
        public int EnableCredsspSupport = 1;
        public int EnableSuperPan;
        public string FullAddress = string.Empty;
        public int GatewayCredentialsSource = 4;
        public string GatewayHostname = string.Empty;
        public int GatewayProfileUsageMethod;
        public int GatewayUsageMethod = 4;
        public int KeyboardHook = 2;
        public int NegotiateSecurityLayer = 1;
        public int NetworkAutoDetect = 1;
        // Public password_51 As Binary
        public int PinConnectionBar = 1;
        public int PromptForCredentials;
        public int PromptForCredentialsOnClient;
        public int PromptCredentialOnce = 1;
        public int PublicMode;
        public int RedirectClipboard = 1;
        public int RedirectComPorts;
        public int RedirectDirectX = 1;
        public int RedirectDrives;
        public int RedirectPosDevices;
        public int RedirectPrinters = 1;
        public int RedirectSmartCards = 1;
        public string RemoteApplicationCmdline = string.Empty;
        public int RemoteApplicationExpandCmdline = 1;
        public int RemoteApplicationExpandWorkingDir;
        public string RemoteApplicationFile = string.Empty;
        public string RemoteApplicationFileExtensions = string.Empty;
        public string RemoteApplicationIcon = string.Empty;
        public int RemoteApplicationMode;
        public string RemoteApplicationName = string.Empty;
        public string RemoteApplicationProgram = string.Empty;
        public int ScreenModeId = 2;
        public int ServerPort = 3389;
        public int SessionBpp = 32;
        public string ShellWorkingDirectory = string.Empty;
        public int SmartSizing;
        public int SpanMonitors;
        public int SuperPanAccelerationFactor = 1;
        public string UsbDeviceStoreDirect = string.Empty;
        public int UseMultimonitor;
        public string Username = string.Empty;
        public int VideoPlaybackMode = 1;
        public string WindowPositionAsString = "0,3,0,0,800,600";

        public void SaveRdpFile(string filePath, bool saveDefaultSettings = false)
        {
            var LockCheck = new LockChecker.New.LockChecker();
            var SkipFile = false;
            var FileLocked = LockCheck.CheckLock(filePath);
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
                File.WriteAllText(filePath, GetRdpString(saveDefaultSettings));
            }
        }

        public string GetRdpString(bool saveDefaultSettings = false)
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

            if (saveDefaultSettings || DefaultRDP.AudioCaptureMode != AudioCaptureMode)
            {
                rdpString = rdpString.Append("audiocapturemode:").Append(AudioCaptureMode.GetTypeNameForRdp()).Append(':').AppendLine(AudioCaptureMode.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.AudioMode != AudioMode)
            {
                rdpString = rdpString.Append("audiomode:").Append(AudioMode.GetTypeNameForRdp()).Append(':').AppendLine(AudioMode.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.AudioQualityMode != AudioQualityMode)
            {
                rdpString = rdpString.Append("audioqualitymode:").Append(AudioQualityMode.GetTypeNameForRdp()).Append(':').AppendLine(AudioQualityMode.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.AuthenticationLevel != AuthenticationLevel)
            {
                rdpString = rdpString.Append("authentication level:").Append(AuthenticationLevel.GetTypeNameForRdp()).Append(':').AppendLine(AuthenticationLevel.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.AutoReconnectMaxRetries != AutoReconnectMaxRetries)
            {
                rdpString = rdpString.Append("autoreconnect max retries:").Append(AutoReconnectMaxRetries.GetTypeNameForRdp()).Append(':').AppendLine(AutoReconnectMaxRetries.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.AutoReconnectionEnabled != AutoReconnectionEnabled)
            {
                rdpString = rdpString.Append("autoreconnection enabled:").Append(AutoReconnectionEnabled.GetTypeNameForRdp()).Append(':').AppendLine(AutoReconnectionEnabled.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.BandwidthAutodetect != BandwidthAutodetect)
            {
                rdpString = rdpString.Append("bandwidthautodetect:").Append(BandwidthAutodetect.GetTypeNameForRdp()).Append(':').AppendLine(BandwidthAutodetect.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.BitmapCachePersistEnable != BitmapCachePersistEnable)
            {
                rdpString = rdpString.Append("bitmapcachepersistenable:").Append(BitmapCachePersistEnable.GetTypeNameForRdp()).Append(':').AppendLine(BitmapCachePersistEnable.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.BitmapCacheSize != BitmapCacheSize)
            {
                rdpString = rdpString.Append("bitmapcachesize:").Append(BitmapCacheSize.GetTypeNameForRdp()).Append(':').AppendLine(BitmapCacheSize.ToString());
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

            if (saveDefaultSettings || DefaultRDP.DesktopHeight != DesktopHeight)
            {
                rdpString = rdpString.Append("desktopheight:").Append(DesktopHeight.GetTypeNameForRdp()).Append(':').AppendLine(DesktopHeight.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.DesktopWidth != DesktopWidth)
            {
                rdpString = rdpString.Append("desktopwidth:").Append(DesktopWidth.GetTypeNameForRdp()).Append(':').AppendLine(DesktopWidth.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.DeviceStoreDirect ?? "") != (DeviceStoreDirect ?? ""))
            {
                rdpString = rdpString.Append("devicestoredirect:").Append(DeviceStoreDirect.GetTypeNameForRdp()).Append(':').AppendLine(DeviceStoreDirect);
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

            if (saveDefaultSettings || DefaultRDP.DisableConnectionSharing != DisableConnectionSharing)
            {
                rdpString = rdpString.Append("disableconnectionsharing:").Append(DisableConnectionSharing.GetTypeNameForRdp()).Append(':').AppendLine(DisableConnectionSharing.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.DisableRemoteAppCapsCheck != DisableRemoteAppCapsCheck)
            {
                rdpString = rdpString.Append("disableremoteappcapscheck:").Append(DisableRemoteAppCapsCheck.GetTypeNameForRdp()).Append(':').AppendLine(DisableRemoteAppCapsCheck.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.DisplayConnectionBar != DisplayConnectionBar)
            {
                rdpString = rdpString.Append("displayconnectionbar:").Append(DisplayConnectionBar.GetTypeNameForRdp()).Append(':').AppendLine(DisplayConnectionBar.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.Domain ?? "") != (Domain ?? ""))
            {
                rdpString = rdpString.Append("domain:").Append(Domain.GetTypeNameForRdp()).Append(':').AppendLine(Domain);
            }

            if (saveDefaultSettings || (DefaultRDP.DriveStoreDirect ?? "") != (DriveStoreDirect ?? ""))
            {
                rdpString = rdpString.Append("drivestoredirect:").Append(DriveStoreDirect.GetTypeNameForRdp()).Append(':').AppendLine(DriveStoreDirect);
            }

            if (saveDefaultSettings || DefaultRDP.EnableCredsspSupport != EnableCredsspSupport)
            {
                rdpString = rdpString.Append("enablecredsspsupport:").Append(EnableCredsspSupport.GetTypeNameForRdp()).Append(':').AppendLine(EnableCredsspSupport.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.EnableSuperPan != EnableSuperPan)
            {
                rdpString = rdpString.Append("enablesuperpan:").Append(EnableSuperPan.GetTypeNameForRdp()).Append(':').AppendLine(EnableSuperPan.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.FullAddress ?? "") != (FullAddress ?? ""))
            {
                rdpString = rdpString.Append("full address:").Append(FullAddress.GetTypeNameForRdp()).Append(':').AppendLine(FullAddress);
            }

            if (saveDefaultSettings || DefaultRDP.GatewayCredentialsSource != GatewayCredentialsSource)
            {
                rdpString = rdpString.Append("gatewaycredentialssource:").Append(GatewayCredentialsSource.GetTypeNameForRdp()).Append(':').AppendLine(GatewayCredentialsSource.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.GatewayHostname ?? "") != (GatewayHostname ?? ""))
            {
                rdpString = rdpString.Append("gatewayhostname:").Append(GatewayHostname.GetTypeNameForRdp()).Append(':').AppendLine(GatewayHostname);
            }

            if (saveDefaultSettings || DefaultRDP.GatewayProfileUsageMethod != GatewayProfileUsageMethod)
            {
                rdpString = rdpString.Append("gatewayprofileusagemethod:").Append(GatewayProfileUsageMethod.GetTypeNameForRdp()).Append(':').AppendLine(GatewayProfileUsageMethod.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.GatewayUsageMethod != GatewayUsageMethod)
            {
                rdpString = rdpString.Append("gatewayusagemethod:").Append(GatewayUsageMethod.GetTypeNameForRdp()).Append(':').AppendLine(GatewayUsageMethod.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.KeyboardHook != KeyboardHook)
            {
                rdpString = rdpString.Append("keyboardhook:").Append(KeyboardHook.GetTypeNameForRdp()).Append(':').AppendLine(KeyboardHook.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.NegotiateSecurityLayer != NegotiateSecurityLayer)
            {
                rdpString = rdpString.Append("negotiate security layer:").Append(NegotiateSecurityLayer.GetTypeNameForRdp()).Append(':').AppendLine(NegotiateSecurityLayer.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.NetworkAutoDetect != NetworkAutoDetect)
            {
                rdpString = rdpString.Append("networkautodetect:").Append(NetworkAutoDetect.GetTypeNameForRdp()).Append(':').AppendLine(NetworkAutoDetect.ToString());
            }
            // If SaveDefaultSettings Or Not DefaultRDP.password_51 = password_51 Then RDPstring += "password 51" & ":" & password_51.GetType().ToString.Replace("System.", "").ToLower.Substring(0, 1) & ":" & password_51.ToString & vbCrLf
            if (saveDefaultSettings || DefaultRDP.PinConnectionBar != PinConnectionBar)
            {
                rdpString = rdpString.Append("pinconnectionbar:").Append(PinConnectionBar.GetTypeNameForRdp()).Append(':').AppendLine(PinConnectionBar.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.PromptForCredentials != PromptForCredentials)
            {
                rdpString = rdpString.Append("prompt for credentials:").Append(PromptForCredentials.GetTypeNameForRdp()).Append(':').AppendLine(PromptForCredentials.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.PromptForCredentialsOnClient != PromptForCredentialsOnClient)
            {
                rdpString = rdpString.Append("prompt for credentials on client:").Append(PromptForCredentialsOnClient.GetTypeNameForRdp()).Append(':').AppendLine(PromptForCredentialsOnClient.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.PromptCredentialOnce != PromptCredentialOnce)
            {
                rdpString = rdpString.Append("promptcredentialonce:").Append(PromptCredentialOnce.GetTypeNameForRdp()).Append(':').AppendLine(PromptCredentialOnce.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.PublicMode != PublicMode)
            {
                rdpString = rdpString.Append("public mode:").Append(PublicMode.GetTypeNameForRdp()).Append(':').AppendLine(PublicMode.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.RedirectClipboard != RedirectClipboard)
            {
                rdpString = rdpString.Append("redirectclipboard:").Append(RedirectClipboard.GetTypeNameForRdp()).Append(':').AppendLine(RedirectClipboard.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.RedirectComPorts != RedirectComPorts)
            {
                rdpString = rdpString.Append("redirectcomports:").Append(RedirectComPorts.GetTypeNameForRdp()).Append(':').AppendLine(RedirectComPorts.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.RedirectDirectX != RedirectDirectX)
            {
                rdpString = rdpString.Append("redirectdirectx:").Append(RedirectDirectX.GetTypeNameForRdp()).Append(':').AppendLine(RedirectDirectX.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.RedirectDrives != RedirectDrives)
            {
                rdpString = rdpString.Append("redirectdrives:").Append(RedirectDrives.GetTypeNameForRdp()).Append(':').AppendLine(RedirectDrives.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.RedirectPosDevices != RedirectPosDevices)
            {
                rdpString = rdpString.Append("redirectposdevices:").Append(RedirectPosDevices.GetTypeNameForRdp()).Append(':').AppendLine(RedirectPosDevices.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.RedirectPrinters != RedirectPrinters)
            {
                rdpString = rdpString.Append("redirectprinters:").Append(RedirectPrinters.GetTypeNameForRdp()).Append(':').AppendLine(RedirectPrinters.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.RedirectSmartCards != RedirectSmartCards)
            {
                rdpString = rdpString.Append("redirectsmartcards:").Append(RedirectSmartCards.GetTypeNameForRdp()).Append(':').AppendLine(RedirectSmartCards.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.RemoteApplicationCmdline ?? "") != (RemoteApplicationCmdline ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationcmdline:").Append(RemoteApplicationCmdline.GetTypeNameForRdp()).Append(':').AppendLine(RemoteApplicationCmdline);
            }

            if (saveDefaultSettings || DefaultRDP.RemoteApplicationExpandCmdline != RemoteApplicationExpandCmdline)
            {
                rdpString = rdpString.Append("remoteapplicationexpandcmdline:").Append(RemoteApplicationExpandCmdline.GetTypeNameForRdp()).Append(':').AppendLine(RemoteApplicationExpandCmdline.ToString());
            }

            if (saveDefaultSettings || DefaultRDP.RemoteApplicationExpandWorkingDir != RemoteApplicationExpandWorkingDir)
            {
                rdpString = rdpString.Append("remoteapplicationexpandworkingdir:").Append(RemoteApplicationExpandWorkingDir.GetTypeNameForRdp()).Append(':').AppendLine(RemoteApplicationExpandWorkingDir.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.RemoteApplicationFile ?? "") != (RemoteApplicationFile ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationfile:").Append(RemoteApplicationFile.GetTypeNameForRdp()).Append(':').AppendLine(RemoteApplicationFile);
            }

            if (saveDefaultSettings || (DefaultRDP.RemoteApplicationFileExtensions ?? "") != (RemoteApplicationFileExtensions ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationfileextensions:").Append(RemoteApplicationFileExtensions.GetTypeNameForRdp()).Append(':').AppendLine(RemoteApplicationFileExtensions);
            }

            if (saveDefaultSettings || (DefaultRDP.RemoteApplicationIcon ?? "") != (RemoteApplicationIcon ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationicon:").Append(RemoteApplicationIcon.GetTypeNameForRdp()).Append(':').AppendLine(RemoteApplicationIcon);
            }

            if (saveDefaultSettings || DefaultRDP.RemoteApplicationMode != RemoteApplicationMode)
            {
                rdpString = rdpString.Append("remoteapplicationmode:").Append(RemoteApplicationMode.GetTypeNameForRdp()).Append(':').AppendLine(RemoteApplicationMode.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.RemoteApplicationName ?? "") != (RemoteApplicationName ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationname:").Append(RemoteApplicationName.GetTypeNameForRdp()).Append(':').AppendLine(RemoteApplicationName);
            }

            if (saveDefaultSettings || (DefaultRDP.RemoteApplicationProgram ?? "") != (RemoteApplicationProgram ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationprogram:").Append(RemoteApplicationProgram.GetTypeNameForRdp()).Append(':').AppendLine(RemoteApplicationProgram);
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

            if (saveDefaultSettings || DefaultRDP.SuperPanAccelerationFactor != SuperPanAccelerationFactor)
            {
                rdpString = rdpString.Append("superpanaccelerationfactor:").Append(SuperPanAccelerationFactor.GetTypeNameForRdp()).Append(':').AppendLine(SuperPanAccelerationFactor.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.UsbDeviceStoreDirect ?? "") != (UsbDeviceStoreDirect ?? ""))
            {
                rdpString = rdpString.Append("usbdevicestoredirect:").Append(UsbDeviceStoreDirect.GetTypeNameForRdp()).Append(':').AppendLine(UsbDeviceStoreDirect);
            }

            if (saveDefaultSettings || DefaultRDP.UseMultimonitor != UseMultimonitor)
            {
                rdpString = rdpString.Append("use multimon:").Append(UseMultimonitor.GetTypeNameForRdp()).Append(':').AppendLine(UseMultimonitor.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.Username ?? "") != (Username ?? ""))
            {
                rdpString = rdpString.Append("username:").Append(Username.GetTypeNameForRdp()).Append(':').AppendLine(Username);
            }

            if (saveDefaultSettings || DefaultRDP.VideoPlaybackMode != VideoPlaybackMode)
            {
                rdpString = rdpString.Append("videoplaybackmode:").Append(VideoPlaybackMode.GetTypeNameForRdp()).Append(':').AppendLine(VideoPlaybackMode.ToString());
            }

            if (saveDefaultSettings || (DefaultRDP.WindowPositionAsString ?? "") != (WindowPositionAsString ?? ""))
            {
                rdpString = rdpString.Append("winposstr:").Append(WindowPositionAsString.GetTypeNameForRdp()).Append(':').AppendLine(WindowPositionAsString);
            }

            return rdpString.ToString();
        }

        public void LoadRdpFile(string filePath)
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
                switch (SplitLine[0])
                {
                    case "administrative session":
                        AdministrativeSession = int.Parse(SplitLine[2]);
                        break;
                    case "allow desktop composition":
                        AllowDesktopComposition = int.Parse(SplitLine[2]);
                        break;
                    case "allow font smoothing":
                        AllowFontSmoothing = int.Parse(SplitLine[2]);
                        break;
                    case "alternate full address":
                        AlternateFullAddress = SplitLine[2];
                        break;
                    case "alternate shell":
                        AlternateShell = SplitLine[2];
                        break;
                    case "audiocapturemode":
                        AudioCaptureMode = int.Parse(SplitLine[2]);
                        break;
                    case "audiomode":
                        AudioMode = int.Parse(SplitLine[2]);
                        break;
                    case "audioqualitymode":
                        AudioQualityMode = int.Parse(SplitLine[2]);
                        break;
                    case "authentication level":
                        AuthenticationLevel = int.Parse(SplitLine[2]);
                        break;
                    case "autoreconnect max retries":
                        AutoReconnectMaxRetries = int.Parse(SplitLine[2]);
                        break;
                    case "autoreconnection enabled":
                        AutoReconnectionEnabled = int.Parse(SplitLine[2]);
                        break;
                    case "bandwidthautodetect":
                        BandwidthAutodetect = int.Parse(SplitLine[2]);
                        break;
                    case "bitmapcachepersistenable":
                        BitmapCachePersistEnable = int.Parse(SplitLine[2]);
                        break;
                    case "bitmapcachesize":
                        BitmapCacheSize = int.Parse(SplitLine[2]);
                        break;
                    case "compression":
                        Compression = int.Parse(SplitLine[2]);
                        break;
                    case "connect to console":
                        ConnectToConsole = int.Parse(SplitLine[2]);
                        break;
                    case "connection type":
                        ConnectionType = int.Parse(SplitLine[2]);
                        break;
                    case "desktop size id":
                        DesktopSizeId = int.Parse(SplitLine[2]);
                        break;
                    case "desktopheight":
                        DesktopHeight = int.Parse(SplitLine[2]);
                        break;
                    case "desktopwidth":
                        DesktopWidth = int.Parse(SplitLine[2]);
                        break;
                    case "devicestoredirect":
                        DeviceStoreDirect = SplitLine[2];
                        break;
                    case "disable ctrl+alt+del":
                        DisableCtrlAltDel = int.Parse(SplitLine[2]);
                        break;
                    case "disable full window drag":
                        DisableFullWindowDrag = int.Parse(SplitLine[2]);
                        break;
                    case "disable menu anims":
                        DisableMenuAnims = int.Parse(SplitLine[2]);
                        break;
                    case "disable themes":
                        DisableThemes = int.Parse(SplitLine[2]);
                        break;
                    case "disable wallpaper":
                        DisableWallpaper = int.Parse(SplitLine[2]);
                        break;
                    case "disableconnectionsharing":
                        DisableConnectionSharing = int.Parse(SplitLine[2]);
                        break;
                    case "disableremoteappcapscheck":
                        DisableRemoteAppCapsCheck = int.Parse(SplitLine[2]);
                        break;
                    case "displayconnectionbar":
                        DisplayConnectionBar = int.Parse(SplitLine[2]);
                        break;
                    case "domain":
                        Domain = SplitLine[2];
                        break;
                    case "drivestoredirect":
                        DriveStoreDirect = SplitLine[2];
                        break;
                    case "enablecredsspsupport":
                        EnableCredsspSupport = int.Parse(SplitLine[2]);
                        break;
                    case "enablesuperpan":
                        EnableSuperPan = int.Parse(SplitLine[2]);
                        break;
                    case "full address":
                        FullAddress = SplitLine[2];
                        break;
                    case "gatewaycredentialssource":
                        GatewayCredentialsSource = int.Parse(SplitLine[2]);
                        break;
                    case "gatewayhostname":
                        GatewayHostname = SplitLine[2];
                        break;
                    case "gatewayprofileusagemethod":
                        GatewayProfileUsageMethod = int.Parse(SplitLine[2]);
                        break;
                    case "gatewayusagemethod":
                        GatewayUsageMethod = int.Parse(SplitLine[2]);
                        break;
                    case "keyboardhook":
                        KeyboardHook = int.Parse(SplitLine[2]);
                        break;
                    case "negotiate security layer":
                        NegotiateSecurityLayer = int.Parse(SplitLine[2]);
                        break;
                    case "networkautodetect":
                        NetworkAutoDetect = int.Parse(SplitLine[2]);
                        break;
                    // If SplitLine(0) = "password 51" Then password_51 = SplitLine(2)
                    case "pinconnectionbar":
                        PinConnectionBar = int.Parse(SplitLine[2]);
                        break;
                    case "prompt for credentials":
                        PromptForCredentials = int.Parse(SplitLine[2]);
                        break;
                    case "prompt for credentials on client":
                        PromptForCredentialsOnClient = int.Parse(SplitLine[2]);
                        break;
                    case "promptcredentialonce":
                        PromptCredentialOnce = int.Parse(SplitLine[2]);
                        break;
                    case "public mode":
                        PublicMode = int.Parse(SplitLine[2]);
                        break;
                    case "redirectclipboard":
                        RedirectClipboard = int.Parse(SplitLine[2]);
                        break;
                    case "redirectcomports":
                        RedirectComPorts = int.Parse(SplitLine[2]);
                        break;
                    case "redirectdirectx":
                        RedirectDirectX = int.Parse(SplitLine[2]);
                        break;
                    case "redirectdrives":
                        RedirectDrives = int.Parse(SplitLine[2]);
                        break;
                    case "redirectposdevices":
                        RedirectPosDevices = int.Parse(SplitLine[2]);
                        break;
                    case "redirectprinters":
                        RedirectPrinters = int.Parse(SplitLine[2]);
                        break;
                    case "redirectsmartcards":
                        RedirectSmartCards = int.Parse(SplitLine[2]);
                        break;
                    case "remoteapplicationcmdline":
                        RemoteApplicationCmdline = SplitLine[2];
                        break;
                    case "remoteapplicationexpandcmdline":
                        RemoteApplicationExpandCmdline = int.Parse(SplitLine[2]);
                        break;
                    case "remoteapplicationexpandworkingdir":
                        RemoteApplicationExpandWorkingDir = int.Parse(SplitLine[2]);
                        break;
                    case "remoteapplicationfile":
                        RemoteApplicationFile = SplitLine[2];
                        break;
                    case "remoteapplicationfileextensions":
                        RemoteApplicationFileExtensions = SplitLine[2];
                        break;
                    case "remoteapplicationicon":
                        RemoteApplicationIcon = SplitLine[2];
                        break;
                    case "remoteapplicationmode":
                        RemoteApplicationMode = int.Parse(SplitLine[2]);
                        break;
                    case "remoteapplicationname":
                        RemoteApplicationName = SplitLine[2];
                        break;
                    case "remoteapplicationprogram":
                        RemoteApplicationProgram = SplitLine[2];
                        break;
                    case "screen mode id":
                        ScreenModeId = int.Parse(SplitLine[2]);
                        break;
                    case "server port":
                        ServerPort = int.Parse(SplitLine[2]);
                        break;
                    case "session bpp":
                        SessionBpp = int.Parse(SplitLine[2]);
                        break;
                    case "shell working directory":
                        ShellWorkingDirectory = SplitLine[2];
                        break;
                    case "smart sizing":
                        SmartSizing = int.Parse(SplitLine[2]);
                        break;
                    case "span monitors":
                        SpanMonitors = int.Parse(SplitLine[2]);
                        break;
                    case "superpanaccelerationfactor":
                        SuperPanAccelerationFactor = int.Parse(SplitLine[2]);
                        break;
                    case "usbdevicestoredirect":
                        UsbDeviceStoreDirect = SplitLine[2];
                        break;
                    case "use multimon":
                        UseMultimonitor = int.Parse(SplitLine[2]);
                        break;
                    case "username":
                        Username = SplitLine[2];
                        break;
                    case "videoplaybackmode":
                        VideoPlaybackMode = int.Parse(SplitLine[2]);
                        break;
                    case "winposstr":
                        WindowPositionAsString = SplitLine[2];
                        break;
                }
            }
        }
    }
}