using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Editor
{
    public class EditorViewComponentModel : PageModel
    {
        [BindProperty]
        public static EditorModel EditorModel { get; set; }

        public void OnGet()
        {
            EditorModel = new EditorModel() { Value = "Some Value" };
        }
    }
}
