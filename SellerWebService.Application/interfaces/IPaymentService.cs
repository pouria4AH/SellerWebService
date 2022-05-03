using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using SellerWebService.DataLayer.DTOs.Payment;

namespace SellerWebService.Application.interfaces
{
    public interface IPaymentService 
    {
        Task<object> Payment(int amount, string description, string callbackUrl , IConfiguration configuration);
        Task<ValidateResultDto> Validate(int amount, string authority, string status);
    }
}
