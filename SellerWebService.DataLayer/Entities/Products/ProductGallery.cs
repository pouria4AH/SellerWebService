using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_framework.Entities;

namespace SellerWebService.DataLayer.Entities.Products
{
    public class ProductGallery : BaseEntity
    {

        #region prop

        public long ProductId { get; set; }
        [Display(Name = "ترتیب نمایش")]
        public int DisplayPriority { get; set; }
        [Display(Name = "عکس محصول")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImageName { get; set; }

        [Display(Name = "الت عکس")]
        public string PictureAlt { get; set; }

        [Display(Name = "عنوان عکس")]
        public string PictureTitle { get; set; }
        #endregion

        #region realstion

        public Product Product { get; set; }

        #endregion
    }
}
