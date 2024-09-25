using CustomerManagementApi.DTOs;
using CustomerManagementApi.Models;

namespace CustomerManagementApi.Helpers
{
    public static class CustomerMapper
    {
        public static CustomerDto ToDto(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                Surname = customer.Surname,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
        }

        public static Customer ToModel(CreateCustomerDto customerDto)
        {
            return new Customer
            {
                FirstName = customerDto.FirstName,
                Surname = customerDto.Surname,
                Email = customerDto.Email,
                PhoneNumber = customerDto.PhoneNumber,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };
        }

        public static void UpdateCustomerModel(UpdateCustomerDto dto, Customer customer)
        {
            customer.FirstName = dto.FirstName;
            customer.Surname = dto.Surname;
            customer.Email = dto.Email;
            customer.PhoneNumber = dto.PhoneNumber;
            customer.Updated = DateTime.Now;
        }
    }
}
