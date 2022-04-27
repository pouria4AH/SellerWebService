using SellerWebService.DataLayer.DTOs.Account;
using SellerWebService.DataLayer.Entities.Account;

namespace SellerWebService.Application.interfaces
{
    public interface IUserService : IAsyncDisposable
    {
        Task<RegisterUserResult> RegisterUser(RegisterUserDTO register, string role);
        Task<bool> IsUserExistsByMobileNumber(string mobile);
        Task<ActiveMobileState> ActiveMobile(ActivateMobileDTO activate);
        Task<LoginUserResult> GetUserForLogin(LoginUserDTO login);
        Task<User> GetUserByMobile(string mobile);
        Task<ForgotPassUserResult> RecoverUserPassword(ForgotPassUserDTO forgot);
    }
}
