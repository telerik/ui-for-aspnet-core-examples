using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Telerik.Examples.Mvc.Controllers.MultiSelect
{
    public class MultiSelectPostController : Controller
    {
        public IActionResult MultiSelectPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MultiSelectPost(SampleModel userInput)
        {
            return View("MultiSelectDetails", userInput);
        }

        public JsonResult GetData()
        {
            var data = Enumerable.Range(1, 15).Select(d => new MultiSelectItem
            {
                TheText = "item " + d,
                TheValue = d
            });

            return Json(data.ToList());
        }
    }
}