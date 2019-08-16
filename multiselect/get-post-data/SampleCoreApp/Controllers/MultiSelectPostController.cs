using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleCoreApp.Models;

namespace SampleCoreApp.Controllers
{
	public class MultiSelectPostController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(sampleModel userInput)
		{
			return View("Details", userInput);
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