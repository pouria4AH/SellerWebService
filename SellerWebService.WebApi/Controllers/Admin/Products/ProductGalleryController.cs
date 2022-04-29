using _0_framework.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Products;
using SellerWebService.DataLayer.Entities.Products;
using SellerWebService.DataLayer.Migrations;

namespace SellerWebService.WebApi.Controllers.Admin.Products
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class ProductGalleryController : ControllerBase
    {
        #region ctor
        private readonly IProductService _productService;
        public ProductGalleryController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        [HttpGet("get-galleries-by-{productId}")]
        public async Task<List<ProductGallery>> GetGalleryForProduct(long productId)
        {
            return await _productService.GetAllProductGallery(productId);
        }  

        [HttpPost("create-gallery-for-{productId}")]
        public async Task<ActionResult<CreateOurEditProductGalleryDTO>> CreateGallery([FromForm] CreateOurEditProductGalleryDTO gallery, [FromRoute] long productId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _productService.CreateProductGallery(gallery, productId);
                    switch (res)
                    {
                        case CreateOurEditProductGalleryResult.ImageIsNull:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                                ApplicationMessages.IsNotImage, null));
                        case CreateOurEditProductGalleryResult.ProductNotFound:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning, ApplicationMessages.NotFound, null));
                        case CreateOurEditProductGalleryResult.Success:
                            gallery.Image = null;
                            return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, gallery));

                    }
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                    ApplicationMessages.Error, null));
            }
            catch (Exception e)
            {
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                    ApplicationMessages.Error, e));
            }
        }

        [HttpPost("get-gallery-for-edit-by-{galleryId}")]
        public async Task<ActionResult<ProductGallery>> GetForEdit(long galleryId)
        {
            var gallery = await _productService.GetProductGalleryFourEdit(galleryId);
            if (gallery == null) return NotFound();
            return Ok(gallery);
        }

        [HttpPost("edit-product-gallery-by-{galleryId}")]
        public async Task<ActionResult<OperationResponse>> EditGallery([FromRoute] long galleryId,
            [FromBody] CreateOurEditProductGalleryDTO gallery)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _productService.EditProductGallery(galleryId, gallery);
                    switch (res)
                    {
                        case CreateOurEditProductGalleryResult.ImageIsNull:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning,
                                ApplicationMessages.IsNotImage, null));
                        case CreateOurEditProductGalleryResult.ProductNotFound:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                                ApplicationMessages.NotFound, null));
                        case CreateOurEditProductGalleryResult.Success:
                            gallery.Image = null;
                            return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                                ApplicationMessages.Success, gallery));

                    }
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                    ApplicationMessages.Error, null));
            }
            catch (Exception e)
            {
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                    ApplicationMessages.Error, e));
            }
        }
    }
}

