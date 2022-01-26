using Microsoft.EntityFrameworkCore;

public class AccountDbContext:DbContext
{
    public AccountDbContext(DbContextOptions<AccountDbContext> options):base(options)
    {
        
    }

    public DbSet<Account> Accounts { get; set; }

}