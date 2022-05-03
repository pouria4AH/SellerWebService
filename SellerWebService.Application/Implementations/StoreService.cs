using _0_framework.Account;
using Microsoft.EntityFrameworkCore;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Store;
using SellerWebService.DataLayer.Entities.Account;
using SellerWebService.DataLayer.Entities.Store;
using SellerWebService.DataLayer.Repository;

namespace SellerWebService.Application.Implementations
{
    public class StoreService : IStoreService

    {
        #region ctor
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<StoreData> _storeRepository;
        private readonly IGenericRepository<StoreDetails> _storeDetailsRepository;

        public StoreService(IGenericRepository<User> userRepository, IGenericRepository<StoreData> storeRepository, IGenericRepository<StoreDetails> storeDetailsRepository)
        {
            _userRepository = userRepository;
            _storeRepository = storeRepository;
            _storeDetailsRepository = storeDetailsRepository;
        }

        #endregion
        public async Task<RegisterStoreResult> RegisterStore(RegisterStoreDto store, Guid userCode)
        {
            try
            {
                var user = await _userRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(x => x.UniqueCode == userCode && x.Role != AccountRole.Seller && x.Role != AccountRole.SellerEmployee);
                if (user == null) return RegisterStoreResult.UserNotFound;
                var IsExists = await _storeRepository.GetQuery().AsQueryable().AnyAsync(x => x.PersonalId == store.PersonalId);
                if (IsExists) return RegisterStoreResult.PersonalIdExists;
                StoreData newStore = new StoreData
                {
                    CompanyName = store.CompanyName,
                    Mobile = user.Mobile,
                    OwnerFirstName = user.FirstName,
                    OwnerLastName = user.LastName,
                    PersonalId = store.PersonalId,
                    UniqueCode = Guid.NewGuid(),
                    IsActive = false,
                };
                if (store.Address != null) newStore.Address = store.Address;
                if (store.ZipCode != null) newStore.ZipCode = store.ZipCode;
                await _storeRepository.AddEntity(newStore);
                await _storeRepository.SaveChanges();
                user.StoreDataId = newStore.Id;
                user.StoreCode = newStore.UniqueCode;
                user.Role = AccountRole.Seller;
                _userRepository.EditEntity(user);
                await _userRepository.SaveChanges();
                return RegisterStoreResult.Success;
            }
            catch (Exception e)
            {
                return RegisterStoreResult.Error;
            }
        }

        #region dispose
        public async ValueTask DisposeAsync()
        {
            if (_storeDetailsRepository != null) await _storeDetailsRepository.DisposeAsync();
            if (_storeRepository != null) await _storeRepository.DisposeAsync();
            if (_userRepository != null) await _userRepository.DisposeAsync();

        }
        #endregion
    }
}
