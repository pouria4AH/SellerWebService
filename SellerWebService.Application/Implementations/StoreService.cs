using _0_framework.Account;
using _0_framework.Extensions;
using _0_framework.Utils;
using Microsoft.AspNetCore.Http;
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
        private readonly IGenericRepository<BankData> _bankRepository;
        public StoreService(IGenericRepository<User> userRepository, IGenericRepository<StoreData> storeRepository, IGenericRepository<StoreDetails> storeDetailsRepository, IGenericRepository<StorePayment> storePaymentRepository, IGenericRepository<BankData> bankRepository)
        {
            _userRepository = userRepository;
            _storeRepository = storeRepository;
            _storeDetailsRepository = storeDetailsRepository;
            _storePaymentRepository = storePaymentRepository;
            _bankRepository = bankRepository;
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
            user.StoreData.IsActive = true;
            await _storeRepository.SaveChanges();
            await _storePaymentRepository.AddEntity(newPayment);
            await _storePaymentRepository.SaveChanges();
            return true;
        }

        #endregion

        #region store details

        public async Task<bool> IsHaveStoreDetails(Guid storeCode)
        {
            return await _storeDetailsRepository.GetQuery().AsQueryable().AnyAsync(x => x.StoreCode == storeCode && x.IsActive && !x.IsDelete);
        }

        public async Task<CreateStoreDetailsResult> CreateStoreDetails(CreateStoreDetailsDto createStoreDetails, Guid storeCode)

        {
            try
            {
                var store = await _storeRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => x.UniqueCode == storeCode);
                if (store == null) return CreateStoreDetailsResult.StoreIsNull;
                var newDetails = new StoreDetails
                {
                    Description = createStoreDetails.Description,
                    IsActive = true,
                    Mobile = createStoreDetails.Mobile,
                    StoreCode = storeCode,
                    StoreDataId = store.Id,
                };
                if (createStoreDetails.TelegramNumber != null) newDetails.TelegramNumber = createStoreDetails.TelegramNumber;
                if (createStoreDetails.WhatsappNumber != null) newDetails.WhatsappNumber = createStoreDetails.WhatsappNumber;
                if (createStoreDetails.Email != null) newDetails.Email = createStoreDetails.Email;
                if (createStoreDetails.Phone != null) newDetails.Phone = createStoreDetails.Phone;
                if (createStoreDetails.Instagram != null) newDetails.Instagram = createStoreDetails.Instagram;
                await _storeDetailsRepository.AddEntity(newDetails);
                await _storeDetailsRepository.SaveChanges();
                return CreateStoreDetailsResult.Success;
            }
            catch (Exception e)
            {
                return CreateStoreDetailsResult.Error;
            }

        }

        public async Task<bool> CreateSignature(IFormFile image, Guid storeCode)
        {
            try
            {
                var storeDetails = await _storeDetailsRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => x.StoreCode == storeCode);
                if (storeDetails == null) return false;
                if (image != null && image.IsImage())
                {
                    var imageName = Guid.NewGuid().ToString("N") +
                                    Path.GetExtension(image.FileName);
                    image.AddImageToServer(imageName,
                        PathExtension.StoreDetailsSignatureImageServer, null, null);
                    storeDetails.SigntureImage = imageName;

                    _storeDetailsRepository.EditEntity(storeDetails);
                    await _storeDetailsRepository.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> CreateStamp(IFormFile image, Guid storeCode)
        {
            try
            {
                var storeDetails = await _storeDetailsRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => x.StoreCode == storeCode);
                if (storeDetails == null) return false;
                if (image != null && image.IsImage())
                {
                    var imageName = Guid.NewGuid().ToString("N") +
                                    Path.GetExtension(image.FileName);
                    image.AddImageToServer(imageName,
                        PathExtension.StoreDetailsStampImageServer, null, null);
                    storeDetails.StampImage = imageName;

                    _storeDetailsRepository.EditEntity(storeDetails);
                    await _storeDetailsRepository.SaveChanges();

                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<CreateStoreDetailsResult> EditStoreDetails(CreateStoreDetailsDto storeDetails, Guid storeCode)
        {
            try
            {
                var store = await _storeDetailsRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => x.StoreCode == storeCode);
                if (store == null) return CreateStoreDetailsResult.StoreIsNull;
                store.Description = storeDetails.Description;
                store.Email = storeDetails.Email;
                store.Instagram = storeDetails.Instagram;
                store.WhatsappNumber = storeDetails.WhatsappNumber;
                store.Mobile = storeDetails.Mobile;
                store.TelegramNumber = storeDetails.TelegramNumber;
                store.Phone = storeDetails.Phone;
                _storeDetailsRepository.EditEntity(store);
                await _storeDetailsRepository.SaveChanges();
                return CreateStoreDetailsResult.Success;
            }
            catch (Exception e)
            {
                return CreateStoreDetailsResult.Error;
            }
        }

        public async Task<bool> EditSignature(IFormFile image, Guid storeCode)
        {
            try
            {
                var storeDetails = await _storeDetailsRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => x.StoreCode == storeCode);
                if (storeDetails == null || storeDetails.SigntureImage == null) return false;
                if (image != null && image.IsImage())
                {
                    var imageName = Guid.NewGuid().ToString("N") +
                                    Path.GetExtension(image.FileName);
                    image.AddImageToServer(imageName,
                        PathExtension.StoreDetailsSignatureImageServer, null, null, null, storeDetails.SigntureImage);
                    storeDetails.SigntureImage = imageName;

                    _storeDetailsRepository.EditEntity(storeDetails);
                    await _storeDetailsRepository.SaveChanges();

                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> EditStamp(IFormFile image, Guid storeCode)
        {
            try
            {
                var storeDetails = await _storeDetailsRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => x.StoreCode == storeCode);
                if (storeDetails == null || storeDetails.StampImage == null) return false;
                if (image != null && image.IsImage())
                {
                    var imageName = Guid.NewGuid().ToString("N") +
                                    Path.GetExtension(image.FileName);
                    image.AddImageToServer(imageName,
                        PathExtension.StoreDetailsStampImageServer, null, null, null, storeDetails.StampImage);
                    storeDetails.StampImage = imageName;
                    _storeDetailsRepository.EditEntity(storeDetails);
                    await _storeDetailsRepository.SaveChanges();

                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }



        #endregion

        #region store bank
        public async Task<bool> CreateBankData(BankDataDto bankData, Guid storeCode)
        {
            try
            {
                var store = await _storeRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => x.UniqueCode == storeCode && !x.IsDelete);
                if (store == null) return false;
                var newBank = new BankData
                {
                    StoreCode = store.UniqueCode,
                    StoreDataId = store.Id,
                    AccountNumber = bankData.AccountNumber,
                    BankName = bankData.BankName,
                    CardNumber = bankData.CardNumber,
                    Owner = bankData.Owner,
                    ShabaNumber = bankData.ShabaNumber
                };
                await _bankRepository.AddEntity(newBank);
                await _bankRepository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> EditBankData(BankDataDto bankData, Guid storeCode)
        {
            try
            {
                var mainBank = await _bankRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => !x.IsDelete && x.StoreCode == storeCode);
                if (mainBank == null) return false;
                mainBank.AccountNumber = bankData.AccountNumber;
                mainBank.BankName = bankData.BankName;
                mainBank.CardNumber = bankData.CardNumber;
                mainBank.Owner = bankData.Owner;
                mainBank.ShabaNumber = bankData.ShabaNumber;
                _bankRepository.EditEntity(mainBank);
                await _bankRepository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<BankDataDto> GetBankData(Guid storeCode)
        {
            var data = await _bankRepository.GetQuery().AsQueryable()
                .SingleOrDefaultAsync(x => !x.IsDelete && x.StoreCode == storeCode);
            if (data == null) return null;
            return new BankDataDto
            {
                AccountNumber = data.AccountNumber,
                BankName = data.BankName,
                CardNumber = data.CardNumber,
                Owner = data.Owner,
                ShabaNumber = data.ShabaNumber,
            };
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
