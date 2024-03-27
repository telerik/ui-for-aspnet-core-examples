using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Hubs
{
    public class ChildGridHub : Hub
    {
        public override System.Threading.Tasks.Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName());
            return base.OnConnectedAsync();
        }

        public override System.Threading.Tasks.Task OnDisconnectedAsync(Exception e)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName());
            return base.OnDisconnectedAsync(e);
        }

        public IEnumerable<Person> Read(HierarchyRequestModel request)
             => Enumerable.Range(1, 10).Select(i => new Person
             {
                 PersonID = i,
                 BirthDate = DateTime.Now.AddDays(i),
                 ProductID = i % 2 == 0 ? 1 : 2,
                 Name = "Name" + i
             })
             .Where(person => person.ProductID == request.ProductID)
             .ToList();

        public Person Create(Person person)
        {
            Clients.OthersInGroup(GetGroupName()).SendAsync("create", person);
            return person;
        }

        public void Update(Person person)
           => Clients.OthersInGroup(GetGroupName()).SendAsync("update", person);

        public void Destroy(Person person)
           =>  Clients.OthersInGroup(GetGroupName()).SendAsync("destroy", person);
        public string GetGroupName()
           =>  GetRemoteIpAddress();

        public string GetRemoteIpAddress()
           => Context.GetHttpContext()?.Connection.RemoteIpAddress.ToString();
    }
}
