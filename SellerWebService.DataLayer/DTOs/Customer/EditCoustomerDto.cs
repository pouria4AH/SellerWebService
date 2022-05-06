namespace SellerWebService.DataLayer.DTOs.Customer
{
    public class EditCustomerDto : CreateCustomerDto
    {
        public Guid CustomerCode { get; set; }

    }
}
