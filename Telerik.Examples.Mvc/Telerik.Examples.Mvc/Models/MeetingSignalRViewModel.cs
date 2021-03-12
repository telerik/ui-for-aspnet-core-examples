using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.Mvc.Models
{
    public class MeetingSignalRViewModel : ISchedulerEvent
    {
        public Guid? ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        private DateTime start;
        public DateTime Start
        {
            get => start;
            set => start = value.ToUniversalTime();
        }

        public string StartTimezone { get; set; }

        private DateTime end;

        public DateTime End
        {
            get => end;
            set => end = value.ToUniversalTime();
        }

        public string EndTimezone { get; set; }

        public string RecurrenceRule { get; set; }
        public int? RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public bool IsAllDay { get; set; }
        public string Timezone { get; set; }
        public int? RoomID { get; set; }
        public IEnumerable<int> Attendees { get; set; }
    }
}
