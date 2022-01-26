using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class AccountClient
{
    private readonly HttpClient _httpClient;
    public AccountClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("AccountClient");
    }

    public async Task<Result> AddNewAccount(string customerId)
    {
        var json = JsonSerializer.Serialize(new { CustomerId = customerId });
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await _httpClient.PostAsync($"/api/account/", data);
        if(result.StatusCode == System.Net.HttpStatusCode.OK)
            return new Result(true,"Account created");
        
        return new Result(false, "Account could not create");
    }
}