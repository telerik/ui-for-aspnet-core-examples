using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.Mvc.Controllers.Scheduler
{
    public class SchedulerSignalRController : Controller
    {
        public IActionResult SchedulerSignalR()
        {
            return View();
        }
    }
}
