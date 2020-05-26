using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Controllers.Grid
{
    public class DateTimeOffsetController : Controller
    {
        private readonly IMapper mapper;
        private readonly CarsService service;


        public DateTimeOffsetController(IMapper mapper, CarsService service)
        {
            this.mapper = mapper;
            this.service = service;
        }

        public IActionResult Index()
        {
            return View(mapper.Map<CarViewModel>(service.GetAllCars().FirstOrDefault()));
        }

        public IActionResult AllCars([DataSourceRequest] DataSourceRequest request)
        {
            return Json(service.GetAllCars().ToDataSourceResult(request, car => mapper.Map<CarViewModel>(car)));
        }
    }
}