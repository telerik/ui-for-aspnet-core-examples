using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using ListBoxAsEditor.Models;
using Kendo.Mvc.Extensions;

namespace ListBoxAsEditor.Controllers
{
    public class GridController : Controller
    {
        public static IEnumerable<Location> locations = Enumerable.Range(11, 16).Select(i => new Location
        {
            Name = "Name " + i,
            Id = i
        });

        public static List<OrderViewModel> dbOrders = Enumerable.Range(1, 10).Select(i => new OrderViewModel
        {
            OrderID = i,
            Freight = i * 10,
            OrderDate = new DateTime(2016, 9, 15).AddDays(i % 7),
            ShipName = "ShipName " + i,
            ShipCity = "ShipCity " + i,
            Locations = new List<Location>() {
                    new Location()
                     {
                         Id = i,
                        Name = "Name " + i
                    }
            }
        }).ToList();

        public ActionResult Orders_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(dbOrders.ToDataSourceResult(request));
        }

        public ActionResult Orders_Update([DataSourceRequest]DataSourceRequest request, OrderViewModel order, IEnumerable<Location> availableLocations)
        {
            // save in database
            for (int i = 0; i < dbOrders.Count; i++)
            {
                if (dbOrders[i].OrderID == order.OrderID) {
                    dbOrders[i] = order;
                }
            }

            // update remaining locations
            locations = availableLocations;

            return Json(new[] { order }.ToDataSourceResult(request));
        }

        public ActionResult GetLocations([DataSourceRequest]DataSourceRequest request)
        {
            // save in database
            return Json(locations.ToDataSourceResult(request));
        }


    }
}
