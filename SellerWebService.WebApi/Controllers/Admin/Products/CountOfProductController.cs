using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Products;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SellerWebService.WebApi.Controllers.Admin.Products
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class CountOfProductController : ControllerBase
    {
        //private readonly IProductService _productService;

        //public CountOfProductController(IProductService productService)
        //{
        //    _productService = productService;
        //}

        //[HttpGet("get-counts-for-product")]
        //public async Task<ActionResult<List<EditCountDto>>> GetCounts(long productId)
        //{
        //    var listCount = await _productService.GetAllCountForProduct(productId);
        //    if (listCount == null) return NotFound();
        //    return Ok(listCount);
        //}

        //[HttpPost("create-count-for-product")]
        //public async Task<ActionResult<OperationResponse>> CreateCount([FromBody] CreateCountDto count)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var res = await _productService.CreateCount(count);
        //        switch (res)
        //        {
        //            case CreateOurEditCountResult.Error:
        //                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, "مشکلی در ثبت پیش امد", null));
        //            case CreateOurEditCountResult.IsExisted:
        //                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning, "رکورد تکراری است", null));
        //            case CreateOurEditCountResult.NotFound:
        //                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,"محصولی با این مشخصات پیدا نشد", null));
        //            case CreateOurEditCountResult.Success:
        //                return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success, "عملیات ثبت موفق امیز بود", count));
        //        }

        //    }

        //    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, "مشکی پیش امده است",
        //        null));
        //}
    }
}
