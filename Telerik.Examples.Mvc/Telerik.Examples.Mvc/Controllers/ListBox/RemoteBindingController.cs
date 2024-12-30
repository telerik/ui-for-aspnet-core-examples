using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Telerik.Examples.Mvc.Database;

namespace Telerik.Examples.Mvc.Controllers.ListBox
{
    public class RemoteBindingController : Controller
    {
        private readonly InMemoryDbContext _dbContext;

        public RemoteBindingController(InMemoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult RemoteBinding()
        {
            return View();
        }

        public IActionResult GetEmployees()
        {
            var employees = _dbContext.Employees.ToList();
            return Json(employees);
        }
    }
}
