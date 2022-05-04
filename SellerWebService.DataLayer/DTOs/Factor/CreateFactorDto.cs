namespace SellerWebService.DataLayer.DTOs.Factor
{
    public class CreateFactorDto
    {

        public Guid CustomerCode { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "نام فاکتور")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "پیش پرداخت")]
        [Range(0, 100)]
        public int Prepayment { get; set; } = 100;

        [Display(Name = "تاریخ تحویل")]
        public int DeliveryDate { get; set; }

        [Display(Name = "مالیات")]
        [Range(0, 100)]
        public int taxation { get; set; }
    }

    public enum CreateFactorResult
    {
        Success,
        Error,
        IsAlreadyPublish,
        FactorNotFound
    }

}
