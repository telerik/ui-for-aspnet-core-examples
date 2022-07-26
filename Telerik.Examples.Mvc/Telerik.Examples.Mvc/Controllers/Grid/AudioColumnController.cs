using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Controllers.Grid
{
    public class AudioColumnController : Controller
    {
        private static ICollection<MusicianViewModel> musicians;

        public AudioColumnController()
        {
            if (musicians == null)
            {
                musicians = new List<MusicianViewModel>()
                {
                    new MusicianViewModel() {ID = 1, Name = "Eminem", Age = 40, Nationality = "American", Intro = new Audio() {FileName="Eminem",Extension="mp3" } },
                    new MusicianViewModel() {ID = 2, Name = "Shakira", Age = 42, Nationality = "Spanish", Intro = new Audio() {FileName="Shakira",Extension="mp3" } },
                    new MusicianViewModel() {ID = 3, Name = "50Cent", Age = 42, Nationality = "American", Intro = new Audio() {FileName="50Cent",Extension="mp3" } },
                    new MusicianViewModel() {ID = 4, Name = "Jay-Z", Age = 42, Nationality = "American", Intro = new Audio() {FileName="Jay-Z",Extension="mp3" } },
                    new MusicianViewModel() {ID = 5, Name = "Jeniffer Lopez", Age = 42, Nationality = "Spanish", Intro = new Audio() {FileName="JenniferLopez",Extension="mp3" } },
                };

            }
        }
        public IActionResult AudioColumn()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(musicians.ToDataSourceResult(request));
        }
    }
}
