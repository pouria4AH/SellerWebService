using _0_framework.Entities;
using SellerWebService.DataLayer.Entities.Store;

namespace SellerWebService.DataLayer.Entities.Tickets
{
    public class Ticket : BaseEntity
    {
        #region props

        public long? StoreDataId { get; set; }

        [Display(Name = "برای سرویس است")]
        public bool IsForService { get; set; }
        public Guid? FactorCode { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "خواننده شده توسط مالک")]
        public bool IsReadByOwner { get; set; }

        [Display(Name = " خوانده شده توسط ارسال کننده")]
        public bool IsReadByaSender { get; set; }

        [Display(Name = "بخش مورد نظر")]
        public TicketSection TicketSection { get; set; }

        [Display(Name = "وضعیت")]
        public TicketState TicketState { get; set; }


        #endregion

        #region relations
        public StoreData StoreData { get; set; }
        public ICollection<TicketsMessage> TicketsMessages { get; set; }
        #endregion
    }
    public enum TicketSection
    {
        [Display(Name = "پشتیبانی")]
        Support,
        [Display(Name = " مشتری")]
        Customer,
    }
    public enum TicketState
    {
        [Display(Name = "در حال برسی")]
        UnderProcess,
        [Display(Name = "پاسخ داده شده")]
        Answered,
        [Display(Name = "یک طرفه")]
        OneWay
    }
}
