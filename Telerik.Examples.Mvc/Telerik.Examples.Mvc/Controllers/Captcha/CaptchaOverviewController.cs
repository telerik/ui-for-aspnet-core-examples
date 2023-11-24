using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Drawing.Imaging;
using Telerik.Web.Captcha;
using Telerik.Examples.Mvc.Models;
using Microsoft.AspNetCore.Http;

namespace Telerik.Examples.Mvc.Controllers.Captcha
{
    public class CaptchaOverviewController : Controller
    {
        protected readonly IWebHostEnvironment HostingEnvironment;
        protected readonly string CaptchaPath;

        public CaptchaOverviewController(IWebHostEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
            CaptchaPath = Path.Combine(hostingEnvironment.WebRootPath, "shared", "Captcha");

            if (!Directory.Exists(CaptchaPath))
            {
                Directory.CreateDirectory(CaptchaPath);
            }
        }

        public IActionResult CaptchaOverview()
        {
            GenerateNewCaptcha();
            return View(new Person());
        }

        [HttpPost]
        public IActionResult Save(Person formData, CaptchaInputModel captchaModel)
        {
            if (string.IsNullOrEmpty(captchaModel.CaptchaID))
            {
                GenerateNewCaptcha();
            }
            else
            {
                string text = GetCaptchaText(captchaModel.CaptchaID);

                if (text == captchaModel.Captcha.ToUpperInvariant())
                {
                    ModelState.Clear();
                    GenerateNewCaptcha();
                }
            }

            return View("CaptchaOverview", formData);
        }

        public ActionResult Reset_Events()
        {
            CaptchaImage newCaptcha = SetCaptchaImage();

            return Json(new CaptchaInputModel
            {
                Captcha = Url.Content("~/shared/Captcha/" + newCaptcha.UniqueId + ".png"),
                CaptchaID = newCaptcha.UniqueId
            });
        }

        public ActionResult Validate_Events(CaptchaInputModel model)
        {
            string text = GetCaptchaText(model.CaptchaID);

            return Json(text == model.Captcha.ToUpperInvariant());
        }

        private void GenerateNewCaptcha()
        {
            CaptchaImage captchaImage = SetCaptchaImage();

            ViewData["Captcha"] = Url.Content("~/shared/Captcha/" + captchaImage.UniqueId + ".png");
            ViewData["CaptchaID"] = captchaImage.UniqueId;
        }

        private string GetCaptchaText(string captchaId)
        {
            string text = HttpContext.Session.GetString("captcha_" + captchaId);

            return text;
        }

        private CaptchaImage SetCaptchaImage()
        {
            CaptchaImage newCaptcha = CaptchaHelper.GetNewCaptcha();

            MemoryStream audio = CaptchaHelper.SpeakText(newCaptcha);
            using (FileStream file = new FileStream(Path.Combine(CaptchaPath, newCaptcha.UniqueId + ".wav"), FileMode.Create, FileAccess.Write))
            {
                audio.WriteTo(file);
            }

            var image = CaptchaHelper.RenderCaptcha(newCaptcha);
            image.Save(Path.Combine(CaptchaPath, newCaptcha.UniqueId + ".png"), ImageFormat.Png);

            HttpContext.Session.SetString("captcha_" + newCaptcha.UniqueId, newCaptcha.Text);

            return newCaptcha;
        }
    }
}
