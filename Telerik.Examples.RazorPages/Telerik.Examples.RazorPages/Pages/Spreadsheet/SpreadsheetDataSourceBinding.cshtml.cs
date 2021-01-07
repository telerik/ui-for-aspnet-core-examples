using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Spreadsheet
{
    public class SpreadsheetDataSourceBindingModel : PageModel
    {
		public static IList<Product> SpreadData;

		public void OnGet()
		{
            if (SpreadData == null)
            {
				SpreadData = Enumerable.Range(1, 50).Select(x => new Product() { 
					ProductID = x,
                    CategoryID = x % 7,
                    ProductName = "Product " + x
				}).ToList();
            }
		}

        public JsonResult OnGetData_Source_Products_Read([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(SpreadData.ToDataSourceResult(request));
        }

        public ActionResult OnPostData_Source_Products_Submit(SpreadsheetSubmitViewModel model)
        {
            var result = new SpreadsheetSubmitViewModel()
            {
                Created = new List<Product>(),
                Updated = new List<Product>(),
                Destroyed = new List<Product>()
            };

            if ((model.Created != null || model.Updated != null || model.Destroyed != null) && ModelState.IsValid)
            {
                if (model.Created != null)
                {
                    foreach (var created in model.Created)
                    {
                        SpreadData.Add(created);
                        result.Created.Add(created);
                    }
                }

                if (model.Updated != null)
                {
                    foreach (var updated in model.Updated)
                    {
                        var target = SpreadData.FirstOrDefault(x=>x.ProductID == updated.ProductID);
                        target = updated;
                        result.Updated.Add(updated);
                    }
                }

                if (model.Destroyed != null)
                {
                    foreach (var destroyed in model.Destroyed)
                    {
                        var target = SpreadData.FirstOrDefault(x => x.ProductID == destroyed.ProductID);
                        SpreadData.Remove(target);
                        result.Destroyed.Add(destroyed);
                    }
                }

                return new JsonResult(result);
            }
            else
            {
                return StatusCode(400, "The models contain invalid property values.");
            }
        }
    }
}
