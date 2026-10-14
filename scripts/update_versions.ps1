param(
    [string]$oldKendoVersion = "2025.3.825",
    [string]$newKendoVersion = "2025.4.1111",
    
    [string]$oldThemesVersion = "12.0.0",
    [string]$newThemesVersion = "12.0.1",


    [string]$oldWebCaptchaVersion = "2.0.3",
    [string]$newWebCaptchaVersion = "2.0.4",

    [string]$oldCommonWebVersions = "2025.3.812",
    [string]$newCommonWebVersions = "2025.3.813"
)

function Replace_Reference{
    param(
        [string]$path,
        [string]$oldReference,
        [string]$newReference
    )

     (Get-Content -Path $path) |
             ForEach-Object {$_ -Replace $oldReference , $newReference} |
                 Set-Content -Path $path
}

function Get_JsDelivrIntegrity {
    param(
        [string]$packageName,
        [string]$version,
        [string]$filePath
    )

    if ($version -notmatch '^\d+\.\d+\.\d+(?:\.\d+)?$') {
        throw "Invalid package version: $version"
    }

    $metadata = Invoke-RestMethod -Uri "https://data.jsdelivr.com/v1/package/npm/$packageName@$version/flat"
    if (-not $metadata.files) {
        throw "jsDelivr returned invalid flat metadata for $packageName@$version"
    }

    $file = $metadata.files | Where-Object { $_.name -eq "/$filePath" } | Select-Object -First 1
    if (-not $file -or $file.hash -notmatch '^[A-Za-z0-9+/]{43}=$') {
        throw "jsDelivr metadata has no SHA-256 hash for $packageName@$version/$filePath"
    }

    return "sha256-$($file.hash)"
}

function Update_LayoutIntegrity {
    param(
        [string]$layoutPath,
        [string]$fileName,
        [string]$integrity
    )

    $content = Get-Content -Path $layoutPath -Raw
    $pattern = '(' + [regex]::Escape($fileName) + '"\s+integrity=")[^"]+'
    if ([regex]::Matches($content, $pattern).Count -ne 1) {
        throw "Expected one integrity attribute for $fileName in $layoutPath"
    }

    $content = [regex]::Replace($content, $pattern, ('${1}' + $integrity))
    Set-Content -Path $layoutPath -Value $content -NoNewline
}

function Update_LayoutVersions {
    param (
        [string[]]$layoutPathsToUpdate
    )

    $kendoAllIntegrity = Get_JsDelivrIntegrity -packageName '@progress/kendo-ui' -version $newKendoVersion -filePath 'umd/kendo.all.min.js'
    $kendoAspNetMvcIntegrity = Get_JsDelivrIntegrity -packageName '@progress/kendo-ui' -version $newKendoVersion -filePath 'umd/kendo.aspnetmvc.min.js'
    $themeIntegrity = Get_JsDelivrIntegrity -packageName '@progress/kendo-theme-default' -version $newThemesVersion -filePath 'dist/default-ocean-blue.css'

    foreach($layoutPath in $layoutPathsToUpdate) {
        Replace_Reference -path $layoutPath -oldReference $oldKendoVersion -newReference $newKendoVersion
        Replace_Reference -path $layoutPath -oldReference $oldThemesVersion -newReference $newThemesVersion
        Update_LayoutIntegrity -layoutPath $layoutPath -fileName 'kendo.all.min.js' -integrity $kendoAllIntegrity
        Update_LayoutIntegrity -layoutPath $layoutPath -fileName 'kendo.aspnetmvc.min.js' -integrity $kendoAspNetMvcIntegrity
        Update_LayoutIntegrity -layoutPath $layoutPath -fileName 'default-ocean-blue.css' -integrity $themeIntegrity
    }

}

function Update_ProjectVersions {
    param (
        [string[]]$csprojPathsToUpdate
    )
   
    foreach($csprojPath in $csprojPathsToUpdate) {
        Replace_Reference -path $csprojPath -oldReference $oldKendoVersion -newReference $newKendoVersion
        Replace_Reference -path $csprojPath -oldReference "Include=""Telerik.Web.Captcha"" Version=""$oldWebCaptchaVersion""" -newReference "Include=""Telerik.Web.Captcha"" Version=""$newWebCaptchaVersion"""
        Replace_Reference -path $csprojPath -oldReference "Include=""Telerik.Web.PDF"" Version=""$oldCommonWebVersions""" -newReference "Include=""Telerik.Web.PDF"" Version=""$newCommonWebVersions"""
        Replace_Reference -path $csprojPath -oldReference "Include=""Telerik.Web.Spreadsheet"" Version=""$oldCommonWebVersions""" -newReference "Include=""Telerik.Web.Spreadsheet"" Version=""$newCommonWebVersions"""
        Replace_Reference -path $csprojPath -oldReference "Include=""Telerik.Core.Export"" Version=""$oldCommonWebVersions""" -newReference "Include=""Telerik.Core.Export"" Version=""$newCommonWebVersions"""
    }
}

$layoutPathsToUpdate = @(
    'Telerik.Examples.ContentSecurityPolicy\Views\Shared\_Layout.cshtml',
    'Telerik.Examples.Mvc\Telerik.Examples.Mvc\Views\Shared\_Layout.cshtml',
    'Telerik.Examples.RazorPages\Telerik.Examples.RazorPages\Pages\Shared\_Layout.cshtml'
)

$csprojPathsToUpdate = @(
    'Telerik.Examples.ContentSecurityPolicy\Telerik.Examples.ContentSecurityPolicy.csproj',
    'Telerik.Examples.RazorPages\Telerik.Examples.RazorPages\Telerik.Examples.RazorPages.csproj',
    'Telerik.Examples.Mvc\Telerik.Examples.Mvc\Telerik.Examples.Mvc.csproj'
)

Update_LayoutVersions -layoutPathsToUpdate $layoutPathsToUpdate
Update_ProjectVersions -csprojPathsToUpdate $csprojPathsToUpdate