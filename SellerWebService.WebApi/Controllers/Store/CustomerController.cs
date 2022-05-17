using _0_framework.Account;
using _0_framework.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Customer;
using SellerWebService.WebApi.Extensions;

namespace SellerWebService.WebApi.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AccountRole.Seller)]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse>> CreateCustomer(CreateCustomerDto customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _customerService.CreateCustomer(customer, User.GetStoreCode());
                    switch (res)
                    {
                        case CreateOurEditCustomerResult.IsExist:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning,
                                ApplicationMessages.IsExist, null));
                        case CreateOurEditCustomerResult.Error:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
                        case CreateOurEditCustomerResult.Success:
                            return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));
                    }
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.ModelIsNotValid, null));
            }
            catch (Exception e)
            {
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
            }
        }

        [HttpPut]
        public async Task<ActionResult<OperationResponse>> EditCustomer(EditCustomerDto customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _customerService.EditCustomer(customer, User.GetStoreCode());
                    switch (res)
                    {
                        case CreateOurEditCustomerResult.NotFound:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning,
                                ApplicationMessages.NotFound, null));
                        case CreateOurEditCustomerResult.Error:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
                        case CreateOurEditCustomerResult.Success:
                            return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));
                    }
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.ModelIsNotValid, null));
            }
            catch (Exception e)
            {
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
            }
        }

        [HttpDelete("/{customerCode}")]
        public async Task<ActionResult> DeleteCustomer(string customerCode)
        {
            try
            {
                var code = Guid.Parse(customerCode);
                var res = await _customerService.DeleteCustomer(code, User.GetStoreCode());
                if (res) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
        }

        [HttpGet("{customerCode}")]
        public async Task<ActionResult<ReadCustomerDto>> GetCustomer(string customerCode)
        {
            var customer = await _customerService.GetCustomer(Guid.Parse(customerCode), User.GetStoreCode());
            if (customer == null) return NotFound();
            return Ok(customer);
        }
    }
}
