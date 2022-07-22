using _0_framework.Messages;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Factor;
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

        //[HttpGet("get-customer")]
        //public async Task<IActionResult> GetCustomerCode([FromQuery] SearchCustomerDto? search)
        //{
        //    if (search != null)  {
        //        ViewBag.Customer = await _customerService.SearchForCustomer(search, User.GetStoreCode());
        //    }
        //    ViewBag.Customer = new List<ReadCustomerDto>() ;
        //    return View(new SearchCustomerDto());
        //}
        [HttpGet("create-factor")]
        public IActionResult CreateFactor()
        {
            return View();
        }
        [HttpPost("create-factor"),ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFactor(CreateFactorDto factor)
        {
            try
            {
                if (!ModelState.IsValid || factor.CustomerCode == Guid.Empty)
                    TempData[ErrorMessage] = ApplicationMessages.ModelIsNotValid;
                var res = await _factorService.CreateBlankFactor(factor, User.GetStoreCode());
                if (res != Guid.Empty)
                {
                    TempData[SuccessMessage] = ApplicationMessages.Success;
                    return RedirectToAction("CreateFactorDetails", res);
                }
                return View();
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = ApplicationMessages.Error;
                return View();
            }
        }

        [HttpGet("create-details/{factorCode}")]
        public IActionResult CreateFactorDetails(Guid factorCode)
        {
            return View();
        }
    }

}
