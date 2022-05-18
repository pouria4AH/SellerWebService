using _0_framework.Entities;

namespace SellerWebService.DataLayer.Entities.Tickets
{
    public class TicketsMessage : BaseEntity
    {
        #region props

        public long TicketId { get; set; }
        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Text { get; set; }

        #endregion

        #region relations

        public Ticket Ticket { get; set; }
        #endregion
    }
}
