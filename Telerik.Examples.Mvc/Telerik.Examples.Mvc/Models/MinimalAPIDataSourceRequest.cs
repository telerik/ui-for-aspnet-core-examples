using Kendo.Mvc.Infrastructure;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using NuGet.Packaging.Licenses;
using System.Linq;
using System;

namespace Telerik.Examples.Mvc.Models
{
    public class MinimalAPIDataSourceRequest : DataSourceRequest
    {
        public static List<OrderViewModel> GetData()
        {
            return Enumerable.Range(1, 50).Select(i => new OrderViewModel
            {
                OrderID = i,
                Freight = i * 10,
                EmployeeID = i,
                CustomerID = i.ToString(),
                ShipAddress = "ShipAddress" + i,
                ShipCountry = "Country" + i,
                ContactName = "ContactName" + i,
                OrderDate = DateTime.Now.AddDays(i),
                ShippedDate = DateTime.Now.AddDays(i + 2),
                ShipName = "ShipName " + i,
                ShipCity = "ShipCity " + i
            }).ToList();
        }

        public static ValueTask<MinimalAPIDataSourceRequest?> BindAsync(HttpContext context,
                                                  ParameterInfo parameter)
        {
            const string page = "page";
            const string pageSize = "pageSize";
            const string sorts = "sort";
            const string groups = "group";
            const string filter = "filter";
            const string aggregates = "aggregate";


            var requestPage = int.Parse(context.Request.Query["page"]);
            var requestPageSize = int.Parse(context.Request.Query["pageSize"]);

            var filterExpression = FilterDescriptorFactory.Create(context.Request.Query[filter]);

            var sortingQuery = context.Request.Query[sorts].ToString().Split("-");

            SortDescriptor? sortingExpression = null;
            if (sortingQuery.Length > 1)
            {
                sortingExpression = new SortDescriptor
                {
                    Member = sortingQuery[0],
                    SortDirection = sortingQuery[1] == "asc" ? ListSortDirection.Ascending : ListSortDirection.Descending
                };
            }

            var result = new MinimalAPIDataSourceRequest
            {
                Page = requestPage,
                PageSize = requestPageSize,
                Filters = filterExpression,
                Sorts = sortingExpression != null ? new List<SortDescriptor> { sortingExpression } : null
            };

            return ValueTask.FromResult<MinimalAPIDataSourceRequest?>(result);
        }
    }
}
