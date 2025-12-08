using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Telerik.Examples.Mvc.Controllers.Editor
{
    public class SmartEditorController : Controller
    {
        private readonly AiService _smartEditorService;

        public SmartEditorController(AiService smartEditorService)
        {
            _smartEditorService = smartEditorService;
        }

        public IActionResult SmartEditor()
        {
            return View();
        }

        [HttpPost]
        [Route("api/smarteditor/edit")]
        public async Task<IActionResult> EditText([FromBody] EditRequest request)
        {
            if (string.IsNullOrEmpty(request.Text) || string.IsNullOrEmpty(request.Instruction))
                return BadRequest("Both text and instruction are required.");

            var revisedText = await _smartEditorService.EditTextAsync(request.Text, request.Instruction);
            return Ok(new { Suggestions = revisedText });
        }
    }

    public class EditRequest
    {
        public string Text { get; set; }
        public string Instruction { get; set; }
    }
}