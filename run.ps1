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

# Stop a copy running from this build first: it holds bin\ open and the build would fail
# with "the file is locked by RemoteApp.Tool.exe". An installed copy is left alone.
# Reading Path of an elevated process from a normal shell can fail, hence the try; when the
# path is unknown the process is only reported, never killed.
if (-not $NoLaunch) {
    $unknown = @()
    Get-Process 'RemoteApp.Tool' -ErrorAction SilentlyContinue | ForEach-Object {
        $processPath = $null
        try { $processPath = $_.Path } catch { }
        if ($processPath -eq $exe) {
            $_.CloseMainWindow() | Out-Null
            Start-Sleep -Milliseconds 400
            if (-not $_.HasExited) {
                $_.Kill()
                $_.WaitForExit(2000) | Out-Null
            }
        }
        elseif (-not $processPath) {
            $unknown += $_.Id
        }
    }

    if ($unknown.Count -gt 0) {
        Write-Warning "Another RemoteApp Tool instance is running (pid $($unknown -join ', ')). If the build fails with a locked file, close it first."
    }
}

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
