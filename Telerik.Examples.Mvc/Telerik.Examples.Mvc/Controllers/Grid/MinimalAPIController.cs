using Microsoft.AspNetCore.Mvc;

namespace Telerik.Examples.Mvc.Controllers.Grid
{
    public class MinimalAPIController: Controller
    {
        public IActionResult MinimalAPI()
        {
            return View();
        }
    }
}
