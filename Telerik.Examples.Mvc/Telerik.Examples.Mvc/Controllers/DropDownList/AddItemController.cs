using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Controllers.DropDownList
{
    public class AddItemController : Controller
    {
        public IActionResult AddItem()
        {
            return View("~/Views/DropDownList/AddItem.cshtml");
        }
        public IActionResult GetLocations()
        {
            IEnumerable<Location> locations = LocationsData();

            return Json(locations);
        }
        public IActionResult CreateLocation(Location location)
        {
            return Json(new[] { location });
        }
        private List<Location> LocationsData()
        {
            return new List<Location>()
            {
                new Location() { Id = 1, Name = "London" },
                new Location() { Id = 2, Name = "Paris" },
                new Location() { Id = 3, Name = "Sofia" }
            };
        }
    }
}
