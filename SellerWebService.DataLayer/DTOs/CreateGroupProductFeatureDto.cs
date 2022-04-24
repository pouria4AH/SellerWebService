namespace SellerWebService.DataLayer.DTOs
{
    public class CreateGroupProductFeatureDto
    {
        public long ProductId { get; set; }
        public long ProductFeatureCategoryId { get; set; }
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Order { get; set; }
    }

    public enum CreateGroupProductFeatureResult
    {
        Error,
        Success,
        IsExisted,
        NotFound
    }
}
