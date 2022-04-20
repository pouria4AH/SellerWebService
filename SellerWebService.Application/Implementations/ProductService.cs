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
        private readonly IGenericRepository<ProductCategory> _productCategoryRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductSelectedCategory> _productSelectedCategoryRepository;

        public ProductService(IGenericRepository<ProductFeatureCategory> productFeatureCategoryRepository,
            IGenericRepository<CountOfProduct> countOfProductRepository,
            IGenericRepository<ProductCategory> productCategoryRepository,
            IGenericRepository<Product> productRepository, 
            IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository)
        {
            _productFeatureCategoryRepository = productFeatureCategoryRepository;
            _countOfProductRepository = countOfProductRepository;
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
            _productSelectedCategoryRepository = productSelectedCategoryRepository;
        }

        #endregion

        #region product feature category

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

        public async Task<CreateOurEditProductFeatureCategoryResult> CreateFeatureCategory(
            CreateProductFeatureCategoryDto featureCategory)
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

        public async Task<CreateOurEditProductFeatureCategoryResult> EditFeatureCategory(
            EditProductFeatureCategoryDto featureCategory)
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
            var checkExisted = await _countOfProductRepository.GetQuery().AsQueryable()
                .AnyAsync(x => x.Count == count.Count && x.ProductId == count.ProductId);
            if (checkExisted) return CreateOurEditCountResult.IsExisted;

            var checkProductId = await _productRepository.GetQuery().AsQueryable()
                .AnyAsync(x => x.Id == count.ProductId);
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


        #region selected category

        public async Task AddSelectedCategory(long productId, List<long> selectedCategories)
        {
            var productSelectedCategories = new List<ProductSelectedCategory>();
            foreach (var categoryId in selectedCategories)
            {
                productSelectedCategories.Add(new ProductSelectedCategory
                {
                    ProductId = productId,
                    ProductCategoryId = categoryId
                });
            }

            await _productSelectedCategoryRepository.AddRangeEntities(productSelectedCategories);
            await _productSelectedCategoryRepository.SaveChanges();
        }

        public async Task RemoveSelectedCategory(long productId)
        {
            var selectedCategory = await _productSelectedCategoryRepository.GetQuery().AsQueryable()
                .Where(x => x.ProductId == productId).ToListAsync();
            _productSelectedCategoryRepository.DeletePermanentEntities(selectedCategory);
        }

        #endregion

        #region product category

        public async Task<CreateOurEditProductCategoryResult> CreateProductCategory(
            CreateProductCategoryDto productCategory)
        {
            //if (productCategory.ParentId == 0) return CreateOurEditProductCategoryResult.Error;
            //if (productCategory.ParentId != null && productCategory.ParentId != 0)
            //{
            //    var check = await _productCategoryRepository.GetQuery().AsQueryable()
            //        .AnyAsync(x => x.ParentId == productCategory.ParentId);
            //    if (!check) return CreateOurEditProductCategoryResult.ParentNotExisted;
            //}

            var checkForExisted = await _productCategoryRepository.GetQuery().AsQueryable()
                .AnyAsync(x => x.Name == productCategory.Name || x.SeoTitle == productCategory.SeoTitle);
            if (checkForExisted) return CreateOurEditProductCategoryResult.IsExisted;
            var newCategory = new ProductCategory
            {
                Name = productCategory.Name,
                ExrernalLink = productCategory.ExrernalLink,
                InternalLink = productCategory.InternalLink,
                Keywords = productCategory.Keywords,
                //ParentId = productCategory.ParentId,
                MetaDescription = productCategory.MetaDescription,
                Description = productCategory.Description,
                PictureName = productCategory.PictureAddress,
                PictureAlt = productCategory.PictureAlt,
                PictureTitle = productCategory.PictureTitle,
                SeoTitle = productCategory.SeoTitle,
                ShortDescription = productCategory.ShortDescription,
                IsActive = productCategory.IsActive
            };
            //if(productCategory.ParentId != null || productCategory.ParentId != 0) newCategory.ParentId = productCategory.ParentId;
            await _productCategoryRepository.AddEntity(newCategory);
            await _productCategoryRepository.SaveChanges();
            return CreateOurEditProductCategoryResult.Success;

        }

        public async Task<List<EditProductCategoryDto>> GetAllProductCategory()
        {
            return await _productCategoryRepository.GetQuery().AsQueryable().Where(x => !x.IsDelete)
                .Select(x => new EditProductCategoryDto
                {
                    //ParentId = x.ParentId,
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ExrernalLink = x.ExrernalLink,
                    InternalLink = x.InternalLink,
                    IsActive = x.IsActive,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    PictureAddress = x.PictureName,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    SeoTitle = x.SeoTitle,
                    ShortDescription = x.ShortDescription
                })
                .ToListAsync();

        }

        public async Task<CreateOurEditProductCategoryResult> EditProductCategory(
            EditProductCategoryDto productCategory)
        {
            var mainCategory = await _productCategoryRepository.GetEntityById(productCategory.Id);
            if (mainCategory == null || mainCategory.IsDelete) return CreateOurEditProductCategoryResult.NotFound;

            mainCategory.Keywords = productCategory.Keywords;
            mainCategory.MetaDescription = productCategory.MetaDescription;
            mainCategory.PictureName = productCategory.PictureAddress;
            mainCategory.PictureAlt = productCategory.PictureAlt;
            mainCategory.PictureTitle = productCategory.PictureTitle;
            mainCategory.SeoTitle = productCategory.SeoTitle;
            mainCategory.ShortDescription = productCategory.ShortDescription;
            mainCategory.Description = productCategory.Description;
            mainCategory.ExrernalLink = productCategory.ExrernalLink;
            mainCategory.InternalLink = productCategory.InternalLink;
            mainCategory.Name = productCategory.Name;
            mainCategory.IsActive = productCategory.IsActive;


            _productCategoryRepository.EditEntity(mainCategory);
            await _productCategoryRepository.SaveChanges();
            return CreateOurEditProductCategoryResult.Success;

        }

        public async Task<EditProductCategoryDto> GetProductCategoryById(long id)
        {
            var category = await _productCategoryRepository.GetEntityById(id);
            if(category == null || category.IsDelete) return null;
            return new EditProductCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ExrernalLink = category.ExrernalLink,
                InternalLink = category.InternalLink,
                IsActive = category.IsActive,
                Keywords = category.Keywords,
                MetaDescription = category.MetaDescription,
                PictureAddress = category.PictureName,
                PictureAlt = category.PictureAlt,
                PictureTitle = category.PictureTitle,
                SeoTitle = category.SeoTitle,
                ShortDescription = category.ShortDescription
            };
        }
        #endregion

        #region dipose
        public async ValueTask DisposeAsync()
        {
           if(_productFeatureCategoryRepository != null) await _productFeatureCategoryRepository.DisposeAsync();
           if(_productCategoryRepository != null) await _productCategoryRepository.DisposeAsync();
           if(_countOfProductRepository != null) await _countOfProductRepository.DisposeAsync();
           if(_productRepository != null) await _productRepository.DisposeAsync();
           if(_productSelectedCategoryRepository != null) await _productSelectedCategoryRepository.DisposeAsync();
        }

        #endregion
    }
}
