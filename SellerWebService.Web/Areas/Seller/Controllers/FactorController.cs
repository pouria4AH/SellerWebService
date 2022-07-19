using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Customer;
using SellerWebService.Web.Extension;

namespace SellerWebService.Web.Areas.Seller.Controllers
{
    public class FactorController : BaseController
    {
        #region ctor
        private readonly IFactorService _factorService;
        private readonly ICustomerService _customerService;

        public FactorController(IFactorService factorService, ICustomerService customerService)
        {
            _factorService = factorService;
            _customerService = customerService;
        }
        #endregion

        [HttpGet("get-customer")]
        public async Task<IActionResult> GetCustomerCode([FromQuery] SearchCustomerDto? search)
        {
            ViewBag.Customers = await _customerService.SearchForCustomer(search, User.GetStoreCode());
            return View(new SearchCustomerDto());
        }

    }
}
