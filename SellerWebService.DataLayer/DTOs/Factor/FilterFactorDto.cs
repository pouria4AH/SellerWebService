using SellerWebService.DataLayer.DTOs.Paging;
using SellerWebService.DataLayer.Entities.Factor;

namespace SellerWebService.DataLayer.DTOs.Factor
{
    public class FilterFactorDto : BasePaging
    {
        [Display(Name = "نام فاکتور")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? Name { get; set; }

        [Display(Name = "کمترین قیمت نهایی  ")]
        [Range(0, long.MaxValue)]
        public long? MinFinalPrice { get; set; }

        [Display(Name = " بیشترین قیمت نهایی")]
        [Range(0, long.MaxValue)]
        public long?  MaxFinalPrice { get; set; } 

        [Display(Name = "پیش پرداخت")]
        [Range(0, 100)]
        public int? Prepayment { get; set; } = 100;

        public Guid? CustomerCode { get; set; }
        public FilterFactorStatus FilterFactorStatus { get; set; }
        public FilterFactorPaymentState FirstFilterFactorPaymentState { get; set; }
        public FilterFactorPaymentState FinalFirstFilterFactorPaymentState { get; set; }
        public FilterFactorOrder FilterFactorOrder { get; set; }
        public List<Entities.Factor.Factor>? Factors { get; private set; }
        public FilterFactorDto SetProduct(List<Entities.Factor.Factor> factors)
        {
            this.Factors = factors;
            return this;
        }

        public FilterFactorDto SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.TakeEntities = paging.TakeEntities;
            this.SkipEntities = paging.SkipEntities;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            this.AllEntitiesCount = paging.AllEntitiesCount;
            this.PageCount = paging.PageCount;
            return this;
        }
    }

    public enum FilterFactorOrder
    {
        [Display(Name = "نزولی تاریخ")]
        CreateDate_Des,
        [Display(Name = "صعودی تاریخ")]
        CreateDate_Aec,
        [Display(Name = "نزولی قیمت")]
        Price_Des,
        [Display(Name = "صعودی قیمت")]
        Price_Asc
    }
    public enum FilterFactorStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "انتظار")]
        Waiting,
        [Display(Name = "رد شده")]
        Reject,
        [Display(Name = "قبول شده")]
        Accepted,
        [Display(Name = "آماده برای پرداخت دوم")]
        ReadyToFinalPayed,
        [Display(Name = "آماده تحویل")]
        Ready,
        [Display(Name = "تحویل داده شده")]
        Delivered,
        [Display(Name = "تاریخ گذشته")]
        Expired,
        [Display(Name = "در حال ثبت")]
        Open,
        
    }
    public enum FilterFactorPaymentState
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "درگاه بانکی")]
        Portal,
        [Display(Name = "کارت به کارت")]
        BankCreditCard,
        [Display(Name = "چک")]
        BankCheck
    }

}
