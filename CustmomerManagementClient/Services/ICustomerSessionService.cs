using CustomerManagementClient.Models;

public interface ICustomerSessionService
{
    List<CustomerViewModel> GetCustomers();
    void SetCustomers(List<CustomerViewModel> customers);
    int GetNextTempId();
    void SetNextTempId(int nextTempId);
}
