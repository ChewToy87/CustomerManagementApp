using CustomerManagementClient.DTOs;
using CustomerManagementClient.Models;

public class CustomerMapper : ICustomerMapper
{
    public CustomerViewModel MapToViewModel(CustomerDto dto)
    {
        return new CustomerViewModel
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            Surname = dto.Surname,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        };
    }

    public CustomerDto MapToDto(CustomerViewModel vm)
    {
        return new CustomerDto
        {
            Id = vm.Id > 0 ? vm.Id : 0,
            FirstName = vm.FirstName,
            Surname = vm.Surname,
            Email = vm.Email,
            PhoneNumber = vm.PhoneNumber
        };
    }
}
