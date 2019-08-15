using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public static IList<Product> Products
        {
            get;
            set;
        }

        static HomeController()
        {
            var random = new Random();

            Products = Enumerable.Range(0, 20).Select(index => new Product
            {
                ProductID = index + 1,
                ProductName = "Product " + index,
                UnitsInStock = random.Next(20, 200),
                Available = index % 3 == 0 ? (bool?)null : index % 2 == 0
            }).ToList();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public IActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(Products.ToDataSourceResult(request));
        }

        public IActionResult Update([DataSourceRequest]DataSourceRequest request, Product product)
        {
            if (ModelState.IsValid)
            {
                var dataItem = Products.Where(p => p.ProductID == product.ProductID).FirstOrDefault();

                dataItem.ProductID = product.ProductID;
                dataItem.ProductName = product.ProductName;
                dataItem.UnitsInStock = product.UnitsInStock;
                dataItem.Available = product.Available;
            }

            // Return the updated entities as well as any validation errors.
            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
