// DTOs/CustomerDto.cs
namespace CustomerManagementClient.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }  // Non-nullable Id to match your API

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
