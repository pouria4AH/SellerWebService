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
        public IActionResult CreateBankData()
        {
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
    }
}
