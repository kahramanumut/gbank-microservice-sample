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
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<AccountDbContext>();

            if(!dbContext.Accounts.Any())
            {
                List<Account> accounts = new List<Account>
                {
                    new Account { Id = "EEB4A2A3-E8AF-4A77-8FC8-029474804033", CustomerId = "1C7E3544-3F8D-4CF0-85CB-1E6472AABC63", CreationTime = DateTime.Now.AddDays(-45) }, //David Fowler
                    new Account { Id = "72C48C1D-BDB0-41DB-8575-68321A1A3CA9", CustomerId = "D4013BB6-2CC0-4305-A6E2-570FED4EE9D8", CreationTime = DateTime.Now.AddDays(-23) }, //Scott Hanselman 
                    new Account { Id = "ADC8E698-F544-4203-A084-21EDDABACC68", CustomerId = "C4FCF5DE-DDF3-492B-9A59-97898AE23D69", CreationTime = DateTime.Now.AddDays(-12) }, //Uncle Bob
                    new Account { Id = "1EB8C173-28BE-4074-A30E-B51BC3B87B21", CustomerId = "127F1619-53A7-4F6B-9B00-2B0CD6BD0056", CreationTime = DateTime.Now.AddDays(-68) }, //Umut Kahraman
                };

                dbContext.Accounts.AddRange(accounts);
                dbContext.SaveChanges();
            }
        }
    }
}