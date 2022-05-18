using Microsoft.EntityFrameworkCore;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Ticket;
using SellerWebService.DataLayer.Entities.Factor;
using SellerWebService.DataLayer.Entities.Store;
using SellerWebService.DataLayer.Entities.Tickets;
using SellerWebService.DataLayer.Repository;

namespace SellerWebService.Application.Implementations
{
    internal class TicketService : ITicketService
    {
        #region ctor
        private readonly IGenericRepository<Ticket> _ticketRepository;
        private readonly IGenericRepository<TicketsMessage> _ticketsMessageRepository;
        private readonly IGenericRepository<Factor> _factorRepository;
        private readonly IGenericRepository<StoreData> _storeDataRepository;
        public TicketService(IGenericRepository<Ticket> ticketRepository, IGenericRepository<TicketsMessage> ticketsMessageRepository, IGenericRepository<Factor> factorRepository, IGenericRepository<StoreData> storeDataRepository)
        {
            _ticketRepository = ticketRepository;
            _ticketsMessageRepository = ticketsMessageRepository;
            _factorRepository = factorRepository;
            _storeDataRepository = storeDataRepository;
        }
        #endregion

        public async Task<bool> SendFromCustomer(CreateTicketFromCustomerDto ticket)
        {
            try
            {
                var factor = await _factorRepository.GetQuery()
                    .Include(x=>x.Customer)
                    .AsQueryable()
                    .SingleOrDefaultAsync(x => !x.IsDelete && x.Code == ticket.FactorCode);
                if (factor == null) return false;
                var newTicket = new Ticket
                {
                    FactorCode = factor.Code,
                    IsForService = false,
                    IsReadByOwner = false,
                    IsReadByaSender = true,
                    TicketSection = TicketSection.Customer,
                    StoreDataId = factor.Customer.StoreDataId,
                    Title = ticket.Title,
                };
                await _ticketRepository.AddEntity(newTicket);
                await _ticketRepository.SaveChanges();
                var newMessage = new TicketsMessage
                {
                    Text = ticket.Text,
                    TicketId = newTicket.Id
                };
                await _ticketsMessageRepository.AddEntity(newMessage);
                await _ticketsMessageRepository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #region dispose
        public async ValueTask DisposeAsync()
        {
            if (_factorRepository != null) await _factorRepository.DisposeAsync();
            if (_ticketRepository != null) await _ticketRepository.DisposeAsync();
            if (_ticketsMessageRepository != null) await _ticketsMessageRepository.DisposeAsync();
        }
        #endregion
    }
}
