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
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<CustomerDbContext>();

            if(!dbContext.Customers.Any())
            {
                List<Customer> customers = new List<Customer>
                {
                    new Customer { Id = "1C7E3544-3F8D-4CF0-85CB-1E6472AABC63", Name = "David", Surname = "Fowler", Age = 30 },
                    new Customer { Id = "D4013BB6-2CC0-4305-A6E2-570FED4EE9D8", Name = "Scott", Surname = "Hanselman", Age = 23 },
                    new Customer { Id = "C4FCF5DE-DDF3-492B-9A59-97898AE23D69", Name = "Uncle", Surname = "Bob", Age = 45 },
                    new Customer { Id = "127F1619-53A7-4F6B-9B00-2B0CD6BD0056", Name = "Umut", Surname = "Kahraman", Age = 30 },
                };

                dbContext.Customers.AddRange(customers);
                dbContext.SaveChanges();
            }
        }
    }
}