using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Notification
{
    public class NotificationIndexModel : PageModel
    {
        public void OnGet()
        {

        }

        public JsonResult OnPostRead()
        {
            NotificationModel model = new NotificationModel()
            {
                Text = "Notification Text",
                Time = DateTime.Now
            };
            return new JsonResult(model);
        }
    }
}