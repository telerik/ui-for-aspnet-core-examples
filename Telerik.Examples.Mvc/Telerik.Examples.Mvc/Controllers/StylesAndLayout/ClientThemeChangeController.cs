using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Controllers.StylesAndLayout
{
    public class ClientThemeChangeController : Controller
    {
        public IActionResult ClientThemeChange()
        {
            return View();
        }
        public ActionResult Orders_Read([DataSourceRequest] DataSourceRequest request)
        {
            var result = Enumerable.Range(0, 50).Select(i => new OrderViewModel
            {
                OrderID = i,
                Freight = i * 10,
                OrderDate = new DateTime(2016, 9, 15).AddDays(i % 7),
                ShipName = "ShipName " + i,
                ShipCity = "ShipCity " + i
            });

            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }
        public IActionResult GetThemes()
        {
            var themes = new List<Theme>()
            {
                new Theme() { ThemeId = "default-v2", Name = "Default-v2" },
                new Theme() { ThemeId = "bootstrap-v4", Name = "Bootstrap v4" },
                new Theme() { ThemeId = "material-v2", Name = "Material-v2" },
                new Theme() { ThemeId = "default", Name = "Default" },
                new Theme() { ThemeId = "material", Name = "Material" },
                new Theme() { ThemeId = "moonlight", Name = "Moonlight" },
                new Theme() { ThemeId = "uniform", Name = "Uniform" },
                new Theme() { ThemeId = "bootstrap", Name = "Bootstrap v3" },
                new Theme() { ThemeId = "highcontrast", Name = "High Contrast" },
                new Theme() { ThemeId = "metroblack", Name = "Metro Black" },
                new Theme() { ThemeId = "silver", Name = "Sliver" },
                new Theme() { ThemeId = "blueopal", Name = "Blue Opal" },
                new Theme() { ThemeId = "flat", Name = "Flat" },
                new Theme() { ThemeId = "metro", Name = "Metro" },
                new Theme() { ThemeId = "office365", Name = "Office 365" },
                new Theme() { ThemeId = "black", Name = "Black" },
                new Theme() { ThemeId = "fiori", Name = "Fiori" },
                new Theme() { ThemeId = "materialblack", Name = "Material black" },
                new Theme() { ThemeId = "nova", Name = "Nova" },
            };
            return Json(themes);
        }
        [HttpPost]
        public IActionResult SetTheme(string selection)
        {
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddMinutes(10);

            Response.Cookies.Append("theme", selection, cookie);
            var returnUrl = Request.Headers["Referer"].ToString();
            return Json(new { result = "Redirect", url = returnUrl });
        }
    }
}
