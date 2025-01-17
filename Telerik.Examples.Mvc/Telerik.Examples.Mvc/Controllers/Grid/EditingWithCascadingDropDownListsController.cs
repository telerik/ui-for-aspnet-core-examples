using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Controllers.Grid
{
    public class EditingWithCascadingDropDownListsController : Controller
    {
        private readonly IEnumerable<Vendor> vendors;
        private readonly IEnumerable<Customer> customers;
        private readonly IEnumerable<Item> items;

        public ActionResult EditingWithCascadingDropDownLists()
        {
            return View();
        }

        public EditingWithCascadingDropDownListsController()
        {
            this.items = Enumerable.Range(1, 125)
                .Select(i => new Item
                {
                    ItemId = i,
                    ItemName = "ItemName " + i,
                    VendorId = (i - 1) / 5 + 1,
                });

            this.vendors = Enumerable.Range(1, 25)
                .Select(i => new Vendor
                {
                    VendorId = i,
                    VendorName = "VendorName " + i,
                    CustomerId = (i - 1) / 5 + 1
                });

            this.customers = Enumerable.Range(1, 5)
                .Select(i => new Customer
                {
                    CustomerId = i,
                    CustomerName = "CustomerName " + i,
                });
        }

        [HttpPost]
        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var licenses = Enumerable.Range(1, 50)
                .Select(i => new License
                {
                    Customer = new Customer { CustomerId = 1, CustomerName = "CustomerName 1" },
                    Vendor = new Vendor { VendorId = 1, VendorName = "VendorName 1", CustomerId = 1 },
                    LicenseId = i,
                    Item = new Item { ItemId = 1, ItemName = "ItemName 1", VendorId = 1 }
                });

            return Json(licenses.ToDataSourceResult(request));
        }

        [HttpPost]
        public JsonResult Create([DataSourceRequest] DataSourceRequest request, License license)
        {
            return Json(new[] { license }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public JsonResult Update([DataSourceRequest] DataSourceRequest request, License license)
        {
            return Json(new[] { license }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult GetCustomers()
        {
            return Json(customers);
        }

        public JsonResult GetVendors(int customerId)
        {
            return Json(vendors.Where(f => f.CustomerId == customerId));
        }

        public JsonResult GetItems(int vendorId)
        {
            return Json(items.Where(f => f.VendorId == vendorId));
        }
    }
}
