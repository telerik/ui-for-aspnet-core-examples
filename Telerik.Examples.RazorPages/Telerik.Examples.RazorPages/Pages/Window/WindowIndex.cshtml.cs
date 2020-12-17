using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Window
{
    public class WindowIndexModel : PageModel
    {
        public string Text = String.Empty;
        public void OnGet()
        {
            Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi facilisis, tortor in imperdiet iaculis, lacus sem placerat mi, eu pellentesque lectus nisl a tortor. Praesent efficitur ipsum quis neque vestibulum, id condimentum nisl bibendum. Nulla dictum efficitur eros id volutpat. Nam tellus sem, condimentum at lacinia eu, porta a mi. Fusce varius suscipit ullamcorper. Sed ac luctus tellus. Aenean efficitur purus pellentesque, accumsan mi ut, porttitor purus. Nulla efficitur, lacus quis pellentesque pellentesque, neque enim iaculis ligula, vel volutpat mi odio id nibh. Suspendisse laoreet commodo magna vitae tempus. Curabitur vitae massa tortor. In nisi turpis, tristique vitae volutpat in, iaculis non lacus. Morbi quis augue risus. Mauris porttitor, sem ut cursus pretium, diam turpis vulputate eros, vitae ullamcorper arcu lacus vel libero.";
        }
    }
}