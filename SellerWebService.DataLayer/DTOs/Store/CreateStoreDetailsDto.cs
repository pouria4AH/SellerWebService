using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SellerWebService.DataLayer.DTOs.Store
{
    public class CreateStoreDetailsDto
    {
        [Display(Name = "ادرس اینستاگرام")]
        public string? Instagram { get; set; }

        [Display(Name = "شماره واتساپ")]
        public string? WhatsappNumber { get; set; }

        [Display(Name = "شماره تلگرام")]
        public string? TelegramNumber { get; set; }

        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Mobile { get; set; }

        [Display(Name = "شماره ثابت")]
        public string? Phone { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }

    public enum CreateStoreDetailsResult
    {
        Error,
        Success,
        StoreIsNull
    }
}
