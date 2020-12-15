using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.RazorPages.Models
{
    public class TimelineEventModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }

        public DateTime EventDate { get; set; }

        public string AltField { get; set; }

        public List<TimelineEventModelImage> Images { get; set; }
        public List<TimelineEventModelAction> Actions { get; set; }
    }

    public class TimelineEventModelImage
    {
        public string src { get; set; }
    }
    public class TimelineEventModelAction
    {
        public string text { get; set; }
        public string url { get; set; }
    }
}
