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
        [HttpPost("create-details"), ValidateAntiForgeryToken]
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
            if (!(await _storeService.IsHaveStoreDetails(User.GetStoreCode()))) return RedirectToAction("CreateDetails");
            ViewBag.logo = await _storeService.GetLogo(User.GetStoreCode());
            return View(await _storeService.GetDetailsForEdit(User.GetStoreCode()));
        }
        [HttpPost("edit-details"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDetails(CreateStoreDetailsDto StoreDetails, IFormFile image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _storeService.EditStoreDetails(StoreDetails, image, User.GetStoreCode());
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

        [HttpGet("create-signature")]
        public async Task<IActionResult> CreateSignature()
        {
            var (res, data) = await _storeService.GetSignature(User.GetStoreCode());
            if (res) return RedirectToAction("EditSignature");
            return View();
        }

        [HttpPost("create-signature")]
        public async Task<IActionResult> CreateSignature(IFormFile signature)
        {
            try
            {
                var res = await _storeService.CreateSignature(signature, User.GetStoreCode());
                if (res)
                {
                    TempData[SuccessMessage] = ApplicationMessages.Success;
                    return RedirectToAction("Index", "Home");
                }

                TempData[ErrorMessage] = ApplicationMessages.Error;
                return View();
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = ApplicationMessages.Error;
                return View();
            }
        }
        [HttpGet("edit-signature")]
        public async Task<IActionResult> EditSignature()
        {
            var (res, data) = await _storeService.GetSignature(User.GetStoreCode());
            if (!res) return RedirectToAction("CreateSignature");
            ViewBag.ImageName = data;
            return View();
        }

        [HttpPost("edit-signature")]
        public async Task<IActionResult> EditSignature(IFormFile signature)
        {
            try
            {
                var res = await _storeService.EditSignature(signature, User.GetStoreCode());
                if (res)
                {
                    TempData[SuccessMessage] = ApplicationMessages.Success;
                    return RedirectToAction("Index", "Home");
                }

                TempData[ErrorMessage] = ApplicationMessages.Error;
                return View();
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = ApplicationMessages.Error;
                return View();
            }
        }
        [HttpGet("create-stamp")]
        public async Task<IActionResult> CreateStamp()
        {
            var (res, data) = await _storeService.GetStamp(User.GetStoreCode());
            if (res || data == null) return RedirectToAction("EditStamp");
            return View();
        }

        [HttpPost("create-stamp")]
        public async Task<IActionResult> CreateStamp(IFormFile stamp)
        {
            try
            {
                var res = await _storeService.CreateStamp(stamp, User.GetStoreCode());
                if (res)
                {
                    TempData[SuccessMessage] = ApplicationMessages.Success;
                    return RedirectToAction("Index", "Home");
                }

                TempData[ErrorMessage] = ApplicationMessages.Error;
                return View();
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = ApplicationMessages.Error;
                return View();
            }
        }
        [HttpGet("edit-stamp")]
        public async Task<IActionResult> EditStamp()
        {
            var (res, data) = await _storeService.GetStamp(User.GetStoreCode());
            if (!res || data == null) return RedirectToAction("CreateStamp");
            ViewBag.ImageName = data;
            return View();
        }

        [HttpPost("edit-stamp")]
        public async Task<IActionResult> EditStamp(IFormFile stamp)
        {
            try
            {
                var res = await _storeService.EditStamp(stamp, User.GetStoreCode());
                if (res)
                {
                    TempData[SuccessMessage] = ApplicationMessages.Success;
                    return RedirectToAction("Index", "Home");
                }

                TempData[ErrorMessage] = ApplicationMessages.Error;
                return View();
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = ApplicationMessages.Error;
                return View();
            }
        }
    }
}
