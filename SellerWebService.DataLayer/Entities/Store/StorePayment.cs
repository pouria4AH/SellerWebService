using System.Security.AccessControl;
using _0_framework.Entities;

namespace SellerWebService.DataLayer.Entities.Store
{
    public class StorePayment : BaseEntity
    {
        #region prop
        public long StoreDataId { get; set; }
        public DateTime PaymentDate { get; set; }
        [Display(Name = "کد پیگیری")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string TracingCode { get; set; }
        [Display(Name = "کد پیگیری")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        //public string Description { get; set; }
        public bool IsPayed { get; set; }
        public Guid StoreCode { get; set; }
        public Guid UserCode { get; set; }
        #endregion
        #region relation
        public StoreData StoreData { get; set; }
        #endregion
    }

}
