namespace SellerWebService.DataLayer.DTOs.Products
{
    public class ReadProductCategoryDto
    {
        public long Id { get; set; }

        [Display(Name = "نام دسته محصول")]
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
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PictureName { get; set; }

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

        public string OriginAddress { get; set; }

        public string ThumbAddress { get; set; }
    }
}
