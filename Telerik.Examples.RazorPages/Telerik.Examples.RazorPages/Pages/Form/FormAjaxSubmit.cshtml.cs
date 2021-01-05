using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Form
{
    public class FormAjaxSubmitModel : PageModel
    {
        [BindProperty]
        public OrderViewModel Order { get; set; }

        public void OnGet()
        {
            if (Order == null)
            {
                Order = new OrderViewModel();
            }
        }

        public IActionResult OnPostSubmit(OrderViewModel model)
        {
            //handle server validation and add model errors to be returned to the View
            if (model.ShipName != "John Doe")
            {
                ModelState.AddModelError("ShipName", "Ship Name is incorrect");
            }

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