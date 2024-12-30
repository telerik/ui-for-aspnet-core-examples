using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Telerik.Examples.Mvc.Database;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Seeders
{
    public class DataSeeder
    {
        public static void SeedListBoxItems(InMemoryDbContext dbContext)
        {
            if (dbContext.Employees.Any())
            {
                return;
            }

            var employees = new List<EmployeeViewModel>
            {
                new EmployeeViewModel(){ Id = 1, Name = "Steven White" },
                new EmployeeViewModel(){ Id = 2, Name = "Nancy King" },
                new EmployeeViewModel(){ Id = 3, Name = "Nancy Davolio" },
                new EmployeeViewModel(){ Id = 4, Name = "Michael Leverling" },
                new EmployeeViewModel(){ Id = 5, Name = "Andrew Callahan" },
                new EmployeeViewModel(){ Id = 6, Name = "Michael Suyama" },
            };

            dbContext.Employees.AddRange(employees);
            dbContext.SaveChanges();
        }
    }
}
