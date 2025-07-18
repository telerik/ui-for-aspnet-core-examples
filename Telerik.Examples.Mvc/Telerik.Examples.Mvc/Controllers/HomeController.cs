﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telerik.Examples.Mvc.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Text.RegularExpressions;
using System.IO;
using System.Text.Json;

namespace Telerik.Examples.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public HomeController(ILogger<HomeController> logger, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider )
        {
            _logger = logger;
            this._actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public IActionResult Index()
        {          
            var list = this._actionDescriptorCollectionProvider.ActionDescriptors.Items
                .OfType<ControllerActionDescriptor>()
                .Where(w => w.ControllerName.Replace("Controller",String.Empty) == w.ActionName)
                .Select(s => new Demo
                { 
                    ComponentName = Regex.Match(s.ControllerTypeInfo.FullName, @"(?<=\bControllers\b.).*(?=\.)").Value,
                    ControllerName = s.ControllerName,
                    ActionName = s.ActionName
                })
                .ToList();

            System.IO.File.WriteAllBytes(Directory.GetCurrentDirectory() + "/wwwroot/files/ExamplesEndpoints.txt", JsonSerializer.SerializeToUtf8Bytes(list));
            
            return View();
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
