namespace SellerWebService.DataLayer.DTOs.Ticket
{
    public class CreateTicketFromCustomerDto
    {
        public Guid FactorCode { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
