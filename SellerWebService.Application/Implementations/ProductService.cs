using _0_framework.Extensions;
using _0_framework.Utils;
using Microsoft.EntityFrameworkCore;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Products;
using SellerWebService.DataLayer.Entities.Products;
using SellerWebService.DataLayer.Entities.Store;
using SellerWebService.DataLayer.Repository;

namespace SellerWebService.Application.Implementations
{
    public class ProductService : IProductService
    {
        #region ctor

        private readonly IGenericRepository<ProductFeatureCategory> _productFeatureCategoryRepository;
        //private readonly IGenericRepository<ProductCategory> _productCategoryRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<GroupForProductFeature> _groupForProductFeatureRepository;
        private readonly IGenericRepository<ProductFeature> _productFeatureRepository;
        private readonly IGenericRepository<ProductGallery> _productGalleryRepository;
        private readonly IGenericRepository<StoreData> _storeDataRepository;

        public ProductService(IGenericRepository<ProductFeatureCategory> productFeatureCategoryRepository,
            //IGenericRepository<ProductCategory> productCategoryRepository,
            IGenericRepository<Product> productRepository,
            IGenericRepository<GroupForProductFeature> groupForProductFeatureRepository, IGenericRepository<ProductFeature> productFeatureRepository, IGenericRepository<ProductGallery> productGalleryRepository, IGenericRepository<StoreData> storeDataRepository)
        {
            _productFeatureCategoryRepository = productFeatureCategoryRepository;
            //_productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
            _groupForProductFeatureRepository = groupForProductFeatureRepository;
            _productFeatureRepository = productFeatureRepository;
            _productGalleryRepository = productGalleryRepository;
            _storeDataRepository = storeDataRepository;
        }

        #endregion

        #region product feature category

        public async Task<List<EditProductFeatureCategoryDto>> GetProductFeatureCategories(Guid storeCode)
        {
            return await _productFeatureCategoryRepository.GetQuery().AsQueryable()
                .Include(x => x.StoreData)
                .Where(x => !x.IsDelete && x.StoreData.UniqueCode == storeCode)
                .Select(x =>
                    new EditProductFeatureCategoryDto
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Name = x.Name
                    }).ToListAsync();
        }

