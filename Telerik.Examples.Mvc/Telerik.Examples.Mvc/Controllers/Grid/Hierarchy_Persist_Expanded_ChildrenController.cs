using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Controllers.Grid
{
    public class Hierarchy_Persist_Expanded_ChildrenController : Controller
    {
        private static ICollection<Product> products;
        private static ICollection<Person> people;
        public IActionResult Hierarchy_Persist_Expanded_Children()
        {
            if (products == null)
            {
                var random = new Random();
                products = Enumerable.Range(1, 4).Select(i => new Product
                {
                    Discontinued = i % 2 == 1,
                    ProductID = i,
                    ProductName = "Product" + i,
                    UnitPrice = random.Next(1, 1000),
                    UnitsInStock = random.Next(1, 1000),
                    UnitsOnOrder = random.Next(1, 1000)

                }).ToList();
            }
            if (people == null)
            {
                people = Enumerable.Range(1, 10).Select(i => new Person
                {
                    PersonID = i,
                    BirthDate = DateTime.Now.AddDays(i),
                    ProductID = i % 2 == 0 ? 1 : 2,
                    Name = "Name" + i
                }).ToList();
            }
            return View();
        }
        public ActionResult Read_Products([DataSourceRequest] DataSourceRequest request)
        {
            return Json(products.ToDataSourceResult(request));
        }

        [AcceptVerbs("Post")]
        public ActionResult Create_Products([DataSourceRequest] DataSourceRequest request, Product product)
        {
            //save item to database
            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult Update_Products([DataSourceRequest] DataSourceRequest request, Product product)
        {
            //save item to database
            products.Add(product);
            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Read_People([DataSourceRequest] DataSourceRequest request, int productID)
        {
            return Json(people
                .Where(person => person.ProductID == productID)
                .ToDataSourceResult(request));
        }

        [AcceptVerbs("Post")]
        public ActionResult Create_People([DataSourceRequest] DataSourceRequest request, Person person)
        {
            //save child item to database
            return Json(new[] { person }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult Update_People([DataSourceRequest] DataSourceRequest request, Person person)
        {
            //save child item to database
            people.Add(person);
            return Json(new[] { person }.ToDataSourceResult(request, ModelState));
        }
    }
}
