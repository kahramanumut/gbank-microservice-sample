using System.Collections.Generic;
using Duende.IdentityServer.Models;

public class Resources
{
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new[]
        {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
    }

    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new[]
        {
                new ApiResource
                {
                    Name = "customerapi",
                    DisplayName = "Customer API",
                    Description = "Allow the application to access Customer API",
                    Scopes = new List<string> { ConfigConstants.CustomerApiReadScope, ConfigConstants.CustomerApiWriteScope},
                    ApiSecrets = new List<Secret> {new Secret("GBankCustomerSecret".Sha256())},
                    UserClaims = new List<string> {"role"}
                },
                new ApiResource
                {
                    Name = "accountapi",
                    DisplayName = "Account API",
                    Description = "Allow the application to access Account API",
                    Scopes = new List<string> { ConfigConstants.AccountApiReadScope, ConfigConstants.AccountApiWriteScope },
                    ApiSecrets = new List<Secret> {new Secret("GBankAccountSecret".Sha256())},
                    UserClaims = new List<string> {"role"}
                },
                new ApiResource
                {
                    Name = "transactionapi",
                    DisplayName = "Transaction API",
                    Description = "Allow the application to access Transaction API",
                    Scopes = new List<string> { ConfigConstants.TransactionApiReadScope, ConfigConstants.TransactionApiWriteScope },
                    ApiSecrets = new List<Secret> {new Secret("GBankTransactionSecret".Sha256())},
                    UserClaims = new List<string> {"role"}
                }
            };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new[]
        {       
                //Customer
                new ApiScope(ConfigConstants.CustomerApiReadScope, "Read Access to Customer API"),
                new ApiScope(ConfigConstants.CustomerApiWriteScope, "Write Access to Customer API"),

                //Account
                new ApiScope(ConfigConstants.AccountApiReadScope, "Read Access to Account API"),
                new ApiScope(ConfigConstants.AccountApiWriteScope, "Write Access to Account API"),

                //Transaction
                new ApiScope(ConfigConstants.TransactionApiReadScope, "Read Access to Transaction API"),
                new ApiScope(ConfigConstants.TransactionApiWriteScope, "Write Access to Transaction API"),
            };
    }
}