using CustomerManagementClient.DTOs;
using CustomerManagementClient.Models;

public interface ICustomerMapper
{
    CustomerViewModel MapToViewModel(CustomerDto dto);
    CustomerDto MapToDto(CustomerViewModel vm);
}
