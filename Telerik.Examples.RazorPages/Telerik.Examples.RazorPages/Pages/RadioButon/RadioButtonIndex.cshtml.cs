using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Telerik.Examples.RazorPages.Pages.RadioButon
{
    public class RadioButtonIndexModel : PageModel
    {
        [BindProperty]
        public bool IAgreeProp { get; set; }
        public void OnGet()
        {
            IAgreeProp = true;
        }
    }
}
