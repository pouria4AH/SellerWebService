using Microsoft.EntityFrameworkCore;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Products;
using SellerWebService.DataLayer.Entities.Products;
using SellerWebService.DataLayer.Repository;

namespace SellerWebService.Application.Implementations
{
    public class ProductService : IProductService
    {
        #region ctor

        private readonly IGenericRepository<ProductFeatureCategory> _productFeatureCategoryRepository;

        public ProductService(IGenericRepository<ProductFeatureCategory> productFeatureCategoryRepository)
        {
            _productFeatureCategoryRepository = productFeatureCategoryRepository;
        }

        #endregion

        #region  product feature category

        public async Task<List<EditProductFeatureCategoryDto>> GetProductFeatureCategories()
        {
            return await _productFeatureCategoryRepository.GetQuery().AsQueryable()
                .Where(x => !x.IsDelete)
                .Select(x =>
                new EditProductFeatureCategoryDto
                {
                    Id = x.Id,
                    Description = x.Description,
                    Name = x.Name
                }).ToListAsync();
        }

        public async Task<CreateOurEditProductFeatureCategoryResult> CreateFeatureCategory(CreateProductFeatureCategoryDto featureCategory)
        {
            if (featureCategory != null)
            {
                var checkExisted = await _productFeatureCategoryRepository.GetQuery().AsQueryable()
                    .AnyAsync(x => x.Name == featureCategory.Name);
                if (checkExisted) return CreateOurEditProductFeatureCategoryResult.IsExisted;

                ProductFeatureCategory newfeatureCategory = new ProductFeatureCategory
                {
                    Name = featureCategory.Name,
                    Description = featureCategory.Description
                };
                await _productFeatureCategoryRepository.AddEntity(newfeatureCategory);
                await _productFeatureCategoryRepository.SaveChanges();
                return CreateOurEditProductFeatureCategoryResult.Success;
            }
            return CreateOurEditProductFeatureCategoryResult.Error;
        }

        public async Task<CreateOurEditProductFeatureCategoryResult> EditFeatureCategory(EditProductFeatureCategoryDto featureCategory)
        {
            if (featureCategory == null) return CreateOurEditProductFeatureCategoryResult.Error; 

            var category = await _productFeatureCategoryRepository.GetQuery().AsQueryable()
                .SingleOrDefaultAsync(x => x.Id == featureCategory.Id && !x.IsDelete);

            if (category == null) return CreateOurEditProductFeatureCategoryResult.NotFound;
            category.Description = featureCategory.Description;
            category.Name = featureCategory.Name;   
            _productFeatureCategoryRepository.EditEntity(category);
            await _productFeatureCategoryRepository.SaveChanges();
            return CreateOurEditProductFeatureCategoryResult.Success;
        }

        #endregion

        #region dipose
        public async ValueTask DisposeAsync()
        {
            await _productFeatureCategoryRepository.DisposeAsync();
        }

        #endregion
    }
}
