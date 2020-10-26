using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages
{
    public class AutoCompleteCRUDOperationsModel : PageModel
    {
        public static List<OrderViewModel> orders;

        public void OnGet()
        {
            if (orders == null)
            {
                orders = new List<OrderViewModel>();

                Enumerable.Range(1, 50).ToList().ForEach(i => orders.Add(new OrderViewModel
                {
                    ShipName = "ship name " + i
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