using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Telerik.Examples.RazorPages.Pages.Wizard
{
    public class WizardIndexModel : PageModel
    {
        [BindProperty]
        public UserModel UserViewModel { get; set; }
       

        public void OnGet()
        {
            
             UserViewModel = new UserModel() 
             { 
                 AccountDetails = new Account() 
                 { 
                     Username = "kev123", 
                     Email = "kevin@mymail.com" 
                 },
                 PersonalDetails = new Person()
                 {
                     FullName = "Kevin Carter",
                     Country = "Norway"
                 }
             };
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


        public class UserModel
        {
            public Account  AccountDetails { get; set; }
            public Person PersonalDetails { get; set; }
        }


        public class Account
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public class Person
        {
            [Required]
            public string FullName { get; set; }

            [Required]
            public string Country { get; set; }
            public string About { get; set; }
        }
    }
}
