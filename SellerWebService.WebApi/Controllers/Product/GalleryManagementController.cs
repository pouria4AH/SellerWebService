using _0_framework.Account;
using _0_framework.Http;
using _0_framework.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Products;
using SellerWebService.DataLayer.Entities.Products;
using SellerWebService.WebApi.Extensions;

namespace SellerWebService.WebApi.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AccountRole.Seller + " " + AccountRole.SellerEmployee)]
    public class GalleryManagementController : ControllerBase
    {
        #region ctor

        private readonly IProductService _productService;

        public GalleryManagementController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion
        /// <summary>
        /// در یافت همه کالری های یک محصول
        /// </summary>
        /// <remarks>get product id and return 200 by list of productGallery (not dto) our is dont have gallery return 400 by non data</remarks>
        /// <response code="200">by list of product gallery</response>
        /// <response code="400">by non data</response>   
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("{productId}")]
        public async Task<ActionResult<List<ProductGallery>>> GetAllProductGallery([FromRoute] long productId)
        {
            try
            {
                var res = await _productService.GetAllProductGallery(productId);
                if (res == null || !res.Any()) return BadRequest();
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// ساخت یک عکس با همان گالری برای محصول
        /// </summary>
        /// <remarks>get CreateOurEditProductGalleryDTO on body and product id on route and return OperationResponse by non data anyway</remarks>
        /// <param name="gallery"></param>
        /// <param name="productId"></param>
        /// <response code="200">return OperationResponse by non data</response>
        /// <response code="400">return OperationResponse by non data</response>   
        [HttpPost("{productId}")]
        public async Task<ActionResult<OperationResponse>> CreateGallery([FromBody] CreateOurEditProductGalleryDTO gallery, [FromRoute] long productId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _productService.CreateProductGallery(gallery, productId, User.GetStoreCode());
                    switch (res)
                    {
                        case CreateOurEditProductGalleryResult.ImageIsNull:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.IsNotImage, null));
                        case CreateOurEditProductGalleryResult.ProductNotFound:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.NotFound, null));
                        case CreateOurEditProductGalleryResult.Success:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));
                    }
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));

            }
            catch (Exception e)
            {
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.IsNotImage, null));
            }
        }
        /// <summary>
        /// ادیت کردن گالری
        /// </summary>
        /// <remarks>get gallery id on route and CreateOurEditProductGalleryDTO on body return OperationResponse by non data anyway </remarks>
        /// <param name="gallery"></param>
        /// <param name="id"></param>
        /// <response code="200">return OperationResponse by non data</response>
        /// <response code="400">return OperationResponse by non data</response>   
        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse>> EditGallery([FromBody] CreateOurEditProductGalleryDTO gallery, [FromRoute] long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _productService.EditProductGallery(id, gallery, User.GetStoreCode());
                    switch (res)
                    {
                        case CreateOurEditProductGalleryResult.ImageIsNull:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.IsNotImage, null));
                        case CreateOurEditProductGalleryResult.ProductNotFound:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.NotFound, null));
                        case CreateOurEditProductGalleryResult.Success:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));
                    }
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));

            }
            catch (Exception e)
            {
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.IsNotImage, null));
            }
        }
        /// <summary>
        /// گرفنن اظلاعات کامل یک گالری
        /// </summary>
        /// <remarks>get gallery is on route and return 200 by CreateOurEditProductGalleryDTO and if gallery dont exsist return 400 by non data</remarks>
        /// <param name="id"></param>
        /// <response code="200">return CreateOurEditProductGalleryDTO</response>
        /// <response code="400">return non data</response>   
        [HttpGet("{id}")]
        public async Task<ActionResult<CreateOurEditProductGalleryDTO>> GetFullGallery([FromRoute] long id)
        {
            var res = await _productService.GetProductGalleryFourEdit(id, User.GetStoreCode());
            if (res != null) return Ok(res);
            return BadRequest();
        }
    }
}
