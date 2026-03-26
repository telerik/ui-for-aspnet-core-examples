using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Controllers.Gantt
{
    public class ViewsController : Controller
    {
        public IActionResult Views()
        {
            return View("~/Views/Gantt/Views.cshtml");
        }

        private IList<GanttTaskViewModel> GetViewsTasks()
        {
            return new List<GanttTaskViewModel>
            {
                new GanttTaskViewModel
                {
                    TaskID = 1,
                    Title = "Project Plan",
                    ParentID = null,
                    OrderId = 0,
                    Start = new DateTime(2024, 7, 14),
                    End = new DateTime(2024, 8, 31),
                    PercentComplete = 0.52m,
                    Summary = true,
                    Expanded = true
                },
                new GanttTaskViewModel
                {
                    TaskID = 2,
                    Title = "Research Phase",
                    ParentID = 1,
                    OrderId = 0,
                    Start = new DateTime(2024, 7, 14),
                    End = new DateTime(2024, 7, 10),
                    PercentComplete = 1m,
                    Summary = false,
                    Expanded = true
                },
                new GanttTaskViewModel
                {
                    TaskID = 3,
                    Title = "Design Phase",
                    ParentID = 1,
                    OrderId = 1,
                    Start = new DateTime(2024, 7, 8),
                    End = new DateTime(2024, 7, 19),
                    PercentComplete = 1m,
                    Summary = false,
                    Expanded = true
                },
                new GanttTaskViewModel
                {
                    TaskID = 4,
                    Title = "Development",
                    ParentID = 1,
                    OrderId = 2,
                    Start = new DateTime(2024, 7, 18),
                    End = new DateTime(2024, 8, 16),
                    PercentComplete = 0.6m,
                    Summary = true,
                    Expanded = true
                },
                new GanttTaskViewModel
                {
                    TaskID = 5,
                    Title = "Backend Development",
                    ParentID = 4,
                    OrderId = 0,
                    Start = new DateTime(2024, 7, 18),
                    End = new DateTime(2024, 8, 2),
                    PercentComplete = 0.75m,
                    Summary = false,
                    Expanded = true
                },
                new GanttTaskViewModel
                {
                    TaskID = 6,
                    Title = "Frontend Development",
                    ParentID = 4,
                    OrderId = 1,
                    Start = new DateTime(2024, 7, 29),
                    End = new DateTime(2024, 8, 16),
                    PercentComplete = 0.45m,
                    Summary = false,
                    Expanded = true
                },
                new GanttTaskViewModel
                {
                    TaskID = 7,
                    Title = "Testing & QA",
                    ParentID = 1,
                    OrderId = 3,
                    Start = new DateTime(2024, 8, 7),
                    End = new DateTime(2024, 8, 17),
                    PercentComplete = 0.2m,
                    Summary = false,
                    Expanded = true
                },
                new GanttTaskViewModel
                {
                    TaskID = 8,
                    Title = "Deployment & Launch",
                    ParentID = 1,
                    OrderId = 4,
                    Start = new DateTime(2024, 8, 16),
                    End = new DateTime(2024, 8, 24),
                    PercentComplete = 0m,
                    Summary = false,
                    Expanded = true
                }
            };
        }

        private IList<GanttDependencyViewModel> GetViewsDependencies()
        {
            return new List<GanttDependencyViewModel>
            {
                new GanttDependencyViewModel { DependencyID = 1, PredecessorID = 2, SuccessorID = 3, Type = DependencyType.FinishStart },
                new GanttDependencyViewModel { DependencyID = 2, PredecessorID = 3, SuccessorID = 4, Type = DependencyType.FinishStart },
                new GanttDependencyViewModel { DependencyID = 3, PredecessorID = 5, SuccessorID = 6, Type = DependencyType.FinishFinish },
                new GanttDependencyViewModel { DependencyID = 4, PredecessorID = 6, SuccessorID = 7, Type = DependencyType.FinishStart },
                new GanttDependencyViewModel { DependencyID = 5, PredecessorID = 7, SuccessorID = 8, Type = DependencyType.FinishStart }
            };
        }

        public JsonResult Views_Read_Tasks([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetViewsTasks().ToDataSourceResult(request));
        }

        public JsonResult Views_Read_Dependencies([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetViewsDependencies().ToDataSourceResult(request));
        }
    }
}
