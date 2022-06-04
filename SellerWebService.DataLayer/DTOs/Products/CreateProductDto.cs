using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using SellerWebService.DataLayer.Entities.Products;

namespace SellerWebService.DataLayer.DTOs.Products
{
    public class CreateProductDto
    {
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
        [Range(0, 100)]
        public double Prepayment { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ShortDescription { get; set; }

        [Display(Name = "ادرس عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile Picture { get; set; }

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
        public string? ExrernalLink { get; set; }

        [Display(Name = "لینک داخلی")]
        public string? InternalLink { get; set; }

        //public CountState StateForCount { get; set; }
        public List<long> Counts { get; set; }
        public List<long> selectedCategories { get; set; }
    }
    public enum CreateOurEditProductResult
    {
        Error,
        Success,
        IsNotImage,
        IsExisted,
        CountListIsNotExisted,
        NotFound
    }
}
