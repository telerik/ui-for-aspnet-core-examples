param(
    [string]$newDotnetVersion = "10", # Example: "10"
    [string]$sdkVersion = "10.0.0", # Example: "10.0.0"
    [string]$targetFramework = "net10.0" # Example: "net10.0"
)


function Update-CsprojFiles {
    param (
        [string[]]$csprojFiles,
        [hashtable]$packagesToNewVersionMap
    )

    foreach ($csprojFile in $csprojFiles) {
        if (Test-Path $csprojFile) {
            $csprojContent = Get-Content -Path $csprojFile -Raw

            if ($csprojContent -match "<TargetFramework>(.+)<") {
                $csprojContent = $csprojContent -replace "<TargetFramework>(.+)<", "<TargetFramework>$targetFramework<"
            }

            if($csprojContent -match "<TargetFrameworks>(.+)<"){
                 $csprojContent = $csprojContent -replace "<TargetFrameworks>(.+)<", "<TargetFrameworks>$targetFramework<"
            }

            foreach ($package in $packagesToNewVersionMap.GetEnumerator()) {
                $packageName = $package.Name
                $packageNewVersion = $package.Value

                # Look for the package reference and replace the version with the new one
                if ($csprojContent -match "<PackageReference Include=""$packageName"" Version=""(.+?)""") {
                    $csprojContent = $csprojContent -replace "<PackageReference Include=""$packageName"" Version=""(.+?)""", "<PackageReference Include=""$packageName"" Version=""$packageNewVersion"""
                }
            }

            $csprojContent | Set-Content -Path $csprojFile -NoNewline
        }
    }
}

function Get-CSProjFiles {
    param (
        [string[]]$pathsToExclude,
        [string[]]$dirsToUpdate
    )

    $csprojFiles = @()
    foreach ($dir in $dirsToUpdate) {
        $csprojFiles += Get-ChildItem -Path "./$dir" -Recurse -Filter *.csproj | Where-Object {
            $_.FullName -notin $pathsToExclude # Exclude the paths that we don't want to update
        } | Select-Object -ExpandProperty FullName
    }

    return $csprojFiles
}


function Get-PackagesToUpdate {
    param (
        [string[]]$csprojFiles,
        [string[]]$prefixes
    )

    $packages = @()

    foreach ($csprojFile in $csprojFiles) {
        if (Test-Path $csprojFile) {

            $csprojContent = Get-Content -Path $csprojFile -Raw
            $packagesInCurrentFile = [regex]::Matches($csprojContent, '<PackageReference Include="((' + ($prefixes -join '|') + ')\..+?)"')

            foreach ($package in $packagesInCurrentFile) {
                $packageName = $package.Groups[1].Value
                if ($packages -notcontains $packageName) {
                    $packages += $packageName
                }
            }
        }
    }

    return $packages
}


function Get-PackagesVersions {
    param (
        [string[]]$packagesToUpdate,
        [string]$newDotnetVersion
    )

    $packagesToNewVersionMap = @{}

    foreach ($packageName in $packagesToUpdate) {
        $packageNameLower = $packageName.ToLower()
        $url = "https://api.nuget.org/v3-flatcontainer/$packageNameLower/index.json"

        $packageInfo = Invoke-WebRequest -Uri $url -Method Get
        $packageInfoObj = $packageInfo | ConvertFrom-Json
        $packageVersions = $packageInfoObj.versions

      
        $packageVersions = $packageVersions | Where-Object {
            $_ -notmatch "beta"
        }

        $filteredVersions = $packageVersions | Where-Object {
            $_.StartsWith($newDotnetVersion) -and $_ -notmatch "maui"
        }


        $latestVersion = $filteredVersions | Select-Object -Last 1

        if($filteredVersions.count -eq 0) {
           $latestVersion = $packageVersions | Select-Object -Last 1
        }

        if($packageNameLower -eq 'system.io.abstractions') {
            continue
        }
        if ($null -ne $latestVersion) {
            $packagesToNewVersionMap.Add($packageName, $latestVersion)
        }
        else {
            Write-Host "No new version found for $packageName"
            continue
        }
    }

    
    return $packagesToNewVersionMap
}

# Can be used for debugging
# $newDotnetVersion = "10"
# $sdkVersion = "10.0.100"

# If there are projects that should not be updated, add their paths here
$pathsToExclude = @(
    # Example - "../src/Playground/Playground.csproj",
)

$dirsToUpdate = @(
    'Telerik.Examples.ContentSecurityPolicy',
    'Telerik.Examples.Mvc/Telerik.Examples.Mvc',
    'Kendo.FormFilling.ServerProcessing/Kendo.FormFilling.ServerProcessing',
    'KendoUI.FileManager.BlobStorage/KendoUI.FileManager.BlobStorage',
    'Telerik.Examples.RazorPages/Telerik.Examples.RazorPages'
)


# Get the .csproj files from the repo
$csprojFiles = Get-CSProjFiles -pathsToExclude $pathsToExclude -dirsToUpdate $dirsToUpdate 


$prefixes = @("Microsoft", "System") # If needed add more packages prefixes
# Iterate the csproj files and get all the packages that need to be updated
# The packages are filtered by the prefixes that we have defined above
$packagesToUpdate = Get-PackagesToUpdate -csprojFiles $csprojFiles -prefixes $prefixes

# Iterates the packages and gets the latest version for the new dotnet version
# The result is a map with the package name as key and the new version as value
$packagesToNewVersionMap = Get-PackagesVersions -packagesToUpdate $packagesToUpdate -newDotnetVersion $newDotnetVersion

Update-CsprojFiles -csprojFiles $csprojFiles -packagesToNewVersionMap $packagesToNewVersionMap