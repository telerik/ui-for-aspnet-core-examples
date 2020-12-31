using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.TreeMap
{
    public class TreeMapBindingModel : PageModel
    {
        public static IList<PopulationUSA> TreeMapItems;

        public void OnGet()
        {
            TreeMapItems = TreeMapDataRepository.PopulationUSAData();
        }

        public JsonResult OnGetReadOptional()
        {
            return new JsonResult(TreeMapItems);
        }
    }
}
