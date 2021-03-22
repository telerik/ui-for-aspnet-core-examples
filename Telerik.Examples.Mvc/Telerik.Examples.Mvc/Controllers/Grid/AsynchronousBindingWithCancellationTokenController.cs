using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Telerik.Examples.Mvc.Controllers.Grid
{
    public class AsynchronousBindingWithCancellationTokenController : Controller
    {
        public AsynchronousBindingWithCancellationTokenController()
        {
        }

        public IActionResult AsynchronousBindingWithCancellationToken()
        {
            return View();
        }

        public ActionResult Orders_Read([DataSourceRequest] DataSourceRequest request)
        {
            CancellationTokenSource source = new CancellationTokenSource(2000);
            CancellationToken token = source.Token;

            var result = Enumerable.Range(0, 50).Select(i => new OrderViewModel
            {
                OrderID = i,
                Freight = i * 10,
                OrderDate = new DateTime(2016, 9, 15).AddDays(i % 7),
                ShipName = "ShipName " + i,
                ShipCity = "ShipCity " + i
            });

            // TO BE RELEASED
            //var dsResult = result.ToDataSourceResult(request, token);

            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }
    }
}