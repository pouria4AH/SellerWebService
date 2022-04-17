using _0_framework.Entities;

namespace SellerWebService.DataLayer.Entities.Products
{
    public class ProductFeatureCategory : BaseEntity
    {
        #region props

        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }
        #endregion

        #region relations

        public ICollection<ProductFeature> ProductFeatures { get; set; }
        #endregion

    }
}
