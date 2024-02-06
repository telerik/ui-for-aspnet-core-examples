using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace Telerik.Examples.Mvc.Controllers.MultiSelect
{
	public class Cascading_MultiSelectController : Controller
	{
		public IActionResult Cascading_MultiSelect()
		{
			return View();
		}
		public IActionResult ReadParent()
		{
			var results = Enumerable.Range(1, 10).Select(i => new SelectListItem
			{
				Text = "Super cool item " + i,
				Value = i.ToString()
			});
			return Json(results);
		}

		public IActionResult ReadChild()
		{
			var selectionList = this.Request.Form.ToList().Select(item => (Int32.Parse(item.Value)));
			var listMax = selectionList.Max(value => value);
			var results = Enumerable.Range(1, listMax).Select(i => new SelectListItem
			{
				Text = "Nice item " + i,
				Value = i.ToString()
			});
			return Json(results);
		}
	}
}
