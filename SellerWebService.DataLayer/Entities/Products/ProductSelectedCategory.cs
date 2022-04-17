namespace SellerWebService.DataLayer.Entities.Products
{
    public class ProductSelectedCategory : BaseEntity
    {
        #region prop

        public long ProductId { get; set; }
        public long ProductCategoryId { get; set; }

        #endregion

        #region relations

        public Product Product { get; set; }
        public ProductCategory ProductCategory { get; set; }
        #endregion
    }
}
