using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITransactionService
{
    public Task<Result<TransactionDto>> AddTransaction(AddTransactionDto dto);
    public Result<List<TransactionDto>> GetCustomerTransactions(string accountId, Filter filter);
}