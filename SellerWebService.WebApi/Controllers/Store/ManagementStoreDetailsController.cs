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
    [Authorize(Roles = AccountRole.Seller)]
    public class ManagementStoreDetailsController : ControllerBase
    {
        #region ctor
        private readonly IStoreService _storeService;
        public ManagementStoreDetailsController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        #endregion

        [HttpGet("have-any-store-details")]
        public async Task<ActionResult> HaveStoreDetails()
        {
            var res = await _storeService.IsHaveStoreDetails(User.GetUserStoreCode());
            return Ok(res);
        }

        [HttpPost("create-store-details")]
        public async Task<ActionResult<OperationResponse>> EditStoreDerails([FromBody] CreateStoreDetailsDto storeDetails)
        {
            try
            {
                if(await _storeService.IsHaveStoreDetails(User.GetUserStoreCode()))
                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.StoreIsExists, null));

                if (ModelState.IsValid)
                {
                    var res = await _storeService.EditStoreDetails(storeDetails, User.GetUserStoreCode());
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

        [HttpPost("edit-store-details")]
        public async Task<ActionResult<OperationResponse>> CreateStoreDerails([FromBody] CreateStoreDetailsDto storeDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _storeService.CreateStoreDetails(storeDetails, User.GetUserStoreCode());
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

        [HttpPost("creaet-signature-image")]
        public async Task<ActionResult> CreateSignature(IFormFile image)
        {
            try
            {
                var res = await _storeService.CreateSignature(image, User.GetUserStoreCode());
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

        [HttpPost("edit-stamp-image")]
        public async Task<ActionResult> CreateStamp(IFormFile image)
        {
            try
            {
                var res = await _storeService.CreateStamp(image, User.GetUserStoreCode());
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

        [HttpPost("edit-signature-image")]
        public async Task<ActionResult> EditSignature(IFormFile image)
        {
            try
            {
                var res = await _storeService.EditSignature(image, User.GetUserStoreCode());
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

        [HttpPost("creaet-stamp-image")]
        public async Task<ActionResult> EditStamp(IFormFile image)
        {
            try
            {
                var res = await _storeService.EditStamp(image, User.GetUserStoreCode());
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
