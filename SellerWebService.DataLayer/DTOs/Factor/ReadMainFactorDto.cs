namespace SellerWebService.DataLayer.DTOs.Factor
{
    public class ReadMainFactorDto
    {
        [Display(Name = "کد محصول")]
        public Guid Code { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "نام فاکتور")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "قیمت محصول")]
        public long TotalPrice { get; set; }

        [Display(Name = "قیمت نهایی")]
        public long FinalPrice { get; set; }

        [Display(Name = "مجموع تخفیف")]
        public long TotalDiscount { get; set; }

        [Display(Name = "پیش پرداخت")]
        [Range(0, 100)]
        public int Prepayment { get; set; }

        [Display(Name = "تاریخ تحویل")]
        public int DeliveryDate { get; set; }

        [Display(Name = "مالیات")]
        [Range(0, 100)]
        public int taxation { get; set; }

        [Display(Name = "وضعیت فعلی فاکتور")]
        public string FactorStatus { get; set; }

        public List<CreateFactorDetailsDto> CreateFactorDetailsDtos { get; set; }

        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "تلفن همراه")]
        public string Mobile { get; set; }

        public string CustomerCode { get; set; }

    }
}
