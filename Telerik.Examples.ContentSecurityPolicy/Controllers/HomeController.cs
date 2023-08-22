using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telerik.Examples.ContentSecurityPolicy.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Text.RegularExpressions;

namespace Telerik.Examples.ContentSecurityPolicy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public HomeController(ILogger<HomeController> logger, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _logger = logger;
            this._actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public IActionResult Index()
        {

            var demoEndpoints = this._actionDescriptorCollectionProvider.ActionDescriptors.Items
                .OfType<ControllerActionDescriptor>()
                .Where(w => w.ControllerName.Replace("Controller", String.Empty) == w.ActionName)
                .Select(s => new Demo
                {
                    ComponentName = Regex.Match(s.ControllerTypeInfo.FullName, @"(?<=\bControllers\b.).*(?=\.)").Value,
                    ControllerName = s.ControllerName,
                    ActionName = s.ActionName
                })
                .GroupBy(g => g.ComponentName)
                .ToList();

            return View(demoEndpoints);
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
