global using _0_framework.Http;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Products;

namespace SellerWebService.WebApi.Controllers.Admin.Products
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProductFeatureCategoriesController : ControllerBase
    {
        #region ctor
        private readonly IProductService _productService;

        public ProductFeatureCategoriesController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        [HttpGet("get-all-product-feature-category")]
        public async Task<ActionResult<List<EditProductFeatureCategoryDto>>> Get()
        {
            var featureCategories = await _productService.GetProductFeatureCategories();

            if (featureCategories == null) return BadRequest();

            return Ok(featureCategories);
        }

        [HttpPost("create-product-feature-category")]
        public async Task<ActionResult<OperationResponse>> PostCreate([FromBody] CreateProductFeatureCategoryDto featureCategory)
        {
            if (featureCategory == null) return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, "مقادیر وارد شده خالی هستن", null));

            if (!ModelState.IsValid) return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning, "لطفا مقادیر را درست وارد کنید", null));

            var res = await _productService.CreateFeatureCategory(featureCategory);
            switch (res)
            {
                case CreateOurEditProductFeatureCategoryResult.IsExisted:
                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning,
                        "این دسته از قبل وجود دارد", featureCategory));
                case CreateOurEditProductFeatureCategoryResult.Error:
                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                        "خطایی رخ داد", null));
                case CreateOurEditProductFeatureCategoryResult.Success:
                    return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                        "دسته مورد نظر با موفقیت ثبت شد", featureCategory));
            }

            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                "عمیات با خطا مواجه شد", null));
        }

        [HttpPut("edit-product-feature-category/")]
        public async Task<ActionResult<OperationResponse>> EditCategory([FromBody] EditProductFeatureCategoryDto featureCategory)
        {
            if (featureCategory == null)
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                    "اطلاعات را کامل وارد کنید", null));

            if (ModelState.IsValid)
            {
                var res = await _productService.EditFeatureCategory(featureCategory);
                switch (res)
                {
                    case CreateOurEditProductFeatureCategoryResult.Error:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "خطایی رخ داد", null));
                    case CreateOurEditProductFeatureCategoryResult.NotFound:
                        return BadRequest(
                            OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                                "اطلاعتی با این مشخصات یافت نشد", null));
                    case CreateOurEditProductFeatureCategoryResult.Success:
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            "عملیات با موفقیت انجام شد", featureCategory));
                }

                return BadRequest(
                    OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                        "خطلایی رخ داد لطفا دوباره تلاش کنید", null));
            }

            return Ok();
        }

    }
}
