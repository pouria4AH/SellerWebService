using SellerWebService.DataLayer.DTOs.Ticket;

namespace SellerWebService.Application.interfaces
{
    internal interface ITicketService : IAsyncDisposable
    {
        Task<bool> SendFromCustomer(CreateTicketFromCustomerDto ticket);
    }
}
