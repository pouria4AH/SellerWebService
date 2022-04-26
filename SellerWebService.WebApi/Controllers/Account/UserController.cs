using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Account;

namespace SellerWebService.WebApi.Controllers.Account
{
    [Route("api/Account/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register-user")]
        public async Task<ActionResult<OperationResponse>> Register([FromBody]RegisterUserDTO register)
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
    }
}
