using System;
using System.Linq;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Telerik.Examples.Mvc.Models;
using Kendo.Mvc.Extensions;

namespace Telerik.Examples.Mvc.Controllers.Gantt
{
    public class CrudOperationsController : Controller
    {
        public static IList<GanttTaskViewModel> tasks;
        public static IList<GanttDepedencyViewModel> dependencies;
        public CrudOperationsController()
        {
            if (tasks == null)
            {
                tasks = new List<GanttTaskViewModel>()
                {
                    new GanttTaskViewModel
                    {
                        TaskID = "7",
                        Title = "Software validation, research and implementation",
                        OrderId = 0,
                        ParentID = null,
                        Start = new DateTime(2014,6,2),
                        End = new DateTime(2014,7,12),
                        PercentComplete = (decimal)0.43,
                        Summary = true,
                        Expanded = true
                    },
                    new GanttTaskViewModel
                    {
                        TaskID = "18",
                        Title = "Project Kickoff",
                        OrderId = 0,
                        ParentID = "7",
                        Start = new DateTime(2014,6,2),
                        End = new DateTime(2014,6,2),
                        PercentComplete = (decimal)0.23,
                        Summary = false,
                        Expanded = true
                    },
                    new GanttTaskViewModel
                    {
                        TaskID = "20",
                        Title = "Market Research",
                        OrderId = 1,
                        ParentID = "18",
                        Start = new DateTime(2014,6,2),
                        End = new DateTime(2014,6,4),
                        PercentComplete = (decimal)0.82,
                        Summary = false,
                        Expanded = true
                    }
                };
            }

            if (dependencies == null)
            {
                dependencies = new List<GanttDepedencyViewModel>()
                {
                    new GanttDepedencyViewModel()
                    {
                        DependencyID = 553,
                        PredecessorID = "18",
                        SuccessorID = "20",
                        Type = DependencyType.FinishStart
                    }
                };
            }
        }
        public IActionResult CrudOperations()
        {
            return View();
        }

        public JsonResult Read_Tasks([DataSourceRequest] DataSourceRequest request)
        {
            return Json(tasks.ToDataSourceResult(request));
        }

        public JsonResult Create_Task([DataSourceRequest] DataSourceRequest request, GanttTaskViewModel task)
        {
            task.TaskID = Guid.NewGuid().ToString();

            tasks.Add(task);

            return Json(new[] { task }.ToDataSourceResult(request));
        }

        public JsonResult Update_Task([DataSourceRequest] DataSourceRequest request, GanttTaskViewModel task)
        {
            int index = tasks.IndexOf(tasks.FirstOrDefault(item => { return item.TaskID == task.TaskID; }));
            tasks[index] = task;

            return Json(new[] { task }.ToDataSourceResult(request));
        }

        public JsonResult Destroy_Task([DataSourceRequest] DataSourceRequest request, GanttTaskViewModel task)
        {
            int index = tasks.IndexOf(tasks.FirstOrDefault(item => { return item.TaskID == task.TaskID; }));
            tasks.RemoveAt(index);

            return Json(new[] { task }.ToDataSourceResult(request));
        }

        public JsonResult Read_Dependencies([DataSourceRequest] DataSourceRequest request)
        {
            return Json(dependencies.ToDataSourceResult(request));
        }

        public JsonResult Create_Dependency([DataSourceRequest] DataSourceRequest request, GanttDepedencyViewModel dependency)
        {

            dependencies.Add(dependency);


            return Json(new[] { dependency }.ToDataSourceResult(request));
        }

        public JsonResult Update_Dependency([DataSourceRequest] DataSourceRequest request, GanttDepedencyViewModel dependency)
        {
            int index = dependencies.IndexOf(dependencies.FirstOrDefault(item => { return item.DependencyID == dependency.DependencyID; }));
            dependencies[index] = dependency;

            return Json(new[] { dependency }.ToDataSourceResult(request));
        }

        public JsonResult Destroy_Dependency([DataSourceRequest] DataSourceRequest request, GanttDepedencyViewModel dependency)
        {
            int index = dependencies.IndexOf(dependencies.FirstOrDefault(item => { return item.DependencyID == dependency.DependencyID; }));
            dependencies.RemoveAt(index);

            return Json(new[] { dependency }.ToDataSourceResult(request));
        }
    }
}
