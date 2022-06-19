using _0_framework.Account;
using _0_framework.Http;
using _0_framework.Messages;
using Microsoft.AspNetCore.Authorization;
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
        /// <summary>
        /// گرفتن همه محصولات
        /// </summary>
        /// <remarks>return all product for store by list of ReadProductDto if do not have product return 400 by not data</remarks>
        /// <response code="200">return 200 by list of ReadProductDto</response>
        /// <response code="400">return 400 by non data</response>
        [HttpGet]
        public async Task<ActionResult> GetAllProduct()
        {
            var list = await _productService.GetAllProduct(User.GetStoreCode());
            if (list == null || !list.Any()) return BadRequest();
            return Ok(list);
        }
        /// <summary>
        /// گرفتن یک تک محصول
        /// </summary>
        /// <returns>get product id on route and return ReadProductDto</returns>
        /// <param name="id"></param>
        /// <response code="200">return 200 by ReadProductDto</response>
        /// <response code="400">return 400 by non data</response>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct([FromRoute] long id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return BadRequest();
            return Ok(product);
        }
        /// <summary>
        /// ساخت محصول
        /// </summary>
        /// <remarks>create a product and return OperationResponse if is 200 code have product id and for 400 code do not have data </remarks>
        /// <param name="product"></param>
        /// <response code="200">return 200 operation response and id by id key in object</response>
        /// <response code="400">return 400 operation response by non data</response>   
        [HttpPost]
        public async Task<ActionResult<OperationResponse>> Create([FromBody] CreateProductDto product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    (var res,long id) = await _productService.CreateProduct(product, User.GetStoreCode());
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
                            return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, new { id = id}));


                    }
                }
                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));

            }
            catch (Exception e)
            {

                return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
            }
        }
        /// <summary>
        /// ادیت کردن محصول
        /// </summary>
        /// <remarks>edit product by EditProductDto return 400 and 200 by OperationResponse and dont have data for all </remarks>
        /// <param name="product"></param>
        /// <response code="200">return 200 operation response and non data</response>
        /// <response code="400">return 400 operation response by non data</response>   
        [HttpPut]
        public async Task<ActionResult<OperationResponse>> Edit([FromForm] EditProductDto product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _productService.EditProduct(product, User.GetStoreCode());
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
        /// <summary>
        /// فعال یا غیر فعال کردن محصول
        /// </summary>
        /// <remarks>get product id and return just 200 and 400 code </remarks>
        /// <param name="id"></param>
        /// <response code="200">by non data</response>
        /// <response code="400">by non data</response>   
        [HttpPatch("{id}")]
        public async Task<ActionResult> ChangeActiveState([FromRoute] long id)
        {
            var res = await _productService.ChangeProductActiveState(id, User.GetStoreCode());
            if (res) return Ok();
            return BadRequest();
        }

    }
}
