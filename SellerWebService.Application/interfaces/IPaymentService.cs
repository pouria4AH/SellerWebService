using Dto.Response.Payment;
using Microsoft.Extensions.Configuration;
using SellerWebService.DataLayer.DTOs.Payment;

namespace SellerWebService.Application.interfaces
{
    public interface IPaymentService 
    {
        Task<Request> Payment(int amount, string description, string callbackUrl , IConfiguration configuration);
        Task<ValidateResultDto> Validate(int amount, string authority, string status, IConfiguration configuration);
    }
}
