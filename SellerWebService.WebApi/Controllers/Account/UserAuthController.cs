using _0_framework.Account;
using _0_framework.Messages;
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
                string role = AccountRole.User;
                var res = await _userService.RegisterUser(register, role);
                switch (res)
                {
                    case RegisterUserResult.Error:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            ApplicationMessages.Error, null));
                    case RegisterUserResult.Success:
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            ApplicationMessages.Success, new
                            {
                                mobile = register.Mobile,
                                firstName = register.FirstName,
                                lastName = register.LastName,
                            }));
                    case RegisterUserResult.MobileExists:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning,
                            ApplicationMessages.MobileExists, null));
                }
            }
            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                ApplicationMessages.Error, null));
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
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Warning,
                            ApplicationMessages.ActiveCodeIsExpired, null));
                    case ActiveMobileState.MobileIsActiveAlready:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Info,
                            ApplicationMessages.MobileIsActive, null));
                    case ActiveMobileState.UserNotFound:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            ApplicationMessages.UserNotFound, null));
                    case ActiveMobileState.CodeIsWrong:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            ApplicationMessages.ActiveCodeIsWrong, null));
                    case ActiveMobileState.Success:
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            ApplicationMessages.MobileIsActivated, null));
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
                            ApplicationMessages.UserNotActive, new
                            {
                                IsActive = false,
                                mobile = login.Mobile
                            }));
                    case LoginUserResult.NotFound:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            ApplicationMessages.UserNotFound, null));
                    case LoginUserResult.Success:
                        var user = await _userService.GetUserByMobile(login.Mobile);
                        var token = user.GenerateJwtToken(_configuration);
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            ApplicationMessages.Success, new
                            {
                                mobile = user.Mobile,
                                token = token
                            }));
                }
            }
            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger, ApplicationMessages.ModelIsNotValid, null));
        }

        [HttpPost("recover-user-password")]
        public async Task<ActionResult<OperationResponse>> RecoverUserPassword([FromBody] ForgotPassUserDTO forgot)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.RecoverUserPassword(forgot);
                switch (res)
                {
                    case ForgotPassUserResult.NotFound:
                        return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                            ApplicationMessages.MobileNotFound, null));
                    case ForgotPassUserResult.Success:
                        return Ok(OperationResponse.SendStatus(OperationResponseStatusType.Success,
                            ApplicationMessages.Success, null));
                }
            }
            return BadRequest(OperationResponse.SendStatus(OperationResponseStatusType.Danger,
                ApplicationMessages.Error, null));
        }

    }
}
