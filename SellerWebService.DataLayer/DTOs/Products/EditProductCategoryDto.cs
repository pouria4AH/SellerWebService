using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerWebService.DataLayer.DTOs.Products
{
    public class EditProductCategoryDto : CreateProductCategoryDto
    {
        public long Id { get; set; }
    }
}
