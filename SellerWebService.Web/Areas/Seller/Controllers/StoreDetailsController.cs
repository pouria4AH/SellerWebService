using _0_framework.Account;
using _0_framework.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Store;
using SellerWebService.Web.Extension;

namespace SellerWebService.Web.Areas.Seller.Controllers
{
    [Authorize(Roles = AccountRole.Seller)]
    public class StoreDetailsController : BaseController
    {
        #region ctor
        private readonly IStoreService _storeService;
        public StoreDetailsController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        #endregion

        [HttpGet("create-details")]
        public async Task<IActionResult> CreateDetails()
        {
            if (await _storeService.IsHaveStoreDetails(User.GetStoreCode())) return RedirectToAction("EditDetails");
            return View();
        }
        [HttpPost("create-details"),ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetails(CreateStoreDetailsDto StoreDetails, IFormFile image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _storeService.CreateStoreDetails(StoreDetails, image, User.GetStoreCode());
                    switch (res)
                    {
                        case CreateStoreDetailsResult.Error:
                            TempData[ErrorMessage] = ApplicationMessages.Error;
                            break;
                        case CreateStoreDetailsResult.StoreIsNull:
                            TempData[WarningMessage] = ApplicationMessages.NotFound;
                            break;
                        case CreateStoreDetailsResult.Success:
                            TempData[SuccessMessage] = ApplicationMessages.Success;
                            return RedirectToAction("Index", "Home");
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = ApplicationMessages.Error;
                return View();

            }
        }
        [HttpGet("edit-details")]
        public async Task<IActionResult> EditDetails()
        {
            if (await _storeService.IsHaveStoreDetails(User.GetStoreCode())) return RedirectToAction("CreateDetails");
            return View();
        }
    }
}
