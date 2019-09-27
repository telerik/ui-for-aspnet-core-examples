using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public static List<Person> persons = new List<Person>();

        static HomeController()
        {
            persons.Add(new Person { PersonID = 1, Name = "John", BirthDate = new DateTime(1968, 6, 26) });
            persons.Add(new Person { PersonID = 2, Name = "Sara", BirthDate = new DateTime(1974, 9, 13) });
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult GetPersons([DataSourceRequest] DataSourceRequest dsRequest)
        {
            var result = persons.ToDataSourceResult(dsRequest);
            return Json(result);
        }

        public ActionResult UpdatePerson([DataSourceRequest] DataSourceRequest dsRequest, Person person)
        {

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
