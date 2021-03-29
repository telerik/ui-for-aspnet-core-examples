using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Controllers.Grid
{
    public class EncodedForeignKeyValuesController : Controller
    {

        public IActionResult EncodedForeignKeyValues()
        {
            PopulateCities();

            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var result = Enumerable.Range(1, 20).Select(i => new ForeignKeyOrderViewModel
            {
                OrderID = i,
                Freight = i * 10,
                OrderDate = new DateTime(2016, 9, 15).AddDays(i % 7),
                ShipName = "ShipName " + i,
                ShipCityId = i
            });

            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }

        public ActionResult Update([DataSourceRequest] DataSourceRequest request, ForeignKeyOrderViewModel order)
        {
            return Json(new[] { order }.ToDataSourceResult(request));
        }

        public ActionResult Create([DataSourceRequest] DataSourceRequest request, ForeignKeyOrderViewModel order)
        {
            return Json(new[] { order }.ToDataSourceResult(request));
        }

        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, ForeignKeyOrderViewModel order)
        {
            return Json(new[] { order }.ToDataSourceResult(request));
        }

        private void PopulateCities()
        {
            var cities = new List<CityViewModel>();
            for (int i = 1; i <= 20; i++)
            {
                cities.Add(new CityViewModel
                {
                    CityID = i,
                    CityName = @System.Web.HttpUtility.UrlEncode("<script>City</script><bold>№.</bold>") + i
                }); ;
            }

            ViewData["cities"] = cities;
        }
    }
}
