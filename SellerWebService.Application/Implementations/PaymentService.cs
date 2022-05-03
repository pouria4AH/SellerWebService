﻿using Dto.Payment;
using Dto.Response.Payment;
using Microsoft.Extensions.Configuration;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Payment;
using ZarinPal.Class;

namespace SellerWebService.Application.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly Payment _payment;

        public PaymentService()
        {
            var expose = new Expose();
            _payment = expose.CreatePayment();
        }
        public async Task<Request> Payment(int amount, string description, string callbackUrl, IConfiguration configuration)
        {
            var res = await _payment.Request(new DtoRequest()
            {
                Amount = amount,
                Description = description,
                CallbackUrl = callbackUrl,
                MerchantId = configuration.GetSection("ZarinPal:MerchantId").Value
            }, ZarinPal.Class.Payment.Mode.sandbox);
            return res;
        }

        public async Task<ValidateResultDto> Validate(int amount, string authority, string status, IConfiguration configuration)
        {
            var verification = await _payment.Verification(new DtoVerification()
            {
                Amount = amount,
                Authority = authority,
                MerchantId = configuration.GetSection("ZarinPal:MerchantId").Value
            }, ZarinPal.Class.Payment.Mode.sandbox);

            if (status != null && authority != null && status == "OK")
            {
                return new ValidateResultDto
                {
                    IsSuccess = true,
                    RefId = verification.RefId
                };
            }

            return new ValidateResultDto
            {
                IsSuccess = false,
                RefId = 0
            };
        }
    }
}
