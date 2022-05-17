using _0_framework.Account;
using _0_framework.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Factor;
using SellerWebService.WebApi.Extensions;

namespace SellerWebService.WebApi.Controllers.Factor
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AccountRole.Seller + "," + AccountRole.SellerEmployee)]
    public class ManagementFactorController : ControllerBase
    {
        #region ctor

        private readonly IFactorService _factorService;

        public ManagementFactorController(IFactorService factorService)
        {
            _factorService = factorService;
        }

        #endregion

        #region create factore
        [HttpPost("blank-factor")]
        public async Task<ActionResult<OperationResponse>> CreateBlankFactor([FromBody] CreateFactorDto factor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var code = await _factorService.CreateBlankFactor(factor, User.GetStoreCode());
                    if (code == Guid.Empty)
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            ApplicationMessages.Error, null));

                    return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                        ApplicationMessages.Success, new
                        {
                            factorCode = code
                        }));
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                    ApplicationMessages.Error, null));
            }
            catch (Exception e)
            {
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));

            }
        }

        [HttpPost("factor-details/{factorCode}")]
        public async Task<ActionResult> CreateFactorDetails([FromBody] List<CreateFactorDetailsDto> factorDetails, [FromRoute] string factorCode)
        {
            var res = await _factorService.CreateFactorDetails(factorDetails, User.GetStoreCode(), Guid.Parse(factorCode));
            if (res) return Ok();
            return BadRequest();
        }

        [HttpGet("factor/{factorCode}")]
        public async Task<ReadMainFactorDto> GetMainFactor([FromRoute] string factorCode)
        {
            return await _factorService.GetFinialFactorToConfirm(Guid.Parse(factorCode), User.GetStoreCode());
        }

        [HttpGet("publish-factor/{factorCode}")]
        public async Task<ActionResult<OperationResponse>> PublishFactor([FromRoute] string factorCode)
        {
            try
            {

                var res = await _factorService.PublishFactor(Guid.Parse(factorCode), User.GetStoreCode());
                switch (res)
                {
                    case CreateFactorResult.FactorNotFound:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            ApplicationMessages.NotFound, null));
                    case CreateFactorResult.IsAlreadyPublish:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning,
                            ApplicationMessages.IsExist, null));
                    case CreateFactorResult.Error:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
                    case CreateFactorResult.Success:
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.ModelIsNotValid, null));

            }
            catch (Exception e)
            {
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
            }
        }

        [HttpGet("full-factor/{factorCode}")]
        public async Task<ReadFullFactorDto> GetFactor([FromRoute] string factorCode)
        {
            return await _factorService.GetFinialFactor(Guid.Parse(factorCode), User.GetStoreCode());
        }

        #endregion

        #region state
        [HttpGet("reject/{factorCode}")]
        public async Task<ActionResult> RejectFactor([FromRoute] string factorCode)
        {
            try
            {
                var res = await _factorService.RejectFactor(Guid.Parse(factorCode), User.GetStoreCode());
                if (res) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("accepted/{factorCode}")]
        public async Task<ActionResult> AcceptedFactor([FromRoute] string factorCode, [FromBody] AcceptedFactorDto accepted)
        {
            try
            {
                var res = await _factorService.AcceptedFactor(accepted, Guid.Parse(factorCode),
                    User.GetUserStoreCode());
                if (res) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("ready-to-final/{factorCode}")]
        public async Task<ActionResult> ReadyToFinalPay([FromRoute] string factorCode)
        {
            try
            {
                var res = await _factorService.ReadyToFinalPayedFactor(Guid.Parse(factorCode), User.GetUserStoreCode());
                if (res) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("ready/{factorCode}")]
        public async Task<ActionResult> Ready([FromRoute] string factorCode, [FromBody] AcceptedFactorDto? accepted)
        {
            try
            {
                var res = await _factorService.ReadyFactor(accepted, Guid.Parse(factorCode), User.GetUserStoreCode());
                if (res) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("delivered/{factorCode}")]
        public async Task<ActionResult> Delivered([FromRoute] string factorCode)
        {
            try
            {
                var res = await _factorService.DeliveredFactor(Guid.Parse(factorCode), User.GetUserStoreCode());
                if (res) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        #endregion

    }
}

