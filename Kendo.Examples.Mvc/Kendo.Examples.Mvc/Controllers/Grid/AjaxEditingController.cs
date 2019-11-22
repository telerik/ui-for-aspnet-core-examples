using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Examples.Mvc.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace Kendo.Examples.Mvc.Controllers.Grid
{
    public class AjaxEditingController : Controller
    {
        private static ICollection<Product> products;

        public AjaxEditingController()
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
        public IActionResult AjaxEditing()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(products.ToDataSourceResult(request));
        }

        [AcceptVerbs("Post")]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, Product product)
        {
            //save item to database
            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, Product product)
        {
            //save item to database
            products.Add(product);
            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, Product product)
        {
            //remove item from database
            products.Remove(product);
            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }
    }
}