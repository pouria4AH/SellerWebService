using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Products;

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

        [HttpGet("get-all-product")]
        public async Task<IEnumerable<ReadProductDto>> GetAllProducts()
        {
            return await _productService.GetAllProduct();
        }

        [HttpGet("get-product-by-{id}")]
        public async Task<ReadProductDto> GetProduct(long id)
        {
            return await _productService.GetProductById(id);
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

        [HttpPut("edit-product")]
        public async Task<ActionResult<OperationResponse>> EditProduct([FromForm] EditProductDto product)
        {
            if (ModelState.IsValid)
            {
                var res = await _productService.EditProduct(product);
                switch (res)
                {
                    case CreateOurEditProductResult.IsNotImage:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "لطفا عکس را درست وارد کنید", null));
                    case CreateOurEditProductResult.Error:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                                "مشکلی پیش امد دوباره تلاش کنید", null));
                    case CreateOurEditProductResult.CountListIsNotExisted:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "لیست تیراژ را وارد کنید", null));
                    case CreateOurEditProductResult.NotFound:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "محصول مورد نظر یافت نشد", null));
                    case CreateOurEditProductResult.Success:
                        product.Picture = null;
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            "عملیات موفق امیز بود", product));
                }
            }
            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                "مشکلی پیش امد دوباره تلاش کنید", null));
        }
    }

}
