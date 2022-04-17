using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Products;

namespace SellerWebService.WebApi.Controllers.Admin.Products
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProductFeatureCategories : ControllerBase
    {
        //private readonly IProductService _productService;

        //public ProductFeatureCategories(IProductService productService)
        //{
        //    _productService = productService;
        //}

        [HttpGet("get-all-product-feature-category")]
        public async  Task<ActionResult<List<CreateOurEditProductFeatureCategoryDto>>> Get()
        {
            //var featureCategories = await _productService.GetProductFeatureCategories();
            return Ok();
        }
    }
}
