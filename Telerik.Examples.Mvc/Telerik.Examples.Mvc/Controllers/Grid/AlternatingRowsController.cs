using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace Telerik.Examples.Mvc.Controllers.Grid
{
    public class AlternatingRowsController : Controller
    {
        public static ICollection<Student> students = new List<Student>();
        public AlternatingRowsController()
        {
            var names = new List<string> { "John", "Jane", "Jeremy", "James" };
            var lastNames = new List<string> { "Hammont", "Clarkson", "May", "Hennethon" };
            var random = new Random();
            if (students.Count == 0)
            {
                for (int i = 1; i < 50; i++)
                {
                    students.Add(new Student
                    {
                        Age = random.Next(1, 20),
                        Birthday = new DateTime(random.Next(1950, 2018), random.Next(1, 12), random.Next(1, 27)),
                        FirstName = names[random.Next(0, 4)],
                        LastName = lastNames[random.Next(0, 4)],
                        Id = i
                    });
                }
            }
        }

        public IActionResult AlternatingRows()
        {
            return View();
        }

        public ActionResult GetStudents([DataSourceRequest] DataSourceRequest request)
        {

            return Json(students.ToDataSourceResult(request));
        }

        public ActionResult EditStudent(Student student)
        {
            students.Where(x => x.Id == student.Id).Select(x => student);

            return Json(student);
        }

        public ActionResult CreateStudent(Student student)
        {
            student.Id = students.Count + 1;
            students.Add(student);

            return Json(student);
        }

        public ActionResult DestroyStudent(Student student)
        {
            students.Remove(students.First(x => x.Id == student.Id));

            return Json(student);
        }
    }
}