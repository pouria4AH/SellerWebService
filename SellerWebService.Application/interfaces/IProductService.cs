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

        #region selected category
        Task AddSelectedCategory(long productId, List<long> selectedCategoryId);
        Task RemoveSelectedCategory(long productId);
        #endregion

        #region product category

        Task<CreateOurEditProductCategoryResult> CreateProductCategory(CreateProductCategoryDto productCategory);
        //Task<EditProductCategoryDto> GetProductCategory(long productId);
        Task<List<EditProductCategoryDto>> GetAllProductCategory();
        Task<CreateOurEditProductCategoryResult> EditProductCategory(EditProductCategoryDto productCategory);
        Task<EditProductCategoryDto> GetProductCategoryById(long id);

        #endregion

        #region product


        #endregion
    }
}
