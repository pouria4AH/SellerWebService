using System.Security.Principal;
using _0_framework.Account;
using _0_framework.Http;
using _0_framework.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Customer;
using SellerWebService.WebApi.Extensions;

namespace SellerWebService.WebApi.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AccountRole.Seller)]
    public class CustomerController : ControllerBase
    {
        //#region ctor
        //private readonly ICustomerService _customerService;

        //public CustomerController(ICustomerService customerService)
        //{
        //    _customerService = customerService;
        //}
        //#endregion

        ///// <summary>
        ///// در این قسمت ادمین یا کارمند مشتری اد میکنند
        ///// </summary>
        ///// <remarks>get customer data and return operation response non object any way</remarks>
        ///// <response code="200">return operation response</response>
        ///// <response code="400">return operation response </response>
        //[HttpPost]
        //public async Task<ActionResult<OperationResponse>> CreateCustomer(CreateCustomerDto customer)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var res = await _customerService.CreateCustomer(customer, User.GetStoreCode());
        //            switch (res)
        //            {
        //                case CreateOurEditCustomerResult.IsExist:
        //                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning,
        //                        ApplicationMessages.IsExist, null));
        //                case CreateOurEditCustomerResult.Error:
        //                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
        //                case CreateOurEditCustomerResult.Success:
        //                    return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));
        //            }
        //        }
        //        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.ModelIsNotValid, null));
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
        //    }
        //}

        ///// <summary>
        ///// در قسمت مشتری ویرایش میشود میشود
        ///// </summary>
        ///// <param name="customer"></param>
        ///// <remarks>difference by create just customer returns operation response by non object</remarks> 
        //[HttpPut]
        //public async Task<ActionResult<OperationResponse>> EditCustomer(EditCustomerDto customer)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var res = await _customerService.EditCustomer(customer, User.GetStoreCode());
        //            switch (res)
        //            {
        //                case CreateOurEditCustomerResult.NotFound:
        //                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning,
        //                        ApplicationMessages.NotFound, null));
        //                case CreateOurEditCustomerResult.Error:
        //                    return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
        //                case CreateOurEditCustomerResult.Success:
        //                    return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success, ApplicationMessages.Success, null));
        //            }
        //        }
        //        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.ModelIsNotValid, null));
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.Error, null));
        //    }
        //}

        ///// <summary>
        ///// حذف مشتری با کد مشتری
        ///// </summary>
        ///// <param name="customerCode"></param>
        ///// <remarks>send customer code in url and return 200 our 400 by non data</remarks>
        //[HttpDelete("{customerCode}")]
        //public async Task<ActionResult> DeleteCustomer(string customerCode)
        //{
        //    try
        //    {
        //        var code = Guid.Parse(customerCode);
        //        var res = await _customerService.DeleteCustomer(code, User.GetStoreCode());
        //        if (res) return Ok();
        //        return BadRequest();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest();
        //    }
        //}

        ///// <summary>
        ///// پیدا کردن مشتری با کد مشتری
        ///// </summary>
        ///// <param name="customerCode"></param>
        ///// <remarks>just return 404 our 200 if is 200 code have data (ReadCustomerDto) but 404 is not data</remarks>
        ///// <response code="200">send 200 by ReadCustomerDto</response>
        ///// <response code="404">send non object</response>
        //[HttpGet("{customerCode}")]
        //public async Task<ActionResult<ReadCustomerDto>> GetCustomer(string customerCode)
        //{
        //    var customer = await _customerService.GetCustomer(Guid.Parse(customerCode), User.GetStoreCode());
        //    if (customer == null) return NotFound();
        //    return Ok(customer);
        //}

        ///// <summary>
        ///// سرچ کردن مشتری با شماره و نام و نام خانوادگی
        ///// </summary>
        ///// <param name="search">get by query string</param>
        ///// <remarks>just return fore 200 have data but for 400 has not data</remarks>
        ///// <response code="400">send non object</response>
        ///// <response code="200">send 200 by list of ReadCustomerDto</response>
        //[HttpGet("search")]
        //[Authorize(Roles = AccountRole.Seller + "," + AccountRole.SellerEmployee)]
        //public async Task<ActionResult> SearchCustomer([FromQuery] SearchCustomerDto search)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(search.Mobile) && !string.IsNullOrEmpty(search.firstName) &&
        //            !string.IsNullOrEmpty(search.lastName)) return BadRequest();
        //        var res = await _customerService.SearchForCustomer(search, IdentityExtensions.GetStoreCode((IPrincipal)User));
        //        if (res != null && res.Any()) return Ok(res);
        //        return BadRequest();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
