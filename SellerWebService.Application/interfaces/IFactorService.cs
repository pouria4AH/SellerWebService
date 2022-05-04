using SellerWebService.DataLayer.DTOs.Factor;

namespace SellerWebService.Application.interfaces
{
    public interface IFactorService : IAsyncDisposable
    {
        Task<Guid> CreateBlankFactor(CreateFactorDto factor, Guid storeCode);
        Task<bool> CreateFactorDetails(CreateFactorDetailsDto factorDetails, Guid storeCode);
        Task<ReadMainFactorDto> GetFinialFactorToConfirm(Guid factorCode, Guid storeCode);
        Task<CreateFactorResult> PublishFactor(Guid factorCode, Guid storeCode);
    }
}