        public async Task<CreateOurEditProductFeatureCategoryResult> CreateFeatureCategory(
            CreateProductFeatureCategoryDto featureCategory, Guid storeCode)
        {
            if (featureCategory != null)
            {
                var checkExisted = await _productFeatureCategoryRepository.GetQuery().AsQueryable()
                    .Include(x => x.StoreData)
                    .AnyAsync(x => x.Name == featureCategory.Name && x.StoreData.UniqueCode == storeCode);
                if (checkExisted) return CreateOurEditProductFeatureCategoryResult.IsExisted;
                var store = await _storeDataRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(x => !x.IsDelete && x.UniqueCode == storeCode);
                if (store == null) return CreateOurEditProductFeatureCategoryResult.Error;

                ProductFeatureCategory newfeatureCategory = new ProductFeatureCategory
                {
                    StoreDataId = store.Id,
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
            EditProductFeatureCategoryDto featureCategory, Guid storeCode)
        {
            if (featureCategory == null) return CreateOurEditProductFeatureCategoryResult.Error;

            var category = await _productFeatureCategoryRepository.GetQuery().AsQueryable()
                .Include(x => x.StoreData)
                .SingleOrDefaultAsync(x => x.Id == featureCategory.Id && !x.IsDelete && x.StoreData.UniqueCode == storeCode);

            if (category == null) return CreateOurEditProductFeatureCategoryResult.NotFound;
            category.Description = featureCategory.Description;
            category.Name = featureCategory.Name;
            _productFeatureCategoryRepository.EditEntity(category);
            await _productFeatureCategoryRepository.SaveChanges();
            return CreateOurEditProductFeatureCategoryResult.Success;
        }

        #endregion

        #region selected category

        //public async Task AddSelectedCategory(long productId, List<long> selectedCategories)
        //{
        //    var productSelectedCategories = new List<ProductSelectedCategory>();
        //    foreach (var categoryId in selectedCategories)
        //    {
        //        productSelectedCategories.Add(new ProductSelectedCategory
        //        {
        //            ProductId = productId,
        //            ProductCategoryId = categoryId
        //        });
        //    }

        //    await _productSelectedCategoryRepository.AddRangeEntities(productSelectedCategories);
        //    await _productSelectedCategoryRepository.SaveChanges();
        //}

        //public async Task RemoveSelectedCategory(long productId)
        //{
        //    var selectedCategory = await _productSelectedCategoryRepository.GetQuery().AsQueryable()
        //        .Where(x => x.ProductId == productId).ToListAsync();
        //    _productSelectedCategoryRepository.DeletePermanentEntities(selectedCategory);
        //}

        #endregion

        #region product category

        //public async Task<CreateOurEditProductCategoryResult> CreateProductCategory(
        //    CreateProductCategoryDto productCategory)
        //{

        //    var checkForExisted = await _productCategoryRepository.GetQuery().AsQueryable()
        //        .AnyAsync(x => x.Name == productCategory.Name );
        //    if (checkForExisted) return CreateOurEditProductCategoryResult.IsExisted;

        //    if (productCategory.Picture == null) return CreateOurEditProductCategoryResult.IsNotImage;
        //    var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(productCategory.Picture.FileName);
        //    var res = productCategory.Picture.AddImageToServer(imageName, PathExtension.ProductCategoryOriginServer,
        //        150, 150,
        //        PathExtension.ProductCategoryThumbServer);

        //    if (res)
        //    {
        //        var newCategory = new ProductCategory
        //        {
        //            Name = productCategory.Name,
        //            Description = productCategory.Description,
        //            IsActive = productCategory.IsActive
        //        };
        //        await _productCategoryRepository.AddEntity(newCategory);
        //        await _productCategoryRepository.SaveChanges();
        //        return CreateOurEditProductCategoryResult.Success;
        //    }

        //    return CreateOurEditProductCategoryResult.Error;
        //}

        //public async Task<List<ReadProductCategoryDto>> GetAllProductCategory()
        //{
        //    return await _productCategoryRepository.GetQuery().AsQueryable().Where(x => !x.IsDelete)
        //        .Select(x => new ReadProductCategoryDto
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //            Description = x.Description,
        //            IsActive = x.IsActive,
        //            OriginAddress = PathExtension.ProductCategoryOrigin,
        //            ThumbAddress = PathExtension.ProductCategoryThumb,
        //        })
        //        .ToListAsync();

        //}

        //public async Task<CreateOurEditProductCategoryResult> EditProductCategory(
        //    EditProductCategoryDto productCategory)
        //{
        //    var mainCategory = await _productCategoryRepository.GetEntityById(productCategory.Id);
        //    if (mainCategory == null || mainCategory.IsDelete) return CreateOurEditProductCategoryResult.NotFound;

        //    mainCategory.Description = productCategory.Description;
        //    mainCategory.Name = productCategory.Name;
        //    mainCategory.IsActive = productCategory.IsActive;

        //    _productCategoryRepository.EditEntity(mainCategory);
        //    await _productCategoryRepository.SaveChanges();
        //    return CreateOurEditProductCategoryResult.Success;

        //}

        //public async Task<ReadProductCategoryDto> GetProductCategoryById(long id)
        //{
        //    var category = await _productCategoryRepository.GetEntityById(id);
        //    if (category == null || category.IsDelete) return null;
        //    return new ReadProductCategoryDto
        //    {
        //        Id = category.Id,
        //        Name = category.Name,
        //        Description = category.Description,
        //        IsActive = category.IsActive,
        //        OriginAddress = PathExtension.ProductCategoryOrigin,
        //        ThumbAddress = PathExtension.ProductCategoryThumb,
        //    };
        //}

        //public async Task<bool> ChangeProductCategoryActiveState(long id)
        //{
        //    var productCategory = await _productCategoryRepository.GetEntityById(id);
        //    if (productCategory.IsDelete) return false;
        //    if (productCategory == null) return false;
        //    productCategory.IsActive = !productCategory.IsActive;
        //    _productCategoryRepository.EditEntity(productCategory);
        //    await _productCategoryRepository.SaveChanges();
        //    return true;
        //}

        #endregion\

        #region product

        public async Task<CreateOurEditProductResult> CreateProduct(CreateProductDto product, Guid storeCode)
        {
            try
            {
                bool isExisted = await _productRepository.GetQuery().AsQueryable()
                    .Include(x => x.StoreData)
                    .AnyAsync(x =>
                    x.Name == product.Name && x.SeoTitle == product.SeoTitle && !x.IsDelete && x.StoreData.UniqueCode == storeCode);
                if (isExisted) return CreateOurEditProductResult.IsExisted;

                //if (product.StateForCount != CountState.Single)
                //{
                //    if (product.Counts == null || !product.Counts.Any())
                //        return CreateOurEditProductResult.CountListIsNotExisted;
                //}
                var store = await _storeDataRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => x.UniqueCode == storeCode && !x.IsDelete);
                if (store == null) return CreateOurEditProductResult.Error;
                if (product.Picture == null) return CreateOurEditProductResult.IsNotImage;

                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(product.Picture.FileName);
                var res = product.Picture.AddImageToServer(imageName, PathExtension.ProductOriginServer, 150, 150,
                    PathExtension.ProductThumbServer);
                if (!res) return CreateOurEditProductResult.IsNotImage;

                var newProduct = new Product
                {
                    StoreDataId = store.Id,
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
                    //StateForCount = product.StateForCount,
                    //Prepayment = product.Prepayment
                };
                if (product.Size != null) newProduct.Size = product.Size;

                await _productRepository.AddEntity(newProduct);
                await _productRepository.SaveChanges();

                //if (product.selectedCategories.Any())
                //    await AddSelectedCategory(newProduct.Id, product.selectedCategories);

                return CreateOurEditProductResult.Success;
            }
            catch (Exception e)
            {
                return CreateOurEditProductResult.Error;
            }
        }

        public async Task<List<ReadProductDto>> GetAllProduct(Guid storeCode)
        {
            return await _productRepository.GetQuery().AsQueryable()
                .Include(x => x.StoreData)
                .Where(x => !x.IsDelete && x.StoreData.UniqueCode == storeCode)
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
                    //Prepayment = x.Prepayment
                    //OriginName = PathExtension.ProductOrigin,
                    //CategoriesId = x.ProductSelectedCategories.Select(z => z.ProductCategoryId).ToList(),
                    //StateForCount = x.StateForCount,
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
            };
        }

