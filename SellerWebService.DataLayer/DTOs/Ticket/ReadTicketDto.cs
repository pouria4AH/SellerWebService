using SellerWebService.DataLayer.Entities.Tickets;

namespace SellerWebService.DataLayer.DTOs.Ticket
{
    public class ReadTicketDto
    {
        public Entities.Tickets.Ticket Ticket { get; set; }

        public string SectionName { get; set; }

        public string StateName { get; set; }

        public List<TicketsMessage> TicketsMessages { get; set; }
    }
}
