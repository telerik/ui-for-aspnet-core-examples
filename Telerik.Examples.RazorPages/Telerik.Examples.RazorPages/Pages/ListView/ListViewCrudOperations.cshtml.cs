using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.ListView
{
    public class ListViewCrudOperationsModel : PageModel
    {
        public static IList<Product> products;

        public void OnGet()
        {
            if (products == null)
            {
                products = new List<Product>();

                Enumerable.Range(1, 50).ToList().ForEach(i => products.Add(new Product
                {
                    ProductID = i,
                    ProductName = "Product Name " + i,
                    UnitPrice = i * 10,
                    UnitsInStock = i * 3,
                    Discontinued = i % 2 == 0
                }));
            }
        }

        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(products.ToDataSourceResult(request));
        }

        public JsonResult OnPostCreate([DataSourceRequest] DataSourceRequest request, Product product)
        {
            product.ProductID = products.Count + 2;
            products.Add(product);

            return new JsonResult(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostUpdate([DataSourceRequest] DataSourceRequest request, Product product)
        {
            products.Where(x => x.ProductID == product.ProductID).Select(x => product);

            return new JsonResult(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDestroy([DataSourceRequest] DataSourceRequest request, Product product)
        {
            products.Remove(products.FirstOrDefault(x => x.ProductID == product.ProductID));

            return new JsonResult(new[] { product }.ToDataSourceResult(request, ModelState));
        }
    }
}
