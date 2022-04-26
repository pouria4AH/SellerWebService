namespace SellerWebService.DataLayer.DTOs.Account
{
    public class CaptchaDto
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Captcha { get; set; }
    }
}
