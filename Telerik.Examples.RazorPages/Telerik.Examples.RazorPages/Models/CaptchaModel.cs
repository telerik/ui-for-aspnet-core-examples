namespace Telerik.Examples.RazorPages.Models
{
    public class CaptchaModel
    {
        private string _captchaValue;
        public string Captcha
        {
            get
            {
                return string.IsNullOrEmpty(_captchaValue) ? "" : _captchaValue;
            }
            set
            {
                _captchaValue = value;
            }
        }
        public string CaptchaID { get; set; }
    }
}
