using Microsoft.EntityFrameworkCore;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Customer;
using SellerWebService.DataLayer.Entities.Account;
using SellerWebService.DataLayer.Entities.Store;
using SellerWebService.DataLayer.Repository;

namespace SellerWebService.Application.Implementations
{
    public class CustomerService : ICustomerService
    {
        #region ctor

        private readonly IGenericRepository<Customer> _coustomerRepository;
        private readonly IGenericRepository<StoreData> _storeDataRepository;

        public CustomerService(IGenericRepository<Customer> coustomerRepository,
            IGenericRepository<StoreData> storeDataRepository)
        {
            _coustomerRepository = coustomerRepository;
            _storeDataRepository = storeDataRepository;
        }

        #endregion

        #region crud

        public async Task<CreateOurEditCustomerResult> CreateCustomer(CreateCustomerDto customer, Guid storeCode)
        {
            try
            {
                var res = await _coustomerRepository.GetQuery().AsQueryable().AnyAsync(x =>
                    !x.IsDelete && x.Mobile == customer.Mobile && x.StoreCode == storeCode);
                if (res) return CreateOurEditCustomerResult.IsExist;

                var store = await _storeDataRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => x.UniqueCode == storeCode);
                if (store == null) return CreateOurEditCustomerResult.Error;
                Customer newCustomer = new Customer
                {
                    UniqueCode = Guid.NewGuid(),
                    StoreCode = storeCode,
                    Mobile = customer.Mobile,
                    StoreDataId = store.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                };
                if (customer.CompanyName != null) newCustomer.CompanyName = customer.CompanyName;
                if (customer.Email != null) newCustomer.Email = customer.Email;
                if (customer.Address != null) newCustomer.Address = customer.Address;
                if (customer.ZipCode != null) newCustomer.ZipCode = customer.ZipCode;

                await _coustomerRepository.AddEntity(newCustomer);
                await _coustomerRepository.SaveChanges();
                return CreateOurEditCustomerResult.Success;
            }
            catch (Exception e)
            {
                return CreateOurEditCustomerResult.Error;
            }
        }

        public async Task<CreateOurEditCustomerResult> EditCustomer(EditCustomerDto customer, Guid storeCode)
        {
            try
            {
                var mainCustomer = await _coustomerRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x =>
                        x.UniqueCode == customer.CustomerCode && x.StoreCode == storeCode && !x.IsDelete);
                if (mainCustomer == null) return CreateOurEditCustomerResult.NotFound;
                if (customer.CompanyName != null) mainCustomer.CompanyName = customer.CompanyName;
                if (customer.Email != null) mainCustomer.Email = customer.Email;
                if (customer.Address != null) mainCustomer.Address = customer.Address;
                if (customer.ZipCode != null) mainCustomer.ZipCode = customer.ZipCode;
                mainCustomer.FirstName = customer.FirstName;
                mainCustomer.LastName = customer.LastName;
                mainCustomer.Mobile = customer.Mobile;
                _coustomerRepository.EditEntity(mainCustomer);
                await _coustomerRepository.SaveChanges();
                return CreateOurEditCustomerResult.Success;
            }
            catch (Exception e)
            {
                return CreateOurEditCustomerResult.Error;
            }
        }

        public async Task<bool> DeleteCustomer(Guid customerCode, Guid storeCode)
        {
            try
            {
                var customer = await _coustomerRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => x.UniqueCode == customerCode && x.StoreCode == storeCode && !x.IsDelete);
                if (customer == null) return false;
                _coustomerRepository.DeleteEntity(customer);
                await _coustomerRepository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<ReadCustomerDto> GetCustomer(Guid customerCode, Guid storeCode)
        {
            var customer = await _coustomerRepository.GetQuery().AsQueryable()
                .SingleOrDefaultAsync(x => !x.IsDelete && x.UniqueCode == customerCode && x.StoreCode == storeCode);
            if (customer == null) return null;
            return new ReadCustomerDto
            {
                StoreCode = storeCode,
                UniqueCode = customer.UniqueCode,
                Mobile = customer.Mobile,
                FirstName = customer.FirstName,
                Address = customer.Address,
                LastName = customer.LastName,
                CompanyName = customer.CompanyName,
                Email = customer.Email,
                ZipCode = customer.ZipCode
            };
        }

        #endregion

        #region dispose

        public async ValueTask DisposeAsync()
        {
            if (_coustomerRepository != null) await _coustomerRepository.DisposeAsync();
        }

        #endregion
    }
}