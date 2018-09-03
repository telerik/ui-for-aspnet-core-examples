using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPageGridTest.Pages.Customer
{
    public class IndexModel : PageModel
    {
        public List<RazorPageGridTest.Customer> Data { get; set; }

        public void OnGet()
        {
            Data = new List<RazorPageGridTest.Customer>();

            for (int i = 1; i <= 100; i++)
            {
                Data.Add(new RazorPageGridTest.Customer()
                {
                    CustomerId = i,
                    Name = "Name " + i.ToString(),
                    Address = "Address " + i.ToString(),
                    ClockOut = DateTime.Now.AddDays(i)
                });
            }
        }

        

        public JsonResult OnPostReadRecords()
        {
            List<RazorPageGridTest.Customer> data = new List<RazorPageGridTest.Customer>();

            for (int i = 1; i <= 100; i++)
            {
                data.Add(new RazorPageGridTest.Customer()
                {
                    CustomerId = i,
                    Name = "Name "+ i.ToString(),
                    Address = "Address " + i.ToString(),
                    ClockOut = DateTime.Now.AddHours(i)
                });
            }

            return new JsonResult(data);
        }

        public JsonResult OnPostUpdateRecord([DataSourceRequest] DataSourceRequest request, RazorPageGridTest.Customer customer)
        {
            System.Diagnostics.Debug.WriteLine("Updating");

            return new JsonResult(customer);
        }
        
    }
}