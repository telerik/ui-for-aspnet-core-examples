using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Telerik.AI.SmartComponents.Extensions;
using Telerik.Examples.Mvc.Models;
using Telerik.SvgIcons;

namespace Telerik.Examples.Mvc.Controllers.Grid
{
    public class SmartGridController : Controller
    {
        private readonly AiService _smartGridService;
        IChatClient _chatClient;

        public SmartGridController(IChatClient smartGridService)
        {
            _chatClient = smartGridService;
        }

        public IActionResult SmartGrid()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetSales([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetFullSalesData().ToDataSourceResult(request));
        }

        [HttpPost]
        public async Task<IActionResult> Analyze([FromBody] GridAIRequest request)
        {
            var messages = request.Contents.Select(dto => dto).ToList();

            var options = new ChatOptions();
            options.AddGridChatTools(request.Columns);

            List<Microsoft.Extensions.AI.ChatMessage> conversationMessages = request.Contents
                .Select(m => new Microsoft.Extensions.AI.ChatMessage(ChatRole.User, m.Text))
                .ToList();

            if (_chatClient == null)
            {
                return StatusCode(500, "Chat service is not available.");
            }

            ChatResponse completion = await _chatClient.GetResponseAsync(conversationMessages, options);

            GridAIResponse response = completion.ExtractGridResponse();

            return new ContentResult
            {
                Content = System.Text.Json.JsonSerializer.Serialize(
                    response,
                    new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }),
                ContentType = "application/json"
            };

        }

        private List<SaleRecord> GetFullSalesData()
        {
            return new List<SaleRecord>
            {
                new SaleRecord { Id = 1, SalesPerson = "Alice", Region = "North", UnitsSold = 120, Total = 24000, Month = "January" },
                new SaleRecord { Id = 2, SalesPerson = "Bob", Region = "South", UnitsSold = 80, Total = 16000, Month = "January" },
                new SaleRecord { Id = 3, SalesPerson = "Charlie", Region = "West", UnitsSold = 150, Total = 30000, Month = "February" },
                new SaleRecord { Id = 4, SalesPerson = "Alice", Region = "North", UnitsSold = 130, Total = 26000, Month = "February" },
                new SaleRecord { Id = 5, SalesPerson = "Bob", Region = "South", UnitsSold = 90, Total = 18000, Month = "March" },
                new SaleRecord { Id = 6, SalesPerson = "Charlie", Region = "West", UnitsSold = 170, Total = 34000, Month = "March" },
                new SaleRecord { Id = 7, SalesPerson = "Alice", Region = "East", UnitsSold = 100, Total = 20000, Month = "April" },
                new SaleRecord { Id = 8, SalesPerson = "David", Region = "South", UnitsSold = 75, Total = 15000, Month = "April" },
                new SaleRecord { Id = 9, SalesPerson = "Eva", Region = "North", UnitsSold = 110, Total = 22000, Month = "May" },
                new SaleRecord { Id = 10, SalesPerson = "Bob", Region = "South", UnitsSold = 95, Total = 19000, Month = "May" },
                new SaleRecord { Id = 11, SalesPerson = "Charlie", Region = "West", UnitsSold = 160, Total = 32000, Month = "June" },
                new SaleRecord { Id = 12, SalesPerson = "Alice", Region = "East", UnitsSold = 115, Total = 23000, Month = "June" },
                new SaleRecord { Id = 13, SalesPerson = "David", Region = "North", UnitsSold = 105, Total = 21000, Month = "July" },
                new SaleRecord { Id = 14, SalesPerson = "Eva", Region = "West", UnitsSold = 135, Total = 27000, Month = "July" },
                new SaleRecord { Id = 15, SalesPerson = "Alice", Region = "East", UnitsSold = 125, Total = 25000, Month = "August" },
                new SaleRecord { Id = 16, SalesPerson = "Charlie", Region = "South", UnitsSold = 140, Total = 28000, Month = "August" },
                new SaleRecord { Id = 17, SalesPerson = "David", Region = "North", UnitsSold = 100, Total = 20000, Month = "September" },
                new SaleRecord { Id = 18, SalesPerson = "Bob", Region = "West", UnitsSold = 155, Total = 31000, Month = "September" },
                new SaleRecord { Id = 19, SalesPerson = "Eva", Region = "South", UnitsSold = 85, Total = 17000, Month = "October" },
                new SaleRecord { Id = 20, SalesPerson = "Charlie", Region = "East", UnitsSold = 145, Total = 29000, Month = "October" }
            };
        }

        public class GridAnalysisRequest
        {
            public string Instructions { get; set; }
            public string GridJson { get; set; }
        }
    }
}
