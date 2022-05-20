using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerWebService.DataLayer.DTOs.Customer
{
    public class SearchCustomerDto
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? Mobile { get; set; }
    }
}
