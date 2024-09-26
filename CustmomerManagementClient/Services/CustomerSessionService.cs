using CustomerManagementClient.Helpers;
using CustomerManagementClient.Models;

public class CustomerSessionService : ICustomerSessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomerSessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public List<CustomerViewModel> GetCustomers()
    {
        var customers = _httpContextAccessor.HttpContext.Session.GetObject<List<CustomerViewModel>>("Customers");
        if (customers == null)
        {
            customers = new List<CustomerViewModel>();
            SetCustomers(customers);
        }
        return customers;
    }

    public void SetCustomers(List<CustomerViewModel> customers)
    {
        _httpContextAccessor.HttpContext.Session.SetObject("Customers", customers);
    }

    public int GetNextTempId()
    {
        return _httpContextAccessor.HttpContext.Session.GetInt32("NextTempId") ?? -1;
    }

    public void SetNextTempId(int nextTempId)
    {
        _httpContextAccessor.HttpContext.Session.SetInt32("NextTempId", nextTempId);
    }
}
