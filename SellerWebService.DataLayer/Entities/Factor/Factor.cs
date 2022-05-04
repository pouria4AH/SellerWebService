using System.Security.AccessControl;
using _0_framework.Entities;
using SellerWebService.DataLayer.Entities.Account;
using SellerWebService.DataLayer.Entities.Products;
using SellerWebService.DataLayer.Entities.Store;

namespace SellerWebService.DataLayer.Entities.Factor
{
    public class Factor : BaseEntity
    {
        #region prop
        public long CustomerId { get; set; }

        [Display(Name = "کد محصول")]
        public Guid Code { get; set; }

        [Display(Name = "تاریخ پرداخت اول")]
        public DateTime? FirstPaymentDate { get; set; }

        [Display(Name = "تاریخ پرداخت دوم")]
        public DateTime? FinalPaymentDate { get; set; }

        [Display(Name = "پرداخت اول")]
        public bool IsFirstPaid { get; set; }

        [Display(Name = "پرداخت دوم")]
        public bool IsFinalPaid { get; set; }

        [Display(Name = "کد پیگیری اول")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? FirstTracingCode { get; set; }

        [Display(Name = "کد پیگیری دوم")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? FinalTracingCode { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "نام فاکتور")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "مجموع قیمت ")]
        [Range(0, long.MaxValue)]
        public long TotalPrice { get; set; } = 0; 
        
        [Display(Name = "مجموع تخفیف")]
        [Range(0, long.MaxValue)]
        public long TotalDiscount { get; set; } = 0;

        [Display(Name = "قیمت نهایی")]
        [Range(0, long.MaxValue)]
        public long FinalPrice { get; set; } = 0;

        [Display(Name = "پیش پرداخت")]
        [Range(0, 100)]
        public int Prepayment { get; set; } = 100;

        [Display(Name = "تاریخ تحویل")]
        public int DeliveryDate { get; set; }

        [Display(Name = "مالیات")]
        [Range(0,100)]
        public int taxation { get; set; } = 0;

        public Guid StoreCode { get; set; }

        [Display(Name = "وضعیت فعلی فاکتور")]
        public FactorStatus FactorStatus { get; set; }

        public FactorPaymentState FirstFactorPaymentState { get; set; }
        public FactorPaymentState FinalFactorPaymentState { get; set; }
        #endregion
        #region relations
        public Customer Customer { get; set; }
        public ICollection<FactorDetails> FactorDetails { get; set; }
        
        #endregion
    }

    public enum FactorStatus
    {

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
        Open
    }

    public enum FactorPaymentState
    {
        [Display(Name = "درگاه بانکی")]
        Portal,
        [Display(Name = "کارت به کارت")]
        BankCreditCard,
        [Display(Name = "چک")]
        BankCheck
    }
}
