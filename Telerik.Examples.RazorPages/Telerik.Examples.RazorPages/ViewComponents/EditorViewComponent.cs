using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.ViewComponents
{
    public class EditorViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(EditorModel editorModel)
        {
            return View("default", editorModel);
        }
    }
}
