using SellerWebService.DataLayer.Entities.Products;

namespace SellerWebService.DataLayer.DTOs.Products
{
    public class ReadProductDto
    {
        public long Id { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "عنوان سٔو")]
        public string SeoTitle { get; set; }

        [Display(Name = "قیمت پیش فرض")]
        public long DefaultPrice { get; set; }

        [Display(Name = "سایز پیش فرض")]
        public string? Size { get; set; }

        [Display(Name = "پرداخت اولیه")]
        public double Prepayment { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        public string ShortDescription { get; set; }

        [Display(Name = "ادرس عکس")]
        public string PictureName { get; set; }

        [Display(Name = "الت عکس")]
        public string PictureAlt { get; set; }

        [Display(Name = "عنوان عکس")]
        public string PictureTitle { get; set; }

        [Display(Name = "فعال / غیر فعال")]
        public bool IsActive { get; set; }

        [Display(Name = "توضیحات متا")]
        public string MetaDescription { get; set; }

        [Display(Name = "کیبورد")]
        public string Keywords { get; set; }

        [Display(Name = "لینک خارجی")]
        public string? ExrernalLink { get; set; }

        [Display(Name = "لینک داخلی")]
        public string? InternalLink { get; set; }

        [Display(Name = "لیست تیراژ")]
        public string CountArray { get; set; }

        [Display(Name = "وضعیت تیراژ")]
        //public CountState StateForCount { get; set; }

        public string OriginAddress { get; set; }

        public string ThumbAddress { get; set; }
        public List<long> CategoriesId { get; set; }
    }
}
