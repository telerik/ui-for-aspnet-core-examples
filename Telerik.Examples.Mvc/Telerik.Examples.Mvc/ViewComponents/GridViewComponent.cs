using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.ViewComponents
{
    public class GridViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<Product> gridModel)
        {
            return View("default", gridModel);
        }
    }
}
