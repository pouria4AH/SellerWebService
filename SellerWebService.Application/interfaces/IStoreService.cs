using Microsoft.AspNetCore.Http;
using SellerWebService.DataLayer.DTOs.Payment;
using SellerWebService.DataLayer.DTOs.Store;
using SellerWebService.DataLayer.Entities.Account;

namespace SellerWebService.Application.interfaces
{
    public interface IStoreService : IAsyncDisposable
    {
        #region Active store
        Task<RegisterStoreResult> RegisterStore(RegisterStoreDto store, Guid userCode);
        Task<bool> ActiveStore(int refId,Guid userCode);
        #endregion

        #region store details
        Task<bool> IsHaveStoreDetails(Guid storeCode);
        Task<CreateStoreDetailsResult> CreateStoreDetails(CreateStoreDetailsDto createStoreDetails, IFormFile image, Guid storeCode);
        Task<bool> CreateSignature(IFormFile image, Guid storeCode);
        Task<bool> CreateStamp(IFormFile image, Guid storeCode);
        //Task<bool> CreateLogo(IFormFile image, Guid storeCode);
        Task<CreateStoreDetailsResult> EditStoreDetails(CreateStoreDetailsDto storeDetails, IFormFile image, Guid storeCode);
        Task<bool> EditSignature(IFormFile image, Guid storeCode);
        Task<bool> EditStamp(IFormFile image, Guid storeCode);
        //Task<bool> EditLogo(IFormFile image, Guid storeCode);
        #endregion

        #region bank data
        Task<bool> CreateBankData(BankDataDto bankData, Guid storeCode);
        Task<bool> EditBankData(BankDataDto bankData, Guid storeCode);
        Task<BankDataDto> GetBankData(Guid storeCode);
        Task<bool> HaveAnyBankData(Guid storeCode);

        #endregion
    }
}
