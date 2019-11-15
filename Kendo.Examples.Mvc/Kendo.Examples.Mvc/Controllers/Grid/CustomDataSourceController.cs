using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Examples.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kendo.Examples.Mvc.Controllers.Grid
{
    public class CustomDataSourceController : Controller
    {
        private static ICollection<Product> products;
        public CustomDataSourceController()
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

        public IActionResult CustomDataSource()
        {
            return View();
        }
        public ActionResult Read()
        {
            return Json(new MyResponseModel { DataCollection = products, TotalRecords = products.Count });
        }
    }
}