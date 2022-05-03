using SellerWebService.DataLayer.DTOs.Store;
using SellerWebService.DataLayer.Entities.Account;

namespace SellerWebService.Application.interfaces
{
    public interface IStoreService : IAsyncDisposable
    {
        #region Active store
        Task<RegisterStoreResult> RegisterStore(RegisterStoreDto store, Guid userCode);
        //Task<>
        #endregion

    }
}
