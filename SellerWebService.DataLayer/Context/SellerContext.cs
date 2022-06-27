using Microsoft.EntityFrameworkCore;
using SellerWebService.DataLayer.Entities.Account;
using SellerWebService.DataLayer.Entities.Factor;
using SellerWebService.DataLayer.Entities.Products;
using SellerWebService.DataLayer.Entities.Store;
using SellerWebService.DataLayer.Entities.TempV2;
using SellerWebService.DataLayer.Entities.Tickets;

namespace SellerWebService.DataLayer.Context
{
    public class SellerContext : DbContext
    {
        public SellerContext(DbContextOptions<SellerContext> options) : base(options)
        {

        }
        #region products
        public DbSet<Product> Products { get; set; }
        //-----------public DbSet<ProductCategory> ProductCategories { get; set; }
        //-------------public DbSet<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        public DbSet<ProductFeatureCategory> ProductFeatureCategories { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<GroupForProductFeature> GroupForProductFeatures { get; set; }
        public DbSet<ProductGallery> ProductGalleries { get; set; }
        #endregion
        #region users
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        #endregion
        #region factor
        public DbSet<Factor> Factors { get; set; }
        public DbSet<FactorDetails> FactorDetails { get; set; }
        //----------public DbSet<FactorFeatureSelected> FactorFeatureSelecteds { get; set; }
        #endregion
        #region store
        public DbSet<StoreData> StoreDatas { get; set; }
        public DbSet<StoreDetails> StoreDetails { get; set; }
        public DbSet<BankData> BankDatas { get; set; }

        #endregion
        #region Tickets
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketsMessage> TicketsMessages { get; set; }
        #endregion
        public DbSet<Presell> Presells { get; set; }
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
