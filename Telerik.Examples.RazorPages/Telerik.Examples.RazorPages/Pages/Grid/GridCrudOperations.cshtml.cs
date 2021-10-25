using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.RazorPages.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Telerik.Examples.RazorPages.Pages.Grid
{
    public class GridCrudOperationsModel : PageModel
    {
        public static IList<OrderViewModel> orders;

        public void OnGet()
        {
            if (orders == null)
            {
                orders = new List<OrderViewModel>();

                Enumerable.Range(1, 50).ToList().ForEach(i => orders.Add(new OrderViewModel
                {
                    OrderID = i,
                    OrderDate = DateTime.Now.AddDays(i % 7),
                    Freight = i * 10,
                    ShipName = "ShipName " + i,
                    ShipCity = "ShipCity " + i
                }));

            }
        }

        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(orders.ToDataSourceResult(request));
        }

        public JsonResult OnPostCreate([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
        {
            order.OrderID = orders.Count + 2;
            orders.Add(order);

            return new JsonResult(new[] { order }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostUpdate([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
        {
            orders.Where(x => x.OrderID == order.OrderID).Select(x => order);

            return new JsonResult(new[] { order }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDestroy([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
        {
            orders.Remove(orders.FirstOrDefault(x => x.OrderID == order.OrderID));

            return new JsonResult(new[] { order }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult OnPostSave(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    }
}
