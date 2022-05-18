namespace SellerWebService.DataLayer.DTOs.Ticket
{
    public class CreateCustomerTicketDto : CreateTicketDto
    {
        public Guid FactorCode { get; set; }
      
    }
}
