namespace SellerWebService.DataLayer.DTOs.Customer
{
    public class ReadCustomerDto
    {
        [Display(Name = "ایمیل")]
        public string? Email { get; set; }

        [Display(Name = "تلفن همراه")]
        public string Mobile { get; set; }

        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string? CompanyName { get; set; }

        [Display(Name = "آدرس")]
        public string? Address { get; set; }

        [Display(Name = "کد پستی")]
        public string? ZipCode { get; set; }

        [Display(Name = "کد فروشگاه")]
        public Guid StoreCode { get; set; }

        [Display(Name = "کد ")]
        public Guid UniqueCode { get; set; }
    }
}
