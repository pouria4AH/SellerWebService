using _0_framework.Entities;
using SellerWebService.DataLayer.Entities.Store;

namespace SellerWebService.DataLayer.Entities.Products
{
    public class GroupForProductFeature : BaseEntity
    {
        #region prop

        public long StoreDataId { get; set; }
        public long ProductId { get; set; }
        public long ProductFeatureCategoryId { get; set; }
        public int Order { get; set; }

        #endregion

        #region relations

        public StoreData StoreData { get; set; }
        public Product Product { get; set; }
        public ProductFeatureCategory ProductFeatureCategory { get; set; }
        public ICollection<ProductFeature> ProductFeatures { get; set; }
        //public ICollection<FactorFeatureSelected> FactorFeatureSelecteds { get; set; }
        #endregion
    }
}
