using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerWebService.DataLayer.DTOs.Products
{
    public class CreateProductFeatureDto
    {
        public long GroupForProductFeatureId { get; set; }
        public long ProductId { get; set; }
        [Display(Name = "نام ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "قیمت اضافه")]
        [Range(0, long.MaxValue)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long ExtraPrice { get; set; }
    }

    public enum CreateOrEditProductFeatureResult
    {
        Error,
        Success,
        IsExisted,
        NotFound
    }
}
