﻿global using _0_framework.Entities;
global using System.ComponentModel.DataAnnotations;

namespace SellerWebService.DataLayer.Entities.Product
{
    public class ProductCategory : BaseEntity
    {
        #region prop

        public long? ParentId { get; set; }

        [Display(Name = "نام محصول")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "عنوان سٔو")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SeoTitle { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ShortDescription { get; set; }

        [Display(Name = "ادرس عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PictureAddress { get; set; }

        [Display(Name = "الت عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PictureAlt { get; set; }

        [Display(Name = "عنوان عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PictureTitle { get; set; }

        [Display(Name = "فعال / غیر فعال")]
        public bool IsActive { get; set; }

        [Display(Name = "توضیحات متا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string MetaDescription { get; set; }

        [Display(Name = "کیبورد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Keywords { get; set; }

        [Display(Name = "لینک خارجی")]
        public string ExrernalLink { get; set; }

        [Display(Name = "لینک داخلی")]
        public string InternalLink { get; set; }
        #endregion

        #region relations

        public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        public ProductCategory Parent { get; set; }

        #endregion

    }
}
