using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerWebService.DataLayer.DTOs.Store
{
    public class BankDataDto
    {
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }
        public string ShabaNumber { get; set; }
        public string BankName { get; set; }
        public string Owner { get; set; }
    }
}
