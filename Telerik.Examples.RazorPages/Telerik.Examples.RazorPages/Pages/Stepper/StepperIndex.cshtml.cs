using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Telerik.Examples.RazorPages.Pages.Stepper
{
    public class StepperIndexModel : PageModel
    {        
        public bool Label { get; set; }
        public bool Indicator { get; set; }

        public double Value { get; set; }
        public void OnGet()
        {            
            Label = true;
            Indicator = false;
        }
    }
}
