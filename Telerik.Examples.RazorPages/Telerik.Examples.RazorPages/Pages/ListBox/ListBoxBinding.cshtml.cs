using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Telerik.Examples.RazorPages.Pages.ListBox
{
    public class ListBoxBindingModel : PageModel
    {
        public static IList<string> ListBoxItems;

        public void OnGet()
        {
            if (ListBoxItems == null)
            {
                ListBoxItems = new List<string>();

                ListBoxItems.Add("Steven White");
                ListBoxItems.Add("Nancy King");
                ListBoxItems.Add("Nancy Davolio");
                ListBoxItems.Add("Robert Davolio");
                ListBoxItems.Add("Michael Leverling");
                ListBoxItems.Add("Andrew Callahan");
                ListBoxItems.Add("Michael Suyama");
            }
        }

        public JsonResult OnGetReadOptional()
        {
            return new JsonResult(ListBoxItems);
        }
    }
}
