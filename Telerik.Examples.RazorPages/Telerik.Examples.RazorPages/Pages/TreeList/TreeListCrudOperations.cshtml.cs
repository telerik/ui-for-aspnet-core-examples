using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.TreeList
{
    public class TreeListCrudOperationsModel : PageModel
    {
        private static IList<EmployeeDirectoryModel> employees;

        public void OnGet()
        {
            GetDirectory();
        }

        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            var result = employees.ToTreeDataSourceResult(request,
                e => e.EmployeeId,
                e => e.ReportsTo,
                e => e
            );

            return new JsonResult(result);
        }

        public JsonResult OnPostCreate([DataSourceRequest] DataSourceRequest request, EmployeeDirectoryModel employee)
        {
            employee.EmployeeId = employees.Count + 2;
            employees.Add(employee);

            return new JsonResult(new[] { employee }.ToTreeDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostUpdate([DataSourceRequest] DataSourceRequest request, EmployeeDirectoryModel employee)
        {
            var target = employees.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId);

            if(target != null)
            {
                target.FirstName = employee.FirstName;
                target.LastName = employee.LastName;
                target.HireDate = employee.HireDate;
                target.Position = employee.Position;
                target.ReportsTo = employee.ReportsTo;
            }

            return new JsonResult(new[] { employee }.ToTreeDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDestroy([DataSourceRequest] DataSourceRequest request, EmployeeDirectoryModel employee)
        {
            employees.Remove(employees.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId));

            return new JsonResult(new[] { employee }.ToTreeDataSourceResult(request, ModelState));
        }

        private IList<EmployeeDirectoryModel> GetDirectory()
        {
            if (employees == null)
            {
                employees = new List<EmployeeDirectoryModel>();
                employees.Add(new EmployeeDirectoryModel
                {
                    EmployeeId = 1,
                    FirstName = "Daryl",
                    LastName = "Sweeney",
                    ReportsTo = null,
                    HireDate = DateTime.Now.AddDays(1),
                    Position = "CEO",
                });

                employees.Add(new EmployeeDirectoryModel
                {
                    EmployeeId = 2,
                    FirstName = "Guy",
                    LastName = "Wooten",
                    ReportsTo = 1,
                    HireDate = DateTime.Now.AddDays(2),
                    Position = "Chief Technical Officer"
                });

                employees.Add(new EmployeeDirectoryModel
                {
                    EmployeeId = 3,
                    FirstName = "Buffy",
                    LastName = "Weber",
                    ReportsTo = 2,
                    HireDate = DateTime.Now.AddDays(3),
                    Position = "VP, Engineering"
                });

                employees.Add(new EmployeeDirectoryModel
                {
                    EmployeeId = 4,
                    FirstName = "Hyacinth",
                    LastName = "Hood",
                    ReportsTo = 3,
                    HireDate = DateTime.Now.AddDays(4),
                    Position = "Team Lead"
                });

                employees.Add(new EmployeeDirectoryModel
                {
                    EmployeeId = 5,
                    FirstName = "Akeem",
                    LastName = "Carr",
                    ReportsTo = 4,
                    HireDate = DateTime.Now.AddDays(5),
                    Position = "Junior Software Developer"
                });

                employees.Add(new EmployeeDirectoryModel
                {
                    EmployeeId = 6,
                    FirstName = "Rinah",
                    LastName = "Simon",
                    ReportsTo = 4,
                    HireDate = DateTime.Now.AddDays(6),
                    Position = "Software Developer"
                });

                employees.Add(new EmployeeDirectoryModel
                {
                    EmployeeId = 7,
                    FirstName = "Gage",
                    LastName = "Daniels",
                    ReportsTo = 3,
                    HireDate = DateTime.Now.AddDays(7),
                    Position = "Software Architect",
                });

                employees.Add(new EmployeeDirectoryModel
                {
                    EmployeeId = 8,
                    FirstName = "Constance",
                    LastName = "Vazquez",
                    ReportsTo = 3,
                    HireDate = DateTime.Now.AddDays(8),
                    Position = "Director, Engineering",
                });

                employees.Add(new EmployeeDirectoryModel
                {
                    EmployeeId = 9,
                    FirstName = "Darrel",
                    LastName = "Solis",
                    ReportsTo = 8,
                    HireDate = DateTime.Now.AddDays(9),
                    Position = "Team Lead",
                });

                employees.Add(new EmployeeDirectoryModel
                {
                    EmployeeId = 10,
                    FirstName = "Brian",
                    LastName = "Yang",
                    ReportsTo = 9,
                    HireDate = DateTime.Now.AddDays(10),
                    Position = "Senior Software Developer",
                });

                employees.Add(new EmployeeDirectoryModel
                {
                    EmployeeId = 11,
                    FirstName = "Lillian",
                    LastName = "Bradshaw",
                    ReportsTo = 9,
                    HireDate = DateTime.Now.AddDays(11),
                    Position = "Software Developer",
                });
            }

            return employees;
        }
    }
}
