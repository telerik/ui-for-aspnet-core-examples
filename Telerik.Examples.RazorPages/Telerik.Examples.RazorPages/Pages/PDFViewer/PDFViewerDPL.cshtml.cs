using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Web.PDF;

namespace Telerik.Examples.RazorPages.Pages.PDFViewer
{
    public class PDFViewerDPLModel : PageModel
    {
        private IHostingEnvironment _hostingEnvironment;

        public PDFViewerDPLModel(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        public void OnGet()
        {
        }

        public IActionResult OnGetRead(int? pageNumber)
        {
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "sample.pdf");
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            JsonResult jsonResult;
            FixedDocument doc = FixedDocument.Load(stream);

            if (pageNumber == null)
            {
                jsonResult = new JsonResult(doc.ToJson());
            }
            else
            {
                jsonResult = new JsonResult(doc.GetPage((int)pageNumber));
            }

            return jsonResult;
        }
    }
}
