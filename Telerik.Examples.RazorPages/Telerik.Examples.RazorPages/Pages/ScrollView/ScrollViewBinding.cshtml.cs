using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.ScrollView
{
    public class ScrollViewBindingModel : PageModel
    {
        public static List<Models.Product> ScrollViewItems { get; set; }
        public void OnGet()
        {
            if (ScrollViewItems == null)
            {
                ScrollViewItems = new List<Models.Product>();

                ScrollViewItems.Add(new Models.Product { ImageUrl = "image1.jpg", ProductName = "Chai" });
                ScrollViewItems.Add(new Models.Product { ImageUrl = "image2.jpg", ProductName = "Chang" });
                ScrollViewItems.Add(new Models.Product { ImageUrl = "image3.jpg", ProductName = "Aniseed Syrup" });
                ScrollViewItems.Add(new Models.Product { ImageUrl = "image4.jpg", ProductName = "Ikura" });
                ScrollViewItems.Add(new Models.Product { ImageUrl = "image5.jpg", ProductName = "Tofu" });
                ScrollViewItems.Add(new Models.Product { ImageUrl = "image6.jpg", ProductName = "Konbu" });
                ScrollViewItems.Add(new Models.Product { ImageUrl = "image7.jpg", ProductName = "Pavlova" });
                ScrollViewItems.Add(new Models.Product { ImageUrl = "image8.jpg", ProductName = "Cloud" });
                ScrollViewItems.Add(new Models.Product { ImageUrl = "image9.jpg", ProductName = "Sun" });
            }
        }

        public JsonResult OnPostReadOptional([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(ScrollViewItems.ToDataSourceResult(request));
        }
    }
}
