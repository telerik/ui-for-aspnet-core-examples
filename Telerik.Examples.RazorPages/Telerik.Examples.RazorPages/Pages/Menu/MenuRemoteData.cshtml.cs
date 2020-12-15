using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Menu
{
    public class MenuRemoteDataModel : PageModel
    {
        public static List<Category> categories;
        public void OnGet()
        {
            if (categories == null)
            {
                categories = new List<Category>()
                {
                    new Category()
                    {
                        CategoryID = 1,
                        CategoryName = "Clothes",
                        Products = new List<Product>()
                        {
                            new Product(){ ProductID = 10, ProductName = "Dresses", CategoryID = 1},
                            new Product(){ ProductID = 11, ProductName = "T-shirts", CategoryID = 1}
                        }
                    },
                    new Category()
                    {
                        CategoryID = 2,
                        CategoryName = "Furniture",
                        Products = new List<Product>()
                        {
                            new Product(){ ProductID = 12, ProductName = "Sofas", CategoryID = 2},
                            new Product(){ ProductID = 13, ProductName = "Accessories", CategoryID = 2},
                            new Product(){ ProductID = 14, ProductName = "Tables", CategoryID = 2}
                        }
                    }
                };
            }
        }

        public JsonResult OnGetRead()
        {
            var result = categories.ToList().Select((category) =>
                new
                {
                    Name = category.CategoryName,
                    Products = category.Products
                        .Where((product) => product.CategoryID == category.CategoryID)
                        .Select((product) => new { Name = product.ProductName })
                }
            );
            return new JsonResult(result);
        }
    }
}