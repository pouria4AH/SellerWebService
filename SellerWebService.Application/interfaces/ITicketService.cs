using SellerWebService.DataLayer.DTOs.Ticket;

namespace SellerWebService.Application.interfaces
{
    public interface ITicketService : IAsyncDisposable
    {
        Task<bool> SendFromCustomer(CreateCustomerTicketDto customerTicket);
        Task<bool> SendFromSeller(CreateTicketDto ticket, Guid storeCode);
        Task<bool> AnswerTicket(string text, long ticketId);
    }
}
