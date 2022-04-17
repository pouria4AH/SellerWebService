namespace SellerWebService.DataLayer.Entities.Product
{
    public class ProductFeatureCategory : BaseEntity
    {
        #region props

        public long ProductFeaturesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        #endregion

        #region relations

        public ICollection<ProductFeature> ProductFeatures { get; set; }
        #endregion

    }
}
