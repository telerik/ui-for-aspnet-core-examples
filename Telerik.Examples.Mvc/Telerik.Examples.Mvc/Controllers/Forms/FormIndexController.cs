using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using ProductModel = Telerik.Examples.Mvc.Models.Product;

namespace Telerik.Examples.Mvc.Controllers.FormIndex
{
    public class FormIndexController : Controller
    {
        public IActionResult FormIndex()
        {
            return View(new ProductModel { Discontinued = true, ProductID = 1, ProductName = "Chai", UnitPrice = 18, UnitsInStock = 39 });
        }
        public IActionResult Data(ProductModel product)
        {
            return Json(product);
        }
    }
}