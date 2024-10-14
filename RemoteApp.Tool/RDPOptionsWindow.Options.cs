using System;

namespace RemoteApp.Tool
{
    public partial class RDPOptionsWindow
    {
        /// <summary>
        ///     A single entry of the .rdp file option catalogue.
        /// </summary>
        internal sealed class RdpOption
        {
            public RdpOption(string name, string key, string type, string defaultValue, string description)
            {
                Name = name;
                Key = key;
                Type = type;
                DefaultValue = defaultValue;
                Description = description;
                Value = string.Empty;
            }

            /// <summary>Internal name used for persistence.</summary>
            public string Name { get; }

            /// <summary>Key as written into the .rdp file.</summary>
            public string Key { get; }

            /// <summary>"i" for integer options, "s" for string options.</summary>
            public string Type { get; }

            /// <summary>Documented default, only used for the description text.</summary>
            public string DefaultValue { get; }

            public string Description { get; }

            /// <summary>Current value; empty means "do not write this option".</summary>
            public string Value { get; set; }

            public RdpOption Clone()
            {
                return new RdpOption(Name, Key, Type, DefaultValue, Description) { Value = Value };
            }
        }

        /// <summary>
        ///     Catalogue of every .rdp option this window can edit. Generated from the upstream
        ///     RDPOptionsWindow.vb option table; keep the order and wording in sync with it.
        /// </summary>
        private static readonly RdpOption[] OptionCatalogue =
        {
            new RdpOption("administrative_session", "administrative session", "i", "0", "Connect to the administrative session of the remote computer.\\n\\n0 - Do not use the administrative session.\\n1 - Connect to the administrative session."),
            new RdpOption("allow_desktop_composition", "allow desktop composition", "i", "0", "Determines whether desktop composition (needed for Aero) is permitted when you log on to the remote computer.\\n\\n0 - Disable desktop composition in the remote session.\\n1 - Desktop composition is permitted."),
            new RdpOption("allow_font_smoothing", "allow font smoothing", "i", "0", "Determines whether font smoothing may be used in the remote session.\\n\\n0 - Disable font smoothing in the remote session.\\n1 - Font smoothing is permitted."),
            new RdpOption("audiocapturemode", "audiocapturemode", "i", "0", "Determines how sounds captured (recorded) on the local computer are handled when you are connected to the remote computer.\\n\\n0 - Do not capture audio from the local computer.\\n1 - Capture audio from the local computer and send to the remote computer."),
            new RdpOption("audiomode", "audiomode", "i", "0", "Determines how sounds on a remote computer are handled when you are connected to the remote computer.\\n\\n0 - Play sounds on the local computer.\\n1 - Play sounds on the remote computer.\\n2 - Do not play sounds."),
            new RdpOption("audioqualitymode", "audioqualitymode", "i", "0", "Determines the quality of the audio played in the remote session.\\n\\n0 - Dynamically adjust audio quality based on available bandwidth.\\n1 - Always use medium audio quality.\\n2 - Always use uncompressed audio quality."),
            new RdpOption("authentication_level", "authentication level", "i", "2", "Determines what should happen when server authentication fails.\\n\\n0 - If server authentication fails, connect without giving a warning.\\n1 - If server authentication fails, do not connect.\\n2 - If server authentication fails, show a warning and allow the user to connect or not.\\n3 - Server authentication is not required."),
            new RdpOption("autoreconnect_max_retries", "autoreconnect max retries", "i", "20", "Determines the maximum number of times the client computer will try to reconnect to the remote computer if the connection is dropped.\\nNote: The maximum value Remote Desktop can handle is 200."),
            new RdpOption("autoreconnection_enabled", "autoreconnection enabled", "i", "1", "Determines whether the client computer will automatically try to reconnect to the remote computer if the connection is dropped.\\n\\n0 - Do not attempt to reconnect.\\n1 - Attempt to reconnect."),
            new RdpOption("bandwidthautodetect", "bandwidthautodetect", "i", "1", "Enables the option for automatic detection of the network type. Used in conjunction with networkautodetect. Also see connection type.\\n\\n0 - Do not enable the option for automatic network detection.\\n1 - Enable the option for automatic network detection."),
            new RdpOption("bitmapcachepersistenable", "bitmapcachepersistenable", "i", "1", "Determines whether bitmaps are cached on the local computer (disk-based cache). Bitmap caching can improve the performance of your remote session.\\n\\n0 - Do not cache bitmaps.\\n1 - Cache bitmaps."),
            new RdpOption("bitmapcachesize", "bitmapcachesize", "i", "1500", "Specifies the size in kilobytes of the memory-based bitmap cache. The maximum value is 32000."),
            new RdpOption("camerastoredirect", "camerastoredirect", "s", "", "Configures which cameras to redirect. This setting uses a semicolon-delimited list of KSCATEGORY_VIDEO_CAMERA interfaces of cameras enabled for redirection."),
            new RdpOption("compression", "compression", "i", "1", "Determines whether the connection should use bulk compression.\\n\\n0 - Do not use bulk compression.\\n1 - Use bulk compression."),
            new RdpOption("connect_to_console", "connect to console", "i", "0", "Connect to the console session of the remote computer.\\n\\n0 - Connect to a normal session.\\n1 - Connect to the console screen."),
            new RdpOption("connection_type", "connection type", "i", "2", "Specifies pre-defined performance settings for the Remote Desktop session.\\n\\n1 - Modem (56 Kbps).\\n2 - Low-speed broadband (256 Kbps - 2 Mbps).\\n3 - Satellite (2 Mbps - 16 Mbps with high latency).\\n4 - High-speed broadband (2 Mbps - 10 Mbps).\\n5 - WAN (10 Mbps or higher with high latency).\\n6 - LAN (10 Mbps or higher).\\n7 - Automatic bandwidth detection. Requires bandwidthautodetect.\\n\\nBy itself, this setting does nothing. When selected in the RDC GUI, this option changes several performance related settings (themes, animation, font smoothing, etcetera). These separate settings always overrule the connection type setting."),
            new RdpOption("desktop_size_id", "desktop size id", "i", "0", "Specifies pre-defined dimensions of the remote session desktop.\\n\\n0 - 640x480.\\n1 - 800x600.\\n2 - 1024x768.\\n3 - 1280x1024.\\n4 - 1600x1200.\\n\\nThis setting is ignored when either /w and /h, or desktopwidth and desktopheight are already specified."),
            new RdpOption("desktopheight", "desktopheight", "i", "600", "The height (in pixels) of the remote session desktop."),
            new RdpOption("desktopscalefactor", "desktopscalefactor", "i", "", "Specifies the scale factor of the remote session to make the content appear larger.\\n\\nSupported values:\\nNumerical value from the following list: 100, 125, 150, 175, 200, 250, 300, 400, 500\\n\\nNote: The desktopscalefactor property is being deprecated and will soon be unavailable."),
            new RdpOption("desktopwidth", "desktopwidth", "i", "800", "The width (in pixels) of the remote session desktop."),
            new RdpOption("devicestoredirect", "devicestoredirect", "s", "", "Determines which supported Plug and Play devices on the client computer will be redirected and available in the remote session.\\n\\nNo value specified - Do not redirect any supported Plug and Play devices.\\n* - Redirect all supported Plug and Play devices, including ones that are connected later.\\nDynamicDevices - Redirect any supported Plug and Play devices that are connected later.\\nThe hardware ID for one or more Plug and Play devices - Redirect the specified supported Plug and Play device(s)."),
            new RdpOption("disable_ctrl+alt+del", "disable ctrl+alt+del", "i", "1", "Determines whether you have to press CTRL+ALT+DELETE before entering credentials after you are connected to the remote computer.\\n\\n0 - CTRL+ALT+DELETE is required before logging in.\\n1 - CTRL+ALT+DELETE is not required. You can logon immediately.\\n\\nNote: When disabled, this setting will also delay the autologin until the user has pressed CTRL+ALT+DELETE."),
            new RdpOption("disable_full_window_drag", "disable full window drag", "i", "1", "Determines whether window content is displayed when you drag the window to a new location.\\n\\n0 - Show the contents of the window while dragging.\\n1 - Show an outline of the window while dragging."),
            new RdpOption("disable_menu_anims", "disable menu anims", "i", "1", "Determines whether menus and windows can be displayed with animation effects in the remote session.\\n\\n0 - Menu and window animation is permitted.\\n1 - No menu and window animation."),
            new RdpOption("disable_themes", "disable themes", "i", "0", "Determines whether themes are permitted when you log on to the remote computer.\\n\\n0 - Themes are permitted.\\n1 - Disable theme in the remote session."),
            new RdpOption("disable_wallpaper", "disable wallpaper", "i", "1", "Determines whether the desktop background is displayed in the remote session.\\n\\n0 - Display the wallpaper.\\n1 - Do not show any wallpaper."),
            new RdpOption("disableconnectionsharing", "disableconnectionsharing", "i", "0", "Determines whether a new Terminal Server session is started with every launch of a RemoteApp to the same computer and with the same credentials.\\n\\n0 - No new session is started. The currently active session of the user is shared.\\n1 - A new login session is started for the RemoteApp."),
            new RdpOption("disableremoteappcapscheck", "disableremoteappcapscheck", "i", "0", "Specifies whether the Remote Desktop client should check the remote computer for RemoteApp capabilities.\\n0 - Check the remote computer for RemoteApp capabilities before logging in.\\n1 - Do not check the remote computer for RemoteApp capabilities.Note: This setting must be set to 1 when connecting to Windows XP SP3, Vista or 7 computers with RemoteApps configured on them."),
            new RdpOption("displayconnectionbar", "displayconnectionbar", "i", "1", "Determines whether the connection bar appears when you are in full screen mode.\\n\\n0 - Do not show the connection bar.\\n1 - Show the connection bar."),
            new RdpOption("domain", "domain", "s", "", "Specifies the name of the domain of the user."),
            new RdpOption("drivestoredirect", "drivestoredirect", "s", "", "Determines which local disk drives on the client computer will be redirected and available in the remote session.\\n\\nNo value specified - Do not redirect any drives.\\n* - Redirect all disk drives, including drives that are connected later.\\nDynamicDrives - Redirect any drives that are connected later.\\nThe drive and labels for one or more drives - Redirect the specified drive(s)."),
            new RdpOption("dynamic_resolution", "dynamic resolution", "i", "1", "Determines whether the resolution of a remote session is automatically updated when the local window is resized.\\n\\n0 - Session resolution remains static during the session.\\n1 - Session resolution updates as the local window resizes."),
            new RdpOption("enablecredsspsupport", "enablecredsspsupport", "i", "1", "Determines whether Remote Desktop will use CredSSP for authentication if it's available.\\n\\n0 - Do not use CredSSP, even if the operating system supports it.\\n1 - Use CredSSP, if the operating system supports it."),
            new RdpOption("enablerdsaadauth", "enablerdsaadauth", "i", "0", "Determines whether the client will use Microsoft Entra ID to authenticate to the remote PC. When used with Azure Virtual Desktop, this provides a single sign-on experience. This property replaces the property targetisaadjoined.\\n\\n0 - Connections won't use Microsoft Entra authentication, even if the remote PC supports it.\\n1 - Connections will use Microsoft Entra authentication if the remote PC supports it."),
            new RdpOption("enablesuperpan", "enablesuperpan", "i", "0", "Determines whether SuperPan is enabled or disabled. SuperPan allows the user to navigate a remote desktop in full-screen mode without scroll bars, when the dimensions of the remote desktop are larger than the dimensions of the current client window. The user can point to the window border, and the desktop view will scroll automatically in that direction.\\n\\n0 - Do not use SuperPan. The remote session window is sized to the client window size.\\n1 - Enable SuperPan. The remote session window is sized to the dimensions specified through /w and /h, or through desktopwidth and desktopheight."),
            new RdpOption("encode_redirected_video_capture", "encode redirected video capture", "i", "1", "Enables or disables encoding of redirected video.\\n\\n0 - Disable encoding of redirected video.\\n\\n1 - Enable encoding of redirected video."),
            new RdpOption("gatewaycredentialssource", "gatewaycredentialssource", "i", "4", "Specifies the credentials that should be used to validate the connection with the RD Gateway.\\n\\n0 - Ask for password (NTLM).\\n1 - Use smart card.\\n4 - Allow user to select later."),
            new RdpOption("kdcproxyname", "kdcproxyname", "s", "", "Specifies the fully qualified domain name of a KDC proxy."),
            new RdpOption("keyboardhook", "keyboardhook", "i", "2", "Determines how Windows key combinations are applied when you are connected to a remote computer.\\n\\n0 - Windows key combinations are applied on the local computer.\\n1 - Windows key combinations are applied on the remote computer.\\n2 - Windows key combinations are applied in full-screen mode only."),
            new RdpOption("maximizetocurrentdisplays", "maximizetocurrentdisplays", "i", "0", "Determines which display a remote session uses for full screen on when maximizing. Requires use multimon set to 1. Only available on Windows App for Windows and the Remote Desktop app for Windows.\\n\\n0 - Session is full screen on the displays initially selected when maximizing.\\n1 - Session dynamically is full screen on the displays the session window spans when maximizing."),
            new RdpOption("negotiate_security_layer", "negotiate security layer", "i", "1", "Determines whether the level of security is negotiated or not.\\n\\n0 - Security layer negotiation is not enabled and the session is started by using Secure Sockets Layer (SSL).\\n1 - Security layer negotiation is enabled and the session is started by using x.224 encryption."),
            new RdpOption("networkautodetect", "networkautodetect", "i", "1", "Determines whether to use automatic network bandwidth detection or not. Requires the option bandwidthautodetect to be set and correlates with connection type 7.\\n\\n0 - Use automatic network bandwitdh detection.\\n1 - Do not use automatic network bandwitdh detection."),
            new RdpOption("pinconnectionbar", "pinconnectionbar", "i", "1", "Determines whether or not the connection bar should be pinned to the top of the remote session upon connection when in full screen mode.\\n\\n0 - The connection bar should not be pinned to the top of the remote session.\\n1 - The connection bar should be pinned to the top of the remote session."),
            new RdpOption("prompt_for_credentials", "prompt for credentials", "i", "0", "Determines whether Remote Desktop Connection will prompt for credentials when connecting to a remote computer for which the credentials have been previously saved.\\n\\n0 - Remote Desktop will use the saved credentials and will not prompt for credentials.\\n1 - Remote Desktop will prompt for credentials."),
            new RdpOption("prompt_for_credentials_on_client", "prompt for credentials on client", "i", "0", "Determines whether Remote Desktop Connection will prompt for credentials when connecting to a server that does not support server authentication.\\n\\n0 - Remote Desktop will not prompt for credentials.\\n1 - Remote Desktop will prompt for credentials."),
            new RdpOption("promptcredentialonce", "promptcredentialonce", "i", "1", "When connecting through an RD Gateway, determines whether RDC should use the same credentials for both the RD Gateway and the remote computer.\\n\\n0 - Remote Desktop will not use the same credentials .\\n1 - Remote Desktop will use the same credentials for both the RD gateway and the remote computer."),
            new RdpOption("public_mode", "public mode", "i", "0", "Determines whether Remote Desktop Connection will be started in public mode.\\n\\n0 - Remote Desktop will not start in public mode .\\n1 - Remote Desktop will start in public mode and will not save any user data (credentials, bitmap cache, MRU) on the local machine."),
            new RdpOption("redirectclipboard", "redirectclipboard", "i", "1", "Determines whether the clipboard on the client computer will be redirected and available in the remote session and vice versa.\\n\\n0 - Do not redirect the clipboard.\\n1 - Redirect the clipboard."),
            new RdpOption("redirectcomports", "redirectcomports", "i", "0", "Determines whether the COM (serial) ports on the client computer will be redirected and available in the remote session.\\n\\n0 - The COM ports on the local computer are not available in the remote session.\\n1 - The COM ports on the local computer are available in the remote session."),
            new RdpOption("redirectdirectx", "redirectdirectx", "i", "1", "Determines whether DirectX will be enabled for the remote session.\\n\\n0 - Do not enable DirectX rendering.\\n1 - Enable DirectX rendering in the remote session."),
            new RdpOption("redirectdrives", "redirectdrives", "i", "0", "Determines whether local disk drives on the client computer will be redirected and available in the remote session.\\n\\n0 - The drives on the local computer are not available in the remote session.\\n1 - The drives on the local computer are available in the remote session.\\n\\nNote: This setting is replaced by drivestoredirect from RDC 6.0 onward."),
            new RdpOption("redirected_video_capture_encoding_quality", "redirected video capture encoding quality", "i", "0", "Controls the quality of encoded video.\\n\\n0 - High compression video. Quality may suffer when there's a lot of motion.\\n1 - Medium compression.\\n2 - Low compression video with high picture quality."),
            new RdpOption("redirectlocation", "redirectlocation", "i", "0", "Determines whether the location of the local device will be redirected and available in the remote session.\\n\\n0 - The remote session uses the location of the remote computer.\\n1 - The remote session uses the location of the local device."),
            new RdpOption("redirectposdevices", "redirectposdevices", "i", "0", "Determines whether Microsoft Point of Service (POS) for .NET devices connected to the client computer will be redirected and available in the remote session.\\n\\n0 - The POS devices from the local computer are not available in the remote session.\\n1 - The POS devices from the local computer are available in the remote session."),
            new RdpOption("redirectprinters", "redirectprinters", "i", "1", "Determines whether printers configured on the client computer will be redirected and available in the remote session.\\n\\n0 - The printers on the local computer are not available in the remote session.\\n1 - The printers on the local computer are available in the remote session."),
            new RdpOption("redirectsmartcards", "redirectsmartcards", "i", "1", "Determines whether smart card devices on the client computer will be redirected and available in the remote session.\\n\\n0 - The smart card device on the local computer is not available in the remote session.\\n1 - The smart card device on the local computer is available in the remote session."),
            new RdpOption("redirectwebauthn", "redirectwebauthn", "i", "1", "Determines whether WebAuthn requests on the remote computer will be redirected to the local computer allowing the use of local authenticators (such as Windows Hello for Business and security key).\\n\\n0 - WebAuthn requests from the remote session aren't sent to the local computer for authentication and must be completed in the remote session.\\n1 - WebAuthn requests from the remote session are sent to the local computer for authentication."),
            new RdpOption("remoteapplicationcmdline", "remoteapplicationcmdline", "s", "", "Optional command line parameters for the RemoteApp."),
            new RdpOption("remoteapplicationexpandworkingdir", "remoteapplicationexpandworkingdir", "i", "0", "Determines whether environment variables contained in the RemoteApp working directory parameter should be expanded locally or remotely.\\n\\n0 - Environment variables should be expanded to the values of the local computer.\\n1 - Environment variables should be expanded on the remote computer to the values of the remote computer.\\n\\nNote: The RemoteApp working directory is specified through the shell working directory parameter."),
            new RdpOption("remoteapplicationicon", "remoteapplicationicon", "s", "", "Specifies the file name of an icon file to be displayed in the Remote Desktop interface while starting the RemoteApp. By default RDC will show the standard Remote Desktop icon.\\n\\nNote: Only .ico files are supported."),
            new RdpOption("screen_mode_id", "screen mode id", "i", "2", "Determines whether the remote session window appears full screen when you connect to the remote computer.\\n\\n1 - The remote session will appear in a window.\\n2 - The remote session will appear full screen."),
            new RdpOption("selectedmonitors", "selectedmonitors", "s", "", "Specifies which local displays to use for the remote session. The selected displays must be contiguous. Requires use multimon to be set to 1.\\n\\nComma separated list of machine-specific display IDs. You can retrieve IDs by calling mstsc.exe /l. The first ID listed will be set as the primary display in the session. Defaults to all displays."),
            new RdpOption("session_bpp", "session bpp", "i", "32", "Determines the color depth (in bits) on the remote computer when you connect.\\n\\n8 - 256 colors (8 bit).\\n15 - High color (15 bit).\\n16 - High color (16 bit).\\n24 - True color (24 bit).\\n32 - Highest quality (32 bit)."),
            new RdpOption("shell_working_directory", "shell working directory", "s", "", "The working directory on the remote computer to be used if an alternate shell is specified."),
            new RdpOption("signscope", "signscope", "s", "", "Comma-delimited list of .rdp file settings for which the signature is generated when using .rdp file signing."),
            new RdpOption("singlemoninwindowedmode", "singlemoninwindowedmode", "i", "0", "Determines whether a multi display remote session automatically switches to single display when exiting full screen. Requires use multimon set to 1. Only available on Windows App for Windows and the Remote Desktop app for Windows.\\n\\n0 - A remote session retains all displays when exiting full screen.\\n1 - A remote session switches to a single display when exiting full screen."),
            new RdpOption("smart_sizing", "smart sizing", "i", "0", "Determines whether the client computer should scale the content on the remote computer to fit the window size of the client computer when the window is resized.\\n\\n0 - The client window display will not be scaled when resized.\\n1 - The client window display will be scaled when resized."),
            new RdpOption("span_monitors", "span monitors", "i", "0", "Determines whether the remote session window will be spanned across multiple monitors when you connect to the remote computer.\\n\\n0 - Monitor spanning is not enabled.\\n1 - Monitor spanning is enabled.\\n\\nNote: When using Remote Desktop Connection 7 (Windows 7/2008), the use multimon setting is recommended."),
            new RdpOption("superpanaccelerationfactor", "superpanaccelerationfactor", "i", "1", "Specifies the number of pixels that the screen view scrolls in a given direction for every pixel of mouse movement by the client when in SuperPan mode"),
            new RdpOption("targetisaadjoined", "targetisaadjoined", "i", "0", "Allows connections to Microsoft Entra joined session hosts using a username and password. This property is only applicable to non-Windows clients and local Windows devices that aren't joined to Microsoft Entra.\\n\\n0 - Connections to Microsoft Entra joined session hosts will succeed for Windows devices that meet the requirements, but other connections will fail.\\n1 - Connections to Microsoft Entra joined hosts will succeed but are restricted to entering user name and password credentials when connecting to session hosts.\\n\\nNote: This property is being replaced by enablerdsaadauth."),
            new RdpOption("usbdevicestoredirect", "usbdevicestoredirect", "s", "", "Determines which supported RemoteFX USB devices on the client computer will be redirected and available in the remote session when you connect to a remote session that supports RemoteFX USB redirection.\\n\\nNo value specified - Do not redirect any supported RemoteFX USB devices.\\n* - Redirect all supported RemoteFX USB devices for redirection that are not redirected by high-level redirection mechanisms.\\n{Device Setup Class GUID} - Redirect all supported RemoteFX USB devices that are members of the specified device setup class.\\nUSB\\InstanceID - Redirect the supported RemoteFX USB device specified by the given instance ID.\\n-USB\\InstanceID - Do not redirect the supported RemoteFX USB device specified by the given instance ID, even if the device is in a device setup class that is redirected.."),
            new RdpOption("use_multimon", "use multimon", "i", "0", "Determines whether the session should use true multiple monitor support when connecting to the remote computer.\\n\\n0 - Do not enable multiple monitor support.\\n1 - Enable multiple monitor support."),
            new RdpOption("username", "username", "s", "", "Specifies the name of the user account that will be used to log on to the remote computer."),
            new RdpOption("videoplaybackmode", "videoplaybackmode", "i", "1", "Determines whether RDC will use RDP efficient multimedia streaming for video playback.\\n\\n0 - Do not use RDP efficient multimedia streaming for video playback.\\n1 - Use RDP efficient multimedia streaming for video playback when possible."),
            new RdpOption("winposstr", "winposstr", "s", "0,3,0,0,800,600", "Specifies the position and dimensions of the session window on the client computer."),
            new RdpOption("workspaceid", "workspaceid", "s", "", "This setting defines the RemoteApp and Desktop ID associated with the RDP file that contains this setting.\\n\\nNo"),
        };

        /// <summary>
        ///     Options used the first time the client connection window runs, replacing the
        ///     hard coded defaults that used to live in CreateRdpFile. Rows are
        ///     [internal name, rdp key, type, value].
        /// </summary>
        internal static readonly string[,] RecommendedDefaults = new string[,]
        {
            { "disableremoteappcapscheck", "disableremoteappcapscheck", "i", "1" },
            { "drivestoredirect", "drivestoredirect", "s", "*" },
            { "prompt_for_credentials", "prompt for credentials", "i", "1" },
            { "promptcredentialonce", "promptcredentialonce", "i", "0" },
            { "redirectcomports", "redirectcomports", "i", "1" },
            { "span_monitors", "span monitors", "i", "1" },
            { "use_multimon", "use multimon", "i", "1" },
        };
    }
}

