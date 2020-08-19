using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.MultiSelect
{
    public class MultiSelectIndexModel : PageModel
    {
        public static List<Customer> customers;
        public void OnGet()
        {
            if (customers == null)
            {
                customers = new List<Customer>();

                Enumerable.Range(1, 50).ToList().ForEach(i => customers.Add(new Customer
                {
                    CustomerId = i,
                    Name = "Customer Name " + i,
                    Address = "city " + i
                }));

            }
        }

        public JsonResult OnGetRead()
        {            
            var filteredData = customers;
            return new JsonResult(filteredData);
        }
    }
}