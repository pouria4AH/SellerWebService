using Microsoft.EntityFrameworkCore;
using SellerWebService.DataLayer.Entities.Account;
using SellerWebService.DataLayer.Entities.Factor;
using SellerWebService.DataLayer.Entities.Products;

namespace SellerWebService.DataLayer.Context
{
    public class SellerContext : DbContext
    {
        public SellerContext(DbContextOptions<SellerContext> options) : base(options)
        {

        }
        #region products
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        public DbSet<ProductFeatureCategory> ProductFeatureCategories { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<GroupForProductFeature> GroupForProductFeatures { get; set; }
        public DbSet<ProductGallery> ProductGalleries { get; set; }
        #endregion
        #region oder
        public DbSet<User> Users { get; set; }
        #endregion
        #region factor
        public DbSet<Factor> Factors { get; set; }
        public DbSet<FactorDetails> FactorDetails { get; set; }
        //public DbSet<FactorFeatureSelected> FactorFeatureSelecteds { get; set; }
        #endregion


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
        //    {
        //        relationship.DeleteBehavior = DeleteBehavior.Restrict;
        //    }
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
