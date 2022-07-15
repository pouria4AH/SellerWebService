using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerWebService.DataLayer.DTOs.Store
{
    public class ReadStoreDetailsDto
    {
        [Display(Name = "ادرس اینستاگرام")]
        public string? Instagram { get; set; }

        [Display(Name = "شماره واتساپ")]
        public string? WhatsappNumber { get; set; }

        [Display(Name = "شماره تلگرام")]
        public string? TelegramNumber { get; set; }

        [Display(Name = "شماره همراه")]
        public string Mobile { get; set; }

        [Display(Name = "شماره ثابت")]
        public string? Phone { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "آدرس")]
        public string? Address { get; set; }

        [Display(Name = "ایمیل")]
        public string? Email { get; set; }

        [Display(Name = "ادرس سایت")]
        public string? Website { get; set; }
        
        public string LogoImage { get; set; }
    }
}
