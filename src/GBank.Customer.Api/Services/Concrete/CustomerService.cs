using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapsterMapper;

public class CustomerService : ICustomerService
{
    private readonly CustomerDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly AccountClient _accountClient;
    private readonly AccountMessageSender _accountMessageSender;

    public CustomerService(CustomerDbContext dbContext, IMapper mapper, AccountClient accountClient, AccountMessageSender accountMessageSender)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _accountClient = accountClient;
        _accountMessageSender = accountMessageSender;
    }

    public Result<List<CustomerDto>> GetAllCustomers()
    {
        List<Customer> customers = _dbContext.Customers.ToList();
        if (customers.Count == 0)
            return new Result<List<CustomerDto>>(null, false, "Customers infomation could not found");

        var data = _mapper.Map<List<Customer>, List<CustomerDto>>(customers);
        return new Result<List<CustomerDto>>(data, true, "Customers information has got successfully");
    }

    public Result<CustomerDto> GetCustomerById(string customerId)
    {
        Customer existCustomer = _dbContext.Customers.Where(x => x.Id == customerId.ToUpper()).FirstOrDefault();
        if (existCustomer == null)
            return new Result<CustomerDto>(null, false, "Customer information could not found");

        CustomerDto data = _mapper.Map<Customer, CustomerDto>(existCustomer);
        return new Result<CustomerDto>(data, true, "Customer information has got successfully");
    }

    public Result AddNewCustomer(AddCustomerDto newCustomer)
    {
        Customer customer = _mapper.Map<AddCustomerDto, Customer>(newCustomer);

        //TODO: Add Transaction, Transactions are not supported by the in-memory store
        // So if an error occur, delete customer 
        _dbContext.Customers.Add(customer);
        _dbContext.SaveChanges();

        //Before designed by HttpClient, but after changed with Message Queue
        //Result accountCreated = await _accountClient.AddNewAccount(customer.Id);

        _accountMessageSender.SendAccount(customer.Id);
        return new Result(true, "New customer and account added");

    }

}