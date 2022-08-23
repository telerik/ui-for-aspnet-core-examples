using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Kendo.Core.Export;
using Telerik.Documents.Primitives;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Streaming;

namespace Telerik.Examples.Mvc.Controllers.Editor
{
    public class WatermarkController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public WatermarkController(IWebHostEnvironment webHostEnvironment)
        {
            this._hostingEnvironment = webHostEnvironment;
        }
        public IActionResult Watermark()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Export(EditorExportData data)
        {
            string fileDownloadName = data.FileName + ".pdf";
            string mimeType = "application/pdf";

            var settings = new EditorDocumentsSettings();
            settings.HtmlImportSettings.LoadImageFromUri += HtmlImportSettings_LoadImageFromUri;

            try
            {
                var filestream = EditorExport.Export(data, settings).FileStream;

                RadFixedPage foregroundContentOwner = GenerateForegroundTextContent("CONFIDENTIAL");
                MemoryStream ms = new MemoryStream();
                byte[] renderedBytes = null;

                using (PdfFileSource fileSource = new PdfFileSource(filestream))
                {
                    using (PdfStreamWriter fileWriter = new PdfStreamWriter(ms, true))
                    {
                        foreach (PdfPageSource pageSource in fileSource.Pages)
                        {
                            using (PdfPageStreamWriter pageWriter = fileWriter.BeginPage(pageSource.Size, pageSource.Rotation))
                            {
                                pageWriter.WriteContent(pageSource);

                                using (pageWriter.SaveContentPosition())
                                {
                                    double xCenteringTranslation = (pageSource.Size.Width - foregroundContentOwner.Size.Width) / 2;
                                    double yCenteringTranslation = (pageSource.Size.Height - foregroundContentOwner.Size.Height) / 2;
                                    pageWriter.ContentPosition.Translate(xCenteringTranslation, yCenteringTranslation);
                                    pageWriter.WriteContent(foregroundContentOwner);
                                }
                            }
                        }
                    }
                }
                renderedBytes = ms.ToArray();
                return File(renderedBytes, mimeType, fileDownloadName);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
        private void HtmlImportSettings_LoadImageFromUri(object sender, Telerik.Windows.Documents.Flow.FormatProviders.Html.LoadImageFromUriEventArgs e)
        {
            var uri = e.Uri;
            var absoluteUrl = uri.StartsWith("http://") || uri.StartsWith("www.");

            if (!absoluteUrl)
            {
                var trimmedUri = uri.Trim().TrimStart(Path.AltDirectorySeparatorChar).Replace("aspnet-core" + Path.AltDirectorySeparatorChar.ToString(), "").Replace('/','\\');
                var webRootPath = _hostingEnvironment.WebRootPath;
                var filePath = Path.Combine(webRootPath, trimmedUri);

                using (var fileStream = System.IO.File.OpenRead(filePath))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        e.SetImageInfo(memoryStream.ToArray(),"png");
                    }
                }
            }
        }
        private static RadFixedPage GenerateForegroundTextContent(string text)
        {
            Block block = new Block();
            block.BackgroundColor = new RgbColor(50, 0, 0, 0);
            block.GraphicProperties.FillColor = new RgbColor(255, 0, 0);
            block.TextProperties.FontSize = 120;
            block.InsertText(text);
            Size horizontalTextSize = block.Measure();
            double rotatedTextSquareSize = (horizontalTextSize.Width + horizontalTextSize.Height) / Math.Sqrt(2);

            RadFixedPage foregroundContentOwner = new RadFixedPage();
            foregroundContentOwner.Size = new Size(rotatedTextSquareSize, rotatedTextSquareSize);
            FixedContentEditor foregroundEditor = new FixedContentEditor(foregroundContentOwner);
            foregroundEditor.Position.Translate(horizontalTextSize.Height / Math.Sqrt(2), 0);
            foregroundEditor.Position.Rotate(45);
            foregroundEditor.DrawBlock(block);

            return foregroundContentOwner;
        }

    }
}
