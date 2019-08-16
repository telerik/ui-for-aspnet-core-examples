using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using MultiSelectInGrid.Models;
using Kendo.Mvc.Extensions;

namespace MultiSelectInGrid.Controllers
{
    public class GridController : Controller
    {
        public static List<OrderViewModel> dbOrders = Enumerable.Range(1, 10).Select(i => new OrderViewModel
        {
            OrderID = i,
            Freight = i * 10,
            ShipName = "ShipName " + i,
            ShipCity = "ShipCity " + i,
            Employees = new List<EmployeeViewModel>() {
                new EmployeeViewModel(){ Id = i, Name = "Name " + i }
            }
        }).ToList();

        public static List<EmployeeViewModel> dbEmployees = Enumerable.Range(1, 20).Select(i => new EmployeeViewModel
        {
            Id = i,
            Name = "Name " + i
        }).ToList();

        public ActionResult Orders_Read([DataSourceRequest]DataSourceRequest request)
        {
            var dsResult = dbOrders.ToDataSourceResult(request);
            return Json(dsResult);
        }

        public ActionResult Orders_Update([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
        {
            if (order != null && ModelState.IsValid)
            {
                // own create logic or use with sample data to test         
                for (int i = 0; i < dbOrders.Count; i++)
                {
                    if (order.OrderID == dbOrders[i].OrderID)
                    {
                        dbOrders[i] = order;
                    }
                }
            }

            //Return any validation errors, if any.
            return Json(new[] { order }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Orders_Create([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
        {
            if (order != null && ModelState.IsValid)
            {
                // own update logic or use with sample data to test

                var nextId = dbOrders.Count + 1;
                order.OrderID = nextId;
                dbOrders.Add(order);
            }

            //Return any validation errors, if any.
            return Json(new[] { order }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Orders_Destroy([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
        {
            if (order != null)
            {
                // own destroy logic or use with sample data to test

                for (int i = 0; i < dbOrders.Count; i++)
                {
                    if (order.OrderID == dbOrders[i].OrderID)
                    {
                        dbOrders.Remove(dbOrders[i]);
                        break;
                    }
                }
            }

            //Return any validation errors, if any.
            return Json(new[] { order }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult GetEmployees()
        {
            return Json(dbEmployees);
        }
    }
}
