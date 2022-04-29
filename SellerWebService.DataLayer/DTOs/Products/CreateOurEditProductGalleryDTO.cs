using Microsoft.AspNetCore.Http;

namespace SellerWebService.DataLayer.DTOs.Products
{
    public class CreateOurEditProductGalleryDTO
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "اولویت محصول")]
        public int DisplayPriority { get; set; }
        [Display(Name = "عکس محصول")]
        public IFormFile Image { get; set; }
        [Display(Name = " عکس محصول نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImageName { get; set; }
        [Display(Name = "الت عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PictureAlt { get; set; }
        [Display(Name = "عنوان عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PictureTitle { get; set; }
    }

    public enum CreateOurEditProductGalleryResult
    {
        Success,
        ImageIsNull,
        ProductNotFound
    }
}
