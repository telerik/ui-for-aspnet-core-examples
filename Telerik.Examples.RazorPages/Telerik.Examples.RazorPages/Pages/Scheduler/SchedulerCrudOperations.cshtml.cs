using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Scheduler
{
    public class SchedulerCrudOperationsModel : PageModel
    {
        public static IList<MeetingViewModel> meetings;

        public void OnGet()
        {
            if (meetings == null)
            {
                meetings = new List<MeetingViewModel>();
                Enumerable.Range(1, 5).ToList().ForEach(x => meetings.Add(new MeetingViewModel()
                {
                    MeetingID = x,
                    Title = "Event " + x,
                    Start = DateTime.Now.AddHours(x * 2),
                    End = DateTime.Now.AddHours(x * 3),
                    Description = "Description for event " + x,
                    Attendees = new List<int>() { (x % 3) +1 }
                })); ;

            }
        }

        public virtual JsonResult OnPostMeetings_Read([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(meetings.ToDataSourceResult(request));
        }
        public virtual JsonResult OnPostMeetings_Destroy([DataSourceRequest] DataSourceRequest request, MeetingViewModel meeting)
        {
            if (ModelState.IsValid)
            {
                meetings.Remove(meetings.First(x => x.MeetingID == meeting.MeetingID));
            }

            return new JsonResult(new[] { meeting }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult OnPostMeetings_Create([DataSourceRequest] DataSourceRequest request, MeetingViewModel meeting)
        {
            if (ModelState.IsValid)
            {
                meeting.MeetingID = meetings.Count + 2;
                meetings.Add(meeting);
            }

            return new JsonResult(new[] { meeting }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult OnPostMeetings_Update([DataSourceRequest] DataSourceRequest request, MeetingViewModel meeting)
        {
            if (ModelState.IsValid)
            {
                meetings.Where(x => x.MeetingID == meeting.MeetingID).Select(x => meeting);
            }

            return new JsonResult(new[] { meeting }.ToDataSourceResult(request, ModelState));
        }
    }
}