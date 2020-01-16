using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.RazorPages.Models;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Telerik.Examples.RazorPages.Pages.Grid
{
    public class GridCustomDataSourceModel : PageModel
    {
        public JsonResult OnPostReadRecords()
        {
            List<Customer> data = new List<Customer>();

            for (int i = 1; i <= 100; i++)
            {
                data.Add(new Customer()
                {
                    CustomerId = i,
                    Name = "Name " + i.ToString(),
                    Address = "Address " + i.ToString(),
                    ClockOut = DateTime.Now.AddHours(i)
                });
            }

            return new JsonResult(data);
        }

        public JsonResult OnPostUpdateRecord([DataSourceRequest] DataSourceRequest request, Customer customer)
        {
            System.Diagnostics.Debug.WriteLine("Updating");

            return new JsonResult(customer);
        }

    }
}