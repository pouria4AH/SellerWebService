using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Presell;

namespace SellerWebService.WebApi.Controllers
{
    public class HomeController : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string WarningMessage = "WarningMessage";

        private readonly IPresellService _presellService;

        public HomeController(IPresellService presellService)
        {
            _presellService = presellService;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("/"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreatePresellDto presell)
        {
            if (ModelState.IsValid)
            {
                var res = await _presellService.Create(presell);
                switch (res)
                {
                    case presellResult.Error:
                        TempData[ErrorMessage] = "مشکلی پیش امده است لطفا دوباره تلاش کنید";
                        break;
                    case presellResult.Exists:
                        TempData[WarningMessage] = "قبلا درخواستی با این مشخصات ثبت شده است";
                        break;
                    case presellResult.Success:
                        TempData[SuccessMessage] = "عملیات موفق آمیز بود";
                        break;
                }

                return View();
            }
            return View();
        }
    }
}
