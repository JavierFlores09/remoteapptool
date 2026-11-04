# RemoteApp Tool — v2 (WPF, .NET 8)

This is [stivencp32-ux's `v2-modernization`](https://github.com/stivencp32-ux/remoteapptool/tree/v2-modernization)
**rebased onto the C# tree** instead of the original VB.NET tree.

In the fork it was based on, `src-v2` sat next to the untouched VB.NET solution, so the
"modernization" started from a dead code base. Here its parent is `modernize/csharp-port`, the C#
port running at upstream 6.1.1, with the product MSI and the upstream fixes already in place.

## What is in here

| project | target | contents |
|---|---|---|
| `RemoteAppTool.App` | `net8.0-windows` (WPF) | Material Design shell, `RemoteAppsView`, `ClientConnectionView`, `HostOptionsView`, `AboutView` |
| `RemoteAppTool.Core` | `net8.0-windows` | registry access (`SystemRemoteApps`), `Rdp2Msi`, `ClientSettings` |
| `RemoteAppTool.RdpFileLib` | `net8.0-windows` | `.rdp` reader/writer |
| `RemoteAppTool.SignLib` | `net8.0-windows` | `rdpsign` wrapper |

Build:

```powershell
dotnet build src-v2\RemoteAppTool.sln -c Release
```

## Status: prototype, not at parity with the WinForms app

The upstream app at the root of this repository is the shipping one. The WPF prototype covers:

- list / create / edit / delete RemoteApps in the TSAppAllowList registry
- per-app `.rdp` and `.msi` generation (`Rdp2Msi`, `WixInstalled()` pre-check)
- client connection defaults (server, port, gateway, redirections) stored per user
- host options (timeouts, allow-list toggle)

Missing compared with `../RemoteApp.Tool` (WinForms), in rough order of importance:

1. **RDP options editor** — no equivalent of `RDPOptionsWindow`; the prototype only exposes a free
   text box for extra options.
2. **File type associations** — `FileTypeAssociation` exists in the model but there is no UI.
3. **Icon picking** — a path text box, no `IconLib` extraction or icon index selection.
4. **Duplicate app / backup to `.reg`**.
5. **Signing UI** — `RdpSign` is wired in Core, there is no certificate selector.
6. **Localisation** — hardcoded English strings.

So this tree is a UI/architecture reference, not a replacement yet.

## The next step: share one core

Right now `src-v2` carries its own copies of the registry, `.rdp` and signing code, because the
root libraries target `net48` and WPF needs `net8.0-windows`. Duplicating them is the main
maintenance hazard of this layout. The intended fix:

1. Convert `RemoteAppLib`, `RDPFileLib`, `RDPSign` and `LockChecker` to SDK-style projects that
   multi-target `net48;net8.0-windows` (`UseWindowsForms` is needed by `RDPFileLib` for its locked
   file dialogs, or those `MessageBox` calls move behind an interface).
2. Point both `RemoteApp.Tool` (net48) and `RemoteAppTool.App` (net8) at those shared projects and
   delete `RemoteAppTool.Core` / `RemoteAppTool.RdpFileLib` / `RemoteAppTool.SignLib`.
3. Align the model: `RemoteAppLib.FileTypeAssociationCollection` vs `List<FileTypeAssociation>`,
   and move `ClientSettings` into the shared library so both front-ends read the same per-user
   settings file.
4. Only then port the missing features from step 1 to the WPF shell.
