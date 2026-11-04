<meta name="google-site-verification" content="MGrMbBi28Ut57MeqKI6srDSmlrprH0lug6rwNMIi0_A" />

# RemoteApp Tool

## C# fork, updated to upstream 6.1.1

This tree is [zbalkan's C# port](https://github.com/zbalkan/remoteapptool) (commit `ebed775`) brought
back in line with upstream [kimmknight/remoteapptool](https://github.com/kimmknight/remoteapptool)
`master` (`9c97cbd`, 2024-10-15, version 6.1.1), plus the fixes listed below; this build reports itself as 6.1.1.1.
All seven projects are C#; the VB.NET originals are gone.

## What changed in this branch

### Upstream work ported onto the original conversion

* `RDPOptionsWindow` — the per-option .rdp editor (upstream issue #100) including the 2024 additions
  from issue #141 (`enablerdsaadauth`, `kdcproxyname`, `targetisaadjoined`, `desktopscalefactor`,
  `dynamic_resolution`, `maximizetocurrentdisplays`, `singlemoninwindowedmode`,
  `remoteapplicationcmdline`). The .rdp serializer gained the matching options plus
  `AdditionalOptions` for free-form entries. The 76-entry option catalogue is generated from the
  upstream VB table so the wording is identical.
* Duplicate RemoteApp (issue #52), with the new alias `<name> copy`.
* IPv6 addresses accepted in the address fields (issue #65).
* Default RemoteApp icon taken from `user32.dll` instead of `mstsc.exe` (issue #128).
* Per-app icon caching so icons are only re-read when an app is added or edited (issue #136).
* WiX detection fixed (PR #84).
* App selection preserved after add/edit, About window shows the company name, version 6.1.1.

### Defects of the original port fixed here

* `LockChecker` returned `"No locks\r\n"`, so every caller that compares against `"No locks"`
  believed the target file was locked and skipped writing the .rdp/.msi. This made saving look
  broken; found by calling the built assemblies, not by reading the code.
* `My.Settings` had no equivalent: settings were read from `App.config` and never written, so window
  size and connection defaults were lost on every exit. They are now persisted to
  `%APPDATA%\RemoteApp Tool\settings.dat`.
* `RDPOptionsWindow` never called `InitializeComponent`. VB does that implicitly for
  `DesignerGenerated` forms, C# does not, so the dialog opened blank.
* `SystemRemoteApps` cached both registry handles before `Init()` created the
  `TSAppAllowList\Applications` key, so the first `GetAll()` threw a `NullReferenceException` on any
  host that never had a RemoteApp — the common first run. The handles are now opened on demand
  (upstream has the same shape).
* A failed MSI build (missing `candle.exe`, WiX 4/5 without those tools) escaped the click handler
  as an unhandled exception; it now shows a message naming the WiX v3 requirement.
* Leftover `*.New.csproj.bak` files removed, output assembly renamed from `RDP2MSILib.New` to `RDP2MSILib`.

## Requirements

| | |
|---|---|
| OS | Windows 7 SP1 / Server 2008 R2 or later (the tool manages the local RDS host) |
| Runtime | .NET Framework 4.8 |
| Privileges | Administrator — the app relaunches itself elevated and writes `HKLM\...\Terminal Server\TSAppAllowList` |
| Per-app MSI generation | **WiX Toolset v3** (`candle.exe`, `light.exe`). WiX 4/5 does not ship those tools; the product MSI in `installer\` does use WiX 5 |
| RDP signing (optional) | `rdpsign.exe` from the Windows SDK |

Publishing a RemoteApp only works on a Windows edition that can host them (Server, or a client with
the RDS/RemoteApp feature). On Windows Home the tool runs and writes the registry entries, but the
client connection will fail.

## Packaging

`installer\RemoteAppTool.wxs` builds a per-machine MSI (Start-menu and desktop shortcuts, ARP icon,
major upgrade). `installer\build-msi.ps1` builds it locally; `.github/workflows/release.yml` stamps
the version from a `v*` tag, builds both the portable zip and the MSI and publishes them as a
release. The MSI was verified to build and contain the executable, its config and the five
dependencies.

## Where this came from

The work was assembled from a survey of the upstream forks:

* [`zbalkan/remoteapptool`](https://github.com/zbalkan/remoteapptool) — the C# conversion this branch starts from.
* `kimmknight/remoteapptool` — upstream work, re-implemented in C# rather than merged, so the deleted
  VB.NET projects do not come back.
* `danielb7390` (PR #84) — WiX detection fix.
* [`matherm-aboehm/remoteapptool`](https://github.com/matherm-aboehm/remoteapptool) — caret crash fix.
* [`wiggins-kong/remoteapptool`](https://github.com/wiggins-kong/remoteapptool) — WiX installer layout and
  the release workflow. Its Simplified-Chinese string replacement was not taken: that belongs in
  satellite resources rather than replacing the English text.
* `KaneGreen` (IconLib as source) and `rob89m` (net48, no committed signing key, IconLib 0.74.3) are
  already covered by this tree.

A separate WPF / .NET 8 front-end built on this same base lives on the `modernize/wpf-on-csharp`
branch under `src-v2`.

## Building

Requires MSBuild (the Visual Studio 2022 Build Tools are enough). `IconLib` 0.74.3 comes from NuGet
(PackageReference). If the .NET Framework 4.8 targeting pack is not installed, `build.ps1` falls back
to the reference assemblies from the NuGet cache.

```powershell
pwsh -File run.ps1                   # incremental build, then launch from bin\ (no MSI needed)
pwsh -File build.ps1                 # full Release build of the whole solution
pwsh -File installer\build-msi.ps1   # optional: product MSI (needs the WiX tool, see the script)
```

`build.ps1` restores the NuGet packages itself, so no separate restore step is needed.

You do not have to install the MSI to try a change. `run.ps1` rebuilds incrementally — a couple of
seconds when little changed — and starts `RemoteApp.Tool\bin\<Configuration>\RemoteApp.Tool.exe`
elevated; installing is only needed to test the package itself. `-NoBuild` launches what is already
there, `-NoLaunch` builds without starting the app. Settings live in
`%APPDATA%\RemoteApp Tool\settings.dat`, so a copy running from `bin\` and an installed copy share
them.

## WPF prototype (`src-v2`)

`src-v2` holds stivencp32-ux's .NET 8 / WPF / Material Design rewrite, rebased onto this tree instead
of the original VB.NET code. It is a separate solution and a work in progress: it covers the app list,
per-app .rdp/.msi generation and client defaults, but not yet the .rdp options editor, file type
associations, icon picking or signing. See [src-v2/README.md](src-v2/README.md) for the gap list and
the plan to share one core between both front-ends.

```powershell
pwsh -File build-all.ps1             # both front-ends
```

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

### Features

* Create and manage RemoteApps on Windows desktops and servers
* Generate RDP files
* Generate MSI installers (requires WiX Toolset)
* Use a Remote Desktop Gateway
* Set options such as session timeouts
* Select icons for your apps
* File type associations for deployed apps
* Sign RDP files
* Backup RemoteApps

### Requirements

* Microsoft .Net Framework 4
* [WiX Toolset](http://wixtoolset.org/) (If you want to create MSIs. Reboot after installing.)
* A **supported** edition of Windows XP, 7, 8, 10, or Server. See the [compatibility chart](https://github.com/kimmknight/remoteapptool/wiki/Windows-Compatibility).

**Note:** If you try to host RemoteApps on an incompatible edition of Windows (eg. Windows 10 Home), the tool will run but RemoteApps ***will not work***. The RDP client will appear to be connecting, then just disappear with no error shown.

### Download

**Latest:**

[RemoteApp Tool 6.0.0.0 Installer](https://github.com/kimmknight/remoteapptool/releases/download/v6.0.0.0/RemoteApp.Tool.6000.msi)

[RemoteApp Tool 6.0.0.0 Zip](https://github.com/kimmknight/remoteapptool/releases/download/v6.0.0.0/RemoteApp.Tool.6000.zip)

Please note: The latest installer no longer works on Windows XP, use the Zip instead.

**Previous:**

[RemoteApp Tool 5.3.0.0 Installer](http://www.kimknight.net/remoteapptool/RemoteApp%20Tool%205300.msi)

[RemoteApp Tool 5.3.0.0 Zip](http://www.kimknight.net/remoteapptool/remoteapptool5300.zip)

### User guide

[How to create a RemoteApp and use it on another computer](https://github.com/kimmknight/remoteapptool/wiki/Create-a-RemoteApp-and-use-it-on-another-computer)

### Screenshots

![Screenshot 1](https://raw.githubusercontent.com/wiki/kimmknight/remoteapptool/images/screenshots/ss1.png)

![Screenshot 2](https://raw.githubusercontent.com/wiki/kimmknight/remoteapptool/images/screenshots/ss2.png)

![Screenshot 3](https://raw.githubusercontent.com/wiki/kimmknight/remoteapptool/images/screenshots/ss3.png)

![Screenshot 4](https://raw.githubusercontent.com/wiki/kimmknight/remoteapptool/images/screenshots/ss4.png)

