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
        private readonly IGenericRepository<CountOfProduct> _countOfProductRepository;

        public ProductService(IGenericRepository<ProductFeatureCategory> productFeatureCategoryRepository, IGenericRepository<CountOfProduct> countOfProductRepository)
        {
            _productFeatureCategoryRepository = productFeatureCategoryRepository;
            _countOfProductRepository = countOfProductRepository;
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

        #region count of product
        public async Task<CreateOurEditCountResult> CreateCount(CreateCountDto count)
        {
            if (count == null) return CreateOurEditCountResult.Error;
            var checkExisted = await _countOfProductRepository.GetQuery().AsQueryable().AnyAsync(x => x.Count == count.Count && x.ProductId == count.ProductId);
            if (checkExisted) return CreateOurEditCountResult.IsExisted;

            var checkProductId = await _countOfProductRepository.GetQuery().AsQueryable()
                .AnyAsync(x => x.ProductId == count.ProductId);
            if (!checkProductId) return CreateOurEditCountResult.NotFound;

            CountOfProduct newCount = new CountOfProduct
            {
                Name = count.Name,
                ProductId = count.ProductId,
                Count = count.Count
            };
            await _countOfProductRepository.AddEntity(newCount);
            await _countOfProductRepository.SaveChanges();
            return CreateOurEditCountResult.Success;
        }
        public async Task<List<EditCountDto>> GetAllCountForProduct(long productId)
        {
            if (productId == 0 || productId == null) return null;
            var existed = await _countOfProductRepository.GetQuery().AsQueryable()
                .AnyAsync(x => x.ProductId == productId);
            if (!existed) return null;
            return await _countOfProductRepository.GetQuery().AsQueryable()
                .Where(x => x.ProductId == productId && !x.IsDelete)
                .Select(x => new EditCountDto
                {
                    ProductId = productId,
                    Name = x.Name,
                    Count = x.Count
                }).ToListAsync();
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
