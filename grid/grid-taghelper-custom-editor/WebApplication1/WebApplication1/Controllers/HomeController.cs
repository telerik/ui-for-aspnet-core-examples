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
        public static ICollection<Student> students = new List<Student>();


        public HomeController()
        {
            var names = new List<string> { "John", "Jane", "Jeremy", "James" };
            var lastNames = new List<string> { "Hammont", "Clarkson", "May", "Hennethon" };
            var random = new Random();
            if (students.Count == 0)
            {
                for (int i = 1; i < 20; i++)
                {
                    students.Add(new Student
                    {
                        EnrollmentDate = new DateTime(random.Next(1950, 2018), random.Next(1, 12), random.Next(1, 27)),
                        FirstMidName = names[random.Next(0, 4)],
                        LastName = lastNames[random.Next(0, 4)],
                        ID = i,
                        Status = new Status { Id = i % 2 + 1, Text = i % 2 == 0 ? "Active" : "Inactive" }
                    });
                }
            }
        }
        public IActionResult Index()
        {
            return View();
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

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(students.ToDataSourceResult(request));
        }

        public ActionResult GetNames([DataSourceRequest] DataSourceRequest request)
        {
            var result = new HashSet<string>();

            students.Each(x => {

                if (x.FirstMidName != null)
                {
                    result.Add(x.FirstMidName);
                }
            });

            return Json(result);

        }



        [AcceptVerbs("Post")]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, Student student)
        {
            if (student != null && ModelState.IsValid)
            {
                students.Add(student);
            }

            return Json(new[] { student }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, Student student)
        {
            if (student != null && ModelState.IsValid)
            {
                students.Add(student);
            }

            return Json(new[] { student }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, Student student)
        {
            if (student != null)
            {
                students.Remove(student);
            }

            return Json(new[] { student }.ToDataSourceResult(request, ModelState));
        }




        public IActionResult Privacy()
        {
            return View();
        }

    }
}
