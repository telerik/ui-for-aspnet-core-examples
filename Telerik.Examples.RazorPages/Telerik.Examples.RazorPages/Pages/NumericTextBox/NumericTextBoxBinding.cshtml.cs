using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Telerik.Examples.RazorPages.Pages
{
    public class NumericTextBoxBindingModel : PageModel
    {

        [BindProperty]
        public int Price { get; set; }
        public void OnGet()
        {
        }
        public void OnPost()
        {
        }
    }
}
