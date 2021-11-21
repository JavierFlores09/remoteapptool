using System;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace RDPFileLib.New
{
    public class RDPFile
    {
        public int administrative_session;
        public int allow_desktop_composition;
        public int allow_font_smoothing;
        public string alternate_full_address = string.Empty;
        public string alternate_shell = string.Empty;
        public int audiocapturemode;
        public int audiomode;
        public int audioqualitymode;
        public int authentication_level = 2;
        public int autoreconnect_max_retries = 20;
        public int autoreconnection_enabled = 1;
        public int bandwidthautodetect = 1;
        public int bitmapcachepersistenable = 1;
        public int bitmapcachesize = 1500;
        public int compression = 1;
        public int connect_to_console;
        public int connection_type = 2;
        public int desktop_size_id;
        public int desktopheight = 600;
        public int desktopwidth = 800;
        public string devicestoredirect = string.Empty;
        public int disable_ctrl_alt_del = 1;
        public int disable_full_window_drag = 1;
        public int disable_menu_anims = 1;
        public int disable_themes;
        public int disable_wallpaper = 1;
        public int disableconnectionsharing;
        public int disableremoteappcapscheck;
        public int displayconnectionbar = 1;
        public string domain = string.Empty;
        public string drivestoredirect = string.Empty;
        public int enablecredsspsupport = 1;
        public int enablesuperpan;
        public string full_address = string.Empty;
        public int gatewaycredentialssource = 4;
        public string gatewayhostname = string.Empty;
        public int gatewayprofileusagemethod;
        public int gatewayusagemethod = 4;
        public int keyboardhook = 2;
        public int negotiate_security_layer = 1;
        public int networkautodetect = 1;
        // Public password_51 As Binary
        public int pinconnectionbar = 1;
        public int prompt_for_credentials;
        public int prompt_for_credentials_on_client;
        public int promptcredentialonce = 1;
        public int public_mode;
        public int redirectclipboard = 1;
        public int redirectcomports;
        public int redirectdirectx = 1;
        public int redirectdrives;
        public int redirectposdevices;
        public int redirectprinters = 1;
        public int redirectsmartcards = 1;
        public string remoteapplicationcmdline = string.Empty;
        public int remoteapplicationexpandcmdline = 1;
        public int remoteapplicationexpandworkingdir;
        public string remoteapplicationfile = string.Empty;
        public string remoteapplicationfileextensions = string.Empty;
        public string remoteapplicationicon = string.Empty;
        public int remoteapplicationmode;
        public string remoteapplicationname = string.Empty;
        public string remoteapplicationprogram = string.Empty;
        public int screen_mode_id = 2;
        public int server_port = 3389;
        public int session_bpp = 32;
        public string shell_working_directory = string.Empty;
        public int smart_sizing;
        public int span_monitors;
        public int superpanaccelerationfactor = 1;
        public string usbdevicestoredirect = string.Empty;
        public int use_multimon;
        public string username = string.Empty;
        public int videoplaybackmode = 1;
        public string winposstr = "0,3,0,0,800,600";

        public void SaveRDPfile(string FilePath, bool SaveDefaultSettings = false)
        {
            var LockCheck = new LockChecker.New.LockChecker();
            string FileLocked;
            bool SkipFile = false;
            FileLocked = LockCheck.CheckLock(FilePath);
            while (FileLocked != "No locks")
            {
                if (MessageBox.Show($"The file {FilePath} is currently locked.  Lock information:{FileLocked}{Environment.NewLine}Do you want to try again?", "File Locked", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FileLocked = LockCheck.CheckLock(FilePath);
                }
                else
                {
                    _ = MessageBox.Show($"The following file will not be copied:{Environment.NewLine}{FilePath}");
                    SkipFile = true;
                    FileLocked = "No locks";
                }
            }

            if (!SkipFile)
            {
                File.WriteAllText(FilePath, GetRDPstring(SaveDefaultSettings));
            }
        }

        public string GetRDPstring(bool SaveDefaultSettings = false)
        {
            var rdpString = new StringBuilder();
            var DefaultRDP = new RDPFile();
            if (SaveDefaultSettings || DefaultRDP.administrative_session != administrative_session)
            {
                rdpString = rdpString.Append("administrative session:").Append(administrative_session.GetTypeNameForRdp()).Append(':').AppendLine(administrative_session.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.allow_desktop_composition != allow_desktop_composition)
            {
                rdpString = rdpString.Append("allow desktop composition:").Append(allow_desktop_composition.GetTypeNameForRdp()).Append(':').AppendLine(allow_desktop_composition.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.allow_font_smoothing != allow_font_smoothing)
            {
                rdpString = rdpString.Append("allow font smoothing:").Append(allow_font_smoothing.GetTypeNameForRdp()).Append(':').AppendLine(allow_font_smoothing.ToString());
            }

            if (SaveDefaultSettings || (DefaultRDP.alternate_full_address ?? "") != (alternate_full_address ?? ""))
            {
                rdpString = rdpString.Append("alternate full address:").Append(alternate_full_address.GetTypeNameForRdp()).Append(':').AppendLine(alternate_full_address);
            }

            if (SaveDefaultSettings || (DefaultRDP.alternate_shell ?? "") != (alternate_shell ?? ""))
            {
                rdpString = rdpString.Append("alternate shell:").Append(alternate_shell.GetTypeNameForRdp()).Append(':').AppendLine(alternate_shell);
            }

            if (SaveDefaultSettings || DefaultRDP.audiocapturemode != audiocapturemode)
            {
                rdpString = rdpString.Append("audiocapturemode:").Append(audiocapturemode.GetTypeNameForRdp()).Append(':').AppendLine(audiocapturemode.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.audiomode != audiomode)
            {
                rdpString = rdpString.Append("audiomode:").Append(audiomode.GetTypeNameForRdp()).Append(':').AppendLine(audiomode.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.audioqualitymode != audioqualitymode)
            {
                rdpString = rdpString.Append("audioqualitymode:").Append(audioqualitymode.GetTypeNameForRdp()).Append(':').AppendLine(audioqualitymode.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.authentication_level != authentication_level)
            {
                rdpString = rdpString.Append("authentication level:").Append(authentication_level.GetTypeNameForRdp()).Append(':').AppendLine(authentication_level.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.autoreconnect_max_retries != autoreconnect_max_retries)
            {
                rdpString = rdpString.Append("autoreconnect max retries:").Append(autoreconnect_max_retries.GetTypeNameForRdp()).Append(':').AppendLine(autoreconnect_max_retries.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.autoreconnection_enabled != autoreconnection_enabled)
            {
                rdpString = rdpString.Append("autoreconnection enabled:").Append(autoreconnection_enabled.GetTypeNameForRdp()).Append(':').AppendLine(autoreconnection_enabled.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.bandwidthautodetect != bandwidthautodetect)
            {
                rdpString = rdpString.Append("bandwidthautodetect:").Append(bandwidthautodetect.GetTypeNameForRdp()).Append(':').AppendLine(bandwidthautodetect.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.bitmapcachepersistenable != bitmapcachepersistenable)
            {
                rdpString = rdpString.Append("bitmapcachepersistenable:").Append(bitmapcachepersistenable.GetTypeNameForRdp()).Append(':').AppendLine(bitmapcachepersistenable.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.bitmapcachesize != bitmapcachesize)
            {
                rdpString = rdpString.Append("bitmapcachesize:").Append(bitmapcachesize.GetTypeNameForRdp()).Append(':').AppendLine(bitmapcachesize.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.compression != compression)
            {
                rdpString = rdpString.Append("compression:").Append(compression.GetTypeNameForRdp()).Append(':').AppendLine(compression.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.connect_to_console != connect_to_console)
            {
                rdpString = rdpString.Append("connect to console:").Append(connect_to_console.GetTypeNameForRdp()).Append(':').AppendLine(connect_to_console.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.connection_type != connection_type)
            {
                rdpString = rdpString.Append("connection type:").Append(connection_type.GetTypeNameForRdp()).Append(':').AppendLine(connection_type.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.desktop_size_id != desktop_size_id)
            {
                rdpString = rdpString.Append("desktop size id:").Append(desktop_size_id.GetTypeNameForRdp()).Append(':').AppendLine(desktop_size_id.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.desktopheight != desktopheight)
            {
                rdpString = rdpString.Append("desktopheight:").Append(desktopheight.GetTypeNameForRdp()).Append(':').AppendLine(desktopheight.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.desktopwidth != desktopwidth)
            {
                rdpString = rdpString.Append("desktopwidth:").Append(desktopwidth.GetTypeNameForRdp()).Append(':').AppendLine(desktopwidth.ToString());
            }

            if (SaveDefaultSettings || (DefaultRDP.devicestoredirect ?? "") != (devicestoredirect ?? ""))
            {
                rdpString = rdpString.Append("devicestoredirect:").Append(devicestoredirect.GetTypeNameForRdp()).Append(':').AppendLine(devicestoredirect);
            }

            if (SaveDefaultSettings || DefaultRDP.disable_ctrl_alt_del != disable_ctrl_alt_del)
            {
                rdpString = rdpString.Append("disable ctrl+alt+del:").Append(disable_ctrl_alt_del.GetTypeNameForRdp()).Append(':').AppendLine(disable_ctrl_alt_del.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.disable_full_window_drag != disable_full_window_drag)
            {
                rdpString = rdpString.Append("disable full window drag:").Append(disable_full_window_drag.GetTypeNameForRdp()).Append(':').AppendLine(disable_full_window_drag.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.disable_menu_anims != disable_menu_anims)
            {
                rdpString = rdpString.Append("disable menu anims:").Append(disable_menu_anims.GetTypeNameForRdp()).Append(':').AppendLine(disable_menu_anims.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.disable_themes != disable_themes)
            {
                rdpString = rdpString.Append("disable themes:").Append(disable_themes.GetTypeNameForRdp()).Append(':').AppendLine(disable_themes.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.disable_wallpaper != disable_wallpaper)
            {
                rdpString = rdpString.Append("disable wallpaper:").Append(disable_wallpaper.GetTypeNameForRdp()).Append(':').AppendLine(disable_wallpaper.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.disableconnectionsharing != disableconnectionsharing)
            {
                rdpString = rdpString.Append("disableconnectionsharing:").Append(disableconnectionsharing.GetTypeNameForRdp()).Append(':').AppendLine(disableconnectionsharing.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.disableremoteappcapscheck != disableremoteappcapscheck)
            {
                rdpString = rdpString.Append("disableremoteappcapscheck:").Append(disableremoteappcapscheck.GetTypeNameForRdp()).Append(':').AppendLine(disableremoteappcapscheck.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.displayconnectionbar != displayconnectionbar)
            {
                rdpString = rdpString.Append("displayconnectionbar:").Append(displayconnectionbar.GetTypeNameForRdp()).Append(':').AppendLine(displayconnectionbar.ToString());
            }

            if (SaveDefaultSettings || (DefaultRDP.domain ?? "") != (domain ?? ""))
            {
                rdpString = rdpString.Append("domain:").Append(domain.GetTypeNameForRdp()).Append(':').AppendLine(domain);
            }

            if (SaveDefaultSettings || (DefaultRDP.drivestoredirect ?? "") != (drivestoredirect ?? ""))
            {
                rdpString = rdpString.Append("drivestoredirect:").Append(drivestoredirect.GetTypeNameForRdp()).Append(':').AppendLine(drivestoredirect);
            }

            if (SaveDefaultSettings || DefaultRDP.enablecredsspsupport != enablecredsspsupport)
            {
                rdpString = rdpString.Append("enablecredsspsupport:").Append(enablecredsspsupport.GetTypeNameForRdp()).Append(':').AppendLine(enablecredsspsupport.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.enablesuperpan != enablesuperpan)
            {
                rdpString = rdpString.Append("enablesuperpan:").Append(enablesuperpan.GetTypeNameForRdp()).Append(':').AppendLine(enablesuperpan.ToString());
            }

            if (SaveDefaultSettings || (DefaultRDP.full_address ?? "") != (full_address ?? ""))
            {
                rdpString = rdpString.Append("full address:").Append(full_address.GetTypeNameForRdp()).Append(':').AppendLine(full_address);
            }

            if (SaveDefaultSettings || DefaultRDP.gatewaycredentialssource != gatewaycredentialssource)
            {
                rdpString = rdpString.Append("gatewaycredentialssource:").Append(gatewaycredentialssource.GetTypeNameForRdp()).Append(':').AppendLine(gatewaycredentialssource.ToString());
            }

            if (SaveDefaultSettings || (DefaultRDP.gatewayhostname ?? "") != (gatewayhostname ?? ""))
            {
                rdpString = rdpString.Append("gatewayhostname:").Append(gatewayhostname.GetTypeNameForRdp()).Append(':').AppendLine(gatewayhostname);
            }

            if (SaveDefaultSettings || DefaultRDP.gatewayprofileusagemethod != gatewayprofileusagemethod)
            {
                rdpString = rdpString.Append("gatewayprofileusagemethod:").Append(gatewayprofileusagemethod.GetTypeNameForRdp()).Append(':').AppendLine(gatewayprofileusagemethod.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.gatewayusagemethod != gatewayusagemethod)
            {
                rdpString = rdpString.Append("gatewayusagemethod:").Append(gatewayusagemethod.GetTypeNameForRdp()).Append(':').AppendLine(gatewayusagemethod.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.keyboardhook != keyboardhook)
            {
                rdpString = rdpString.Append("keyboardhook:").Append(keyboardhook.GetTypeNameForRdp()).Append(':').AppendLine(keyboardhook.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.negotiate_security_layer != negotiate_security_layer)
            {
                rdpString = rdpString.Append("negotiate security layer:").Append(negotiate_security_layer.GetTypeNameForRdp()).Append(':').AppendLine(negotiate_security_layer.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.networkautodetect != networkautodetect)
            {
                rdpString = rdpString.Append("networkautodetect:").Append(networkautodetect.GetTypeNameForRdp()).Append(':').AppendLine(networkautodetect.ToString());
            }
            // If SaveDefaultSettings Or Not DefaultRDP.password_51 = password_51 Then RDPstring += "password 51" & ":" & password_51.GetType().ToString.Replace("System.", "").ToLower.Substring(0, 1) & ":" & password_51.ToString & vbCrLf
            if (SaveDefaultSettings || DefaultRDP.pinconnectionbar != pinconnectionbar)
            {
                rdpString = rdpString.Append("pinconnectionbar:").Append(pinconnectionbar.GetTypeNameForRdp()).Append(':').AppendLine(pinconnectionbar.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.prompt_for_credentials != prompt_for_credentials)
            {
                rdpString = rdpString.Append("prompt for credentials:").Append(prompt_for_credentials.GetTypeNameForRdp()).Append(':').AppendLine(prompt_for_credentials.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.prompt_for_credentials_on_client != prompt_for_credentials_on_client)
            {
                rdpString = rdpString.Append("prompt for credentials on client:").Append(prompt_for_credentials_on_client.GetTypeNameForRdp()).Append(':').AppendLine(prompt_for_credentials_on_client.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.promptcredentialonce != promptcredentialonce)
            {
                rdpString = rdpString.Append("promptcredentialonce:").Append(promptcredentialonce.GetTypeNameForRdp()).Append(':').AppendLine(promptcredentialonce.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.public_mode != public_mode)
            {
                rdpString = rdpString.Append("public mode:").Append(public_mode.GetTypeNameForRdp()).Append(':').AppendLine(public_mode.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.redirectclipboard != redirectclipboard)
            {
                rdpString = rdpString.Append("redirectclipboard:").Append(redirectclipboard.GetTypeNameForRdp()).Append(':').AppendLine(redirectclipboard.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.redirectcomports != redirectcomports)
            {
                rdpString = rdpString.Append("redirectcomports:").Append(redirectcomports.GetTypeNameForRdp()).Append(':').AppendLine(redirectcomports.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.redirectdirectx != redirectdirectx)
            {
                rdpString = rdpString.Append("redirectdirectx:").Append(redirectdirectx.GetTypeNameForRdp()).Append(':').AppendLine(redirectdirectx.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.redirectdrives != redirectdrives)
            {
                rdpString = rdpString.Append("redirectdrives:").Append(redirectdrives.GetTypeNameForRdp()).Append(':').AppendLine(redirectdrives.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.redirectposdevices != redirectposdevices)
            {
                rdpString = rdpString.Append("redirectposdevices:").Append(redirectposdevices.GetTypeNameForRdp()).Append(':').AppendLine(redirectposdevices.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.redirectprinters != redirectprinters)
            {
                rdpString = rdpString.Append("redirectprinters:").Append(redirectprinters.GetTypeNameForRdp()).Append(':').AppendLine(redirectprinters.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.redirectsmartcards != redirectsmartcards)
            {
                rdpString = rdpString.Append("redirectsmartcards:").Append(redirectsmartcards.GetTypeNameForRdp()).Append(':').AppendLine(redirectsmartcards.ToString());
            }

            if (SaveDefaultSettings || (DefaultRDP.remoteapplicationcmdline ?? "") != (remoteapplicationcmdline ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationcmdline:").Append(remoteapplicationcmdline.GetTypeNameForRdp()).Append(':').AppendLine(remoteapplicationcmdline);
            }

            if (SaveDefaultSettings || DefaultRDP.remoteapplicationexpandcmdline != remoteapplicationexpandcmdline)
            {
                rdpString = rdpString.Append("remoteapplicationexpandcmdline:").Append(remoteapplicationexpandcmdline.GetTypeNameForRdp()).Append(':').AppendLine(remoteapplicationexpandcmdline.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.remoteapplicationexpandworkingdir != remoteapplicationexpandworkingdir)
            {
                rdpString = rdpString.Append("remoteapplicationexpandworkingdir:").Append(remoteapplicationexpandworkingdir.GetTypeNameForRdp()).Append(':').AppendLine(remoteapplicationexpandworkingdir.ToString());
            }

            if (SaveDefaultSettings || (DefaultRDP.remoteapplicationfile ?? "") != (remoteapplicationfile ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationfile:").Append(remoteapplicationfile.GetTypeNameForRdp()).Append(':').AppendLine(remoteapplicationfile);
            }

            if (SaveDefaultSettings || (DefaultRDP.remoteapplicationfileextensions ?? "") != (remoteapplicationfileextensions ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationfileextensions:").Append(remoteapplicationfileextensions.GetTypeNameForRdp()).Append(':').AppendLine(remoteapplicationfileextensions);
            }

            if (SaveDefaultSettings || (DefaultRDP.remoteapplicationicon ?? "") != (remoteapplicationicon ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationicon:").Append(remoteapplicationicon.GetTypeNameForRdp()).Append(':').AppendLine(remoteapplicationicon);
            }

            if (SaveDefaultSettings || DefaultRDP.remoteapplicationmode != remoteapplicationmode)
            {
                rdpString = rdpString.Append("remoteapplicationmode:").Append(remoteapplicationmode.GetTypeNameForRdp()).Append(':').AppendLine(remoteapplicationmode.ToString());
            }

            if (SaveDefaultSettings || (DefaultRDP.remoteapplicationname ?? "") != (remoteapplicationname ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationname:").Append(remoteapplicationname.GetTypeNameForRdp()).Append(':').AppendLine(remoteapplicationname);
            }

            if (SaveDefaultSettings || (DefaultRDP.remoteapplicationprogram ?? "") != (remoteapplicationprogram ?? ""))
            {
                rdpString = rdpString.Append("remoteapplicationprogram:").Append(remoteapplicationprogram.GetTypeNameForRdp()).Append(':').AppendLine(remoteapplicationprogram);
            }

            if (SaveDefaultSettings || DefaultRDP.screen_mode_id != screen_mode_id)
            {
                rdpString = rdpString.Append("screen mode id:").Append(screen_mode_id.GetTypeNameForRdp()).Append(':').AppendLine(screen_mode_id.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.server_port != server_port)
            {
                rdpString = rdpString.Append("server port:").Append(server_port.GetTypeNameForRdp()).Append(':').AppendLine(server_port.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.session_bpp != session_bpp)
            {
                rdpString = rdpString.Append("session bpp:").Append(session_bpp.GetTypeNameForRdp()).Append(':').AppendLine(session_bpp.ToString());
            }

            if (SaveDefaultSettings || (DefaultRDP.shell_working_directory ?? "") != (shell_working_directory ?? ""))
            {
                rdpString = rdpString.Append("shell working directory:").Append(shell_working_directory.GetTypeNameForRdp()).Append(':').AppendLine(shell_working_directory);
            }

            if (SaveDefaultSettings || DefaultRDP.smart_sizing != smart_sizing)
            {
                rdpString = rdpString.Append("smart sizing:").Append(smart_sizing.GetTypeNameForRdp()).Append(':').AppendLine(smart_sizing.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.span_monitors != span_monitors)
            {
                rdpString = rdpString.Append("span monitors:").Append(span_monitors.GetTypeNameForRdp()).Append(':').AppendLine(span_monitors.ToString());
            }

            if (SaveDefaultSettings || DefaultRDP.superpanaccelerationfactor != superpanaccelerationfactor)
            {
                rdpString = rdpString.Append("superpanaccelerationfactor:").Append(superpanaccelerationfactor.GetTypeNameForRdp()).Append(':').AppendLine(superpanaccelerationfactor.ToString());
            }

            if (SaveDefaultSettings || (DefaultRDP.usbdevicestoredirect ?? "") != (usbdevicestoredirect ?? ""))
            {
                rdpString = rdpString.Append("usbdevicestoredirect:").Append(usbdevicestoredirect.GetTypeNameForRdp()).Append(':').AppendLine(usbdevicestoredirect);
            }

            if (SaveDefaultSettings || DefaultRDP.use_multimon != use_multimon)
            {
                rdpString = rdpString.Append("use multimon:").Append(use_multimon.GetTypeNameForRdp()).Append(':').AppendLine(use_multimon.ToString());
            }

            if (SaveDefaultSettings || (DefaultRDP.username ?? "") != (username ?? ""))
            {
                rdpString = rdpString.Append("username:").Append(username.GetTypeNameForRdp()).Append(':').AppendLine(username);
            }

            if (SaveDefaultSettings || DefaultRDP.videoplaybackmode != videoplaybackmode)
            {
                rdpString = rdpString.Append("videoplaybackmode:").Append(videoplaybackmode.GetTypeNameForRdp()).Append(':').AppendLine(videoplaybackmode.ToString());
            }

            if (SaveDefaultSettings || (DefaultRDP.winposstr ?? "") != (winposstr ?? ""))
            {
                rdpString = rdpString.Append("winposstr:").Append(winposstr.GetTypeNameForRdp()).Append(':').AppendLine(winposstr);
            }

            return rdpString.ToString();
        }

        public void LoadRDPfile(string FilePath)
        {
            var sr = new StreamReader(FilePath);
            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();
                var SplitLine = line.Split(':');
                if (string.IsNullOrEmpty(SplitLine[2]))
                {
                    continue;
                }
                if (SplitLine[0].Equals("administrative session"))
                {
                    administrative_session = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("allow desktop composition"))
                {
                    allow_desktop_composition = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("allow font smoothing"))
                {
                    allow_font_smoothing = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("alternate full address"))
                {
                    alternate_full_address = SplitLine[2];
                }
                else if (SplitLine[0].Equals("alternate shell"))
                {
                    alternate_shell = SplitLine[2];
                }
                else if (SplitLine[0].Equals("audiocapturemode"))
                {
                    audiocapturemode = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("audiomode"))
                {
                    audiomode = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("audioqualitymode"))
                {
                    audioqualitymode = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("authentication level"))
                {
                    authentication_level = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("autoreconnect max retries"))
                {
                    autoreconnect_max_retries = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("autoreconnection enabled"))
                {
                    autoreconnection_enabled = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("bandwidthautodetect"))
                {
                    bandwidthautodetect = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("bitmapcachepersistenable"))
                {
                    bitmapcachepersistenable = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("bitmapcachesize"))
                {
                    bitmapcachesize = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("compression"))
                {
                    compression = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("connect to console"))
                {
                    connect_to_console = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("connection type"))
                {
                    connection_type = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("desktop size id"))
                {
                    desktop_size_id = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("desktopheight"))
                {
                    desktopheight = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("desktopwidth"))
                {
                    desktopwidth = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("devicestoredirect"))
                {
                    devicestoredirect = SplitLine[2];
                }
                else if (SplitLine[0].Equals("disable ctrl+alt+del"))
                {
                    disable_ctrl_alt_del = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("disable full window drag"))
                {
                    disable_full_window_drag = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("disable menu anims"))
                {
                    disable_menu_anims = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("disable themes"))
                {
                    disable_themes = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("disable wallpaper"))
                {
                    disable_wallpaper = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("disableconnectionsharing"))
                {
                    disableconnectionsharing = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("disableremoteappcapscheck"))
                {
                    disableremoteappcapscheck = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("displayconnectionbar"))
                {
                    displayconnectionbar = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("domain"))
                {
                    domain = SplitLine[2];
                }
                else if (SplitLine[0].Equals("drivestoredirect"))
                {
                    drivestoredirect = SplitLine[2];
                }
                else if (SplitLine[0].Equals("enablecredsspsupport"))
                {
                    enablecredsspsupport = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("enablesuperpan"))
                {
                    enablesuperpan = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("full address"))
                {
                    full_address = SplitLine[2];
                }
                else if (SplitLine[0].Equals("gatewaycredentialssource"))
                {
                    gatewaycredentialssource = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("gatewayhostname"))
                {
                    gatewayhostname = SplitLine[2];
                }
                else if (SplitLine[0].Equals("gatewayprofileusagemethod"))
                {
                    gatewayprofileusagemethod = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("gatewayusagemethod"))
                {
                    gatewayusagemethod = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("keyboardhook"))
                {
                    keyboardhook = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("negotiate security layer"))
                {
                    negotiate_security_layer = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("networkautodetect"))
                {
                    networkautodetect = int.Parse(SplitLine[2]);
                }
                // If SplitLine(0) = "password 51" Then password_51 = SplitLine(2)
                else if (SplitLine[0].Equals("pinconnectionbar"))
                {
                    pinconnectionbar = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("prompt for credentials"))
                {
                    prompt_for_credentials = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("prompt for credentials on client"))
                {
                    prompt_for_credentials_on_client = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("promptcredentialonce"))
                {
                    promptcredentialonce = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("public mode"))
                {
                    public_mode = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("redirectclipboard"))
                {
                    redirectclipboard = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("redirectcomports"))
                {
                    redirectcomports = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("redirectdirectx"))
                {
                    redirectdirectx = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("redirectdrives"))
                {
                    redirectdrives = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("redirectposdevices"))
                {
                    redirectposdevices = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("redirectprinters"))
                {
                    redirectprinters = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("redirectsmartcards"))
                {
                    redirectsmartcards = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("remoteapplicationcmdline"))
                {
                    remoteapplicationcmdline = SplitLine[2];
                }
                else if (SplitLine[0].Equals("remoteapplicationexpandcmdline"))
                {
                    remoteapplicationexpandcmdline = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("remoteapplicationexpandworkingdir"))
                {
                    remoteapplicationexpandworkingdir = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("remoteapplicationfile"))
                {
                    remoteapplicationfile = SplitLine[2];
                }
                else if (SplitLine[0].Equals("remoteapplicationfileextensions"))
                {
                    remoteapplicationfileextensions = SplitLine[2];
                }
                else if (SplitLine[0].Equals("remoteapplicationicon"))
                {
                    remoteapplicationicon = SplitLine[2];
                }
                else if (SplitLine[0].Equals("remoteapplicationmode"))
                {
                    remoteapplicationmode = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("remoteapplicationname"))
                {
                    remoteapplicationname = SplitLine[2];
                }
                else if (SplitLine[0].Equals("remoteapplicationprogram"))
                {
                    remoteapplicationprogram = SplitLine[2];
                }
                else if (SplitLine[0].Equals("screen mode id"))
                {
                    screen_mode_id = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("server port"))
                {
                    server_port = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("session bpp"))
                {
                    session_bpp = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("shell working directory"))
                {
                    shell_working_directory = SplitLine[2];
                }
                else if (SplitLine[0].Equals("smart sizing"))
                {
                    smart_sizing = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("span monitors"))
                {
                    span_monitors = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("superpanaccelerationfactor"))
                {
                    superpanaccelerationfactor = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("usbdevicestoredirect"))
                {
                    usbdevicestoredirect = SplitLine[2];
                }
                else if (SplitLine[0].Equals("use multimon"))
                {
                    use_multimon = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("username"))
                {
                    username = SplitLine[2];
                }
                else if (SplitLine[0].Equals("videoplaybackmode"))
                {
                    videoplaybackmode = int.Parse(SplitLine[2]);
                }
                else if (SplitLine[0].Equals("winposstr"))
                {
                    winposstr = SplitLine[2];
                }
            }
        }
    }
}