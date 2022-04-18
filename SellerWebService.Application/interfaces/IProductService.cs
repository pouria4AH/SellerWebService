using SellerWebService.DataLayer.DTOs.Products;

namespace SellerWebService.Application.interfaces
{
    public interface IProductService : IAsyncDisposable
    {
        #region product feature category
        Task<List<EditProductFeatureCategoryDto>> GetProductFeatureCategories();
        Task<CreateOurEditProductFeatureCategoryResult> CreateFeatureCategory(CreateProductFeatureCategoryDto featureCategory);
        Task<CreateOurEditProductFeatureCategoryResult> EditFeatureCategory(EditProductFeatureCategoryDto featureCategory);
        #endregion

        #region Count of product

        Task<List<EditCountDto>> GetAllCountForProduct(long productId);
        Task<CreateOurEditCountResult> CreateCount(CreateCountDto count);
        #endregion
    }
}
