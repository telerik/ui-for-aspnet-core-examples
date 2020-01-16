using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace Telerik.Examples.Mvc.Controllers.Grid
{
    public class DragAndDropBetweenGridsController : Controller
    {
        public IActionResult DragAndDropBetweenGrids()
        {
            return View();
        }
        public ActionResult Orders_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = Enumerable.Range(0, 50).Select(i => new OrderViewModel
            {
                OrderID = i,
                Freight = i * 10,
                OrderDate = DateTime.Now.AddDays(i),
                ShipName = "ShipName " + i,
                ShipCity = "ShipCity " + i
            });

            return Json(result.ToDataSourceResult(request));
        }

        public ActionResult Orders_Read2([DataSourceRequest]DataSourceRequest request)
        {
            var result = Enumerable.Range(1, 1).Select(i => new OrderViewModel
            {
                OrderID = i * 100,
                Freight = i * 10,
                OrderDate = DateTime.Now.AddDays(i),
                ShipName = "ShipName " + i * 100,
                ShipCity = "ShipCity " + i * 100
            });

            return Json(result.ToDataSourceResult(request));
        }
    }
}