using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_framework.Account;
using Microsoft.EntityFrameworkCore;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Store;
using SellerWebService.DataLayer.Entities.Account;
using SellerWebService.DataLayer.Repository;

namespace SellerWebService.Application.Implementations
{
    public class SellerEmployeeService : ISellerEmployeeService
    {
        #region ctor
        private readonly IGenericRepository<User> _userRepository;

        public SellerEmployeeService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        public async Task<AddSellerEmployeeResult> CreateSellerEmployee(AddSellerEmployeeDto employee, Guid sellerCode)
        {
            try
            {
                var admin = await _userRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => x.UniqueCode == sellerCode && x.IsDelete && x.Role == AccountRole.Seller);
                if (admin == null) return AddSellerEmployeeResult.UserNotFound;
                if (await _userRepository.GetQuery().AsQueryable().Where(x =>
                            x.StoreCode == admin.StoreCode && !x.IsDelete && x.Role == AccountRole.SellerEmployee)
                        .CountAsync() > 5) return AddSellerEmployeeResult.FullEmployee;
                if (await _userRepository.GetQuery().AsQueryable().AnyAsync(x => x.Mobile == employee.Mobile))
                    return AddSellerEmployeeResult.MobileExists;
                var newUser = new User
                {
                    StoreCode = admin.StoreCode,
                    Mobile = employee.Mobile,
                    UniqueCode = Guid.NewGuid(),
                    Role = AccountRole.SellerEmployee,
                    LastName = employee.LastName,
                    FirstName = employee.FirstName,
                    StoreDataId = admin.StoreDataId,
                    Password = employee.Password,
                    IsMobileActive = true

                };
                await _userRepository.AddEntity(newUser);
                await _userRepository.SaveChanges();
                return AddSellerEmployeeResult.Success;
            }
            catch (Exception e)
            {
                return AddSellerEmployeeResult.Error;
            }

        }

        public async Task<bool> ToggleBlockEmployee(Guid storeCode, Guid employeeCode)
        {
            try
            {
                var employee = await _userRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => x.UniqueCode == employeeCode && x.StoreCode == storeCode && x.Role == AccountRole.SellerEmployee && !x.IsDelete);
                if (employee == null) return false;
                employee.IsBlocked = !employee.IsBlocked;
                _userRepository.EditEntity(employee);
                await _userRepository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteEmployee(Guid storeCode, Guid employeeCode)
        {
            try
            {
                var employee = await _userRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => x.UniqueCode == employeeCode && x.StoreCode == storeCode && x.Role == AccountRole.SellerEmployee && !x.IsDelete);
                if (employee == null) return false;
                _userRepository.DeleteEntity(employee);
                await _userRepository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }



        #region dispose
        public async ValueTask DisposeAsync()
        {
            if (_userRepository != null) await _userRepository.DisposeAsync();
        }
        #endregion
    }
}
