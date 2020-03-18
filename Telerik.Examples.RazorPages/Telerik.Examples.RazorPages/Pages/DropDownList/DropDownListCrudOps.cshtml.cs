using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.RazorPages.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Telerik.Examples.RazorPages.Pages.DropDownList
{
    public class DropDownListCrudOps : PageModel
    {
        public static List<OrderViewModel> orders;

        public void OnGet()
        {
            if (orders == null)
            {
                orders = new List<OrderViewModel>();

                Enumerable.Range(1, 50).ToList().ForEach(i => orders.Add(new OrderViewModel
                {
                    OrderID = i + 1,
                    Freight = i * 10,
                    ShipName = "ship name " + i,
                    ShipCity = "ship city " + i
                }));

            }
        }

        public JsonResult OnGetRead(string filterValue)
        {
            if (filterValue != null)
            {
                var filteredData = orders.Where(p => p.ShipName.Contains(filterValue));
                return new JsonResult(filteredData);
            }
            return new JsonResult(orders);
        }

    }
}