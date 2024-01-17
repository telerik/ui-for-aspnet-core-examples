using Telerik.Examples.Mvc.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Telerik.Examples.Mvc.Hubs
{
    public class GridHub : Hub
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

        public IEnumerable<ProductViewModel> Read()
        {

            var result = Enumerable.Range(1, 50).Select(i => new ProductViewModel
            {
                ProductID = i,
                ProductName = "Product ID "+i,
                UnitPrice= new Random().Next(1, 20),
                UnitsInStock = i * new Random().Next(0,45),
                Discontinued = false,
                LastSupply = DateTime.Now.AddMonths(-1),
                Category = new Category() { CategoryID=i%5,CategoryName="Category "+ i % 5 },
                CategoryID = i % 5,
                QuantityPerUnit = new Random().Next(1, 10).ToString()
            }).ToList();
            return result;

        }

        public ProductViewModel Create(ProductViewModel product)
        {

            Clients.OthersInGroup(GetGroupName()).SendAsync("create", product);

            return product;
        }

        public void Update(ProductViewModel product)
        {
            Clients.OthersInGroup(GetGroupName()).SendAsync("update", product);
        }

        public void Destroy(ProductViewModel product)
        {
            Clients.OthersInGroup(GetGroupName()).SendAsync("destroy", product);
        }

        public string GetGroupName()
        {
            return GetRemoteIpAddress();
        }

        public string GetRemoteIpAddress()
        {
            return Context.GetHttpContext()?.Connection.RemoteIpAddress.ToString();
        }

    }
}