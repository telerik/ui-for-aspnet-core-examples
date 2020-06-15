using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Controllers.DateTimeOffset
{
    public class DateTimeOffsetController : Controller
    {
        private readonly IMapper mapper;
        private readonly CarsService service;
        public static IEnumerable<Car> cars;

        public DateTimeOffsetController(IMapper mapper, CarsService service)
        {
            this.mapper = mapper;
            this.service = service;

            if (cars == null)
            {
                cars = service.GetAllCars();
            }
        }

        public IActionResult DateTimeOffset()
        {
            return View(mapper.Map<CarViewModel>(cars.FirstOrDefault()));
        }

        public IActionResult AllCars([DataSourceRequest] DataSourceRequest request)
        {
            var result = cars.Select(car => mapper.Map<CarViewModel>(car));
            return Json(result.ToDataSourceResult(request));
        }
    }
}