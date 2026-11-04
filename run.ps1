# Builds and launches the app straight from the build output, so iterating does not mean
# reinstalling the MSI every time.
#
#   pwsh -File run.ps1                  # incremental build, then launch (UAC prompt)
#   pwsh -File run.ps1 -NoBuild         # just launch what is already built
#   pwsh -File run.ps1 -NoLaunch        # build only, e.g. before attaching a debugger
#
# The MSI is only needed to test packaging itself; everything else runs fine from bin\.
param(
    [ValidateSet('Debug', 'Release')]
    [string] $Configuration = 'Release',

    [switch] $NoBuild,

    [switch] $NoLaunch
)

$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot
$exe = Join-Path $root "RemoteApp.Tool\bin\$Configuration\RemoteApp.Tool.exe"

if (-not $NoBuild) {
    & pwsh -NoProfile -File (Join-Path $root 'build.ps1') -Configuration $Configuration -Incremental
    if ($LASTEXITCODE -ne 0) { throw 'Build failed.' }
}

if (-not (Test-Path $exe)) {
    throw "Not built yet: $exe. Run without -NoBuild first."
}

Write-Host "Launching $exe"

if ($NoLaunch) {
    Write-Host '(no launch requested)'
    return
}

# Replace a copy that is still open, but leave an installed copy alone.
Get-Process 'RemoteApp.Tool' -ErrorAction SilentlyContinue |
    Where-Object { $_.Path -eq $exe } |
    ForEach-Object { $_.CloseMainWindow() | Out-Null; Start-Sleep -Milliseconds 300; if (-not $_.HasExited) { $_.Kill() } }

# The app needs administrator rights to write HKLM\...\TSAppAllowList, and relaunches itself
# if it does not have them. Starting it elevated here avoids a second process and keeps the
# console alive so you can see early failures.
$identity = [Security.Principal.WindowsIdentity]::GetCurrent()
$elevated = (New-Object Security.Principal.WindowsPrincipal($identity)).IsInRole(
    [Security.Principal.WindowsBuiltInRole]::Administrator)

if ($elevated) {
    & $exe
}
else {
    Start-Process -FilePath $exe -Verb RunAs
}
