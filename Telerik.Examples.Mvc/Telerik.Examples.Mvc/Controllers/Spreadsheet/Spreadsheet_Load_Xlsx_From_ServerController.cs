using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using System.IO;
using Telerik.SvgIcons;
using Telerik.Web.Spreadsheet;

namespace Telerik.Examples.Mvc.Controllers.Spreadsheet
{
    public class Spreadsheet_Load_Xlsx_From_ServerController : Controller
    {
        public IActionResult Spreadsheet_Load_Xlsx_From_Server()
        {
            return View("~/Views/Spreadsheet/Spreadsheet_Load_Xlsx_From_Server.cshtml");
        }
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Spreadsheet_Load_Xlsx_From_ServerController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult ReadFile()
        {
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "files", "Test.xlsx");
            var exists = System.IO.File.Exists(filePath);
            if (System.IO.File.Exists(filePath))
            {
                Stream fileStream = System.IO.File.OpenRead(filePath);
                var workbook = Workbook.Load(fileStream, Path.GetExtension(filePath));
                return Content(workbook.ToJson(), Telerik.Web.Spreadsheet.MimeTypes.JSON);
            }
            else
            {
                return Content("rugda rugdil");
            }
        }
    }
}
