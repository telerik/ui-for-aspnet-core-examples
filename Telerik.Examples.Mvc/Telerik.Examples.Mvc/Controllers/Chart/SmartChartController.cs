using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Telerik.Examples.Mvc.Controllers.Chart
{
    public class SmartChartController : Controller
    {
        private readonly AiService _chartService;
        private readonly ILogger<SmartChartController> _logger;

        public SmartChartController(AiService chartService, ILogger<SmartChartController> logger)
        {
            _chartService = chartService;
            _logger = logger;
        }

        [HttpPost]
        [Route("SmartChart/GenerateChart")]
        public async Task<IActionResult> GenerateChart([FromBody] ChartRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Instructions))
                return BadRequest("Instructions cannot be empty.");

            try
            {
                var chartConfig = await _chartService.GenerateChartConfigAsync(request.Instructions);
                return Json(new { config = chartConfig });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating chart");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult SmartChart()
        {
            return View();
        }
    }

    public class ChartRequest
    {
        public string Instructions { get; set; }
    }
}
