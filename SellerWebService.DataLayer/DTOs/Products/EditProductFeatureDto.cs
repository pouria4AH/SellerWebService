namespace SellerWebService.DataLayer.DTOs.Products
{
    public class EditProductFeatureDto 
    {
        public long Id { get; set; }
        [Display(Name = "نام ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "قیمت اضافه")]
        [Range(0, long.MaxValue)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long ExtraPrice { get; set; }
    }
}
