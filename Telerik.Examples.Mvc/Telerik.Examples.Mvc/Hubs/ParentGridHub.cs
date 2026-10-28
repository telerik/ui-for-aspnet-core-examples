using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System;
using Telerik.Examples.Mvc.Models;
using ProductModel = Telerik.Examples.Mvc.Models.Product;

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

        public IEnumerable<ProductModel> Read()
         => Enumerable.Range(1, 2).Select(i => new ProductModel
            {
                Discontinued = i % 2 == 1,
                ProductID = i,
                ProductName = "Product" + i,
                UnitPrice = _randomHelper.Next(1, 1000),
                UnitsInStock = _randomHelper.Next(1, 1000),
                UnitsOnOrder = _randomHelper.Next(1, 1000)
            }).ToList();

        public ProductModel Create(ProductModel product)
        {
            Clients.OthersInGroup(GetGroupName()).SendAsync("create", product);
            return product;
        }

        public void Update(ProductModel product)
           => Clients.OthersInGroup(GetGroupName()).SendAsync("update", product);

        public void Destroy(ProductModel product)
           => Clients.OthersInGroup(GetGroupName()).SendAsync("destroy", product);

        public string GetGroupName()
           => GetRemoteIpAddress();

        public string GetRemoteIpAddress()
           => Context.GetHttpContext()?.Connection.RemoteIpAddress.ToString();
    }
}
