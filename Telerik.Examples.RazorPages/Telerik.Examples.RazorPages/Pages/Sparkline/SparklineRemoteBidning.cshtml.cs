using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Sparkline
{
    public class SparklineRemoteBidningModel : PageModel
    {

        public static List<Weather> items;
        public void OnGet()
        {
            var random = new Random();
            if (items == null)
            {
                items = new List<Weather>();
                Enumerable.Range(0, 30).ToList().ForEach(i => items.Add(new Weather
                {
                    Id = i,
                    Rain = random.Next(0, 10),
                    TMax = random.Next(2, 11),
                    Wind = random.Next(8, 30)
                }));

            }

        }

        public JsonResult OnPostRead()
        {
            return new JsonResult(items);
        }

    }
}