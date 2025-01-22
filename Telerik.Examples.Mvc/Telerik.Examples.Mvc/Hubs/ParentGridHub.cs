using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System;
using Telerik.Examples.Mvc.Models;
using Product = Telerik.Examples.Mvc.Models.Product;

namespace Telerik.Examples.Mvc.Hubs
{
    public class ParentGridHub : Hub
    {
        private Random _randomHelper { get; set; }

        public ParentGridHub()
        {
            _randomHelper = new Random();
        }

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

        public IEnumerable<Product> Read()
         => Enumerable.Range(1, 2).Select(i => new Product
            {
                Discontinued = i % 2 == 1,
                ProductID = i,
                ProductName = "Product" + i,
                UnitPrice = _randomHelper.Next(1, 1000),
                UnitsInStock = _randomHelper.Next(1, 1000),
                UnitsOnOrder = _randomHelper.Next(1, 1000)
            }).ToList();

        public Product Create(Product product)
        {
            Clients.OthersInGroup(GetGroupName()).SendAsync("create", product);
            return product;
        }

        public void Update(Product product)
           => Clients.OthersInGroup(GetGroupName()).SendAsync("update", product);

        public void Destroy(Product product)
           => Clients.OthersInGroup(GetGroupName()).SendAsync("destroy", product);

        public string GetGroupName()
           => GetRemoteIpAddress();

        public string GetRemoteIpAddress()
           => Context.GetHttpContext()?.Connection.RemoteIpAddress.ToString();
    }
}
