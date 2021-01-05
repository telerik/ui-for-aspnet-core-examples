using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Form
{
    public class FormStandardSubmitModel : PageModel
    {
        [BindProperty]
        public OrderViewModel Order { get; set; }
        [TempData]
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
            if (Order == null)
            {
                Order = new OrderViewModel();
            }
        }

        public IActionResult OnPost()
        {
            var model = Request.Form;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("Success");

        }
    }
}
