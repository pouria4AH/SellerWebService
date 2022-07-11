using _0_framework.Entities;

namespace SellerWebService.DataLayer.Entities.Store
{
    public class StoreDetails : BaseEntity
    {
        #region prop

        public long StoreDataId { get; set; }

        [Display(Name = "ادرس اینستاگرام")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? Instagram { get; set; }

        [Display(Name = "شماره واتساپ")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? WhatsappNumber { get; set; }

        [Display(Name = "شماره تلگرام")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? TelegramNumber { get; set; }

        [Display(Name = "شماره همراه")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Mobile { get; set; }

        [Display(Name = "شماره ثابت")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? Phone { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "نام تصویر امضا")]
        public string? SigntureImage { get; set; }

        [Display(Name = "نام تصویر مهر")]
        public string? StampImage { get; set; }

        [Display(Name = "نام تصویر لوگو")]
        public string? LogoImage { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Display(Name = "ادرس سایت")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [DataType(DataType.Url)]
        public string? Website { get; set; }
        [Display(Name = "آدرس")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? Address { get; set; }

        [Display(Name = "کد فروشگاه")]
        public Guid StoreCode { get; set; }

        public bool IsActive { get; set; } = false;
        #endregion

        #region relations
        public StoreData StoreData { get; set; }
        #endregion
    }
}
