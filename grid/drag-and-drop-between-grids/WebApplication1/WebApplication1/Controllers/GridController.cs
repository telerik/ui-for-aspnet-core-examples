using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class GridController : Controller
    {
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