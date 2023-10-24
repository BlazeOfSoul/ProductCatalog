using Coravel.Invocable;
using Newtonsoft.Json;
using ProductCatalog.API.DTO.Response.DollarRate;

namespace ProductCatalog.API.Invocables;

public class DollarExchangeRateChecker : IInvocable
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DollarExchangeRateChecker> _logger;
    private readonly int _retryIntervalInMinutes = 60;
    private decimal _dollarRate;

    public DollarExchangeRateChecker(IHttpClientFactory httpClientFactory, ILogger<DollarExchangeRateChecker> logger)
    {
        _httpClient = httpClientFactory.CreateClient();
        _logger = logger;
    }

    public async Task Invoke()
    {
        try
        {
            var response = await _httpClient.GetAsync("https://api.nbrb.by/exrates/rates/431");

            if (response.IsSuccessStatusCode)
                _logger.LogWarning("Failed to get the dollar exchange rate. Status code: {statusCode}",
                    (int)response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ExchangeRateResponse>(content);

            _dollarRate = result.CurOfficialRate;
            _logger.LogInformation(
                "The current dollar exchange rate is received. Time - '{dateTime}'. Rate - '{rate}'",
                DateTime.UtcNow, _dollarRate);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning("An error occurred while making the HTTP request: {errorMessage}", ex.Message);
            _logger.LogWarning("A second attempt to get the rate will be made in 1 hour.");
            await Task.Delay(TimeSpan.FromMinutes(_retryIntervalInMinutes));
            await Invoke();
        }
        catch (Exception ex)
        {
            _logger.LogError("An unexpected error occurred: {errorMessage}", ex.Message);
        }
    }

    public decimal GetDollarRate()
    {
        return _dollarRate;
    }
}