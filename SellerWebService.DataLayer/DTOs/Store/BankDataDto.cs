namespace SellerWebService.DataLayer.DTOs.Store
{
    public class BankDataDto
    {     
        [Display(Name = "شماره حساب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string AccountNumber { get; set; }
        [Display(Name = "شماره کارت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CardNumber { get; set; }
        [Display(Name = "شماره شبا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ShabaNumber { get; set; }
        [Display(Name = "نام بانک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string BankName { get; set; }
        [Display(Name = "نام دارنده حساب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Owner { get; set; }
    }
}
