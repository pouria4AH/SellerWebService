global using System.ComponentModel.DataAnnotations;
using _0_framework.Entities;

namespace SellerWebService.DataLayer.Entities.Products
{
    public class Product : BaseEntity
    {
        #region prop
        [Display(Name = "نام محصول")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "عنوان سٔو")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SeoTitle { get; set; }

        [Display(Name = "قیمت پیش فرض")]
        [Range(0, long.MaxValue)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long DefaultPrice { get; set; }

        [Display(Name = "سایز پیش فرض")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? Size { get; set; }

        [Display(Name = "پرداخت اولیه")]
        [Range(0,100)]
        public double Prepayment { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ShortDescription { get; set; }

        [Display(Name = "ادرس عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PictureName { get; set; }

        [Display(Name = "الت عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string PictureAlt { get; set; }

        [Display(Name = "عنوان عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string PictureTitle { get; set; }

        [Display(Name = "فعال / غیر فعال")]
        public bool IsActive { get; set; }

        [Display(Name = "توضیحات متا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string MetaDescription { get; set; }

        [Display(Name = "کیبورد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Keywords { get; set; }

        [Display(Name = "لینک خارجی")]
        public string? ExrernalLink { get; set; }

        [Display(Name = "لینک داخلی")]
        public string? InternalLink { get; set; }

        public string CountArray { get; set; }
        public CountState StateForCount { get; set; }
        #endregion

        #region relations

        public ICollection<ProductSelectedCategory> ProductSelectedCategories { get; set; }
        public ICollection<GroupForProductFeature> GroupForProductFeatures { get; set; }
        public ICollection<ProductGallery> ProductGalleries { get; set; }
        #endregion

    }
    public enum CountState
    {
        Single,
        List,
        Together,

    }
}
