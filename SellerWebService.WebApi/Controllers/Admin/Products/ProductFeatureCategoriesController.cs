using _0_framework.Http;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Products;

namespace SellerWebService.WebApi.Controllers.Admin.Products
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProductFeatureCategoriesController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductFeatureCategoriesController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get-all-product-feature-category") , ValidateAntiForgeryToken]
        public async Task<ActionResult<List<CreateProductFeatureCategoryDto>>> Get()
        {
            var featureCategories = await _productService.GetProductFeatureCategories();
            return Ok(featureCategories);
        }

        [HttpPost("create-product-feature-category"), ValidateAntiForgeryToken]
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

        [HttpPut("edit-product-feature-category/{id}") , ValidateAntiForgeryToken]
        public async Task<ActionResult<OperationResponse>> EditCategory(long id, [FromBody] EditProductFeatureCategoryDto featureCategory)
        {
            return Ok();
        }

    }
}
