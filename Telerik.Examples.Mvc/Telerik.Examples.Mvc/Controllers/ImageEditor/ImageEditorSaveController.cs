using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.Mvc.Controllers.ImageEditor
{
    public class ImageEditorSaveController : Controller
    {
        public IActionResult ImageEditorSave()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImageEditorSave(string contentType, string base64, string fileName)
        {
            byte[] bytes = Convert.FromBase64String(base64);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            image.Save(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName), System.Drawing.Imaging.ImageFormat.Png);
            return View();
        }
    }
}
