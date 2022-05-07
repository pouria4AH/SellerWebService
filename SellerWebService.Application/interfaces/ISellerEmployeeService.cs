using SellerWebService.DataLayer.DTOs.Store;

namespace SellerWebService.Application.interfaces
{
    public interface ISellerEmployeeService : IAsyncDisposable
    {
        Task<AddSellerEmployeeResult> CreateSellerEmployee(AddSellerEmployeeDto storeCode, Guid sellerCode);
        Task<bool> ToggleBlockEmployee(Guid sellerCode, Guid employeeCode);
        Task<bool> DeleteEmployee(Guid storeCode, Guid employeeCode);
    }
}
