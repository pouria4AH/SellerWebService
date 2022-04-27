using Microsoft.EntityFrameworkCore;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Account;
using SellerWebService.DataLayer.Entities.Account;
using SellerWebService.DataLayer.Repository;

namespace SellerWebService.Application.Implementations
{
    public class UserService : IUserService
    {
        #region ctor
        private readonly IPasswordHelper _passwordHelper;
        private readonly IGenericRepository<User> _userRepository;
        public UserService(IPasswordHelper passwordHelper, IGenericRepository<User> userRepository)
        {
            _passwordHelper = passwordHelper;
            _userRepository = userRepository;
        }
        #endregion

        public async Task<RegisterUserResult> RegisterUser(RegisterUserDTO register, string role)
        {
            try
            {
                if (!await IsUserExistsByMobileNumber(register.Mobile))
                {
                    var user = new User
                    {
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        Mobile = register.Mobile,
                        Password = _passwordHelper.EncodePasswordMd5(register.Password),
                        MobileActiveCode = new Random().Next(10000, 99999).ToString(),
                        EmailActiveCode = Guid.NewGuid().ToString("N"),
                        Role = role,
                        IsEmailActive = false,
                        IsMobileActive = false,
                        UniqueCode = Guid.NewGuid(),
                    };
                    await _userRepository.AddEntity(user);
                    await _userRepository.SaveChanges();
                    await SendActiveCode(user.MobileActiveCode);
                    return RegisterUserResult.Success;
                }
                return RegisterUserResult.MobileExists;
            }
            catch (Exception e)
            {
                return RegisterUserResult.Error;
            }
        }

        public async Task<bool> IsUserExistsByMobileNumber(string mobile)
        {
            return await _userRepository.GetQuery().AsQueryable().AnyAsync(x => x.Mobile == mobile);
        }

        public async Task<ActiveMobileState> ActiveMobile(ActivateMobileDTO activate)
        {
            var user = await _userRepository.GetQuery().AsQueryable()
                .SingleOrDefaultAsync(x => x.Mobile == activate.Mobile);
            if (user != null)
            {
                if (user.IsMobileActive) return ActiveMobileState.MobileIsActiveAlready;
                
                if (user.MobileActiveCode == activate.MobileActiveCode)
                {
                    if (user.LastUpdateDate.AddMinutes(10) < DateTime.Now)
                    {
                        user.MobileActiveCode = new Random().Next(100000, 999999).ToString();
                        await SendActiveCode(user.MobileActiveCode);
                        user.LastUpdateDate = DateTime.Now;
                        await _userRepository.SaveChanges();
                        return ActiveMobileState.ExpiredCode;
                    }
                    user.IsMobileActive = true;
                    user.MobileActiveCode = new Random().Next(100000, 999999).ToString();
                    user.LastUpdateDate = DateTime.Now;
                    await _userRepository.SaveChanges();
                    return ActiveMobileState.Success;
                }
                return ActiveMobileState.CodeIsWrong;
            }
            return ActiveMobileState.UserNotFound;
        }

        private async Task<bool> SendActiveCode(string code)
        {
            Console.WriteLine(code);
            return true;
        }
        #region dispose


        public async ValueTask DisposeAsync()
        {
           /*if(_userRepository != null)*/ await _userRepository.DisposeAsync();
        }

        #endregion
    }
}
