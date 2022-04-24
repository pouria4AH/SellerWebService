namespace SellerWebService.DataLayer.DTOs.Products
{
    public class ReadGroupProductFeatureDto : CreateGroupProductFeatureDto
    {
        public long Id { get; set; }
        public List<EditProductFeatureDto> ProductFeatures { get; set; }
    }
}
