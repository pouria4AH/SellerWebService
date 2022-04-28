using Microsoft.AspNetCore.Http;

namespace SellerWebService.DataLayer.DTOs.Products
{
    public class CreateOurEditProductGalleryDTO
    {
        [Display(Name = "اولویت محصول")]
        public int DisplayPriority { get; set; }
        [Display(Name = "عکس محصول")]
        public IFormFile Image { get; set; }
        [Display(Name = " عکس محصول نام")]
        public string ImageName { get; set; }
        [Display(Name = "الت عکس")]
        public string PictureAlt { get; set; }
        [Display(Name = "عنوان عکس")]
        public string PictureTitle { get; set; }
    }

    public enum CreateOurEditProductGalleryResult
    {
        Success,
        ImageIsNull,
        ProductNotFound
    }
}
