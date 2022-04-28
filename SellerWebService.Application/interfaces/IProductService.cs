using Microsoft.AspNetCore.Http;
using SellerWebService.DataLayer.DTOs;
using SellerWebService.DataLayer.DTOs.Products;
using SellerWebService.DataLayer.Entities.Products;

namespace SellerWebService.Application.interfaces
{
    public interface IProductService : IAsyncDisposable
    {
        #region product feature category
        Task<List<EditProductFeatureCategoryDto>> GetProductFeatureCategories();
        Task<CreateOurEditProductFeatureCategoryResult> CreateFeatureCategory(CreateProductFeatureCategoryDto featureCategory);
        Task<CreateOurEditProductFeatureCategoryResult> EditFeatureCategory(EditProductFeatureCategoryDto featureCategory);
        #endregion

        #region selected category
        Task AddSelectedCategory(long productId, List<long> selectedCategoryId);
        Task RemoveSelectedCategory(long productId);
        #endregion

        #region product category

        Task<CreateOurEditProductCategoryResult> CreateProductCategory(CreateProductCategoryDto productCategory);
        //Task<EditProductCategoryDto> GetProductCategory(long productId);
        Task<List<ReadProductCategoryDto>> GetAllProductCategory();
        Task<CreateOurEditProductCategoryResult> EditProductCategory(EditProductCategoryDto productCategory);
        Task<ReadProductCategoryDto> GetProductCategoryById(long id);
        Task<bool> ChangeProductCategoryActiveState(long id);

        #endregion

        #region product

        Task<CreateOurEditProductResult> CreateProduct(CreateProductDto product);
        Task<List<ReadProductDto>> GetAllProduct();
        Task<ReadProductDto> GetProductById(long id);
        Task<CreateOurEditProductResult> EditProduct(EditProductDto product);
        Task<bool> ChangeProductActiveState(long id);

        #endregion

        #region product feature
        Task<CreateGroupProductFeatureResult> CreateGroupOfProductFeature(
            CreateGroupProductFeatureDto groupProductFeature);
        Task<bool> DeleteGroup(long groupId);
        Task<List<ReadGroupProductFeatureDto>> GetGroupsForProduct(long productId);
        Task<CreateOrEditProductFeatureResult> CreateProductFeature(CreateProductFeatureDto feature);
        Task<CreateOrEditProductFeatureResult> EditProductFeature(EditProductFeatureDto feature);

        #endregion

        #region product gallery

        Task<List<ProductGallery>> GetAllProductGallery(long id);
        Task<CreateOurEditProductGalleryDTO> GetProductGalleryFourEdit(long galleryId);
        Task<CreateOurEditProductGalleryResult> CreateProductGallery(CreateOurEditProductGalleryDTO createOurEdit,
            long productId);
        Task<CreateOurEditProductGalleryResult> EditProductGallery(long galleryId,
            CreateOurEditProductGalleryDTO gallery);

        #endregion
    }
}
