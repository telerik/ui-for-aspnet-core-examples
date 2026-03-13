using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kendo.FormFilling.ServerProcessing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kendo.FormFilling.ServerProcessing.Controllers
{
    public class GridController : Controller
    {
        public ActionResult Orders_Read([DataSourceRequest] DataSourceRequest request)
        {
            var result = Enumerable.Range(0, 50).Select(i => new OrderViewModel
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

        [AcceptVerbs("Post")]
        public ActionResult Editing_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<OrderViewModel> orders)
        {
            var results = new List<OrderViewModel>();

            if (orders != null && ModelState.IsValid)
            {
                foreach (var order in orders)
                {
                    //orderService.Create(order);
                    results.Add(order);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult Editing_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<OrderViewModel> orders)
        {
            if (orders != null && ModelState.IsValid)
            {
                foreach (var order in orders)
                {
                    //orderService.Update(order);
                }
            }

            return Json(orders.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult Editing_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<OrderViewModel> orders)
        {
            if (orders.Any())
            {
                foreach (var order in orders)
                {
                    //orderService.Destroy(order);
                }
            }

            return Json(orders.ToDataSourceResult(request, ModelState));
        }
    }
}
