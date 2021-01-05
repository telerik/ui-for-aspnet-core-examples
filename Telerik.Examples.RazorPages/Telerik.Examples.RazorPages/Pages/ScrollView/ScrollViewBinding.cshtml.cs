using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.ScrollView
{
    public class ScrollViewBindingModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
