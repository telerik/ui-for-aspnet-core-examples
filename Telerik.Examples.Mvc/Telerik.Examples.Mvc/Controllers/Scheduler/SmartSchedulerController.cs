using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Controllers;

namespace Telerik.Examples.Mvc.Controllers.Scheduler
{
    public class SmartSchedulerController : Controller
    {
        private readonly AiService _aiService;
        private readonly ILogger<SmartSchedulerController> _logger;

        public SmartSchedulerController(AiService aiService, ILogger<SmartSchedulerController> logger)
        {
            _aiService = aiService;
            _logger = logger;
        }

        [HttpPost]
        [Route("SmartScheduler/GenerateSchedule")]
        public async Task<IActionResult> GenerateSchedule([FromBody] SchedulerRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Instructions))
                return BadRequest("Instructions cannot be empty.");

            try
            {
                var config = await _aiService.GenerateSchedulerConfigAsync(request.Instructions);
                return Json(new { config });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Scheduler generation error");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult SmartScheduler()
        {
            return View();
        }
    }

    public class SchedulerRequest
    {
        public string Instructions { get; set; }
    }
}
