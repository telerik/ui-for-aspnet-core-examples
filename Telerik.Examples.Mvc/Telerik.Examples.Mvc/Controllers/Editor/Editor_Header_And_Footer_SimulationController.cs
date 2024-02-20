using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text.RegularExpressions;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.FormatProviders.Html;
using Telerik.Windows.Documents.Flow.Model.Collections;
using Telerik.Windows.Documents.Flow.Model.Styles;
using Telerik.Windows.Documents.Flow.Model;
using Kendo.Core.Export;

namespace Telerik.Examples.Mvc.Controllers.Editor
{
    public class Editor_Header_And_Footer_SimulationController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public Editor_Header_And_Footer_SimulationController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        public IActionResult Editor_Header_And_Footer_Simulation()
        {
            string docXFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "files", "sample.docx");

            DocxFormatProvider docxProvider = new();
            HtmlFormatProvider htmlProvider = new();
            RadFlowDocument? document = null;
            Paragraph headers = null;
            InlineCollection headerBlockContent;
            InlineCollection footerBlockContent;
            Paragraph footers = null;

            using (FileStream input = new FileStream(docXFilePath, FileMode.Open))
            {
                document = docxProvider.Import(input);
                headers = (Paragraph) document.Sections.First().Headers.Default.Blocks.First();
                headerBlockContent = headers.Inlines;
                footers = (Paragraph) document.Sections.First().Footers.Default.Blocks.First();
                footerBlockContent = footers.Inlines;
            }
            var htmlString = htmlProvider.Export(document);
            var headerString = headerBlockContent[0].ToString();
            var footerString = footerBlockContent[1].ToString();

            ViewData["htmlString"] = htmlString;
            ViewData["headerString"] = headerString;
            ViewData["footerString"] = footerString;

            return View();
        }
        [HttpPost]
        public ActionResult Export(EditorExportData data)
        {
            string editorHtml = data.Value;
            string encodedHtml = System.Net.WebUtility.HtmlDecode(data.Value);
            string classNameHeader = "header";
            string classNameFooter = "footer";
            string patternHeader = $"<p\\s+class=\"{classNameHeader}\"[^>]*>.*?</p>";
            string patternFooter = $"<p\\s+class=\"{classNameFooter}\"[^>]*>.*?</p>";
            Regex regexHeader = new Regex(patternHeader);
            Regex regexFooter = new Regex(patternFooter);
            string header = regexHeader.Match(encodedHtml).Value;
            string footer = regexFooter.Match(encodedHtml).Value;
            encodedHtml = encodedHtml.Replace(header, "").Replace(footer, "");
            data.Value = System.Net.WebUtility.HtmlEncode(encodedHtml);

            header = header.Replace("<p class=\"header\">", "").Replace("</p>", "");
            footer = footer.Replace("<p class=\"footer\">", "").Replace("</p>", "");

            DocxFormatProvider docxProvider = new();
            var exportFileStreamRaw = EditorExport.Export(data);
            byte[] exportFileStreamProcessed;
            using (Stream output = exportFileStreamRaw.FileStream)
            {
                var document = docxProvider.Import(output);
                Header defaultHeader = document.Sections.First().Headers.Add();
                Paragraph defaultHeaderParagraph = defaultHeader.Blocks.AddParagraph();
                defaultHeaderParagraph.TextAlignment = Alignment.Center;
                defaultHeaderParagraph.Inlines.AddRun(header);

                Footer defaultFooter = document.Sections.First().Footers.Add();
                Paragraph defaultFooterParagraph = defaultFooter.Blocks.AddParagraph();
                defaultFooterParagraph.TextAlignment = Alignment.Center;
                defaultFooterParagraph.Inlines.AddRun(footer);
                exportFileStreamProcessed = docxProvider.Export(document);
            }
            try
            {
                return File(exportFileStreamProcessed, "application/octet-stream", data.FileName + ".docx");
            }
            catch
            {
                return RedirectToAction("Editor_Header_And_Footer_Simulation", "Editor_Header_And_Footer_Simulation");
            }
        }
    }
}