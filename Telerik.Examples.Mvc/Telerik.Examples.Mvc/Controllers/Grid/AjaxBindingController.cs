using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace Telerik.Examples.Mvc.Controllers.Grid
{
    public class AjaxBindingController : Controller
    {
        private static ICollection<Product> products;
        public IActionResult AjaxBinding()
        {
            return View();
        }
        public AjaxBindingController()
        {
            if (products == null)
            {
                var random = new Random();
                products = Enumerable.Range(1, 100).Select(x => new Product
                {
                    Discontinued = x % 2 == 1,
                    ProductID = x,
                    ProductName = "Product " + x,
                    UnitPrice = random.Next(1, 1000),
                    UnitsInStock = random.Next(1, 1000),
                    UnitsOnOrder = random.Next(1, 1000)

                }).ToList();
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(products.ToDataSourceResult(request));
        }
    }
}