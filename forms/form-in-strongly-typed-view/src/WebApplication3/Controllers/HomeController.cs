using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {           
            return View(new Product { Discontinued = true, ProductID = 1, ProductName = "Chai", UnitPrice = 18, UnitsInStock = 39 });
        }

        public IActionResult Data(Product product)
        {
            return Json(product);
        }

    }
}
