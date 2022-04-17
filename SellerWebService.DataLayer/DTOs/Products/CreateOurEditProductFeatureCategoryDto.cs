namespace SellerWebService.DataLayer.DTOs.Products
{
    public class CreateOurEditProductFeatureCategoryDto
    {
        public long? Id { get; set; }

        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }
    }

    public enum CreateOurEditProductFeatureCategoryResult
    {
        NotFound,
        Success,
        IsExisted,
        Error
    }

}
