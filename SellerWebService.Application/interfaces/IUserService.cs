using SellerWebService.DataLayer.DTOs.Account;
using SellerWebService.DataLayer.Entities.Account;

namespace SellerWebService.Application.interfaces
{
    public interface IUserService : IAsyncDisposable
    {
        Task<RegisterUserResult> RegisterUser(RegisterUserDTO register, string role);
        Task<bool> IsUserExistsByMobileNumber(string mobile);
    }
}
