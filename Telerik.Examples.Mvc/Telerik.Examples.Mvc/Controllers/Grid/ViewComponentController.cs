using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Telerik.Examples.Mvc.Models;
using ProductModel = Telerik.Examples.Mvc.Models.Product;

namespace Telerik.Examples.Mvc.Controllers.Grid
{
    public class ViewComponentController : Controller
    {
        public IActionResult ViewComponent()
        {
            var random = new Random();

            var data = Enumerable.Range(1, 10)
                .Select(x => new ProductModel
                {
                    Discontinued = x % 2 == 1,
                    ProductID = x,
                    ProductName = "Product " + x,
                    UnitPrice = random.Next(1, 1000),
                    UnitsInStock = random.Next(1, 1000),
                    UnitsOnOrder = random.Next(1, 1000)

                })
                .ToList();

            return View(data);
        }
    }
}
