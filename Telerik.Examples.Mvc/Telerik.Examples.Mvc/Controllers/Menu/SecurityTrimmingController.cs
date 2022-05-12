using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Telerik.Examples.Mvc.Controllers.Menu
{
    public class SecurityTrimmingController : Controller
    {
        [AllowAnonymous]
        public IActionResult SecurityTrimming()
        {
            return View("~/Views/Menu/SecurityTrimming.cshtml");
        }
    }
}
