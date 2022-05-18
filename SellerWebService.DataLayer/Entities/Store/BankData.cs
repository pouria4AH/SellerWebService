using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_framework.Entities;

namespace SellerWebService.DataLayer.Entities.Store
{
    public class BankData : BaseEntity
    {
        #region prop

        public long StoreDataId { get; set; }
        [Display(Name = "شماره حساب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string AccountNumber { get; set; }
        [Display(Name = "شماره کارت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string CardNumber { get; set; }
        [Display(Name = "شماره شبا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ShabaNumber { get; set; }
        [Display(Name = "نام بانک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(60, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string BankName { get; set; }
        [Display(Name = "نام صاحب حساب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(60, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Owner { get; set; }
        public Guid StoreCode { get; set; }
        #endregion

        #region relations

        public StoreData StoreData { get; set; }
        #endregion

    }
}
