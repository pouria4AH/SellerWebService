using _0_framework.Extensions;
using _0_framework.Utils;
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
        private readonly IGenericRepository<ProductCategory> _productCategoryRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductSelectedCategory> _productSelectedCategoryRepository;
        private readonly IGenericRepository<GroupForProductFeature> _groupForProductFeatureRepository;
        private readonly IGenericRepository<ProductFeature> _productFeatureRepository;
        private readonly IGenericRepository<ProductGallery> _productGalleryRepository;

        public ProductService(IGenericRepository<ProductFeatureCategory> productFeatureCategoryRepository,
            IGenericRepository<ProductCategory> productCategoryRepository,
            IGenericRepository<Product> productRepository,
            IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository,
            IGenericRepository<GroupForProductFeature> groupForProductFeatureRepository, IGenericRepository<ProductFeature> productFeatureRepository, IGenericRepository<ProductGallery> productGalleryRepository)
        {
            _productFeatureCategoryRepository = productFeatureCategoryRepository;
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
            _productSelectedCategoryRepository = productSelectedCategoryRepository;
            _groupForProductFeatureRepository = groupForProductFeatureRepository;
            _productFeatureRepository = productFeatureRepository;
            _productGalleryRepository = productGalleryRepository;
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
                .AnyAsync(x => x.Name == productCategory.Name );
            if (checkForExisted) return CreateOurEditProductCategoryResult.IsExisted;

            if (productCategory.Picture == null) return CreateOurEditProductCategoryResult.IsNotImage;
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(productCategory.Picture.FileName);
            var res = productCategory.Picture.AddImageToServer(imageName, PathExtension.ProductCategoryOriginServer,
                150, 150,
                PathExtension.ProductCategoryThumbServer);

            if (res)
            {
                var newCategory = new ProductCategory
                {
                    Name = productCategory.Name,
                    Description = productCategory.Description,
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
                    IsActive = x.IsActive,
                    OriginAddress = PathExtension.ProductCategoryOrigin,
                    ThumbAddress = PathExtension.ProductCategoryThumb,
                })
                .ToListAsync();

        }

        public async Task<CreateOurEditProductCategoryResult> EditProductCategory(
            EditProductCategoryDto productCategory)
        {
            var mainCategory = await _productCategoryRepository.GetEntityById(productCategory.Id);
            if (mainCategory == null || mainCategory.IsDelete) return CreateOurEditProductCategoryResult.NotFound;

            mainCategory.Description = productCategory.Description;
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
                IsActive = category.IsActive,
                OriginAddress = PathExtension.ProductCategoryOrigin,
                ThumbAddress = PathExtension.ProductCategoryThumb,
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
            try
            {
                bool isExisted = await _productRepository.GetQuery().AsQueryable().AnyAsync(x =>
                    x.Name == product.Name && x.SeoTitle == product.SeoTitle && !x.IsDelete);
                if (isExisted) return CreateOurEditProductResult.IsExisted;

                if (product.StateForCount != CountState.Single)
                {
                    if (product.Counts == null || !product.Counts.Any())
                        return CreateOurEditProductResult.CountListIsNotExisted;
                }

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
                    Keywords = product.Keywords,
                    MetaDescription = product.MetaDescription,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    PictureName = imageName,
                    IsActive = product.IsActive,
                    StateForCount = product.StateForCount,
                    Prepayment = product.Prepayment
                };
                if (product.Size != null) newProduct.Size = product.Size;


                await _productRepository.AddEntity(newProduct);
                await _productRepository.SaveChanges();


                if (product.selectedCategories.Any())
                    await AddSelectedCategory(newProduct.Id, product.selectedCategories);

                return CreateOurEditProductResult.Success;
            }
            catch (Exception e)
            {
                return CreateOurEditProductResult.Error;
            }
        }

        public async Task<List<ReadProductDto>> GetAllProduct()
        {
            return await _productRepository.GetQuery().AsQueryable()
                .Where(x => !x.IsDelete)
                .Include(x => x.ProductSelectedCategories)
                .Select(x => new ReadProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    DefaultPrice = x.DefaultPrice,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PictureName = x.PictureName,
                    IsActive = x.IsActive,
                    SeoTitle = x.SeoTitle,
                    ShortDescription = x.ShortDescription,
                    Size = x.Size,
                    OriginAddress = PathExtension.ProductOrigin,
                    ThumbAddress = PathExtension.ProductThumb,
                    CategoriesId = x.ProductSelectedCategories.Select(z => z.ProductCategoryId).ToList(),
                    StateForCount = x.StateForCount,
                    Prepayment = x.Prepayment
                }).ToListAsync();
        }

        public async Task<ReadProductDto> GetProductById(long id)
        {
            var product = await _productRepository.GetEntityById(id);
            if (product == null) return null;
            return new ReadProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                DefaultPrice = product.DefaultPrice,
                Keywords = product.Keywords,
                MetaDescription = product.MetaDescription,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                PictureName = product.PictureName,
                IsActive = product.IsActive,
                SeoTitle = product.SeoTitle,
                ShortDescription = product.ShortDescription,
                Size = product.Size,
                OriginAddress = PathExtension.ProductOrigin,
                ThumbAddress = PathExtension.ProductThumb,
                CategoriesId = await _productSelectedCategoryRepository.GetQuery().AsQueryable()
                    .Where(x => x.ProductId == id).Select(x => x.ProductCategoryId).ToListAsync(),
                StateForCount = product.StateForCount,
                Prepayment = product.Prepayment
            };
        }

        public async Task<CreateOurEditProductResult> EditProduct(EditProductDto product)
        {
            var mainProduct = await _productRepository.GetEntityById(product.Id);
            if (mainProduct == null || mainProduct.IsDelete) return CreateOurEditProductResult.NotFound;

            if (product.StateForCount != CountState.Single)
            {
                if (product.Counts == null || !product.Counts.Any())
                    return CreateOurEditProductResult.CountListIsNotExisted;
            }

            if (product.Picture != null)
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(product.Picture.FileName);
                var res = product.Picture.AddImageToServer(imageName, PathExtension.ProductOriginServer, 150, 150,
                    PathExtension.ProductThumbServer, mainProduct.PictureName);
                if (res)
                {
                    mainProduct.PictureName = imageName;
                }
                else
                {
                    return CreateOurEditProductResult.IsNotImage;
                }
            }

            mainProduct.IsActive = product.IsActive;
            mainProduct.SeoTitle = product.SeoTitle;
            mainProduct.ShortDescription = product.ShortDescription;
            mainProduct.Description = product.Description;
            mainProduct.Size = product.Size;
            mainProduct.DefaultPrice = product.DefaultPrice;
            mainProduct.StateForCount = product.StateForCount;
            mainProduct.Keywords = product.Keywords;
            mainProduct.MetaDescription = product.MetaDescription;
            mainProduct.Name = product.Name;
            mainProduct.PictureAlt = product.PictureAlt;
            mainProduct.PictureTitle = product.PictureTitle;
            mainProduct.Prepayment = product.Prepayment;

            await RemoveSelectedCategory(product.Id);
            await AddSelectedCategory(product.Id, product.selectedCategories);

            _productRepository.EditEntity(mainProduct);
            await _productRepository.SaveChanges();
            return CreateOurEditProductResult.Success;

        }

        public async Task<bool> ChangeProductActiveState(long id)
        {
            try
            {
                var product = await _productRepository.GetEntityById(id);
                if (product == null) return false;
                product.IsActive = !product.IsActive;
                _productRepository.EditEntity(product);
                await _productRepository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        #endregion

        #region product feature

        public async Task<CreateGroupProductFeatureResult> CreateGroupOfProductFeature(
            CreateGroupProductFeatureDto groupProductFeature)
        {
            try
            {
                var checkExiskted = await _groupForProductFeatureRepository.GetQuery().AsQueryable().AnyAsync(x =>
                    groupProductFeature.ProductId == x.ProductId &&
                    x.ProductFeatureCategoryId == groupProductFeature.ProductFeatureCategoryId && !x.IsDelete);
                if (checkExiskted) return CreateGroupProductFeatureResult.IsExisted;
                //var product = await _productRepository.GetEntityById(groupProductFeature.ProductId);
                //var category =
                //    await _productFeatureCategoryRepository.GetEntityById(groupProductFeature.ProductFeatureCategoryId);
                //if (category == null || product == null) return CreateGroupProductFeatureResult.NotFound;
                var newGroup = new GroupForProductFeature
                {
                    ProductId = groupProductFeature.ProductId,
                    ProductFeatureCategoryId = groupProductFeature.ProductFeatureCategoryId,
                    Order = groupProductFeature.Order
                };
                var orderCheck = await _groupForProductFeatureRepository.GetQuery().AsQueryable().AnyAsync(x => x.ProductId == groupProductFeature.ProductId && x.Order == groupProductFeature.Order && !x.IsDelete);
                if(orderCheck) return CreateGroupProductFeatureResult.IsExisted;
                await _groupForProductFeatureRepository.AddEntity(newGroup);
                await _groupForProductFeatureRepository.SaveChanges();
                return CreateGroupProductFeatureResult.Success;
            }
            catch (Exception e)
            {
                return CreateGroupProductFeatureResult.Error;
            }
        }

        public async Task<bool> DeleteGroup(long groupId)
        {
            try
            {
                var group = await _groupForProductFeatureRepository.GetEntityById(groupId);
                if (group == null) return false;
                group.IsDelete = true;
                _groupForProductFeatureRepository.EditEntity(group);
                await _groupForProductFeatureRepository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<List<ReadGroupProductFeatureDto>> GetGroupsForProduct(long productId)
        {
            return await _groupForProductFeatureRepository.GetQuery().Include(x => x.ProductFeatures).AsQueryable()
                 .Where(x => x.ProductId == productId)
                 .Select(x => new ReadGroupProductFeatureDto
                 {
                     Id = x.Id,
                     Order = x.Order,
                     ProductFeatureCategoryId = x.ProductFeatureCategoryId,
                     ProductId = x.ProductId,
                     ProductFeatures = x.ProductFeatures.Where(x => !x.IsDelete).Select(x => new EditProductFeatureDto
                     {
                         Id = x.Id,
                         Description = x.Description,
                         ExtraPrice = x.ExtraPrice,
                         Name = x.Name
                     }).ToList()
                 }).ToListAsync();
        }

        public async Task<CreateOrEditProductFeatureResult> CreateProductFeature(CreateProductFeatureDto feature)
        {
            try
            {
                var newFeature = new ProductFeature
                {
                    Name = feature.Name,
                    Description = feature.Description,
                    ExtraPrice = feature.ExtraPrice,
                    GroupForProductFeatureId = feature.GroupForProductFeatureId
                };
                await _productFeatureRepository.AddEntity(newFeature);
                await _productFeatureRepository.SaveChanges();
                return CreateOrEditProductFeatureResult.Success;
            }
            catch (Exception e)
            {
                return CreateOrEditProductFeatureResult.Error;
            }
        }

        public async Task<CreateOrEditProductFeatureResult> EditProductFeature(EditProductFeatureDto feature)
        {
            try
            {
                var productFeature = await _productFeatureRepository.GetEntityById(feature.Id);
                if (productFeature == null) return CreateOrEditProductFeatureResult.NotFound;
                productFeature.Name = feature.Name;
                productFeature.Description = feature.Description;
                productFeature.ExtraPrice = feature.ExtraPrice;
                _productFeatureRepository.EditEntity(productFeature);
                await _productFeatureRepository.SaveChanges();
                return CreateOrEditProductFeatureResult.Success;
            }
            catch (Exception e)
            {
                return CreateOrEditProductFeatureResult.Error;
            }
        }

        #endregion

        #region product gallery
        public async Task<List<ProductGallery>> GetAllProductGallery(long id)
        {
            return await _productGalleryRepository.GetQuery().AsQueryable().Where(x => x.ProductId == id).ToListAsync();
        }
        public async Task<CreateOurEditProductGalleryResult> CreateProductGallery(CreateOurEditProductGalleryDTO createOurEdit, long productId)
        {
            var product = await _productRepository.GetEntityById(productId);
            if (product == null) return CreateOurEditProductGalleryResult.ProductNotFound;
            if (createOurEdit.Image == null || !createOurEdit.Image.IsImage()) return CreateOurEditProductGalleryResult.ImageIsNull;

            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(createOurEdit.Image.FileName);
            createOurEdit.Image.AddImageToServer(imageName, PathExtension.ProductGalleryOriginServer, 100, 100,
                PathExtension.ProductGalleryThumbServer);
            await _productGalleryRepository.AddEntity(new ProductGallery
            {
                ProductId = productId,
                ImageName = imageName,
                DisplayPriority = createOurEdit.DisplayPriority,
                PictureAlt = createOurEdit.PictureAlt,
                PictureTitle = createOurEdit.PictureTitle,
            });
            await _productGalleryRepository.SaveChanges();
            return CreateOurEditProductGalleryResult.Success;
        }
        public async Task<CreateOurEditProductGalleryDTO> GetProductGalleryFourEdit(long galleryId)
        {
            var gallery = await _productGalleryRepository.GetQuery()
                .Include(x => x.Product)
                .SingleOrDefaultAsync(x => x.Id == galleryId);
            if (gallery == null) return null;
            return new CreateOurEditProductGalleryDTO
            {
                ImageName = gallery.ImageName,
                DisplayPriority = gallery.DisplayPriority,
                PictureTitle = gallery.PictureTitle,
                PictureAlt = gallery.PictureAlt,
            };
        }
        public async Task<CreateOurEditProductGalleryResult> EditProductGallery(long galleryId, CreateOurEditProductGalleryDTO gallery)
        {
            var mainGallery = await _productGalleryRepository.GetQuery()
                .Include(x => x.Product)
                .SingleOrDefaultAsync(x => x.Id == galleryId);
            if (mainGallery == null) return CreateOurEditProductGalleryResult.ProductNotFound;
            if (gallery.Image != null && gallery.Image.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(gallery.Image.FileName);
                gallery.Image.AddImageToServer(imageName, PathExtension.ProductGalleryOriginServer, 100, 100,
                    PathExtension.ProductGalleryThumbServer, mainGallery.ImageName);

                mainGallery.ImageName = imageName;
            }
            mainGallery.DisplayPriority = gallery.DisplayPriority;
            mainGallery.PictureTitle = gallery.PictureTitle;
            mainGallery.PictureAlt = gallery.PictureAlt;
            _productGalleryRepository.EditEntity(mainGallery);
            await _productGalleryRepository.SaveChanges();
            return CreateOurEditProductGalleryResult.Success;
        }
        #endregion

        #region dipose

        public async ValueTask DisposeAsync()
        {
           /* if (_productFeatureCategoryRepository != null)*/ await _productFeatureCategoryRepository.DisposeAsync();
           /* if (_productCategoryRepository != null)*/ await _productCategoryRepository.DisposeAsync();
           /* if (_productRepository != null)*/ await _productRepository.DisposeAsync();
           /* if (_productSelectedCategoryRepository != null)*/ await _productSelectedCategoryRepository.DisposeAsync();
            /*if (_groupForProductFeatureRepository != null)*/ await _groupForProductFeatureRepository.DisposeAsync();
        }

        #endregion
    }
}


