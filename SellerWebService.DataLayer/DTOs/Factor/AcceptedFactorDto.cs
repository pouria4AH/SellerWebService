using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellerWebService.DataLayer.Entities.Factor;

namespace SellerWebService.DataLayer.DTOs.Factor
{
    public class AcceptedFactorDto
    {
        public DateTime PaymenyDate { get; set; }
        public string TracingCode { get; set; }
        public FactorPaymentState PaymentState { get; set; }

    }
}
