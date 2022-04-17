using Microsoft.EntityFrameworkCore;
using SellerWebService.DataLayer.Entities.Products;

namespace SellerWebService.DataLayer.Context
{
    public class MainContext : DbContext
    {
        #region products
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        public DbSet<ProductFeatureCategory> ProductFeatureCategories { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        #endregion

        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
