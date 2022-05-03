using _0_framework.Account;
using _0_framework.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Store;
using SellerWebService.WebApi.Extensions;

namespace SellerWebService.WebApi.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddStoreController : ControllerBase
    {
        #region ctor
        private readonly IStoreService _storeService;
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;
        public AddStoreController(IStoreService storeService, IPaymentService paymentService, IConfiguration configuration)
        {
            _storeService = storeService;
            _paymentService = paymentService;
            _configuration = configuration;
        }
        #endregion

        [HttpPost("register-store")]
        [Authorize(Roles = AccountRole.User)]
        public async Task<ActionResult<OperationResponse>> CreateStore([FromBody] RegisterStoreDto store)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _storeService.RegisterStore(store, User.GetUserUniqueCode());
                    switch (res)
                    {
                        case RegisterStoreResult.StoreIsExists:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Info,
                                ApplicationMessages.StoreIsExists, null));
                        case RegisterStoreResult.UserNotFound:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                                ApplicationMessages.UserNotFound, null));
                        case RegisterStoreResult.Error:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                                ApplicationMessages.Error, null));
                        case RegisterStoreResult.Success:
                            return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                                ApplicationMessages.Success, null));
                    }
                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                        ApplicationMessages.Error, null));
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                    ApplicationMessages.Error, null));
            }
            catch (Exception e)
            {
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                    ApplicationMessages.Error, null));
            }
        }

        [HttpGet("get-pay-gateway")]
        [Authorize(Roles = AccountRole.Seller)]
        public async Task<ActionResult> GetPayGateway()
        {
            var res = await _paymentService.Payment(2000, "خرید اشتراک", "https://localhost:7210/api/AddStore/validate-payment", _configuration);
            if (res.Authority == null)
                return BadRequest(res.Errors);
            return Ok($"https://sandbox.zarinpal.com/pg/StartPay/{res.Authority}");
        }

        [HttpGet("validate-payment")]
        public async Task<ActionResult<OperationResponse>> Validate([FromQuery] string authority, [FromQuery] string status)
        {
            var res = await _paymentService.Validate(2000, authority, status, _configuration);
            if (res.IsSuccess)
            {
                return Ok(res.RefId);
            }
            return BadRequest();
            
        }
    }
}
