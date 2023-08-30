using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Grid
{
    public class GridCascadingForeignKeysModel : PageModel
    {

        public static IList<ShippingModel> shipments;

        public void OnGet()
        {
            if (shipments == null)
            {
                shipments = new List<ShippingModel>();

                Enumerable.Range(1, 10).ToList().ForEach(i => shipments.Add(new ShippingModel
                {
                    OrderID = i + 1,
                    Freight = i * 10,
                    OrderDate = new DateTime(2016, 9, 15).AddDays(i % 7),
                    Fk_StateID = i,
                    Fk_CountryID = i % 2 == 0 ? 1 : 2,
                    ShipName = "ShipName " + i,
                    ShipCity = "ShipCity " + i
                }));

            }
        }

        public JsonResult OnGetCountries()
        {
            var countries = Enumerable.Range(1, 2)
              .Select(i => new CountryModel
              {
                  ID = i.ToString(),
                  Description = "Country" + i
              }).ToList();

            return new JsonResult(countries);
        }
        public JsonResult OnGetStateLookup(int? countryId)
        {
            var states = Enumerable.Range(1, 10)
              .Select(i => new StateModel
              {
                  ID = i.ToString(),
                  Description = "State" + i,
                  CountryID = i % 2 == 0 ? 1 : 2
              }).ToList();

            if (countryId != null)
            {
                states = states
                    .Where(w => w.CountryID == countryId)
                    .ToList();
            }

            return new JsonResult(states);
        }

        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(shipments.ToDataSourceResult(request));
        }

        public JsonResult OnPostCreate([DataSourceRequest] DataSourceRequest request, ShippingModel shipment)
        {
            shipment.OrderID = shipments.Count + 2;
            shipments.Add(shipment);

            return new JsonResult(new[] { shipment }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostUpdate([DataSourceRequest] DataSourceRequest request, ShippingModel shipment)
        {
            shipments.Where(x => x.OrderID == shipment.OrderID).Select(x => shipment);

            return new JsonResult(new[] { shipment }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDestroy([DataSourceRequest] DataSourceRequest request, ShippingModel shipment)
        {
            shipments.Remove(shipments.FirstOrDefault(x => x.OrderID == shipment.OrderID));

            return new JsonResult(new[] { shipment }.ToDataSourceResult(request, ModelState));
        }
    }
}
