using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Filter
{
    public class FilterBindingModel : PageModel
    {
        public static IList<CustomerViewModel> Customers;
        public static IList<string> Countries = new List<string>() {"UK", "Germany", "Italy", "Venezuela", "China", "Bulgaria", "USA" };
        public static IList<string> Positions = new List<string>() { "Sales Agent", "Sales Representative",  "Owner",  "Order Administrator", "Marketing Manager", "Accounting Manager" };

        public void OnGet()
        {
            if (Customers == null)
            {
                Customers = new List<CustomerViewModel>();
                var rand = new Random();
                for (int i = 1; i < 40; i++)
                {
                    Customers.Add(new CustomerViewModel() { 
                        CustomerID=i,
                        CompanyName="Company " + i,
                        Position = Positions[rand.Next(0,5)],
                        Country = Countries[rand.Next(0, 6)],
                    });
                }
            }
        }

        public JsonResult OnPostCustomers([DataSourceRequest]DataSourceRequest request)
        {
            return new JsonResult(Customers.ToDataSourceResult(request));
        }
    }
}
