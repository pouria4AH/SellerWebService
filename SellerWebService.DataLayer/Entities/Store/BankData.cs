using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_framework.Entities;

namespace SellerWebService.DataLayer.Entities.Store
{
    public class BankData : BaseEntity
    {
        #region prop

        public long StoreDataId { get; set; }
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }
        public string ShabaNumber { get; set; }
        public string BankName { get; set; }
        public string Owner { get; set; }
        public Guid StoreCode { get; set; }
        #endregion

        #region relations

        public StoreData StoreData { get; set; }
        #endregion

    }
}
