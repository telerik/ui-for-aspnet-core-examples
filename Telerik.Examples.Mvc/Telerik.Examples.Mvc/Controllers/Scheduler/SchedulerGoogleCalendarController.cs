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
    public class SchedulerGoogleCalendarController : Controller
    {
        public IActionResult SchedulerGoogleCalendar()
        {
            return View();
        }

        private SchedulerTaskService taskService;

        private List<SchedulerEventModel> appointments = new List<SchedulerEventModel> {
            new SchedulerEventModel {
                TaskID = 1,
                Description = "Task 1 Description",
                Title = "Task 1",
                Start = new DateTime(2023,4,11,6,00,00),
                End= new DateTime(2023,4,11,8,30,00),
                RoomID = 1,
                Attendees = new List<int>() { 1, 2 }
            },
            new SchedulerEventModel {
                TaskID = 2,
                Description = "Task 2 Description",
                Title = "Task 2",
                Start = new DateTime(2023,4,10,12,00,00),
                End= new DateTime(2023,4,10,13,00,00),
                RoomID = 2,
                Attendees = new List<int>() { 2, 3 }
            }
        };

        public SchedulerGoogleCalendarController(GeneralDbContext context)
        {
            taskService = new SchedulerTaskService(context);
        }

        public virtual JsonResult Read([DataSourceRequest] DataSourceRequest request, FilterRange range)
        {
            return Json(appointments.ToDataSourceResult(request));

        }

        public virtual JsonResult Delete([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
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