using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class SeedData
{
    public static void InitializeSampleData(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<TransactionDbContext>();

            if (!dbContext.Transactions.Any())
            {
                //"EEB4A2A3-E8AF-4A77-8FC8-029474804033" //David Fowler
                //"72C48C1D-BDB0-41DB-8575-68321A1A3CA9" //Scott Hanselman 
                //"ADC8E698-F544-4203-A084-21EDDABACC68" //Uncle Bob
                //"1EB8C173-28BE-4074-A30E-B51BC3B87B21" //Umut Kahraman

                List<Transaction> transactions = new List<Transaction>
                {
                    new Transaction { AccountId = "EEB4A2A3-E8AF-4A77-8FC8-029474804033", Amount = 100, Balance = 100, Type = TransactionType.Adding },
                    new Transaction { AccountId = "EEB4A2A3-E8AF-4A77-8FC8-029474804033", Amount = -80, Balance = 20,  Type = TransactionType.Withdraw },
                    new Transaction { AccountId = "EEB4A2A3-E8AF-4A77-8FC8-029474804033", Amount = -20, Balance = 0,   Type = TransactionType.Withdraw },
                    new Transaction { AccountId = "72C48C1D-BDB0-41DB-8575-68321A1A3CA9", Amount = 400, Balance = 400, Type = TransactionType.Adding  },
                    new Transaction { AccountId = "72C48C1D-BDB0-41DB-8575-68321A1A3CA9", Amount = -220, Balance = 180, Type = TransactionType.Withdraw   },
                    new Transaction { AccountId = "ADC8E698-F544-4203-A084-21EDDABACC68", Amount = 1020, Balance = 1020, Type = TransactionType.Adding },
                    new Transaction { AccountId = "1EB8C173-28BE-4074-A30E-B51BC3B87B21", Amount = 3300, Balance = 3300, Type = TransactionType.Adding },
                    new Transaction { AccountId = "1EB8C173-28BE-4074-A30E-B51BC3B87B21", Amount = 5020, Balance = 13400, Type = TransactionType.Adding },
                    new Transaction { AccountId = "1EB8C173-28BE-4074-A30E-B51BC3B87B21", Amount = 16720, Balance = 30120, Type = TransactionType.Adding },
                    new Transaction { AccountId = "1EB8C173-28BE-4074-A30E-B51BC3B87B21", Amount = -12330, Balance = 17790, Type = TransactionType.Withdraw },
                };

                dbContext.Transactions.AddRange(transactions);
                dbContext.SaveChanges();
            }
        }
    }
}