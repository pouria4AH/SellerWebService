using Microsoft.AspNetCore.Http;
using SellerWebService.DataLayer.DTOs;
using SellerWebService.DataLayer.DTOs.Products;
using SellerWebService.DataLayer.Entities.Products;

namespace SellerWebService.Application.interfaces
{
    public interface IProductService : IAsyncDisposable
    {
        #region product feature category
        Task<List<EditProductFeatureCategoryDto>> GetProductFeatureCategories(Guid storeCode);
        Task<CreateOurEditProductFeatureCategoryResult> CreateFeatureCategory(CreateProductFeatureCategoryDto featureCategory, Guid storeCode);
        Task<CreateOurEditProductFeatureCategoryResult> EditFeatureCategory(EditProductFeatureCategoryDto featureCategory, Guid storeCode);
        #endregion

        #region selected category
        //Task AddSelectedCategory(long productId, List<long> selectedCategoryId);
        //Task RemoveSelectedCategory(long productId);
        #endregion

        #region product category

        //Task<CreateOurEditProductCategoryResult> CreateProductCategory(CreateProductCategoryDto productCategory);
        ////Task<EditProductCategoryDto> GetProductCategory(long productId);
        //Task<List<ReadProductCategoryDto>> GetAllProductCategory();
        //Task<CreateOurEditProductCategoryResult> EditProductCategory(EditProductCategoryDto productCategory);
        //Task<ReadProductCategoryDto> GetProductCategoryById(long id);
        //Task<bool> ChangeProductCategoryActiveState(long id);

        #endregion

        #region product

        Task<CreateOurEditProductResult> CreateProduct(CreateProductDto product, Guid storeCode);
        Task<List<ReadProductDto>> GetAllProduct(Guid storeCode);
        Task<ReadProductDto> GetProductById(long id);
        Task<CreateOurEditProductResult> EditProduct(EditProductDto product, Guid storeCode);
        Task<bool> ChangeProductActiveState(long id, Guid storeCode);

        #endregion

        #region product feature
        Task<CreateGroupProductFeatureResult> CreateGroupOfProductFeature(
            CreateGroupProductFeatureDto groupProductFeature, Guid storeCode);
        Task<bool> DeleteGroup(long groupId, Guid storeCode);
        Task<List<ReadGroupProductFeatureDto>> GetGroupsForProduct(long productId);
        Task<CreateOrEditProductFeatureResult> CreateProductFeature(CreateProductFeatureDto feature, Guid storeCode);
        Task<CreateOrEditProductFeatureResult> EditProductFeature(EditProductFeatureDto feature, Guid storeCode);

        #endregion

        #region product gallery

        Task<List<ProductGallery>> GetAllProductGallery(long id);
        Task<CreateOurEditProductGalleryDTO> GetProductGalleryFourEdit(long galleryId,Guid storeCode);
        Task<CreateOurEditProductGalleryResult> CreateProductGallery(CreateOurEditProductGalleryDTO createOurEdit,
            long productId, Guid storeCode);
        Task<CreateOurEditProductGalleryResult> EditProductGallery(long galleryId,
            CreateOurEditProductGalleryDTO gallery, Guid storeCode);

        #endregion
    }
}
