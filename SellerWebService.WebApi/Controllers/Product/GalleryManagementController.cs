using _0_framework.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;

namespace SellerWebService.WebApi.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AccountRole.Seller + " " + AccountRole.SellerEmployee)]
    public class GalleryManagementController : ControllerBase
    {
        //#region ctor

        //private readonly IProductService _productService;

        //public GalleryManagementController(IProductService productService)
        //{
        //    _productService = productService;
        //}

        //#endregion

        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetAllProductGallery([FromRoute] long id)
        //{
        //    try
        //    {
        //        var res = await _productService.GetAllProductGallery(id);
        //        if (res == null || !res.Any()) return BadRequest();
        //        return Ok(res);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
