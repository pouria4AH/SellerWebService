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
    [Authorize(Roles = AccountRole.SellerEmployee+" "+ AccountRole.Seller)]
    public class FeatureCategoryManagementController : ControllerBase
    {

        #region ctor

        private readonly IProductService _productService;

        public FeatureCategoryManagementController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        /// <summary>
        /// گرفتن کل لیست دسته ویژگی هر فروشگاه
        /// </summary>
        /// <remarks>return list of EditProductFeatureCategoryDto if is have category or return 400 by no data </remarks>
        /// <response code="200">return 200 list of EditProductFeatureCategoryDto </response>
        /// <response code="400">return 400 by non data</response>
        [HttpGet]
        public async Task<ActionResult<EditProductFeatureCategoryDto>> GetCategories()
        {
            var res = await _productService.GetProductFeatureCategories(User.GetStoreCode());
            {
                if (res == null || !res.Any()) return BadRequest();
                return Ok(res);
            }
        }
        /// <summary>
        /// ساخت دسته محصول
        /// </summary>
        /// <remarks>get a CreateProductFeatureCategoryDto and create a category</remarks>
        /// <param name="category">CreateProductFeatureCategoryDto</param>
        /// <response code="200">return 200 by operation response and non data</response>
        /// <response code="400">return 400 by operation response and non data</response>
        [HttpPost]
        public async Task<ActionResult<OperationResponse>> CreateCategories([FromBody] CreateProductFeatureCategoryDto category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _productService.CreateFeatureCategory(category, User.GetStoreCode());
                    switch (res)
                    {
                        case CreateOurEditProductFeatureCategoryResult.IsExisted:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.IsExist, null));
                        case CreateOurEditProductFeatureCategoryResult.Error:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
                        case CreateOurEditProductFeatureCategoryResult.Success:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));

                    }
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
            }
            catch (Exception e)
            {
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));

            }
        }
        /// <summary>
        /// ادیت گروه محصول
        /// </summary>
        /// <remarks>get a EditProductFeatureCategoryDto and create a category</remarks>
        /// <param name="category">EditProductFeatureCategoryDto</param>
        /// <response code="200">return 200 by operation response and non data</response>
        /// <response code="400">return 400 by operation response and non data</response>
        [HttpPut]
        public async Task<ActionResult> EditCategories([FromBody] EditProductFeatureCategoryDto category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _productService.EditFeatureCategory(category, User.GetStoreCode());
                    switch (res)
                    {
                        case CreateOurEditProductFeatureCategoryResult.IsExisted:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.IsExist, null));
                        case CreateOurEditProductFeatureCategoryResult.Error:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
                        case CreateOurEditProductFeatureCategoryResult.NotFound:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning, ApplicationMessages.NotFound, null));
                        case CreateOurEditProductFeatureCategoryResult.Success:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));

                    }
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
            }
            catch (Exception e)
            {
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));

            }
        }
    }
}
