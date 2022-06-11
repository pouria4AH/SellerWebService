using SellerWebService.DataLayer.DTOs.Presell;

namespace SellerWebService.Application.interfaces
{
    public interface IPresellService : IAsyncDisposable
    {
        Task<presellResult> Create(CreatePresellDto presell);
    }
}
