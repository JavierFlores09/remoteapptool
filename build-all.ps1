# Builds both front-ends: the shipping WinForms app (.NET Framework 4.8) and the
# WPF prototype (net8.0-windows).
# Usage: pwsh -File build-all.ps1 [-Configuration Release] [-SkipWpf]
param(
    [ValidateSet('Debug', 'Release')]
    [string] $Configuration = 'Release',
    [switch] $SkipWpf
)

$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot

Write-Host '=== WinForms / .NET Framework 4.8 ==='
& pwsh -NoProfile -File (Join-Path $root 'build.ps1') -Configuration $Configuration

if ($SkipWpf) { return }

Write-Host '=== WPF / net8.0-windows (prototype) ==='
dotnet build (Join-Path $root 'src-v2\RemoteAppTool.sln') -c $Configuration --nologo -v minimal
if ($LASTEXITCODE -ne 0) { throw "WPF build failed with exit code $LASTEXITCODE" }
Write-Host 'Both front-ends built.'
