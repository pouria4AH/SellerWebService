using SellerWebService.DataLayer.DTOs.Products;

namespace SellerWebService.Application.interfaces
{
    public interface IProductService : IAsyncDisposable
    {
        #region product feature category
        Task<CreateOurEditProductFeatureCategoryResult> CreateFeatureCategory(CreateOurEditProductFeatureCategoryDto featureCategory);
        #endregion
    }
}
