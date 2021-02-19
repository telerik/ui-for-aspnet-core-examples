using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Wizard
{
    public class WizardAjaxSubmitModel : PageModel
    {
        [BindProperty]
        public UserViewModel User { get; set; }

        public void OnGet()
        {
            if (User == null)
            {
                User = new UserViewModel() 
                { 
                        UserId = 1,
                        Name = "Laura Jones",
                        City = "Valencia"
                };
            }
        }

        public IActionResult OnPostSubmit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState
                                 where item.Value.Errors.Any()
                                 select item).ToDictionary(
                                    kvp => kvp.Key,
                                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                                );

                Response.StatusCode = 400;
                return new JsonResult(new { errors = errorList });
            }
            return new JsonResult(new { success = "Form Posted Successfully" });
        }
    }
}
