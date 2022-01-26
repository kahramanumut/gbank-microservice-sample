using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICustomerService
{
    Result<CustomerDto> GetCustomerById(string customerId);
    Result<List<CustomerDto>> GetAllCustomers();
    Result AddNewCustomer(AddCustomerDto newCustomer);
}