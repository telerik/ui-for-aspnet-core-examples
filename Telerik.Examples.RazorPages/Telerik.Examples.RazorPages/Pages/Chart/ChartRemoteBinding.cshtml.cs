using System;
using System.Collections.Generic;
using Telerik.Examples.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace Telerik.Examples.RazorPages.Pages.Chart
{
    public class ChartRemoteBindingModel : PageModel
    {

        public static List<ElectricityProduction> production;
        public void OnGet()
        {
            if (production == null)
            {
                production = new List<ElectricityProduction>();

                production.Add(new ElectricityProduction { Year = "2000", Solar = 18, Nuclear = 31807, Hydro = 4727, Wind = 62206 });
                production.Add(new ElectricityProduction { Year = "2001", Solar = 24, Nuclear = 43864, Hydro = 6759, Wind = 63708 });
                production.Add(new ElectricityProduction { Year = "2002", Solar = 30, Nuclear = 26270, Hydro = 9342, Wind = 63016 });
                production.Add(new ElectricityProduction { Year = "2003", Solar = 41, Nuclear = 43897, Hydro = 12075, Wind = 61875 });
                production.Add(new ElectricityProduction { Year = "2004", Solar = 56, Nuclear = 34439, Hydro = 15700, Wind = 63606 });
                production.Add(new ElectricityProduction { Year = "2005", Solar = 41, Nuclear = 23025, Hydro = 21176, Wind = 57539 });
                production.Add(new ElectricityProduction { Year = "2006", Solar = 119, Nuclear = 29831, Hydro = 23297, Wind = 60126 });
                production.Add(new ElectricityProduction { Year = "2007", Solar = 508, Nuclear = 30522, Hydro = 27568, Wind = 55103 });
                production.Add(new ElectricityProduction { Year = "2008", Solar = 2578, Nuclear = 26112, Hydro = 32203, Wind = 58973 });

            }

        }
        public JsonResult OnPostRead()
        {
            return new JsonResult(production);
        }

    }
}
