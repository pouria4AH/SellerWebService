using Microsoft.EntityFrameworkCore;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Ticket;
using SellerWebService.DataLayer.Entities.Factor;
using SellerWebService.DataLayer.Entities.Store;
using SellerWebService.DataLayer.Entities.Tickets;
using SellerWebService.DataLayer.Repository;

namespace SellerWebService.Application.Implementations
{
    public class TicketService : ITicketService
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

        public async Task<bool> SendFromCustomer(CreateCustomerTicketDto customerTicket)
        {
            try
            {
                var factor = await _factorRepository.GetQuery()
                    .Include(x => x.Customer)
                    .AsQueryable()
                    .SingleOrDefaultAsync(x => !x.IsDelete && x.Code == customerTicket.FactorCode);
                if (factor == null) return false;
                var newTicket = new Ticket
                {
                    FactorCode = factor.Code,
                    IsForService = false,
                    IsReadByOwner = false,
                    IsReadByaSender = true,
                    TicketSection = TicketSection.Customer,
                    TicketState = TicketState.OneWay,
                    StoreDataId = factor.Customer.StoreDataId,
                    Title = customerTicket.Title,
                };
                await _ticketRepository.AddEntity(newTicket);
                await _ticketRepository.SaveChanges();
                var newMessage = new TicketsMessage
                {
                    Text = customerTicket.Text,
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

        public async Task<bool> SendFromSeller(CreateTicketDto ticket, Guid storeCode)
        {
            try
            {
                var store = await _storeDataRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => x.IsActive && !x.IsDelete && x.UniqueCode == storeCode);
                if (store == null) return false;
                var newTicket = new Ticket
                {
                    IsForService = true,
                    IsReadByOwner = false,
                    IsReadByaSender = true,
                    StoreDataId = store.Id,
                    TicketSection = TicketSection.Support,
                    TicketState = TicketState.UnderProcess,
                    Title = ticket.Title
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

        public async Task<bool> AnswerTicket(string text, long ticketId)
        {
            try
            {
                var mainTicket = await _ticketRepository.GetEntityById(ticketId);
                if (mainTicket == null) return false;
                var newMessage = new TicketsMessage
                {
                    TicketId = mainTicket.Id,
                    Text = text
                };
                mainTicket.IsReadByaSender = false;
                mainTicket.IsReadByOwner = true;
                mainTicket.TicketState = TicketState.Answered;
                _ticketRepository.EditEntity(mainTicket);
                await _ticketRepository.SaveChanges();
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
