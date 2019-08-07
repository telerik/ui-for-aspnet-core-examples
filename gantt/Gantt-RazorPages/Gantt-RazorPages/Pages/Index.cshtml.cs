using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gantt_RazorPages.Data;
using Kendo.Mvc.Extensions;

namespace Gantt_RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        public static IList<TaskViewModel> tasks;
        public static IList<DependencyViewModel> dependencies;

        public void OnGet()
        {
            if (tasks == null)
            {
                tasks = new List<TaskViewModel>()
                {
                    new TaskViewModel
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
                    new TaskViewModel
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
                    new TaskViewModel
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
                dependencies = new List<DependencyViewModel>()
                {
                    new DependencyViewModel()
                    {
                        DependencyID = 553,
                        PredecessorID = "18",
                        SuccessorID = "20",
                        Type = DependencyType.FinishStart
                    }
                };
            }
        }

        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(tasks.ToDataSourceResult(request));
        }

        public JsonResult OnPostCreate([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            task.TaskID = Guid.NewGuid().ToString();

            if (ModelState.IsValid)
            {
                tasks.Add(task);
            }
            return new JsonResult(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostUpdate([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            int index = tasks.IndexOf(tasks.FirstOrDefault(item => { return item.TaskID == task.TaskID; }));
            tasks[index] = task;

            return new JsonResult(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDestroy([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            int index = tasks.IndexOf(tasks.FirstOrDefault(item => { return item.TaskID == task.TaskID; }));
            tasks.RemoveAt(index);

            return new JsonResult(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDependenciesRead([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(dependencies.ToDataSourceResult(request));
        }

        public JsonResult OnPostDependenciesCreate([DataSourceRequest] DataSourceRequest request, DependencyViewModel dependency)
        {
            if (ModelState.IsValid)
            {
                dependencies.Add(dependency);
            }

            return new JsonResult(new[] { dependency }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDependenciesUpdate([DataSourceRequest] DataSourceRequest request, DependencyViewModel dependency)
        {
            int index = dependencies.IndexOf(dependencies.FirstOrDefault(item => { return item.DependencyID == dependency.DependencyID; }));
            dependencies[index] = dependency;

            return new JsonResult(new[] { dependency }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDependenciesDestroy([DataSourceRequest] DataSourceRequest request, DependencyViewModel dependency)
        {
            int index = dependencies.IndexOf(dependencies.FirstOrDefault(item => { return item.DependencyID == dependency.DependencyID; }));
            dependencies.RemoveAt(index);

            return new JsonResult(new[] { dependency }.ToDataSourceResult(request, ModelState));
        }

    }
}
