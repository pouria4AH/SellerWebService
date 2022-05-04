using _0_framework.Entities;
using SellerWebService.DataLayer.Entities.Store;

namespace SellerWebService.DataLayer.Entities.Account
{
    public class Customer : BaseEntity
    {
        #region prop

        public long StoreDataId { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Mobile { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string LastName { get; set; }
        
        [Display(Name = "نام خانوادگی")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? CompanyName { get; set; }

        [Display(Name = "آدرس")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? Address { get; set; }

        [Display(Name = "کد پستی")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? ZipCode { get; set; }

        [Display(Name = "کد فروشگاه")]
        public Guid StoreCode { get; set; }

        [Display(Name = "کد ")]
        public Guid UniqueCode { get; set; }

        #endregion

        #region relations

        public ICollection<Factor.Factor> Factors { get; set; }
        public StoreData StoreData { get; set; }

        #endregion
    }
}
