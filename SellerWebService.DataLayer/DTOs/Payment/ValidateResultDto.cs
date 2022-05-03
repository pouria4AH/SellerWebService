namespace SellerWebService.DataLayer.DTOs.Payment
{
    public class ValidateResultDto
    {
        public int? RefId { get; set; }
        public bool IsSuccess { get; set; }
        public Guid StoreCode { get; set; }
    }
}
