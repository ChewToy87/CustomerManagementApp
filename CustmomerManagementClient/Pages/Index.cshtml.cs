using CustomerManagementClient.Models;
using CustomerManagementClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomerManagementClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerSessionService _sessionService;
        private readonly ICustomerMapper _mapper;
        private readonly ILogger<IndexModel> _logger;

        public string PageTitle { get; set; } = "Customer Management";

        public IndexModel(
            ICustomerService customerService,
            ICustomerSessionService sessionService,
            ICustomerMapper mapper,
            ILogger<IndexModel> logger)
        {
            _customerService = customerService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
        }

        public List<CustomerViewModel> Customers
        {
            get => _sessionService.GetCustomers();
            set => _sessionService.SetCustomers(value);
        }

        private int NextTempId
        {
            get => _sessionService.GetNextTempId();
            set => _sessionService.SetNextTempId(value);
        }

        private void InitializeTempId()
        {
            NextTempId = _sessionService.GetNextTempId() == -1 ? 0 : _sessionService.GetNextTempId();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            InitializeTempId();

            var customerDtos = await _customerService.GetCustomersAsync();
            var customerViewModels = customerDtos.Select(_mapper.MapToViewModel).ToList();

            Customers = customerViewModels;

            return Page();
        }

        public IActionResult OnPostAddCustomer()
        {
            InitializeTempId();

            var newCustomer = new CustomerViewModel
            {
                Id = GenerateTempId(),
                IsAdded = true,
                IsEditing = true
            };

            var customers = Customers;
            customers.Add(newCustomer);
            Customers = customers;

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
                Customers = customers;
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
                if (customer.IsAdded)
                {
                    customers.Remove(customer);
                    Customers = customers;
                    return new EmptyResult();
                }
                else
                {
                    customer.IsEditing = false;
                    Customers = customers;
                    return Partial("Partials/_CustomerRowPartial", customer);
                }
            }

            return BadRequest();
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
                    customers.Remove(customer);
                    Customers = customers;
                    return new EmptyResult();
                }
                else
                {
                    customer.IsDeleted = true;
                    Customers = customers;
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
                Customers = customers;
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

            Customers = customers;

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
                return Partial("Partials/_ConfirmationModalPartial", this);
            }

            await ProcessChangesAsync();

            var customerDtos = await _customerService.GetCustomersAsync();
            var customerViewModels = customerDtos.Select(_mapper.MapToViewModel).ToList();

            Customers = customerViewModels;

            NextTempId = -1;
            _sessionService.SetNextTempId(NextTempId);

            return Partial("Partials/_CustomerTablePartial", customerViewModels);
        }

        private async Task ProcessChangesAsync()
        {
            var customers = Customers;

            foreach (var customer in customers.ToList())
            {
                var customerDto = _mapper.MapToDto(customer);

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
                    _logger.LogError(ex, $"Error processing customer ID {customerDto.Id}: {ex.Message}");
                    throw;
                }
            }
        }

        private int GenerateTempId()
        {
            NextTempId--;
            _sessionService.SetNextTempId(NextTempId);
            return NextTempId;
        }
    }
}
