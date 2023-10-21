using Coravel.Invocable;
using Newtonsoft.Json;
using ProductCatalog.API.DTO.Response.DollarRate;

namespace ProductCatalog.API.Invocables;

public class DollarExchangeRateChecker : IInvocable
{
    private readonly HttpClient _httpClient;
    private decimal _dollarRate;
    private int _retryIntervalInMinutes = 60;

    public async Task Invoke()
    {
        try
        {
            var response = await _httpClient.GetAsync("https://api.nbrb.by/exrates/rates/431");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ExchangeRateResponse>(content);

            decimal dollarRate = result.Cur_OfficialRate;
        }
        catch (Exception ex)
        {
            await Task.Delay(TimeSpan.FromMinutes(_retryIntervalInMinutes));
            await Invoke();
        }
    }
}