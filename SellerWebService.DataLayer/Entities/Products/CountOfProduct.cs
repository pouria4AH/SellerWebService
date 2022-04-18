using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_framework.Entities;

namespace SellerWebService.DataLayer.Entities.Products
{
    public class CountOfProduct : BaseEntity
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public long Count { get; set; }

        public Product Product { get; set; }

    }
}
