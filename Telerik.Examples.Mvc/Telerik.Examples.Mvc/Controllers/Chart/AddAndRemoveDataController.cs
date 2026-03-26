using Microsoft.AspNetCore.Mvc;

namespace Telerik.Examples.Mvc.Controllers.Chart
{
    public class AddAndRemoveDataController : Controller
    {
        public IActionResult AddAndRemoveData()
        {
            return View("~/Views/Chart/AddAndRemoveData.cshtml");
        }
    }
}
