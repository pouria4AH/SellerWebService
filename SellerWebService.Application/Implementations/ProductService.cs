﻿using _0_framework.Extensions;
using _0_framework.Utils;
using Microsoft.AspNetCore.Http;
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

        public async Task CreateCount(long productId, IEnumerable<CreateCountDto> counts)
        {
            List<CountOfProduct> listCount = new List<CountOfProduct>();
            foreach (var count in counts)
            {
                CountOfProduct newCount = new CountOfProduct
                {
                    //Name = count.Name,
                    ProductId = productId,
                    Count = count.Count
                };
                listCount.Add(newCount);
            }
            await _countOfProductRepository.AddRangeEntities(listCount);
            await _countOfProductRepository.SaveChanges();
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

            var checkForExisted = await _productCategoryRepository.GetQuery().AsQueryable()
                .AnyAsync(x => x.Name == productCategory.Name || x.SeoTitle == productCategory.SeoTitle);
            if (checkForExisted) return CreateOurEditProductCategoryResult.IsExisted;

            if (productCategory.Picture == null) return CreateOurEditProductCategoryResult.IsNotImage;
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(productCategory.Picture.FileName);
            var res = productCategory.Picture.AddImageToServer(imageName, PathExtension.ProductCategoryOriginServer, 150, 150,
                PathExtension.ProductCategoryThumbServer);

            if (res)
            {
                var newCategory = new ProductCategory
                {
                    Name = productCategory.Name,
                    ExrernalLink = productCategory.ExrernalLink,
                    InternalLink = productCategory.InternalLink,
                    Keywords = productCategory.Keywords,
                    MetaDescription = productCategory.MetaDescription,
                    Description = productCategory.Description,
                    PictureName = imageName,
                    PictureAlt = productCategory.PictureAlt,
                    PictureTitle = productCategory.PictureTitle,
                    SeoTitle = productCategory.SeoTitle,
                    ShortDescription = productCategory.ShortDescription,
                    IsActive = productCategory.IsActive
                };
                await _productCategoryRepository.AddEntity(newCategory);
                await _productCategoryRepository.SaveChanges();
                return CreateOurEditProductCategoryResult.Success;
            }
            return CreateOurEditProductCategoryResult.Error;
        }

        public async Task<List<ReadProductCategoryDto>> GetAllProductCategory()
        {
            return await _productCategoryRepository.GetQuery().AsQueryable().Where(x => !x.IsDelete)
                .Select(x => new ReadProductCategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ExrernalLink = x.ExrernalLink,
                    InternalLink = x.InternalLink,
                    IsActive = x.IsActive,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    PictureName = x.PictureName,
                    OriginAddress = PathExtension.ProductCategoryOrigin,
                    ThumbAddress = PathExtension.ProductCategoryThumb,
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

            if (productCategory.Picture != null)
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(productCategory.Picture.FileName);
                var res = productCategory.Picture.AddImageToServer(imageName, PathExtension.ProductCategoryOriginServer, 150, 150,
                    PathExtension.ProductCategoryThumbServer, mainCategory.PictureName);
                if (res)
                {
                    mainCategory.PictureName = imageName;
                }
            }

            mainCategory.Keywords = productCategory.Keywords;
            mainCategory.MetaDescription = productCategory.MetaDescription;
            //mainCategory.PictureName = productCategory.Picture;
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

        public async Task<ReadProductCategoryDto> GetProductCategoryById(long id)
        {
            var category = await _productCategoryRepository.GetEntityById(id);
            if (category == null || category.IsDelete) return null;
            return new ReadProductCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ExrernalLink = category.ExrernalLink,
                InternalLink = category.InternalLink,
                IsActive = category.IsActive,
                Keywords = category.Keywords,
                MetaDescription = category.MetaDescription,
                PictureName = category.PictureName,
                OriginAddress = PathExtension.ProductCategoryOrigin,
                ThumbAddress = PathExtension.ProductCategoryThumb,
                PictureAlt = category.PictureAlt,
                PictureTitle = category.PictureTitle,
                SeoTitle = category.SeoTitle,
                ShortDescription = category.ShortDescription
            };
        }

        public async Task<bool> ChangeProductCategoryActiveState(long id)
        {
            var productCategory = await _productCategoryRepository.GetEntityById(id);
            if (productCategory.IsDelete) return false;
            if (productCategory == null) return false;
            productCategory.IsActive = !productCategory.IsActive;
            _productCategoryRepository.EditEntity(productCategory);
            await _productCategoryRepository.SaveChanges();
            return true;
        }

        #endregion

        #region product

        public async Task<CreateOurEditProductResult> CreateProduct(CreateProductDto product)
        {
            //try
            //{
                bool isExisted = await _productRepository.GetQuery().AsQueryable().AnyAsync(x =>
                    x.Name == product.Name && x.SeoTitle == product.SeoTitle && !x.IsDelete);
                if (isExisted) return CreateOurEditProductResult.IsExisted;

                if (product.Picture == null) return CreateOurEditProductResult.IsNotImage;
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(product.Picture.FileName);
                var res = product.Picture.AddImageToServer(imageName, PathExtension.ProductOriginServer, 150, 150,
                    PathExtension.ProductThumbServer);
                if (!res) return CreateOurEditProductResult.IsNotImage;

                var newProduct = new Product
                {
                    Name = product.Name,
                    SeoTitle = product.SeoTitle,
                    Description = product.Description,
                    DefaultPrice = product.DefaultPrice,
                    ShortDescription = product.ShortDescription,
                    ExrernalLink = product.ExrernalLink,
                    InternalLink = product.InternalLink,
                    Keywords = product.Keywords,
                    MetaDescription = product.MetaDescription,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    PictureName = imageName,
                    IsActive = product.IsActive,
                    StateForCount = product.StateForCount,
                };
                if (product.Size != null) newProduct.Size = product.Size;

                await _productRepository.AddEntity(newProduct);
                //await _productRepository.SaveChanges();

                if (product.StateForCount != CountState.Single)
                {
                    if (product.CreateCounts == null || !product.CreateCounts.Any()) return CreateOurEditProductResult.CountListIsNotExisted;
                    //await CreateCount(newProduct.Id, product.CreateCounts);
                }

                if (product.selectedCategories.Any()) await AddSelectedCategory(newProduct.Id, product.selectedCategories);

                return CreateOurEditProductResult.Success;
            //}
            //catch (Exception e)
            //{
            //    return CreateOurEditProductResult.Error;
            //}
        }

        #endregion

        #region dipose
        public async ValueTask DisposeAsync()
        {
            if (_productFeatureCategoryRepository != null) await _productFeatureCategoryRepository.DisposeAsync();
            if (_productCategoryRepository != null) await _productCategoryRepository.DisposeAsync();
            if (_countOfProductRepository != null) await _countOfProductRepository.DisposeAsync();
            if (_productRepository != null) await _productRepository.DisposeAsync();
            if (_productSelectedCategoryRepository != null) await _productSelectedCategoryRepository.DisposeAsync();
        }

        #endregion
    }
}
