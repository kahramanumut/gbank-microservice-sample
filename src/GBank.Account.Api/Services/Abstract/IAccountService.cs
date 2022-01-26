using System.Collections.Generic;

public interface IAccountService
{
    Result HasExistAccount(string accountId);
    Result<List<AccountDto>> GetAccountsByCustomerId(string customerId);
    Result<AccountDto> AddAccount(AddAccountDto newAccount);
}