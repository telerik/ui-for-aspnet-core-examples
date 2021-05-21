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

        public async Task<ActionResult> EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            CancellationTokenSource source = new CancellationTokenSource(2000);
            CancellationToken token = source.Token;

            var result = await GetOrders().ToDataSourceResultAsync(request, token);
            return Json(result);
        }

        private List<OrderViewModel> GetOrders()
        {
            Thread.Sleep(3000);
            return Enumerable.Range(0, 50).Select(i => new OrderViewModel
            {
                OrderID = i,
                Freight = i * 10,
                OrderDate = DateTime.Now.AddDays(i),
                ShipName = "ShipName " + i,
                ShipCity = "ShipCity " + i
            }).ToList();
        }
    }
}