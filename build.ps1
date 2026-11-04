# Builds the solution for .NET Framework 4.8 without requiring a full Visual Studio install.
# Usage: pwsh -File build.ps1 [-Configuration Release]
param(
    [ValidateSet('Debug', 'Release')]
    [string] $Configuration = 'Release'
)

$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot
$solution = Join-Path $root 'RemoteApp Tool.sln'

function Find-MSBuild {
    $vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
    if (Test-Path $vswhere) {
        $path = & $vswhere -latest -products '*' -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' |
            Select-Object -First 1
        if ($path) { return $path }
    }

    foreach ($candidate in @(
            "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe",
            "${env:ProgramFiles}\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe",
            "${env:ProgramFiles}\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe")) {
        if (Test-Path $candidate) { return $candidate }
    }

    throw 'MSBuild.exe not found. Install the Visual Studio 2022 Build Tools.'
}

# The .NET Framework 4.8 targeting pack is often missing on build agents. The NuGet package with the
# reference assemblies is enough; pass its location to MSBuild as TargetFrameworkRootPath.
function Find-ReferenceAssemblies {
    $candidates = @(
        (Join-Path $root 'packages\Microsoft.NETFramework.ReferenceAssemblies.net48\build')
    )

    $globalPackages = $null
    if (Get-Command dotnet -ErrorAction SilentlyContinue) {
        $globalPackages = (& dotnet nuget locals global-packages --list) -replace '^global-packages:\s*', ''
    }
    if (-not $globalPackages) { $globalPackages = Join-Path $env:USERPROFILE '.nuget\packages' }

    $packageDir = Join-Path $globalPackages 'microsoft.netframework.referenceassemblies.net48'
    if (Test-Path $packageDir) {
        $candidates += Get-ChildItem $packageDir -Directory |
            Sort-Object Name -Descending |
            ForEach-Object { Join-Path $_.FullName 'build' }
    }

    return $candidates | Where-Object { Test-Path (Join-Path $_ '.NETFramework\v4.8') } | Select-Object -First 1
}

$msbuild = Find-MSBuild
$arguments = @($solution, '/t:Rebuild', "/p:Configuration=$Configuration", '/v:m', '/nologo')

# IconLib comes from NuGet (PackageReference), so restore before building.
Write-Host 'Restoring NuGet packages...'
& $msbuild $solution /t:Restore /v:q /nologo | Out-Null
if ($LASTEXITCODE -ne 0) { throw 'NuGet restore failed.' }

$referenceAssemblies = Find-ReferenceAssemblies
if ($referenceAssemblies) {
    Write-Host "Using .NET Framework reference assemblies from $referenceAssemblies"
    $arguments += "/p:TargetFrameworkRootPath=$referenceAssemblies"
}
else {
    Write-Host 'Using the installed .NET Framework 4.8 targeting pack'
}

& $msbuild @arguments
if ($LASTEXITCODE -ne 0) { throw "Build failed with exit code $LASTEXITCODE" }
Write-Host "Build succeeded ($Configuration)."
