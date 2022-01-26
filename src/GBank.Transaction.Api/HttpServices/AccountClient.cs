using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class AccountClient
{
    private readonly HttpClient _httpClient;
    public AccountClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("AccountClient");
    }

    public async Task<Result> HasExistAccount(string accountId)
    {
        var result = await _httpClient.GetAsync($"/api/account/{accountId}");
        if(result.StatusCode == System.Net.HttpStatusCode.OK)
            return new Result(true,"There is a account");
        
        return new Result(false, "There isn't any account");
    }
}