using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Controllers.Grid
{
    public class ToggleEditModeController : Controller
    {
        public IActionResult ToggleEditMode()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var result = Enumerable.Range(1, 50).Select(i => new OrderViewModel
            {
                OrderID = i,
                Freight = i * 10,
                OrderDate = new DateTime(2016, 9, 15).AddDays(i % 7),
                ShipName = "ShipName " + i,
                ShipCity = "ShipCity " + i
            });

            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }

        public ActionResult Update([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
        {
            return Json(new[] { order }.ToDataSourceResult(request));
        }

        public ActionResult Create([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
        {
            return Json(new[] { order }.ToDataSourceResult(request));
        }

        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
        {
            return Json(new[] { order }.ToDataSourceResult(request));
        }
    }
}
