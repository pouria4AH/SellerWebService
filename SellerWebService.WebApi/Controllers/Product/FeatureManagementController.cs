using _0_framework.Account;
using _0_framework.Http;
using _0_framework.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Products;
using SellerWebService.WebApi.Extensions;

namespace SellerWebService.WebApi.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AccountRole.Seller + " " + AccountRole.SellerEmployee)]
    public class FeatureManagementController : ControllerBase
    {
        #region ctor
        private readonly IProductService _productService;

        public FeatureManagementController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        [HttpPost("group")]
        public async Task<ActionResult<OperationResponse>> CreateGroup([FromBody] CreateGroupProductFeatureDto group)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _productService.CreateGroupOfProductFeature(group, User.GetStoreCode());
                    switch (res)
                    {
                        case CreateGroupProductFeatureResult.Error:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
                        case CreateGroupProductFeatureResult.IsExisted:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning, ApplicationMessages.IsExist, null));
                        case CreateGroupProductFeatureResult.OrderExisted:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.OrderIsExist, null));
                        case CreateGroupProductFeatureResult.Success:
                            return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));

                    }
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
            }
            catch (Exception e)
            {
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
            }
        }

        [HttpDelete("group/{id}")]
        public async Task<ActionResult> DeleteGroup([FromRoute] long id)
        {
            try
            {
                var res = await _productService.DeleteGroup(id, User.GetStoreCode());
                if (res) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("feature")]
        public async Task<ActionResult<OperationResponse>> CreateFeature([FromBody] CreateProductFeatureDto feature)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _productService.CreateProductFeature(feature, User.GetStoreCode());
                    switch (res)
                    {
                        case CreateOrEditProductFeatureResult.Error:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
                        case CreateOrEditProductFeatureResult.NotFound:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.StoreNotFound, null));
                        case CreateOrEditProductFeatureResult.Success:
                            return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));
                    }
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));

            }
            catch (Exception e)
            {
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
            }
        }

        [HttpPut("feature")]
        public async Task<ActionResult<OperationResponse>> EditFeature([FromBody] EditProductFeatureDto feature)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _productService.EditProductFeature(feature, User.GetStoreCode());
                    switch (res)
                    {
                        case CreateOrEditProductFeatureResult.Error:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
                        case CreateOrEditProductFeatureResult.NotFound:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.NotFound, null));
                        case CreateOrEditProductFeatureResult.Success:
                            return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));
                    }
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));

            }
            catch (Exception e)
            {
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
            }
        }

        [HttpGet("group/{id}")]
        public async Task<ActionResult<List<ReadGroupProductFeatureDto>>> GetAllGroup([FromRoute] long id)
        {
            try
            {
                var res = await _productService.GetGroupsForProduct(id);
                if (res == null || !res.Any()) return BadRequest();
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
    }
}
