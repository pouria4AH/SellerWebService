using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs;
using SellerWebService.DataLayer.DTOs.Products;

namespace SellerWebService.WebApi.Controllers.Admin.Products
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProductFeatureController : ControllerBase
    {
        #region ctor
        private readonly IProductService _productService;
        public ProductFeatureController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        [HttpGet("get-all-groups-for-product-{id}")]
        public async Task<List<ReadGroupProductFeatureDto>> GetAllGroups(long id)
        {
            return await _productService.GetGroupsForProduct(id);
        }

        [HttpPost("create-group-of-product-feature")]
        public async Task<ActionResult<OperationResponse>> CreateGroupOfProductFeature(
            [FromBody] CreateGroupProductFeatureDto group)
        {
            if (ModelState.IsValid)
            {
                var res = await _productService.CreateGroupOfProductFeature(group);
                switch (res)
                {
                    case CreateGroupProductFeatureResult.IsExisted:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "همچین دسته قبلا وجود دارد", null));
                    case CreateGroupProductFeatureResult.Error:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "همچین ترتیبی  قبلا وجود دارد", null));
                    case CreateGroupProductFeatureResult.OrderExisted:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "مشکی پیش امده دوباره تلاش کنید", null));
                    case CreateGroupProductFeatureResult.Success:
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            "عملیات موقق امیز بود", group));

                }
            }
            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                "مشکی پیش امده دوباره تلاش کنید", null));
        }

        [HttpDelete("delete-group-of-product-feature")]
        public async Task<ActionResult<OperationResponse>> DeleteGroupOfProductFeature(long groupId)
        {
            var res = await _productService.DeleteGroup(groupId);
            if (res)
                return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                    "عملیات موقق امیز بود", null));

            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                "مشکی پیش امده دوباره تلاش کنید", null));
        }

        [HttpPost("edit-product-feature")]
        public async Task<ActionResult<OperationResponse>> CreateProductFeature(EditProductFeatureDto feature)
        {
            if (ModelState.IsValid)
            {
                var res = await _productService.EditProductFeature(feature);
                switch (res)
                {
                    case CreateOrEditProductFeatureResult.Error:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "مشکی پیش امده دوباره تلاش کنید", null));
                    case CreateOrEditProductFeatureResult.Success:
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            "عملیات موفق امیز بود", feature));
                }
            }
            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                "مشکی پیش امده دوباره تلاش کنید", null));
        }  [HttpPost("create-product-feature")]
        public async Task<ActionResult<OperationResponse>> CreateProductFeature(CreateProductFeatureDto feature)
        {
            if (ModelState.IsValid)
            {
                var res = await _productService.CreateProductFeature(feature);
                switch (res)
                {
                    case CreateOrEditProductFeatureResult.Error:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "مشکی پیش امده دوباره تلاش کنید", null));
                    case CreateOrEditProductFeatureResult.Success:
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            "عملیات موفق امیز بود", feature));
                }
            }
            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                "مشکی پیش امده دوباره تلاش کنید", null));
        }

    }
}
