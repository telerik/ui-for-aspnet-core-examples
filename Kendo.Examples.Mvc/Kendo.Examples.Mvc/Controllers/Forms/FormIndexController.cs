using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Examples.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kendo.Examples.Mvc.Controllers.FormIndex
{
    public class FormIndexController : Controller
    {
        public IActionResult FormIndex()
        {
            return View(new Product { Discontinued = true, ProductID = 1, ProductName = "Chai", UnitPrice = 18, UnitsInStock = 39 });
        }
        public IActionResult Data(Product product)
        {
            return Json(product);
        }
    }
}