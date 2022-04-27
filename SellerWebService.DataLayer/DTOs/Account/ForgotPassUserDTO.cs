using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerWebService.DataLayer.DTOs.Account
{
    public class ForgotPassUserDTO : CaptchaDto
    {
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Mobile { get; set; }
    }

    public enum ForgotPassUserResult
    {
        Success,
        NotFound,
        Error
    }
}
