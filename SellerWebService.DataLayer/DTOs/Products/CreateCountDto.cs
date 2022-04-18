namespace SellerWebService.DataLayer.DTOs.Products
{
    public class CreateCountDto
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public long Count { get; set; }
    }
    public enum CreateOurEditCountResult
    {
        NotFound,
        Success,
        IsExisted,
        Error
    }
}
