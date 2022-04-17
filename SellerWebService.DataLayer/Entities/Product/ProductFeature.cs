using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerWebService.DataLayer.Entities.Product
{
    public class ProductFeature : BaseEntity
    {
        #region props
        public long ProductFeatureCategoryId { get; set; }
        public long ProductId { get; set; }
        [Display(Name = "نام ویژگی")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(160, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "قیمت اضافه")]
        [Range(0, long.MaxValue)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long ExtraPrice { get; set; }
        #endregion

        #region relations

        public ProductFeatureCategory ProductFeatureCategory { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}
