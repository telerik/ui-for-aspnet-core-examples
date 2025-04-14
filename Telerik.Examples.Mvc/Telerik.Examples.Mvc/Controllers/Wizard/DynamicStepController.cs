using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Controllers.Wizard
{
    public class DynamicStepController : Controller
    {
        public IActionResult DynamicStep()
        {
            return View("/Views/Wizard/DynamicStep.cshtml");
        }

        [HttpPost]
        public IActionResult DynamicStep(UserDetailsModel model)
        {
            return Json(model);
        }
        public IActionResult Load_Step(string type)
        {
            return PartialView("_DynamicStepPartial", type);
        }
    }
}
