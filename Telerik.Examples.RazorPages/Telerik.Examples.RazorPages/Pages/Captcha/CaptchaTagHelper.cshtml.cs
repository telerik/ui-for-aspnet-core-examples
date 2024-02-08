using Microsoft.AspNetCore.Mvc;
using Telerik.Examples.RazorPages.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Telerik.Web.Captcha;
using Microsoft.AspNetCore.Http;
using System.Drawing.Imaging;

namespace Telerik.Examples.RazorPages.Pages.Captcha
{
    public class CaptchaTagHelperModel : PageModel
    {
        [BindProperty]
        public OrderViewModel Order { get; set; }

        protected readonly IWebHostEnvironment HostingEnvironment;
        protected string CaptchaPath { get; set; }

        public CaptchaTagHelperModel(IWebHostEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;

            var captchaFilePath = Path.Combine("shared", "UserFiles", "Folders", "Captcha");
            CaptchaPath = $"{HostingEnvironment.WebRootPath}\\{captchaFilePath}";
        }

        public void OnGet(CaptchaModel captchaModel)
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
            if (Order == null)
            {
                Order = new OrderViewModel();
            }
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("Success");
        }
        public ActionResult OnGetReset()
        {
            CaptchaImage newCaptcha = SetCaptchaImage();

            return new JsonResult(new CaptchaModel
            {
                Captcha = "../shared/UserFiles/Folders/Captcha/" + newCaptcha.UniqueId + ".png",
                CaptchaID = newCaptcha.UniqueId
            });
        }

        public ActionResult OnGetValidate(CaptchaModel model)
        {
            string text = GetCaptchaText(model.CaptchaID);

            return new JsonResult(text == model.Captcha.ToUpperInvariant());
        }
        private void GenerateNewCaptcha()
        {
            CaptchaImage captchaImage = SetCaptchaImage();

            ViewData["Captcha"] = Url.Content("~/shared/UserFiles/Folders/Captcha/" + captchaImage.UniqueId + ".png");
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
