using SellerWebService.DataLayer.DTOs.Customer;

namespace SellerWebService.Application.interfaces
{
    public interface ICustomerService : IAsyncDisposable
    {
        Task<CreateOurEditCustomerResult> CreateCustomer(CreateCustomerDto customer, Guid storeCode);
        Task<CreateOurEditCustomerResult> EditCustomer(EditCustomerDto customer, Guid storeCode);
        Task<bool> DeleteCustomer(Guid customerCode, Guid storeCode);
        Task<ReadCustomerDto> GetCustomer(Guid customerCode, Guid storeCode);
        Task<EditCustomerDto> GetCustomerForEdit(Guid customerCode, Guid storeCode);
        Task<List<ReadCustomerDto>> SearchForCustomer(SearchCustomerDto search, Guid storeCode);
    }
}
