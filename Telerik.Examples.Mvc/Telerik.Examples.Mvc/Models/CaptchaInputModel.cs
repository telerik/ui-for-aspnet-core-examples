namespace Telerik.Examples.Mvc.Models
{
    public class CaptchaInputModel
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
