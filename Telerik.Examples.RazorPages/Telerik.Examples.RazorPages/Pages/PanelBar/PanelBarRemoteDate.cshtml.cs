using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.PanelBar
{
    public class PanelBarRemoteDateModel : PageModel
    {
        public static IList<Employee> employees;
        public void OnGet()
        {
            if (employees == null)
            {
                employees = new List<Employee>()
                {
                     new Employee(){ EmployeeID = 1, FirstName = "Peter", LastName = "Harper", ReportsTo = null },
                    new Employee(){ EmployeeID = 2, FirstName = "Anton", LastName = "Jones", ReportsTo = null },
                    new Employee(){ EmployeeID = 3, FirstName = "Martin", LastName = "Louis", ReportsTo = 1 },
                    new Employee(){ EmployeeID = 4, FirstName = "Nicola", LastName = "Brighton", ReportsTo = 1 },
                    new Employee(){ EmployeeID = 5, FirstName = "Amanda", LastName = "Littrel", ReportsTo = 2 },
                    new Employee(){ EmployeeID = 6, FirstName = "Kevin", LastName = "Richardson", ReportsTo = 2 },
                    new Employee(){ EmployeeID = 7, FirstName = "Haward", LastName = "Jackson", ReportsTo = 2 },
                    new Employee(){ EmployeeID = 8, FirstName = "Sharon", LastName = "Andrews", ReportsTo = 5 },
                    new Employee(){ EmployeeID = 9, FirstName = "Mikaela", LastName = "Roberts", ReportsTo = 5 }
                };
            }
            
        }        

        public JsonResult OnGetRead(int? id)
        {       
            var result = employees.ToList()
                .Where(v => id.HasValue ? v.ReportsTo == id : v.ReportsTo == null)
                .Select((employee) =>
                    new
                    {
                        id = employee.EmployeeID,
                        Name = employee.FirstName + " " + employee.LastName,
                        hasChildren = employees.Any(o => o.ReportsTo == employee.EmployeeID)
                    }
            );
            return new JsonResult(result);
        }
    }
}