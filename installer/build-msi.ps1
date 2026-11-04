# Builds the product MSI with WiX v4/v5.
# Usage: pwsh -File installer\build-msi.ps1 [-Version 6.1.1.0] [-Output dist\RemoteApp.Tool.msi]
param(
    [string] $Version = '6.1.1.0',
    [string] $Output = 'dist\RemoteApp.Tool.msi'
)

$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent

& (Join-Path $root 'build.ps1') -Configuration Release

$buildDir = Join-Path $root 'RemoteApp.Tool\bin\Release'
if (-not (Test-Path (Join-Path $buildDir 'RemoteApp.Tool.exe'))) {
    throw "Build output not found in $buildDir"
}

$wixPath = (Get-Command wix -ErrorAction SilentlyContinue).Source
if (-not $wixPath) {
    $localWix = Join-Path $root '.wix\wix.exe'
    if (Test-Path $localWix) {
        $wixPath = $localWix
    }
    else {
        throw 'wix.exe not found. Install it with: dotnet tool install --tool-path .wix wix --version 5.0.2, then: .wix\wix.exe extension add -g WixToolset.UI.wixext/5.0.2'
    }
}

$outputPath = Join-Path $root $Output
New-Item -ItemType Directory -Force (Split-Path $outputPath -Parent) | Out-Null

& $wixPath build (Join-Path $PSScriptRoot 'RemoteAppTool.wxs') `
    -ext WixToolset.UI.wixext `
    -d "BuildDir=$buildDir" `
    -d "ProductVersion=$Version" `
    -o $outputPath

if ($LASTEXITCODE -ne 0 -or -not (Test-Path $outputPath)) {
    throw 'MSI build failed.'
}

Write-Host "Created $outputPath"
