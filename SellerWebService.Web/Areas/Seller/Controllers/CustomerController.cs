using _0_framework.Messages;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Customer;
using SellerWebService.Web.Extension;

namespace SellerWebService.Web.Areas.Seller.Controllers
{
    public class CustomerController : BaseController
    {

        #region ctor
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        #endregion
        [HttpGet("create-customer")]
        public IActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost("create-customer"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _customerService.CreateCustomer(customer, User.GetStoreCode());
                    switch (res)
                    {
                        case CreateOurEditCustomerResult.IsExist:
                            TempData[WarningMessage] = ApplicationMessages.CustomerIsExists;
                            return View(customer);
                        case CreateOurEditCustomerResult.Error:
                            TempData[ErrorMessage] = ApplicationMessages.Error;
                            return View(customer);
                        case CreateOurEditCustomerResult.Success:
                            TempData[SuccessMessage] = ApplicationMessages.Success;
                            ModelState.Clear();
                            return View(new CreateCustomerDto());
                    }
                }
                return View(customer);
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = ApplicationMessages.Error;
                return View(customer);
            }
        }
        [HttpGet("edit-customer/{customerCode}")]
        public async Task<IActionResult> EditCustomer(Guid customerCode)
        {
            var customer = await _customerService.GetCustomerForEdit(customerCode, User.GetStoreCode());
            if (customer == null) RedirectToAction("Index", "Home");
            return View(customer);
        }
        [HttpPost("edit-customer/{customerCode}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(EditCustomerDto customer, Guid customerCode)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _customerService.EditCustomer(customer, User.GetStoreCode());
                    switch (res)
                    {
                        case CreateOurEditCustomerResult.IsExist:
                            TempData[WarningMessage] = ApplicationMessages.CustomerIsExists;
                            return View(customer);
                        case CreateOurEditCustomerResult.NotFound:
                            TempData[WarningMessage] = ApplicationMessages.NotFound;
                            return View(customer);
                        case CreateOurEditCustomerResult.Error:
                            TempData[ErrorMessage] = ApplicationMessages.Error;
                            return View(customer);
                        case CreateOurEditCustomerResult.Success:
                            TempData[SuccessMessage] = ApplicationMessages.Success;
                            return View(customer);
                    }
                }
                return View(customer);
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = ApplicationMessages.Error;
                return View(customer);
            }
        }
    }
}
