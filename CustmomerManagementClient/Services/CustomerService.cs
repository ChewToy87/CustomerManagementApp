using CustomerManagementClient.DTOs;
using System.Text.Json;

namespace CustomerManagementClient.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public CustomerService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = $"{configuration["BaseUrl"]}api/customers";
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<CustomerDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task AddCustomerAsync(CustomerDto customer)
        {
            using var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(customer.FirstName ?? ""), "FirstName");
            formContent.Add(new StringContent(customer.Surname ?? ""), "Surname");
            formContent.Add(new StringContent(customer.Email ?? ""), "Email");
            formContent.Add(new StringContent(customer.PhoneNumber ?? ""), "PhoneNumber");

            var response = await _httpClient.PostAsync(_apiUrl, formContent);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdCustomer = JsonSerializer.Deserialize<CustomerDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            customer.Id = createdCustomer.Id;
        }

        public async Task UpdateCustomerAsync(CustomerDto customer)
        {
            using var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(customer.Id.ToString()), "Id");
            formContent.Add(new StringContent(customer.FirstName ?? ""), "FirstName");
            formContent.Add(new StringContent(customer.Surname ?? ""), "Surname");
            formContent.Add(new StringContent(customer.Email ?? ""), "Email");
            formContent.Add(new StringContent(customer.PhoneNumber ?? ""), "PhoneNumber");

            var response = await _httpClient.PutAsync($"{_apiUrl}/{customer.Id}", formContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
