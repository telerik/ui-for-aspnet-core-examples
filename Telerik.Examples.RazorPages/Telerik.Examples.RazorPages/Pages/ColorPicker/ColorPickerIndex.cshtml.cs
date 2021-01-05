using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Telerik.Examples.RazorPages.Pages.ColorPicker
{
    public class ColorPickerIndexModel : PageModel
    {
        public bool ClearButton { get; set; }
        public bool Buttons { get; set; }

        public string Value { get; set; }
        public void OnGet()
        {
            ClearButton = true;
            Buttons = false;
            Value = "#94ed67";
        }
    }
}
