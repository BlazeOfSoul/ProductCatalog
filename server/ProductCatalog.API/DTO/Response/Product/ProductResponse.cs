namespace ProductCatalog.API.DTO.Response;

public class ProductResponse
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public double PriceInRubles { get; set; }

    public string? GeneralNote { get; set; }

    public string? SpecialNote { get; set; }

    public string? Category { get; set; }
}