        public async Task<CreateOurEditProductResult> EditProduct(EditProductDto product, Guid storeCode)
        {
            var mainProduct = await _productRepository.GetQuery().AsQueryable()
                .Include(x => x.StoreData)
                .SingleOrDefaultAsync(x => x.Id == product.Id && !x.IsDelete && x.StoreData.UniqueCode == storeCode);
            if (mainProduct == null) return CreateOurEditProductResult.NotFound;

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
            //mainProduct.StateForCount = product.StateForCount;
            mainProduct.Keywords = product.Keywords;
            mainProduct.MetaDescription = product.MetaDescription;
            mainProduct.Name = product.Name;
            mainProduct.PictureAlt = product.PictureAlt;
            mainProduct.PictureTitle = product.PictureTitle;
            //mainProduct.Prepayment = product.Prepayment;

            //await RemoveSelectedCategory(product.Id);
            //await AddSelectedCategory(product.Id, product.selectedCategories);

            _productRepository.EditEntity(mainProduct);
            await _productRepository.SaveChanges();
            return CreateOurEditProductResult.Success;

        }

        public async Task<bool> ChangeProductActiveState(long id, Guid storeCode)
        {
            try
            {
                var product = await _productRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(x => !x.IsDelete && x.Id == id && x.StoreData.UniqueCode == storeCode);
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
            CreateGroupProductFeatureDto groupProductFeature, Guid storeCode)
        {
            try
            {
                var checkExiskted = await _groupForProductFeatureRepository.GetQuery().AsQueryable()
                    .Include(x => x.StoreData)
                    .AnyAsync(x =>
                    groupProductFeature.ProductId == x.ProductId &&
                    x.ProductFeatureCategoryId == groupProductFeature.ProductFeatureCategoryId &&
                    !x.IsDelete && x.StoreData.UniqueCode == storeCode);
                if (checkExiskted) return CreateGroupProductFeatureResult.IsExisted;
                var product = await _productRepository.GetQuery().AsQueryable().Include(x => x.StoreData).SingleOrDefaultAsync(x => x.Id == groupProductFeature.ProductId);

                if (product == null && product.StoreData.UniqueCode != storeCode)
                    return CreateGroupProductFeatureResult.Error;

                var newGroup = new GroupForProductFeature
                {
                    StoreDataId = product.StoreDataId,
                    ProductId = groupProductFeature.ProductId,
                    ProductFeatureCategoryId = groupProductFeature.ProductFeatureCategoryId,
                    Order = groupProductFeature.Order
                };
                var orderCheck = await _groupForProductFeatureRepository.GetQuery().AsQueryable().AnyAsync(x => x.ProductId == groupProductFeature.ProductId && x.Order == groupProductFeature.Order && !x.IsDelete);
                if (orderCheck) return CreateGroupProductFeatureResult.OrderExisted;
                await _groupForProductFeatureRepository.AddEntity(newGroup);
                await _groupForProductFeatureRepository.SaveChanges();
                return CreateGroupProductFeatureResult.Success;
            }
            catch (Exception e)
            {
                return CreateGroupProductFeatureResult.Error;
            }
        }

        public async Task<bool> DeleteGroup(long groupId, Guid storeCode)
        {
            try
            {
                var group = await _groupForProductFeatureRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => !x.IsDelete && x.Id == groupId && x.StoreData.UniqueCode == storeCode);
                if (group == null) return false;
                _groupForProductFeatureRepository.DeleteEntity(group);
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

        public async Task<CreateOrEditProductFeatureResult> CreateProductFeature(CreateProductFeatureDto feature, Guid storeCode)
        {
            try
            {
                var store = await _storeDataRepository.GetQuery().AsQueryable()
                    .SingleOrDefaultAsync(x => !x.IsDelete && x.UniqueCode == storeCode);
                if(store == null) return CreateOrEditProductFeatureResult.NotFound;
                var newFeature = new ProductFeature
                {
                    StoreDataId = store.Id,
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

        public async Task<CreateOrEditProductFeatureResult> EditProductFeature(EditProductFeatureDto feature, Guid storeCode)
        {
            try
            {
                var productFeature = await _productFeatureRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(x=>!x.IsDelete && x.Id == feature.Id && x.StoreData.UniqueCode == storeCode);
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
        public async Task<CreateOurEditProductGalleryResult> CreateProductGallery(CreateOurEditProductGalleryDTO createOurEdit, long productId, Guid storeCode)
        {
            var product = await _productRepository.GetQuery().AsQueryable()
                .Include(x=>x.StoreData)
                .SingleOrDefaultAsync(x=>x.Id == productId && x.StoreData.UniqueCode == storeCode );
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
        public async Task<CreateOurEditProductGalleryDTO> GetProductGalleryFourEdit(long galleryId, Guid storeCode)
        {
            var gallery = await _productGalleryRepository.GetQuery()
                .Include(x => x.Product)
                .ThenInclude(x=>x.StoreData)
                .SingleOrDefaultAsync(x => x.Id == galleryId && x.Product.StoreData.UniqueCode == storeCode);
            if (gallery == null) return null;
            return new CreateOurEditProductGalleryDTO
            {
                ImageName = gallery.ImageName,
                DisplayPriority = gallery.DisplayPriority,
                PictureTitle = gallery.PictureTitle,
                PictureAlt = gallery.PictureAlt,
            };
        }
        public async Task<CreateOurEditProductGalleryResult> EditProductGallery(long galleryId, CreateOurEditProductGalleryDTO gallery, Guid storeCode)
        {
            var mainGallery = await _productGalleryRepository.GetQuery()
                .Include(x => x.Product)
                .ThenInclude(x=>x.StoreData)
                .SingleOrDefaultAsync(x => x.Id == galleryId && !x.IsDelete && x.Product.StoreData.UniqueCode == storeCode);
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
            if (_productFeatureCategoryRepository != null)
                await _productFeatureCategoryRepository.DisposeAsync();
            if (_productRepository != null)
                await _productRepository.DisposeAsync();
            if (_groupForProductFeatureRepository != null)
                await _groupForProductFeatureRepository.DisposeAsync();
            if (_productFeatureRepository != null)
                await _productFeatureRepository.DisposeAsync();
            if (_productGalleryRepository != null)
                await _productGalleryRepository.DisposeAsync();
            if (_storeDataRepository != null)
                await _storeDataRepository.DisposeAsync();
        }

        #endregion
    }
}


