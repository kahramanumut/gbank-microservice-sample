using System.Collections.Generic;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

public class Clients
{
    public static IEnumerable<Client> Get()
    {
        return new List<Client>
            {
                new Client
                {
                    ClientId = "customerClient",
                    ClientName = "Customer client application using client credentials",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret("GBankCustomerSecret".Sha256())},
                    AllowedScopes = new List<string> {ConfigConstants.CustomerApiReadScope , ConfigConstants.CustomerApiWriteScope}
                },
                new Client
                {
                    ClientId = "accountClient",
                    ClientName = "Account client application using client credentials",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret("GBankAccountSecret".Sha256())},
                    AllowedScopes = new List<string> {ConfigConstants.AccountApiReadScope , ConfigConstants.AccountApiWriteScope}
                },
                new Client
                {
                    ClientId = "transactionClient",
                    ClientName = "Transaction client application using client credentials",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret("GBankTransactionSecret".Sha256())},
                    AllowedScopes = new List<string> {ConfigConstants.TransactionApiReadScope , ConfigConstants.TransactionApiWriteScope}
                }
            };
    }
}