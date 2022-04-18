using Microsoft.EntityFrameworkCore;
using SellerWebService.Application.interfaces;
using SellerWebService.DataLayer.DTOs.Products;
using SellerWebService.DataLayer.Entities.Products;
using SellerWebService.DataLayer.Repository;

namespace SellerWebService.Application.Implementations
{
    public class ProductService : IProductService
    {
        public ProductService(IGenericRepository<Product> genericRepository)
        {
            GenericRepository = genericRepository;
        }

        public IGenericRepository<Product> GenericRepository { get; set; }
        //#region ctor

        //private readonly IGenericRepository<ProductFeatureCategory> _productFeatureCategoryRepository;

        //public ProductService(IGenericRepository<ProductFeatureCategory> productFeatureCategoryRepository)
        //{
        //    _productFeatureCategoryRepository = productFeatureCategoryRepository;
        //}

        //#endregion

        //#region  product feature category

        ////public async Task<List<CreateOurEditProductFeatureCategoryDto>> GetProductFeatureCategories()
        ////{
        ////    return await _productFeatureCategoryRepository.GetQuery().AsQueryable()
        ////        .Where(x=>!x.IsDelete)
        ////        .Select(x =>
        ////        new CreateOurEditProductFeatureCategoryDto
        ////        {
        ////            Id = x.Id,
        ////            Description = x.Description,
        ////            Name = x.Name
        ////        }).ToListAsync();
        ////}

        //public async Task<CreateOurEditProductFeatureCategoryResult> CreateFeatureCategory(CreateOurEditProductFeatureCategoryDto featureCategory)
        //{
        //    if (featureCategory != null)
        //    {
        //        var checkExisted = await _productFeatureCategoryRepository.GetQuery().AsQueryable()
        //            .AnyAsync(x => x.Name == featureCategory.Name);
        //        if (checkExisted) return CreateOurEditProductFeatureCategoryResult.IsExisted;

        //        ProductFeatureCategory newfeatureCategory = new ProductFeatureCategory
        //        {
        //            Name = featureCategory.Name,
        //            Description = featureCategory.Description
        //        };
        //        await _productFeatureCategoryRepository.AddEntity(newfeatureCategory);
        //        await _productFeatureCategoryRepository.SaveChanges();
        //        return CreateOurEditProductFeatureCategoryResult.Success;
        //    }
        //    return CreateOurEditProductFeatureCategoryResult.Error;
        //}
        //#endregion
        //#region dipose
        //public async ValueTask DisposeAsync()
        //{
        //     await _productFeatureCategoryRepository.DisposeAsync();
        //}

        //#endregion
    }
}
