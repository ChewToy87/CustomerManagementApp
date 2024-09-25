// Services/ICustomerService.cs
using CustomerManagementClient.DTOs;

namespace CustomerManagementClient.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetCustomersAsync();
        Task AddCustomerAsync(CustomerDto customer);
        Task UpdateCustomerAsync(CustomerDto customer);
        Task DeleteCustomerAsync(int id);
    }
}
