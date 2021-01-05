using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Telerik.Examples.RazorPages.Pages.Rating
{
    public class RatingIndexModel : PageModel
    {
        public string Precision { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        public double Value { get; set; }
        public void OnGet()
        {
            Min = 1;
            Max = 10;
            Precision = "half";
            Value = 7.5;
        }
    }
}
