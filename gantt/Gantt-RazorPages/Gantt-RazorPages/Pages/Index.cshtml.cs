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
                        TaskID = 7,
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
                        TaskID = 18,
                        Title = "Project Kickoff",
                        OrderId = 0,
                        ParentID = 7,
                        Start = new DateTime(2014,6,2),
                        End = new DateTime(2014,6,2),
                        PercentComplete = (decimal)0.23,
                        Summary = false,
                        Expanded = true
                    },
                    new TaskViewModel
                    {
                        TaskID = 20,
                        Title = "Market Research",
                        OrderId = 1,
                        ParentID = 18,
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
                        PredecessorID = 18,
                        SuccessorID = 20,
                        Type = DependencyType.FinishStart
                    }
                };
            }
        }

        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(tasks.ToDataSourceResult(request));
        }

        public JsonResult OnPostDependenciesRead([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(dependencies.ToDataSourceResult(request));
        }

    }
}
