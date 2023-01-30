using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace Telerik.Examples.Mvc.Controllers.Scheduler
{
    public class SchedulerCustomEditController : Controller
    {
        public IActionResult SchedulerCustomEdit()
        {
            return View();
        }

        private SchedulerTaskService taskService;

        public SchedulerCustomEditController(GeneralDbContext context)
        {
            taskService = new SchedulerTaskService(context);
        }

        public virtual JsonResult Read([DataSourceRequest] DataSourceRequest request, FilterRange range)
        {
            //var t = taskService.GetAll();
            var data = taskService.GetRange(range.Start, range.End);
            return Json(data.ToDataSourceResult(request));
            //return Json(taskService.GetAll().ToDataSourceResult(request));

        }

        public virtual JsonResult Destroy([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                taskService.Delete(task, ModelState);
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Create([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                taskService.Insert(task, ModelState);
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Update([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                taskService.Update(task, ModelState);
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            taskService.Dispose();
            base.Dispose(disposing);
        }
    }
}