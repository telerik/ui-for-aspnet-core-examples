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

function Update_LayoutVersions {
    param (
        [string[]]$layoutPathsToUpdate
    )
   

    foreach($layoutPath in $layoutPathsToUpdate) {
        Replace_Reference -path $layoutPath -oldReference $oldKendoVersion -newReference $newKendoVersion
        Replace_Reference -path $layoutPath -oldReference $oldThemesVersion -newReference $newThemesVersion
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