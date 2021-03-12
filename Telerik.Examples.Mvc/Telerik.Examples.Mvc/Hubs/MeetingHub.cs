using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Hubs
{
    public class MeetingHub : Hub
    {
        //static collection used for Demo purposes. You can fetch events from a database, for example.
        public static List<MeetingSignalRViewModel> Meetings { get; set; }

        public MeetingHub()
        {
            Meetings = new List<MeetingSignalRViewModel>() {
            new MeetingSignalRViewModel()
            {
                ID = Guid.Parse("4ead4694-4709-475b-80e4-20346002678d"),
                Title = "Meeting 1",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2)
            }
            };
        }
        public async Task<MeetingSignalRViewModel> Create(MeetingSignalRViewModel item)
        {
            item.ID = Guid.NewGuid();
            Meetings.Add(item);

            await Clients.Others.SendAsync("CREATE", item);

            return item;
        }

        public IEnumerable<MeetingSignalRViewModel> Read()
        {
            return Meetings;
        }

        public async System.Threading.Tasks.Task Update(MeetingSignalRViewModel item)
        {
            var target = Meetings.FirstOrDefault(x => x.ID == item.ID);
            target = item;

            await Clients.Others.SendAsync("UPDATE", item);
        }

        public async System.Threading.Tasks.Task Destroy(MeetingSignalRViewModel item)
        {
            var target = Meetings.Find(x => x.ID == item.ID);
            Meetings.Remove(target);

            await Clients.Others.SendAsync("DESTROY", item);
        }
    }
}
