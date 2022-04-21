using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Products;

namespace SellerWebService.WebApi.Controllers.Admin.Products
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        #region ctor
        private readonly IProductService _productService;

        public ProductCategoryController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion
        [HttpGet("get-all-product-category")]
        public async Task<ActionResult<List<EditProductFeatureCategoryDto>>> GetAllCategories()
        {
            var list = await _productService.GetAllProductCategory();
            if (!list.Any()) return NotFound();
            return Ok(list);
        }

        [HttpGet("get-product-category-by-{id}")]
        public async Task<ActionResult<EditProductCategoryDto>> GetCategoryById(long id)
        {
            var category = await _productService.GetProductCategoryById(id);
            if(category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost("create-product-category")]
        public async Task<ActionResult<OperationResponse>> CreateProductCategory([FromForm] CreateProductCategoryDto category)
        {
            if (ModelState.IsValid)
            {
                var res = await _productService.CreateProductCategory(category);
                switch (res)
                {
                    case CreateOurEditProductCategoryResult.Error:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, "مشکلی پیش امده است دوباره تلاش کنید", null));
                    case CreateOurEditProductCategoryResult.IsNotImage:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning, "لطفا تصویر را وارد کنید", null));
                    case CreateOurEditProductCategoryResult.IsExisted:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "دسته ای  با این نام وجود دارد", null));
                    case CreateOurEditProductCategoryResult.Success:
                        category.Picture = null;
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            "عملیات موفق امیز بود", category));

                }
            }
            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning,
                "اطلاعات را درست وارد کنید", null));
        }

        [HttpPut("edit-product-category")]
        public async Task<ActionResult<OperationResponse>> EditProductCategory(EditProductCategoryDto category)
        {
            if (ModelState.IsValid)
            {
                var res = await _productService.EditProductCategory(category);

                switch (res)
                {
                    case CreateOurEditProductCategoryResult.NotFound:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "گروهی با این مشخصات پیدا نشد", null));
                    case CreateOurEditProductCategoryResult.Success:
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            "عملیات موفق امیز بود", category));
                }

            }

            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                "مشکلی پیش امده است لطفا دوباره تلاش کنید", null));
        }

        [HttpPatch("change-active-state-for-product-category-by-{id}")]
        public async Task<ActionResult<OperationResponse>> ChangeActiveState(long id)
        {
            var res = await _productService.ChangeProductCategoryActiveState(id);
            if (res) return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                "عملیات موفق امیز بود", null);
        }
    }
}
