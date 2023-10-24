using Newtonsoft.Json;

namespace ProductCatalog.API.DTO.Response.DollarRate;

public class ExchangeRateResponse
{
    [JsonProperty("Cur_ID")] public int CurID { get; set; }

    [JsonProperty("Date")] public DateTime Date { get; set; }

    [JsonProperty("Cur_Abbreviation")] public string CurAbbreviation { get; set; }

    [JsonProperty("Cur_Scale")] public int CurScale { get; set; }

    [JsonProperty("Cur_Name")] public string CurName { get; set; }

    [JsonProperty("Cur_OfficialRate")] public decimal CurOfficialRate { get; set; }
}