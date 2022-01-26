using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICustomerService
{
    Result<CustomerDto> GetCustomerById(string customerId);
    Result<List<CustomerDto>> GetAllCustomers();
    Task<Result> AddNewCustomer(AddCustomerDto newCustomer);
}