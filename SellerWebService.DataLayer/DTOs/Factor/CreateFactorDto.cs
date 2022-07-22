namespace SellerWebService.DataLayer.DTOs.Factor
{
    public class CreateFactorDto
    {
        [Display(Name = "کد مشتری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Guid CustomerCode { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "نام فاکتور")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "پیش پرداخت")]
        [Range(0, 100)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Prepayment { get; set; } = 100;

        [Display(Name = "تاریخ تحویل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(0,int.MaxValue)]
        public int DeliveryDate { get; set; }

        [Display(Name = "مالیات")]
        [Range(0, 100)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int taxation { get; set; }

        [Display(Name ="چند روز تا انقضا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(0, int.MaxValue)]
        public int longDayToExpired { get; set; }
    }

    public enum CreateFactorResult
    {
        Success,
        Error,
        IsAlreadyPublish,
        FactorNotFound
    }

}
