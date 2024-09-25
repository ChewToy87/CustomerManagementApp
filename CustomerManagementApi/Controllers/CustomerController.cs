using CustomerManagementApi.DTOs;
using CustomerManagementApi.Helpers;
using CustomerManagementApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomersController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customers = await _repository.GetAllAsync();
            return Ok(customers.Select(c => CustomerMapper.ToDto(c)));
        }

        // GET: api/customers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(CustomerMapper.ToDto(customer));
        }

        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromForm] CreateCustomerDto createCustomerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = CustomerMapper.ToModel(createCustomerDto);
            await _repository.AddAsync(customer);

            var customerDto = CustomerMapper.ToDto(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customerDto);
        }

        // PUT: api/customers/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, [FromForm] UpdateCustomerDto updateCustomerDto)
        {
            if (id != updateCustomerDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            CustomerMapper.UpdateCustomerModel(updateCustomerDto, customer);
            await _repository.UpdateAsync(customer);

            return NoContent();
        }

        // DELETE: api/customers/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
