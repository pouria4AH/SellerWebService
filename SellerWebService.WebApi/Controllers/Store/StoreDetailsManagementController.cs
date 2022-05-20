using _0_framework.Account;
using _0_framework.Http;
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
    [Authorize(Roles = AccountRole.Seller)]
    public class StoreDetailsManagementController : ControllerBase
    {
        #region ctor
        private readonly IStoreService _storeService;

        public StoreDetailsManagementController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        #endregion
        /// <summary>
        /// ایا اطلاعات تماس دارد
        /// </summary>
        /// <remarks>just return 200 and 400</remarks>
        /// <returns></returns>
        [HttpGet("have-any-store-details")]
        public async Task<ActionResult> HaveStoreDetails()
        {
            var res = await _storeService.IsHaveStoreDetails(User.GetStoreCode());
            if (res) return Ok();
            return BadRequest();
        }
        /// <summary>
        /// ساخت اطلاعات فروشگاه
        /// </summary>
        /// <param name="storeDetails"></param>
        /// <remarks>send create factor dto if is success return 200 code operation response by factor code and if not success return 400 by non data and 404 by not operation response </remarks>
        /// <response code="200">return 200 by factor code</response>
        /// <response code="400">return 400 by non data</response>
        [HttpPost("store-details")]
        public async Task<ActionResult<OperationResponse>> CreateStoreDerails([FromBody] CreateStoreDetailsDto storeDetails)
        {
            try
            {
                if (await _storeService.IsHaveStoreDetails(User.GetStoreCode()))
                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.StoreIsExists, null));

                if (ModelState.IsValid)
                {
                    var res = await _storeService.CreateStoreDetails(storeDetails, User.GetStoreCode());
                    switch (res)
                    {
                        case CreateStoreDetailsResult.StoreIsNull:
                            return NotFound();
                        case CreateStoreDetailsResult.Error:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
                        case CreateStoreDetailsResult.Success:
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
        /// <summary>
        /// ادیت اطلاعات فروشگاه
        /// </summary>
        /// <param name="storeDetails"></param>
        /// <remarks>send create factor dto if is success return 200 code operation response by factor code and if not success return 400 by non data and 404 by not operation response </remarks>
        /// <response code="200">return 200 by factor code</response>
        /// <response code="400">return 400 by non data</response>
        [HttpPut("store-details")]
        public async Task<ActionResult<OperationResponse>> EditStoreDerails([FromBody] CreateStoreDetailsDto storeDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _storeService.EditStoreDetails(storeDetails, User.GetStoreCode());
                    switch (res)
                    {
                        case CreateStoreDetailsResult.StoreIsNull:
                            return NotFound();
                        case CreateStoreDetailsResult.Error:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
                        case CreateStoreDetailsResult.Success:
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
        /// <summary>
        /// ساخت عکس مهر
        /// </summary>
        /// <param name="image">image file</param>
        /// <returns>just return 200 and 400 </returns>
        [HttpPost("signature-image")]
        public async Task<ActionResult> CreateSignature([FromForm] IFormFile image)
        {
            try
            {
                var res = await _storeService.CreateSignature(image, User.GetStoreCode());
                if (res)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// ساخت عکس لوگو
        /// </summary>
        /// <param name="image">image file</param>
        /// <returns>just return 200 and 400 </returns>
        [HttpPost("logo-image")]
        public async Task<ActionResult> CreateLogo([FromForm] IFormFile image)
        {
            try
            {
                var res = await _storeService.CreateLogo(image, User.GetStoreCode());
                if (res)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// ساخت عکس مهر
        /// </summary>
        /// <param name="image">image file</param>
        /// <returns>just return 200 and 400 </returns>
        [HttpPost("stamp-image")]
        public async Task<ActionResult> CreateStamp([FromForm] IFormFile image)
        {
            try
            {
                var res = await _storeService.CreateStamp(image, User.GetStoreCode());
                if (res)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// ادیت عکس امضا
        /// </summary>
        /// <param name="image">image file</param>
        /// <returns>just return 200 and 400 </returns>
        [HttpPut("signature-image")]
        public async Task<ActionResult> EditSignature([FromForm] IFormFile image)
        {
            try
            {
                var res = await _storeService.EditSignature(image, User.GetStoreCode());
                if (res)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// ادیت عکس مهر
        /// </summary>
        /// <param name="image">image file</param>
        /// <returns>just return 200 and 400 </returns>
        [HttpPut("stamp-image")]
        public async Task<ActionResult> EditStamp([FromForm] IFormFile image)
        {
            try
            {
                var res = await _storeService.EditStamp(image, User.GetStoreCode());
                if (res)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// ادیت عکس لوگو
        /// </summary>
        /// <param name="image">image file</param>
        /// <returns>just return 200 and 400 </returns>
        [HttpPut("logo-image")]
        public async Task<ActionResult> EditLogo([FromForm] IFormFile image)
        {
            try
            {
                var res = await _storeService.EditLogo(image, User.GetStoreCode());
                if (res)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

    }
}
