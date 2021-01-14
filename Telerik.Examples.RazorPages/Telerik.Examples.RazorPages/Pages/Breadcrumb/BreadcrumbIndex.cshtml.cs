using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Telerik.Examples.RazorPages.Pages.Breadcrumb
{
    public class BreadcrumbIndexModel : PageModel
    {
        public List<BreadcrumbItem> Items { get; set; }
        
        public void OnGet()
        {
            Items = new List<BreadcrumbItem>()
            {
                new BreadcrumbItem(){ Text = "All components", Href = "https://demos.telerik.com/aspnet-core/", Icon = "home", IsRoot = true},
                new BreadcrumbItem(){ Text = "Breadcrumb", Href = "/breadcrumb", Icon= "globe", IsRoot = false },
                new BreadcrumbItem(){ Text = "Icons", Href = "/icons", Icon="globe", IsRoot = false},
            };
        }

        public class BreadcrumbItem
        {
            public string Href { get; set; }
            public string Text { get; set; }
            public string Icon { get; set; }
            public bool IsRoot { get; set; }
        }
    }
}
