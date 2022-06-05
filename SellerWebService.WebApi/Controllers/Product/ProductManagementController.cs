using _0_framework.Account;
using _0_framework.Http;
using _0_framework.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Products;
using SellerWebService.WebApi.Extensions;

namespace SellerWebService.WebApi.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AccountRole.Seller + " " + AccountRole.SellerEmployee)]
    public class ProductManagementController : ControllerBase
    {
        #region ctor
        private readonly IProductService _productService;

        public ProductManagementController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult> GetAllProduct()
        {
            var list = await _productService.GetAllProduct(User.GetStoreCode());
            if (list == null || !list.Any()) return BadRequest();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct([FromRoute] long id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return BadRequest();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse>> Create([FromBody] CreateProductDto product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _productService.CreateProduct(product, User.GetStoreCode());
                    switch (res)
                    {
                        case CreateOurEditProductResult.IsNotImage:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.IsNotImage, null));
                        case CreateOurEditProductResult.IsExisted:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                                ApplicationMessages.IsExist, null));
                        case CreateOurEditProductResult.Error:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
                        case CreateOurEditProductResult.Success:
                            return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));


                    }
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));

            }
            catch (Exception e)
            {

                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
            }
        }

        [HttpPut]
        public async Task<ActionResult<OperationResponse>> Edit([FromForm] EditProductDto product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _productService.EditProduct(product,User.GetStoreCode());
                    switch (res)
                    {
                        case CreateOurEditProductResult.IsNotImage:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.IsNotImage, null));
                        case CreateOurEditProductResult.IsExisted:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                                ApplicationMessages.IsExist, null));
                        case CreateOurEditProductResult.Error:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
                        case CreateOurEditProductResult.NotFound:
                            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.NotFound, null));
                        case CreateOurEditProductResult.Success:
                            return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));

                    }
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
            }
            catch (Exception e)
            {
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));

            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> ChangeActiveState([FromRoute] long id)
        {
            var res = await _productService.ChangeProductActiveState(id, User.GetStoreCode());
            if (res) return Ok();
            return BadRequest();
        }

    }
}
