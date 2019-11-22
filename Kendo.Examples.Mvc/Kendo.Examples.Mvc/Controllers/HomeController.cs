using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Kendo.Examples.Mvc.Models;
using System.IO;

namespace Kendo.Examples.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var foldersToExclude = new string[] { "Views", "Home", "Shared"};
            var fileNames = new List<string>();
            var directoryNames = new List<string>();
            var txtPath = @".\Views";
            string[] files = Directory.GetFiles(txtPath, "*.cshtml", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var parentDirectory = Path.GetFileName(Path.GetDirectoryName(file));
                if (!foldersToExclude.Contains(parentDirectory) && !fileName.StartsWith("_"))
                {
                    if (!directoryNames.Contains(parentDirectory))
                    {
                        fileNames.Add(parentDirectory);
                        fileNames.Add(fileName);
                        directoryNames.Add(parentDirectory);
                    }
                    else
                    {
                        fileNames.Add(fileName);
                    }
                }
            }

            return View(fileNames);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
