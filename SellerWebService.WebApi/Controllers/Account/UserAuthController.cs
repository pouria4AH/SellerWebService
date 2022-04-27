using _0_framework.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Account;
using SellerWebService.WebApi.Extensions;

namespace SellerWebService.WebApi.Controllers.Account
{
    [Route("api/Account/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserAuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register-user")]
        public async Task<ActionResult<OperationResponse>> Register([FromBody] RegisterUserDTO register)
        {
            if (ModelState.IsValid)
            {
                string role = "User";
                var res = await _userService.RegisterUser(register, role);
                switch (res)
                {
                    case RegisterUserResult.Error:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "مشکلی پیش امده است", null));
                    case RegisterUserResult.Success:
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            "عملیات موفق امیز بود", new
                            {
                                mobile = register.Mobile,
                                firstName = register.FirstName,
                                lastName = register.LastName,
                            }));
                    case RegisterUserResult.MobileExists:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning,
                            "شماره مورد نظر از قبل وجود دارد", null));
                }
            }
            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                "مشکلی پیش امده است", null));
        }

        [HttpPost("active-mobile")]
        public async Task<ActionResult<OperationResponse>> ActiveMobile([FromBody] ActivateMobileDTO activate)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.ActiveMobile(activate);
                switch (res)
                {
                    case ActiveMobileState.ExpiredCode:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning, "تاریخ کد شما گذشته است", null));
                    case ActiveMobileState.MobileIsActiveAlready:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Info,
                            "این شماره در حال حاضر فعال است", null));
                    case ActiveMobileState.UserNotFound:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "کاربر مورد نظر پیدا نشد", null));
                    case ActiveMobileState.CodeIsWrong:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "کد اشتباه است", null));
                    case ActiveMobileState.Success:
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            "موبایل شما فعال شد", null));
                }
            }
            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, "اطلاعات وارد شده نادرست است", null));
        }

        [HttpPost("login-user")]
        public async Task<ActionResult<OperationResponse>> Login([FromBody] LoginUserDTO login)
        {
            if (HttpContext.User.Identity.IsAuthenticated) return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Info, "شما لاگین هستید", null));

            if (ModelState.IsValid)
            {
                var res = await _userService.GetUserForLogin(login);
                switch (res)
                {
                    case LoginUserResult.NotActivated:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning,
                            "اکانت شما فعال نیست", new
                            {
                                IsActive = false,
                                mobile = login.Mobile
                            }));
                    case LoginUserResult.NotFound:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "کاربر مورد نظر یافت نشد", null));
                    case LoginUserResult.Success:
                        var user = await _userService.GetUserByMobile(login.Mobile);
                        var token = user.GenerateJwtToken(_configuration);
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            "عمیات با موقیت انجام شد", new
                            {
                                mobile = user.Mobile,
                                token = token
                            }));
                }
            }
            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, "اطلاعات وارد شده نادرست است", null));
        }

        [HttpPost("recover-user-password")]
        public async Task<ActionResult<OperationResponse>> RecoverUserPassword([FromBody]ForgotPassUserDTO forgot)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.RecoverUserPassword(forgot);
                switch (res)
                {
                    case ForgotPassUserResult.NotFound:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            "مشکلی پیش امده است", null));
                    case ForgotPassUserResult.Success:
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            "عمیات با موقیت انجام شد", null));
                }

            }
            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                "مشکلی پیش امده است", null));
        }

    }
}
