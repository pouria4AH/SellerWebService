using _0_framework.Account;
using _0_framework.Http;
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
        /// <summary>
        /// ساخت فاکتور خام
        /// </summary>
        /// <param name="factor"></param>
        /// <remarks>send create factor dto if is success return 200 code operation response by factor code and if not success return 400 by non data</remarks>
        /// <response code="200">return 200 by factor code</response>
        /// <response code="400">return 400 by non data</response>
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
        /// <summary>
        /// ساخت لیست محصولات در فاکتور
        /// </summary>
        /// <param name="factorDetails"></param>
        /// <param name="factorCode"></param>
        ///  <remarks>send list CreateFactorDetailsDto and factor code from route four create factor details
        /// if send again is not problem because before data has been delete
        /// </remarks>
        /// <response code="200">return 200 by non data</response>
        /// <response code="400">return 400 by non data</response>
        [HttpPost("factor-details/{factorCode}")]
        public async Task<ActionResult> CreateFactorDetails([FromBody] List<CreateFactorDetailsDto> factorDetails, [FromRoute] string factorCode)
        {
            var res = await _factorService.CreateFactorDetails(factorDetails, User.GetStoreCode(), Guid.Parse(factorCode));
            if (res) return Ok();
            return BadRequest();
        }

        /// <summary>
        /// ارسال بدنه اصلی فاکتور 
        /// </summary>
        ///  <remarks>send factor by ReadMainFactorDto if factor is null return empty array like [] and if have factor return ReadMainFactorDto by non http code
        /// </remarks>
        [HttpGet("factor/{factorCode}")]
        public async Task<ReadMainFactorDto> GetMainFactor([FromRoute] string factorCode)
        {
            return await _factorService.GetFinialFactorToConfirm(Guid.Parse(factorCode), User.GetStoreCode());
        }
        /// <summary>
        /// پابلیش کردن فاکتور
        /// </summary>
        /// <param name="factorCode"></param>
        /// <remarks>send factor code from route and return operation response by 400 our 200 code by non data</remarks>
        /// <response code="200">return 200 by non data</response>
        /// <response code="400">return 400 by non data</response>
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
        /// <summary>
        /// گرفتن فاکتور کامل 
        /// </summary>
        /// <param name="factorCode"></param>
        /// <remarks>return full factor if is ok return ReadFullFactorDto and if is empty return [] </remarks>
        [HttpGet("full-factor/{factorCode}")]
        public async Task<ReadFullFactorDto> GetFactor([FromRoute] string factorCode)
        {
            return await _factorService.GetFinialFactor(Guid.Parse(factorCode), User.GetStoreCode());
        }

        #endregion

        #region state
        /// <summary>
        /// رد کردن فاکتور
        /// </summary>
        /// <param name="factorCode">get factor code in url</param>
        /// <remarks>just return 200 as success and 400 fore fail</remarks>
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
        /// <summary>
        /// قبول کردن فاکتور
        /// </summary>
        /// <param name="factorCode">get factor code in url</param>
        /// <remarks>get accepted factor dto in body just return 200 as success and 400 fore fail</remarks>
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
        /// <summary>
        /// اماده کردن برای پرداخت نهایی
        /// </summary>
        /// <param name="factorCode">get factor code in url</param>
        /// <remarks>just return 200 as success and 400 fore fail</remarks>
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
        /// <summary>
        /// قبول پرداخت دوم فاکتور
        /// </summary>
        /// <param name="factorCode">get factor code in url</param>
        /// <remarks>get accepted factor dto but is optional in body just return 200 as success and 400 fore fail</remarks>
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
        /// <summary>
        /// ارسال شد
        /// </summary>
        /// <param name="factorCode">get factor code in url</param>
        /// <remarks>just return 200 as success and 400 fore fail</remarks>
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

