using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using _0_framework.Entities;
using SellerWebService.DataLayer.Entities.Factor;

namespace SellerWebService.DataLayer.Entities.Products
{
    public class GroupForProductFeature : BaseEntity
    {
        #region prop
        public long ProductId { get; set; }
        public long ProductFeatureCategoryId { get; set; }
        public int Order { get; set; }

        #endregion

        #region relations

        public Product Product { get; set; }
        public ProductFeatureCategory ProductFeatureCategory { get; set; }
        public ICollection<ProductFeature> ProductFeatures { get; set; }
        //public ICollection<FactorFeatureSelected> FactorFeatureSelecteds { get; set; }
        #endregion
    }
}
