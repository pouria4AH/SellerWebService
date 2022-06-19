using System.Security.Principal;
using _0_framework.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Ticket;
using SellerWebService.WebApi.Extensions;

namespace SellerWebService.WebApi.Controllers.Ticket
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("customer")]
        [AllowAnonymous]
        public async Task<ActionResult> SendFromCustomer([FromBody] CreateCustomerTicketDto ticket)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _ticketService.SendFromCustomer(ticket);
                    if (res) return Ok();
                    return BadRequest();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();

            }
        }

        [HttpPost("seller")]
        [Authorize(Roles = AccountRole.Seller + "," + AccountRole.SellerEmployee)]
        public async Task<ActionResult> SendFromSeller([FromBody] CreateTicketDto ticket)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _ticketService.SendFromSeller(ticket, IdentityExtensions.GetStoreCode((IPrincipal)User));
                    if (res) return Ok();
                    return BadRequest();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //[HttpPost("admin")]
        //[Authorize(Roles = AccountRole.SysAdmin)]
        //public async Task<ActionResult> AnswerTicket([FromBody] AnswerTicketDto answer)
        //{
        //   try
        //   {
        //       var res = await _ticketService.AnswerTicket(answer);
        //       if (res) return Ok();
        //       return BadRequest();
        //   }
        //   catch (Exception e)
        //   {
        //       return BadRequest();
        //   }

        //}

        //[HttpGet("seller/{ticketId}")]
        //[Authorize(Roles = AccountRole.Seller + "," + AccountRole.SellerEmployee)]
        //public async Task<ActionResult<ReadTicketDto>> GetTicketForSeller([FromRoute]long ticketId)
        //{
        //    try
        //    {
        //        var res = await _ticketService.GetTicketStoreForRead(ticketId,User.GetStoreCode());
        //        if(res != null) return Ok(res);
        //        return BadRequest();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest();
        //    }
        //}

    }
}
