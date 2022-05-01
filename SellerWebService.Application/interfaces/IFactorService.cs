using SellerWebService.DataLayer.DTOs.Factor;

namespace SellerWebService.Application.interfaces
{
    public interface IFactorService : IAsyncDisposable
    {
        Task<Guid> CreateBlankFactor(CreateFactorDto factor);
        Task<bool> CreateFactorDetails(CreateFactorDetailsDto factorDetails);
    }
}
