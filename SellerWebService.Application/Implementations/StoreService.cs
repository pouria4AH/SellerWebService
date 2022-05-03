using System.Xml;
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
        private readonly IGenericRepository<StorePayment> _storePaymentRepository;

        public StoreService(IGenericRepository<User> userRepository, IGenericRepository<StoreData> storeRepository, IGenericRepository<StoreDetails> storeDetailsRepository, IGenericRepository<StorePayment> storePaymentRepository)
        {
            _userRepository = userRepository;
            _storeRepository = storeRepository;
            _storeDetailsRepository = storeDetailsRepository;
            _storePaymentRepository = storePaymentRepository;
        }

        #endregion

        #region Active store
        public async Task<RegisterStoreResult> RegisterStore(RegisterStoreDto store, Guid userCode)
        {
            try
            {
                var user = await _userRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(x => x.UniqueCode == userCode && x.Role == AccountRole.User && !x.IsDelete);
                if (user == null) return RegisterStoreResult.UserNotFound;
                var IsExists = await _storeRepository.GetQuery().Include(x => x.Users)
                    .AsQueryable().AnyAsync(x => x.PersonalId == store.PersonalId && x.Users.Any(y => y.UniqueCode == user.UniqueCode && !x.IsDelete));
                if (user.StoreDataId != null) return RegisterStoreResult.StoreIsExists;
                if (IsExists) return RegisterStoreResult.StoreIsExists;
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

        public async Task<bool> ActiveStore(int refId, Guid userCode)
        {
            var user = await _userRepository.GetQuery().Include(x => x.StoreData).AsQueryable()
                .SingleOrDefaultAsync(x => x.UniqueCode == userCode);
            if (user == null) return false;
            if (user.StoreData.IsActive) return false;
            var newPayment = new StorePayment
            {
                IsPayed = true,
                PaymentDate = DateTime.Now,
                StoreCode = user.StoreData.UniqueCode,
                UserCode = user.UniqueCode,
                StoreDataId = user.StoreData.Id,
                TracingCode = refId.ToString()
            };
            user.StoreData.IsActive =true;
            await _storeRepository.SaveChanges();
            await _storePaymentRepository.AddEntity(newPayment);
            await _storePaymentRepository.SaveChanges();
            return true;
        }

        #endregion

        #region store details

        public async Task<bool> IsHaveStoreDetails(Guid storeCode)
        {
            return await _storeDetailsRepository.GetQuery().AsQueryable().AnyAsync(x => x.StoreCode == storeCode);
        }

        #endregion

        #region dispose
        public async ValueTask DisposeAsync()
        {
            if (_storeDetailsRepository != null) await _storeDetailsRepository.DisposeAsync();
            if (_storeRepository != null) await _storeRepository.DisposeAsync();
            if (_userRepository != null) await _userRepository.DisposeAsync();
            if (_storePaymentRepository != null) await _storePaymentRepository.DisposeAsync();

        }
        #endregion
    }
}
