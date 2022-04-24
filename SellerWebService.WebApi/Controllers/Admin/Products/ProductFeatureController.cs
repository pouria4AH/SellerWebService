using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs;
using SellerWebService.DataLayer.DTOs.Products;

namespace SellerWebService.WebApi.Controllers.Admin.Products
{
    [Route("api/[controller]")]
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



        [HttpPost("create-group-of-product-feature")]
        public async Task<ActionResult<OperationResponse>> CreateGroupOfProductFeature(
            [FromBody] CreateGroupProductFeatureDto group)
        {
            if (ModelState.IsValid)
            {
                var res = _productService.CrateGroupOfProductFeature(group);

            }
        }

    }
}
