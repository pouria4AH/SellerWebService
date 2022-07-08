using System.Security.Claims;
using _0_framework.Messages;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Account;

namespace SellerWebService.Web.Controllers
{
    public class AccountController : SiteBaseController
    {
        #region ctor

        private readonly IUserService _userService;
        private readonly ICaptchaValidator _captchaValidator;

        public AccountController(IUserService userService, ICaptchaValidator captchaValidator)
        {
            _userService = userService;
            _captchaValidator = captchaValidator;
        }
        #endregion
        #region Login

        [HttpGet("login")]
        public IActionResult Login()
        {

            if (User.Identity.IsAuthenticated) return Redirect("/");
            return View();
        }
        [HttpPost("login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDTO login)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(login.Captcha))
            {
                TempData[ErrorMessage] = "کد کپجای شما تایید نشد";
                return View("Login");
            }
            if (ModelState.IsValid)
            {
                var res = await _userService.GetUserForLogin(login);
                switch (res)
                {
                    case LoginUserResult.NotFound:
                        TempData[ErrorMessage] = ApplicationMessages.UserNotFound;
                        break;
                    case LoginUserResult.NotActivated:
                        TempData[WarningMessage] = ApplicationMessages.UserNotActive;
                        break;
                    case LoginUserResult.Success:

                        var user = await _userService.GetUserByMobile(login.Mobile);

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.MobilePhone, user.Mobile),
                            new Claim(ClaimTypes.GivenName, user.FirstName ),
                            new Claim(ClaimTypes.Surname, user.LastName),
                            new Claim(ClaimTypes.Role, user.Role),
                            new Claim(ClaimTypes.NameIdentifier, user.UniqueCode.ToString("N")),

                        };
                        if (user.StoreCode != null) claims.Add(new Claim(ClaimTypes.SerialNumber, user.StoreCode?.ToString("N")));

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = login.RememberMe
                        };
                        await HttpContext.SignInAsync(principal, properties);
                        TempData[SuccessMessage] = ApplicationMessages.Success;
                        return Redirect("/");
                }
            }
            return View();
        }

        #endregion

        #region log out

        [HttpGet("log-out")]
        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync();
            return Redirect("./");
        }
        #endregion
    }
}
