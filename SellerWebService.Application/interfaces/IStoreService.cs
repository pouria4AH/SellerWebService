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
        Task<CreateStoreDetailsResult> CreateStoreDetails(CreateStoreDetailsDto createStoreDetails, Guid storeCode);
        Task<bool> CreateSignature(IFormFile image, Guid storeCode);
        Task<bool> CreateStamp(IFormFile image, Guid storeCode);

        #endregion

    }
}
