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
    public class BankController : BaseController
    {
        #region ctor
        private readonly IStoreService _storeService;

        public BankController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        #endregion

        [HttpGet("create-bank-data")]
        public async Task<IActionResult> CreateBankData()
        {
            if (await _storeService.HaveAnyBankData(User.GetStoreCode())) return RedirectToAction("EditBankData");
            return View();
        }

        [HttpPost("create-bank-data"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBankData(BankDataDto data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _storeService.CreateBankData(data, User.GetStoreCode());
                    if (res)
                    {
                        TempData[SuccessMessage] = ApplicationMessages.Success;
                        return RedirectToAction("Index", "Home");
                    }
                    TempData[ErrorMessage] = ApplicationMessages.Error;
                    return View();
                }

                return View();
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = ApplicationMessages.Error;
                return View();
            }
        } 
        [HttpGet("edit-bank-data")]
        public async Task<IActionResult> EditBankData()
        {
            if (!(await _storeService.HaveAnyBankData(User.GetStoreCode()))) return RedirectToAction("CreateBankData");
          var data =  await _storeService.GetBankData(User.GetStoreCode());
            return View(data);
        }

        [HttpPost("edit-bank-data"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBankData(BankDataDto data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //if (data == await _storeService.GetBankData(User.GetStoreCode()))
                    //{
                    //    TempData[WarningMessage] = ApplicationMessages.IsExist;
                    //    return View();
                    //}
                    var res = await _storeService.EditBankData(data, User.GetStoreCode());
                    if (res)
                    {
                        TempData[SuccessMessage] = ApplicationMessages.Success;
                        return RedirectToAction("Index", "Home");
                    }
                    TempData[ErrorMessage] = ApplicationMessages.Error;
                    return View();
                }

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
