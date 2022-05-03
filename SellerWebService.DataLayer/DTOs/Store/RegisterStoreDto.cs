namespace SellerWebService.DataLayer.DTOs.Store
{
    public class RegisterStoreDto
    {
        [Display(Name = "نام خانوادگی")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CompanyName { get; set; }

        [Display(Name = "آدرس")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? Address { get; set; }

        [Display(Name = "کد پستی")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? ZipCode { get; set; }

        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PersonalId { get; set; }
    }

    public enum RegisterStoreResult
    {
        Error,
        Success,
        UserNotFound,
        PersonalIdExists,
    }
}
