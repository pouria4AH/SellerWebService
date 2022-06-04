using _0_framework.Entities;
using SellerWebService.DataLayer.Entities.Store;

namespace SellerWebService.DataLayer.Entities.Products
{
    public class ProductFeatureCategory : BaseEntity
    {
        #region props
        public long StoreDataId { get; set; }

        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }
        #endregion

        #region relations
        public ICollection<GroupForProductFeature> GroupForProductFeatures { get; set; }
        public StoreData StoreData { get; set; }
        #endregion

    }
}
