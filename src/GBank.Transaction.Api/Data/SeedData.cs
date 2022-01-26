using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class SeedData
{
    public static void InitializeSampleData(this IApplicationBuilder app)
    {
        using(var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<TransactionDbContext>();
            var transactionService = serviceScope.ServiceProvider.GetRequiredService<ITransactionService>();

            if(!dbContext.Transactions.Any())
            {
                //"EEB4A2A3-E8AF-4A77-8FC8-029474804033" //David Fowler
                //"72C48C1D-BDB0-41DB-8575-68321A1A3CA9" //Scott Hanselman 
                //"ADC8E698-F544-4203-A084-21EDDABACC68" //Uncle Bob
                //"1EB8C173-28BE-4074-A30E-B51BC3B87B21" //Umut Kahraman

                List<AddTransactionDto> transactions = new List<AddTransactionDto>
                {
                    new AddTransactionDto { AccountId = "EEB4A2A3-E8AF-4A77-8FC8-029474804033", Amount = 100 },
                    new AddTransactionDto { AccountId = "EEB4A2A3-E8AF-4A77-8FC8-029474804033", Amount = -80 },
                    new AddTransactionDto { AccountId = "EEB4A2A3-E8AF-4A77-8FC8-029474804033", Amount = -20 },
                    new AddTransactionDto { AccountId = "72C48C1D-BDB0-41DB-8575-68321A1A3CA9", Amount = 400 },
                    new AddTransactionDto { AccountId = "72C48C1D-BDB0-41DB-8575-68321A1A3CA9", Amount = -220 },
                    new AddTransactionDto { AccountId = "ADC8E698-F544-4203-A084-21EDDABACC68", Amount = 1020 },
                    new AddTransactionDto { AccountId = "1EB8C173-28BE-4074-A30E-B51BC3B87B21", Amount = 3300 },
                    new AddTransactionDto { AccountId = "1EB8C173-28BE-4074-A30E-B51BC3B87B21", Amount = 5020 },
                    new AddTransactionDto { AccountId = "1EB8C173-28BE-4074-A30E-B51BC3B87B21", Amount = 16720 },
                    new AddTransactionDto { AccountId = "1EB8C173-28BE-4074-A30E-B51BC3B87B21", Amount = -12330 },
                };

                foreach (var item in transactions)
                {
                    transactionService.AddTransaction(item);
                }
            }
        }
    }
}