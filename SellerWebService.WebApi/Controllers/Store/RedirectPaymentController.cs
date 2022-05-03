using Microsoft.AspNetCore.Mvc;
using SellerWebService.Application.interfaces;

namespace SellerWebService.WebApi.Controllers.Store
{
    [Route("payment/redirectResult")]
    public class RedirectPaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;

        public RedirectPaymentController(IPaymentService paymentService, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _configuration = configuration;
        }

      
    }
}
