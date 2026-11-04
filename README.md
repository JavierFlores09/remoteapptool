<meta name="google-site-verification" content="MGrMbBi28Ut57MeqKI6srDSmlrprH0lug6rwNMIi0_A" />

# RemoteApp Tool

## C# fork, updated to upstream 6.1.1

This tree is [zbalkan's C# port](https://github.com/zbalkan/remoteapptool) (commit `ebed775`) brought
back in line with upstream [kimmknight/remoteapptool](https://github.com/kimmknight/remoteapptool)
`master` (`9c97cbd`, 2024-10-15, version 6.1.1). All seven projects are C#; the VB.NET originals are gone.

Upstream work that this branch ports on top of the original conversion:

* `RDPOptionsWindow` — the per-option .rdp editor (upstream issue #100) including the 2024 additions
  from issue #141 (`enablerdsaadauth`, `kdcproxyname`, `targetisaadjoined`, `desktopscalefactor`,
  `dynamic_resolution`, `maximizetocurrentdisplays`, `singlemoninwindowedmode`,
  `remoteapplicationcmdline`). The .rdp serializer gained the matching options plus
  `AdditionalOptions` for free-form entries.
* Duplicate RemoteApp (issue #52), with the new alias `<name> copy`.
* IPv6 addresses accepted in the address fields (issue #65).
* Default RemoteApp icon taken from `user32.dll` instead of `mstsc.exe` (issue #128).
* Per-app icon caching so icons are only re-read when an app is added or edited (issue #136).
* WiX detection fixed (`candle.exe` is a file, not a directory; PR #84).
* App selection preserved after add/edit, About window shows the company name, version 6.1.1.

Defects of the original port that are fixed here:

* `LockChecker` returned `"No locks\r\n"`, so every caller that compares against `"No locks"`
  believed the target file was locked and skipped writing the .rdp/.msi.
* `My.Settings` had no equivalent: settings were read from `App.config` and never written.
  They are now persisted to `%APPDATA%\RemoteApp Tool\settings.dat`.
* Leftover `*.New.csproj.bak` files removed, output assembly renamed from `RDP2MSILib.New` to `RDP2MSILib`.

## Building

Requires MSBuild (the Visual Studio 2022 Build Tools are enough). `IconLib` 0.74.3 comes from NuGet
(PackageReference). If the .NET Framework 4.8 targeting pack is not installed, `build.ps1` falls back
to the reference assemblies from the NuGet cache.

```powershell
pwsh -File build.ps1                 # Release build of the whole solution
pwsh -File installer\build-msi.ps1   # optional: product MSI (needs the WiX tool, see the script)
```

`build.ps1` restores the NuGet packages itself, so no separate restore step is needed.

## Roadmap

- [x] Convert every project to C#
- [x] Catch up with upstream 6.1.1
- [ ] Write unit tests for the libraries
- [ ] Refactor the `My.*` shims (`Forms.cs`, `UserSettings.cs`)
- [ ] Migrate the libraries to SDK-style projects and `net8.0-windows`

## Original Readme
With Microsoft RemoteApp technology, you can seamlessly use an application that is running on another computer.

RemoteApp Tool is a utility that allows you to create/manage RemoteApps hosted on Windows (7, 8, 10, XP and Server) as well as generate RDP and MSI files for clients.

If you want your RemoteApps to appear in the Start Menu of your clients, or via a web interface, check out [RAWeb](https://github.com/kimmknight/raweb)!

If you have questions, comments or suggestions about RemoteApp Tool, please visit the [forum](https://groups.google.com/forum/embed/?place=forum/remoteapptool).

## Features

* Create and manage RemoteApps on Windows desktops and servers
* Generate RDP files
* Generate MSI installers (requires WiX Toolset)
* Use a Remote Desktop Gateway
* Set options such as session timeouts
* Select icons for your apps
* File type associations for deployed apps
* Sign RDP files
* Backup RemoteApps

## Requirements

* Microsoft .Net Framework 4
* [WiX Toolset](http://wixtoolset.org/) (If you want to create MSIs. Reboot after installing.)
* A **supported** edition of Windows XP, 7, 8, 10, or Server. See the [compatibility chart](https://github.com/kimmknight/remoteapptool/wiki/Windows-Compatibility).

**Note:** If you try to host RemoteApps on an incompatible edition of Windows (eg. Windows 10 Home), the tool will run but RemoteApps ***will not work***. The RDP client will appear to be connecting, then just disappear with no error shown.

## Download

**Latest:**

[RemoteApp Tool 6.0.0.0 Installer](https://github.com/kimmknight/remoteapptool/releases/download/v6.0.0.0/RemoteApp.Tool.6000.msi)

[RemoteApp Tool 6.0.0.0 Zip](https://github.com/kimmknight/remoteapptool/releases/download/v6.0.0.0/RemoteApp.Tool.6000.zip)

Please note: The latest installer no longer works on Windows XP, use the Zip instead.

**Previous:**

[RemoteApp Tool 5.3.0.0 Installer](http://www.kimknight.net/remoteapptool/RemoteApp%20Tool%205300.msi)

[RemoteApp Tool 5.3.0.0 Zip](http://www.kimknight.net/remoteapptool/remoteapptool5300.zip)

## User guide

[How to create a RemoteApp and use it on another computer](https://github.com/kimmknight/remoteapptool/wiki/Create-a-RemoteApp-and-use-it-on-another-computer)

## Screenshots

![Screenshot 1](https://raw.githubusercontent.com/wiki/kimmknight/remoteapptool/images/screenshots/ss1.png)

![Screenshot 2](https://raw.githubusercontent.com/wiki/kimmknight/remoteapptool/images/screenshots/ss2.png)

![Screenshot 3](https://raw.githubusercontent.com/wiki/kimmknight/remoteapptool/images/screenshots/ss3.png)

![Screenshot 4](https://raw.githubusercontent.com/wiki/kimmknight/remoteapptool/images/screenshots/ss4.png)
