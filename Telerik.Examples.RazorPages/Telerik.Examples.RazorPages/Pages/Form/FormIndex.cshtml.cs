using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Form
{
    public class FormIndexModel : PageModel
    {
        public OrderViewModel Order = new OrderViewModel();

        public void OnGet()
        {

        }

        public IActionResult OnPostSubmit(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return new JsonResult(new { success = "Form Posted Successfully" });

        }
    }
}