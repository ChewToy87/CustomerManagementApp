using CustomerManagementClient.DTOs;
using CustomerManagementClient.Helpers;
using CustomerManagementClient.Models;
using CustomerManagementClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomerManagementClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<IndexModel> _logger;
        private int _nextTempId;
        public string PageTitle { get; set; } = "Customer Management";

        public IndexModel(ICustomerService customerService, ILogger<IndexModel> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        public List<CustomerViewModel> Customers
        {
            get
            {
                var customers = HttpContext.Session.GetObject<List<CustomerViewModel>>("Customers");
                if (customers == null)
                {
                    customers = new List<CustomerViewModel>();
                    HttpContext.Session.SetObject("Customers", customers);
                }
                return customers;
            }
            set
            {
                HttpContext.Session.SetObject("Customers", value);
            }
        }

        private void InitializeTempId()
        {
            _nextTempId = HttpContext.Session.GetInt32("NextTempId") ?? -1;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            InitializeTempId();

            var customerDtos = await _customerService.GetCustomersAsync();
            var customerViewModels = customerDtos.Select(MapToViewModel).ToList();

            // Store the customers in session
            Customers = customerViewModels;

            return Page();
        }

        public IActionResult OnPostAddCustomer()
        {
            InitializeTempId();

            var customers = Customers;

            var newCustomer = new CustomerViewModel
            {
                Id = GenerateTempId(),
                IsAdded = true,
                IsEditing = true
            };

            customers.Add(newCustomer);
            Customers = customers; // Update session

            return Partial("Partials/_CustomerEditRowPartial", newCustomer);
        }

        public IActionResult OnPostEditCustomer(int customerId)
        {
            InitializeTempId();

            var customers = Customers;

            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            if (customer != null)
            {
                customer.IsEditing = true;
                Customers = customers; // Update session
            }

            return Partial("Partials/_CustomerEditRowPartial", customer);
        }

        public IActionResult OnPostCancelEdit(int customerId)
        {
            InitializeTempId();

            var customers = Customers;

            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            if (customer != null)
            {
                customer.IsEditing = false;
                Customers = customers; // Update session
            }

            return Partial("Partials/_CustomerRowPartial", customer);
        }

        public IActionResult OnPostDeleteCustomer(int customerId)
        {
            InitializeTempId();

            var customers = Customers;

            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            if (customer != null)
            {
                if (customer.IsAdded)
                {
                    // If the customer is newly added and not yet saved, remove it from the list
                    customers.Remove(customer);
                    Customers = customers; // Update session

                    // Remove the row from the table
                    return new EmptyResult();
                }
                else
                {
                    customer.IsDeleted = true;
                    Customers = customers; // Update session

                    // Return the deleted row partial
                    return Partial("Partials/_CustomerDeletedRowPartial", customer);
                }
            }

            return BadRequest();
        }

        public IActionResult OnPostUndoDelete(int customerId)
        {
            InitializeTempId();

            var customers = Customers;

            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            if (customer != null)
            {
                customer.IsDeleted = false;
                Customers = customers; // Update session
            }

            return Partial("Partials/_CustomerRowPartial", customer);
        }

        public async Task<IActionResult> OnPostSaveCustomerAsync(int customerId)
        {
            InitializeTempId();

            var customers = Customers;

            var customer = customers.FirstOrDefault(c => c.Id == customerId);

            if (customer == null)
            {
                // Handle error
                return BadRequest();
            }

            var index = customer.Id.ToString();

            if (!await TryUpdateModelAsync(customer, $"Customers[{index}]"))
            {
                return Partial("Partials/_CustomerEditRowPartial", customer);
            }

            if (!ModelState.IsValid)
            {
                return Partial("Partials/_CustomerEditRowPartial", customer);
            }

            customer.IsEditing = false;
            if (!customer.IsAdded)
            {
                customer.IsEdited = true;
            }

            Customers = customers; // Update session

            return Partial("Partials/_CustomerRowPartial", customer);
        }

        public IActionResult OnPostSaveChanges()
        {
            InitializeTempId();
            return Partial("Partials/_ConfirmationModalPartial", this);
        }

        public async Task<IActionResult> OnPostConfirmSaveChangesAsync()
        {
            InitializeTempId();

            if (!ModelState.IsValid)
            {
                // Return the modal again with validation errors if any
                return Partial("Partials/_ConfirmationModalPartial", this);
            }

            await ProcessChangesAsync();

            var customerDtos = await _customerService.GetCustomersAsync();
            var customerViewModels = customerDtos.Select(MapToViewModel).ToList();

            // Update the session with the latest data
            Customers = customerViewModels;

            // Reset the temporary ID counter
            _nextTempId = -1;
            HttpContext.Session.SetInt32("NextTempId", _nextTempId);

            // Return the customer table partial to replace the table body
            return Partial("Partials/_CustomerTablePartial", customerViewModels);
        }

        private async Task ProcessChangesAsync()
        {
            var customers = Customers;

            foreach (var customer in customers.ToList())
            {
                var customerDto = MapToDto(customer);

                try
                {
                    if (customer.IsAdded)
                    {
                        await _customerService.AddCustomerAsync(customerDto);
                    }
                    else if (customer.IsEdited)
                    {
                        await _customerService.UpdateCustomerAsync(customerDto);
                    }
                    else if (customer.IsDeleted)
                    {
                        await _customerService.DeleteCustomerAsync(customerDto.Id);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    _logger.LogError(ex, $"Error processing customer ID {customerDto.Id}: {ex.Message}");
                    // You may choose to handle the exception differently
                    throw;
                }
            }
        }

        private int GenerateTempId()
        {
            _nextTempId--;
            HttpContext.Session.SetInt32("NextTempId", _nextTempId);
            return _nextTempId;
        }

        private CustomerViewModel MapToViewModel(CustomerDto dto)
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

        private CustomerDto MapToDto(CustomerViewModel vm)
        {
            return new CustomerDto
            {
                Id = vm.Id > 0 ? vm.Id : 0, // Set Id to 0 if it's a new customer
                FirstName = vm.FirstName,
                Surname = vm.Surname,
                Email = vm.Email,
                PhoneNumber = vm.PhoneNumber
            };
        }
    }
}
