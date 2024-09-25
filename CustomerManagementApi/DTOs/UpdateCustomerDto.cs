using System.ComponentModel.DataAnnotations;

namespace CustomerManagementApi.DTOs
{
    public class UpdateCustomerDto : CreateCustomerDto
    {
        [Required]
        public int Id { get; set; }
    }
}
