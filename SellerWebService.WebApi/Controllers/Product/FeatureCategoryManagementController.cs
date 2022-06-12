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

        //#region ctor

        //private readonly IProductService _productService;

        //public FeatureCategoryManagementController(IProductService productService)
        //{
        //    _productService = productService;
        //}
        //#endregion

        //[HttpGet]
        //public async Task<ActionResult> GetCategories()
        //{
        //    var res = await _productService.GetProductFeatureCategories(User.GetStoreCode());
        //    {
        //        if (res == null || !res.Any()) return BadRequest();
        //        return Ok(res);
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult> CreateCategories([FromBody] CreateProductFeatureCategoryDto category)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var res = await _productService.CreateFeatureCategory(category, User.GetStoreCode());
        //            switch (res)
        //            {
        //                case CreateOurEditProductFeatureCategoryResult.IsExisted:
        //                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.IsExist, null));
        //                case CreateOurEditProductFeatureCategoryResult.Error:
        //                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
        //                case CreateOurEditProductFeatureCategoryResult.Success:
        //                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));

        //            }
        //        }
        //        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));

        //    }
        //}

        //[HttpPut]
        //public async Task<ActionResult> EditCategories([FromBody] EditProductFeatureCategoryDto category)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var res = await _productService.EditFeatureCategory(category, User.GetStoreCode());
        //            switch (res)
        //            {
        //                case CreateOurEditProductFeatureCategoryResult.IsExisted:
        //                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.IsExist, null));
        //                case CreateOurEditProductFeatureCategoryResult.Error:
        //                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
        //                case CreateOurEditProductFeatureCategoryResult.NotFound:
        //                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning, ApplicationMessages.NotFound, null));
        //                case CreateOurEditProductFeatureCategoryResult.Success:
        //                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));

        //            }
        //        }
        //        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));

        //    }
        //}
    }
}
