using System;
using System.Linq;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Grid
{
    public class MultiSelectAsEditorModel : PageModel
    {
        public static IList<StoreViewModel> stores;

        public void OnGet()
        {
            if (stores == null)
            {
                stores = new List<StoreViewModel>();

                Enumerable.Range(1, 50).ToList().ForEach(i => stores.Add(new StoreViewModel
                {
                    StoreID = i,
                    StoreName = "StoreName " + i,
                    Budget = i * 1000,
                    EmployeesCount = i * 25,
                    Customers = new List<Customer>()
                    {
                        new Customer()
                        {
                            CustomerId = i,
                            Name = "CustomerName" + i,
                            Address = "Address" + i,
                            ClockOut = DateTime.Now.AddHours(i)
                        }
                    }
                }));
            }
        }

        public JsonResult OnPostCreate([DataSourceRequest] DataSourceRequest request, StoreViewModel store)
        {
            store.StoreID = stores.Count + 3;
            stores.Add(store);

            return new JsonResult(new[] { store }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(stores.ToDataSourceResult(request));
        }

        public JsonResult OnPostUpdate([DataSourceRequest] DataSourceRequest request, StoreViewModel store)
        {
            stores.Where(s => s.StoreID == store.StoreID).Select(s => store);

            return new JsonResult(new[] { store }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDestroy([DataSourceRequest] DataSourceRequest request, StoreViewModel store)
        {
            stores.Remove(stores.FirstOrDefault(s => s.StoreID == store.StoreID));

            return new JsonResult(new[] { store }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnGetCustomers()
        {
            var customers = new List<Customer>();

            Enumerable.Range(1, 20).ToList().ForEach(i => customers.Add(new Customer
            {
                CustomerId = i,
                Name = "CustomerName " + i,
                Address = "Address " + i,
                ClockOut = DateTime.Now.AddHours(i)
            }));

            return new JsonResult(customers);
        }
    }
}
