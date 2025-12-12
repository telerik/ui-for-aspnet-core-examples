using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Controllers.Chat
{
    public class SmartChatController : Controller  
    {
        public IActionResult SmartChat()
        {
            return View();
        }

        private readonly AiService _ai;
        public SmartChatController(AiService ai) { _ai = ai; }

        [HttpPost]
        public async Task<IActionResult> Ask([FromBody] AiPrompt req)
        {
            try
            {
                var result = await _ai.ProcessAsync(req.Prompt);
                return Json(new { answer = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

    }
}
