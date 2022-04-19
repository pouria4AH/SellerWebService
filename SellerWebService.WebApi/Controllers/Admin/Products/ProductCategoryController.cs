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

        [HttpPost("create-product-category")]
        public async Task<ActionResult<OperationResponse>> CreateProductCategory(CreateProductCategoryDto category)
        {
            if (ModelState.IsValid)
            {
                var res = await _productService.CreateProductCategory(category);
                switch (res)
                {
                    case CreateOurEditProductCategoryResult.Error:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, "مشکلی پیش امده است دوباره تلاش کنید", null));
                    //case CreateOurEditProductCategoryResult.ParentNotExisted:
                    //    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, "سردسته ای با این مشخصات یافت نشد", null));
                    case CreateOurEditProductCategoryResult.IsExisted:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "دسته ای  با این نام وجود دارد", null));
                    case CreateOurEditProductCategoryResult.Success:
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            "عملیات موفق امیز بود", category));

                }
            }
            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning,
                "اطلاعات را درست وارد کنید", null));
        }
    }
}
