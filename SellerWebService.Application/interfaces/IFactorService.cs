using SellerWebService.DataLayer.DTOs.Factor;
using SellerWebService.DataLayer.Entities.Factor;

namespace SellerWebService.Application.interfaces
{
    public interface IFactorService : IAsyncDisposable
    {
        Task<Guid> CreateBlankFactor(CreateFactorDto factor, Guid storeCode);
        Task<bool> CreateFactorDetails(List<CreateFactorDetailsDto> factorDetails, Guid storeCode, Guid factorCode);
        Task<ReadMainFactorDto> GetFinialFactorToConfirm(Guid factorCode, Guid storeCode);
        Task<CreateFactorResult> PublishFactor(Guid factorCode, Guid storeCode);
        Task<ReadFullFactorDto> GetFinialFactor(Guid factorCode, Guid storeCode);
        Task<bool> RejectFactor(Guid factorCode, Guid storeCode);
        Task<bool> ReadyToFinalPayedFactor(Guid factorCode, Guid storeCode);
        Task<bool> DeliveredFactor(Guid factorCode, Guid storeCode);
        Task<bool> AcceptedFactor(AcceptedFactorDto accepted, Guid factorCode, Guid storeCode);
        Task<bool> ReadyFactor(AcceptedFactorDto? accepted, Guid factorCode, Guid storeCode);
        Task<bool> ExpiredFactor(Factor factor);
        Task<FilterFactorDto> FilterFactor(FilterFactorDto filter);
    }
}