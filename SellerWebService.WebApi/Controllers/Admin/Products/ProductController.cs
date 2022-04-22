using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Products;
using SellerWebService.DataLayer.Entities.Products;

namespace SellerWebService.WebApi.Controllers.Admin.Products
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create-product")]
        public async Task<ActionResult<OperationResponse>> CreateProduct([FromForm] CreateProductDto product)
        {
            if (ModelState.IsValid)
            {
                var res = await _productService.CreateProduct(product);

                switch (res)
                {
                    case CreateOurEditProductResult.IsNotImage:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning,
                            "لطفا عکس وارد کنید", null));
                    case CreateOurEditProductResult.CountListIsNotExisted:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning,
                            "لیست تیراژ را وارد کنید", null));
                    case CreateOurEditProductResult.IsExisted:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "محصول مورد نظر قبلا وجود دارد", null));
                    case CreateOurEditProductResult.Error:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "مشکلی پیش امده بعدا تلاش کنید", null));
                    case CreateOurEditProductResult.Success:
                        product.Picture = null;
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            "عملیات با موفقیت انجام شد", product));
                }
            }
            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                "لطفا اطلاعات را درست وارد کنید", null));
        }

    }

}
