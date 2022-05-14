using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Telerik.Examples.Mvc.Controllers.Menu
{
    public class SecurityTrimmingController : Controller
    {
        public IActionResult SecurityTrimming()
        {
            return View("~/Views/Menu/SecurityTrimming.cshtml");
        }

        [Authorize]
        public IActionResult Security_Information()
        {
            return View("~/Views/Menu/Security_Information.cshtml");
        }
        
    }
}
