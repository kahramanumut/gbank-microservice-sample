using System;
using System.Collections.Generic;
using System.Linq;
using MapsterMapper;

public class AccountService : IAccountService
{
    private readonly AccountDbContext _dbContext;
    private readonly IMapper _mapper;

    public AccountService(AccountDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public Result<AccountDto> AddAccount(AddAccountDto newAccount)
    {
        Account account = new Account
        {
            CustomerId = newAccount.CustomerId
        };

        _dbContext.Accounts.Add(account);
        _dbContext.SaveChanges();

        return new Result<AccountDto>(_mapper.Map<Account, AccountDto>(account), true, "New account added");
    }

    public Result<List<AccountDto>> GetAccountsByCustomerId(string customerId)
    {
        List<Account> customerAccounts = _dbContext.Accounts.Where(x => x.Id == customerId.ToUpper()).ToList();
        if(customerAccounts.Count == 0)
            return new Result<List<AccountDto>>(null, false, "Account information could not found");
        
        List<AccountDto> data = _mapper.Map<List<Account>, List<AccountDto>>(customerAccounts);
        return new Result<List<AccountDto>>(data, true, "Accounts information has got successfully");
    }

    public Result HasExistAccount(string accountId)
    {
        bool exist = _dbContext.Accounts.Where(x => x.Id == accountId.ToUpper()).Any();

        if(exist)
            return new Result(true, "There is a account");
        
        return new Result(false, "There isn't any account");
    }
}