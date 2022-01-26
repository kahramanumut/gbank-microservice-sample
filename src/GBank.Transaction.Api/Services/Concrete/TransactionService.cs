using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

public class TransactionService : ITransactionService
{

    private readonly TransactionDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly AccountClient _accountClient;
    public TransactionService(TransactionDbContext dbContext, IMapper mapper, AccountClient accountClient)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _accountClient = accountClient;
    }

    public async Task<Result<TransactionDto>> AddTransaction(AddTransactionDto dto)
    {
        Result accountResult = await _accountClient.HasExistAccount(dto.AccountId);

        if(!accountResult.IsSuccess)
            return new Result<TransactionDto>(null, false, "There is not any Account");

        Transaction existTransaction = _dbContext.Transactions.Where(x => x.AccountId == dto.AccountId).AsNoTracking().LastOrDefault();
        Transaction newTransaction = _mapper.Map<AddTransactionDto, Transaction>(dto);
        
        if(existTransaction == null)
            newTransaction.Balance = dto.Amount;
        else
            newTransaction.Balance = existTransaction.Balance + dto.Amount;
        
        newTransaction.Type = dto.Amount < 0 ? TransactionType.Withdraw : TransactionType.Adding;

        _dbContext.Transactions.Add(newTransaction);
        _dbContext.SaveChanges();

        TransactionDto lastTransaction = _mapper.Map<Transaction, TransactionDto>(newTransaction);
        return new Result<TransactionDto>(lastTransaction, true,"New Transaction added successfully");
    }

    public Result<List<TransactionDto>> GetCustomerTransactions(string accountId, Filter filter)
    {
        var transactionQuery = _dbContext.Transactions
                            .Where(x => x.AccountId == accountId.ToUpper())
                            .OrderByDescending(x => x.Id)
                            .AsQueryable();

        if(filter.StartDate != null)
            transactionQuery = transactionQuery.Where(x => filter.StartDate.Value.Date <= x.CreationTime.Date);
        if(filter.FinishDate != null)
            transactionQuery = transactionQuery.Where(x => filter.FinishDate.Value.Date >= x.CreationTime.Date);

        List<Transaction> transactions = transactionQuery.Take(filter.Limit).ToList();
        
        var data = _mapper.Map<List<Transaction>, List<TransactionDto>>(transactions);
        return new Result<List<TransactionDto>>(data, true, "Transaction informations has retrieved successfully");
    }
}