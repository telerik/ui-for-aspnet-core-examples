using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Telerik.Examples.RazorPages.Pages.Loader
{
    public class LoaderIndexModel : PageModel
    {
        public LoaderSize Size { get; set; }

        public LoaderThemeColor ThemeColor { get; set; }
        public void OnGet()
        {
            ThemeColor = LoaderThemeColor.Success;
            Size = LoaderSize.Large;
        }
    }
}
